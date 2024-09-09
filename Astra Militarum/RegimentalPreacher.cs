using Roster_Builder.Death_Guard;
using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class RegimentalPreacher : Datasheets
    {
        public RegimentalPreacher()
        {
            DEFAULT_POINTS = 40;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m_c";
            Weapons.Add("Autopistol, Servo-stubber and Power Maul (+5 pts)");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "CHARACTER", "MILITARUM AUXILLA", "PREACHER"
            });
            Role = "Elites";
            WarlordTrait = "Front-line Combatant";
        }

        public override Datasheets CreateUnit()
        {
            return new RegimentalPreacher();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Autogun, Autopistol and Chainsword",
                "Autopistol, Servo-stubber and Power Maul (+5 pts)",
                "Bolter, Preacher Shotgun and Chainsword"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

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

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            if (Stratagem.Contains(cbStratagem1.Text))
            {
                cbStratagem1.Checked = true;
                cbStratagem1.Enabled = true;
            }
            else
            {
                cbStratagem1.Checked = false;
                cbStratagem1.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem1.Text));
            }

            if (Stratagem.Contains(cbStratagem2.Text))
            {
                cbStratagem2.Checked = true;
                cbStratagem2.Enabled = true;
            }
            else
            {
                cbStratagem2.Checked = false;
                cbStratagem2.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem2.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                        cmbWarlord.Text = WarlordTrait;
                        cmbWarlord.Enabled = false;
                    }
                    else { this.isWarlord = false; }
                    break;
                case 71:
                    if (cbStratagem1.Checked)
                    {
                        Stratagem.Add(cbStratagem1.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem1.Text))
                        {
                            Stratagem.Remove(cbStratagem1.Text);
                        }
                    }
                    break;
                case 72:
                    if (cbStratagem2.Checked)
                    {
                        Stratagem.Add(cbStratagem2.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem2.Text))
                        {
                            Stratagem.Remove(cbStratagem2.Text);
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[0].Contains("Power Maul (+5 pts)"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Regimental Preacher - " + Points + "pts";
        }
    }
}