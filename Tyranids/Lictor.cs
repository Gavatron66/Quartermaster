using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Lictor : Datasheets
    {
        public Lictor()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "FLESH HOOKS", "FEEDER TENDRILS", "LICTOR"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Lictor();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }
        public override string ToString()
        {
            return "Lictor - " + Points + "pts";
        }
    }
}
