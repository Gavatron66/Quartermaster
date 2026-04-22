using Roster_Builder.Adeptus_Custodes;
using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class HuntaRig : Datasheets
    {
        public HuntaRig()
        {
            DEFAULT_POINTS = 160;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "VEHICLE", "TRANSPORT", "BEAST SNAGGA", "HUNTA RIG"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new HuntaRig();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Hunta Rig - " + Points + "pts";
        }
    }
}