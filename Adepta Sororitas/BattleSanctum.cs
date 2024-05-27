using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class BattleSanctum : Datasheets
    {
        public BattleSanctum()
        {
            DEFAULT_POINTS = 80;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS",
                "TERRAIN", "AREA TERRAIN", "RUINS", "BATTLE SANCTUM"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new BattleSanctum();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Battle Sanctum - " + Points + "pts";
        }
    }
}
