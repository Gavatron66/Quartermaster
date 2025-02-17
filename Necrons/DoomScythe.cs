using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class DoomScythe : Datasheets
    {
        public DoomScythe()
        {
            DEFAULT_POINTS = 165;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "VEHICLE", "AIRCRAFT", "FLY", "DOOM SCYTHE"
            });
            Role = "Flyer";
        }
        public override Datasheets CreateUnit()
        {
            return new DoomScythe();
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
            return "Doom Scythe - " + Points + "pts";
        }
    }
}
