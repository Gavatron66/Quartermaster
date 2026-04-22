using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class StealthBattlesuits : Datasheets
    {
        int currentIndex;
        bool antiloop = false;

        public StealthBattlesuits()
        {
            DEFAULT_POINTS = 25;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m4k";
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "<SEPT>",
                "INFANTRY", "CORE", "BATTLESUIT", "FLY", "JET PACK", "STEALTH BATTLESUITS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new StealthBattlesuits();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as T_au;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["lblOption1"].Visible = true;
            panel.Controls["lblOption2"].Visible = true;
            cmbOption1.Visible = true;
            cmbOption2.Visible = true;
            cbOption4.Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();

            if (Weapons[3] == "")
            {
                lbModelSelect.Items.Add("Stealth Shas'vre");
            }
            else if (Weapons[4] == "")
            {
                lbModelSelect.Items.Add("Stealth Shas'vre w/ " + Weapons[3]);
            }
            else
            {
                lbModelSelect.Items.Add("Stealth Shas'vre w/ " + Weapons[3] + " and " + Weapons[4]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[i + 3] == "")
                {
                    lbModelSelect.Items.Add("Stealth Shas'ui");
                }
                else if (Weapons[i + 4] == "")
                {
                    lbModelSelect.Items.Add("Stealth Shas'ui w/ " + Weapons[i + 3]);
                }
                else
                {
                    lbModelSelect.Items.Add("Stealth Shas'ui w/ " + Weapons[i + 3] + " and " + Weapons[i + 4]);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Fusion Blaster (+5 pts)";

            cbOption2.Text = "Drone Controller (+5 pts)";

            cbOption3.Text = "Target Lock (+5 pts)";

            cbOption4.Text = "Homing Beacon (+5 pts)";
            if(Weapons.Contains(cbOption4.Text))
            {
                cbOption4.Checked = true;
            }
            else
            {
                cbOption4.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiloop)
            {
                return;
            }

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 2) + 3] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 3] = "";
                    }
                    UpdateList(lbModelSelect);
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 2) + 4] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 4] = "";
                    }
                    UpdateList(lbModelSelect);
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[(currentIndex * 2) + 4] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 4] = "";
                    }
                    UpdateList(lbModelSelect);
                    break;
                case 24:
                    if (cbOption4.Checked)
                    {
                        Weapons[2] = cbOption4.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("");
                            Weapons.Add("");
                            lbModelSelect.Items.Add("Stealth Shas'ui");
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2) + 3, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }

                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;
                    cbOption1.Enabled = true;
                    cbOption2.Enabled = true;
                    cbOption3.Enabled = true;

                    int[] weaponsCheck = new int[2];
                    foreach (var weapon in Weapons)
                    {
                        if(weapon == cbOption1.Text)
                        {
                            weaponsCheck[0]++;
                        }

                        if (weapon == cbOption2.Text)
                        {
                            weaponsCheck[1]++;
                        }

                        if (weapon == cbOption3.Text)
                        {
                            weaponsCheck[1]++;
                        }
                    }

                    if (Weapons[(currentIndex * 2) + 3] != "")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        if (weaponsCheck[0] == UnitSize / 3)
                        {
                            cbOption1.Enabled = false;
                        }
                        cbOption1.Checked = false;
                    }

                    if (Weapons[(currentIndex * 2) + 4] == cbOption2.Text)
                    {
                        cbOption2.Checked = true;
                    }
                    else
                    {
                        if (weaponsCheck[1] == UnitSize / 3)
                        {
                            cbOption2.Enabled = false;
                        }
                        cbOption2.Checked = false;
                    }

                    if (Weapons[(currentIndex * 2) + 4] == cbOption3.Text)
                    {
                        cbOption3.Checked = true;
                    }
                    else
                    {
                        if (weaponsCheck[1] == UnitSize / 3)
                        {
                            cbOption3.Enabled = false;
                        }
                        cbOption3.Checked = false;
                    }

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Drone Controller (+5 pts)" || weapon == "Fusion Blaster (+5 pts)" || weapon == "Homing Beacon (+5 pts)"
                    || weapon == "Target Lock (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Gun Drone (+10 pts)" || weapon == "Marker Drone (+10 pts)")
                {
                    Points += 10;
                }

                if(weapon == "Shield Drone (+15 pts)")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Stealth Battlesuits - " + Points + "pts";
        }

        private void UpdateList(ListBox lb)
        {
            antiLoop = true;

            if(currentIndex == 0)
            {
                if (Weapons[3] == "" && Weapons[4] == "")
                {
                    lb.Items[0] = ("Stealth Shas'vre");
                }
                else if (Weapons[4] == "" && Weapons[3] != "")
                {
                    lb.Items[0] = ("Stealth Shas'vre w/ " + Weapons[3]);
                }
                else if (Weapons[3] == "" && Weapons[4] != "")
                {
                    lb.Items[0] = ("Stealth Shas'vre w/ " + Weapons[4]);
                }
                else
                {
                    lb.Items[0] = ("Stealth Shas'vre w/ " + Weapons[3] + " and " + Weapons[4]);
                }
                return;
            }

            if (Weapons[(currentIndex * 2) + 3] == "" && Weapons[(currentIndex * 2) + 4] == "")
            {
                lb.Items[currentIndex] = ("Stealth Shas'ui");
            }
            else if (Weapons[(currentIndex * 2) + 3] != "" && Weapons[(currentIndex * 2) + 4] == "")
            {
                lb.Items[currentIndex] = ("Stealth Shas'ui w/ " + Weapons[(currentIndex * 2) + 3]);
            }
            else if (Weapons[(currentIndex * 2) + 4] != "" && Weapons[(currentIndex * 2) + 3] == "")
            {
                lb.Items[currentIndex] = ("Stealth Shas'ui w/ " + Weapons[(currentIndex * 2) + 4]);
            }
            else
            {
                lb.Items[currentIndex] = ("Stealth Shas'ui w/ " + Weapons[(currentIndex * 2) + 3] + " and " + Weapons[(currentIndex * 2) + 4]);
            }

            antiLoop = false;
        }
    }
}
