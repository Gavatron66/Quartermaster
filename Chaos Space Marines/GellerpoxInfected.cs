using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class GellerpoxInfected : Datasheets
    {
        public GellerpoxInfected()
        {
            DEFAULT_POINTS = 150;
            UnitSize = 7;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE",
                "INFANTRY", "GELLERPOX INFECTED"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new GellerpoxInfected();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Gellerpox Infected - " + Points + "pts";
        }
    }
}
