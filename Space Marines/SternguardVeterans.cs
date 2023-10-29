using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class SternguardVeterans : Datasheets
    {
        int currentIndex;
        public SternguardVeterans()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Bolt Pistol");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Special Issue Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "STERNGUARD VETERAN SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new SternguardVeterans();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Sternguard Veteran Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Sternguard Veteran w/ " + Weapons[i + 1]);
            }

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
            });
            if (f.currentSubFaction == "Blood Angels" || f.currentSubFaction == "Deathwatch")
            {
                cmbOption1.Items.Insert(3, "Hand Flamer");
                cmbOption1.Items.Insert(4, "Inferno Pistol");
            }
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[0]);
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

            switch (code)
            {
                case 11:
                    Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Sternguard Veteran Sergeant w/" + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Sternguard Veteran w/ " + Weapons[currentIndex + 1];
                    }
                    break;
                case 12:
                    Weapons[0] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Sternguard Veteran Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    break;
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = Weapons.Count - 2;

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Special Issue Boltgun");
                            lbModelSelect.Items.Add("Sternguard Veteran w/ " + Weapons[i]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        for (int i = temp; i > UnitSize; i--)
                        {
                            lbModelSelect.Items.RemoveAt(i - 1);
                            Weapons.RemoveAt(i - 1);
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
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    if (currentIndex == 0)
                    {
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[0]);

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
                            "Lightning Claw",
                            "Special Issue Boltgun",
                            "Plasma Pistol",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword",
                            "Storm Bolter",
                        });
                        if (repo.currentSubFaction == "Blood Angels")
                        {
                            cmbOption1.Items.Insert(7, "Hand Flamer");
                            cmbOption1.Items.Insert(8, "Inferno Pistol");
                        }
                    }
                    else
                    {
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;

                        weaponsCheck(cmbOption1 as ComboBox);
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                    antiLoop = false;
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;
            foreach (string item in Weapons)
            {
                if(item == "Storm Bolter")
                {
                    Points += 3;
                }

                if(item == "Combi-flamer" || item == "Combi-grav" || item == "Combi-melta"
                    || item == "Combi-plasma" || item == "Flamer" || item == "Grav-pistol"
                    || item == "Hand Flamer" || item == "Inferno Pistol" || item == "Lightning Claw"
                    || item == "Plasma Pistol" || item == "Power Axe" || item == "Plasma Maul"
                    || item == "Power Sword")
                {
                    Points += 5;
                }

                if(item == "Grav-cannon" || item == "Grav-gun" || item == "Heavy Bolter"
                    || item == "Heavy Flamer" || item == "Meltagun" || item == "Plasma Gun"
                    || item == "Power Fist")
                {
                    Points += 10;
                }

                if(item == "Lascannon" || item == "Missile Launcher" || item == "Plasma Cannon")
                {
                    Points += 15;
                }

                if(item == "Multi-melta")
                {
                    Points += 20;
                }
            }
        }

        public override string ToString()
        {
            return "Sternguard Veteran Squad - " + Points + "pts";
        }

        private void weaponsCheck(ComboBox cmbOption1)
        {
            int restrict = 0;
            string[] restrictArray = new string[]
            {
                "Grav-cannon",
                "Heavy Bolter",
                "Heavy Flamer",
                "Lascannon",
                "Missile Launcher",
                "Multi-melta",
                "Plasma Cannon",
                "Flamer",
                "Grav-gun",
                "Meltagun",
                "Plasma Gun"
            };

            foreach (string weapon in Weapons)
            {
                if(restrictArray.Contains(weapon))
                {
                    restrict++;
                }
                if(restrict == 2)
                {
                    break;
                }
            }

            if(restrict == 2 && !restrictArray.Contains(Weapons[currentIndex + 1]))
            {
                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Combi-flamer",
                    "Combi-grav",
                    "Combi-melta",
                    "Combi-plasma",
                    "Special Issue Boltgun",
                    "Storm Bolter"
                });
            }
            else
            {
                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Combi-flamer",
                    "Combi-grav",
                    "Combi-melta",
                    "Combi-plasma",
                    "Flamer",
                    "Grav-cannon",
                    "Grav-gun",
                    "Heavy Bolter",
                    "Heavy Flamer",
                    "Lascannon",
                    "Meltagun",
                    "Missile Launcher",
                    "Multi-melta",
                    "Plasma Cannon",
                    "Plasma Gun",
                    "Special Issue Boltgun",
                    "Storm Bolter"
                });
            }
        }
    }
}
