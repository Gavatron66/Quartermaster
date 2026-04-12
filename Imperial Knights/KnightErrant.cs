using Roster_Builder.Aeldari;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Imperial_Knights
{
    public class KnightErrant : Datasheets
    {
        string freebladeTradition = string.Empty;
        string stratWarlordTrait;
        string freebladeRelic = "(None)"; //For the Stratagem: Favoured Knight

        public KnightErrant()
        {
            DEFAULT_POINTS = 425;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m_c";
            Weapons.Add("Meltagun (+5 pts)");
            Weapons.Add("Reaper Chainsword");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "IMPERIAL KNIGHTS", "<QUESTOR ALLEGIANCE>", "<NOBLE HOUSEHOLD>",
                "VEHICLE", "TITANIC", "QUESTORIS-CLASS", "KNIGHT ERRANT"
            });
            Role = "Lord of War";
        }

        public override Datasheets CreateUnit()
        {
            return new KnightErrant();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ImperialKnights;
            Template.LoadTemplate(TemplateCode, panel);

            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;

            cbOption1.Location = new System.Drawing.Point(panel.Controls["lblOption3"].Location.X - 16, panel.Controls["lblOption3"].Location.Y + 32);
            panel.Controls["lblOption4"].Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 28);
            cmbOption4.Location = new System.Drawing.Point(panel.Controls["lblOption3"].Location.X + 4, cbOption1.Location.Y + 56);

            panel.Controls["lblFactionupgrade"].Visible = true;
            cmbFaction.Visible = true;
            cbOption1.Visible = true;
            panel.Controls["lblOption4"].Visible = true;
            cmbOption4.Visible = true;
            cmbOption4.Enabled = false;

            cbOption1.Text = "Make this Knight a Freeblade?";
            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(repo.GetCustomSubfactionList2().ToArray());

            restrictedIndexes.Clear();
            foreach (var tradition in (repo as ImperialKnights).freebladeTraditions)
            {
                restrictedIndexes.Add(cmbOption4.Items.IndexOf(tradition));
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption4);

            antiLoop = true;
            if (repo.currentSubFaction == "Freeblade Lance")
            {
                cbOption1.Checked = true;
                cbOption1.Enabled = false;

                if (freebladeTradition != string.Empty)
                {
                    cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(freebladeTradition);
                }

                cmbFaction.Enabled = false;
                cmbOption4.Enabled = true;
            }
            else if (freebladeTradition != string.Empty)
            {
                cbOption1.Checked = true;
                cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(freebladeTradition);
                cmbOption4.Enabled = true;
                cmbFaction.Enabled = false;
            }
            else
            {
                cbOption1.Checked = false;
                if ((repo as ImperialKnights).hasFreeblade)
                {
                    cbOption1.Enabled = false;
                }
            }
            antiLoop = false;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Stubber",
                "Meltagun (+5 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Reaper Chainsword",
                "Thunderstrike Gauntlet"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Ironstorm Missile Pod (+20 pts)",
                "Stormspear Rocket Pod (+40 pts)",
                "Twin Icarus Autocannon (+20 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

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

            if (Relic != null && cmbRelic.Items.Contains(Relic))
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = 0;
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

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox; // For Stratagem 3
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox; // For Stratagem 4

            cbStratagem3.Text = repo.StratagemList[2];
            cbStratagem3.Location = new System.Drawing.Point(cbStratagem2.Location.X, cbStratagem2.Location.Y + 32);
            cbStratagem3.Visible = true;

            panel.Controls["lblOption6"].Visible = false;
            panel.Controls["lblOption6"].Location = new System.Drawing.Point(cmbOption4.Location.X, panel.Controls["lblOption4"].Location.Y + 64);
            cmbOption6.Visible = false;
            cmbOption6.Location = new System.Drawing.Point(panel.Controls["lblOption6"].Location.X, panel.Controls["lblOption6"].Location.Y + 23);
            cmbOption6.Items.Clear();
            cmbOption6.Items.AddRange(repo.GetWarlordTraits("").ToArray());

            cbStratagem4.Text = repo.StratagemList[3];
            cbStratagem4.Location = new System.Drawing.Point(cbStratagem3.Location.X, cbStratagem3.Location.Y + 32);
            cbStratagem4.Visible = (repo.currentSubFaction == "Freeblade Lance" || repo.currentSubFaction == "House Raven") ? true : false;

            panel.Controls["lblOption5"].Visible = false;
            panel.Controls["lblOption5"].Location = new System.Drawing.Point(cmbOption4.Location.X, panel.Controls["lblOption6"].Location.Y + 64);
            cmbOption5.Visible = false;
            cmbOption5.Location = new System.Drawing.Point(panel.Controls["lblOption5"].Location.X, panel.Controls["lblOption5"].Location.Y + 23);
            cmbOption5.Items.Clear();
            cmbOption5.Items.AddRange(repo.GetRelics(Keywords).ToArray());
            cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(freebladeRelic);

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

                if (repo.currentSubFaction == "Freeblade Lance")
                {
                    cmbOption5.Visible = true;
                    panel.Controls["lblOption5"].Visible = true;
                }
            }
            else
            {
                cbStratagem4.Checked = false;
                cbStratagem4.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem4.Text));

                if (repo.currentSubFaction == "Freeblade Lance")
                {
                    cmbOption5.Visible = false;
                    panel.Controls["lblOption5"].Visible = false;
                    stratWarlordTrait = "";
                }
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
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;

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
                    if (cmbOption4.SelectedIndex != -1)
                    {
                        if (!restrictedIndexes.Contains(cmbOption4.SelectedIndex))
                        {
                            freebladeTradition = cmbOption4.SelectedItem.ToString();

                            if (repo.currentSubFaction == "Freeblade Lance")
                            {
                                (repo as ImperialKnights).freebladeTraditions.Add(freebladeTradition);
                            }
                        }
                        else
                        {
                            if (freebladeTradition == string.Empty)
                            {
                                cmbOption4.SelectedIndex = -1;
                            }
                            else
                            {
                                cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(freebladeTradition);
                            }
                        }
                    }
                    else
                    {
                        freebladeTradition = string.Empty;
                    }
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
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    cmbOption3.Enabled = true;

                    if(chosenRelic == "Ravager" || chosenRelic == "Honour's Bite" || chosenRelic == "Bringer of Justice")
                    {
                        cmbOption2.SelectedIndex = 0;
                        cmbOption2.Enabled = false;
                    }
                    else if(chosenRelic == "The Paragon Gauntlet")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if(chosenRelic == "Judgement")
                    {
                        cmbOption3.SelectedIndex = 2;
                        cmbOption3.Enabled = false;
                    }
                    else if(chosenRelic == "Fury of Mars")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }

                    Relic = chosenRelic;
                    break;
                case 18:
                    freebladeRelic = cmbOption5.SelectedItem as string;
                    break;
                case 19:
                    stratWarlordTrait = cmbOption6.SelectedItem as string;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        cmbOption4.Enabled = true;
                        cmbFaction.Enabled = false;

                        if(repo.currentSubFaction != "Freeblade Lance")
                        {
                            (repo as ImperialKnights).hasFreeblade = true;
                        }

                        if (Factionupgrade != string.Empty)
                        {
                            Factionupgrade = string.Empty;
                            cmbFaction.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        cmbFaction.Enabled = true;
                        cmbFaction.SelectedIndex = 0;
                        cmbOption4.Enabled = false;
                        cmbOption4.SelectedIndex = -1;

                        if (repo.currentSubFaction != "Freeblade Lance")
                        {
                            (repo as ImperialKnights).hasFreeblade = false;
                        }
                        else
                        {
                            if (freebladeTradition != string.Empty)
                            {
                                (repo as ImperialKnights).freebladeTraditions.Remove(freebladeTradition);
                            }
                        }
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
                    if (cbStratagem3.Checked && !Stratagem.Contains(cbStratagem3.Text))
                    {
                        Stratagem.Add(cbStratagem3.Text);
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
                    if (cbStratagem4.Checked && !Stratagem.Contains(cbStratagem4.Text))
                    {
                        Stratagem.Add(cbStratagem4.Text);

                        if (repo.currentSubFaction == "Freeblade Lance")
                        {
                            cmbOption5.Visible = true;
                            panel.Controls["lblOption5"].Visible = true;
                        }
                    }
                    else if (cbStratagem4.Checked)
                    {
                        if (repo.currentSubFaction == "Freeblade Lance")
                        {
                            cmbOption5.Visible = true;
                            panel.Controls["lblOption5"].Visible = true;
                            cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(freebladeRelic);
                        }
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
                        }

                        if (repo.currentSubFaction == "Freeblade Lance")
                        {
                            cmbOption5.Visible = false;
                            panel.Controls["lblOption5"].Visible = false;
                            cmbOption5.SelectedIndex = 0;
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[0] == "Meltagun (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[2] != "(None)")
            {
                if (Weapons[2] == "Stormspear Rocket Pod (+40 pts)")
                {
                    Points += 40;
                }
                else
                {
                    Points += 20;
                }
            }
        }

        public override string ToString()
        {
            return "Knight Errant - " + Points + "pts";
        }
    }
}