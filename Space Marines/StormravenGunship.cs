using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class StormravenGunship : Datasheets
    {
        public StormravenGunship()
        {
            DEFAULT_POINTS = 310;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m1k";
            Weapons.Add("Twin Assault Cannon");
            Weapons.Add("Typhoon Missile Launcher");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "AIRCRAFT", "TRANSPORT", "FLY", "MACHINE SPIRIT", "STORMRAVEN GUNSHIP"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new StormravenGunship();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Twin Assault Cannon",
                "Twin Heavy Plasma Cannon",
                "Twin Lascannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Twin Heavy Bolter",
                "Twin Multi-melta",
                "Typhoon Missile Launcher"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Two Hurricane Bolters";
            if (Weapons[2] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

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
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Twin Lascannon"))
            {
                Points += 10;
            }

            if (Weapons.Contains("Typhoon Missile Launcher"))
            {
                Points += 10;
            }

            if (Weapons.Contains("Twin Multi-melta"))
            {
                Points += 20;
            }

            if (Weapons.Contains("Two Hurricane Bolters"))
            {
                Points += 30;
            }
        }

        public override string ToString()
        {
            return "Stormraven Gunship - " + Points + "pts";
        }
    }
}
