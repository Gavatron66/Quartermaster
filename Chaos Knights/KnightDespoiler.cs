using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Knights
{
    public class KnightDespoiler : Datasheets
    {
        string dreadbladeBond = string.Empty;
        string stratWarlordTrait;
        string extraBond = string.Empty;

        public KnightDespoiler()
        {
            DEFAULT_POINTS = 375;
            Points = DEFAULT_POINTS;
            TemplateCode = "4m_c";
            Weapons.Add("Daemonbreath Meltagun (+5 pts)");
            Weapons.Add("Reaper Chainsword");
            Weapons.Add("Warpstrike Claw");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "CHAOS KNIGHTS", "<QUESTOR TRAITORIS>", "<DREAD HOUSEHOLD>",
                "VEHICLE", "TITANIC", "ABHORRENT-CLASS", "KNIGHT DESPOILER"
            });
            PsykerPowers = new string[] { string.Empty };
            Role = "Lord of War";
        }

        public override Datasheets CreateUnit()
        {
            return new KnightDespoiler();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosKnights;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox; // For Stratagem 3
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;

            cbStratagem1.Location = new System.Drawing.Point(cbStratagem1.Location.X, cbStratagem1.Location.Y - 16);
            cbStratagem2.Location = new System.Drawing.Point(cbStratagem2.Location.X, cbStratagem2.Location.Y - 24);

            cbOption1.Location = new System.Drawing.Point(panel.Controls["lblOption4"].Location.X - 16, panel.Controls["lblOption4"].Location.Y + 32);
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 28);
            panel.Controls["lblExtra1"].Text = panel.Controls["lblOption4"].Text;
            cmbDiscipline.Location = new System.Drawing.Point(panel.Controls["lblOption4"].Location.X + 4, cbOption1.Location.Y + 56);

            cbStratagem3.Text = repo.StratagemList[2];
            cbStratagem3.Location = new System.Drawing.Point(cbStratagem2.Location.X, cbStratagem2.Location.Y + 24);
            cbStratagem3.Visible = true;

            panel.Controls["lblOption6"].Visible = true;
            panel.Controls["lblOption6"].Location = new System.Drawing.Point(panel.Controls["lblExtra1"].Location.X, panel.Controls["lblExtra1"].Location.Y + 64);
            cmbOption6.Visible = true;
            cmbOption6.Location = new System.Drawing.Point(cmbDiscipline.Location.X, panel.Controls["lblOption6"].Location.Y + 23);
            cmbOption6.Items.Clear();
            cmbOption6.Items.AddRange(repo.GetWarlordTraits("").ToArray());

            cbStratagem4.Text = repo.StratagemList[3];
            cbStratagem4.Location = new System.Drawing.Point(cbStratagem3.Location.X, cbStratagem3.Location.Y + 24);
            cbStratagem4.Visible = true;

            lblPsyker.Visible = true;
            clbPsyker.Visible = true;
            lblPsyker.Location = new System.Drawing.Point(panel.Controls["lblOption6"].Location.X, cmbOption6.Location.Y + 32);
            clbPsyker.Location = new System.Drawing.Point(cmbOption6.Location.X, panel.Controls["lblPsyker"].Location.Y + 23);

            panel.Controls["lblOption5"].Visible = true;
            cmbOption5.Visible = true;
            panel.Controls["lblOption5"].Location = new System.Drawing.Point(cbStratagem4.Location.X, cbStratagem4.Location.Y + 32);
            cmbOption5.Location = new System.Drawing.Point(cbStratagem4.Location.X, panel.Controls["lblOption5"].Location.Y + 23);

            panel.Controls["lblFactionupgrade"].Visible = true;
            cmbFaction.Visible = true;
            cbOption1.Visible = true;
            panel.Controls["lblExtra1"].Visible = true;
            cmbDiscipline.Visible = true;
            cmbDiscipline.Enabled = false;

            cbOption1.Text = "Make this Knight a Dreadblade?";
            cmbDiscipline.Items.Clear();
            cmbDiscipline.Items.AddRange(repo.GetCustomSubfactionList2().ToArray());

            cmbOption5.Items.Clear();
            cmbOption5.Items.AddRange(repo.GetCustomSubfactionList2().ToArray());

            antiLoop = true;
            if (dreadbladeBond != string.Empty)
            {
                cbOption1.Checked = true;
                cmbDiscipline.SelectedIndex = cmbDiscipline.Items.IndexOf(dreadbladeBond);
                cmbDiscipline.Enabled = true;

                if (cmbOption5.Items.Contains(extraBond))
                {
                    cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(extraBond);
                }
            }
            else
            {
                cbOption1.Checked = false;
                if ((repo as ChaosKnights).hasDreadblade)
                {
                    cbOption1.Enabled = false;
                }
            }
            antiLoop = false;

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

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Daemonbreath Meltagun (+5 pts)",
                "Diabolus Heavy Stubber"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Daemonbreath Thermal Cannon (+50 pts)",
                "Despoiler Gatling Cannon and Heavy Darkflamer (+60 pts)",
                "Despoiler Battle Cannon and Diabolus Heavy Stubber (+50 pts)",
                "Reaper Chainsword"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Daemonbreath Thermal Cannon (+50 pts)",
                "Despoiler Gatling Cannon and Heavy Darkflamer (+60 pts)",
                "Despoiler Battle Cannon and Diabolus Heavy Stubber (+50 pts)",
                "Warpstrike Claw"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "(None)",
                "Havoc Missle Pod (+20 pts)",
                "Ruinspear Rocket Pod (+40 pts)",
                "Twin Hellstorm Autocannon (+20 pts)"
            });
            cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);

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

            if (!cmbRelic.Items.Contains("Rune of Nak'T'Graa") && dreadbladeBond != string.Empty)
            {
                cmbRelic.Items.Insert(cmbRelic.Items.IndexOf("The Tyrant's Banner"), "Rune of Nak'T'Graa");
            }

            if (Factionupgrade == "Pyrothrone (+35 pts)" && !cmbRelic.Items.Contains("The Twisted Mask"))
            {
                cmbRelic.Items.Insert(cmbRelic.Items.IndexOf("Panoply of the Cursed Knights"), "The Twisted Mask");
            }

            if (Relic != null && cmbRelic.Items.Contains(Relic))
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = 0;
            }

            clbPsyker.Items.Clear();
            clbPsyker.Items.AddRange(repo.GetPsykerPowers("").ToArray());

            if (Factionupgrade == "Pyrothrone (+35 pts)" && Relic == "The Twisted Mask")
            {
                lblPsyker.Text = "Select two of the following:";
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
            else
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

            if (Stratagem.Contains(cbStratagem3.Text))
            {
                cbStratagem3.Checked = true;
                cbStratagem3.Enabled = true;
                cmbOption6.Visible = true;
                panel.Controls["lblOption6"].Visible = true;
                cmbOption6.SelectedIndex = cmbOption6.Items.IndexOf(stratWarlordTrait);
            }
            else
            {
                cbStratagem3.Checked = false;
                cbStratagem3.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem3.Text));
                cmbOption6.Visible = false;
                panel.Controls["lblOption6"].Visible = false;
                stratWarlordTrait = "";

                if (!isWarlord)
                {
                    cbStratagem3.Enabled = false;
                }
            }

            if (Stratagem.Contains(cbStratagem4.Text))
            {
                cbStratagem4.Checked = true;
                cbStratagem4.Enabled = true;
            }
            else
            {
                cbStratagem4.Checked = false;
                cbStratagem4.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem4.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem.ToString();
                    break;
                case 14:
                    Weapons[3] = cmbOption4.SelectedItem.ToString();
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

                    if (Factionupgrade == "Pyrothrone (+35 pts)")
                    {
                        lblPsyker.Visible = true;
                        clbPsyker.Visible = true;
                        cmbRelic.Items.Insert(cmbRelic.Items.IndexOf("Panoply of the Cursed Knights"), "The Twisted Mask");
                    }
                    else
                    {
                        lblPsyker.Visible = false;
                        clbPsyker.Visible = false;

                        for (int i = 0; i < PsykerPowers.Length; i++)
                        {
                            PsykerPowers[i] = string.Empty;
                        }

                        cmbRelic.Items.Remove("The Twisted Mask");
                    }

                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    cmbOption3.Enabled = true;
                    Relic = chosenRelic;

                    if(Relic == "The Teeth That Hungers")
                    {
                        cmbOption2.SelectedIndex = 3;
                        cmbOption2.Enabled = false;
                    }
                    else if(Relic == "The Gauntlet of Ascension")
                    {
                        cmbOption3.SelectedIndex = 3;
                        cmbOption3.Enabled = false;
                    }
                    else if (Relic == "Serpentstrike Core (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    else if (Relic == "Serpentstrike Core (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 0;
                        cmbOption2.Enabled = false;
                    }
                    else if (Relic == "Serpentstrike Core (Slot 3)")
                    {
                        cmbOption3.SelectedIndex = 0;
                        cmbOption3.Enabled = false;
                    }

                    if (Relic == "Rune of Nak'T'Graa")
                    {
                        panel.Controls["lblOption5"].Visible = true;
                        cmbOption5.Visible = true;
                    }
                    else
                    {
                        panel.Controls["lblOption5"].Visible = false;
                        cmbOption5.Visible = false;
                        extraBond = string.Empty;
                    }

                    if (Factionupgrade == "Pyrothrone (+35 pts)" && Relic == "The Twisted Mask")
                    {
                        panel.Controls["lblPsyker"].Text = "Select two of the following:";
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
                    else if (PsykerPowers.Length > 1)
                    {
                        panel.Controls["lblPsyker"].Text = "Select one of the following:";
                        if (PsykerPowers.Length != 1)
                        {
                            PsykerPowers = new string[1] { string.Empty };
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
                    }

                    break;
                case 18:
                    extraBond = cmbOption5.SelectedItem.ToString();
                    break;
                case 19:
                    stratWarlordTrait = cmbOption6.SelectedItem as string;
                    break;
                case 111:
                    if (cmbDiscipline.SelectedIndex < 0)
                    {
                        dreadbladeBond = string.Empty;
                    }
                    else
                    {
                        dreadbladeBond = cmbDiscipline.SelectedItem.ToString();
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        cmbDiscipline.Enabled = true;
                        (repo as ChaosKnights).hasDreadblade = true;
                        cmbRelic.Items.Insert(cmbRelic.Items.IndexOf("The Tyrant's Banner"), "Rune of Nak'T'Graa");
                    }
                    else
                    {
                        cmbDiscipline.Enabled = false;
                        cmbDiscipline.SelectedIndex = -1;

                        (repo as ChaosKnights).hasDreadblade = false;
                        cmbRelic.Items.Remove("Rune of Nak'T'Graa");
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                        cbStratagem3.Enabled = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; cbStratagem3.Enabled = false; cbStratagem3.Checked = false; }
                    break;
                case 60:
                    if (Factionupgrade == "Pyrothrone (+35 pts)" && Relic == "The Twisted Mask")
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
                case 73:
                    if (cbStratagem3.Checked)
                    {
                        if (!Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Add(cbStratagem3.Text);
                        }
                        cmbOption6.Visible = true;
                        panel.Controls["lblOption6"].Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Remove(cbStratagem3.Text);
                        }
                        cmbOption6.Visible = false;
                        panel.Controls["lblOption6"].Visible = false;
                        cmbOption6.SelectedIndex = -1;
                    }
                    break;
                case 74:
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[0] == "Daemonbreath Meltagun (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[1] == "Daemonbreath Thermal Cannon (+50 pts)"
                || Weapons[1] == "Despoiler Battle Cannon and Diabolus Heavy Stubber (+50 pts)")
            {
                Points += 50;
            }
            else if (Weapons[1] == "Despoiler Gatling Cannon and Heavy Darkflamer (+60 pts)")
            {
                Points += 60;
            }

            if (Weapons[2] == "Daemonbreath Thermal Cannon (+50 pts)"
                || Weapons[2] == "Despoiler Battle Cannon and Diabolus Heavy Stubber (+50 pts)")
            {
                Points += 50;
            }
            else if (Weapons[2] == "Despoiler Gatling Cannon and Heavy Darkflamer (+60 pts)")
            {
                Points += 60;
            }

            if (Weapons[3] == "Havoc Missle Pod (+20 pts)" || Weapons[3] == "Twin Hellstorm Autocannon (+20 pts)")
            {
                Points += 20;
            }
            else if (Weapons[3] == "Ruinspear Rocket Pod (+40 pts)")
            {
                Points += 40;
            }
        }

        public override string ToString()
        {
            return "Knight Despoiler - " + Points + "pts";
        }
    }
}