using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Beastmaster : Datasheets
    {
        public Beastmaster()
        {
            UnitSize = 1;
            Points = 35;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<WYCH CULT>",
                "INFANTRY", "CHARACTER", "SKYBOARD", "FLY", "BEASTMASTER"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Beastmaster();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Beastmaster - " + Points + "pts";
        }
    }
}
