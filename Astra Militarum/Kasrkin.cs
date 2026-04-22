using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Kasrkin : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;
        List<string> specialWeapons;
        int[] special;

        public Kasrkin()
        {
            DEFAULT_POINTS = 100;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Bolt Pistol");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Hot-shot Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "CADIAN",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "KASRKIN"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Kasrkin();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Kasrkin Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Kasrkin Trooper w/ " + Weapons[i + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
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
                                lbModelSelect.Items[currentIndex] = "Kasrkin Sergeant w/ " + Weapons[currentIndex];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Kasrkin Sergeant w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
                            }
                        }
                        else
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Kasrkin Trooper w/ " + Weapons[currentIndex + 1];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    break;
                case 12:
                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[1] == "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = "Kasrkin Sergeant w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Kasrkin Sergeant w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
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
                        cmbOption1.SelectedIndex = 2;
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
                                "Bolt Pistol",
                                "Hot-shot Laspistol",
                                "Plasma Pistol (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);
                        }
                        else
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Hot-shot Lasgun",
                                "Hot-shot Lasgun and Vox-caster",
                                "Hot-shot Laspistol and Melta Mine",
                                "Hot-shot Marksman Rifle",
                                "Hot-shot Volley Gun",
                                "Meltagun",
                                "Plasma Gun"
                            });

                            if (specialW == 4 && (Weapons[currentIndex + 1].Contains("Hot-shot Las") || Weapons[currentIndex + 1].Contains("Marksman")))
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 1, 6, 7, 8 });
                            }
                            else
                            {
                                if (special[0] == 2 && Weapons[currentIndex + 1] != (string)cmbOption1.Items[0])
                                {
                                    restrictedIndexes.Add(0);
                                }

                                if (special[1] == 2 && Weapons[currentIndex + 1] != (string)cmbOption1.Items[1])
                                {
                                    restrictedIndexes.Add(1);
                                }

                                if (special[6] == 2 && Weapons[currentIndex + 1] != (string)cmbOption1.Items[6])
                                {
                                    restrictedIndexes.Add(6);
                                }

                                if (special[7] == 2 && Weapons[currentIndex + 1] != (string)cmbOption1.Items[7])
                                {
                                    restrictedIndexes.Add(7);
                                }

                                if (special[8] == 2 && Weapons[currentIndex + 1] != (string)cmbOption1.Items[8])
                                {
                                    restrictedIndexes.Add(8);
                                }
                            }

                            if (special[3] == 1 && Weapons[currentIndex + 1] != (string)cmbOption1.Items[3])
                            {
                                restrictedIndexes.Add(3);
                            }

                            if (special[4] == 1 && Weapons[currentIndex + 1] != (string)cmbOption1.Items[4])
                            {
                                restrictedIndexes.Add(4);
                            }

                            if (special[5] == 1 && Weapons[currentIndex + 1] != (string)cmbOption1.Items[5])
                            {
                                restrictedIndexes.Add(5);
                            }

                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        }
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

            if (Weapons[1] == "Power Sword (+5 pts)")
            {
                Points += 5;
            }

            specialW = 0;
            special = new int[9];
            specialWeapons = new List<string>();

            if(currentIndex > 1)
            {
                foreach (var weapon in Weapons)
                {
                    if (weapon != Weapons[0] && weapon != Weapons[1])
                    {
                        if (weapon.Contains("Vox-caster"))
                        {
                            special[3]++;
                        }
                        else if (weapon.Contains("Marksman"))
                        {
                            special[5]++;
                        }
                        else if (weapon.Contains("Melta Mine"))
                        {
                            special[4]++;
                        }
                        else if (!weapon.Contains("Hot-shot Las"))
                        {
                            specialW++;
                            special[cmbOption1.Items.IndexOf(weapon)]++;
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Kasrkin - " + Points + "pts";
        }
    }
}
