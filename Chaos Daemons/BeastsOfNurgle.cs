using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class BeastsOfNurgle : Datasheets
    {
        public BeastsOfNurgle()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "NURGLE",
                "BEAST", "DAEMON", "BEASTS OF NURGLE"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new BeastsOfNurgle();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosDaemons;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
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
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Beasts of Nurgle - " + Points + "pts";
        }
    }
}
