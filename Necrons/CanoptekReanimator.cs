using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class CanoptekReanimator : Datasheets
    {
        public CanoptekReanimator()
        {
            DEFAULT_POINTS = 80;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "CANOPTEK", "<DYNASTY>",
                "MONSTER", "CANOPTEK REANIMATOR"
            });
            Role = "Elites";
        }
        public override Datasheets CreateUnit()
        {
            return new CanoptekReanimator();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Canoptek Reanimator - " + Points + "pts";
        }
    }
}
