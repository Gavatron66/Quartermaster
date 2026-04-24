using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class DaemonPrince : Datasheets
    {
        public DaemonPrince()
        {
            DEFAULT_POINTS = 120;
            Points = DEFAULT_POINTS + 15;
            TemplateCode = "1m1k_pc";
            Weapons.Add("Hellforged Sword (+10 pts)");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "CHARACTER", "MONSTER", "DAEMON", "DAEMON PRINCE", "MARK OF CHAOS",
                "PSYKER"
            });
            PsykerPowers = new string[1] { string.Empty };
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new DaemonPrince();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

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

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
            Factionupgrade = "Mark of Nurgle (+15 pts)";
            cmbFaction.Items.Remove("(None)");

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("DH");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            if (Relic != "Liber Hereticus" || Relic != "Malefic Tome")
            {
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
            }
            else
            {
                lblPsyker.Text = "Select two of the following:";
                if (PsykerPowers.Length != 2)
                {
                    PsykerPowers = new string[2] { string.Empty, string.Empty };
                }

                clbPsyker.ClearSelected();
                for (int i = 0; i < clbPsyker.Items.Count; i++)
                {
                    clbPsyker.SetItemChecked(i, false);
                }

                if (PsykerPowers[0] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
                }
                if (PsykerPowers[1] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[1]), true);
                }
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

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

                    if(Factionupgrade == "Mark of Khorne (+15 pts)")
                    {
                        PsykerPowers[0] = string.Empty;
                        if(PsykerPowers.Length > 1)
                        {
                            PsykerPowers[1] = string.Empty;
                        }
                        clbPsyker.Visible = false;
                        panel.Controls["lblPsyker"].Visible = false;
                    }
                    else if (PsykerPowers[0] == string.Empty)
                    {
                        clbPsyker.Visible = true;
                        panel.Controls["lblPsyker"].Visible = true;
                    }

                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
<<<<<<< Updated upstream
=======
                    cmbFaction.Enabled = true;
                    cmbOption1.Enabled = true;
                    cbOption1.Enabled = true;

                    if(chosenRelic == "Zaall, the Wrathful" || chosenRelic == "Talisman of Burning Blood")
                    {
                        cmbFaction.SelectedIndex = 0;
                        cmbFaction.Enabled = false;
                    }
                    else if(chosenRelic == "G'holl'ax, the Decayed" || chosenRelic == "Orb of Unlife")
                    {
                        cmbFaction.SelectedIndex = 2;
                        cmbFaction.Enabled = false;
                    }
                    else if (chosenRelic == "Q'o'ak, the Boundless" || chosenRelic == "Eye of Tzeentch")
                    {
                        cmbFaction.SelectedIndex = 1;
                        cmbFaction.Enabled = false;
                    }
                    else if (chosenRelic == "Thaa'ris and Rhi'ol, the Rapacious" || chosenRelic == "Intoxicating Elixir")
                    {
                        cmbFaction.SelectedIndex = 3;
                        cmbFaction.Enabled = false;
                    }
                    else if(chosenRelic == "Ashen Axe" || chosenRelic == "Axe of the Forgemaster")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Claw of the Stygian Count")
                    {
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }
                    else if(chosenRelic == "Talons of the Night Terror")
                    {
                        cbOption1.Checked = true;
                        cbOption1.Enabled = false;
                    }

                    if (chosenRelic == "Liber Hereticus" || chosenRelic == "Malefic Tome")
                    {
                        panel.Controls["lblPsyker"].Text = "Select two of the following:";
                        var temp = PsykerPowers;

                        PsykerPowers = new string[2] { string.Empty, string.Empty };
                        if (temp[0] != string.Empty)
                        {

                            for (int i = 0; i < clbPsyker.Items.Count; i++)
                            {
                                clbPsyker.SetItemChecked(i, false);
                            }

                            clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp[0]), true);
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        }
                    }
                    else
                    {
                        panel.Controls["lblPsyker"].Text = "Select one of the following:";
                        var temp = PsykerPowers;

                        PsykerPowers = new string[1] { string.Empty };
                        if (temp[0] != string.Empty)
                        {

                            for (int i = 0; i < clbPsyker.Items.Count; i++)
                            {
                                clbPsyker.SetItemChecked(i, false);
                            }

                            clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp[0]), true);
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        }
                    }

>>>>>>> Stashed changes
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
                    if (Relic == "Liber Hereticus" || Relic == "Malefic Tome")
                    {
                        if (clbPsyker.CheckedItems.Count < 2)
                        {
                            break;
                        }
                        else if (clbPsyker.CheckedItems.Count == 2)
                        {
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                            PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                        }
                        else
                        {
                            clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                        }
                    }
                    else
                    {
                        if (clbPsyker.CheckedItems.Count == 1)
                        {
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        }
                        else
                        {
                            clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                        }
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
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons.Contains("Wings (+35 pts)"))
            {
                Points += 35;
            }

            if(Weapons.Contains("Hellforged Sword (+10 pts)") || Weapons.Contains("Daemonic Axe (+10 pts)"))
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Heretic Astartes Daemon Prince - " + Points + "pts";
        }
    }
}
