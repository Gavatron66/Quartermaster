using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Veterans : Datasheets
    {
        int currentIndex = 0;
        string[] HeavyWeapons = new string[]
        {
            "Grav-cannon",
            "Heavy Bolter",
            "Heavy Flamer",
            "Lascannon",
            "Missile Launcher",
            "Multi-melta",
            "Plasma Cannon"
        };

        public Veterans()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 2;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            Weapons.Add("");
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "COMMAND SQUAD", "COMPANY VETERANS"
            });
        }
        
        public override Datasheets CreateUnit()
        {
            return new Veterans();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 2;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 5;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[2] == "Combat Shield")
            {
                lbModelSelect.Items.Add("Company Veteran Sergeant w/ " + Weapons[0] + ", " + Weapons[1] + " and " + Weapons[2]);
            }
            else
            {
                lbModelSelect.Items.Add("Company Veteran Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i * 3) + 2] == "Combat Shield")
                {
                    lbModelSelect.Items.Add("Company Veteran w/ " + Weapons[i * 3] + ", " + Weapons[(i * 3) + 1] + " and " + Weapons[(i * 3) + 2]);
                }
                else
                {
                    lbModelSelect.Items.Add("Company Veteran w/ " + Weapons[i * 3] + " and " + Weapons[(i * 3) + 1]);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Boltgun",
                "Combi-flamer",
                "Combi-grav",
                "Combi-melta",
                "Combi-plasma",
                "Flamer",
                "Grav-gun",
                "Lightning Claw",
                "Melta Gun",
                "Plasma Gun",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Storm Bolter",
                "Storm Shield",
                "Thunder Hammer"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Grav-pistol",
                "Lightning Claw",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Storm Shield",
                "Thunder Hammer"
            });
            if (f.currentSubFaction == "Blood Angels" || f.currentSubFaction == "Deathwatch")
            {
                cmbOption2.Items.Insert(3, "Hand Flamer");
                cmbOption2.Items.Insert(4, "Inferno Pistol");
            }

            cbOption1.Text = "Combat Shield";
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 3] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Company Veteran Sergeant w/ " + Weapons[(currentIndex) * 3] + " and " + Weapons[(currentIndex * 3) + 1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Company Veteran w/ " + Weapons[(currentIndex) * 3] + " and " + Weapons[(currentIndex * 3) + 1];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 3) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Company Veteran Sergeant w/ " + Weapons[(currentIndex) * 3] + " and " + Weapons[(currentIndex * 3) + 1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Company Veteran w/ " + Weapons[(currentIndex) * 3] + " and " + Weapons[(currentIndex * 3) + 1];
                    }
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 3) + 2] = cbOption1.Text;
                        if(currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = ("Company Veteran Sergeant w/ " + Weapons[currentIndex * 3] + ", " + Weapons[(currentIndex * 3) + 1] + " and " + Weapons[(currentIndex * 3) + 2]);
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = ("Company Veteran w/ " + Weapons[currentIndex * 3] + ", " + Weapons[(currentIndex * 3) + 1] + " and " + Weapons[(currentIndex * 3) + 2]);
                        }
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 2] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Boltgun");
                        Weapons.Add("Bolt Pistol");
                        Weapons.Add("");
                        lbModelSelect.Items.Add("Company Veteran w/ " + Weapons[(UnitSize - 1) * 3] + " and " + Weapons[((UnitSize - 1) * 3) + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 3) - 1, 3);
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
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }
                    antiLoop = true;

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    if(repo.currentSubFaction == "Dark Angels")
                    {
                        cbOption1.Visible = true;
                    }

                    if(UnitSize == 5)
                    {
                        bool heavy = false;
                        int heavyIndex = -1;

                        for(int i = 0; i < 5; i++)
                        {
                            if (HeavyWeapons.Contains(Weapons[i * 3])) {
                                heavy = true;
                                heavyIndex = i;
                            }
                        }

                        if(!heavy || currentIndex == heavyIndex)
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Astartes Chainsword",
                                "Boltgun",
                                "Combi-flamer",
                                "Combi-grav",
                                "Combi-melta",
                                "Combi-plasma",
                                "Flamer",
                                "Grav-cannon",
                                "Grav-gun",
                                "Heavy Bolter",
                                //"Heavy Flamer",
                                "Lascannon",
                                "Lightning Claw",
                                "Melta Gun",
                                "Missile Launcher",
                                "Multi-melta",
                                "Plasma Cannon",
                                "Plasma Gun",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Bolter",
                                "Storm Shield",
                                "Thunder Hammer"
                            });
                            if (repo.currentSubFaction == "Blood Angels" || repo.currentSubFaction == "Deathwatch")
                            {
                                cmbOption1.Items.Insert(10, "Heavy Flamer");
                            }
                        }
                        else
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Astartes Chainsword",
                                "Boltgun",
                                "Combi-flamer",
                                "Combi-grav",
                                "Combi-melta",
                                "Combi-plasma",
                                "Flamer",
                                "Grav-gun",
                                "Lightning Claw",
                                "Melta Gun",
                                "Plasma Gun",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Bolter",
                                "Storm Shield",
                                "Thunder Hammer"
                            });
                        }
                    }
                    else
                    {
                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                                "Astartes Chainsword",
                                "Boltgun",
                                "Combi-flamer",
                                "Combi-grav",
                                "Combi-melta",
                                "Combi-plasma",
                                "Flamer",
                                "Grav-gun",
                                "Lightning Claw",
                                "Melta Gun",
                                "Plasma Gun",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Storm Bolter",
                                "Storm Shield",
                                "Thunder Hammer"
                        });
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 3) + 1]);
                    if (Weapons[(currentIndex * 3) + 2] != "")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                    }
                    antiLoop = false;

                    lbModelSelect.SelectedIndex = currentIndex;

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Combat Shield" || weapon == "Lightning Claw" || weapon == "Power Axe" ||
                    weapon == "Power Maul" || weapon == "Power Sword")
                {
                    Points += 3;
                }

                if(weapon == "Storm Shield")
                {
                    Points += 4;
                }

                if(weapon == "Flamer" || weapon == "Grav-pistol" || weapon == "Hand Flamer" ||
                    weapon == "Inferno Pistol" || weapon == "Plasma Pistol" || weapon == "Storm Bolter")
                {
                    Points += 5;
                }

                if(weapon == "Power Fist")
                {
                    Points += 8;
                }

                if(weapon == "Combi-flamer" || weapon == "Combi-grav" || weapon == "Combi-melta" || 
                    weapon == "Combi-plasma" || weapon == "Grav-cannon" || weapon == "Grav-gun" ||
                    weapon == "Heavy Bolter" || weapon == "Heavy Flamer" || weapon == "Meltagun" ||
                    weapon == "Plasma Gun")
                {
                    Points += 10;
                }

                if(weapon == "Thunder Hammer")
                {
                    Points += 12;
                }

                if(weapon == "Lascannon" || weapon == "Missile Launcher" || weapon == "Plasma Cannon")
                {
                    Points += 15;
                }

                if(weapon == "Multi-melta")
                {
                    Points += 20;
                }
            }
        }

        public override string ToString()
        {
            return "Company Veterans - " + Points + "pts";
        }
    }
}
