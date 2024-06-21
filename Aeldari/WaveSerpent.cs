using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class WaveSerpent : Datasheets
    {
        public WaveSerpent()
        {
            DEFAULT_POINTS = 145;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m4k";
            Weapons.Add("Twin Scatter Laser");
            Weapons.Add("Twin Shuriken Catapult");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "VEHICLE", "TRANSPORT", "FLY", "WAVE SERPENT"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new WaveSerpent();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Aeldari;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Twin Aeldari Missile Launcher (+10 pts)",
                "Twin Bright Lance (+20 pts)",
                "Twin Scatter Laser",
                "Twin Shuriken Cannon",
                "Twin Starcannon (+10 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Shuriken Cannon (+10 pts)",
                "Twin Shuriken Catapult"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Crystal Targeting Matrix (+10 pts)";
            if (Weapons[2] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Spirit Stones (+10 pts)";
            if (Weapons[3] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Star Engines (+10 pts)";
            if (Weapons[4] != string.Empty)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }

            cbOption4.Text = "Vectored Engines (+20 pts)";
            if (Weapons[5] != string.Empty)
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
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[3] = cbOption2.Text;
                    }
                    else { Weapons[3] = string.Empty; }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[4] = cbOption3.Text;
                    }
                    else { Weapons[4] = string.Empty; }
                    break;
                case 24:
                    if (cbOption4.Checked)
                    {
                        Weapons[5] = cbOption4.Text;
                    }
                    else { Weapons[5] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Crystal Targeting Matrix (+10 pts)" || weapon == "Spirit Stones (+10 pts)" ||
                    weapon == "Star Engines (+10 pts)" || weapon == "Shuriken Cannon (+10 pts)" ||
                    weapon == "Twin Aeldari Missile Launcher (+10 pts)" || weapon == "Twin Starcannon (+15 pts)")
                {
                    Points += 10;
                }
                if (weapon == "Twin Bright Lance (+20 pts)" || weapon == "Vectored Engines (+20 pts)")
                {
                    Points += 20;
                }
            }
        }

        public override string ToString()
        {
            return "Wave Serpent - " + Points + "pts";
        }
    }
}
