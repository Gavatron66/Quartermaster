using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class SkullAltar : Datasheets
    {
        public SkullAltar()
        {
            DEFAULT_POINTS = 50;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "KHORNE",
                "BUILDING", "VEHICLE", "TRANSPORT", "WARP LOCUS", "SKULL ALTAR"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new SkullAltar();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Skull Altar - " + Points + "pts";
        }
    }
}
