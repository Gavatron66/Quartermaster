using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class RubricMarinesCSM : Datasheets
    {
        public RubricMarinesCSM()
        {
            Points = 0;
        }

        public override Datasheets CreateUnit()
        {
            return new RubricMarinesCSM();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            throw new NotImplementedException();
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Rubric Marines - " + Points + "pts";
        }
    }
}
