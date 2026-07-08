using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class MutoidVermin : Datasheets
    {
        public MutoidVermin()
        {
            DEFAULT_POINTS = 80;
            UnitSize = 16;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE",
                "BEAST", "MUTOID VERMIN"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new MutoidVermin();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Mutoid Vermin - " + Points + "pts";
        }
    }
}
