using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class SkullCannon : Datasheets
    {
        public SkullCannon()
        {
            DEFAULT_POINTS = 90;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "KHORNE",
                "VEHICLE", "DAEMON", "BLOODLETTERS", "SKULL CANNON"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new SkullCannon();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Skull Cannon - " + Points + "pts";
        }
    }
}
