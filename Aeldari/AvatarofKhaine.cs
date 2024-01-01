using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class AvatarofKhaine : Datasheets
    {
        public AvatarofKhaine()
        {
            DEFAULT_POINTS = 280;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "CHARACTER", "MONSTER", "DAEMON", "AVATAR OF KHAINE"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new AvatarofKhaine();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Avatar of Khaine - " + Points + "pts";
        }
    }
}
