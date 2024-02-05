using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Outriders : Datasheets
    {
        public Outriders()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "BIKER", "CORE", "PRIMARIS", "OUTRIDER SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Outriders();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Outrider Squad - " + Points + "pts";
        }
    }
}
