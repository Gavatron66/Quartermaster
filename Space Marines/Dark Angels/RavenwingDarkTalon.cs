using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Dark_Angels
{
    public class RavenwingDarkTalon : Datasheets
    {
        public RavenwingDarkTalon()
        {
            DEFAULT_POINTS = 170;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DARK ANGELS",
                "VEHICLE", "FLY", "AIRCRAFT", "RAVENWING", "RAVENWING DARK TALON"
            });
            Role = "Flyer";
        }

        public override Datasheets CreateUnit()
        {
            return new RavenwingDarkTalon();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }
        public override string ToString()
        {
            return "Ravenwing Dark Talon - " + Points + "pts";
        }
    }
}
