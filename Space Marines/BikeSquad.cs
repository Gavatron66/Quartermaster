using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class BikeSquad : Datasheets
    {
        int currentIndex = 0;
        bool isLoading = false;
        int special;
        int attackIndex = -1;

        public BikeSquad()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m1k";
            Weapons.Add(""); // Attack Bike Squad?
            Weapons.Add(""); // Attack Bike Weapon
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Pistol");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "BIKER", "CORE", "BIKE SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new BikeSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            if(repo.currentSubFaction == "Space Wolves")
            {
                nudUnitSize.Maximum = 16;
            }
            else
            {
                nudUnitSize.Maximum = 8;
            }

            if(repo.currentSubFaction != "Space Wolves" && currentSize > 8)
            {
                nudUnitSize.Value = 8;
            }
            else
            {
                nudUnitSize.Value = currentSize;
            }

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Biker Sergeant w/ " + Weapons[2]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Space Marine Biker w/ " + Weapons[i + 2]);
            }

            cbOption1.Text = "Include an Attack Bike (+50 pts)";
            cbOption1.Location = new System.Drawing.Point(243, 59);
            if (Weapons[0] == "")
            {
                cbOption1.Checked = false;
            }
            else
            {
                cbOption1.Checked = true;
            }
            cbOption1.Visible = true;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(isLoading) { return; }
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    isLoading = true;
                    if(currentIndex == attackIndex)
                    {
                        Weapons[1] = cmbOption1.SelectedItem.ToString();
                    }
                    else
                    {
                        Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                    }
                    
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Biker Sergeant w/ " + Weapons[currentIndex + 2];
                    }
                    else if (currentIndex == attackIndex)
                    {
                        lbModelSelect.Items[attackIndex] = "Attack Bike w/ " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Space Marine Biker w/ " + Weapons[currentIndex + 2];
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = "Attack Bike";
                        if (Weapons[1] == "")
                        {
                            Weapons[1] = ("Heavy Bolter");
                        }
                        attackIndex = lbModelSelect.Items.Count;
                        lbModelSelect.Items.Add("Attack Bike w/ " + Weapons[1]);
                    }
                    else
                    {
                        Weapons[0] = "";
                        lbModelSelect.Items.Remove("Attack Bike w/ " + Weapons[1]);
                        Weapons[1] = "";
                    }
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Bolt Pistol");
                            lbModelSelect.Items.Add("Space Marine Biker w/ " + Weapons[temp + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(temp + 1, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;

                        if(currentIndex == 0)
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Astartes Chainsword",
                                "Bolt Pistol",
                                "Combi-flamer",
                                "Combi-grav",
                                "Combi-melta",
                                "Combi-plasma",
                                "Grav-pistol",
                                //Hand Flamer
                                //Inferno Pistol
                                "Lightning Claw",
                                "Plasma Pistol",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Bolter",
                                "Thunder Hammer"
                            });
                            if(repo.currentSubFaction == "Deathwatch" || repo.currentSubFaction == "Blood Angels")
                            {
                                cmbOption1.Items.Insert(7, "Hand Flamer");
                                cmbOption1.Items.Insert(8, "Inferno Pistol");
                            }
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[2]);
                        }
                        else if (currentIndex == attackIndex)
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Heavy Bolter",
                                "Multi-melta (+10 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);
                        }
                        else if(special < 2 || !(Weapons[currentIndex + 2] == "Bolt Pistol" || Weapons[currentIndex + 2] == "Astartes Chainsword")) {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Astartes Chainsword",
                                "Bolt Pistol",
                                "Flamer",
                                "Grav-gun",
                                "Meltagun",
                                "Plasma Gun"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);
                        }
                        else
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Astartes Chainsword",
                                "Bolt Pistol"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);
                        }
                    }
                    break;
            }

            isLoading = false;

            Points = DEFAULT_POINTS * UnitSize;
            special = 0;

            if(Weapons.Contains("Attack Bike"))
            {
                Points += 50;
            }

            if(Weapons.Contains("Multi-melta (+10 pts)"))
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Bike Squad - " + Points + "pts";
        }
    }
}
