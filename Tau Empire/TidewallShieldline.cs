using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class TidewallShieldline : Datasheets
    {
        public TidewallShieldline()
        {
            DEFAULT_POINTS = 80;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1k";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "<SEPT>",
                "TERRAIN", "BUILDING", "VEHICLE", "TRANSPORT", "TIDEWALL",
                "SHIEDLINE", "DEFENCE PLATFORM"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new TidewallShieldline();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cbOption1.Text = "Include a Tidewall Defence Platform (+80 pts)";
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
            CheckBox cb = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 21:
                    if (cb.Checked)
                    {
                        Weapons[0] = cb.Text;
                    }
                    else { Weapons[0] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;
            if(cb.Checked) { Points += 80; }
        }

        public override string ToString()
        {
            return "Tidewall Shieldline - " + Points + "pts";
        }
    }
}
