using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class RubricMarines : Datasheets
    {
        int currentIndex;
        bool icon = false;

        public RubricMarines()
        {
            DEFAULT_POINTS = 21;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL1m1k";
            Weapons.Add("Inferno Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Inferno Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TZEENTCH", "HERETIC ASTARTES", "ARCANA ASTARTES", "THOUSAND SONS", "<GREAT CULT>",
                "INFANTRY", "PSYKER", "CORE", "RUBRIC MARINES"
            });
            PsykerPowers = new string[1] { string.Empty };
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new RubricMarines();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ThousandSons;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Aspiring Sorcerer w/ " + Weapons[0] + " and Force Stave");
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Rubric Marine w/ " + Weapons[i]);
            }

            cbOption1.Visible = true;
            cbOption1.Text = "Icon of Flame";
            if(icon)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            panel.Controls["lblPsyker"].Visible = true;
            panel.Controls["lblPsyker"].Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 55);

            clbPsyker.Visible = true;
            clbPsyker.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 80);

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            if (Factionupgrade == "Protégé (+5 pts)")
            {
                panel.Controls["lblPsyker"].Text = "Select two of the following:";
                clbPsyker.ClearSelected();
                for (int i = 0; i < clbPsyker.Items.Count; i++)
                {
                    clbPsyker.SetItemChecked(i, false);
                }

                if (PsykerPowers[0] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
                }
                if (PsykerPowers[1] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[1]), true);
                }
            }
            else
            {
                panel.Controls["lblPsyker"].Text = "Select one of the following:";
                clbPsyker.ClearSelected();
                for (int i = 0; i < clbPsyker.Items.Count; i++)
                {
                    clbPsyker.SetItemChecked(i, false);
                }

                if (PsykerPowers[0] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
                }
            }

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }
            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;
            panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(lbModelSelect.Location.X, lbModelSelect.Location.Y + lbModelSelect.Height + 24);
            cmbFaction.Location = new System.Drawing.Point(lbModelSelect.Location.X + 4, lbModelSelect.Location.Y + lbModelSelect.Height + 44);

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, clbPsyker.Location.Y + 16 + clbPsyker.Height);
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
            if(antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();

                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Aspiring Sorcerer w/ " + Weapons[0] + " and Force Stave";
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Rubric Marine w/ " + Weapons[currentIndex];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFaction.SelectedItem.ToString();

                    if (Factionupgrade == "Protégé (+5 pts)")
                    {
                        panel.Controls["lblPsyker"].Text = "Select two of the following:";
                        var temp2 = PsykerPowers;

                        PsykerPowers = new string[2] { string.Empty, string.Empty };
                        if (temp2[0] != string.Empty)
                        {

                            for (int i = 0; i < clbPsyker.Items.Count; i++)
                            {
                                clbPsyker.SetItemChecked(i, false);
                            }

                            clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp2[0]), true);
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        }
                    }
                    else
                    {
                        panel.Controls["lblPsyker"].Text = "Select one of the following:";
                        var temp3 = PsykerPowers;

                        PsykerPowers = new string[1] { string.Empty };
                        if (temp3[0] != string.Empty)
                        {

                            for (int i = 0; i < clbPsyker.Items.Count; i++)
                            {
                                clbPsyker.SetItemChecked(i, false);
                            }

                            clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp3[0]), true);
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        }
                    }

                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    if(chosenRelic == "Coruscator")
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
                    if(cbOption1.Checked)
                    {
                        icon = true;
                    }
                    else
                    {
                        icon = false;
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Inferno Bolt Gun");
                            lbModelSelect.Items.Add("Rubric Marine w/ " + Weapons[temp]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(temp - 1, 1);
                    }
                    break;
                case 60:
                    if (Factionupgrade == "Protégé (+5 pts)")
                    {
                        if (clbPsyker.CheckedItems.Count < 2)
                        {
                            break;
                        }
                        else if (clbPsyker.CheckedItems.Count == 2)
                        {
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                            PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                        }
                        else
                        {
                            clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                        }
                    }
                    else
                    {
                        if (clbPsyker.CheckedItems.Count < 1)
                        {
                            break;
                        }
                        else if (clbPsyker.CheckedItems.Count == 1)
                        {
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        }
                        else
                        {
                            clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                        }
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
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
                                "Inferno Bolt Pistol",
                                "Plasma Pistol",
                                "Warpflame Pistol"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                            if (Relic == "Coruscator")
                            {
                                cmbOption1.SelectedIndex = 0;
                                cmbOption1.Enabled = false;
                            }
                        }
                        else
                        {
                            cbStratagem5.Visible = false;
                            cmbRelic.Visible = false;
                            panel.Controls["lblRelic"].Visible = false;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Inferno Boltgun",
                                "Soulreaper Cannon (+5 pts)",
                                "Warpflamer (+3 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);

                            if (Weapons.Contains("Soulreaper Cannon (+5 pts)") && Weapons[currentIndex] != "Soulreaper Cannon (+5 pts)")
                            {
                                restrictedIndexes.Add(1);
                            }
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
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
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (var weapon in Weapons)
            {
                if(weapon == "Warpflamer (+3 pts)")
                {
                    Points += 3;
                }
                else if(weapon == "Soulreaper Cannon (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Rubric Marines - " + Points + "pts";
        }
    }
}