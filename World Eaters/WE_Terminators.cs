using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class WE_Terminators : Datasheets
    {
        int currentIndex;
        List<int> restrictedIndexes2 = new List<int>();
        int[] restrict = new int[] { 0, 0, 0, 0, 0, 0, 0 };

        public WE_Terminators()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Combi-bolter");
                Weapons.Add("Accursed Weapon ");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "BUTCHER ASTARTES", "WORLD EATERS",
                "INFANTRY", "CORE", "TERMINATOR", "TERMINATOR SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new WE_Terminators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as WorldEaters;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("World Eaters Terminator Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("World Eaters Terminator w/ " + Weapons[(i * 2)] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Accursed Weapon ",
                "Chainfist (+5 pts)",
                "Power Fist (+5 pts)"
            });
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "World Eaters Terminator Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "World Eaters Terminator w/ " + Weapons[(currentIndex * 2)]
                                + " and " + Weapons[(currentIndex * 2) + 1];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    }

                    if (Weapons[currentIndex * 2] == "Accursed Weapon ")
                    {
                        LoadOptions(cmbOption1, cmbOption2);
                    }
                    break;
                case 12:
                    if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "World Eaters Terminator Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "World Eaters Terminator w/ " + Weapons[(currentIndex * 2)]
                                + " and " + Weapons[(currentIndex * 2) + 1];
                        }
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Combi-bolter");
                            Weapons.Add("Accursed Weapon");
                            lbModelSelect.Items.Add("World Eaters Terminator w/ " + Weapons[(currentIndex * 2)]
                                + " and " + Weapons[(currentIndex * 2) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 2) + 1, 2);
                    }
                    break;
                case 61:
                    if (antiLoop)
                    {
                        break;
                    }

                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Accursed Weapon",
                            "Combi-bolter",
                            "Combi-flamer (+5 pts)",
                            "Combi-melta (+5 pts)",
                            "Combi-plasma (+5 pts)",
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2)]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        LoadOptions(cmbOption1, cmbOption2);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Accursed Weapon",
                            "Combi-bolter",
                            "Combi-flamer (+5 pts)",
                            "Combi-melta (+5 pts)",
                            "Combi-plasma (+5 pts)",
                            "Heavy Flamer (+5 pts)",
                            "Reaper Autocannon (+5 pts)"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2)]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        LoadOptions(cmbOption1, cmbOption2);
                    }

                    antiLoop = false;
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (var weapon in Weapons)
            {
                if (weapon == "Combi-flamer (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Combi-melta (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Combi-plasma (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Heavy Flamer (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Reaper Autocannon (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Chainfist (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Power Fist (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "World Eaters Terminator Squad - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1, ComboBox cmbOption2)
        {
            restrictedIndexes.Clear();

            restrict = new int[] { 0, 0, 0, 0, 0, 0, 0 };

            foreach (var item in Weapons)
            {
                if (item == "Reaper Autocannon (+5 pts)" || item == "Heavy Flamer (+5 pts)")
                {
                    restrict[0]++;
                }
                else if (item == "Combi-plasma (+5 pts)")
                {
                    restrict[1]++;
                }
                else if (item == "Accursed Weapon")
                {
                    restrict[2]++;
                }
                else if (item == "Combi-flamer (+5 pts)")
                {
                    restrict[3]++;
                }
                else if (item == "Combi-melta (+5 pts)")
                {
                    restrict[4]++;
                }
                else if (item == "Power Fist (+5 pts)")
                {
                    restrict[5]++;
                }
                else if (item == "Chainfist (+5 pts)")
                {
                    restrict[6]++;
                }
            }

            if (restrict[0] == UnitSize / 5 && Weapons[currentIndex * 2] != "Reaper Autocannon (+5 pts)" && Weapons[currentIndex * 2] != "Heavy Flamer (+5 pts)")
            {
                restrictedIndexes.Add(5);
                restrictedIndexes.Add(6);
            }
            if (restrict[1] == UnitSize / 5 && Weapons[currentIndex * 2] != "Combi-plasma (+5 pts)")
            {
                restrictedIndexes.Add(4);
            }
            if (restrict[2] == UnitSize / 5 && Weapons[currentIndex * 2] != "Accursed Weapon")
            {
                restrictedIndexes.Add(0);
            }
            if (restrict[3] == (UnitSize / 5) * 2 && Weapons[currentIndex * 2] != "Combi-flamer (+5 pts)")
            {
                restrictedIndexes.Add(2);
            }
            if (restrict[4] == (UnitSize / 5) * 2 && Weapons[currentIndex * 2] != "Combi-melta (+5 pts)")
            {
                restrictedIndexes.Add(3);
            }

            //Champion Relics
            if (currentIndex == 0)
            {
                if (Relic == "Hyper-Growth Bolts" || Relic == "Spitespitter" || Relic == "Loyalty's Reward")
                {
                    cmbOption1.SelectedIndex = 1;
                    restrictedIndexes.Add(0);
                }
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

            restrictedIndexes2.Clear();

            if (restrict[5] == (UnitSize / 5) * 3 && (Weapons[(currentIndex * 2) + 1] != "Power Fist (+5 pts)") || Weapons[currentIndex * 2] == "Accursed Weapon")
            {
                restrictedIndexes2.Add(2);
            }
            if (restrict[6] == UnitSize / 5 && (Weapons[(currentIndex * 2) + 1] != "Chainfist (+5 pts)") || Weapons[currentIndex * 2] == "Accursed Weapon")
            {
                restrictedIndexes2.Add(1);
            }

            this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
        }
    }
}