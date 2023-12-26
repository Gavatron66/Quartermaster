using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class StormSpeederThunderstrike : Datasheets
    {
        public StormSpeederThunderstrike()
        {
            DEFAULT_POINTS = 135;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "FLY", "STORM SPEEDER", "THUNDERSTRIKE"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new StormSpeederThunderstrike();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {

        }

        public override void SaveDatasheets(int code, Panel panel)
        {

        }

        public override string ToString()
        {
            return "Storm Speeder Thunderstrike - " + Points + "pts";
        }
    }
}
