using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class HexmarkDestroyer : Datasheets
    {
        Necrons repo = new Necrons();
        public HexmarkDestroyer()
        {
            DEFAULT_POINTS = 75;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "c";
            Keywords.AddRange(new string[]
            {
                "NECRONS", "DESTROYER CULT", "<DYNASTY>",
                "INFANTRY", "CHARACTER", "HYPERSPACE HUNTER", "HEXMARK DESTROYER"
            });
        }
        public override Datasheets CreateUnit()
        {
            return new HexmarkDestroyer();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
                cmbWarlord.SelectedText = WarlordTrait;
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (Relic != null)
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = -1;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Hexmark Destroyer - " + Points + "pts";
        }
    }
}
