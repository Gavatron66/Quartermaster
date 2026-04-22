using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class BreacherTeam : Datasheets
    {
        bool supportTurret;

        public BreacherTeam()
        {
            DEFAULT_POINTS = 85;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "firewarriors";
            Weapons.Add(""); //Markerlight
            Weapons.Add("(None)"); //Drones
            Weapons.Add("(None)");
            Weapons.Add("Missile Pod (+10 pts)"); //Support Turret
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "<SEPT>",
                "INFANTRY", "CORE", "FIRE WARRIOR TEAM", "PHOTON GRENADES", "BREACHER TEAM"
            });
            Role = "Troops";
            supportTurret = false;
        }

        public override Datasheets CreateUnit()
        {
            return new BreacherTeam();
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

            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblnud1"].Visible = false;
            nudOption1.Visible = false;
            nudOption2.Visible = false;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Guardian Drone (+10 pts)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Guardian Drone (+10 pts)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[2]);

            cbOption1.Text = "Shas'ui Markerlight (+5 pts)";
            if (Weapons[0] == cbOption1.Text)
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
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[3]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[1] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[2] = cmbOption2.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[0] = "";
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
                case 411:
                    Weapons[3] = gb_cmbOption1.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            if (cbOption1.Checked)
            {
                Points += 5;
            }

            if (Weapons[1] == "Shield Drone (+15 pts)")
            {
                Points += 15;
            }
            else if (Weapons[1] == "(None)") { }
            else
            {
                Points += 10;
            }

            if (Weapons[2] == "Shield Drone (+15 pts)")
            {
                Points += 15;
            }
            else if (Weapons[2] == "(None)") { }
            else
            {
                Points += 10;
            }

            if (supportTurret)
            {
                Points += 10;
            }

            if (Weapons[3] == "Missile Pod (+10 pts)" && supportTurret)
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Breacher Team - " + Points + "pts";
        }
    }
}