using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Baharroth : Datasheets
    {
        public Baharroth()
        {
            DEFAULT_POINTS = 160;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI",
                "CHARACTER", "INFANTRY", "JUMP PACK", "FLY", "ASPECT WARRIOR", "PHOENIX LORD", 
                "SWOOPING HAWKS", "BAHARROTH"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Baharroth();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Baharroth - " + Points + "pts";
        }
    }
}
