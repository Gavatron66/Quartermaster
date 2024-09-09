using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class OfficerOfTheFleet : Datasheets
    {
        public OfficerOfTheFleet()
        {
            DEFAULT_POINTS = 25;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "MILITARUM AUXILLA", "ATTACHE", "OFFICER OF THE FLEET"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new OfficerOfTheFleet();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Officer of the Fleet - " + Points + "pts";
        }
    }
}
