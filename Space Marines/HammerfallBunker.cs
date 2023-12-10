using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class HammerfallBunker : Datasheets
    {
        public HammerfallBunker()
        {
            DEFAULT_POINTS = 175;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("Heavy Bolter Array");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "BUILDING", "HAMMERFALL BUNKER"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new HammerfallBunker();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Bolter Array",
                "Heavy Flamer Array"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Hammerfall Bunker - " + Points + "pts";
        }
    }
}
