using Roster_Builder.Tyranids;
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
            Role = "HQ";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            factionsRestrictions = repo.restrictedItems;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            if (repo.hasWarlord && !isWarlord)
            {
                cbWarlord.Enabled = false;
            }

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
            else if (Stratagem.Contains(cbStratagem1.Text))
            {
                cmbWarlord.Enabled = true;
                cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }

            restrictedIndexes = new List<int>();
            for (int i = 0; i < cmbWarlord.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(cmbWarlord.Items[i]) && WarlordTrait != cmbWarlord.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbWarlord);

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

            restrictedIndexes = new List<int>();
            for (int i = 0; i < cmbFaction.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(cmbFaction.Items[i]) && Factionupgrade != cmbFaction.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbFaction);

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[] 
            {
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
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[] 
            {
                "Astartes Chainsword",
                "Balesword",
                "Chainaxe",
                "Lightning Claw",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (repo.hasRelic && Relic == "(None)")
            {
                cmbRelic.Enabled = false;
                cmbRelic.SelectedIndex = -1;
            }
            else
            {
                cmbRelic.Enabled = true;

                if (Relic != null)
                {
                    cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                }
                else
                {
                    cmbRelic.SelectedIndex = -1;
                }
            }

            restrictedIndexes = new List<int>();
            for (int i = 0; i < cmbRelic.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(cmbRelic.Items[i]) && Relic != cmbRelic.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbRelic);

            panel.Controls["lblFactionupgrade"].Visible = true;
            panel.Controls["cmbFactionupgrade"].Visible = true;

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
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                        repo.hasWarlord = true;
                    }
                    else
                    {
                        if (this.isWarlord)
                        {
                            repo.hasWarlord = false;
                        }
                        this.isWarlord = false;
                        cmbWarlord.SelectedIndex = -1;
                    }
                    break;
                case 15:
                    if (!factionsRestrictions.Contains(cmbWarlord.Text))
                    {
                        if (WarlordTrait == "")
                        {
                            WarlordTrait = cmbWarlord.Text;
                            if (WarlordTrait != "")
                            {
                                repo.restrictedItems.Add(WarlordTrait);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(WarlordTrait);
                            WarlordTrait = cmbWarlord.Text;
                            if (WarlordTrait != "")
                            {
                                repo.restrictedItems.Add(WarlordTrait);
                            }
                        }
                    }
                    else
                    {
                        cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
                    }
                    break;
                case 16:
                    if (!factionsRestrictions.Contains(cmbFactionupgrade.Text))
                    {
                        if (Factionupgrade == "(None)")
                        {
                            Factionupgrade = cmbFactionupgrade.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(Factionupgrade);
                            Factionupgrade = cmbFactionupgrade.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                    }
                    else
                    {
                        cmbFactionupgrade.SelectedIndex = cmbFactionupgrade.Items.IndexOf(Factionupgrade);
                    }
                    break;
                case 17:
                    if (!factionsRestrictions.Contains(cmbRelic.Text))
                    {
                        if (Relic == "(None)")
                        {
                            Relic = cmbRelic.Text == "" ? "(None)" : cmbRelic.Text;
                            if (!repo.hasRelic && Relic != "(None)")
                            {
                                hasFreeRelic = true;
                                repo.hasRelic = true;
                            }

                            if (Relic != "(None)")
                            {
                                repo.restrictedItems.Add(Relic);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(Relic);
                            Relic = cmbRelic.Text;
                            if (Relic != "(None)")
                            {
                                repo.restrictedItems.Add(Relic);
                            }
                            else
                            {
                                if (repo.hasRelic && hasFreeRelic)
                                {
                                    hasFreeRelic = false;
                                    repo.hasRelic = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        cmbRelic.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Plaguebringer")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Sword");
                        cmbOption2.Enabled = false;
                    }
                    else
                    {
                        cmbOption2.Enabled = true;
                    }
                    break;
                case 71:
                    if (cbStratagem1.Checked)
                    {
                        if (!Stratagem.Contains(cbStratagem1.Text))
                        {
                            Stratagem.Add(cbStratagem1.Text);
                        }
                        cmbWarlord.Enabled = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem1.Text))
                        {
                            Stratagem.Remove(cbStratagem1.Text);
                            if (repo.hasWarlord)
                            {
                                cmbWarlord.Enabled = false;
                                cmbWarlord.SelectedIndex = -1;
                            }
                        }
                    }
                    break;
                case 72:
                    if (cbStratagem2.Checked)
                    {
                        if (!Stratagem.Contains(cbStratagem2.Text))
                        {
                            Stratagem.Add(cbStratagem2.Text);
                        }
                        cmbRelic.Enabled = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem2.Text))
                        {
                            Stratagem.Remove(cbStratagem2.Text);
                            if (repo.hasRelic)
                            {
                                cmbRelic.Enabled = false;
                                cmbRelic.SelectedIndex = 0;
                            }
                        }
                    }
                    break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override Datasheets CreateUnit()
        {
            return new DG_ChaosLord();
        }
    }
}

