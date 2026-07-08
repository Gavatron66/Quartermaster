using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class RogueTrader : Datasheets
    {
        bool executioner = false;
        bool lectro_maester = false;
        bool adept = false;

        public RogueTrader()
        {
            DEFAULT_POINTS = 60;
            Points = DEFAULT_POINTS;
            TemplateCode = "3k_c";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "NAVIS IMPERIALIS", "ASTRA CARTOGRAPHICA", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "CHARACTER", "ROGUE TRADER", "CONCUSSION GRENADES", "CARTOGRAPHICA ROGUE TRADER",
                "INFANTRY", "CONCUSSION GRENADES", "ROGUE TRADER RETINUE"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new RogueTrader();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ImperialAgents;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            panel.Controls["lblRelic"].Visible = false;
            panel.Controls["cmbRelic"].Visible = false;
            panel.Controls["cbStratagem1"].Visible = false;
            panel.Controls["cbStratagem2"].Visible = false;

            cbOption1.Text = "Death Cult Executioner (+15 pts)";
            cbOption1.Checked = executioner;

            cbOption2.Text = "Lectro-maester (+15 pts)";
            cbOption2.Checked = lectro_maester;

            cbOption3.Text = "Rejuvenant Adept (+10 pts)";
            cbOption3.Checked = adept;

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
                cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            switch (code)
            {
                case 15:
                    if (cmbWarlord.SelectedIndex != -1)
                    {
                        WarlordTrait = cmbWarlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }
                    break;
                case 21:
                    executioner = cbOption1.Checked;
                    break;
                case 22:
                    lectro_maester = cbOption2.Checked;
                    break;
                case 23:
                    adept = cbOption3.Checked;
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += executioner ? 15 : 0;
            Points += lectro_maester ? 15 : 0;
            Points += adept ? 10 : 0;
        }

        public override string ToString()
        {
            return "Cartographica Rogue Trader - " + Points + "pts";
        }
    }
}