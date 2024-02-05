using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class FirePrism : Datasheets
    {
        public FirePrism()
        {
            DEFAULT_POINTS = 165;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m4k";
            Weapons.Add("Twin Shuriken Catapult");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "VEHICLE", "FLY", "FIRE PRISM"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new FirePrism();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Aeldari;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Shuriken Cannon (+5 pts)",
                "Twin Shuriken Catapult"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Crystal Targeting Matrix (+10 pts)";
            if (Weapons[1] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Spirit Stones (+10 pts)";
            if (Weapons[2] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Star Engines (+10 pts)";
            if (Weapons[3] != string.Empty)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }

            cbOption4.Text = "Vectored Engines (+20 pts)";
            if (Weapons[4] != string.Empty)
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[3] = cbOption3.Text;
                    }
                    else { Weapons[3] = string.Empty; }
                    break;
                case 24:
                    if (cbOption4.Checked)
                    {
                        Weapons[4] = cbOption4.Text;
                    }
                    else { Weapons[4] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Shuriken Cannon (+5 pts)")
                {
                    Points += 5;
                }
                if (weapon == "Crystal Targeting Matrix (+10 pts)" || weapon == "Spirit Stones (+10 pts)" ||
                    weapon == "Star Engines (+10 pts)")
                {
                    Points += 10;
                }
                if (weapon == "Aeldari Missile Launcher (+15 pts)" || weapon == "Starcannon (+15 pts)")
                {
                    Points += 15;
                }
                if (weapon == "Bright Lance (+20 pts)" || weapon == "Vectored Engines (+20 pts)")
                {
                    Points += 20;
                }
            }
        }

        public override string ToString()
        {
            return "Fire Prism - " + Points + "pts";
        }
    }
}
