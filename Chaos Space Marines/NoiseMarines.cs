using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class NoiseMarines : Datasheets
    {
        int currentIndex;
        bool icon;
        bool doomSiren;

        public NoiseMarines()
        {
            DEFAULT_POINTS = 21;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m2k";
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "INFANTRY", "CORE", "MARK OF CHAOS", "NOISE MARINES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new NoiseMarines();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 60);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 60);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (doomSiren)
            {
                lbModelSelect.Items.Add("Noise Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and Doom Siren");
            }
            else
            {
                lbModelSelect.Items.Add("Noise Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Noise Marine w/ " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Plasma Pistol (+5 pts)",
                "Power Axe (+5 pts)",
                "Power Fist (+10 pts)",
                "Power Maul (+5 pts)",
                "Power Sword (+5 pts)",
                "Sonic Blaster (+5 pts)",
                "Tainted Chainaxe (+5 pts)"
            });

            cbOption1.Text = "Doom Siren (+10 pts)";
            cbOption2.Text = "Icon of Slaanesh (+5 pts)";
            cbOption2.Visible = true;

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 30);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);

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
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[0] = cmbOption1.SelectedItem.ToString();
                            if (doomSiren)
                            {
                                lbModelSelect.Items[0] = "Noise Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and Doom Siren";
                            }
                            else
                            {
                                lbModelSelect.Items[0] = "Noise Champion w/ " + Weapons[0] + " and " + Weapons[1];
                            }
                        }
                        else
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Noise Marine w/ " + Weapons[currentIndex + 1];
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
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    if (doomSiren)
                    {
                        lbModelSelect.Items[0] = "Noise Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and Doom Siren";
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Noise Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (chosenRelic == "Hyper-Growth Bolts (Slot 1)" || chosenRelic == "Loyalty's Reward (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Hyper-Growth Bolts (Slot 2)" || chosenRelic == "Loyalty's Reward (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Ashen Axe (Slot 1)" || chosenRelic == "Axe of the Forgemaster (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Ashen Axe (Slot 2)" || chosenRelic == "Axe of the Forgemaster (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 3;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Viper's Spite" || chosenRelic == "The Warp's Malice")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Distortion (Slot 1)" || chosenRelic == "Blade of the Relentless (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 5;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Distortion (Slot 2)" || chosenRelic == "Blade of the Relentless (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 6;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "The Black Mace (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 4;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Black Mace (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 5;
                        cmbOption2.Enabled = false;
                    }

                    antiLoop = false;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        doomSiren = true;
                    }
                    else
                    {
                        doomSiren = false;
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
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
                        Weapons.Add("Boltgun");
                        lbModelSelect.Items.Add("Noise Marine w/ " + Weapons[temp + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize + 1, 1);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
                    }

                    antiLoop = true;
                    restrictedIndexes.Clear();

                    if(currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbStratagem5.Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

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
                            "Power Axe (+5 pts)",
                            "Power Fist (+10 pts)",
                            "Power Maul (+5 pts)",
                            "Power Sword (+5 pts)",
                            "Tainted Chainaxe (+5 pts)"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        restrictedIndexes.Clear();

                        #region Champion Relics
                        if (Relic == "Hyper-Growth Bolts (Slot 1)" || Relic == "Loyalty's Reward (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Hyper-Growth Bolts (Slot 2)" || Relic == "Loyalty's Reward (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Ashen Axe (Slot 1)" || Relic == "Axe of the Forgemaster (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 2;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Ashen Axe (Slot 2)" || Relic == "Axe of the Forgemaster (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 3;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Viper's Spite" || Relic == "The Warp's Malice")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Distortion (Slot 1)" || Relic == "Blade of the Relentless (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 5;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Distortion (Slot 2)" || Relic == "Blade of the Relentless (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 6;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "The Black Mace (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 4;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "The Black Mace (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 5;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Blastmaster (+15 pts)",
                            "Boltgun",
                            "Sonic Blaster (+5 pts)"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                        if(Weapons.Contains("Blastmaster (+15 pts)") && Weapons[currentIndex + 1] != "Blastmaster (+15 pts)")
                        {
                            restrictedIndexes.Add(1);
                        }
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1 );

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

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach(var weapon in Weapons)
            {
                if(weapon == "Plasma Pistol (+5 pts)" || weapon == "Sonic Blaster (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Blastmaster (+15 pts)")
                {
                    Points += 15;
                }
                else if (weapon.Contains("+5 pts"))
                {
                    Points += 5;
                }
                else if(weapon.Contains("+10 pts"))
                {
                    Points += 10;
                }
            }

            if(doomSiren) { Points += 10; }
            if(icon) { Points += 5; }
        }

        public override string ToString()
        {
            return "Noise Marines - " + Points + "pts";
        }
    }
}
