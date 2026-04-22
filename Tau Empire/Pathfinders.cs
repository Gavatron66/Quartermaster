using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class Pathfinders : Datasheets
    {
        int currentIndex;
        int[] weaponsCheck;

        public Pathfinders()
        {
            DEFAULT_POINTS = 9;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL3m3k";
            Weapons.Add("(None)"); //Two Drones
            Weapons.Add("(None)");
            Weapons.Add(""); //Grav-inhibitor
            Weapons.Add(""); //Pulse Accelerator
            Weapons.Add(""); //Recon
            for(int i = 0; i < 10; i++)
            {
                Weapons.Add("Pulse Carbine");
            }
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "<SEPT>",
                "INFANTRY", "CORE", "FIRE WARRIOR TEAM", "PHOTON GRENADES", "PATHFINDER TEAM"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Pathfinders();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as T_au;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            nudUnitSize.Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            panel.Controls["lblOption2"].Visible = true;
            cmbOption2.Visible = true;
            panel.Controls["lblOption3"].Visible = true;
            cmbOption3.Visible = true;
            cbOption1.Visible = true;
            cbOption2.Visible = true;
            cbOption3.Visible = true;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Pathfinder Shas'ui w/ " + Weapons[5]);
            for(int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Pathfinder w/ " + Weapons[i + 5]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Drone Controller (+5 pts)",
                "Ion Rifle (+5 pts)",
                "Neuroweb System Jammer (+5 pts)",
                "Pulse Carbine",
                "Rail Rifle (+5 pts)",
                "Semi-automatic Grenade Launcher (+5 pts)"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[0]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Grav-inhibitor Drone (+10 pts)";
            if (Weapons.Contains(cbOption1.Text))
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Pulse Accelerator Drone (+10 pts)";
            if (Weapons.Contains(cbOption2.Text))
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Recon Drone (+15 pts)";
            if (Weapons.Contains(cbOption3.Text))
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop)
            {
                return;
            }

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch (code)
            {
                case 11:
                    antiLoop = true;
                    Weapons[currentIndex + 5] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Pathfinder Shas'ui w/ " + Weapons[5];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Pathfinder w/ " + Weapons[currentIndex + 5];
                    }
                    antiLoop = false;
                    break;
                case 12:
                    Weapons[0] = cmbOption2.SelectedItem.ToString();
                    break;
                case 13:
                    Weapons[1] = cmbOption3.SelectedItem.ToString();
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[3] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[4] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[4] = "";
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if(currentIndex < 0)
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        break;
                    }

                    panel.Controls["lblOption1"].Visible = true;
                    cmbOption1.Visible = true;

                    cmbOption1.Items.Clear();
                    cmbOption1.Items.AddRange(new string[]
                    {
                        "Drone Controller (+5 pts)",
                        "Ion Rifle (+5 pts)",
                        "Neuroweb System Jammer (+5 pts)",
                        "Pulse Carbine",
                        "Rail Rifle (+5 pts)",
                        "Semi-automatic Grenade Launcher (+5 pts)"
                    });
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 5]);

                    if (weaponsCheck[0] == 3 && (Weapons[currentIndex + 5] != "Ion Rifle (+5 pts)" || Weapons[currentIndex + 5] != "Rail Rifle (+5 pts)"))
                    {
                        cmbOption1.Items.Remove("Ion Rifle (+5 pts)");
                        cmbOption1.Items.Remove("Rail Rifle (+5 pts)");
                    }

                    if (weaponsCheck[1] == 1 && Weapons[currentIndex + 5] != "Semi-automatic Grenade Launcher (+5 pts)")
                    {
                        cmbOption1.Items.Remove("Semi-automatic Grenade Launcher (+5 pts)");
                    }

                    if (weaponsCheck[2] == 1 && Weapons[currentIndex + 5] != "Neuroweb System Jammer (+5 pts)")
                    {
                        cmbOption1.Items.Remove("Neuroweb System Jammer (+5 pts)");
                    }

                    if (weaponsCheck[3] == 1 && Weapons[currentIndex + 5] != "Drone Controller (+5 pts)")
                    {
                        cmbOption1.Items.Remove("Drone Controller (+5 pts)");
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            weaponsCheck = new int[4] { 0, 0, 0, 0 };
            foreach (var weapon in Weapons)
            {
                if(weapon == "Ion Rifle (+5 pts)" || weapon == "Rail Rifle (+5 pts)")
                {
                    weaponsCheck[0]++;
                    Points += 5;
                }

                if(weapon == "Semi-automatic Grenade Launcher (+5 pts)")
                {
                    weaponsCheck[1]++;
                    Points += 5;
                }

                if(weapon == "Neuroweb System Jammer (+5 pts)")
                {
                    weaponsCheck[2]++;
                    Points += 5;
                }

                if(weapon == "Drone Controller (+5 pts)")
                {
                    weaponsCheck[3]++;
                    Points += 5;
                }

                if(weapon == "Grav-inhibitor Drone (+10 pts)" || weapon == "Gun Drone (+10 pts)" || weapon == "Marker Drone (+10 pts)" 
                    || weapon == "Pulse Accelerator Drone (+10 pts)")
                {
                    Points += 10;
                }

                if(weapon == "Recon Drone (+15 pts)" || weapon == "Shield Drone (+15 pts)")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Pathfinder Team - " + Points + "pts";
        }
    }
}
