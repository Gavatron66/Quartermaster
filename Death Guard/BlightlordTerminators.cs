using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class BlightlordTerminators : Datasheets
    {
        int currentIndex = 0;

        public BlightlordTerminators()
        {
            DEFAULT_POINTS = 40;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            
            for(int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Combi-bolter");
                Weapons.Add("Balesword");
            }

            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "CORE", "BUBONIC ASTARTES", "TERMINATOR", "BLIGHTLORD TERMINATORS"
            });
            Role = "Elites";
        }
        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            factionsRestrictions = repo.restrictedItems;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFactionUpgrade = panel.Controls["cmbFactionUpgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Blightlord Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            int j = 2;
            for (int i = 0; i < UnitSize - 1; i++)
            {
                lbModelSelect.Items.Add("Blightlord Terminator w/ " + Weapons[j] + " and " + Weapons[j+1]);
                j += 2;
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Balesword",
                "Bubotic Axe"
            });

            cbOption1.Text = "Flail of Corruption";

            cmbOption1.Visible = false;
            cmbOption2.Visible = false;
            panel.Controls["lblOption1"].Visible = false;
            panel.Controls["lblOption2"].Visible = false;
            cbOption1.Visible = false;

            cmbFactionUpgrade.Items.Clear();
            cmbFactionUpgrade.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFactionUpgrade.SelectedIndex = cmbFactionUpgrade.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFactionUpgrade.SelectedIndex = 0;
            }

            restrictedIndexes = new List<int>();
            for (int i = 0; i < cmbFactionUpgrade.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(cmbFactionUpgrade.Items[i]) && Factionupgrade != cmbFactionUpgrade.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbFactionUpgrade);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFactionUpgrade = panel.Controls["cmbFactionUpgrade"] as ComboBox;

            switch (code)
            {
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = Weapons.Count;

                    if(temp / 2 < UnitSize)
                    {
                        for(int i = temp / 2; i < UnitSize; i++)
                        {
                            Weapons.Add("Combi-bolter");
                            Weapons.Add("Balesword");
                            lbModelSelect.Items.Add("Blightlord Terminator w/ " +
                            Weapons[temp] + " and " + Weapons[temp + 1]);
                            temp += 2;
                        }
                    }

                    if(temp / 2 > UnitSize)
                    {
                        int itemsToRemove = (temp / 2) - UnitSize;
                        for(int i = temp / 2; i > UnitSize; i--)
                        {
                            lbModelSelect.Items.RemoveAt(i - 1);
                            Weapons.RemoveAt(temp - 1);
                            Weapons.RemoveAt(temp - 2);
                        }
                    }
                    break;
                case 11:
                    if(currentIndex == 0)
                    {
                        if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                        {
                            Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Blightlord Champion w/ " +
                                Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                        }
                        break;
                    }

                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Blightlord Terminator w/ " +
                            Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Blightlord Champion w/ " +
                            Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                        break;
                    }

                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Blightlord Terminator w/ " +
                        Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[currentIndex * 2] = cbOption1.Text;
                        lbModelSelect.Items[currentIndex] = "Blightlord Terminator w/ Flail of Corruption";
                        cmbOption1.Enabled = false;
                        cmbOption2.Enabled = false;
                    } else
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        Weapons[currentIndex * 2] = "Combi-bolter";
                        Weapons[(currentIndex * 2) + 1] = "Balesword";
                        lbModelSelect.Items[currentIndex] = "Blightlord Terminator w/ " +
                            Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }

                    break;
                case 16:
                    if (!factionsRestrictions.Contains(cmbFactionUpgrade.Text))
                    {
                        if (Factionupgrade == "(None)")
                        {
                            Factionupgrade = cmbFactionUpgrade.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(Factionupgrade);
                            Factionupgrade = cmbFactionUpgrade.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                    }
                    else
                    {
                        cmbFactionUpgrade.SelectedIndex = cmbFactionUpgrade.Items.IndexOf(Factionupgrade);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    if(currentIndex == -1)
                    {
                        break;
                    }

                    if(currentIndex == 0)
                    {
                        LoadChampion(panel, true);
                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        antiLoop = false;
                        break;
                    } else
                    {
                        LoadChampion(panel, false);
                        if (Weapons[currentIndex * 2] == "Flail of Corruption")
                        {
                            cbOption1.Checked = true;
                        }
                        else
                        {
                            antiLoop = true;
                            cmbOption1.Text = Weapons[currentIndex * 2];
                            cmbOption2.SelectedItem = Weapons[(currentIndex * 2) + 1];
                            antiLoop = false;
                            cbOption1.Checked = false;
                        }
                        break;
                    }
            }

            Points = UnitSize * DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Blightlord Terminators - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new BlightlordTerminators();
        }

        private void LoadChampion(Panel panel, bool isChampion)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Visible = true;
            cmbOption2.Visible = true;
            panel.Controls["lblOption1"].Visible = true;
            panel.Controls["lblOption2"].Visible = true;

            restrictedIndexes = new List<int>();
            int[] ConstraintArray = new int[6] { 0, 0, 0, 0, 0, 0 };
            //Combi-flamer, Combi-melta, Combi-plasma
            //Blight Launcher/Reaper Autocanonn, Plague Spewer, Flail of Corruption

            foreach (string Weapon in Weapons)
            {
                if(Weapon == "Combi-flamer")
                {
                    ConstraintArray[0] += 1;
                }
                else if (Weapon == "Combi-melta")
                {
                    ConstraintArray[1] += 1;
                }
                else if (Weapon == "Combi-plasma")
                {
                    ConstraintArray[2] += 1;
                }
                else if(Weapon == "Blight Launcher" || Weapon == "Reaper Autocannon")
                {
                    ConstraintArray[3] += 1;
                }
                else if(Weapon == "Plague Spewer")
                {
                    ConstraintArray[4] += 1;
                }
                else if(Weapon == "Flail of Corruption")
                {
                    ConstraintArray[5] += 1;
                }
            }

            if (isChampion)
            {
                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Combi-bolter",
                    "Combi-flamer",
                    "Combi-melta",
                    "Combi-plasma"
                });

                if (ConstraintArray[0] == UnitSize / 5)
                {
                    restrictedIndexes.Add(1);
                }
                if (ConstraintArray[1] == UnitSize / 5)
                {
                    restrictedIndexes.Add(2);
                }
                if (ConstraintArray[2] == UnitSize / 5)
                {
                    restrictedIndexes.Add(3);
                }

                if (restrictedIndexes.Contains(cmbOption1.Items.IndexOf(Weapons[currentIndex * 2])))
                {
                    if (Weapons[currentIndex * 2] == "Combi-flamer")
                    {
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Combi-flamer"));
                    }
                    else if (Weapons[currentIndex * 2] == "Combi-melta")
                    {
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Combi-melta"));
                    }
                    else if (Weapons[currentIndex * 2] == "Combi-plasma")
                    {
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Combi-plasma"));
                    }
                }

                cbOption1.Visible = false;
                panel.Controls["lblFactionUpgrade"].Visible = true;
                panel.Controls["cmbFactionUpgrade"].Visible = true;
            }

            if (!isChampion)
            {
                panel.Controls["lblFactionUpgrade"].Visible = false;
                panel.Controls["cmbFactionUpgrade"].Visible = false;

                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Blight Launcher",
                    "Combi-bolter",
                    "Combi-flamer",
                    "Combi-melta",
                    "Combi-plasma",
                    "Plague Spewer",
                    "Reaper Autocannon"
                });

                cbOption1.Visible = true;

                if (ConstraintArray[0] == UnitSize / 5)
                {
                    restrictedIndexes.Add(2);
                }
                if (ConstraintArray[1] == UnitSize / 5)
                {
                    restrictedIndexes.Add(3);
                }
                if (ConstraintArray[2] == UnitSize / 5)
                {
                    restrictedIndexes.Add(4);
                }

                if (ConstraintArray[3] == UnitSize / 5)
                {
                    restrictedIndexes.Add(0);
                    restrictedIndexes.Add(6);
                }

                if (ConstraintArray[4] == UnitSize / 5)
                {
                    restrictedIndexes.Add(5);
                }

                if (ConstraintArray[5] == UnitSize / 5)
                {
                    cbOption1.Enabled = false;
                    if (Weapons[currentIndex * 2] == "Flail of Corruption" ||
                        Weapons[(currentIndex * 2) + 1] == "Flail of Corruption")
                    {
                        cbOption1.Enabled = true;
                    }
                } 
                else
                {
                    cbOption1.Enabled = true;
                }

                if (restrictedIndexes.Contains(cmbOption1.Items.IndexOf(Weapons[currentIndex * 2])))
                {
                    if (Weapons[currentIndex * 2] == "Combi-flamer")
                    {
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Combi-flamer"));
                    }
                    else if (Weapons[currentIndex * 2] == "Combi-melta")
                    {
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Combi-melta"));
                    }
                    else if (Weapons[currentIndex * 2] == "Combi-plasma")
                    {
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Combi-plasma"));
                    }
                    else if (Weapons[currentIndex * 2] == "Blight Launcher" || Weapons[currentIndex * 2] == "Reaper Autocannon")
                    {
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Blight Launcher"));
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Reaper Autocannon"));
                    }
                    else if (Weapons[currentIndex * 2] == "Plague Spewer")
                    {
                        restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Plague Spewer"));
                    }
                }

            }
            
            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }
}
