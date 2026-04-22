using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class PlatoonCommandSquad : Datasheets
    {
        int currentIndex;
        List<string> restrictionArray = new List<string>();

        public PlatoonCommandSquad()
        {
            DEFAULT_POINTS = 15;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("Laspistol");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "CHARACTER", "OFFICER", "REGIMENTAL", "COMMANDER",
                "INFANTRY", "REGIMENTAL", "COMMAND SQUAD"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new PlatoonCommandSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            panel.Controls["lblWarlord"].Visible = true;
            panel.Controls["lblRelic"].Visible = true;
            cmbRelic.Visible = true;
            cbWarlord.Visible = true;
            cmbWarlord.Visible = true;

            cbWarlord.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 32);
            panel.Controls["lblWarlord"].Location = new System.Drawing.Point(cbWarlord.Location.X, cbWarlord.Location.Y + 32);
            cmbWarlord.Location = new System.Drawing.Point(cbOption1.Location.X, cbWarlord.Location.Y + 58);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbWarlord.Location.X, cmbWarlord.Location.Y + 32);
            cmbRelic.Location = new System.Drawing.Point(cbOption1.Location.X, cmbWarlord.Location.Y + 58);

            lbModelSelect.Items.Clear();

            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Platoon Commander w/ " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Platoon Commander w/ " + Weapons[0] + " and " + Weapons[1]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[i+1] == "VHWT")
                {
                    lbModelSelect.Items.Add("Veteran Heavy Weapons Team w/ " + Weapons[i + 2]);
                    i += 5;
                }
                else
                {
                    lbModelSelect.Items.Add("Veteran Guardsman w/ " + Weapons[i + 1]);
                }
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Chainsword",
                "Power Fist (+5 pts)",
                "Power Sword"
            });

            cbOption1.Visible = true;
            cbOption1.Text = "Include a Veteran Heavy Weapons Team";

            if (Weapons[4] == "VHWT")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
                cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (Relic != null && cmbRelic.Items.Contains(Relic))
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = 0;
            }

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            cbStratagem1.Visible = true;
            cbStratagem1.Location = new System.Drawing.Point(cmbRelic.Location.X, cmbRelic.Location.Y + 32);
            cbStratagem2.Visible = true;
            cbStratagem2.Location = new System.Drawing.Point(cbStratagem1.Location.X, cbStratagem1.Location.Y + 32);

            if (Stratagem.Contains(cbStratagem1.Text))
            {
                cbStratagem1.Checked = true;
                cbStratagem1.Enabled = true;
            }
            else
            {
                cbStratagem1.Checked = false;
                cbStratagem1.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem1.Text));
            }

            if (Stratagem.Contains(cbStratagem2.Text))
            {
                cbStratagem2.Checked = true;
                cbStratagem2.Enabled = true;
            }
            else
            {
                cbStratagem2.Checked = false;
                cbStratagem2.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem2.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else if (currentIndex == 3 && Weapons[4] == "VHWT")
                    {
                        Weapons[5] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[3] = "Veteran Heavy Weapons Team w/ " + Weapons[5];
                    }
                    else
                    {
                        if(!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Veteran Guardsman w/ " + Weapons[currentIndex + 1];
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        }
                    }
                    break;
                case 12:
                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[1] == "(None)")
                    {
                        lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 15:
                    if (cmbWarlord.SelectedIndex != -1)
                    {
                        WarlordTrait = cmbWarlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (chosenRelic == "Legacy of Kalladius")
                    {
                        Weapons[1] = "Chainsword";
                        if (currentIndex == 0)
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0];
                        }
                    }
                    else if (chosenRelic == "Claw of the Desert Tigers")
                    {
                        Weapons[1] = "Power Sword";
                        if (currentIndex == 0)
                        {
                            cmbOption2.SelectedIndex = 3;
                            cmbOption2.Enabled = false;
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0];
                        }
                    }
                    else if (chosenRelic == "Clarion Proclamatus")
                    {
                        Weapons[2] = "Lasgun and Master Vox";
                        if(currentIndex == 1)
                        {
                            cmbOption1.SelectedIndex = 8;
                        }
                        else
                        {
                            lbModelSelect.Items[1] = "Veteran Guardsman w/ " + Weapons[2];
                        }
                    }
                    else if (chosenRelic == "Finial of the Nemrodesh 1st")
                    {
                        Weapons[2] = "Lasgun and Regimental Standard";
                        if (currentIndex == 1)
                        {
                            cmbOption1.SelectedIndex = 10;
                        }
                        else
                        {
                            lbModelSelect.Items[1] = "Veteran Guardsman w/ " + Weapons[2];
                        }
                    }
                    else if (chosenRelic == "The Emperor's Fury")
                    {
                        Weapons[0] = "Plasma Pistol (+5 pts)";
                        if (currentIndex == 0 && cmbOption1.Items.Count > 0)
                        {
                            cmbOption1.SelectedIndex = 3;
                            cmbOption1.Enabled = false;
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0];
                        }
                    }

                    Relic = chosenRelic;
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        if (Weapons.Contains("VHWT"))
                        {
                            break;
                        }

                        Weapons[4] = "VHWT";
                        Weapons[5] = "Heavy Bolter";
                        lbModelSelect.Items.RemoveAt(4);

                        lbModelSelect.Items[3] = "Veteran Heavy Weapons Team w/ " + Weapons[5];
                    }
                    else
                    {
                        if(!Weapons.Contains("VHWT"))
                        {
                            break;
                        }

                        Weapons[4] = "Lasgun";
                        Weapons[5] = "Lasgun";
                        lbModelSelect.Items.RemoveAt(3);

                        lbModelSelect.Items.Add("Veteran Guardsman w/ " + Weapons[4]);
                        lbModelSelect.Items.Add("Veteran Guardsman w/ " + Weapons[5]);
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
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
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        if (currentIndex == 0)
                        {
                            cmbOption2.Visible = true;
                            panel.Controls["lblOption2"].Visible = true;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Bolt Pistol",
                                "Boltgun",
                                "Laspistol",
                                "Plasma Pistol (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                            cmbOption1.Enabled = true;
                            cmbOption2.Enabled = true;

                            if (Relic == "Legacy of Kalladius" || Relic == "Claw of the Desert Tigers")
                            {
                                cmbOption2.Enabled = false;
                            }
                            else if(Relic == "The Emperor's Fury")
                            {
                                cmbOption1.Enabled = false;
                            }

                            this.DrawItemWithRestrictions(new List<int>(), cmbOption1);
                        }
                        else if (currentIndex == 3 && Weapons[4] == "VHWT")
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Autocannon",
                                "Heavy Bolter",
                                "Lascannon",
                                "Missile Launcher",
                                "Mortar"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[5]);
                            this.DrawItemWithRestrictions(new List<int>(), cmbOption1);
                        }
                        else
                        {
                            cmbOption2.Visible = false;
                            panel.Controls["lblOption2"].Visible = false;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Chainsword",
                                "Chainsword and Master Vox",
                                "Chainsword and Medi-pack",
                                "Chainsword and Regimental Standard",
                                "Flamer",
                                "Grenade Launcher",
                                "Heavy Flamer",
                                "Lasgun",
                                "Lasgun and Master Vox",
                                "Lasgun and Medi-pack",
                                "Lasgun and Regimental Standard",
                                "Meltagun",
                                "Plasma Gun",
                                "Sniper Rifle"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                            restrictedIndexes.Clear();
                            if(currentIndex == 1 && Relic == "Clarion Proclamatus")
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 2, 3, 4, 5, 6, 7, 9, 10, 11, 12, 13 });
                            }
                            else if(currentIndex == 1 && Relic == "Finial of the Nemrodesh 1st")
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 1, 2, 4, 5, 6, 7, 8, 9, 11, 12, 13 });
                            }
                            else
                            {
                                foreach (var weapon in restrictionArray)
                                {
                                    if (weapon != Weapons[currentIndex + 1])
                                    {
                                        if (weapon == "Master Vox" && !Weapons[currentIndex + 1].Contains(weapon))
                                        {
                                            restrictedIndexes.AddRange(new int[] { 1, 8 });
                                        }
                                        else if (weapon == "Medi-pack" && !Weapons[currentIndex + 1].Contains(weapon))
                                        {
                                            restrictedIndexes.AddRange(new int[] { 2, 9 });
                                        }
                                        else if (weapon == "Regimental Standard" && !Weapons[currentIndex + 1].Contains(weapon))
                                        {
                                            restrictedIndexes.AddRange(new int[] { 3, 10 });
                                        }
                                        else
                                        {
                                            restrictedIndexes.Add(cmbOption1.Items.IndexOf(weapon));
                                        }
                                    }
                                }
                            }

                            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        }
                    }

                    antiLoop = false;
                    break;
                case 71:
                    if (cbStratagem1.Checked)
                    {
                        Stratagem.Add(cbStratagem1.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem1.Text))
                        {
                            Stratagem.Remove(cbStratagem1.Text);
                        }
                    }
                    break;
                case 72:
                    if (cbStratagem2.Checked)
                    {
                        Stratagem.Add(cbStratagem2.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem2.Text))
                        {
                            Stratagem.Remove(cbStratagem2.Text);
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if(Weapons.Contains("Plasma Pistol (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Power Fist (+5 pts)"))
            {
                Points += 5;
            }

            restrictionArray.Clear();
            for(int i = 2; i < Weapons.Count; i++)
            {
                if (Weapons[i] != "Lasgun" && Weapons[i] != "Chainsword")
                {
                    if (Weapons[i].Contains("Master Vox"))
                    {
                        restrictionArray.Add("Master Vox");
                    }
                    else if (Weapons[i].Contains("Medi-pack"))
                    {
                        restrictionArray.Add("Medi-pack");
                    }
                    else if (Weapons[i].Contains("Regimental Standard"))
                    {
                        restrictionArray.Add("Regimental Standard");
                    }
                    else
                    {
                        restrictionArray.Add(Weapons[i]);
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Platoon Command Squad - " + Points + "pts";
        }
    }
}