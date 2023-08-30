using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DG_ChaosLord : Datasheets
    {
        public DG_ChaosLord()
        {
            DEFAULT_POINTS = 85;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m_c";
            Weapons.Add("Bolt Pistol");
            Weapons.Add("Astartes Chainsword");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "CHARACTER", "BUBONIC ASTARTES", "LORD OF THE DEATH GUARD", "CHAOS LORD"
            });
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmb = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cb = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmb2 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmb3 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            if (isWarlord)
            {
                cb.Checked = true;
                cmb.Enabled = true;
                cmb.SelectedText = WarlordTrait;
            }
            else
            {
                cb.Checked = false;
                cmb.Enabled = false;
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

            cmb2.Items.Clear();
            cmb2.Items.AddRange(new string[] {
                "Balesword",
                "Bolt Pistol",
                "Chainaxe",
                "Combi-bolter",
                "Combi-flamer",
                "Combi-melta",
                "Combi-plasma",
                "Lightning Claw",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword"
            });
            cmb2.SelectedIndex = cmb2.Items.IndexOf(Weapons[0]);

            cmb3.Items.Clear();
            cmb3.Items.AddRange(new string[] {
                "Astartes Chainsword",
                "Balesword",
                "Chainaxe",
                "Lightning Claw",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword"
            });
            cmb3.SelectedIndex = cmb3.Items.IndexOf(Weapons[1]);

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

        public override string ToString()
        {
            return "Death Guard Chaos Lord - " + Points + "pts";
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox factionud = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox isWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox warlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmb1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmb2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmb1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmb2.SelectedItem as string;
                    break;
                case 25:
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

                    if (cmbRelic.SelectedItem.ToString() == "Plaguebringer")
                    {
                        cmb2.SelectedIndex = cmb2.Items.IndexOf("Power Sword");
                        cmb2.Enabled = false;
                    }
                    else
                    {
                        cmb2.Enabled = true;
                    }
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
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Balesword") || Weapons.Contains("Combi-flamer") || Weapons.Contains("Combi-melta")
                || Weapons.Contains("Combi-plasma") || Weapons.Contains("Power Fist"))
            {
                Points += 10;
            }

            if (Weapons.Contains("Lightning Claw") || Weapons.Contains("Power Axe") || Weapons.Contains("Power Maul")
                || Weapons.Contains("Power Sword"))
            {
                Points += 5;
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override Datasheets CreateUnit()
        {
            return new DG_ChaosLord();
        }
    }
}

