using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Suppressors : Datasheets
    {
        public Suppressors()
        {
            DEFAULT_POINTS = 33;
            UnitSize = 3;
            Points = 100;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "FLY", "JUMP PACK", "SMOKESCREEN", "SUPPRESSOR SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new Suppressors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Suppressor Squad - " + Points + "pts";
        }
    }
}
