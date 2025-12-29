using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class ScoutSquad : Datasheets
    {
        int currentIndex;

        public ScoutSquad()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("");
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
                Weapons.Add("");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "SCOUT", "SMOKESCREEN", "SCOUT SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new ScoutSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[0] == "")
            {
                lbModelSelect.Items.Add("Scout Sergeant w/ " + Weapons[1] + " and " + Weapons[2]);
            }
            else
            {
                lbModelSelect.Items.Add("Scout Sergeant w/ " + Weapons[1] + ", " + Weapons[2] +
                    " and " + Weapons[0]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i * 2 ) + 2] != "")
                {
                    lbModelSelect.Items.Add("Scout w/ " + Weapons[(i * 2) + 1] + " and " + Weapons[(i * 2) + 2]);
                }
                else
                {
                    lbModelSelect.Items.Add("Scout w/ " + Weapons[(i * 2) + 1]);
                }
            }

            cbOption1.Text = "Camo Cloak";

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption2"].Location.X + 20, panel.Controls["cmbOption2"].Location.Y + 60);
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
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
                            if (Weapons[0] == "")
                            {
                                lbModelSelect.Items[0] = ("Scout Sergeant w/ " + Weapons[1] + " and " + Weapons[2]);
                            }
                            else
                            {
                                lbModelSelect.Items[0] = ("Scout Sergeant w/ " + Weapons[1] + ", " + Weapons[2] +
                                    " and " + Weapons[0]);
                            }
                        }
                        else
                        {
                            Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1]
                            + " and " + Weapons[(currentIndex * 2) + 2];

                            if (Weapons[(currentIndex * 2) + 2] != "")
                            {
                                lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1]
                                + " and " + Weapons[(currentIndex * 2) + 2];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1];
                            }
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
                    break;
                case 12:
                    if (Relic != "Frost Weapon" ||
                        (Relic == "Frost Weapon" && (cmbOption2.SelectedIndex == 3 || cmbOption2.SelectedIndex == 5 || cmbOption2.SelectedIndex == 8)))
                    {
                        Weapons[2] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[0] == "")
                        {
                            lbModelSelect.Items[0] = ("Scout Sergeant w/ " + Weapons[1] + " and " + Weapons[2]);
                        }
                        else
                        {
                            lbModelSelect.Items[0] = ("Scout Sergeant w/ " + Weapons[1] + ", " + Weapons[2] +
                                " and " + Weapons[0]);
                        }
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[2]);
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();

                    #region Codex Supplement: Ultramarines
                    if (chosenRelic == "Hellfury Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Sunwrath Pistol")
                    {
                        cmbOption1.SelectedIndex = 9;
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    else if (chosenRelic == "Dragonrage Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Drakeblade")
                    {
                        cmbOption2.SelectedIndex = 8;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard
                    else if (chosenRelic == "Silentus Pistol")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Korvidari Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    #endregion
                    #region Codex Supplement: Iron Hands
                    else if (chosenRelic == "Haywire Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Teeth of Mars")
                    {
                        cmbOption2.SelectedIndex = 0;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: White Scars
                    else if (chosenRelic == "Stormwrath Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    #endregion
                    #region Codex Supplement: Imperial Fists
                    else if (chosenRelic == "Gatebreaker Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Fist of Terra")
                    {
                        cmbOption2.SelectedIndex = 6;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Space Wolves
                    else if (chosenRelic == "Morkai's Teeth Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Frost Weapon")
                    {
                        cmbOption2.SelectedIndex = 8;
                        this.DrawItemWithRestrictions(new List<int> { 0, 1, 2, 4, 6, 7, 9 }, cmbOption2);
                    }
                    #endregion
                    #region Codex Supplement: Dark Angels
                    else if (chosenRelic == "Bolts of Judgement")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Atonement")
                    {
                        cmbOption1.SelectedIndex = 9;
                        cmbOption1.Enabled = false;
                    }
                    #endregion

                    if (chosenRelic != "Frost Weapon") //Space Wolves Sgt Relic
                    {
                        this.DrawItemWithRestrictions(new List<int>(), cmbOption2);
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                    Relic = chosenRelic;
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        if(currentIndex == 0)
                        {
                            Weapons[0] = cbOption1.Text;
                        }
                        else
                        {
                            Weapons[(currentIndex * 2) + 2] = cbOption1.Text;
                        }

                        if(currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Scout Sergeant w/ " + Weapons[1] + ", " + Weapons[2] +
                            " and " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1]
                            + " and " + Weapons[(currentIndex * 2) + 2];
                        }
                    }
                    else
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[0] = cbOption1.Text;
                        }
                        else
                        {
                            Weapons[(currentIndex * 2) + 2] = "";
                        }

                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Scout Sergeant w/ " + Weapons[1] + " and " + Weapons[2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1];
                        }
                    }
                    break;
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = (Weapons.Count - 1) / 2;

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Boltgun");
                            Weapons.Add("");
                            lbModelSelect.Items.Add("Scout w/ " + Weapons[(i * 2) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        for (int i = temp; i > UnitSize; i--)
                        {
                            lbModelSelect.Items.RemoveAt(i - 1);
                            Weapons.RemoveAt((i * 2));
                            Weapons.RemoveAt((i * 2) - 1);
                        }
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        antiLoop = false;
                        break;
                    }

                    if(currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbStratagem5.Visible = true;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cbOption1.Visible = false;
                        cbStratagem5.Visible = true;
                        cmbOption1.Enabled = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
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
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Bolt Pistol",
                            "Grav-pistol",
                            "Lightning Claw",
                            "Plasma Pistol",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword",
                            "Thunder Hammer"
                        });
                        if (repo.customSubFactionTraits[2] == "Blood Angels" || repo.customSubFactionTraits[2] == "Deathwatch")
                        {
                            cmbOption2.Items.Insert(3, "Hand Flamer");
                            cmbOption2.Items.Insert(4, "Inferno Pistol");
                        }
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[2]);

                        if (Relic == "Frost Weapon") //Space Wolves Sgt Relic
                        {
                            this.DrawItemWithRestrictions(new List<int> { 0, 1, 2, 4, 6, 7, 9 }, cmbOption2);
                        }
                        else
                        {
                            this.DrawItemWithRestrictions(new List<int>(), cmbOption2);
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Shotgun",
                            "Boltgun",
                            "Heavy Bolter",
                            "Missile Launcher",
                            "Scout Knife",
                            "Scout Sniper Rifle"
                        });
                        if(repo.currentSubFaction == "Space Wolves" || repo.customSubFactionTraits[2] == "Space Wolves")
                        {
                            cmbOption1.Items.Insert(1, "Bolt Pistol and Power Axe");
                            cmbOption1.Items.Insert(2, "Bolt Pistol and Power Sword");
                            cmbOption1.Items.Insert(4, "Boltgun and Plasma Pistol");
                            cmbOption1.Items.Insert(5, "Flamer");
                            cmbOption1.Items.Insert(6, "Grav-gun");
                            cmbOption1.Items.Insert(8, "Meltagun");
                            cmbOption1.Items.Insert(10, "Plasma Gun");
                        }

                        restrictedIndexes.Clear();

                        if (repo.currentSubFaction == "Space Wolves" || repo.customSubFactionTraits[2] == "Space Wolves")
                        {
                            if((Weapons.Contains("Bolt Pistol and Power Axe") || Weapons.Contains("Bolt Pistol and Power Sword")
                                || Weapons.Contains("Boltgun and Plasma Pistol"))
                                && 
                                !(Weapons[(currentIndex * 2) + 1] == "Bolt Pistol and Power Axe" || Weapons[(currentIndex * 2) + 1] == "Bolt Pistol and Power Sword"
                                || Weapons[(currentIndex * 2) + 1] == "Boltgun and Plasma Pistol"))
                            {
                                restrictedIndexes.AddRange(new int[] { 1, 2, 4 });
                            }

                            if(
                                (Weapons.Contains("Heavy Bolter") || Weapons.Contains("Grav-gun") || Weapons.Contains("Flamer")
                                || Weapons.Contains("Meltagun") || Weapons.Contains("Missile Launcher") || Weapons.Contains("Plasma Gun"))
                                &&
                                !(Weapons[(currentIndex * 2) + 1] == "Heavy Bolter" || Weapons[(currentIndex * 2) + 1] == "Grav-gun"
                                || Weapons[(currentIndex * 2) + 1] == "Flamer" || Weapons[(currentIndex * 2) + 1] == "Meltagun"
                                || Weapons[(currentIndex * 2) + 1] == "Missile Launcher" || Weapons[(currentIndex * 2) + 1] == "Plasma Gun")
                            )
                            {
                                restrictedIndexes.AddRange(new int[] { 5, 6, 7, 8, 9, 10 });
                            }
                        }
                        else
                        {
                            if ((Weapons.Contains("Heavy Bolter") || Weapons.Contains("Missile Launcher"))
                                && !(Weapons[(currentIndex * 2) + 1] == "Heavy Bolter" || Weapons[(currentIndex * 2) + 1] == "Missile Launcher"))
                            {
                                restrictedIndexes.AddRange(new int[] { 2, 3 });
                            }
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }

                    if (Weapons[(currentIndex * 2) + 2] == "" || (currentIndex == 0 && Weapons[0] == ""))
                    {
                        cbOption1.Checked = false;
                    }
                    else
                    {
                        cbOption1.Checked = true;
                    }

                    antiLoop = false;
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
                default:break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Scout Squad - " + Points + "pts";
        }
    }
}