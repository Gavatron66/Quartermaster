using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class MauganRa : Datasheets
    {
        public MauganRa()
        {
            DEFAULT_POINTS = 160;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI",
                "CHARACTER", "INFANTRY", "ASPECT WARRIOR", "PHOENIX LORD", "DARK REAPERS", "MAUGAN RA"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new MauganRa();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Maugan Ra - " + Points + "pts";
        }
    }
}
