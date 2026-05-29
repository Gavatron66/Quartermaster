using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class MutalithVortexBeast : Datasheets
    {
        public MutalithVortexBeast()
        {
            DEFAULT_POINTS = 130;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TZEENTCH", "HERETIC ASTARTES", "THOUSAND SONS", "<GREAT CULT>",
                "MONSTER", "MUTALITH VORTEX BEAST"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new MutalithVortexBeast();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ThousandSons;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Mutalith Vortex Beast - " + Points + "pts";
        }
    }
}
