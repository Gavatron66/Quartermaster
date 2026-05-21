using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Black_Templars
{
    public class BTCrusaders : Datasheets
    {
        int currentIndex = 0;
        List<string> Neophytes = new List<string>();
        const int neophytePts = 13;

        public BTCrusaders()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "crusaders";
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "BLACK TEMPLARS",
                "INFANTRY", "CORE", "CRUSADER SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new BTCrusaders();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudUnitSize2 = panel.Controls["nudUnitSize2"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";
            panel.Controls["lblOption6"].Text = "(+" + neophytePts + " pts/model)";
            panel.Controls["lblOption6"].Location = new System.Drawing.Point(panel.Controls["lblModelPoints"].Location.X, nudUnitSize2.Location.Y);
            panel.Controls["lblOption6"].Visible = true;
            panel.Controls["lblUnitSize2"].Text = "Number of Neophytes:";
            panel.Controls["lblExtra2"].Location = new System.Drawing.Point(panel.Controls["lblNumModels"].Location.X, panel.Controls["lblNumModels"].Location.Y);
            panel.Controls["lblExtra2"].Text = "Number of Initiates:";
            panel.Controls["lblExtra2"].Visible = true;
            panel.Controls["lblNumModels"].Visible = false;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            currentSize = UnitSize;
            nudUnitSize2.Minimum = 0;
            antiLoop = true;
            nudUnitSize2.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize2.Maximum = 10;
            nudUnitSize2.Value = Neophytes.Count;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Sword Brother w/ " + Weapons[0] + " and " + Weapons[1]);

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Initiate w/ " + Weapons[i + 1]);
            }

            for(int i = 0; i < Neophytes.Count; i++)
            {
                lbModelSelect.Items.Add("Neophyte w/ " + Neophytes[i]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[] 
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Grav-pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Plasma Pistol",
                "Thunder Hammer"
            });

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(cmbOption2.Location.X - 40, cmbOption2.Location.Y + 60);
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

            //I don't know why I need this, but otherwise there's a weird bug that occurs if I don't
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cbStratagem4.Text = repo.StratagemList[3];
            cbStratagem4.Location = new System.Drawing.Point(cmbRelic.Location.X, cmbRelic.Location.Y + 30);

            //Relic Bearers Code
            panel.Controls["lblExtra1"].Visible = true;
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(cbStratagem4.Location.X, cbStratagem4.Location.Y + 30);
            panel.Controls["lblExtra1"].Text = "Relic Bearers";

            cmbFaction.Visible = true;
            cmbFaction.Location = new System.Drawing.Point(cbStratagem4.Location.X + 4, cbStratagem4.Location.Y + 54);
            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(this.Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            cmbFaction.Visible = true;

            if (!Weapons.Contains("Power Fist") && !Weapons.Contains("Flamer"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 5, 6 }, cmbFaction);
            }
            else if (!Weapons.Contains("Power Fist"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 5 }, cmbFaction);
            }
            else if (!Weapons.Contains("Flamer"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 6 }, cmbFaction);
            }
            else
            {
                this.DrawItemWithRestrictions(new List<int>(), cmbFaction);
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

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudUnitSize2 = panel.Controls["nudUnitSize2"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Sword Brother w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
                        }
                        else if(currentIndex >= UnitSize) //Neophytes
                        {
                            Neophytes[currentIndex - UnitSize] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Neophyte w/ " + Neophytes[currentIndex - UnitSize];
                        }
                        else //Initiate
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Initiate w/ " + Weapons[currentIndex + 1];
                        }
                    }
                    else
                    {
                        if(currentIndex == 0)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        }
                    }

                    if (!Weapons.Contains("Flamer") && Factionupgrade.Contains("Beastpyre"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    if (!Weapons.Contains("Power Fist") && Factionupgrade.Contains("Fist of Balthus"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    break;
                case 12:
                    if (!restrictedIndexes.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Sword Brother w/ " + Weapons[currentIndex * 2] + " and " + Weapons[currentIndex + 1];
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    if (!Weapons.Contains("Flamer") && Factionupgrade.Contains("Beastpyre"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    if (!Weapons.Contains("Power Fist") && Factionupgrade.Contains("Fist of Balthus"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    break;
                case 16:
                    if (!Weapons.Contains("Power Fist") && cmbFaction.Text.Contains("Fist"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    else if (!Weapons.Contains("Flamer") && cmbFaction.Text.Contains("Beastpyre"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;

                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();

                    if (chosenRelic == "Witchseeker Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                    }
                    else if (chosenRelic == "Witchseeker Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Sword of Judgement (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 13;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Sword of Judgement (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 6;
                        cmbOption2.Enabled = false;
                    }

                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Boltgun");
                        lbModelSelect.Items.Insert(UnitSize - 1, "Initiate w/ " + Weapons[UnitSize]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize - 1, 1);
                    }
                    break;
                case 62:
                    int temp2 = Neophytes.Count;
                    int temp3 = int.Parse(nudUnitSize2.Value.ToString());

                    if (temp2 < temp3)
                    {
                        Neophytes.Add("Boltgun");
                        lbModelSelect.Items.Add("Neophyte w/ " + Neophytes.Last());
                    }

                    if (temp2 > temp3)
                    {
                        lbModelSelect.Items.RemoveAt(UnitSize + temp2 - 1);
                        Neophytes.Remove(Neophytes.Last());
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }
                    antiLoop = true;

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();

                    if (currentIndex == 0)
                    {
                        cbStratagem4.Visible = true;
                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }
                        else
                        {
                            cmbRelic.Visible = false;
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

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);

                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        if (Relic == "Witchseeker Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        }
                        else if (Relic == "Witchseeker Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Sword of Judgement (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 13;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Sword of Judgement (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 6;
                            cmbOption2.Enabled = false;
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else if (currentIndex >= UnitSize) //Neophytes
                    {
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        cbStratagem4.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Boltgun",
                            "Combat Knife"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Neophytes[currentIndex - UnitSize]);

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else //Initiates
                    {
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        cbStratagem4.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Boltgun",
                            "Flamer",
                            "Grav-cannon",
                            "Grav-gun",
                            "Heavy Bolter",
                            "Lascannon",
                            "Meltagun",
                            "Missile Launcher",
                            "Multi-melta",
                            "Plasma Cannon",
                            "Plasma Gun",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                        weaponsCheck();
                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }

                    antiLoop = false;
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

            Points = DEFAULT_POINTS * UnitSize;
            Points += neophytePts * Neophytes.Count;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (!Weapons.Contains("Power Fist") && !Weapons.Contains("Flamer"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 5, 6 }, cmbFaction);
            }
            else if (!Weapons.Contains("Power Fist"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 5 }, cmbFaction);
            }
            else if (!Weapons.Contains("Flamer"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 6 }, cmbFaction);
            }
            else
            {
                this.DrawItemWithRestrictions(new List<int>(), cmbFaction);
            }
        }

        public override string ToString()
        {
            return "Crusader Squad - " + Points + "pts";
        }

        private void weaponsCheck()
        {
            string[] heavyRestrictArray = new string[]
            {
                "Grav-cannon",
                "Heavy Bolter",
                "Lascannon",
                "Missile Launcher",
                "Multi-melta",
                "Plasma Cannon",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword"
            };

            string[] specialRestrictArray = new string[]
            {
                "Flamer",
                "Grav-gun",
                "Meltagun",
                "Plasma Gun"
            };

            if (UnitSize + Neophytes.Count < 10)
            {
                if((Weapons.Contains("Grav-cannon") || Weapons.Contains("Heavy Bolter") || Weapons.Contains("Lascannon")
                        || Weapons.Contains("Missile Launcher") || Weapons.Contains("Multi-melta") || Weapons.Contains("Plasma Cannon")
                        || Weapons.Contains("Flamer") || Weapons.Contains("Grav-gun") || Weapons.Contains("Meltagun")
                        || Weapons.Contains("Plasma Gun") || Weapons.Contains("Power Axe") || Weapons.Contains("Power Fist")
                        || Weapons.Contains("Power Maul") || Weapons.Contains("Power Sword"))
                    && (Weapons[currentIndex + 1] == "Boltgun" || Weapons[currentIndex + 1] == "Astartes Chainsword"))
                {
                    restrictedIndexes.AddRange(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
                }
            }
            else
            {
                if ((Weapons.Contains("Grav-cannon") || Weapons.Contains("Heavy Bolter") || Weapons.Contains("Lascannon")
                        || Weapons.Contains("Missile Launcher") || Weapons.Contains("Multi-melta") || Weapons.Contains("Plasma Cannon")
                        || Weapons.Contains("Power Axe") || Weapons.Contains("Power Fist") || Weapons.Contains("Power Maul") 
                        || Weapons.Contains("Power Sword"))
                    && (Weapons[currentIndex + 1] == "Boltgun" || Weapons[currentIndex + 1] == "Astartes Chainsword")
                        || specialRestrictArray.Contains(Weapons[currentIndex + 1]))
                {
                    restrictedIndexes.AddRange(new int[] { 3, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15 });
                }

                if((Weapons.Contains("Flamer") || Weapons.Contains("Grav-gun") || Weapons.Contains("Meltagun") || Weapons.Contains("Plasma Gun"))
                    && (Weapons[currentIndex + 1] == "Boltgun" || Weapons[currentIndex + 1] == "Astartes Chainsword")
                        || heavyRestrictArray.Contains(Weapons[currentIndex + 1]))
                {
                    restrictedIndexes.AddRange(new int[] { 2, 4, 7, 11 });
                }
            }
        }
    }
}
