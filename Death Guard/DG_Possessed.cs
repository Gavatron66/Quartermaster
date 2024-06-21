using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DG_Possessed : Datasheets
    {
        public DG_Possessed()
        {
            UnitSize = 5;
            DEFAULT_POINTS = 24;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "CORE", "DAEMON", "BUBONIC ASTARTES", "DEATH GUARD POSSESSED"
            });
            Role = "Elites";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = UnitSize;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;
            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nud.Value.ToString());
                    Points = UnitSize * DEFAULT_POINTS;
                    break;
            }
        }

        public override string ToString()
        {
            return "Death Guard Possessed - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new DG_Possessed();
        }
    }
}

