using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class LordOfContagion : Datasheets
    {
        DeathGuard repo = new DeathGuard();
        public LordOfContagion()
        {
            DEFAULT_POINTS = 120;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1m_c";
            Weapons.Add("Plaguereaper");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "CHARACTER", "BUBONIC ASTARTES", "TERMINATOR", "LORD OF THE DEATH GUARD", "LORD OF CONTAGION"
            });
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Plaguereaper",
                "Manreaper and Orb of Desiccation"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

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

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
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

            panel.Controls["lblFactionupgrade"].Visible = true;
            panel.Controls["cmbFactionupgrade"].Visible = true;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmb1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox factionud = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox isWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox warlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmb1.SelectedItem as string;
                    break;
                case 35:
                    if (isWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; }
                    break;
                case 15:
                    if (warlord.SelectedIndex != -1)
                    {
                        WarlordTrait = warlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }

                    break;
                case 16:
                    Factionupgrade = factionud.Text;
                    break;
                case 17:
                    Relic = cmbRelic.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            if (!Weapons.Contains("Plaguereaper"))
            {
                Points += 5;
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Lord of Contagion - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new LordOfContagion();
        }
    }
}
