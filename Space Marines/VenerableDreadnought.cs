using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class VenerableDreadnought : Datasheets
    {
        public VenerableDreadnought()
        {
            DEFAULT_POINTS = 135;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m1k";
            Weapons.Add("Assault Cannon");
            Weapons.Add("DCW and Storm Bolter");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "CORE", "DREADNOUGHT", "SMOKESCREEN", "VENERABLE DREADNOUGHT"
            });
            role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new VenerableDreadnought();
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
                "Assault Cannon",
                "Heavy Plasma Cannon",
                //Helfrost Cannon
                "Multi-melta",
                "Twin Lascannon"
            });
            if (repo.currentSubFaction == "Space Wolves")
            {
                cmbOption1.Items.Insert(2, "Helfrost Cannon");
            }
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "DCW and Heavy Flamer",
                "DCW and Storm Bolter",
                "Missile Launcher"
            });
            if (repo.currentSubFaction == "Space Wolves")
            {
                cmbOption2.Items.Insert(2, "Great Wolf Claw and Heavy Flamer");
                cmbOption2.Items.Insert(3, "Great Wolf Claw and Storm Bolter");
            }
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Fenrisian Axe and Blizzard Shield";
            if (repo.currentSubFaction != "Space Wolves")
            {
                cbOption1.Visible = false;
            }
            else
            {
                cbOption1.Visible = true;
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
                    if(cbOption1.Checked)
                    {
                        Weapons[0] = "Fenrisian Great Axe";
                        Weapons[1] = "Blizzard Shield";

                        cmbOption1.Enabled = false;
                        cmbOption2.Enabled = false;
                    }
                    else
                    {
                        Weapons[0] = "DCW and Storm Bolter";
                        Weapons[1] = "Assault Cannon";

                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                    }
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Venerable Dreadnought - " + Points + "pts";
        }
    }
}
