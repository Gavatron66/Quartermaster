using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class TempestusCommandSquad : Datasheets
    {
        int currentIndex;
        List<string> restrictionArray = new List<string>();

        public TempestusCommandSquad()
        {
            DEFAULT_POINTS = 95;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Hot-shot Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "MILITARUM TEMPESTUS",
                "INFANTRY", "CHARACTER", "OFFICER", "TEMPESTOR PRIME",
                "INFANTRY", "COMMAND SQUAD"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new TempestusCommandSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
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

            cbWarlord.Location = new System.Drawing.Point(cmbOption1.Location.X, cmbOption1.Location.Y + 32);
            panel.Controls["lblWarlord"].Location = new System.Drawing.Point(cbWarlord.Location.X, cbWarlord.Location.Y + 32);
            cmbWarlord.Location = new System.Drawing.Point(cmbOption1.Location.X, cbWarlord.Location.Y + 58);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbWarlord.Location.X, cmbWarlord.Location.Y + 32);
            cmbRelic.Location = new System.Drawing.Point(cmbOption1.Location.X, cmbWarlord.Location.Y + 58);

            lbModelSelect.Items.Clear();
            
            lbModelSelect.Items.Add("Tempestor Prime w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Tempestus Scion w/ " + Weapons[i]);
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
            if (antiLoop)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[0] = "Tempestor Prime w/ " + Weapons[0];
                        }
                        else
                        {
                            Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Tempestor Scion w/ " + Weapons[currentIndex];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
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

                    if (chosenRelic == "Clarion Proclamatus")
                    {
                        Weapons[1] = "Hot-shot Laspistol and Master Vox";
                        if (currentIndex == 1)
                        {
                            cmbOption1.SelectedIndex = 5;
                        }
                        else
                        {
                            lbModelSelect.Items[1] = "Tempestus Scion w/ " + Weapons[1];
                        }
                    }
                    else if (chosenRelic == "Finial of the Nemrodesh 1st")
                    {
                        Weapons[2] = "Hot-shot Lasgun and Regimental Standard";
                        if (currentIndex == 1)
                        {
                            cmbOption1.SelectedIndex = 4;
                        }
                        else
                        {
                            lbModelSelect.Items[1] = "Tempestus Scion w/ " + Weapons[1];
                        }
                    }
                    else if (chosenRelic == "The Emperor's Fury")
                    {
                        Weapons[0] = "Plasma Pistol (+5 pts)";
                        if (currentIndex == 0 && cmbOption1.Items.Count > 0)
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Tempestor Prime w/ " + Weapons[0];
                        }
                    }


                    Relic = chosenRelic;
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
                        panel.Controls["lblOption1"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Enabled = true;

                        if (currentIndex == 0)
                        {

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Bolt Pistol",
                                "Plasma Pistol (+5 pts)",
                                "Tempestus Command Rod (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                            cmbOption1.Enabled = true; 
                            if (Relic == "The Emperor's Fury")
                            {
                                cmbOption1.Enabled = false;
                            }

                            this.DrawItemWithRestrictions(new List<int>(), cmbOption1);
                        }
                        else
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Hot-shot Lasgun",
                                "Hot-shot Lasgun and Medi-pack",
                                "Hot-shot Lasgun and Regimental Standard",
                                "Hot-shot Laspistol and Master Vox",
                                "Hot-shot Volley Gun",
                                "Meltagun",
                                "Plasma Gun"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);

                            restrictedIndexes.Clear();
                            cmbOption1.Enabled = true;
                            if (currentIndex == 1 && Relic == "Clarion Proclamatus")
                            {
                                cmbOption1.Enabled = false;
                            }
                            else if (currentIndex == 1 && Relic == "Finial of the Nemrodesh 1st")
                            {
                                cmbOption1.Enabled = false;
                            }
                            else
                            {
                                foreach (var weapon in restrictionArray)
                                {
                                    if (weapon != Weapons[currentIndex])
                                    {
                                        if (weapon == "Master Vox" && !Weapons[currentIndex].Contains(weapon))
                                        {
                                            restrictedIndexes.Add(5);
                                        }
                                        else if (weapon == "Medi-pack" && !Weapons[currentIndex].Contains(weapon))
                                        {
                                            restrictedIndexes.Add(3);
                                        }
                                        else if (weapon == "Regimental Standard" && !Weapons[currentIndex].Contains(weapon))
                                        {
                                            restrictedIndexes.Add(4);
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

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Plasma Pistol (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Tempestus Command Rod (+5 pts)"))
            {
                Points += 5;
            }

            restrictionArray.Clear();
            for (int i = 1; i < Weapons.Count; i++)
            {
                if (Weapons[i] != "Hot-shot Lasgun")
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
            return "Militarum Tempestus Command Squad - " + Points + "pts";
        }
    }
}