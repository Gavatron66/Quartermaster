using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class ProteusKillTeam : Datasheets
    {
        int currentIndex;
        int[] restrict = new int[3] { 0, 0, 0 };
        List<int> restrictedIndexes2 = new List<int>();
        bool jumpPacks = false;

        public ProteusKillTeam()
        {
            DEFAULT_POINTS = 27;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL3m1k";
            Weapons.Add("Deathwatch Boltgun");
            Weapons.Add("Power Sword");
            Weapons.Add("");
            Weapons.Add("Watch Sergeant (27 pts)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Deathwatch Boltgun");
                Weapons.Add("Power Sword");
                Weapons.Add("");
                Weapons.Add("Deathwatch Veteran (27 pts)");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DEATHWATCH",
                "INFANTRY", "CORE", "KILL TEAM", "PROTEUS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new ProteusKillTeam();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "";

            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            Label lblOption1 = panel.Controls["lblOption1"] as Label;
            lblExtra1.Location = new System.Drawing.Point(lblOption1.Location.X, lblOption1.Location.Y - 25);
            lblExtra1.Text = "If a weapon contains a *, then it can only taken by itself";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add(Weapons[3] + " w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add(Weapons[(i * 4) + 3] + " w/ " + Weapons[(i * 4)] + " and " + Weapons[(i * 4) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Deathwatch Veteran (27 pts)",
                "Deathwatch Terminator (35 pts)",
                "Vanguard Veteran (20 pts)",
                "Veteran Biker (35 pts)"
            });

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
            cmbFaction.Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Text = "Kill Team Specialism";

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(cmbFaction.Location.X, cmbFaction.Location.Y + 32);
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

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if(cmbOption1.SelectedIndex == -1)
                    {
                        break;
                    }

                    Weapons[(currentIndex * 4) + 3] = cmbOption1.SelectedItem.ToString();
                    if (Weapons[(currentIndex * 4) + 3] == "Deathwatch Veteran (27 pts)")
                    {
                        Weapons[(currentIndex * 4)] = "Deathwatch Boltgun";
                        Weapons[(currentIndex * 4) + 1] = "Power Sword";
                        Weapons[(currentIndex * 4) + 2] = "";
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Deathwatch Terminator (35 pts)")
                    {
                        Weapons[(currentIndex * 4)] = "Storm Bolter";
                        Weapons[(currentIndex * 4) + 1] = "Power Fist";
                        Weapons[(currentIndex * 4) + 2] = "";
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Vanguard Veteran (20 pts)")
                    {
                        Weapons[(currentIndex * 4)] = "Bolt Pistol";
                        Weapons[(currentIndex * 4) + 1] = "Astartes Chainsword";
                        Weapons[(currentIndex * 4) + 2] = "";
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Veteran Biker (35 pts)")
                    {
                        Weapons[(currentIndex * 4)] = "(None)";
                        Weapons[(currentIndex * 4) + 1] = "Biker";
                        Weapons[(currentIndex * 4) + 2] = "";
                    }

                    if(Weapons[(currentIndex * 4) + 3] == "Veteran Biker (35 pts)")
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                    }

                    lbModelSelect.SelectedIndex = currentIndex;
                    break;
                case 12:
                    if (!restrictedIndexes.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[currentIndex * 4] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[currentIndex * 4].Contains('*') || Weapons[(currentIndex * 4) + 1] == "Biker")
                        {
                            lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)];
                            cmbOption3.Enabled = false;
                        }
                        else if (Weapons[currentIndex * 4] == "(None)")
                        {
                            lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3];
                            cmbOption3.Enabled = true;
                        }
                        else if (currentIndex == 0 && Weapons[2] == "Combat Shield")
                        {
                            lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + ", " + Weapons[(currentIndex * 4) + 1]
                                + " and a " + Weapons[(currentIndex * 4) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                            cmbOption3.Enabled = true;
                        }
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);
                    }
                    break;
                case 13:
                    Weapons[(currentIndex * 4) + 1] = cmbOption3.SelectedItem.ToString();
                    if (Weapons[currentIndex * 4].Contains('*'))
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)];
                    }
                    else if (currentIndex == 0 && Weapons[2] == "Combat Shield")
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + ", " + Weapons[(currentIndex * 4) + 1]
                            + " and a " + Weapons[(currentIndex * 4) + 2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    restrictedIndexes.Clear();

                    if (chosenRelic == "Banebolts of Eryxia" || chosenRelic == "Artificer Bolt Cache")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 23, 24, 25 });
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        if(Weapons[(currentIndex * 4) + 3] == "Vanguard Veteran (20 pts)")
                        {
                            jumpPacks = true;
                            break;
                        }

                        Weapons[(currentIndex * 4) + 2] = cbOption1.Text;
                        if(currentIndex != 0)
                        {
                            Weapons[(currentIndex * 4) + 3] = cbOption1.Text;
                        }
                        else
                        {
                            Weapons[2] = cbOption1.Text;
                        }

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)];
                        }
                        else if (currentIndex == 0 && Weapons[2] == "Combat Shield")
                        {
                            lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + ", " + Weapons[(currentIndex * 4) + 1]
                                + " and a " + Weapons[(currentIndex * 4) + 2];
                        }
                        else if (Weapons[(currentIndex * 4) + 2] == cbOption1.Text)
                        {
                            lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 4) + 3] == "Vanguard Veteran (20 pts)")
                        {
                            jumpPacks = false;
                            break;
                        }

                        Weapons[(currentIndex * 4) + 2] = "";
                        if (currentIndex != 0)
                        {
                            Weapons[(currentIndex * 4) + 3] = "Deathwatch Veteran (27 pts)";

                            if (Weapons[currentIndex * 4].Contains('*'))
                            {
                                lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)];

                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                            }
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
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            lbModelSelect.Items.Add("<--- Select a Model --->");
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((temp - 1) * 4) - 1, 4);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;
                    cmbOption3.Enabled = true;

                    if (currentIndex < 0)
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        cmbOption3.Visible = false;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = false;
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);
                    cmbOption2.Items.Clear();
                    cmbOption3.Items.Clear();

                    restrictedIndexes.Clear();
                    restrictedIndexes2.Clear();

                    if (Weapons[(currentIndex * 4) + 3] == "Watch Sergeant (27 pts)")
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = true;
                        cmbOption3.Visible = true;
                        cbOption1.Visible = true;
                        lblExtra1.Visible = true;

                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            cmbOption3.Enabled = false;
                        }

                        cmbOption3.Items.Clear();
                        cmbOption3.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 2));
                        cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);

                        cbOption1.Text = "Combat Shield";
                        if (Weapons[(currentIndex * 4) + 2] == cbOption1.Text)
                        {
                            cbOption1.Checked = true;
                        }
                        else
                        {
                            cbOption1.Checked = false;
                        }

                        if (Relic == "Banebolts of Eryxia" || Relic == "Artificer Bolt Cache")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 23, 24, 25 });
                        }
                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                        this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption3);

                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Deathwatch Veteran (27 pts)" || Weapons[(currentIndex * 4) + 3] == "Black Shield")
                    {
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = true;
                        cmbOption3.Visible = true;
                        cbOption1.Visible = true;
                        lblExtra1.Visible = true;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        if (currentIndex < 5)
                        {
                            panel.Controls["lblOption1"].Visible = false;
                            cmbOption1.Visible = false;
                        }
                        else
                        {
                            panel.Controls["lblOption1"].Visible = true;
                            cmbOption1.Visible = true;
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);
                        }

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            cmbOption3.Enabled = false;
                        }

                        cbOption1.Text = "Black Shield";
                        if (Weapons[(currentIndex * 4) + 2] == cbOption1.Text)
                        {
                            cbOption1.Checked = true;
                            cbOption1.Enabled = true;
                        }
                        else
                        {
                            cbOption1.Checked = false;
                            if(Weapons.Contains("Black Shield"))
                            {
                                cbOption1.Enabled = false;
                            }
                        }

                        cmbOption3.Items.Clear();
                        cmbOption3.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 2));
                        cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                        this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption3);
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Deathwatch Terminator (35 pts)")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = true;
                        cmbOption3.Visible = true;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = true;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            cmbOption3.Enabled = false;
                        }

                        cmbOption3.Items.Clear();
                        cmbOption3.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 2));
                        cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                        this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption3);
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Vanguard Veteran (20 pts)")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = true;
                        cmbOption3.Visible = true;
                        cbOption1.Visible = true;
                        lblExtra1.Visible = true;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            cmbOption3.Enabled = false;
                        }

                        cbOption1.Text = "Jump Packs (+3 pts/Vanguard Veteran)";
                        if (jumpPacks)
                        {
                            cbOption1.Checked = true;
                        }
                        else
                        {
                            cbOption1.Checked = false;
                        }

                        cmbOption3.Items.Clear();
                        cmbOption3.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 2));
                        cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                        this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption3);
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Veteran Biker (35 pts)")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = false;
                        cmbOption3.Visible = false;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                        this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption3);
                    }
                    else
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        cmbOption3.Visible = false;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.SelectedIndex = -1;
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption3);

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

            Points = 0;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            restrict[0] = 0;
            restrict[1] = 0;
            restrict[2] = 0;

            foreach (var weapon in Weapons)
            {
                if (weapon == "*Deathwatch Frag Cannon" || weapon == "*Infernus Heavy Bolter"
                    || weapon == "*Heavy Bolter" || weapon == "*Heavy Flamer" || weapon == "*Missile Launcher")
                {
                    restrict[0]++;
                }

                if (weapon == "*Heavy Thunder Hammer (+12 pts)")
                {
                    restrict[1]++;
                    Points += 12;
                }

                if (weapon == "Thunder Hammer (+10 pts)")
                {
                    Points += 10;
                }

                if (weapon == "Xenophase Blade (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon == "Assault Cannon" || weapon == "Cyclone Missile Launcher and Storm Bolter (+10 pts)" || weapon == "Heavy Flamer"
                    || weapon == "Plasma Cannon")
                {
                    restrict[2]++;
                }

                if (weapon == "Cyclone Missile Launcher and Storm Bolter (+10 pts)")
                {
                    Points += 10;
                }
                if (weapon == "Lightning Claw (+3 pts)")
                {
                    Points += 3;
                }

                if (weapon == "Storm Shield (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon == "Power Fist (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Watch Sergeant (27 pts)" || weapon == "Deathwatch Veteran (27 pts)")
                {
                    Points += 27;
                }
                if (weapon == "Deathwatch Terminator (35 pts)")
                {
                    Points += 35;
                }
                if (weapon == "Vanguard Veteran (20 pts)")
                {
                    Points += 20;
                    if(jumpPacks)
                    {
                        Points += 3;
                    }
                }
                if (weapon == "Veteran Biker (35 pts)")
                {
                    Points += 35;
                }
            }

            for(int i = 1; i < Weapons.Count + 1; i *= 4)
            {
                if(Weapons[i-1] == "*Heavy Thunder Hammer (+12 pts)" && Weapons[i + 2] == "Vanguard Veteran")
                {
                    restrict[1]--;
                }
            }
        }

        public override string ToString()
        {
            return "Proteus Kill Team - " + Points + "pts";
        }

        private string[] GetWeapons(string model, int comboBox)
        {
            List<string> weaponsList = new List<string>();

            if (model == "Watch Sergeant (27 pts)" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Deathwatch Boltgun",
                    "Deathwatch Combi-flamer",
                    "Deathwatch Combi-grav",
                    "Deathwatch Combi-melta",
                    "Deathwatch Combi-plasma",
                    "*Deathwatch Shotgun",
                    "Flamer",
                    "Grav-gun",
                    "Grav-pistol",
                    "Hand Flamer",
                    "Inferno Pistol",
                    "Lightning Claw",
                    "Meltagun",
                    "Plasma Gun",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist",
                    "Power Maul",
                    "Power Sword",
                    "*Stalker-pattern Boltgun",
                    "Storm Bolter",
                    "Storm Shield",
                    "Thunder Hammer (+10 pts)",
                    "Xenophase Blade (+5 pts)"
                });
            }
            else if (model == "Watch Sergeant (27 pts)" && comboBox == 2)
            {
                weaponsList.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Grav-pistol",
                    "Hand Flamer",
                    "Inferno Pistol",
                    "Lightning Claw",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist",
                    "Power Maul",
                    "Power Sword",
                    "Thunder Hammer (+10 pts)",
                    "Xenophase Blade (+5 pts)"
                });
            }
            else if ((model == "Deathwatch Veteran (27 pts)" || model == "Black Shield") && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Deathwatch Boltgun",
                    "Deathwatch Combi-flamer",
                    "Deathwatch Combi-grav",
                    "Deathwatch Combi-melta",
                    "Deathwatch Combi-plasma",
                    "*Deathwatch Frag Cannon",
                    "*Deathwatch Shotgun",
                    "Flamer",
                    "Grav-gun",
                    "Grav-pistol",
                    "Hand Flamer",
                    "*Heavy Bolter",
                    "*Heavy Flamer",
                    "*Heavy Thunder Hammer (+12 pts)",
                    "Inferno Pistol",
                    "*Infernus Heavy Bolter",
                    "Lightning Claw",
                    "Meltagun",
                    "*Missile Launcher",
                    "Plasma Gun",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist",
                    "Power Maul",
                    "Power Sword",
                    "*Stalker-pattern Boltgun",
                    "Storm Bolter",
                    "Storm Shield",
                    "Thunder Hammer (+10 pts)",
                });

                if (Weapons[(currentIndex * 4) + 2].Contains("Black Shield"))
                {
                    weaponsList.Remove("*Deathwatch Frag Cannon");
                    weaponsList.Remove("*Infernus Heavy Bolter");
                    weaponsList.Remove("*Heavy Bolter");
                    weaponsList.Remove("*Heavy Flamer");
                    weaponsList.Remove("*Missile Launcher");
                }
                else
                {
                    if (restrict[0] == 4)
                    {
                        if (Weapons[(currentIndex * 4)] != "*Deathwatch Frag Cannon" && Weapons[(currentIndex * 4)] != "*Heavy Bolter"
                            && Weapons[(currentIndex * 4)] != "*Heavy Flamer" && Weapons[(currentIndex * 4)] != "*Infernus Heavy Bolter"
                            && Weapons[(currentIndex * 4)] != "*Missile Launcher")
                        {
                            restrictedIndexes.Add(7);
                            restrictedIndexes.Add(13);
                            restrictedIndexes.Add(14);
                            restrictedIndexes.Add(17);
                            restrictedIndexes.Add(20);
                        }
                    }
                }

                if (restrict[1] == UnitSize / 5 && Weapons[(currentIndex * 4)] != "*Heavy Thunder Hammer (+12 pts)")
                {
                    restrictedIndexes.Add(15);
                }
            }
            else if ((model == "Deathwatch Veteran (27 pts)" || model == "Black Shield") && comboBox == 2)
            {
                weaponsList.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Grav-pistol",
                    "Hand Flamer",
                    "Inferno Pistol",
                    "Lightning Claw",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist",
                    "Power Maul",
                    "Power Sword",
                    "Thunder Hammer (+10 pts)"
                });
            }
            else if (model == "Deathwatch Terminator (35 pts)" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
                {
                    "Assault Cannon",
                    "Cyclone Missile Launcher and Storm Bolter (+10 pts)",
                    "Heavy Flamer",
                    "Plasma Cannon",
                    "Storm Bolter",
                    "*Thunder Hammer and Storm Shield",
                    "*Two Lightning Claws"
                });

                if (restrict[2] == 3)
                {
                    if (Weapons[currentIndex * 4] != "Assault Cannon" && Weapons[currentIndex * 4] != "Cyclone Missile Launcher and Storm Bolter (+10 pts)"
                        && Weapons[currentIndex * 4] != "Heavy Flamer" && Weapons[currentIndex * 4] != "Plasma Cannon")
                    {
                        restrictedIndexes.Add(0);
                        restrictedIndexes.Add(1);
                        restrictedIndexes.Add(2);
                        restrictedIndexes.Add(3);
                    }
                    
                }
            }
            else if (model == "Deathwatch Terminator (35 pts)" && comboBox == 2)
            {
                weaponsList.AddRange(new string[]
                {
                    "Chainfist",
                    "Power Axe",
                    "Power Fist",
                    "Power Maul",
                    "Power Sword",
                });
            }
            else if (model == "Vanguard Veteran (20 pts)" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Grav-pistol",
                    "Hand Flamer",
                    "*Heavy Thunder Hammer (+12 pts)",
                    "Inferno Pistol",
                    "Lightning Claw (+3 pts)",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist (+5 pts)",
                    "Power Maul",
                    "Power Sword",
                    "Storm Shield (+5 pts)",
                    "Thunder Hammer (+10 pts)"
                });
            }
            else if (model == "Vanguard Veteran (20 pts)" && comboBox == 2)
            {
                weaponsList.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Grav-pistol",
                    "Hand Flamer",
                    "Inferno Pistol",
                    "Lightning Claw (+3 pts)",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist (+5 pts)",
                    "Power Maul",
                    "Power Sword",
                    "Storm Shield (+5 pts)",
                    "Thunder Hammer (+10 pts)"
                });
            }
            else if (model == "Veteran Biker (35 pts)" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
                {
                    "(None)",
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Power Axe",
                    "Power Maul",
                    "Power Sword"
                });
            }

            return weaponsList.ToArray();
        }
    }
}
