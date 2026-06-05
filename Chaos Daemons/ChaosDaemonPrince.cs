using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class ChaosDaemonPrince : Datasheets
    {
        public ChaosDaemonPrince()
        {
            DEFAULT_POINTS = 140;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m1k_pc";
            Weapons.Add("Hellforged Sword (+10 pts)");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "<ALLEGIANCE>",
                "CHARACTER", "MONSTER", "DAEMON", "DAEMON PRINCE", "PSYKER"
            });
            PsykerPowers = new string[1] { string.Empty };
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new ChaosDaemonPrince();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosDaemons;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cbStratagem2"].Visible = false;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;

            panel.Controls["lblFactionupgrade"].Visible = false;
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(panel.Controls["lblFactionupgrade"].Location.X, panel.Controls["lblFactionupgrade"].Location.Y);
            panel.Controls["lblExtra1"].Visible = true;
            panel.Controls["lblExtra1"].Text = "Daemonic Allegiance";

            if(Factionupgrade == null || Factionupgrade == "Select one of the following")
            {
                //Hide all the controls until an allegiance is chosen
                #region Hide Controls
                panel.Controls["lblOption1"].Visible = false;
                panel.Controls["cmbOption1"].Visible = false;
                panel.Controls["cbOption1"].Visible = false;
                panel.Controls["lblPsyker"].Visible = false;
                panel.Controls["clbPsyker"].Visible = false;
                panel.Controls["cbWarlord"].Visible = false;
                panel.Controls["lblWarlord"].Visible = false;
                panel.Controls["cmbWarlord"].Visible = false;
                panel.Controls["lblRelic"].Visible = false;
                panel.Controls["cmbRelic"].Visible = false;
                panel.Controls["cbStratagem1"].Visible = false;
                #endregion
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Daemonic Axe (+10 pts)",
                "Hellforged Sword (+10 pts)",
                "Malefic Talons"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Wings (+35 pts)";
            if (Weapons[1] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cmbFaction.Visible = true;

            cmbFaction.Items.Clear();
            if(Factionupgrade == null || Factionupgrade == "Select one of the following")
            {
                cmbFaction.Items.Add("Select one of the following");
            }
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            antiLoop = true;
            if (Factionupgrade != null && Factionupgrade != "Select one of the following")
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            if (Factionupgrade == "Khorne")
            {
                panel.Controls["lblPsyker"].Visible = false;
                panel.Controls["clbPsyker"].Visible = false;

                cmbWarlord.Items.Clear();
                cmbWarlord.Items.AddRange(repo.GetWarlordTraits("KHORNE").ToArray());

                cmbRelic.Items.Clear();
                Keywords.Add("KHORNE");
                cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                Keywords.Remove("KHORNE");

                cbStratagem1.Text = "Stratagem: Relics of the Brass Citadel";
            }
            else if (Factionupgrade == "Tzeentch")
            {
                clbPsyker.Items.Clear();
                clbPsyker.Items.AddRange(repo.GetPsykerPowers("TZEENTCH").ToArray());

                cmbWarlord.Items.Clear();
                cmbWarlord.Items.AddRange(repo.GetWarlordTraits("TZEENTCH").ToArray());

                cmbRelic.Items.Clear();
                Keywords.Add("TZEENTCH");
                cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                Keywords.Remove("TZEENTCH");

                cbStratagem1.Text = "Stratagem: Relics of the Impossible Fortress";
            }
            else if (Factionupgrade == "Nurgle")
            {
                clbPsyker.Items.Clear();
                clbPsyker.Items.AddRange(repo.GetPsykerPowers("NURGLE").ToArray());

                cmbWarlord.Items.Clear();
                cmbWarlord.Items.AddRange(repo.GetWarlordTraits("NURGLE").ToArray());

                cmbRelic.Items.Clear();
                Keywords.Add("NURGLE");
                cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                Keywords.Remove("NURGLE");

                cbStratagem1.Text = "Stratagem: Relics of the Great Garden";
            }
            else if (Factionupgrade == "Slaanesh")
            {
                clbPsyker.Items.Clear();
                clbPsyker.Items.AddRange(repo.GetPsykerPowers("SLAANESH").ToArray());

                cmbWarlord.Items.Clear();
                cmbWarlord.Items.AddRange(repo.GetWarlordTraits("SLAANESH").ToArray());

                cmbRelic.Items.Clear();
                Keywords.Add("SLAANESH");
                cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                Keywords.Remove("SLAANESH");

                cbStratagem1.Text = "Stratagem: Exquisite Gifts";
            }
            else
            {
                cmbRelic.Items.Clear();
                cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
            }
            antiLoop = false;

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

            if (Relic != null && cmbRelic.Items.Contains(Relic))
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = 0;
            }

            lblPsyker.Text = "Select one of the following:";
            clbPsyker.ClearSelected();
            for (int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
            }

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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop) { return; }

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
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
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    if(Factionupgrade != null && Factionupgrade != "Select one of the following")
                    {
                        #region Show Controls
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["cmbOption1"].Visible = true;
                        panel.Controls["cbOption1"].Visible = true;
                        panel.Controls["lblPsyker"].Visible = true;
                        panel.Controls["clbPsyker"].Visible = true;
                        panel.Controls["cbWarlord"].Visible = true;
                        panel.Controls["lblWarlord"].Visible = true;
                        panel.Controls["cmbWarlord"].Visible = true;
                        panel.Controls["lblRelic"].Visible = true;
                        panel.Controls["cmbRelic"].Visible = true;
                        panel.Controls["cbStratagem1"].Visible = true;

                        if(cmbFaction.Items.Count == 5)
                        {
                            cmbFaction.Items.RemoveAt(0);
                        }
                        #endregion
                    }
                    
                    if (Factionupgrade == "Khorne")
                    {
                        panel.Controls["lblPsyker"].Visible = false;
                        panel.Controls["clbPsyker"].Visible = false;

                        cmbWarlord.Items.Clear();
                        cmbWarlord.Items.AddRange(repo.GetWarlordTraits("KHORNE").ToArray());

                        cmbRelic.Items.Clear();
                        Keywords.Add("KHORNE");
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                        cmbRelic.SelectedIndex = 0;
                        Keywords.Remove("KHORNE");

                        cbStratagem1.Text = "Stratagem: Relics of the Brass Citadel";
                    }
                    else if (Factionupgrade == "Tzeentch")
                    {
                        clbPsyker.Items.Clear();
                        clbPsyker.Items.AddRange(repo.GetPsykerPowers("TZEENTCH").ToArray());

                        cmbWarlord.Items.Clear();
                        cmbWarlord.Items.AddRange(repo.GetWarlordTraits("TZEENTCH").ToArray());

                        cmbRelic.Items.Clear();
                        Keywords.Add("TZEENTCH");
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                        cmbRelic.SelectedIndex = 0;
                        Keywords.Remove("TZEENTCH");

                        cbStratagem1.Text = "Stratagem: Relics of the Impossible Fortress";
                    }
                    else if (Factionupgrade == "Nurgle")
                    {
                        clbPsyker.Items.Clear();
                        clbPsyker.Items.AddRange(repo.GetPsykerPowers("NURGLE").ToArray());

                        cmbWarlord.Items.Clear();
                        cmbWarlord.Items.AddRange(repo.GetWarlordTraits("NURGLE").ToArray());

                        cmbRelic.Items.Clear();
                        Keywords.Add("NURGLE");
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                        cmbRelic.SelectedIndex = 0;
                        Keywords.Remove("NURGLE");

                        cbStratagem1.Text = "Stratagem: Relics of the Great Garden";
                    }
                    else if (Factionupgrade == "Slaanesh")
                    {
                        clbPsyker.Items.Clear();
                        clbPsyker.Items.AddRange(repo.GetPsykerPowers("SLAANESH").ToArray());

                        cmbWarlord.Items.Clear();
                        cmbWarlord.Items.AddRange(repo.GetWarlordTraits("SLAANESH").ToArray());

                        cmbRelic.Items.Clear();
                        Keywords.Add("SLAANESH");
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                        cmbRelic.SelectedIndex = 0;
                        Keywords.Remove("SLAANESH");

                        cbStratagem1.Text = "Stratagem: Exquisite Gifts";
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
                    break;
                case 60:
                    if (clbPsyker.CheckedItems.Count < 1)
                    {
                        break;
                    }
                    else if (clbPsyker.CheckedItems.Count == 1)
                    {
                        PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                    }
                    else
                    {
                        clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
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
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[0] != "Malefic Talons")
            {
                Points += 10;
            }

            if (Weapons[1] != "")
            {
                Points += 35;
            }
        }

        public override string ToString()
        {
            return "Daemon Prince of Chaos - " + Points + "pts";
        }
    }
}
