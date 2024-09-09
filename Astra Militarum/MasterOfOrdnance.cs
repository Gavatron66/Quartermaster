using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class MasterOfOrdnance : Datasheets
    {
        public MasterOfOrdnance()
        {
            DEFAULT_POINTS = 25;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "MILITARUM AUXILLA", "ATTACHE", "MASTER OF ORDNANCE"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new MasterOfOrdnance();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Master of Ordnance - " + Points + "pts";
        }
    }
}
