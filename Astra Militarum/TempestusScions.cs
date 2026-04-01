using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class TempestusScions : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;
        bool vox;
        int[] spWCount = new int[7];

        public TempestusScions()
        {
            DEFAULT_POINTS = 11;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            Weapons.Add("Hot-shot Laspistol");
            Weapons.Add("Chainsword");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Hot-shot Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "MILITARUM TEMPESTUS",
                "INFANTRY", "CORE", "PLATOON", "TEMPESTUS SCIONS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new TempestusScions();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Tempestor w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 2; i <= UnitSize; i++)
            {
                lbModelSelect.Items.Add("Tempestus Scion w/ " + Weapons[i]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Chainsword",
                "Power Fist (+5 pts)",
                "Power Sword"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

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
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
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
                            lbModelSelect.Items[currentIndex] = "Tempestor w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
                        }
                        else
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Tempestus Scion w/ " + Weapons[currentIndex + 1];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    break;
                case 12:
                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Tempestor w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (chosenRelic == "Legacy of Kalladius")
                    {
                        cmbOption2.SelectedIndex = 0;
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
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Hot-shot Lasgun");
                        lbModelSelect.Items.Add("Tempestus Scion w/ " + Weapons[temp + 1]);
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
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    else
                    {
                        isLoading = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
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

                            cmbOption2.Visible = true;
                            panel.Controls["lblOption2"].Visible = true;
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Bolt Pistol",
                                "Hot-shot Laspistol",
                                "Plasma Pistol",
                            });

                            if (Relic == "Legacy of Kalladius" || Relic == "Claw of the Desert Tigers")
                            {
                                cmbOption2.Enabled = false;
                            }
                            else if (Relic == "The Emperor's Fury")
                            {
                                cmbOption1.Enabled = false;
                            }
                        }
                        else
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Hot-shot Lasgun",
                                "Hot-shot Laspistol and Vox-caster",
                                "Hot-shot Volley Gun",
                                "Meltagun",
                                "Plasma Gun"
                            });

                            if (vox && !(Weapons[currentIndex + 1] == "Hot-shot Laspistol and Vox-caster"))
                            {
                                restrictedIndexes.Add(3);
                            }

                            if(UnitSize == 10)
                            {
                                if(specialW == 4 && Weapons[currentIndex + 1].Contains("Hot-shot Las"))
                                {
                                    restrictedIndexes.AddRange(new int[] { 0, 1, 4, 5, 6 });
                                }
                                else
                                {
                                    for (int i = 0; i < spWCount.Count(); i++)
                                    {
                                        //specialW += spWCount[i];
                                        if (spWCount[i] == 2 && (Weapons[currentIndex + 1] != (string)cmbOption1.Items[i]))
                                        {
                                            restrictedIndexes.Add(i);
                                        }
                                    }
                                }
                            }
                            else if (specialW == 2 && Weapons[currentIndex + 1].Contains("Hot-shot Las"))
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 1, 4, 5, 6 });
                            }
                            else
                            {
                                for (int i = 0; i < spWCount.Count(); i++)
                                {
                                    if (spWCount[i] == 1 && (Weapons[currentIndex + 1] != (string)cmbOption1.Items[i]))
                                    {
                                        restrictedIndexes.Add(i);
                                    }
                                }
                            }
                        }


                        if(currentIndex == 0)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
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

            Points = DEFAULT_POINTS * UnitSize;

            vox = false;
            specialW = 0;
            spWCount = new int[7];

            for(int i = 2; i < Weapons.Count; i++)
            {
                if (Weapons[i].Contains("Vox-caster"))
                {
                    vox = true;
                }

                if (!Weapons[i].Contains("Hot-shot Las") && currentIndex > 0)
                {
                    specialW++;
                    spWCount[cmbOption1.Items.IndexOf(Weapons[i])]++;
                }
            }

            if (Weapons[1] == "Power Fist (+5 pts)")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Tempestus Scions - " + Points + "pts";
        }
    }
}
