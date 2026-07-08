using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class Jokaero : Datasheets
    {
        public Jokaero()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "JOKAERO", "INQUISITION", "<ORDO>", "AGENTS OF THE IMPERIUM",
                "CHARACTER", "INFANTRY", "JOKAERO WEAPONSMITH"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Jokaero();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Jokaero Weaponsmith - " + Points + "pts";
        }
    }
}
