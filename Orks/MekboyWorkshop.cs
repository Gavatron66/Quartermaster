using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class MekboyWorkshop : Datasheets
    {
        public MekboyWorkshop()
        {
            DEFAULT_POINTS = 70;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "MEKBOY WORKSHOP"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new MekboyWorkshop();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Mekboy Workshop - " + Points + "pts";
        }
    }
}
