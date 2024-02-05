using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class Poxwalkers : Datasheets
    {
        public Poxwalkers()
        {
            UnitSize = 10;
            DEFAULT_POINTS = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "POXWALKERS"
            });
            Role = "Troops";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 20;
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
            return "Poxwalkers - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new Poxwalkers();
        }
    }
}

