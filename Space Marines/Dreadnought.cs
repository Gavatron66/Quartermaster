using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Dreadnought : Datasheets
    {
        public Dreadnought()
        {
            DEFAULT_POINTS = 120;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Assault Cannon");
            Weapons.Add("DCW and Storm Bolter");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "CORE", "SMOKESCREEN", "DREADNOUGHT"
            });
            role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Dreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

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
            if(repo.currentSubFaction == "Space Wolves")
            {
                cmbOption2.Items.Insert(2, "Great Wolf Claw and Heavy Flamer");
                cmbOption2.Items.Insert(3, "Great Wolf Claw and Storm Bolter");
            }
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Dreadnought - " + Points + "pts";
        }
    }
}
