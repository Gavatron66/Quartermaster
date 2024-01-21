using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class SporeMines : Datasheets
    {
        public SporeMines()
        {
            DEFAULT_POINTS = 10;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "LIVING ARTILLERY",
                "BEAST", "FLY", "SPORE MINES"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new SporeMines();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Spore Mines - " + Points + "pts";
        }
    }
}
