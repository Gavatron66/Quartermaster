using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class DoomsdayArk : Datasheets
    {
        public DoomsdayArk()
        {
            DEFAULT_POINTS = 145;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "VEHICLE", "QUANTUM SHIELDING", "FLY", "DOOMSDAY ARK"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new DoomsdayArk();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Doomsday Ark - " + Points + "pts";
        }
    }
}
