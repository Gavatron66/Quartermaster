using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class PteraxiiSkystalkers : Datasheets
    {
        public PteraxiiSkystalkers()
        {
            DEFAULT_POINTS = 16;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "INFANTRY", "JUMP PACK", "FLY", "CORE", "PTERAXII", "ARC GRENADES", "PTERAXII SKYSTALKERS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new PteraxiiSkystalkers();
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
            return "Pteraxii Skystalkers - " + Points + "pts";
        }
    }
}
