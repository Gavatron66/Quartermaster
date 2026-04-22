using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Stompa : Datasheets
    {
        public Stompa()
        {
            DEFAULT_POINTS = 675;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "VEHICLE", "TRANSPORT", "TITANIC", "WALKERZ", "STOMPA"
            });
            Role = "Lord of War";
        }

        public override Datasheets CreateUnit()
        {
            return new Stompa();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Stompa - " + Points + "pts";
        }
    }
}
