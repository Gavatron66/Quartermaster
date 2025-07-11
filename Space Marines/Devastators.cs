using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Devastators : Datasheets
    {
        int currentIndex;
        int weaponsCheck;

        string[] heavyArray = new string[]
        {
            "Grav-cannon",
            "Heavy Bolter",
            "Lascannon",
            "Missile Launcher",
            "Multi-melta (+10 pts)",
            "Plasma Cannon",
        };

        public Devastators()
        {
            DEFAULT_POINTS = 23;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("Bolt Pistol");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "DEVASTATOR SQUAD"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Devastators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
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
            lbModelSelect.Items.Add("Devastator Marine Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Devastator Marine w/ " + Weapons[i + 1]);
            }

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
            if (f.currentSubFaction == "Blood Angels" || f.currentSubFaction == "Deathwatch")
            {
                cmbOption1.Items.Insert(3, "Hand Flamer");
                cmbOption1.Items.Insert(4, "Inferno Pistol");
            }
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "May include an Armorium Cherub";

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["cbOption2"].Location.X, panel.Controls["cbOption2"].Location.Y + 60);
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
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[0] = "Devastator Marine Sergeant w/ " + Weapons[1] + " and " + Weapons[0];
                        }
                        else
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Devastator Marine w/ " + Weapons[currentIndex + 1];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }
                    break;
                case 12:
                    Weapons[0] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Devastator Marine Sergeant w/ " + Weapons[1] + " and " + Weapons[0];
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    antiLoop = true;
                    restrictedIndexes.Clear();

                    #region Codex Supplement: Ultramarines Strat Relics
                    if (chosenRelic == "Hellfury Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                    }
                    else if (chosenRelic == "Hellfury Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Sunwrath Pistol (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 9;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Sunwrath Pistol (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 4;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders Strat Relics
                    else if (chosenRelic == "Dragonrage Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                    }
                    else if (chosenRelic == "Dragonrage Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Drakeblade (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 13;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Drakeblade (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 8;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard Strat Relics
                    else if (chosenRelic == "Silentus Pistol")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Korvidari Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                    }
                    else if (chosenRelic == "Korvidari Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    Relic = chosenRelic;
                    antiLoop = false;
                    break;
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = Weapons.Count - 2;

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Boltgun");
                            lbModelSelect.Items.Add("Devastator Marine w/ " + Weapons[i]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        for (int i = temp; i > UnitSize; i--)
                        {
                            lbModelSelect.Items.RemoveAt(i - 1);
                            Weapons.RemoveAt(i - 1);
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
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    if (currentIndex == 0)
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Visible = true;
                        cbStratagem5.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[0]);

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
                        if (repo.customSubFactionTraits[2] == "Deathwatch" || repo.customSubFactionTraits[2] == "Blood Angels")
                        {
                            cmbOption1.Items.Insert(8, "Hand Flamer");
                            cmbOption1.Items.Insert(9, "Inferno Pistol");
                        }

                        #region Codex Supplement: Ultramarines Strat Relics
                        if (Relic == "Hellfury Bolts (Slot 1)")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        }
                        else if (Relic == "Hellfury Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Sunwrath Pistol (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 9;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Sunwrath Pistol (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 4;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Salamanders Strat Relics
                        else if (Relic == "Dragonrage Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        }
                        else if (Relic == "Dragonrage Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Drakeblade (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 13;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Drakeblade (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 8;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Raven Guard Strat Relics
                        else if (Relic == "Silentus Pistol")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Korvidari Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            restrictedIndexes.AddRange(new int[] { 0, 7, 8, 9, 10, 11, 12, 13, 15 });
                        }
                        else if (Relic == "Korvidari Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                    }
                    else
                    {
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.Visible = false;
                        cbStratagem5.Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Boltgun",
                            "Grav-cannon",
                            "Heavy Bolter",
                            "Lascannon",
                            "Missile Launcher",
                            "Multi-melta (+10 pts)",
                            "Plasma Cannon",
                        });
                        if (repo.customSubFactionTraits[2] == "Deathwatch" || repo.customSubFactionTraits[2] == "Blood Angels")
                        {
                            cmbOption1.Items.Insert(3, "Heavy Flamer");
                        }

                        if(weaponsCheck >= 4 && !heavyArray.Contains(Weapons[currentIndex + 1]))
                        {
                            cmbOption1.Enabled = false;
                        }
                        else
                        {
                            cmbOption1.Enabled = true;
                        }
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

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
            }

            Points = UnitSize * DEFAULT_POINTS;
            weaponsCheck = 0;
            foreach (string item in Weapons)
            {
                if (item == "Multi-melta (+10 pts)")
                {
                    Points += 10;
                }

                if(heavyArray.Contains(item))
                {
                    weaponsCheck++;
                }
            }
        }

        public override string ToString()
        {
            return "Devastator Squad - " + Points + "pts";
        }
    }
}
