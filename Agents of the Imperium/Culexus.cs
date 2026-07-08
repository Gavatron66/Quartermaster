using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class Culexus : Datasheets
    {
        public Culexus()
        {
            DEFAULT_POINTS = 90;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "OFFICIO ASSASSINORUM", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "CHARACTER", "CULEXUS ASSASSIN"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Culexus();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Culexus Assassin - " + Points + "pts";
        }
    }
}
