using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class ChaosTerminators : Datasheets
    {
        int currentIndex;
        List<int> restrictedIndexes2 = new List<int>();

        public ChaosTerminators()
        {
            DEFAULT_POINTS = 36;
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
                "CHAOS", "HERETIC ASTARTES", "TRAITORS ASTARTES", "CHAOS UNDIVDED", "<LEGION>",
                "INFANTRY", "CORE", "TERMINATOR", "CHAOS TERMINATOR SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new ChaosTerminators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
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
            lbModelSelect.Items.Add("Terminator Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Chaos Terminator w/ " + Weapons[(i * 2)] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Accursed Weapon ",
                "Chainfist",
                "Power Fist"
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
                            lbModelSelect.Items[0] = "Terminator Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Chaos Terminator w/ " + Weapons[(currentIndex * 2)]
                                + " and " + Weapons[(currentIndex * 2) + 1];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    }

                    if(Weapons[currentIndex * 2] == "Accursed Weapon ") {
                        LoadOptions(cmbOption1, cmbOption2);
                    }
                    break;
                case 12:
                    if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Terminator Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Chaos Terminator w/ " + Weapons[(currentIndex * 2)]
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
                            lbModelSelect.Items.Add("Chaos Terminator w/ " + Weapons[(currentIndex * 2)]
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

                    if(currentIndex < 0)
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

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Accursed Weapon",
                            "Combi-bolter",
                            "Combi-flamer",
                            "Combi-melta",
                            "Combi-plasma",
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

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Accursed Weapon",
                            "Combi-bolter",
                            "Combi-flamer",
                            "Combi-melta",
                            "Combi-plasma",
                            "Heavy Flamer",
                            "Reaper Autocannon"
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
        }

        public override string ToString()
        {
            return "Chaos Terminator Squad - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1, ComboBox cmbOption2)
        {
            restrictedIndexes.Clear();

            int[] restrict = new int[] { 0, 0, 0, 0, 0, 0, 0 };

            foreach(var item in Weapons)
            {
                if(item == "Reaper Autocannon" || item == "Heavy Flamer")
                {
                    restrict[0]++;
                }
                else if (item == "Combi-plasma")
                {
                    restrict[1]++;
                }
                else if (item == "Accursed Weapon")
                {
                    restrict[2]++;
                }
                else if (item == "Combi-flamer")
                {
                    restrict[3]++;
                }
                else if (item == "Combi-melta")
                {
                    restrict[4]++;
                }
                else if (item == "Power Fist")
                {
                    restrict[5]++;
                }
                else if (item == "Chainfist")
                {
                    restrict[6]++;
                }
            }

            if (restrict[0] == UnitSize / 5 && Weapons[currentIndex * 2] != "Reaper Autocannon" && Weapons[currentIndex * 2] != "Heavy Flamer")
            {
                restrictedIndexes.Add(5);
                restrictedIndexes.Add(6);
            }
            if (restrict[1] == UnitSize / 5 && Weapons[currentIndex * 2] != "Combi-plasma")
            {
                restrictedIndexes.Add(4);
            }
            if (restrict[2] == UnitSize / 5 && Weapons[currentIndex * 2] != "Accursed Weapon")
            {
                restrictedIndexes.Add(0);
            }
            if (restrict[3] == (UnitSize / 5) * 2 && Weapons[currentIndex * 2] != "Combi-flamer")
            {
                restrictedIndexes.Add(2);
            }
            if (restrict[4] == (UnitSize / 5) * 2 && Weapons[currentIndex * 2] != "Combi-melta")
            {
                restrictedIndexes.Add(3);
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

            restrictedIndexes2.Clear();

            if (restrict[5] == (UnitSize / 5) * 3 && (Weapons[(currentIndex * 2) + 1] != "Power Fist") || Weapons[currentIndex * 2] == "Accursed Weapon")
            {
                restrictedIndexes2.Add(2);
            }
            if (restrict[6] == UnitSize / 5 && (Weapons[(currentIndex * 2) + 1] != "Chainfist") || Weapons[currentIndex * 2] == "Accursed Weapon")
            {
                restrictedIndexes2.Add(1);
            }

            this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
        }
    }
}