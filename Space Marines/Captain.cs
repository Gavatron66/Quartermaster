using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Captain : Datasheets
    {
        int stormShield;
        List<int> restrictedIndexes2 = new List<int>();
        private string stratWarlordTrait;

        public Captain()
        {
            DEFAULT_POINTS = 85;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m1k_c";
            Weapons.Add("Master-crafted Boltgun");
            Weapons.Add("Astartes Chainsword");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CHARACTER", "CAPTAIN"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Captain();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox; // For Stratagem 3

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Combi-flamer",
                "Combi-grav",
                "Combi-melta",
                "Combi-plasma",
                "Grav-pistol",
                "Lightning Claw",
                "Master-crafted Boltgun",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Storm Bolter",
                "Storm Shield",
                "Thunder Hammer (+10 pts)"
            });
            if (repo.customSubFactionTraits[2] == "Blood Angels" || repo.customSubFactionTraits[2] == "Deathwatch")
            {
                cmbOption1.Items.Insert(7, "Hand Flamer");
                cmbOption1.Items.Insert(8, "Inferno Pistol");
            }
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Lightning Claw",
                "Relic Blade",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Storm Shield",
                "Thunder Hammer (+10 pts)"
            });
            if (f.currentSubFaction == "Deathwatch")
            {
                cmbOption2.Items.Add("Xenophase Blade");
            }
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Jump Pack (+25 pts)";
            if (Weapons[2] == cbOption1.Text)
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

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
                if (Factionupgrade != "(None)")
                {
                    cmbWarlord.Items.Add("Master of the Codex");
                    cmbRelic.Items.Add("Angel Artifice");
                }
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

            if (Relic != null && cmbRelic.Items.Contains(Relic))
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = 0;
            }

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;

            cbStratagem3.Visible = true;
            cbStratagem3.Location = new System.Drawing.Point(cbStratagem2.Location.X, cbStratagem2.Location.Y + 32);
            cbStratagem3.Text = f.StratagemList[2];

            if (f.currentSubFaction == "<Custom>" && f.customSubFactionTraits[2] != "Unknown")
            {
                cbStratagem4.Visible = true;
            }
            else
            {
                cbStratagem4.Visible = false;
            }

            cbStratagem4.Location = new System.Drawing.Point(cbStratagem3.Location.X, cbStratagem3.Location.Y + 32);
            cbStratagem4.Text = f.StratagemList[3];

            panel.Controls["lblOption6"].Visible = false;
            panel.Controls["lblOption6"].Location = new System.Drawing.Point(panel.Controls["lblWarlord"].Location.X, cmbWarlord.Location.Y + 33);
            cmbOption6.Visible = false;
            cmbOption6.Location = new System.Drawing.Point(panel.Controls["lblOption6"].Location.X, panel.Controls["lblOption6"].Location.Y + 23);
            cmbOption6.Items.Clear();
            cmbOption6.Items.AddRange(repo.GetWarlordTraits("Strat").ToArray());

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
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[0] == "Storm Shield" && stormShield != 1)
                        {
                            stormShield = 0;
                        }
                        else if (stormShield != 1)
                        {
                            stormShield = -1;
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                    }
                    break;
                case 12:
                    if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[1] == "Storm Shield" && stormShield != 0)
                        {
                            stormShield = 1;
                        }
                        else if (stormShield != 0)
                        {
                            stormShield = -1;
                        }
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
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
                    if (Factionupgrade != "(None)" && Factionupgrade != null)
                    {
                        cmbWarlord.Items.Add("Master of the Codex");
                        cmbRelic.Items.Add("Angel Artifice");
                    }
                    else
                    {
                        if (Relic == "Angel Artifice")
                        {
                            cmbRelic.SelectedIndex = 0;
                        }

                        if (WarlordTrait == "Master of the Codex")
                        {
                            cmbWarlord.SelectedIndex = -1;
                        }

                        cmbWarlord.Items.Remove("Master of the Codex");
                        cmbRelic.Items.Remove("Angel Artifice");
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    cbOption1.Enabled = true;

                    #region Codex: Space Marines
                        if (chosenRelic == "Primarch's Wrath")
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Master-crafted Boltgun");
                            cmbOption1.Enabled = false;
                        }
                        else if (chosenRelic == "Purgatorus")
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                            cmbOption1.Enabled = false;
                        }
                        else if (chosenRelic == "The Teeth of Terra")
                        {
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Astartes Chainsword");
                            cmbOption2.Enabled = false;
                        }
                        else if (chosenRelic == "The Burning Blade")
                        {
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Sword");
                            cmbOption2.Enabled = false;
                        }
                        else if (chosenRelic == "The Shield Eternal")
                        {
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Storm Shield");
                            cmbOption2.Enabled = false;
                        }
                    #endregion
                    #region Codex Supplement: Ultramarines
                    else if (chosenRelic == "Soldier's Blade")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Sword");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Vengeance of Ultramar")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Storm Bolter");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Helfury Bolts")
                    {
                        //See the end of SaveDatasheets
                    }
                    else if (chosenRelic == "Sunwrath Pistol")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Plasma Pistol");
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    else if (chosenRelic == "Drake-smiter")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Thunder Hammer (+10 pts)");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Wrath of Prometheus")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Master-crafted Boltgun");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Nocturne's Vengeance")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Combi-flamer");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Helfury Bolts")
                    {
                        //See the end of SaveDatasheets
                    }
                    else if (chosenRelic == "Drakeblade")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Sword");
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard
                    else if (chosenRelic == "The Ebonclaws")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Lightning Claw");
                        cmbOption1.Enabled = false;
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Lightning Claw");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Raven's Fury")
                    {
                        cbOption1.Checked = true;
                        cbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Silentus Pistol")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Helfury Bolts")
                    {
                        //See the end of SaveDatasheets
                    }
                    #endregion
                    else if (chosenRelic == "The Axe of Medusa")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Axe");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Betrayer's Bane")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Combi-melta");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Teeth of Mars")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Astartes Chainsword");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Scimitar of the Great Khan")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Sword");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Equis-pattern Bolt Pistol")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Spartean")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Fist of Vengeance")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Fist");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Fist of Terra")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Fist");
                        cmbOption2.Enabled = false;
                    }
                    Relic = chosenRelic;
                    break;
                case 19:
                    stratWarlordTrait = cmbOption6.SelectedItem as string;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
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
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                        cmbRelic.Items.Clear();
                        Keywords.Add("Strat");
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        Keywords.Remove("Strat");
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
                        }

                        cmbRelic.Items.Clear();
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

                        if (cmbRelic.Items.Contains(Relic))
                        {
                            cmbRelic.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (string weapon in Weapons)
            {
                if (weapon == "Thunder Hammer (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Jump Pack (+25 pts)")
                {
                    Points += 25;
                }
            }

            restrictedIndexes.Clear();
            restrictedIndexes2.Clear();

            if (stormShield > -1)
            {
                if (stormShield == 0)
                {
                    restrictedIndexes2.Add(cmbOption2.Items.IndexOf("Storm Shield"));
                }
                else if (stormShield == 1)
                {
                    restrictedIndexes.Add(cmbOption1.Items.IndexOf("Storm Shield"));
                }
            }

            #region Bolt Relics
            if (Relic == "Hellfury Bolts" || Relic == "Dragonrage Bolts" || Relic == "Korvidari Bolts"
                || Relic == "Haywire Bolts" || Relic == "Stormwrath Bolts" || Relic == "Gatebreaker Bolts")
            {
                restrictedIndexes.AddRange(new int[] { 0, 6, 7, 9, 10, 11, 12, 13, 15, 16 });
                cmbOption1.SelectedIndex = 1;
            }
            #endregion

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
            this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
        }

        public override string ToString()
        {
            return "Captain - " + Points + "pts";
        }
    }
}