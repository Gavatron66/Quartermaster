using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class BikeSquad : Datasheets
    {
        int currentIndex = 0;
        bool isLoading = false;
        int special;
        int attackIndex = -1;

        public BikeSquad()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m1k";
            Weapons.Add(""); // Attack Bike Squad?
            Weapons.Add(""); // Attack Bike Weapon
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Pistol");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "BIKER", "CORE", "BIKE SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new BikeSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            if(repo.currentSubFaction == "Space Wolves")
            {
                nudUnitSize.Maximum = 16;
            }
            else
            {
                nudUnitSize.Maximum = 8;
            }

            if(repo.currentSubFaction != "Space Wolves" && currentSize > 8)
            {
                nudUnitSize.Value = 8;
            }
            else
            {
                nudUnitSize.Value = currentSize;
            }

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Biker Sergeant w/ " + Weapons[2]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Space Marine Biker w/ " + Weapons[i + 2]);
            }

            cbOption1.Text = "Include an Attack Bike (+50 pts)";
            cbOption1.Location = new System.Drawing.Point(243, 59);
            if (Weapons[0] == "")
            {
                cbOption1.Checked = false;
            }
            else
            {
                cbOption1.Checked = true;
            }
            cbOption1.Visible = true;

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, panel.Controls["cmbOption1"].Location.Y + 60);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            if (Stratagem.Contains(cbStratagem5.Text))
            {
                cbStratagem5.Checked = true;
                cbStratagem5.Enabled = true;

                panel.Controls["lblRelic"].Visible = true;
                cmbRelic.Visible = true;

                if (Relic == "(None)")
                {
                    cmbRelic.SelectedIndex = 0;
                }
                else
                {
                    if (Relic != null && cmbRelic.Items.Contains(Relic))
                    {
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                    }
                    else
                    {
                        cmbRelic.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                cbStratagem5.Checked = false;
                cmbRelic.SelectedIndex = 0;
            }

            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(isLoading) { return; }
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    isLoading = true;
                    
                    if (currentIndex == attackIndex)
                    {
                        Weapons[1] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[attackIndex] = "Attack Bike w/ " + Weapons[1];
                    }
                    else
                    {
                        if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                        {
                            if (currentIndex == 0)
                            {
                                Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                                lbModelSelect.Items[currentIndex] = "Biker Sergeant w/ " + Weapons[currentIndex + 2];
                            }
                            else
                            {
                                Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                                lbModelSelect.Items[currentIndex] = "Space Marine Biker w/ " + Weapons[currentIndex + 2];
                            }
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);
                        }
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    cmbOption1.Enabled = true;
                    restrictedIndexes.Clear();

                    #region Codex Supplement: Ultramarines
                    if (chosenRelic == "Hellfury Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Sunwrath Pistol")
                    {
                        cmbOption1.SelectedIndex = 8;
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    else if (chosenRelic == "Dragonrage Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Drakeblade")
                    {
                        cmbOption1.SelectedIndex = 12;
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard
                    else if (chosenRelic == "Silentus Pistol")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Korvidari Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    #endregion
                    #region Codex Supplement: Iron Hands
                    else if (chosenRelic == "Haywire Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Teeth of Mars")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: White Scars
                    else if (chosenRelic == "Stormwrath Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    #endregion
                    #region Codex Supplement: Imperial Fists
                    else if (chosenRelic == "Gatebreaker Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Fist of Terra")
                    {
                        cmbOption1.SelectedIndex = 10;
                        cmbOption1.Enabled = false;
                    }
                    #endregion

                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = "Attack Bike";
                        if (Weapons[1] == "")
                        {
                            Weapons[1] = ("Heavy Bolter");
                        }
                        attackIndex = lbModelSelect.Items.Count;
                        lbModelSelect.Items.Add("Attack Bike w/ " + Weapons[1]);
                    }
                    else
                    {
                        Weapons[0] = "";
                        lbModelSelect.Items.Remove("Attack Bike w/ " + Weapons[1]);
                        Weapons[1] = "";
                    }
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Bolt Pistol");
                            lbModelSelect.Items.Add("Space Marine Biker w/ " + Weapons[temp + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(temp + 1, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbOption1.Enabled = true;

                        restrictedIndexes.Clear();

                        if (currentIndex == 0)
                        {
                            cbStratagem5.Visible = true;

                            if (Stratagem.Contains(cbStratagem5.Text))
                            {
                                panel.Controls["lblRelic"].Visible = true;
                                cmbRelic.Visible = true;
                            }

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
                                //Hand Flamer
                                //Inferno Pistol
                                "Lightning Claw",
                                "Plasma Pistol",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Bolter",
                                "Thunder Hammer"
                            });
                            if (repo.customSubFactionTraits[2] == "Deathwatch" || repo.customSubFactionTraits[2] == "Blood Angels")
                            {
                                cmbOption1.Items.Insert(7, "Hand Flamer");
                                cmbOption1.Items.Insert(8, "Inferno Pistol");
                            }
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[2]);

                            #region Codex Supplement: Ultramarines
                            if (Relic == "Hellfury Bolts")
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                                cmbOption1.SelectedIndex = 1;
                            }
                            else if (Relic == "Sunwrath Pistol")
                            {
                                cmbOption1.SelectedIndex = 8;
                                cmbOption1.Enabled = false;
                            }
                            #endregion
                            #region Codex Supplement: Salamanders
                            else if (Relic == "Dragonrage Bolts")
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                                cmbOption1.SelectedIndex = 1;
                            }
                            else if (Relic == "Drakeblade")
                            {
                                cmbOption1.SelectedIndex = 12;
                                cmbOption1.Enabled = false;
                            }
                            #endregion
                            #region Codex Supplement: Raven Guard
                            else if (Relic == "Silentus Pistol")
                            {
                                cmbOption1.SelectedIndex = 1;
                                cmbOption1.Enabled = false;
                            }
                            else if (Relic == "Korvidari Bolts")
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                                cmbOption1.SelectedIndex = 1;
                            }
                            #endregion
                            #region Codex Supplement: Iron Hands
                            else if (Relic == "Haywire Bolts")
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                                cmbOption1.SelectedIndex = 1;
                            }
                            else if (Relic == "Teeth of Mars")
                            {
                                cmbOption1.SelectedIndex = 0;
                                cmbOption1.Enabled = false;
                            }
                            #endregion
                            #region Codex Supplement: White Scars
                            else if (Relic == "Stormwrath Bolts")
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                                cmbOption1.SelectedIndex = 1;
                            }
                            #endregion
                            #region Codex Supplement: Imperial Fists
                            else if (Relic == "Gatebreaker Bolts")
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                                cmbOption1.SelectedIndex = 1;
                            }
                            else if (Relic == "Fist of Terra")
                            {
                                cmbOption1.SelectedIndex = 10;
                                cmbOption1.Enabled = false;
                            }
                            #endregion
                        }
                        else if (currentIndex == attackIndex)
                        {
                            cbStratagem5.Visible = false;
                            cmbRelic.Visible = false;
                            panel.Controls["lblRelic"].Visible = false;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Heavy Bolter",
                                "Multi-melta (+10 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);
                        }
                        else
                        {
                            cbStratagem5.Visible = false;
                            cmbRelic.Visible = false;
                            panel.Controls["lblRelic"].Visible = false;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Astartes Chainsword",
                                "Bolt Pistol",
                                "Flamer",
                                "Grav-gun",
                                "Meltagun",
                                "Plasma Gun"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);
                            if (repo.customSubFactionTraits[2] == "Space Wolves")
                            {
                                cmbOption1.Items.Add("Plasma Pistol");
                            }

                            if (special == 2 && (Weapons[currentIndex + 2] == "Bolt Pistol" || Weapons[currentIndex + 2] == "Astartes Chainsword"))
                            {
                                restrictedIndexes.AddRange(new int[] { 2, 3, 4, 5 });
                                if (repo.customSubFactionTraits[2] == "Space Wolves")
                                {
                                    restrictedIndexes.Add(6);
                                }
                            }
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    break;
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        Stratagem.Add(cbStratagem5.Text);
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            Stratagem.Remove(cbStratagem5.Text);
                        }
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.SelectedIndex = 0;
                    }
                    break;
            }

            isLoading = false;

            Points = DEFAULT_POINTS * UnitSize;
            special = 0;

            if(Weapons.Contains("Attack Bike"))
            {
                Points += 50;
            }

            if(Weapons.Contains("Multi-melta (+10 pts)"))
            {
                Points += 10;
            }

            for(int i = 3; i < Weapons.Count; i++)
            {
                if (Weapons[i] != "Bolt Pistol" && Weapons[i] != "Astartes Chainsword")
                {
                    special++;
                }
            }
        }

        public override string ToString()
        {
            return "Bike Squad - " + Points + "pts";
        }
    }
}
