using Roster_Builder.Death_Guard;
using Roster_Builder.Necrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    internal class PurestrainGenestealers : Datasheets
    {

        public PurestrainGenestealers()
        {
            UnitSize = 5;
            DEFAULT_POINTS = 14;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "TYRANIDS", "GENESTEALER CULTS", "<CULT>",
                "INFANTRY", "GENESTEALER", "PURESTRAIN GENESTEALERS"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new PurestrainGenestealers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
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
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

        }

        public override string ToString()
        {
            return "Purestrain Genestealers - " + Points + "pts";
        }
    }
}