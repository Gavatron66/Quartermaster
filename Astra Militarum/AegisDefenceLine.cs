using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class AegisDefenceLine : Datasheets
    {
        public AegisDefenceLine()
        {
            DEFAULT_POINTS = 40;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "TERRAIN", "OBSTACLE", "AEGIS DEFENCE LINE"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new AegisDefenceLine();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Aegis Defence Line - " + Points + "pts";
        }
    }
}
