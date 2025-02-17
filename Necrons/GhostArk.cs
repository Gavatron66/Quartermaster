using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class GhostArk : Datasheets
    {
        public GhostArk()
        {
            DEFAULT_POINTS = 115;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "VEHICLE", "QUANTUM SHIELDING", "TRANSPORT", "FLY", "GHOST ARK"
            });
            Role = "Transport";
        }
        public override Datasheets CreateUnit()
        {
            return new GhostArk();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Ghost Ark - " + Points + "pts";
        }
    }
}
