using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class PlagueMarines : Datasheets
    {
        DeathGuard repo = new DeathGuard();
        int currentIndex;

        public PlagueMarines()
        {
            DEFAULT_POINTS = 21;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m3k";

            Weapons.Add("Boltgun");
            Weapons.Add("Plague Knife");
            Weapons.Add("");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }

            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "CORE", "BUBONIC ASTARTES", "PLAGUE MARINES"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new PlagueMarines();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[2] == "")
            {
                lbModelSelect.Items.Add("Plague Champion - " + Weapons[0] + "/" + Weapons[1]);
            } else
            {
                lbModelSelect.Items.Add("Plague Champion - " + Weapons[0] + "/" + Weapons[1] + "with " + Weapons[2]);
            }

            for (int i = 3; i < Weapons.Count; i++)
            {
                lbModelSelect.Items.Add("Plague Marine - " + Weapons[i]);
            }

            cbOption1.Text = "Icon of Despair";
            cbOption2.Text = "Sigil of Decay";
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
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFactionUpgrade = panel.Controls["cmbFactionUpgrade"] as ComboBox;

            switch (code)
            {
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = Weapons.Count - 2;

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Boltgun");
                            lbModelSelect.Items.Add("Plague Marine - " + Weapons[i]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        for (int i = temp; i > UnitSize; i--)
                        {
                            lbModelSelect.Items.RemoveAt(i - 1);
                            Weapons.RemoveAt(i - 1);
                        }
                    }
                    break;
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion - " + Weapons[0] + "/" + Weapons[1]);
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion - " + Weapons[0] + "/" + Weapons[1] + "with " + Weapons[2]);
                        }
                        break;
                    }

                    Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Plague Marine - " + Weapons[currentIndex + 2];
                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion - " + Weapons[0] + "/" + Weapons[1]);
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion - " + Weapons[0] + "/" + Weapons[1] + "with " + Weapons[2]);
                        }
                        break;
                    }
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[currentIndex + 2] = cbOption1.Text;
                        cbOption2.Enabled = false;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        Weapons[currentIndex + 2] = "Boltgun";
                        cbOption2.Enabled = true;
                        cmbOption1.Enabled = true;
                    }
                    lbModelSelect.Items[currentIndex] = "Plague Marine - " + Weapons[currentIndex + 2];
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[currentIndex + 2] = cbOption2.Text;
                        cbOption1.Enabled = false;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        Weapons[currentIndex + 2] = "Boltgun";
                        cbOption1.Enabled = true;
                        cmbOption1.Enabled = true;
                    }
                    lbModelSelect.Items[currentIndex] = "Plague Marine - " + Weapons[currentIndex + 2];
                    break;
                case 16:
                    Factionupgrade = cmbFactionUpgrade.SelectedItem.ToString();
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    if (currentIndex == -1)
                    {
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        LoadChampion(panel, true);
                        break;
                    }

                    LoadChampion(panel, false);
                    break;
                case 23:
                    if (currentIndex == 0)
                    {
                        if (cbOption3.Checked)
                        {
                            Weapons[2] = cbOption3.Text;
                        }
                        else
                        {
                            Weapons[2] = "";
                        }

                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion - " + Weapons[0] + "/" + Weapons[1]);
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion - " + Weapons[0] + "/" + Weapons[1] + " with " + Weapons[2]);
                        }
                        break;
                    }
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

            for(int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i] == "Bubotic Axe")
                {
                    Points += 3;
                }

                if (Weapons[i] == "Mace of Contagion" || Weapons[i] == "Plague Belcher" || Weapons[i] == "Plasma Pistol")
                {
                    Points += 5;
                }

                if (Weapons[i] == "Blight Launcher" || Weapons[i] == "Flail of Corruption" || Weapons[i] == "Great Plague Cleaver" ||
                    Weapons[i] == "Icon of Despair" || Weapons[i] == "Meltagun" || Weapons[i] == "Plague Spewer" || 
                    Weapons[i] == "Plasma Gun" || Weapons[i] == "Power Fist" || Weapons[i] == "Sigil of Decay")
                {
                    Points += 10;
                }
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Plague Marines - " + Points + " pts";
        }

        private void LoadChampion(Panel panel, bool isChampion)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFactionUpgrade = panel.Controls["cmbFactionUpgrade"] as ComboBox;

            panel.Controls["lblOption1"].Visible = true;
            cmbOption1.Visible = true;

            if (isChampion)
            {
                panel.Controls["lblOption2"].Visible = true;
                cmbOption2.Visible = true;
                cbOption1.Visible = false;
                cbOption2.Visible = false;
                cbOption3.Visible = true;
                panel.Controls["lblFactionUpgrade"].Visible = true;
                cmbFactionUpgrade.Visible = true;
                cmbOption1.Enabled = true;

                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Boltgun",
                    "Bolt Pistol",
                    "Plasma Gun",
                    "Plasma Pistol"
                });
                antiLoop = true;
                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                cmbOption2.Items.Clear();
                cmbOption2.Items.AddRange(new string[]
                {
                    "Plague Knife",
                    "Daemonic Plague Blade"
                });
                cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                antiLoop = false;

                cbOption3.Text = "Power Fist";
                if (Weapons[2] == "Power Fist")
                {
                    cbOption3.Checked = true;
                } else
                {
                    cbOption3.Checked = false;
                }

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
            if (!isChampion)
            {
                panel.Controls["lblOption2"].Visible = false;
                cmbOption2.Visible = false;
                cbOption1.Visible = true;
                cbOption2.Visible = true;
                cbOption3.Visible = false;
                panel.Controls["lblFactionUpgrade"].Visible = false;
                cmbFactionUpgrade.Visible = false;

                if (Weapons[currentIndex + 2] == "Icon of Despair")
                {
                    cmbOption1.Enabled = false;
                    cbOption2.Enabled = false;
                    cbOption2.Checked = false;

                    cbOption1.Enabled = true;
                    cbOption1.Checked = true;
                } 
                else if (Weapons[currentIndex + 2] == "Sigil of Decay")
                {
                    cmbOption1.Enabled = false;
                    cbOption1.Enabled = false;
                    cbOption1.Checked = false;

                    cbOption2.Enabled = true;
                    cbOption2.Checked = true;
                }
                else
                {
                    cmbOption1.Enabled = true;
                    cbOption1.Enabled = true;
                    cbOption2.Enabled = true;

                    cbOption1.Checked = false;
                    cbOption2.Checked = false;
                }

                if (Weapons.Contains("Icon of Despair"))
                {
                    if (Weapons[currentIndex + 2] != "Icon of Despair")
                    {
                        cbOption1.Enabled = false;
                    } else
                    {
                        cbOption1.Enabled = true;
                    }
                }

                if (Weapons.Contains("Sigil of Decay"))
                {
                    if (Weapons[currentIndex + 2] != "Sigil of Decay")
                    {
                        cbOption2.Enabled = false;
                    } else
                    {
                        cbOption2.Enabled = true;
                    }
                }

                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Blight Launcher",
                    "Boltgun",
                    "Bubotic Axe",
                    "Flail of Corruption",
                    "Great Plague Cleaver",
                    "Mace of Contagion and Bubotic Axe",
                    "Meltagun",
                    "Plague Belcher",
                    "Plague Knife",
                    "Plague Spewer",
                    "Plasma Gun"
                });
                antiLoop = true;
                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);
                antiLoop = false;

                int[] constraintArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                //Blight Launcher, Plague Spewer, Meltagun/Plague Belcher/Plasma Gun, Plague Knife, Bubotic Axe, Mace of Contagion,
                //      Flail of Corruption, Great Plague Cleaver
                
                foreach(string Weapon in Weapons)
                {
                    if(Weapon == "Blight Launcher")
                    {
                        constraintArray[0] += 1;
                    }

                    if (Weapon == "Plague Spewer")
                    {
                        constraintArray[1] += 1;
                    }

                    if (Weapon == "Meltagun" || Weapon == "Plague Belcher" || Weapon == "Plasma Gun")
                    {
                        constraintArray[2] += 1;
                    }

                    if (Weapon == "Plague Knife")
                    {
                        constraintArray[3] += 1;
                    }

                    if (Weapon == "Bubotic Axe")
                    {
                        constraintArray[4] += 1;
                    }

                    if (Weapon == "Mace of Contagion and Bubotic Axe")
                    {
                        constraintArray[5] += 1;
                    }

                    if (Weapon == "Flail of Corruption")
                    {
                        constraintArray[6] += 1;
                    }

                    if (Weapon == "Great Plague Cleaver")
                    {
                        constraintArray[7] += 1;
                    }
                }

                if ((UnitSize == 10 && constraintArray[0] + constraintArray[1] == 3) || (UnitSize < 10 && constraintArray[0] == 1) ||
                    (UnitSize == 10 && constraintArray[0] == 2))
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Blight Launcher"));
                }

                if ((UnitSize == 10 && constraintArray[0] + constraintArray[1] == 3) || (UnitSize < 10 && constraintArray[1] == 1) ||
                    (UnitSize == 10 && constraintArray[1] == 2))
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Plague Spewer"));
                }

                if (constraintArray[2] == UnitSize / 5)
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Meltagun"));
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Plague Belcher"));
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Plasma Gun"));
                }

                if (constraintArray[3] == UnitSize / 5)
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Plague Knife"));
                }

                if (constraintArray[4] == UnitSize / 5)
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Bubotic Axe"));
                }

                if (constraintArray[5] == UnitSize / 5)
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Mace of Contagion and Bubotic Axe"));
                }

                if (constraintArray[6] == UnitSize / 5)
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Flail of Corruption"));
                }

                if (constraintArray[7] == UnitSize / 5)
                {
                    cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Great Plague Cleaver"));
                }
            }
        }
    }
}
