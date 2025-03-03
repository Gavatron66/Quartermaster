using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Venomcrawler : Datasheets
    {
        public Venomcrawler()
        {
            DEFAULT_POINTS = 105;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "VEHICLE", "DAEMON", "DAEMON ENGINE", "VENOMCRAWLER"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Venomcrawler();
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
            return "Venomcrawler - " + Points + "pts";
        }
    }
}
