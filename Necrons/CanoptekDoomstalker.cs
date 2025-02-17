using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class CanoptekDoomstalker : Datasheets
    {
        public CanoptekDoomstalker()
        {
            DEFAULT_POINTS = 130;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "CANOPTEK", "<DYNASTY>",
                "MONSTER", "CANOPTEK DOOMSTALKER"
            });
            Role = "Heavy Support";
        }
        public override Datasheets CreateUnit()
        {
            return new CanoptekDoomstalker();
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
            return "Canoptek Doomstalker - " + Points + "pts";
        }
    }
}
