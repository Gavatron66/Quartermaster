using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class WebwayGate : Datasheets
    {
        public WebwayGate()
        {
            DEFAULT_POINTS = 100;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "HARLEQUINS", "DRUKHARI",
                "TERRAIN FEATURE", "OBSTACLE", "WEBWAY GATE"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new WebwayGate();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Webway Gate - " + Points + "pts";
        }
    }
}