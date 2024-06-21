using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class MiasmicMalignifier : Datasheets
    {
        public MiasmicMalignifier()
        {
            UnitSize = 2; //Miasmic Malignifier comes with a Pox Furnace
            DEFAULT_POINTS = 65;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "BUILDING", "VEHICLE", "SMOKESCREEN", "MIASMIC MALIGNIFIER",
                "TERRAIN", "OBSTACLE", "POX FURNACE"
            });
        }

        public override void LoadDatasheets(Panel panel, Faction f) { }

        public override void SaveDatasheets(int code, Panel panel) { }

        public override string ToString()
        {
            return "Miasmic Malignifier - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new MiasmicMalignifier();
        }
    }
}
