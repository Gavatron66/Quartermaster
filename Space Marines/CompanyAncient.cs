using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class CompanyAncient : Datasheets
    {
        private string stratWarlordTrait;
        public CompanyAncient()
        {
            DEFAULT_POINTS = 65;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m_c";
            Weapons.Add("Bolt Pistol");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CHARACTER", "ANCIENT", "COMMAND SQUAD", "COMPANY ANCIENT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new CompanyAncient();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox; // For Stratagem 3

            panel.Controls["lblFactionupgrade"].Visible = true;
            cmbFactionupgrade.Visible = true;

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Boltgun",
                "Bolt Pistol",
                "Combi-flamer",
                "Combi-grav",
                "Combi-melta",
                "Combi-plasma",
                "Grav-pistol",
                "Lightning Claw",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Storm Bolter",
                "Thunder Hammer"
            });
            if (repo.customSubFactionTraits[2] == "Blood Angels" || repo.customSubFactionTraits[2] == "Deathwatch")
            {
                cmbOption1.Items.Insert(8, "Hand Flamer");
                cmbOption1.Items.Insert(9, "Inferno Pistol");
            }
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            cmbFactionupgrade.Items.Clear();
            cmbFactionupgrade.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFactionupgrade.SelectedIndex = cmbFactionupgrade.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFactionupgrade.SelectedIndex = 0;
                if (Factionupgrade != "(None)")
                {
                    cmbWarlord.Items.Add("Steadfast Example");
                    cmbRelic.Items.Add("Pennant of the Fallen");
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

            if (f.currentSubFaction != f.customSubFactionTraits[2] && f.customSubFactionTraits[2] != "Unknown")
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
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;

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
                    if (Factionupgrade != "(None)" && Factionupgrade != null)
                    {
                        cmbWarlord.Items.Add("Steadfast Example");
                        cmbRelic.Items.Add("Pennant of the Fallen");
                    }
                    else
                    {
                        if (Relic == "Pennant of the Fallen")
                        {
                            cmbRelic.SelectedIndex = 0;
                        }

                        if (WarlordTrait == "Steadfast Example")
                        {
                            cmbWarlord.SelectedIndex = -1;
                        }

                        cmbWarlord.Items.Remove("Steadfast Example");
                        cmbRelic.Items.Remove("Pennant of the Fallen");
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    #region Codex: Space Marines
                    if (chosenRelic == "Primarch's Wrath")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Boltgun");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Purgatorus")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Teeth of Terra")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Astartes Chainsword");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Burning Blade")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Sword");
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Ultramarines
                    else if (chosenRelic == "Soldier's Blade")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Sword");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Vengeance of Ultramar")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Storm Bolter");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Hellfury Bolts")
                    {
                        //See the end of SaveDatasheets
                        cmbOption1.Enabled = true;
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
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Thunder Hammer");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Wrath of Prometheus")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Boltgun");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Nocturne's Vengeance")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Combi-flamer");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Drakeblade")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Sword");
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard
                    else if (chosenRelic == "Silentus Pistol")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Iron Hands
                    else if (chosenRelic == "The Axe of Medusa")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Axe");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Betrayer's Bane")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Combi-melta");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Teeth of Mars")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Astartes Chainsword");
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: White Scars
                    else if (chosenRelic == "Scimitar of the Great Khan")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Sword");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Equis-pattern Bolt Pistol")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Imperial Fists
                    else if (chosenRelic == "The Spartean")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Fist of Vengeance") // Crimson Fists only
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Fist");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Fist of Terra")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Fist");
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Deathwatch
                    else if (chosenRelic == "The Thief of Secrets")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Sword");
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Space Wolves
                    else if (chosenRelic == "Fireheart")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Plasma Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Black Death")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Axe");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Frost Weapon")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 12, 14, 15 });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Axe");
                    }
                    #endregion
                    else
                    {
                        cmbOption1.Enabled = true;
                    }
                    Relic = chosenRelic;
                    break;
                case 19:
                    stratWarlordTrait = cmbOption6.SelectedItem as string;
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
            }

            Points = DEFAULT_POINTS;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            restrictedIndexes.Clear();
            if (Relic == "Frost Weapon")
            {
                restrictedIndexes.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 12, 14, 15 });
                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Power Axe");
            }

            #region Bolt Relics
            if (Relic == "Hellfury Bolts" || Relic == "Dragonrage Bolts" || Relic == "Korvidari Bolts"
                || Relic == "Haywire Bolts" || Relic == "Stormwrath Bolts" || Relic == "Gatebreaker Bolts"
                || Relic == "Morkai's Teeth Bolts")
            {
                restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                cmbOption1.SelectedIndex = 1;
            }
            else if (Relic == "Banebolts of Eryxia" || Relic == "Artificer Bolt Cache")
            {
                restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 14, 15, 17, 18 });
                cmbOption1.SelectedIndex = 1;
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
            #endregion
        }

        public override string ToString()
        {
            return "Company Ancient - " + Points + "pts";
        }
    }
}
