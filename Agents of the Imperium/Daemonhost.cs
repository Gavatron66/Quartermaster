using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class Daemonhost : Datasheets
    {
        public Daemonhost()
        {
            DEFAULT_POINTS = 25;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "INQUISITION",
                "CHARACTER", "INFANTRY", "DAEMON", "DAEMONHOST"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Daemonhost();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Daemonhost - " + Points + "pts";
        }
    }
}
