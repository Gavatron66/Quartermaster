using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Fuegan : Datasheets
    {
        public Fuegan()
        {
            DEFAULT_POINTS = 170;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI",
                "CHARACTER", "INFANTRY", "ASPECT WARRIOR", "PHOENIX LORD", "FIRE DRADONS", "FUEGAN"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Fuegan();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Fuegan - " + Points + "pts";
        }
    }
}
