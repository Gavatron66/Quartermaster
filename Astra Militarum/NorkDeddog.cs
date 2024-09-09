using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class NorkDeddog : Datasheets
    {
        public NorkDeddog()
        {
            DEFAULT_POINTS = 60;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "MILITARUM AUXILLA", "ABHUMAN", "BODYGUARD", "OGRYNS", "NORK DEDDOG"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new NorkDeddog();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Nork Deddog - " + Points + "pts";
        }
    }
}
