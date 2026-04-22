using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Grey_Knights
{
    public class GKVenerableDreadnought : Datasheets
    {
        public GKVenerableDreadnought()
        {
            DEFAULT_POINTS = 145;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Assault Cannon");
            Weapons.Add("DCW and Storm Bolter");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "SANCTIC ASTARTES", "GREY KNIGHTS",
                "VEHICLE", "CORE", "PSYKER", "DREADNOUGHT", "SMOKESCREEN", "HONOURED KNIGHT", "VENERABLE DREADNOUGHT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new GKVenerableDreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as GreyKnights;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Assault Cannon",
                "Heavy Plasma Cannon",
                "Multi-melta",
                "Twin Lascannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "DCW and Heavy Flamer",
                "DCW and Storm Bolter",
                "Missile Launcher"
            });
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
            return "Venerable Dreadnought - " + Points + "pts";
        }
    }
}
