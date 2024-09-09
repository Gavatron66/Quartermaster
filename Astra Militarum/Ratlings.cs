using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Ratlings : Datasheets
    {
        public Ratlings()
        {
            DEFAULT_POINTS = 50;
            UnitSize = 5;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "MILITARUM AUXILLA", "ABHUMAN", "RATLINGS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Ratlings();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Ratlings - " + Points + "pts";
        }
    }
}
