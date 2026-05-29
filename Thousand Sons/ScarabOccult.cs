using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class ScarabOccult : Datasheets
    {
        int currentIndex;
        int heavy;
        int missiles;

        public ScarabOccult()
        {
            DEFAULT_POINTS = 40;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Inferno Combi-bolter");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TZEENTCH", "HERETIC ASTARTES", "ARCANA ASTARTES", "THOUSAND SONS", "<GREAT CULT>",
                "INFANTRY", "TERMINATOR", "PSYKER", "CORE", "SCARAB OCCULT TERMINATORS"
            });
            PsykerPowers = new string[1] { string.Empty };
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new ScarabOccult();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ThousandSons;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
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
            lbModelSelect.Items.Add("Scarab Occult Sorcerer w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Scarab Occult Terminator w/ " + Weapons[i]);
            }

            panel.Controls["lblPsyker"].Visible = true;
            panel.Controls["lblPsyker"].Location = new System.Drawing.Point(cmbOption1.Location.X - 4, cmbOption1.Location.Y + 55);

            clbPsyker.Visible = true;
            clbPsyker.Location = new System.Drawing.Point(cmbOption1.Location.X - 4, cmbOption1.Location.Y + 80);

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
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
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
                            lbModelSelect.Items[0] = "Scarab Occult Sorcerer w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Scarab Occult Terminator w/ " + Weapons[currentIndex];
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

                    Relic = chosenRelic;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Inferno Combi-bolter");
                            lbModelSelect.Items.Add("Scarab Occult Terminator w/ " + Weapons[temp]);
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
                                "Inferno Combi-bolter",
                                "Prosperine Khopesh"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else
                        {
                            cbStratagem5.Visible = false;
                            cmbRelic.Visible = false;
                            panel.Controls["lblRelic"].Visible = false;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Heavy Warpflamer (+5 pts)",
                                "Inferno Combi-bolter",
                                "Inferno Combi-bolter and Hellfyre Missile Rack (+10 pts)",
                                "Soulreaper Cannon (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);

                            if(Weapons[currentIndex].Contains("Inferno Combi-bolter") && heavy == UnitSize / 5)
                            {
                                restrictedIndexes.Add(0);
                                restrictedIndexes.Add(3);
                            }

                            if (!Weapons[currentIndex].Contains("Hellfyre") && missiles == UnitSize / 5)
                            {
                                restrictedIndexes.Add(2);
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

            missiles = 0;
            heavy = 0;
            foreach (var weapon in Weapons)
            {
                if (weapon == "Heavy Warpflamer (+5 pts)")
                {
                    Points += 5;
                    heavy++;
                }
                else if (weapon == "Soulreaper Cannon (+5 pts)")
                {
                    Points += 5;
                    heavy++;
                }
                else if (weapon == "Inferno Combi-bolter and Hellfyre Missile Rack (+10 pts)")
                {
                    Points += 10;
                    missiles++;
                }
            }
        }

        public override string ToString()
        {
            return "Scarab Occult Terminators - " + Points + "pts";
        }
    }
}