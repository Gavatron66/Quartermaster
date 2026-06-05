using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class ExaltedFlamer : Datasheets
    {
        public ExaltedFlamer()
        {
            DEFAULT_POINTS = 75;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "TZEENTCH",
                "INFANTRY", "CHARACTER", "DAEMON", "FLAMERS", "FLY", "EXALTED FLAMER"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new ExaltedFlamer();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Exalted Flamer - " + Points + "pts";
        }
    }
}
