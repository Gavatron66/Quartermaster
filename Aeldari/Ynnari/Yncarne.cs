using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari.Ynnari
{
    public class Yncarne : Datasheets
    {
        public Yncarne()
        {
            DEFAULT_POINTS = 260;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "YNARRI",
                "CHARACTER", "MONSTER", "FLY", "PSYKER", "DAEMON", "THE YNCARNE"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Yncarne();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "The Yncarne - " + Points + "pts";
        }
    }
}
