using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class SororitasRhino : Datasheets
    {
        public SororitasRhino()
        {
            DEFAULT_POINTS = 80;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "1k";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS", "<ORDER>",
                "VEHICLE", "TRANSPORT", "HALLOWED", "SMOKESCREEN", "SORORITAS RHINO"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new SororitasRhino();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cbOption1.Text = "Hunter-killer Missile (+5 pts)";
            if (Weapons[0] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else { Weapons[0] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (cbOption1.Checked)
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Sororitas Rhino - " + Points + "pts";
        }
    }
}
