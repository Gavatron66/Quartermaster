using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Roster_Builder.Leagues_of_Votann
{
    public class HearthkynWarriors : Datasheets
    {
        int currentIndex;
        bool medpack = false;
        bool comms = false;
        bool scanner = false;
        int[] restrict;

        public HearthkynWarriors()
        {
            DEFAULT_POINTS = 13;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m3k";
            Weapons.Add("Autoch-pattern Bolters");
            Weapons.Add("Autoch-pattern Bolter");
            Weapons.Add("Autoch-pattern Bolt Pistol");
            Weapons.Add("Autoch-pattern Bolter");
            Weapons.Add("Autoch-pattern Bolter");
            Keywords.AddRange(new string[]
            {
                "VOTANN", "<LEAGUE>",
                "INFANTRY", "CORE", "SHIELD CREST", "CONCUSSION", "HEARTHKYN WARRIORS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new HearthkynWarriors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as LeaguesOfVotann;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            #region Custom Form Setup
            Label lblOption1 = panel.Controls["lblOption1"] as Label;
            Label lblOption2 = panel.Controls["lblOption2"] as Label;
            Label lblOption3 = panel.Controls["lblOption3"] as Label;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;


            lbModelSelect.Location = new System.Drawing.Point(lbModelSelect.Location.X, lbModelSelect.Location.Y + 20);
            lbModelSelect.Size = new System.Drawing.Size(lbModelSelect.Size.Width, lbModelSelect.Size.Height - 20);
            cmbOption1.Location = new System.Drawing.Point(cmbOption1.Location.X, cmbOption1.Location.Y + 20);
            lblOption1.Location = new System.Drawing.Point(lblOption1.Location.X, lblOption1.Location.Y + 20);
            lblOption2.Location = new System.Drawing.Point(lblOption2.Location.X, lblOption2.Location.Y + 20);

            cmbOption2.Location = new System.Drawing.Point(cmbOption2.Location.X, cmbOption2.Location.Y + 20);
            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 90);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 85);
            cbOption3.Location = new System.Drawing.Point(cbOption3.Location.X, cbOption3.Location.Y + 85);

            lblOption3.Location = new System.Drawing.Point(86, 63);
            cmbOption3.Location = new System.Drawing.Point(311, 59);

            cmbOption3.Visible = true;
            lblOption3.Visible = true;
            cbOption1.Visible = true;
            cbOption2.Visible = true;
            cbOption3.Visible = true;
            #endregion

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Theyn w/ " + Weapons[1] + " and " + Weapons[2]);
            for (int i = 3; i <= Weapons.Count - 1; i++)
            {
                lbModelSelect.Items.Add("Hearthkyn Warrior w/ " + Weapons[i]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Autoch-pattern Bolt Pistol",
                "EtaCarn Plasma Pistol (+5 pts)",
                "Ion Pistol (+5 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[2]);

            cbOption1.Text = "Medipack (+5 pts)";
            cbOption2.Text = "Multiwave Commans Array (+5 pts)";
            cbOption3.Text = "Pan Spectral Scanner (+5 pts)";

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Autoch-pattern Bolters",
                "Ion Blasters (+1 pts/model)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[0]);

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption3.Location.Y + 32);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            antiLoop = true;
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
            antiLoop = false;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[1] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Theyn w/ " + Weapons[currentIndex + 1] + " and " + Weapons[currentIndex + 2];

                        break;
                    }

                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Hearthkyn Warrior w/ " + Weapons[currentIndex + 2];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);
                    }
                    break;
                case 12:
                    Weapons[2] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Theyn w/ " + Weapons[1] + " and " + Weapons[2];
                    break;
                case 13:
                    Weapons[0] = cmbOption3.SelectedItem.ToString();
                    for(int i = 3; i < Weapons.Count; i++)
                    {
                        if (Weapons[i] == "Autoch-pattern Bolter" && Weapons[0] == "Ion Blasters (+1 pts/model)")
                        {
                            Weapons[i] = "Ion Blaster";
                            lbModelSelect.Items[i - 2] = "Hearthkyn Warrior w/ " + Weapons[i];
                        }
                        else if (Weapons[i] == "Ion Blaster" && Weapons[0] == "Autoch-pattern Bolters")
                        {
                            Weapons[i] = "Autoch-pattern Bolter";
                            lbModelSelect.Items[i - 2] = "Hearthkyn Warrior w/ " + Weapons[i];
                        }
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (chosenRelic == "Grudge's End (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    else if(chosenRelic == "Grudge's End (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 0;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "The Hearthfist")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }

                    Relic = chosenRelic;
                    break;
                case 21:
                    medpack = cbOption1.Checked;
                    break;
                case 22:
                    comms = cbOption2.Checked;
                    break;
                case 23:
                    scanner = cbOption3.Checked;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        if (lbModelSelect.Items.Count < (UnitSize / 10) + 2)
                        {
                            if (Weapons[0] == "Autoch-pattern Bolters")
                            {
                                Weapons.Add("Autoch-pattern Bolter");
                                Weapons.Add("Autoch-pattern Bolter");
                            }
                            else
                            {
                                Weapons.Add("Ion Blaster");
                                Weapons.Add("Ion Blaster");
                            }
                            lbModelSelect.Items.Add("Hearthkyn Warrior w/ " + Weapons[(UnitSize / 10) + 3]);
                            lbModelSelect.Items.Add("Hearthkyn Warrior w/ " + Weapons[(UnitSize / 10) + 4]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        if (lbModelSelect.Items.Count > (UnitSize / 10) + 2)
                        {
                            lbModelSelect.Items.RemoveAt((temp / 10) + 2);
                            lbModelSelect.Items.RemoveAt((temp / 10) + 1);
                            Weapons.RemoveRange((UnitSize / 10) + 4, 2);
                        }
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        break;
                    }
                    else
                    {
                        antiLoop = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.Items.Clear();
                        if (currentIndex == 0)
                        {
                            cbStratagem5.Visible = true;

                            if (Stratagem.Contains(cbStratagem5.Text))
                            {
                                panel.Controls["lblRelic"].Visible = true;
                                cmbRelic.Visible = true;
                            }

                            restrictedIndexes.Clear();

                            cmbOption2.Visible = true;
                            panel.Controls["lblOption2"].Visible = true;
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Autoch-pattern Bolter",
                                "Concussion Gauntlet (+10 pts)",
                                "Ion Blaster",
                                "Plasma Axe (+5 pts)",
                                "Plasma Sword (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);

                            if(Stratagem.Count > 0)
                            {
                                antiLoop = false;
                                int tempIndex = cmbRelic.SelectedIndex;
                                cmbRelic.SelectedIndex = 0;
                                cmbRelic.SelectedIndex = tempIndex;
                                antiLoop = true;
                            }
                        }
                        else
                        {
                            cbOption1.Visible = true;
                            cbOption1.Enabled = true;
                            cbOption2.Visible = true;
                            cbOption2.Enabled = true;
                            cbOption3.Visible = true;
                            cbOption3.Enabled = true;

                            cmbOption1.Items.AddRange(new string[]
                            {
                                //"Autoch-pattern Bolter",
                                "EtaCarn Plasma Beamer (+10 pts)",
                                "HYLas Auto Rifle (+5 pts)",
                                //"Ion Blaster",
                                "L7 Missile Launcher (+10 pts)",
                                "Magna-rail Rifle (+20 pts)"
                            });

                            if (Weapons[0] == "Autoch-pattern Bolters")
                            {
                                cmbOption1.Items.Insert(0, "Autoch-pattern Bolter");
                            }
                            else if (Weapons[0] == "Ion Blasters (+1 pts/model)")
                            {
                                cmbOption1.Items.Insert(2, "Ion Blaster");
                            }

                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);

                            LoadOptions(cmbOption1);
                        }

                        antiLoop = false;
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

            if (Weapons[0] != "Autoch-pattern Bolters")
            {
                Points += UnitSize;
            }

            foreach(var weapon in Weapons)
            {
                if(weapon == "EtaCarn Plasma Beamer (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "EtaCarn Plasma Pistol (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "HYLas Auto Rifle (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Ion Pistol (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "L7 Missile Launcher (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Magna-rail Rifle (+20 pts)")
                {
                    Points += 20;
                }
                else if (weapon == "Concussion Gauntlet (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Plasma Axe (+5 pts)" || weapon == "Plasma Sword (+5 pts)")
                {
                    Points += 5;
                }
            }

            if (medpack) { Points += 5; }
            if (comms) { Points += 5; }
            if (scanner) { Points += 5; }
        }

        public override string ToString()
        {
            return "Hearthkyn Warriors - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1)
        {
            restrict = new int[4] { 0, 0, 0, 0 };
            restrictedIndexes.Clear();

            foreach(var weapon in Weapons)
            {
                if(weapon == "HYLas Auto Rifle (+5 pts)")
                {
                    restrict[0]++;
                }
                else if(weapon == "L7 Missile Launcher (+10 pts)")
                {
                    restrict[1]++;
                }
                else if(weapon == "EtaCarn Plasma Beamer (+10 pts)")
                {
                    restrict[2]++;
                }
                else if(weapon == "Magna-rail Rifle (+20 pts)")
                {
                    restrict[3]++;
                }
            }

            if (restrict[0] == UnitSize / 10 && Weapons[currentIndex + 2] != "HYLas Auto Rifle (+5 pts)")
            {
                restrictedIndexes.Add(cmbOption1.Items.IndexOf("HYLas Auto Rifle (+5 pts)"));
            }
            if (restrict[1] == UnitSize / 10 && Weapons[currentIndex + 2] != "L7 Missile Launcher (+10 pts)")
            {
                restrictedIndexes.Add(cmbOption1.Items.IndexOf("L7 Missile Launcher (+10 pts)"));
            }
            if (restrict[2] == UnitSize / 10 && Weapons[currentIndex + 2] != "EtaCarn Plasma Beamer (+10 pts)")
            {
                restrictedIndexes.Add(cmbOption1.Items.IndexOf("EtaCarn Plasma Beamer (+10 pts)"));
            }
            if (restrict[3] == UnitSize / 10 && Weapons[currentIndex + 2] != "Magna-rail Rifle (+20 pts)")
            {
                restrictedIndexes.Add(cmbOption1.Items.IndexOf("Magna-rail Rifle (+20 pts)"));
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }
}
