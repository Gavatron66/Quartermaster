using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class RelicTerminators : Datasheets
    {
        int currentIndex;
        int restrictionCount;
        int restrictionCount2;
        bool isLoading = false;

        public RelicTerminators()
        {
            DEFAULT_POINTS = 33;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Combi-bolter");
                Weapons.Add("Power Fist");
                Weapons.Add(""); //Grenade Harness
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "TERMINATOR", "RELIC TERMINATOR SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new RelicTerminators();
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
            if (Weapons[2] == "")
            {
                lbModelSelect.Items.Add("Relic Terminator Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            }
            else
            {
                lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + ", " + Weapons[1] + " and a " + Weapons[2];
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i * 3) + 2] == "")
                {
                    lbModelSelect.Items.Add("Relic Terminator w/ " + Weapons[(i * 3)] + " and " + Weapons[(i * 3) + 1]);
                }
                else
                {
                    lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3] + ", " + Weapons[(currentIndex * 3) + 1] + 
                        " and a " + Weapons[(currentIndex * 3) + 2];
                }
            }

            cbOption1.Text = "Grenade Harness";

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["cbOption1"].Location.X, panel.Controls["cbOption1"].Location.Y + 60);
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
                        antiLoop = true;
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        antiLoop = false;
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
            if(isLoading)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:

                    if(currentIndex == 0)
                    {
                        Weapons[currentIndex * 3] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[2] != "")
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                                " and a " + Weapons[2]; 
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                        {
                            Weapons[currentIndex * 3] = cmbOption1.SelectedItem.ToString();
                            if (Weapons[(currentIndex * 3) + 2] != "")
                            {
                                lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3]
                                    + ", " + Weapons[(currentIndex * 3) + 1] + " and a " + Weapons[(currentIndex * 3) + 2];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3] +
                                    " and " + Weapons[(currentIndex * 3) + 1];
                            }
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);
                        }
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 3) + 1] = cmbOption2.SelectedItem.ToString();

                    if (currentIndex == 0)
                    {
                        if (Weapons[2] != "")
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                                " and a " + Weapons[2];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 3) + 2] != "")
                        {
                            lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3]
                                + ", " + Weapons[(currentIndex * 3) + 1] + " and a " + Weapons[(currentIndex * 3) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3] +
                                " and " + Weapons[(currentIndex * 3) + 1];
                        }
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    #region Codex Supplement: Ultramarines
                    if (chosenRelic == "Hellfury Bolts")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    else if (chosenRelic == "Dragonrage Bolts")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Drakeblade")
                    {
                        cmbOption2.SelectedIndex = 3;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard
                    else if (chosenRelic == "Korvidari Bolts")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    #endregion

                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 3) + 2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 2] = "";
                    }

                    if (currentIndex == 0)
                    {
                        if (Weapons[2] != "")
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                                " and a " + Weapons[2];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 3) + 2] != "")
                        {
                            lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3]
                                + ", " + Weapons[(currentIndex * 3) + 1] + " and a " + Weapons[(currentIndex * 3) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3] +
                                " and " + Weapons[(currentIndex * 3) + 1];
                        }
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Combi-bolter");
                            Weapons.Add("Power Fist");
                            lbModelSelect.Items.Add("Relic Terminator w/ " + Weapons[(currentIndex * 3)]
                                + " and " + Weapons[(currentIndex * 3) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 3), 3);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if(currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        cbOption1.Enabled = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                    }

                    isLoading = true;

                    if(currentIndex == 0)
                    {
                        cbOption1.Visible = false;
                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Combi-bolter",
                            "Lightning Claw",
                            "Plasma Blaster",
                            "Volkite Charger"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Chainfist",
                            "Lightning Claw",
                            "Power Fist",
                            "Power Sword"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        if (Relic == "Hellfury Bolts" || Relic == "Dragonrage Bolts" || Relic == "Korvidari Bolts"
                            || Relic == "Haywire Bolts" || Relic == "Stormwrath Bolts" || Relic == "Gatebreaker Bolts")
                        {
                            cmbOption1.Enabled = false;
                        }
                    }
                    else
                    {
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cbOption1.Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Combi-bolter",
                            "Heavy Flamer",
                            "Lightning Claw",
                            "Reaper Autocannon"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Chainfist",
                            "Lightning Claw",
                            "Power Fist",
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 3) + 1]);

                        if (Weapons[(currentIndex * 3) + 2] != "")
                        {
                            cbOption1.Checked = true;
                        }
                        else
                        {
                            cbOption1.Checked = false;
                        }

                        restrictedIndexes.Clear();
                        if(restrictionCount == UnitSize / 5 && !(Weapons[currentIndex * 3] == "Heavy Flamer"
                            || Weapons[currentIndex * 3] == "Reaper Autocannon"))
                        {
                            restrictedIndexes.AddRange(new int[] { 1, 3 });
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                        if(restrictionCount2 == UnitSize / 5 && Weapons[(currentIndex * 3) + 2] != "Grenade Harness")
                        {
                            cbOption1.Enabled = false;
                        }
                        else
                        {
                            cbOption1.Enabled = true;
                        }
                    }

                    isLoading = false;
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

            restrictionCount = 0;
            restrictionCount2 = 0;

            //Heavy Flamer, Reaper Autocannon, Grenade Harness
            foreach (var weapon in Weapons)
            {
                if(weapon == "Heavy Flamer" || weapon == "Reaper Autocannon")
                {
                    restrictionCount++;
                }

                if(weapon == "Grenade Harness")
                {
                    restrictionCount2++;
                }
            }
        }

        public override string ToString()
        {
            return "Relic Terminator Squad - " + Points + "pts";
        }
    }
}
