using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class PrimarisLieutenant : Datasheets
    {
        private string stratWarlordTrait;

        public PrimarisLieutenant()
        {
            DEFAULT_POINTS = 65;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m_c";
            Weapons.Add("Master-crafted Bolt Rifle and Bolt Pistol");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CHARACTER", "PRIMARIS", "LIEUTENANT"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new PrimarisLieutenant();
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

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Master-crafted Auto Bolt Rifle and Bolt Pistol",
                "Master-crafted Auto Bolt Rifle, Bolt Pistol, and Master-crafted Power Sword",
                "Master-crafted Auto Bolt Rifle, Bolt Pistol, and Power Fist",
                "Master-crafted Auto Bolt Rifle and Heavy Bolt Pistol",
                "Master-crafted Auto Bolt Rifle, Heavy Bolt Pistol, and Master-crafted Power Sword",
                "Master-crafted Auto Bolt Rifle, Heavy Bolt Pistol, and Power Fist",
                "Master-crafted Bolt Rifle and Bolt Pistol",
                "Master-crafted Bolt Rifle, Bolt Pistol, and Master-crafted Power Sword",
                "Master-crafted Bolt Rifle, Bolt Pistol, and Power Fist",
                "Master-crafted Bolt Rifle and Heavy Bolt Pistol",
                "Master-crafted Bolt Rifle, Heavy Bolt Pistol, and Master-crafted Power Sword",
                "Master-crafted Bolt Rifle, Heavy Bolt Pistol, and Power Fist",
                "Master-crafted Power Sword and Bolt Pistol",
                "Master-crafted Power Sword and Heavy Bolt Pistol",
                "Master-crafted Power Sword, Neo-volkite Pistol and Storm Shield",
                "Master-crafted Stalker Bolt Rifle and Bolt Pistol",
                "Master-crafted Stalker Bolt Rifle, Bolt Pistol, and Master-crafted Power Sword",
                "Master-crafted Stalker Bolt Rifle, Bolt Pistol, and Power Fist",
                "Master-crafted Stalker Bolt Rifle and Heavy Bolt Pistol",
                "Master-crafted Stalker Bolt Rifle, Heavy Bolt Pistol, and Master-crafted Power Sword",
                "Master-crafted Stalker Bolt Rifle, Heavy Bolt Pistol, and Power Fist",
                "Plasma Pistol and Bolt Pistol",
                "Plasma Pistol, Bolt Pistol, and Master-crafted Power Sword",
                "Plasma Pistol, Bolt Pistol, and Power Fist",
                "Plasma Pistol and Heavy Bolt Pistol",
                "Plasma Pistol, Heavy Bolt Pistol, and Master-crafted Power Sword",
                "Plasma Pistol, Heavy Bolt Pistol, and Power Fist",
                "Power Fist and Bolt Pistol",
                "Power Fist and Heavy Bolt Pistol",
            });
            if (f.currentSubFaction == "Space Wolves")
            {
                cmbOption1.Items.Add("Special Issue Bolt Carbine, Master-crafted Power Axe, and Bolt Pistol");
                cmbOption1.Items.Add("Special Issue Bolt Carbine, Master-crafted Power Axe, and Heavy Bolt Pistol");
            }
            else if (f.currentSubFaction == "Black Templars")
            {
                cmbOption1.Items.Insert(0, "Heavy Bolt Pistol and Master-crafted Power Sword");
                cmbOption1.Items.Insert(0, "Heavy Bolt Pistol and Astartes Chainsword");
                cmbOption1.Items.Insert(0, "Combi-flamer, Heavy Bolt Pistol, and Master-crafted Power Axe");
                cmbOption1.Items.Insert(0, "Combi-flamer, Bolt Pistol, and Master-crafted Power Axe");
                cmbOption1.Items.Insert(0, "Auto-plasma and Master-crafted Power Sword");
                cmbOption1.Items.Insert(0, "Auto-plasma and Astartes Chainsword");
            }
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

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
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
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
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
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
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    restrictedIndexes.Clear();
                    for(int i = 0; i < cmbOption1.Items.Count; i++)
                    {
                        restrictedIndexes.Add(i);
                    }

                    #region Codex: Space Marines
                        if(chosenRelic == "The Shield Eternal")
                        {
                            if(repo.currentSubFaction == "Black Templars")
                            {
                                restrictedIndexes.Remove(20);
                                cmbOption1.SelectedIndex = 20;
                            }
                            else
                            {
                                restrictedIndexes.Remove(14);
                                cmbOption1.SelectedIndex = 14;
                            }
                        }
                        else if (chosenRelic == "The Teeth of Terra" && repo.currentSubFaction == "Black Templars")
                        {
                            restrictedIndexes.Remove(0);
                            restrictedIndexes.Remove(4);
                            cmbOption1.SelectedIndex = 0;
                        }
                        else if (chosenRelic == "Primarch's Wrath")
                        {
                            //Space Wolves only
                            restrictedIndexes.Remove(29);
                            restrictedIndexes.Remove(30);
                            cmbOption1.SelectedIndex = 29;
                        }
                        else if (chosenRelic == "The Burning Blade")
                        {
                            if (repo.currentSubFaction == "Black Templars")
                            {
                                restrictedIndexes.Remove(1);
                                restrictedIndexes.Remove(6);
                                restrictedIndexes.Remove(7);
                                restrictedIndexes.Remove(11);
                                restrictedIndexes.Remove(13);
                                restrictedIndexes.Remove(16);
                                restrictedIndexes.Remove(18);
                                restrictedIndexes.Remove(19);
                                restrictedIndexes.Remove(20);
                                restrictedIndexes.Remove(22);
                                restrictedIndexes.Remove(25);
                                restrictedIndexes.Remove(28);
                                restrictedIndexes.Remove(31);
                                cmbOption1.SelectedIndex = 1;
                            }
                            else
                            {
                                restrictedIndexes.Remove(1);
                                restrictedIndexes.Remove(4);
                                restrictedIndexes.Remove(7);
                                restrictedIndexes.Remove(10);
                                restrictedIndexes.Remove(12);
                                restrictedIndexes.Remove(13);
                                restrictedIndexes.Remove(14);
                                restrictedIndexes.Remove(16);
                                restrictedIndexes.Remove(19);
                                restrictedIndexes.Remove(22);
                                restrictedIndexes.Remove(25);
                                cmbOption1.SelectedIndex = 1;
                            }
                        }
                        else if (chosenRelic == "Purgatorus")
                        {
                            if(repo.currentSubFaction == "Black Templars")
                            {
                                restrictedIndexes.Clear();
                                restrictedIndexes.Add(20);
                                restrictedIndexes.Add(0);
                                restrictedIndexes.Add(1);
                                cmbOption1.SelectedIndex = 2;
                            }
                            else
                            {
                                restrictedIndexes.Clear();
                                restrictedIndexes.Add(14);
                                cmbOption1.SelectedIndex = 0;
                            }
                        }
                        else if (chosenRelic == "Bellicos Bolt Rifle")
                        {
                            if(repo.currentSubFaction == "Black Templars")
                            {
                                restrictedIndexes.RemoveRange(6, 6);
                                cmbOption1.SelectedIndex = 6;
                            }
                            else
                            {
                                restrictedIndexes.RemoveRange(0,6);
                                cmbOption1.SelectedIndex = 0;
                            }
                        }
                        else if (chosenRelic == "Lament")
                        {
                            if (repo.currentSubFaction == "Black Templars")
                            {
                                restrictedIndexes.RemoveRange(21, 6);
                                cmbOption1.SelectedIndex = 21;
                            }
                            else
                            {
                                restrictedIndexes.RemoveRange(15, 6);
                                cmbOption1.SelectedIndex = 15;
                            }
                        }
                    #endregion
                    #region Codex Supplement: Ultramarines
                    else if(chosenRelic == "Soldier's Blade")
                    {
                        restrictedIndexes.Remove(1);
                        restrictedIndexes.Remove(4);
                        restrictedIndexes.Remove(7);
                        restrictedIndexes.Remove(10);
                        restrictedIndexes.Remove(12);
                        restrictedIndexes.Remove(13);
                        restrictedIndexes.Remove(14);
                        restrictedIndexes.Remove(16);
                        restrictedIndexes.Remove(19);
                        restrictedIndexes.Remove(22);
                        restrictedIndexes.Remove(25);
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Hellfury Bolts")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    else if(chosenRelic == "Sunwrath Pistol")
                    {
                        restrictedIndexes.RemoveRange(21, 6);
                        cmbOption1.SelectedIndex = 21;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    else if (chosenRelic == "Dragonrage Bolts")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    else if (chosenRelic == "Drakeblade")
                    {
                        restrictedIndexes.Remove(1);
                        restrictedIndexes.Remove(4);
                        restrictedIndexes.Remove(7);
                        restrictedIndexes.Remove(10);
                        restrictedIndexes.Remove(12);
                        restrictedIndexes.Remove(13);
                        restrictedIndexes.Remove(14);
                        restrictedIndexes.Remove(16);
                        restrictedIndexes.Remove(19);
                        restrictedIndexes.Remove(22);
                        restrictedIndexes.Remove(25);
                        cmbOption1.SelectedIndex = 1;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard
                    else if (chosenRelic == "Ex Tenebris")
                    {
                        restrictedIndexes.RemoveRange(15, 6);
                        cmbOption1.SelectedIndex = 15;
                    }
                    else if (chosenRelic == "Korvidari Bolts" || chosenRelic == "Silentus Pistol")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    #endregion
                    #region Codex Supplement: Iron Hands
                    else if (chosenRelic == "Haywire Bolts")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    #endregion
                    #region Codex Supplement: White Scars
                    else if (chosenRelic == "Stormwrath Bolts" || chosenRelic == "Equis-pattern Bolt Pistol")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    else if (chosenRelic == "Scimitar of the Great Khan")
                    {
                        restrictedIndexes.Remove(1);
                        restrictedIndexes.Remove(4);
                        restrictedIndexes.Remove(7);
                        restrictedIndexes.Remove(10);
                        restrictedIndexes.Remove(12);
                        restrictedIndexes.Remove(13);
                        restrictedIndexes.Remove(14);
                        restrictedIndexes.Remove(16);
                        restrictedIndexes.Remove(19);
                        restrictedIndexes.Remove(22);
                        restrictedIndexes.Remove(25);
                        cmbOption1.SelectedIndex = 1;
                    }
                    #endregion
                    #region Codex Supplement: Imperial Fists
                    else if (chosenRelic == "Gatebreaker Bolts" || chosenRelic == "The Spartean")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    else if(chosenRelic == "Duty's Burden")
                    {
                        restrictedIndexes.RemoveRange(15, 6);
                        restrictedIndexes.RemoveRange(0, 6);
                        cmbOption1.SelectedIndex = 0;
                    }
                    else if (chosenRelic == "Fist of Terra" || chosenRelic == "Fist of Vengeance" /* Crimson Fists only*/ )
                    {
                        restrictedIndexes.Remove(2);
                        restrictedIndexes.Remove(5);
                        restrictedIndexes.Remove(8);
                        restrictedIndexes.Remove(11);
                        restrictedIndexes.Remove(17);
                        restrictedIndexes.Remove(20);
                        restrictedIndexes.Remove(23);
                        restrictedIndexes.Remove(26);
                        restrictedIndexes.Remove(27);
                        restrictedIndexes.Remove(28);
                        cmbOption1.SelectedIndex = 2;
                    }
                    #endregion
                    #region Codex Supplement: Deathwatch
                    else if (chosenRelic == "Dominus Aegis")
                    {
                        restrictedIndexes.Remove(14);
                        cmbOption1.SelectedIndex = 14;
                    }
                    else if (chosenRelic == "The Thief of Secrets")
                    {
                        restrictedIndexes.Remove(1);
                        restrictedIndexes.Remove(4);
                        restrictedIndexes.Remove(7);
                        restrictedIndexes.Remove(10);
                        restrictedIndexes.Remove(12);
                        restrictedIndexes.Remove(13);
                        restrictedIndexes.Remove(14);
                        restrictedIndexes.Remove(16);
                        restrictedIndexes.Remove(19);
                        restrictedIndexes.Remove(22);
                        restrictedIndexes.Remove(25);
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Banebolts of Eryxia" || chosenRelic == "Artificer Bolt Cache")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    #endregion
                    #region Codex Supplement: Space Wolves
                    else if (chosenRelic == "Fireheart")
                    {
                        restrictedIndexes.Remove(21);
                        restrictedIndexes.Remove(22);
                        restrictedIndexes.Remove(23);
                        restrictedIndexes.Remove(24);
                        restrictedIndexes.Remove(25);
                        restrictedIndexes.Remove(26);
                        cmbOption1.SelectedIndex = 21;
                    }
                    else if (chosenRelic == "Black Death")
                    {
                        restrictedIndexes.Remove(29);
                        restrictedIndexes.Remove(30);
                        cmbOption1.SelectedIndex = 29;
                    }
                    else if (chosenRelic == "Morkai's Teeth Bolts")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    else if (chosenRelic == "Frost Weapon")
                    {
                        restrictedIndexes.Remove(1);
                        restrictedIndexes.Remove(4);
                        restrictedIndexes.Remove(7);
                        restrictedIndexes.Remove(10);
                        restrictedIndexes.Remove(12);
                        restrictedIndexes.Remove(13);
                        restrictedIndexes.Remove(14);
                        restrictedIndexes.Remove(16);
                        restrictedIndexes.Remove(19);
                        restrictedIndexes.Remove(22);
                        restrictedIndexes.Remove(25);
                        restrictedIndexes.Remove(29);
                        restrictedIndexes.Remove(30);
                        cmbOption1.SelectedIndex = 1;
                    }
                    #endregion
                    #region Codex Supplement: Dark Angels
                    else if (chosenRelic == "Heavenfall Blade")
                    {
                        restrictedIndexes.Remove(1);
                        restrictedIndexes.Remove(4);
                        restrictedIndexes.Remove(7);
                        restrictedIndexes.Remove(10);
                        restrictedIndexes.Remove(12);
                        restrictedIndexes.Remove(13);
                        restrictedIndexes.Remove(14);
                        restrictedIndexes.Remove(16);
                        restrictedIndexes.Remove(19);
                        restrictedIndexes.Remove(22);
                        restrictedIndexes.Remove(25);
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Atonement")
                    {
                        restrictedIndexes.RemoveRange(21, 6);
                        cmbOption1.SelectedIndex = 21;
                    }
                    else if (chosenRelic == "Bolts of Judgement")
                    {
                        restrictedIndexes.Clear();
                        restrictedIndexes.Add(14);
                        cmbOption1.SelectedIndex = 0;
                    }
                    #endregion
                    else
                    {
                        restrictedIndexes.Clear();
                    }

                    Relic = chosenRelic;
                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
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
        }

        public override string ToString()
        {
            return "Primaris Lieutenant - " + Points + "pts";
        }
    }
}
