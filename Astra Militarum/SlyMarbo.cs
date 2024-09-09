using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class SlyMarbo : Datasheets
    {
        public SlyMarbo()
        {
            DEFAULT_POINTS = 50;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "CATACHAN",
                "INFANTRY", "CHARACTER", "PLATOON", "MELTA MINE", "REGIMENTAL", "SLY MARBO"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new SlyMarbo();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Sly Marbo - " + Points + "pts";
        }
    }
}
