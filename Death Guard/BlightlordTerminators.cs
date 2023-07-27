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
        DeathGuard repo = new DeathGuard();
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
        }
        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
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
            lbModelSelect.Items.Add("Blightlord Champion - " + Weapons[0] + "/" + Weapons[1]);
            int j = 2;
            for (int i = 0; i < UnitSize - 1; i++)
            {
                lbModelSelect.Items.Add("Blightlord Terminator - " + Weapons[j] + "/" + Weapons[j+1]);
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
                            lbModelSelect.Items.Add("Blightlord Terminator - " +
                            Weapons[temp] + "/" + Weapons[temp + 1]);
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
                        Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Blightlord Champion - " +
                            Weapons[currentIndex * 2] + "/" + Weapons[(currentIndex * 2) + 1];
                        break;
                    }

                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Blightlord Terminator - " + 
                        Weapons[currentIndex * 2] + "/" + Weapons[(currentIndex * 2) + 1];
                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Blightlord Champion - " +
                            Weapons[currentIndex * 2] + "/" + Weapons[(currentIndex * 2) + 1];
                        break;
                    }

                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Blightlord Terminator - " +
                        Weapons[currentIndex * 2] + "/" + Weapons[(currentIndex * 2) + 1];
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[currentIndex * 2] = cbOption1.Text;
                        lbModelSelect.Items[currentIndex] = "Blightlord Terminator - Flail of Corruption";
                        cmbOption1.Enabled = false;
                        cmbOption2.Enabled = false;
                    } else
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        Weapons[currentIndex * 2] = "Combi-bolter";
                        Weapons[(currentIndex * 2) + 1] = "Balesword";
                        lbModelSelect.Items[currentIndex] = "Blightlord Terminator - " +
                            Weapons[currentIndex * 2] + "/" + Weapons[(currentIndex * 2) + 1];
                    }

                    break;
                case 16:
                    Factionupgrade = cmbFactionUpgrade.SelectedItem.ToString();
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

            for(int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i] == "Blight Launcher" || Weapons[i] == "Combi-flamer" ||
                    Weapons[i] == "Combi-melta" || Weapons[i] == "Combi-plasma" ||
                    Weapons[i] == "Flail of Corruption" || Weapons[i] == "Plague Spewer" ||
                    Weapons[i] == "Reaper Autocannon")
                {
                    Points += 5;
                }
            }

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

                int[] ConstraintArray = new int[3] { 0, 0, 0 };
                //Blight Launcher/Reaper Autocanonn, Plague Spewer, Flail of Corruption
                foreach (string Weapon in Weapons)
                {
                    if(Weapon == "Blight Launcher" || Weapon == "Reaper Autocannon")
                    {
                        ConstraintArray[0] += 1;
                    }
                    
                    if(Weapon == "Plague Spewer")
                    {
                        ConstraintArray[1] += 1;
                    }

                    if(Weapon == "Flail of Corruption")
                    {
                        ConstraintArray[2] += 1;
                    }
                }

                if (ConstraintArray[0] == UnitSize / 5)
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Blight Launcher"));
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Reaper Autocannon"));
                }

                if (ConstraintArray[1] == UnitSize / 5)
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Plague Spewer"));
                }

                if (ConstraintArray[2] == UnitSize / 5)
                {
                    cbOption1.Enabled = false;
                    if (Weapons[currentIndex * 2] == "Flail of Corruption" ||
                        Weapons[(currentIndex * 2) + 1] == "Flail of Corruption")
                    {
                        cbOption1.Enabled = true;
                    }
                } else
                {
                    cbOption1.Enabled = true;
                }
            }
        }
    }
}
