using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class ExaltedSeekerChariot : Datasheets
    {
        public ExaltedSeekerChariot()
        {
            DEFAULT_POINTS = 85;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "SLAANESH",
                "VEHICLE", "DAEMON", "DAEMONETTES", "EXALTED SEEKER CHARIOT"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new ExaltedSeekerChariot();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Exalted Seeker Chariot - " + Points + "pts";
        }
    }
}
