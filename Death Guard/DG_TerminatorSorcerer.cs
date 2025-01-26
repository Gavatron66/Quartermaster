using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DG_TerminatorSorcerer : Datasheets
    {
        public DG_TerminatorSorcerer()
        {
            DEFAULT_POINTS = 110;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m_pc";
            Weapons.Add("Combi-bolter");
            Weapons.Add("Force Stave");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "CHARACTER", "PSYKER", "BUBONIC ASTARTES", "TERMINATOR", "SORCERER"
            });
            PsykerPowers = new string[2] { string.Empty, string.Empty };
            Role = "HQ";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            factionsRestrictions = repo.restrictedItems;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            if (repo.hasWarlord && !isWarlord)
            {
                cbWarlord.Enabled = false;
            }
            else
            {
                cmbWarlord.Items.Clear();
                List<string> traits = repo.GetWarlordTraits("");
                foreach (var item in traits)
                {
                    cmbWarlord.Items.Add(item);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Combi-bolter",
                "Combi-melta",
                "Lightning Claw"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Chainfist",
                "Force Axe",
                "Force Stave",
                "Lightning Claw",
                "Power Fist"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

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

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            lblPsyker.Text = "Select two of the following:";
            clbPsyker.ClearSelected();
            for (int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            for(int i = 0; i < PsykerPowers.Length; i++)
            {
                if (PsykerPowers[i] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[i]), true);
                }
            }

            if (repo.hasRelic && Relic == "(None)")
            {
                cmbRelic.Enabled = false;
                cmbRelic.SelectedIndex = -1;
            }
            else
            {
                cmbRelic.Enabled = true;
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
            return "DG Terminator Sorcerer - " + Points + "pts";
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox factionud = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox isWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox warlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckedListBox clb = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    ComboBox cmb1 = panel.Controls["cmbOption1"] as ComboBox;
                    Weapons[0] = cmb1.SelectedItem as string;
                    break;
                case 12:
                    ComboBox cmb2 = panel.Controls["cmbOption2"] as ComboBox;
                    Weapons[1] = cmb2.SelectedItem as string;
                    break;
                case 25:
                    if (isWarlord.Checked)
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
                        warlord.SelectedIndex = -1;
                    }
                    break;
                case 15:
                    if (!factionsRestrictions.Contains(warlord.Text))
                    {
                        if (WarlordTrait == "")
                        {
                            WarlordTrait = warlord.Text;
                            if (WarlordTrait != "")
                            {
                                repo.restrictedItems.Add(WarlordTrait);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(WarlordTrait);
                            WarlordTrait = warlord.Text;
                            if (WarlordTrait != "")
                            {
                                repo.restrictedItems.Add(WarlordTrait);
                            }
                        }
                    }
                    else
                    {
                        warlord.SelectedIndex = warlord.Items.IndexOf(WarlordTrait);
                    }
                    break;
                case 16:
                    if (!factionsRestrictions.Contains(factionud.Text))
                    {
                        if (Factionupgrade == "(None)")
                        {
                            Factionupgrade = factionud.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(Factionupgrade);
                            Factionupgrade = factionud.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                    }
                    else
                    {
                        factionud.SelectedIndex = factionud.Items.IndexOf(Factionupgrade);
                    }
                    break;
                case 60:
                    if (clb.CheckedItems.Count < 2)
                    {
                        break;
                    }
                    else if (clb.CheckedItems.Count == 2)
                    {
                        PsykerPowers[0] = clb.CheckedItems[0] as string;
                        PsykerPowers[1] = clb.CheckedItems[1] as string;
                    }
                    else
                    {
                        clb.SetItemChecked(clb.SelectedIndex, false);
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

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override Datasheets CreateUnit()
        {
            return new DG_TerminatorSorcerer();
        }
    }
}
