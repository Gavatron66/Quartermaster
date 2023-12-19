using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class NightScythe : Datasheets
    {
        public NightScythe()
        {
            DEFAULT_POINTS = 120;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "VEHICLE", "AIRCRAFT", "FLY", "TRANSPORT", "NIGHT SCYTHE"
            });
            role = "Flyer";
        }
        public override Datasheets CreateUnit()
        {
            return new NightScythe();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Night Scythe - " + Points + "pts";
        }
    }
}
