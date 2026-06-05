using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class BurningChariot : Datasheets
    {
        public BurningChariot()
        {
            DEFAULT_POINTS = 120;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1k";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "TZEENTCH",
                "VEHICLE", "DAEMON", "FLY", "EXALTED FLAMER", "FLAMERS", "BURNING CHARIOT"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new BurningChariot();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosDaemons;

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cbOption1.Text = "Horror Infestation (+5 pts)";
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
            switch (code)
            {
                case 21:
                    CheckBox cb = panel.Controls["cbOption1"] as CheckBox;
                    if (cb.Checked)
                    {
                        Weapons[0] = cb.Text;
                    }
                    else { Weapons[0] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0] != "")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Burning Chariot - " + Points + "pts";
        }
    }
}
