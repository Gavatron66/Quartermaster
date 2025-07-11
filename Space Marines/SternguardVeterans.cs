using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class SternguardVeterans : Datasheets
    {
        int currentIndex;
        int heavySpec;

        public SternguardVeterans()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Bolt Pistol");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Special Issue Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "STERNGUARD VETERAN SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new SternguardVeterans();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Sternguard Veteran Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Sternguard Veteran w/ " + Weapons[i + 1]);
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
            });
            if (f.currentSubFaction == "Blood Angels" || f.currentSubFaction == "Deathwatch")
            {
                cmbOption1.Items.Insert(3, "Hand Flamer");
                cmbOption1.Items.Insert(4, "Inferno Pistol");
            }
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[0]);

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

            //I don't know why I need this, but otherwise there's a weird bug that occurs if I don't
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
                            lbModelSelect.Items[0] = "Sternguard Veteran Sergeant w/" + Weapons[0] + " and " + Weapons[1];
                        }
                        else
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Sternguard Veteran w/ " + Weapons[currentIndex + 1];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }
                    break;
                case 12:
                    Weapons[0] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Sternguard Veteran Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();

                    #region Codex Supplement: Ultramarines
                    if (chosenRelic == "Hellfury Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 9, 10, 11, 12, 13 });
                        cmbOption1.SelectedIndex = 8;
                    }
                    else if (chosenRelic == "Sunwrath Pistol")
                    {
                        cmbOption2.SelectedIndex = 4;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    else if (chosenRelic == "Dragonrage Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 9, 10, 11, 12, 13 });
                        cmbOption1.SelectedIndex = 8;
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
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 9, 10, 11, 12, 13 });
                        cmbOption1.SelectedIndex = 8;
                    }
                    #endregion

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                    Relic = chosenRelic;
                    break;
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = Weapons.Count - 2;

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Special Issue Boltgun");
                            lbModelSelect.Items.Add("Sternguard Veteran w/ " + Weapons[i]);
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
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    cmbOption2.Enabled = true;

                    if (currentIndex == 0)
                    {
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbStratagem5.Visible = true;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
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
                            "Bolt Pistol",
                            "Combi-flamer",
                            "Combi-grav",
                            "Combi-melta",
                            "Combi-plasma",
                            "Grav-pistol",
                            "Lightning Claw",
                            "Special Issue Boltgun",
                            "Plasma Pistol",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword",
                            "Storm Bolter",
                        });
                        if (repo.currentSubFaction == "Blood Angels")
                        {
                            cmbOption1.Items.Insert(7, "Hand Flamer");
                            cmbOption1.Items.Insert(8, "Inferno Pistol");
                        }
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                        #region Codex Supplement: Ultramarines
                        if (Relic == "Hellfury Bolts")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 6, 7, 9, 10, 11, 12, 13 });
                            cmbOption1.SelectedIndex = 8;
                        }
                        else if (Relic == "Sunwrath Pistol")
                        {
                            cmbOption2.SelectedIndex = 4;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Salamanders
                        else if (Relic == "Dragonrage Bolts")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 6, 7, 9, 10, 11, 12, 13 });
                            cmbOption1.SelectedIndex = 8;
                        }
                        else if (Relic == "Drakeblade")
                        {
                            cmbOption2.SelectedIndex = 8;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Raven Guard
                        else if (Relic == "Silentus Pistol")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Korvidari Bolts")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 6, 7, 9, 10, 11, 12, 13 });
                            cmbOption1.SelectedIndex = 8;
                        }
                        #endregion
                    }
                    else
                    {
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Combi-flamer",
                            "Combi-grav",
                            "Combi-melta",
                            "Combi-plasma",
                            "Flamer",
                            "Grav-cannon",
                            "Grav-gun",
                            "Heavy Bolter",
                            "Heavy Flamer",
                            "Lascannon",
                            "Meltagun",
                            "Missile Launcher",
                            "Multi-melta",
                            "Plasma Cannon",
                            "Plasma Gun",
                            "Special Issue Boltgun",
                            "Storm Bolter"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                        string[] heavySpecList = new string[]
                        {
                            "Flamer",
                            "Grav-cannon",
                            "Grav-gun",
                            "Heavy Bolter",
                            "Heavy Flamer",
                            "Lascannon",
                            "Meltagun",
                            "Missile Launcher",
                            "Multi-melta",
                            "Plasma Cannon",
                            "Plasma Gun"
                        };

                        heavySpec = 0;
                        restrictedIndexes.Clear();
                        foreach(var weapon in Weapons)
                        {
                            if(heavySpecList.Contains(weapon))
                            {
                                heavySpec++;
                            }
                        }

                        if(heavySpec == 2 && !heavySpecList.Contains(Weapons[currentIndex + 1]))
                        {
                            restrictedIndexes.AddRange(new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 });
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
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
        }

        public override string ToString()
        {
            return "Sternguard Veteran Squad - " + Points + "pts";
        }
    }
}
