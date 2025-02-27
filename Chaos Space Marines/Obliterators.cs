using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Obliterators : Datasheets
    {
        public Obliterators()
        {
            UnitSize = 1;
            DEFAULT_POINTS = 90;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "INFANTRY", "DAEMON", "DAEMONKIN", "CULT OF DESTRUCTION", "OBLITERATORS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Obliterators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nud.Value.ToString());
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

        }

        public override string ToString()
        {
            return "Obliterators - " + Points + "pts";
        }
    }
}
