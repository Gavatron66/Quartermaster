using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class RazorwingFlock : Datasheets
    {
        public RazorwingFlock()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI",
                "BEAST", "SWARM", "FLY", "BLADES FOR HIRE", "RAZORWING FLOCKS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new RazorwingFlock();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Drukhari;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = Convert.ToInt32(UnitSize);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Razorwing Flock - " + Points + "pts";
        }
    }
}
