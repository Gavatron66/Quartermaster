using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari.Harlequins
{
    public class Starweaver : Datasheets
    {
        public Starweaver()
        {
            DEFAULT_POINTS = 95;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "HARLEQUINS", "<SAEDATH>",
                "VEHICLE", "TRANSPORT", "FLY", "STARWEAVERS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Starweaver();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Starweaver - " + Points + "pts";
        }
    }
}
