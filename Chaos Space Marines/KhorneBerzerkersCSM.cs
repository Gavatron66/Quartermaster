using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class KhorneBerzerkersCSM : Datasheets
    {
        public KhorneBerzerkersCSM()
        {
            Points = 0;
        }

        public override Datasheets CreateUnit()
        {
            return new KhorneBerzerkersCSM();
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
            return "Khorne Berzerkers - " + Points + "pts";
        }
    }
}
