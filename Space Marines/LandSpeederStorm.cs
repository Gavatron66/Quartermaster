using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class LandSpeederStorm : Datasheets
    {
        public LandSpeederStorm()
        {
            Points = 50;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "LAND SPEEDER", "SCOUT", "FLY", "TRANSPORT", "LAND SPEEDER STORM"
            });
            role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new LandSpeederStorm();
        }

        public override void LoadDatasheets(Panel panel, Faction f) { }

        public override void SaveDatasheets(int code, Panel panel) { }

        public override string ToString()
        {
            return "Land Speeder Storm - " + Points + "pts";
        }
    }
}
