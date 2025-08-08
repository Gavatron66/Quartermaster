using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Baharroth : Datasheets
    {
        public Baharroth()
        {
            DEFAULT_POINTS = 160;
            TemplateCode = "nc";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI",
                "CHARACTER", "INFANTRY", "JUMP PACK", "FLY", "ASPECT WARRIOR", "PHOENIX LORD", 
                "SWOOPING HAWKS", "BAHARROTH"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Baharroth();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Aeldari;

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            cmbWarlord.Visible = false;
            panel.Controls["lblWarlord"].Visible = false;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
            }
            else
            {
                cbWarlord.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            switch (code)
            {
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; }
                    break;
                default: break;
            }
        }

        public override string ToString()
        {
            return "Baharroth - " + Points + "pts";
        }
    }
}
