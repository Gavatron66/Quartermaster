using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class ChaosSpawn : Datasheets
    {
        public ChaosSpawn()
        {
            UnitSize = 1;
            DEFAULT_POINTS = 23;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "BEAST", "CHAOS SPAWN"
            });
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 5;
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
            return "Chaos Spawn - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new ChaosSpawn();
        }
    }
}

