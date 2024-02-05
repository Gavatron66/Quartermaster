using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class Cryptothralls : Datasheets
    {
        public Cryptothralls()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 2;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "CANOPTEK", "<DYNASTY>",
                "INFANTRY", "CRYPTOTHRALLS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Cryptothralls();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Cryptothralls - " + Points + "pts";
        }
    }
}
