using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class StrikeTeam : Datasheets
    {
        bool supportTurret;

        public StrikeTeam()
        {
            DEFAULT_POINTS = 80;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "firewarriors";
            Weapons.Add("10");
            Weapons.Add("0");
            Weapons.Add(""); //Markerlight
            Weapons.Add("(None)"); //Drones
            Weapons.Add("(None)");
            Weapons.Add("Missile Pod (+10 pts)"); //Support Turret
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "<SEPT>",
                "INFANTRY", "CORE", "FIRE WARRIOR TEAM", "PHOTON GRENADES", "STRIKE TEAM"
            });
            Role = "Troops";
            supportTurret = false;
        }

        public override Datasheets CreateUnit()
        {
            return new StrikeTeam();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as T_au;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudOption1 = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption1"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;

            panel.Controls["lblNumModels"].Text = "Number of Models w/ Pulse Rifles: ";
            panel.Controls["lblnud1"].Text = "Number of Models w/ Pulse Carbines: ";

            nudOption1.Minimum = 0;
            nudOption1.Maximum = 10;
            nudOption1.Value = Convert.ToDecimal(Weapons[0]);

            nudOption2.Minimum = 0;
            nudOption2.Maximum = 10;
            nudOption2.Value = Convert.ToDecimal(Weapons[1]);

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Guardian Drone (+10 pts)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[3]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Guardian Drone (+10 pts)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[4]);

            cbOption1.Text = "Shas'ui Markerlight (+5 pts)";
            if (Weapons[2] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Include a Support Turret (+10 pts)";
            if (supportTurret)
            {
                cbOption2.Checked = true;
                gb.Enabled = true;
            }
            else
            {
                cbOption2.Checked = false;
                gb.Enabled = false;
            }

            gb.Text = "Support Turret";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "Missile Pod (+10 pts)",
                "Smart Missile System"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[5]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudOption1 = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption1"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[3] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[4] = cmbOption2.SelectedItem.ToString();
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
                        supportTurret = true;
                        gb.Enabled = true;
                    }
                    else
                    {
                        supportTurret = false;
                        gb.Enabled = false;
                    }
                    break;
                case 30:
                    if (nudOption1.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value <= 10)
                    {
                        Weapons[0] = Convert.ToString(nudOption1.Value);
                    }
                    else
                    {
                        nudOption1.Value -= 1;
                    }
                    break;
                case 31:
                    if (nudOption2.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value <= 10)
                    {
                        Weapons[1] = Convert.ToString(nudOption2.Value);
                    }
                    else
                    {
                        nudOption2.Value -= 1;
                    }
                    break;
                case 411:
                    Weapons[5] = gb_cmbOption1.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            if(cbOption1.Checked)
            {
                Points += 5;
            }

            if (Weapons[3] == "Shield Drone (+15 pts)")
            {
                Points += 15;
            }
            else if (Weapons[3] == "(None)") { }
            else
            {
                Points += 10;
            }

            if (Weapons[4] == "Shield Drone (+15 pts)")
            {
                Points += 15;
            }
            else if (Weapons[4] == "(None)") { }
            else
            {
                Points += 10;
            }

            if (supportTurret)
            {
                Points += 10;
            }

            if(Weapons[5] == "Missile Pod (+10 pts)" && supportTurret)
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Strike Team - " + Points + "pts";
        }
    }
}