using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class BlueHorrors : Datasheets
    {
        public BlueHorrors()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "TZEENTCH",
                "INFANTRY", "DAEMON", "CORE", "HORRORS", "BLUE HORRORS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new BlueHorrors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Blue Horrors - " + Points + "pts";
        }
    }
}
