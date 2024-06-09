using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Ultramarines
{
    public class VictrixGuard : Datasheets
    {
        public VictrixGuard()
        {
            DEFAULT_POINTS = 55;
            UnitSize = 2;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "ULTRAMARINES",
                "INFANTRY", "CORE", "PRIMARIS", "VICTRIX HONOUR GUARD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new VictrixGuard();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Victrix Honour Guard - " + Points + "pts";
        }
    }
}
