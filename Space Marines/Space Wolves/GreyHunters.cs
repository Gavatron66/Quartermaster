using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
    public class GreyHunters : Datasheets
    {
        int currentIndex;
        int[] restrict = new int[2] { 0, 0 };
        List<int> restrictedIndexes2 = new List<int>();
        int wolfGuard = 0;
        // 0 = False, 1 = Pack Leader, 2 = Terminator Pack Leader
        int stratLeader = 0;
        // 0 = None selected, 1 = Bloow Claw Pack Leader, 2 = Wolf Guard
        int wolfStandardIndex = -1;

        public GreyHunters()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add(""); //Wolf Guard Options
            Weapons.Add("");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
                "INFANTRY", "CORE", "GREY HUNTERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new GreyHunters();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionUpgrade"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            panel.Controls["lblExtra1"].Visible = true;
            cmbFaction.Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;

            lbModelSelect.Items.Clear();

            if (Weapons[2] == "(None)")
            {
                lbModelSelect.Items.Add("Pack Leader w/ Boltgun");
            }
            else
            {
                lbModelSelect.Items.Add("Pack Leader w/ Boltgun and " + Weapons[2]);
            }
                
            if (wolfGuard == 1)
            {
                Weapons[0] = "Boltgun";
                Weapons[1] = "Bolt Pistol";
                lbModelSelect.Items.Add("Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
                for (int i = 1; i < UnitSize; i++)
                {
                    lbModelSelect.Items.Add("Grey Hunter w/ " + Weapons[i + 2]);
                }
            }
            else if (wolfGuard == 2)
            {
                Weapons[0] = "Storm Bolter";
                Weapons[1] = "Power Sword";
                lbModelSelect.Items.Add("Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
                for (int i = 1; i < UnitSize; i++)
                {
                    lbModelSelect.Items.Add("Grey Hunter w/ " + Weapons[i + 2]);
                }
            }
            else if (wolfGuard == 0)
            {
                for (int i = 1; i < UnitSize; i++)
                {
                    if(i == wolfStandardIndex)
                    {
                        lbModelSelect.Items.Add("Grey Hunter w/ Wolf Standard and " + Weapons[i + 2]);
                    }
                    else
                    {
                        lbModelSelect.Items.Add("Grey Hunter w/ " + Weapons[i + 2]);
                    }
                }
            }

            cbOption1.Text = "Wolf Standard";

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(new string[]
            {
                "(None)",
                "Wolf Guard Pack Leader (+18 pts)",
                "Wolf Guard Terminator Pack Leader (+34 pts)"
            });
            cmbFaction.SelectedIndex = wolfGuard;

            panel.Controls["lblExtra1"].Location = panel.Controls["lblFactionUpgrade"].Location;
            panel.Controls["lblExtra1"].Text = "May contain one of the following: ";

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblFactionUpgrade"].Location.X, cmbFaction.Location.Y + 32);
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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionUpgrade"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (wolfGuard != 0 && currentIndex == 1)
                        {
                            Weapons[0] = cmbOption1.SelectedItem.ToString();
                            if (wolfGuard == 1)
                            {
                                lbModelSelect.Items[1] = "Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
                            }
                            else if (wolfGuard == 2)
                            {
                                lbModelSelect.Items[1] = "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
                            }
                        }
                        else
                        {
                            Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                            if (currentIndex == 0)
                            {
                                if (Weapons[2] == "(None)")
                                {
                                    lbModelSelect.Items[0] = "Pack Leader w/ Boltgun";
                                }
                                else
                                {
                                    lbModelSelect.Items[0] = "Pack Leader w/ Boltgun and " + Weapons[2];
                                }
                            }
                            else
                            {
                                if (currentIndex == wolfStandardIndex)
                                {
                                    lbModelSelect.Items.Add("Grey Hunter w/ Wolf Standard and " + Weapons[currentIndex + 2]);
                                }
                                else
                                {
                                    lbModelSelect.Items.Add("Grey Hunter w/ " + Weapons[currentIndex + 2]);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (wolfGuard != 0 && currentIndex == 1)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);
                        }
                    }

                    break;
                case 12:
                    if (!restrictedIndexes.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        if (wolfGuard == 1)
                        {
                            lbModelSelect.Items[1] = "Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else if (wolfGuard == 2)
                        {
                            lbModelSelect.Items[1] = "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                    }
                    break;
                case 16:
                    wolfGuard = cmbFaction.SelectedIndex;
                    string temp2 = lbModelSelect.Items[1].ToString();
                    if (temp2.Contains("Blood Claw"))
                    {
                        if (wolfGuard == 1)
                        {
                            Weapons[0] = "Boltgun";
                            Weapons[1] = "Bolt Pistol";
                            lbModelSelect.Items.Insert(1, "Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
                        }
                        if (wolfGuard == 2)
                        {
                            Weapons[0] = "Storm Bolter";
                            Weapons[1] = "Power Sword";
                            lbModelSelect.Items.Insert(1, "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
                        }
                    }
                    else
                    {
                        if (wolfGuard == 1)
                        {
                            Weapons[0] = "Boltgun";
                            Weapons[1] = "Bolt Pistol";
                            lbModelSelect.Items[1] = "Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else if (wolfGuard == 2)
                        {
                            Weapons[0] = "Storm Bolter";
                            Weapons[1] = "Power Sword";
                            lbModelSelect.Items[1] = "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else if (wolfGuard == 0)
                        {
                            if (temp2.Contains("Wolf Guard"))
                            {
                                lbModelSelect.Items.RemoveAt(1);
                                Weapons[0] = "";
                                Weapons[1] = "";
                            }
                        }
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    restrictedIndexes.Clear();
                    restrictedIndexes2.Clear();

                    if (chosenRelic == "Frost Weapon" && stratLeader == 1) //Pack Leader
                    {
                        cmbOption1.SelectedIndex = 2;
                        restrictedIndexes.AddRange(new int[] { 0, 1, 3 });
                    }
                    else if (chosenRelic == "Morkai's Teeth Bolts" && stratLeader == 2 && wolfGuard == 1)
                    {
                        cmbOption1.SelectedIndex = 1;
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 13, 14 });
                    }
                    else if (chosenRelic == "Frost Weapon" && stratLeader == 2 && wolfGuard == 1)
                    {
                        cmbOption2.SelectedIndex = 4;
                        restrictedIndexes2.AddRange(new int[] { 0, 1, 3, 5, 6, 8, 9 });
                    }
                    else if (chosenRelic == "Morkai's Teeth Bolts" && stratLeader == 2 && wolfGuard == 2)
                    {
                        cmbOption1.SelectedIndex = 10;
                        restrictedIndexes.AddRange(new int[] { 0, 5, 6, 7, 8, 9, 11 });
                    }
                    else if (chosenRelic == "Frost Weapon" && stratLeader == 2 && wolfGuard == 2)
                    {
                        cmbOption2.SelectedIndex = 2;
                        restrictedIndexes2.AddRange(new int[] { 0, 3, 4, 6, 7 });
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
                    Relic = chosenRelic;
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        wolfStandardIndex = currentIndex;
                        lbModelSelect.Items[currentIndex] = "Grey Hunter w/ Wolf Standard and " + Weapons[currentIndex + 2];
                    }
                    else
                    {
                        wolfStandardIndex = -1;
                        lbModelSelect.Items[currentIndex] = "Grey Hunter w/ " + Weapons[currentIndex + 2];
                    }
                    break;
                case 30:
                    int temp = UnitSize + (wolfGuard == 0 ? 0 : 1);
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize + (wolfGuard == 0 ? 0 : 1))
                    {
                        for (int i = temp; i < UnitSize + (wolfGuard == 0 ? 0 : 1); i++)
                        {
                            Weapons.Add("Boltgun");
                            lbModelSelect.Items.Add("Grey Hunter w/ " + Weapons[temp + 2 - (wolfGuard == 0 ? 0 : 1)]);
                        }
                    }

                    if (temp > UnitSize + (wolfGuard == 0 ? 0 : 1))
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(temp + (wolfGuard == 0 ? 1 : 0), 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cbOption1.Visible = false;
                        antiLoop = false;
                        break;
                    }
                    else if (currentIndex == 0)
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;

                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text) && stratLeader == 1)
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }
                        else
                        {
                            panel.Controls["lblRelic"].Visible = false;
                            cmbRelic.Visible = false;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Astartes Chainsword",
                            "Power Axe",
                            "Power Fist",
                            "Power Sword"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[2]);

                        restrictedIndexes.Clear();

                        if (Relic == "Frost Weapon" && stratLeader == 1)
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 1, 3 });
                            cmbOption1.SelectedIndex = 2;
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else if (currentIndex == 1 && wolfGuard != 0)
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = false;

                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text) && stratLeader == 2)
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }
                        else
                        {
                            panel.Controls["lblRelic"].Visible = false;
                            cmbRelic.Visible = false;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption2.Items.Clear();

                        if (wolfGuard == 1)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Astartes Chainsword",
                                "Boltgun",
                                "Combi-flamer",
                                "Combi-grav",
                                "Combi-melta",
                                "Combi-plasma",
                                "Lightning Claw",
                                "Plasma Pistol",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Bolter",
                                "Storm Shield",
                                "Thunder Hammer (+10 pts)"
                            });

                            cmbOption2.Items.AddRange(new string[]
                            {
                                "Astartes Chainsword",
                                "Bolt Pistol",
                                "Lightning Claw",
                                "Plasma Pistol",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Shield",
                                "Thunder Hammer (+10 pts)"
                            });

                            restrictedIndexes.Clear();
                            restrictedIndexes2.Clear();

                            if (Relic == "Morkai's Teeth Bolts" && stratLeader == 2)
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 13, 14 });
                                cmbOption1.SelectedIndex = 1;
                            }
                            else if (Relic == "Frost Weapon" && stratLeader == 2)
                            {
                                restrictedIndexes2.AddRange(new int[] { 0, 1, 3, 5, 6, 8, 9 });
                                cmbOption2.SelectedIndex = 4;
                            }
                        }
                        else if (wolfGuard == 2)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Chainfist",
                                "Combi-flamer",
                                "Combi-grav",
                                "Combi-melta",
                                "Combi-plasma",
                                "Lightning Claw",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Bolter",
                                "Thunder Hammer"
                            });

                            cmbOption2.Items.AddRange(new string[]
                            {
                                "Chainfist",
                                "Lightning Claw",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Shield",
                                "Thunder Hammer"
                            });

                            restrictedIndexes.Clear();
                            restrictedIndexes2.Clear();

                            if (Relic == "Morkai's Teeth Bolts" && stratLeader == 2)
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 5, 6, 7, 8, 9, 11 });
                                cmbOption1.SelectedIndex = 10;
                            }
                            else if (Relic == "Frost Weapon" && stratLeader == 2)
                            {
                                restrictedIndexes2.AddRange(new int[] { 0, 3, 4, 6, 7 });
                                cmbOption2.SelectedIndex = 2;
                            }
                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
                    }
                    else
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cbOption1.Visible = true;

                        if(wolfStandardIndex != currentIndex && wolfStandardIndex != -1)
                        {
                            cbOption1.Enabled = false;
                        }
                        else
                        {
                            cbOption1.Enabled = true;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Boltgun",
                            "Flamer",
                            "Grav-gun",
                            "Meltagun",
                            "Plasma Gun",
                            "Plasma Pistol"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);

                        restrictedIndexes.Clear();

                        if (restrict[0] == (UnitSize / 10) + 1 &&
                            (Weapons[currentIndex + 2] == "Boltgun" || Weapons[currentIndex + 2] == "Plasma Pistol"))
                        {
                            restrictedIndexes.AddRange(new int[] { 1, 2, 3, 4 });
                        }

                        if (restrict[1] == 1 && Weapons[currentIndex + 2] != "Plasma Pistol")
                        {
                            restrictedIndexes.Add(5);
                        }
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);

                    antiLoop = false;
                    break;
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        Stratagem.Add(cbStratagem5.Text);
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;

                        if (currentIndex == 0)
                        {
                            stratLeader = 1;
                        }
                        else
                        {
                            stratLeader = 2;
                        }
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
                        stratLeader = 0;
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0].Contains("Thunder Hammer (+10 pts)") || Weapons[1].Contains("Thunder Hammer (+10 pts)"))
            {
                Points += 10;
            }
            if (wolfGuard == 1)
            {
                Points += 18;
            }
            if (wolfGuard == 2)
            {
                Points += 34;
            }

            restrict[0] = 0;
            restrict[1] = 0;

            for (int i = 3; i < Weapons.Count; i++)
            {
                if (Weapons[i] == "Plasma Pistol")
                {
                    restrict[1]++;
                }

                if (Weapons[i] != "Astartes Chainsword" && Weapons[i] != "Plasma Pistol")
                {
                    restrict[0]++;
                }
            }
        }

        public override string ToString()
        {
            return "Grey Hunters - " + Points + "pts";
        }
    }
}
