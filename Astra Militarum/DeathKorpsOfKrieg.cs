using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class DeathKorpsOfKrieg : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        bool mediPack;
        List<string> specialWeapons;
        bool sgtBoltgun = false;

        public DeathKorpsOfKrieg()
        {
            DEFAULT_POINTS = 75;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Laspistol");
            Weapons.Add("Chainsword");
            Weapons.Add("Plasma Gun");
            for (int i = 2; i < UnitSize; i++)
            {
                Weapons.Add("Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "KRIEG",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "INFANTRY SQUAD", "DEATH KORPS OF KRIEG"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathKorpsOfKrieg();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Death Korps Watchmaster w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Death Korps Trooper w/ " + Weapons[i + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Chainsword",
                "Power Sword (+5 pts)"
            });

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption2"].Location.X, panel.Controls["cmbOption2"].Location.Y + 32);
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
            if (isLoading)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                            if (Weapons[1] == "(None)")
                            {
                                lbModelSelect.Items[currentIndex] = "Death Korps Watchmaster w/ " + Weapons[currentIndex];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Death Korps Watchmaster w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
                            }

                            if (Weapons[currentIndex] == "Boltgun")
                            {
                                sgtBoltgun = true;
                                this.DrawItemWithRestrictions(new List<int> { 1, 2, 4 }, cmbRelic);
                                cmbOption2.Enabled = false;
                                cmbOption2.SelectedIndex = -1;
                            }
                            else
                            {
                                sgtBoltgun = false;
                                this.DrawItemWithRestrictions(new List<int> { }, cmbRelic);
                                cmbOption2.Enabled = true;
                                cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                            }
                        }
                        else
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Death Korps Trooper w/ " + Weapons[currentIndex + 1];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    break;
                case 12:
                    if(cmbOption2.SelectedIndex == -1)
                    {
                        break;
                    }

                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[1] == "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = "Death Korps Watchmaster w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Death Korps Watchmaster w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if(sgtBoltgun)
                    {
                        cmbOption2.Enabled = false;

                        if(chosenRelic != "The Barbicant's Key" && chosenRelic != "(None)")
                        {
                            chosenRelic = Relic;
                            cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        }
                    } 

                    if (chosenRelic == "Legacy of Kalladius")
                    {
                        cmbOption2.SelectedIndex = 0;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Claw of the Desert Tigers")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "The Emperor's Fury")
                    {
                        cmbOption1.SelectedIndex = 3;
                        cmbOption1.Enabled = false;
                    }

                    Relic = chosenRelic;
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
                    }
                    else
                    {
                        isLoading = true;
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;


                        restrictedIndexes.Clear();
                        cmbOption1.Items.Clear();
                        if (currentIndex == 0)
                        {
                            cmbOption2.Visible = true;
                            panel.Controls["lblOption2"].Visible = true;
                            cbStratagem5.Visible = true;

                            if (Stratagem.Contains(cbStratagem5.Text))
                            {
                                panel.Controls["lblRelic"].Visible = true;
                                cmbRelic.Visible = true;
                            }

                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Boltgun",
                                "Bolt Pistol",
                                "Laspistol",
                                "Plasma Pistol (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                            if (Relic == "Legacy of Kalladius" || Relic == "Claw of the Desert Tigers")
                            {
                                cmbOption2.Enabled = false;
                            }
                            else if (Relic == "The Emperor's Fury")
                            {
                                cmbOption1.Enabled = false;
                            }
                        }
                        else if (currentIndex == 1)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Lasgun and Vox-caster",
                                "Plasma Gun"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        }
                        else
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Lasgun",
                                "Lasgun and Medi-pack (+5 pts)",
                                "Meltagun"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                            if (specialWeapons.Count == 2 && Weapons[currentIndex + 1].Contains("Lasgun"))
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 1, 4, 5 });
                            }
                            else if (specialWeapons.Count == 2)
                            {
                                if (specialWeapons.Contains(Weapons[currentIndex + 1]))
                                {
                                    restrictedIndexes.Remove(cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]));
                                }

                                if (specialWeapons[0] == Weapons[currentIndex + 1])
                                {
                                    restrictedIndexes.Add(cmbOption1.Items.IndexOf(specialWeapons[1]));
                                }
                                else if (specialWeapons[1] == Weapons[currentIndex + 1])
                                {
                                    restrictedIndexes.Add(cmbOption1.Items.IndexOf(specialWeapons[0]));
                                }
                            }
                            else if (specialWeapons.Count == 1 && Weapons[currentIndex + 1] != specialWeapons[0])
                            {
                                restrictedIndexes.Add(cmbOption1.Items.IndexOf(specialWeapons[0]));
                            }
                        }

                        if (mediPack && !(Weapons[currentIndex + 1] == "Lasgun and Medi-pack (+5 pts)"))
                        {
                            restrictedIndexes.Add(cmbOption1.Items.IndexOf("Lasgun and Medi-pack (+5 pts)"));
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        isLoading = false;
                        break;
                    }
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

            Points = DEFAULT_POINTS;
            if (Weapons[0] == "Plasma Pistol (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[1] == "Power Sword (+5 pts)" )
            {
                Points += 5;
            }

            mediPack = false;
            specialWeapons = new List<string>();

            foreach (var weapon in Weapons)
            {
                if (weapon != Weapons[0] && weapon != Weapons[1])
                {
                    if (weapon.Contains("Medi-pack"))
                    {
                        mediPack = true;
                        Points += 5;
                    }

                    if (!weapon.Contains("Lasgun") && !weapon.Contains("Plasma"))
                    {
                        specialWeapons.Add(weapon);
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Death Korps of Krieg - " + Points + "pts";
        }
    }
}
