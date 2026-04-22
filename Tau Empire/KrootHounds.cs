using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class KrootHounds : Datasheets
    {
        public KrootHounds()
        {
            UnitSize = 4;
            DEFAULT_POINTS = 6;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "KROOT",
                "BEASTS", "T'AU AUXILIARY", "KROOT HOUNDS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new KrootHounds();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 4;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 12;
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
            return "Kroot Hounds - " + Points + "pts";
        }
    }
}
