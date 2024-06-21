using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class SerberysSulphurhounds : Datasheets
    {
        public SerberysSulphurhounds()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize + 10; //Alpha is extra points for some reason
            TemplateCode = "2N";
            Weapons.Add("0");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "CAVALRY", "CORE", "SERBERYS", "SERBERYS SULPHURHOUNDS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new SerberysSulphurhounds();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AdMech;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Maximum = UnitSize / 3;
            nudOption1.Value = Convert.ToInt32(Weapons[0]);
            panel.Controls["lblnud1"].Text = "Phosphor Blast Carbines (+10 pts, 1/3x models):";
            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X + 90, nudOption1.Location.Y);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    nudOption1.Maximum = UnitSize / 3;
                    break;
                case 31:
                    Weapons[0] = nudOption1.Value.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += int.Parse(Weapons[0]) * 10;
        }

        public override string ToString()
        {
            return "Serberys Sulphurhounds - " + Points + "pts";
        }
    }
}
