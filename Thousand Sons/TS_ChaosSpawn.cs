using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class TS_ChaosSpawn : Datasheets
    {
        public TS_ChaosSpawn()
        {
            UnitSize = 1;
            DEFAULT_POINTS = 21;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TZEENTCH", "HERETIC ASTARTES", "THOUSAND SONS", "<GREAT CULT>",
                "BEAST", "CHAOS SPAWN"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new TS_ChaosSpawn();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ThousandSons;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

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
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

        }

        public override string ToString()
        {
            return "Chaos Spawn - " + Points + "pts";
        }
    }
}
