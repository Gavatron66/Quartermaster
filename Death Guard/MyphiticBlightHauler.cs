using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class MyphiticBlightHauler : Datasheets
    {
        public MyphiticBlightHauler()
        {
            DEFAULT_POINTS = 140;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "VEHICLE", "DAEMON", "DAEMON ENGINE", "MYPHITIC BLIGHT-HAULERS"
            });
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = Decimal.ToInt16(nud.Value);
                    Points = UnitSize * DEFAULT_POINTS;
                    break;
            }
        }

        public override string ToString()
        {
            return "Myphitic Blight-hauler - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new MyphiticBlightHauler();
        }
    }
}
