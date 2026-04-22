using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Karandras : Datasheets
    {
        public Karandras()
        {
            DEFAULT_POINTS = 150;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI",
                "CHARACTER", "INFANTRY", "ASPECT WARRIOR", "PHOENIX LORD", "STRIKING SCORPIONS", "KARANDRAS"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Karandras();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Karandras - " + Points + "pts";
        }
    }
}
