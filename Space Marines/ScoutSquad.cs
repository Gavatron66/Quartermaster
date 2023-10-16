using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class ScoutSquad : Datasheets
    {
        int currentIndex;
        public ScoutSquad()
        {
            DEFAULT_POINTS = 14;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            Weapons.Add("");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
                Weapons.Add("");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "SCOUT", "SMOKESCREEN", "SCOUT SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new ScoutSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[2] == "")
            {
                lbModelSelect.Items.Add("Scout Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            }
            else
            {
                lbModelSelect.Items.Add("Scout Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                    " and " + Weapons[2]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i * 2 ) + 2] != "")
                {
                    lbModelSelect.Items.Add("Scout w/ " + Weapons[(i * 2) + 1] + " and " + Weapons[(i * 2) + 2]);
                }
                else
                {
                    lbModelSelect.Items.Add("Scout w/ " + Weapons[(i * 2) + 1]);
                }
            }

            cbOption1.Text = "Camo Cloak";
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

            switch(code)
            {
                case 11:
                    Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items.Add("Scout Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
                        }
                        else
                        {
                            lbModelSelect.Items.Add("Scout Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                                " and " + Weapons[2]);
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 2) + 2] != "")
                        {
                            lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1] 
                            + " and " + Weapons[(currentIndex * 2) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1];
                        }
                    }
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    if (Weapons[2] == "")
                    {
                        lbModelSelect.Items.Add("Scout Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
                    }
                    else
                    {
                        lbModelSelect.Items.Add("Scout Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                            " and " + Weapons[2]);
                    }
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 2) + 2] = cbOption1.Text;
                        if(currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Scout Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                            " and " + Weapons[2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1]
                            + " and " + Weapons[(currentIndex * 2) + 2];
                        }
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 2] = "";
                        if(currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Scout Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Scout w/ " + Weapons[(currentIndex * 2) + 1];
                        }
                    }
                    break;
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = (Weapons.Count - 1) / 2;

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Boltgun");
                            Weapons.Add("");
                            lbModelSelect.Items.Add("Scout  w/ " + Weapons[(i * 2) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        for (int i = temp; i > UnitSize; i--)
                        {
                            lbModelSelect.Items.RemoveAt(i - 1);
                            Weapons.RemoveAt((i * 2));
                            Weapons.RemoveAt((i * 2) - 1);
                        }
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        antiLoop = false;
                        break;
                    }

                    if(currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Boltgun",
                            "Bolt Pistol",
                            "Combi-flamer",
                            "Combi-grav",
                            "Combi-melta",
                            "Combi-plasma",
                            "Grav-pistol",
                            "Lightning Claw",
                            "Plasma Pistol",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword",
                            "Storm Bolter",
                            "Thunder Hammer"
                        });
                        if (repo.currentSubFaction == "Blood Angels" || repo.currentSubFaction == "Deathwatch")
                        {
                            cmbOption1.Items.Insert(8, "Hand Flamer");
                            cmbOption1.Items.Insert(9, "Inferno Pistol");
                        }
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

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
                            "Thunder Hammer"
                        });
                        if (repo.currentSubFaction == "Blood Angels" || repo.currentSubFaction == "Deathwatch")
                        {
                            cmbOption2.Items.Insert(3, "Hand Flamer");
                            cmbOption2.Items.Insert(4, "Inferno Pistol");
                        }
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;

                        weaponsCheck(cmbOption1);
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }

                    if (Weapons[(currentIndex * 2) + 2] == "")
                    {
                        cbOption1.Checked = false;
                    }
                    else
                    {
                        cbOption1.Checked = true;
                    }

                    antiLoop = false;
                    break;
                default:break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Camo Cloak" || weapon == "Scout Sniper Rifle")
                {
                    Points += 2;
                }

                if(weapon == "Flamer" || weapon == "Grav-gun" || weapon == "Grav-pistol" ||
                    weapon == "Hand Flamer" || weapon == "Inferno Pistol" || weapon == "Lightning Claw" ||
                    weapon == "Plasma Pistol" || weapon == "Power Axe" || weapon == "Power Maul" ||
                    weapon == "Power Sword" || weapon == "Storm Bolter")
                {
                    Points += 5;
                }

                if(weapon == "Combi-flmaer" || weapon == "Combi-grav" || weapon == "Combi-melta" || 
                    weapon == "Combi-plasma" || weapon == "Heavy Bolter" || weapon == "Meltagun" ||
                    weapon == "Plasma Gun" || weapon == "Power Fist")
                {
                    Points += 10;
                }

                if(weapon == "Missile Launcher" || weapon == "Thunder Hammer")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Scout Squad - " + Points + "pts";
        }

        void weaponsCheck(ComboBox cmbOption1)
        {
            bool hasWeapon = false;

            if (Weapons[(currentIndex * 2) + 1] != "Astartes Shotgun" &&
                Weapons[(currentIndex * 2) + 1] != "Boltgun" &&
                Weapons[(currentIndex * 2) + 1] != "Combat Knife" &&
                Weapons[(currentIndex * 2) + 1] != "Scout Sniper Rifle")
            {
                hasWeapon = true;
            }
            if (
                !(Weapons.Contains("Heavy Bolter") || Weapons.Contains("Missile Launcher") || Weapons.Contains("Flamer")
                    || Weapons.Contains("Grav-gun") || Weapons.Contains("Meltagun") || Weapons.Contains("Plasma Gun")
                    || Weapons.Contains("Power Axe") || Weapons.Contains("Power Sword") || Weapons.Contains("Plasma Pistol"))
                || hasWeapon)
            {
                if(repo.currentSubFaction == "Space Wolves")
                {
                    cmbOption1.Items.Clear();
                    cmbOption1.Items.AddRange(new string[]
                    {
                        "Astartes Shotgun",
                        "Boltgun",
                        "Combat Knife",
                        "Flamer",
                        "Grav-gun",
                        "Heavy Bolter",
                        "Meltagun",
                        "Missile Launcher",
                        "Plasma Gun",
                        "Plasma Pistol",
                        "Power Axe",
                        "Power Sword",
                        "Scout Sniper Rifle"
                    });
                }
                else
                {
                    cmbOption1.Items.Clear();
                    cmbOption1.Items.AddRange(new string[]
                    {
                        "Astartes Shotgun",
                        "Boltgun",
                        "Combat Knife",
                        "Heavy Bolter",
                        "Missile Launcher",
                        "Scout Sniper Rifle"
                    });
                }
            }
            else
            {
                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Astartes Shotgun",
                    "Boltgun",
                    "Combat Knife",
                    "Scout Sniper Rifle"
                });
            }
        }
    }
}