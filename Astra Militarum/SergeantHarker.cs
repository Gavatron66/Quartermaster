using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class SergeantHarker : Datasheets
    {
        public SergeantHarker()
        {
            DEFAULT_POINTS = 40;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "CATACHAN",
                "INFANTRY", "CHARACTER", "OFFICER", "REGIMENTAL", "SERGEANT HARKER"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new SergeantHarker();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Sergeant Harker - " + Points + "pts";
        }
    }
}
