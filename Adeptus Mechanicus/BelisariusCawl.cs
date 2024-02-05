using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class BelisariusCawl : Datasheets
    {
        public BelisariusCawl()
        {
            DEFAULT_POINTS = 160;
            UnitSize = 1;
            TemplateCode = "nc";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "CULT MECHANICUS", "MARS",
                "MONSTER", "CHARACTER", "SUPREME COMMANDER", "DOCTRINA ASSEMBLER", "TECH-PRIEST", 
                "BELISARIUS CAWL"
            });
            WarlordTrait = "Masterwork Bionics";
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new BelisariusCawl();
        }
        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdMech;

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            cmbWarlord.Enabled = false;
            cmbWarlord.Items.Clear();
            cmbWarlord.Items.Add(WarlordTrait);
            cmbWarlord.SelectedIndex = 0;

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
                    else { this.isWarlord = false; }
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
        }

        public override string ToString()
        {
            return "Belisarius Cawl - " + Points + "pts";
        }
    }
}
