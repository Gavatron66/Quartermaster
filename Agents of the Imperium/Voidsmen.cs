using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class Voidsmen : Datasheets
    {
        bool canid = false;

        public Voidsmen()
        {
            DEFAULT_POINTS = 10;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1k";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "NAVIS IMPERIALIS", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "CORE", "CONCUSSION GRENADES", "VOIDSMEN-AT-ARMS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Voidsmen();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ImperialAgents;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            cbOption1.Text = "Canid (+10 pts)";
            cbOption1.Checked = canid;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 21:
                    canid = cbOption1.Checked;
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS * (UnitSize + (canid ? 1 : 0));
        }

        public override string ToString()
        {
            return "Voidsmen-at-Arms - " + Points + "pts";
        }
    }
}
