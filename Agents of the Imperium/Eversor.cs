using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class Eversor : Datasheets
    {
        public Eversor()
        {
            DEFAULT_POINTS = 90;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "OFFICIO ASSASSINORUM", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "CHARACTER", "EVERSOR ASSASSIN"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Eversor();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Eversor Assassin - " + Points + "pts";
        }
    }
}
