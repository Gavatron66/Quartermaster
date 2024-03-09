using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class KillTeamCassius : Datasheets
    {
        public KillTeamCassius()
        {
            DEFAULT_POINTS = 220;
            UnitSize = 9;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DEATHWATCH",
                "INFANTRY", "CORE", "SMOKESCREEN", "KILL TEAM", "KILL TEAM CASSIUS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new KillTeamCassius();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Kill Team Cassius - " + Points + "pts";
        }
    }
}
