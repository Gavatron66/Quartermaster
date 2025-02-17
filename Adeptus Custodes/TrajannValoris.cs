using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Custodes
{
    public class TrajannValoris : Datasheets
    {
        public TrajannValoris()
        {
            DEFAULT_POINTS = 200;
            TemplateCode = "nc";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS CUSTODES", "<SHIELD HOST>",
                "INFANTRY", "CHARACTER", "CAPTAIN-GENERAL", "TRAJANN VALORIS"
            });
            WarlordTrait = "Champion of the Imperium; Master of Martial Strategy";
            Role = "HQ";
        }
        public override Datasheets CreateUnit()
        {
            return new TrajannValoris();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdeptusCustodes;

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            cbWarlord.Checked = true;
            cbWarlord.Enabled = false;
            cmbWarlord.Enabled = false;
            cmbWarlord.Items.Clear();
            cmbWarlord.Items.Add("Champion of the Imperium; Master of Martial Strategy");
            cmbWarlord.SelectedIndex = 0;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            switch (code)
            {
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                        cmbWarlord.Text = WarlordTrait;
                        cmbWarlord.Enabled = false;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
                    break;
                default: break;
            }

            if (code == -1)
            {
                if (this.isWarlord)
                {
                    cmbWarlord.Text = WarlordTrait;
                    cmbWarlord.Enabled = false;
                }
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Trajann Valoris - " + Points + "pts";
        }
    }
}
