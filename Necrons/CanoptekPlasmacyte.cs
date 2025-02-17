using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class CanoptekPlasmacyte : Datasheets
    {
        public CanoptekPlasmacyte()
        {
            DEFAULT_POINTS = 15;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "CANOPTEK", "<DYNASTY>",
                "BEAST", "FLY", "CANOPTEK PLASMACYTE"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new CanoptekPlasmacyte();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Canoptek Plasmacyte - " + Points + "pts";
        }
    }
}
