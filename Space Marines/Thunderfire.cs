using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Thunderfire : Datasheets
    {
        public Thunderfire()
        {
            DEFAULT_POINTS = 120;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "ARTILLERY", "THUNDERFIRE CANNON",
                "INFANTRY", "CHARACTER", "GUNNER"
            });
            role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Thunderfire();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Thunderfire Cannon - " + Points + "pts";
        }
    }
}
