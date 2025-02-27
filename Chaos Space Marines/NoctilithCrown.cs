using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class NoctilithCrown : Datasheets
    {
        public NoctilithCrown()
        {
            DEFAULT_POINTS = 100;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TRAITORIS ASTARTES",
                "BUILDING", "VEHICLE", "WARP LOCUS", "NOCTILITH CROWN"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new NoctilithCrown();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Noctilith Crown - " + Points + "pts";
        }
    }
}
