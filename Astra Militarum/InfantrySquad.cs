using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class InfantrySquad : Datasheets
    {
        int currentIndex;
        bool vox;
        bool specialWeapon;

        public InfantrySquad()
        {
            DEFAULT_POINTS = 65;
            UnitSize = 10;
            Points =  DEFAULT_POINTS;
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
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "INFANTRY SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new InfantrySquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();

            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Sergeant w/ " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[i + 1] == "HWT")
                {
                    lbModelSelect.Items.Add("Heavy Weapons Team w/ " + Weapons[i + 2]);
                    i += 5;
                }
                else
                {
                    lbModelSelect.Items.Add("Guardsman w/ " + Weapons[i + 1]);
                }
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Chainsword",
                "Power Sword (+5 pts)"
            });

            cbOption1.Visible = true;
            cbOption1.Text = "Include a Heavy Weapons Team";

            if (Weapons[9] == "HWT")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["cbOption1"].Location.X, panel.Controls["cbOption1"].Location.Y + 32);
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Sergeant w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else if (currentIndex == 8 && Weapons[9] == "HWT")
                    {
                        Weapons[10] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[8] = "Heavy Weapons Team w/ " + Weapons[10];
                    }
                    else
                    {
                        if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Guardsman w/ " + Weapons[currentIndex + 1];
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
                        lbModelSelect.Items[0] = "Sergeant w/ " + Weapons[0];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (chosenRelic == "Legacy of Kalladius")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Claw of the Desert Tigers")
                    {
                        cmbOption2.SelectedIndex = 2;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "The Emperor's Fury")
                    {
                        cmbOption1.SelectedIndex = 3;
                        cmbOption1.Enabled = false;
                    }

                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        if (Weapons.Contains("HWT"))
                        {
                            break;
                        }

                        Weapons[9] = "HWT";
                        Weapons[10] = "Heavy Bolter";
                        lbModelSelect.Items.RemoveAt(9);

                        lbModelSelect.Items[8] = "Heavy Weapons Team w/ " + Weapons[10];
                    }
                    else
                    {
                        if (!Weapons.Contains("HWT"))
                        {
                            break;
                        }

                        Weapons[9] = "Lasgun";
                        Weapons[10] = "Lasgun";
                        lbModelSelect.Items.RemoveAt(9);

                        lbModelSelect.Items.Add("Guardsman w/ " + Weapons[9]);
                        lbModelSelect.Items.Add("Guardsman w/ " + Weapons[10]);
                    }
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
                        cbStratagem5.Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

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

                            if (Relic == "Legacy of Kalladius" || Relic == "Claw of the Desert Tigers")
                            {
                                cmbOption2.Enabled = false;
                            }
                            else if (Relic == "The Emperor's Fury")
                            {
                                cmbOption1.Enabled = false;
                            }

                            this.DrawItemWithRestrictions(new List<int>(), cmbOption1);
                        }
                        else if (currentIndex == 8 && Weapons[9] == "HWT")
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
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[10]);
                            this.DrawItemWithRestrictions(new List<int>(), cmbOption1);
                        }
                        else
                        {
                            cmbOption2.Visible = false;
                            panel.Controls["lblOption2"].Visible = false;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Lasgun",
                                "Lasgun and Vox-caster",
                                "Meltagun",
                                "Plasma Gun",
                                "Sniper Rifle"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                            restrictedIndexes.Clear();

                            if(vox && Weapons[currentIndex + 1] != "Lasgun and Vox-caster")
                            {
                                restrictedIndexes.Add(3);
                            }

                            if(specialWeapon && Weapons[currentIndex + 1].Contains("Lasgun"))
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 1, 4, 5, 6 });
                            }

                            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        }
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

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Plasma Pistol (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Power Sword (+5 pts)"))
            {
                Points += 5;
            }

            vox = false;
            specialWeapon = false;
            for (int i = 2; i < Weapons.Count; i++) 
            {
                if (Weapons[i] == "HWT")
                {
                    i += 5;
                }
                else
                {
                    if (Weapons[i] == "Lasgun and Vox-caster")
                    {
                        vox = true;
                    }

                    if (!Weapons[i].Contains("Lasgun"))
                    {
                        specialWeapon = true;
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Infantry Squad - " + Points + "pts";
        }
    }
}