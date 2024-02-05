using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class ConvergenceOfDominion : Datasheets
    {
        public ConvergenceOfDominion()
        {
            DEFAULT_POINTS = 80;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "BUILDING", "VEHICLE", "STARSTELE", "CONVERGENCE OF DOMINION"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new ConvergenceOfDominion();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Convergence of Dominion - " + Points + "pts";
        }
    }
}
