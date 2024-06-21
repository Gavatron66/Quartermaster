using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class SkorpiusDunerider : Datasheets
    {
        public SkorpiusDunerider()
        {
            DEFAULT_POINTS = 85;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "VEHICLE", "TRANSPORT", "DATA-TETHER", "SKORPIUS ENGINE", "SKORPIUS DUNERIDER"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new SkorpiusDunerider();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Skorpius Dunerider - " + Points + "pts";
        }
    }
}
