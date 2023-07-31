using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class Abominant : Datasheets
    {
        public Abominant()
        {
            DEFAULT_POINTS = 95;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "TYRANIDS", "GENESTEALER CULTS", "<CULT>",
                "INFANTRY", "CHARACTER", "ABOMINANT"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new Abominant();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Abominant - " + Points + "pts";
        }
    }
}
