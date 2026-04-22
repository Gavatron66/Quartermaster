using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Dark_Angels
{
    public class DeathwingCommand : Datasheets
    {
        int currentIndex;
        int restriction = 0;

        public DeathwingCommand()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 2;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("");
            Weapons.Add("Storm Bolter and Power Sword");
            Weapons.Add("");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Storm Bolter");
                Weapons.Add("Power Fist");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DARK ANGELS",
                "INFANTRY", "CORE", "TERMINATOR", "DEATHWING", "INNER CIRCLE", "COMMAND SQUAD", "DEATHWING COMMAND SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathwingCommand();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 2;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 5;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Deathwing Sergeant w/ " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Deathwing Terminator w/ " + Weapons[(i * 2) + 1] + " and " + Weapons[(i * 2) + 2]);
            }

            cbOption1.Text = "Watcher in the Dark";

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "",
                "Chainfist",
                "Power Fist",
            });

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["cbOption1"].Location.X, panel.Controls["cbOption1"].Location.Y + 32);
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
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex) || currentIndex == 0)
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[(currentIndex * 2) + 1] == "Thunder Hammer and Storm Shield" || Weapons[(currentIndex * 2) + 1] == "Two Lightning Claws")
                        {
                            if (currentIndex == 0)
                            {
                                lbModelSelect.Items[0] = "Deathwing Sergeant w/ " + Weapons[1];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Deathwing Terminator w/ " + Weapons[(currentIndex * 2) + 1];
                            }
                            cmbOption2.SelectedIndex = 0;
                        }
                        else
                        {
                            if (currentIndex == 0)
                            {
                                cmbOption2.SelectedIndex = 1;
                                lbModelSelect.Items[0] = "Deathwing Sergeant w/ " + Weapons[1];
                            }
                            else
                            {
                                if (Weapons[(currentIndex * 2) + 2] == "")
                                {
                                    cmbOption2.SelectedIndex = 2;
                                }

                                lbModelSelect.Items[currentIndex] = "Deathwing Terminator w/ " + Weapons[(currentIndex * 2) + 1]
                                    + " and " + Weapons[(currentIndex * 2) + 2];
                            }
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 2] = cmbOption2.SelectedItem.ToString();
                    if (Weapons[(currentIndex * 2) + 1] == "Thunder Hammer and Storm Shield" || Weapons[(currentIndex * 2) + 1] == "Two Lightning Claws")
                    {
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Deathwing Sergeant w/ " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Deathwing Terminator w/ " + Weapons[(currentIndex * 2) + 1];
                        }
                        cmbOption2.SelectedIndex = 0;
                    }
                    else
                    {
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Deathwing Sergeant w/ " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Deathwing Terminator w/ " + Weapons[(currentIndex * 2) + 1]
                                + " and " + Weapons[(currentIndex * 2) + 2];
                        }
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    if (chosenRelic == "Bolts of Judgement")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }

                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[0] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Storm Bolter");
                            Weapons.Add("Power Fist");
                            lbModelSelect.Items.Add("Deathwing Terminator w/ " + Weapons[(currentIndex * 2) + 1]
                                + " and " + Weapons[(currentIndex * 2) + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 2) + 1, 2);
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
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        cbStratagem5.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.Visible = false;

                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);

                        if (Weapons[0] == "")
                        {
                            cbOption1.Checked = false;
                        }
                        else
                        {
                            cbOption1.Checked = true;
                        }

                        if (currentIndex == 0)
                        {
                            cmbOption2.Visible = false;
                            panel.Controls["lblOption2"].Visible = false;
                            cbStratagem5.Visible = true;

                            if (Stratagem.Contains(cbStratagem5.Text))
                            {
                                panel.Controls["lblRelic"].Visible = true;
                                cmbRelic.Visible = true;

                                if (Relic == "Bolts of Judgement")
                                {
                                    cmbOption1.SelectedIndex = 0;
                                    cmbOption1.Enabled = false;
                                }
                            }

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Storm Bolter and Power Sword",
                                "Thunder Hammer and Storm Shield",
                                "Two Lightning Claws"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                            this.DrawItemWithRestrictions(new List<int>(), cmbOption1);
                        }
                        else
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "",
                                "Assault Cannon",
                                "Cyclone Missile Launcher and Storm Bolter",
                                "Heavy Flamer",
                                "Plasma Cannon",
                                "Storm Bolter",
                                "Thunder Hammer and Storm Shield",
                                "Two Lightning Claws"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                            restrictedIndexes.Clear();
                            if (UnitSize < 5 || (restriction == UnitSize / 5 && cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]) >= 5))
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 1, 2, 3, 4 });
                            }
                            else
                            {
                                restrictedIndexes.Add(0);
                            }

                            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                            this.DrawItemWithRestrictions(new List<int>() { 0 }, cmbOption2);

                            if (Weapons[(currentIndex * 2) + 1] == "Thunder Hammer and Storm Shield" || Weapons[(currentIndex * 2) + 1] == "Two Lightning Claws")
                            {
                                cmbOption2.Enabled = false;
                            }
                            else
                            {
                                cmbOption2.Enabled = true;
                            }

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
                default: break;
            }

            antiLoop = false;

            Points = DEFAULT_POINTS * UnitSize;
            restriction = 0;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Assault Cannon" || weapon == "Cyclone Missile Launcher and Storm Bolter" || weapon == "Heavy Flamer"
                    || weapon == "Plasma Cannon")
                {
                    restriction++;
                }
            }
        }

        public override string ToString()
        {
            return "Deathwing Command Squad - " + Points + "pts";
        }
    }
}