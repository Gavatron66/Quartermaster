using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Ultramarines
{
    public class HonourGuard : Datasheets
    {
        public HonourGuard()
        {
            DEFAULT_POINTS = 45;
            UnitSize = 2;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "ULTRAMARINES",
                "INFANTRY", "CORE", "HONOUR GUARD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new HonourGuard();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Honour Guard - " + Points + "pts";
        }
    }
}
