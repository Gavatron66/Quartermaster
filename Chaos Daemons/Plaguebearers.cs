using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class Plaguebearers : Datasheets
    {
        public Plaguebearers()
        {
            DEFAULT_POINTS = 130;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "2k";
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "NURGLE",
                "INFANTRY", "DAEMON", "CORE", "PLAGUEBEARERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Plaguebearers();
        }
        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosDaemons;

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cbOption1.Text = "Instrument of Chaos";
            if (Weapons[0] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Daemonic Icon";
            if (Weapons[1] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

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
                case 22:
                    CheckBox cb2 = panel.Controls["cbOption2"] as CheckBox;
                    if (cb2.Checked)
                    {
                        Weapons[1] = cb2.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Plaguebearers - " + Points + "pts";
        }
    }
}
