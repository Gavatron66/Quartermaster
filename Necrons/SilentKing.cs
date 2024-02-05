using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class SilentKing : Datasheets
    {
        public SilentKing()
        {
            DEFAULT_POINTS = 400;
            TemplateCode = "nc";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "SZAREKHAN",
                "VEHICLE", "CHARACTER", "FLY", "SUPREME COMMANDER", "PHAERON", "NOBLE",
                "DYNASTIC AGENT", "THE SILENT KING", "SZAREKH",
                "TRIARCHAL MENHIRS"
            });
            WarlordTrait = "The Triarch's Will";
            Role = "Lord of War";
        }
        public override Datasheets CreateUnit()
        {
            return new SilentKing();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = false;
                cmbWarlord.Text = WarlordTrait;
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }
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
            return "The Silent King - " + Points + "pts";
        }
    }
}
