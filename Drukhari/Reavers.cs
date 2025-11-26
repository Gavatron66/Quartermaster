using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Reavers : Datasheets
    {
        int[] restriction = new int[] { 0, 0 };
        int currentIndex;
        List<int> restrictedIndexes2 = new List<int>();

        public Reavers()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 3;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL3m";
            Weapons.Add("Splinter Rifle");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Splinter Rifle");
                Weapons.Add("(None)");
            }
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<WYCH CULT>",
                "BIKER", "FLY", "CORE", "REAVERS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Reavers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Drukhari;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 12;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[1] == "(None)" && Weapons[2] != "(None)")
            {
                lbModelSelect.Items.Add("Arena Champion w/ " + Weapons[0] + " and " + Weapons[2]);
            }
            else if (Weapons[1] != "(None)" && Weapons[2] == "(None)")
            {
                lbModelSelect.Items.Add("Arena Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            }
            else if (Weapons[1] != "(None)" &&  Weapons[2] != "(None)")
            {
                lbModelSelect.Items.Add("Arena Champion w/ " + Weapons[0] + ", " + Weapons[1] + ", and " + Weapons[2]);
            }
            else
            {
                lbModelSelect.Items.Add("Arena Champion w/ " + Weapons[0]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i * 2) + 2] == "(None)")
                {
                    lbModelSelect.Items.Add("Reaver w/ " + Weapons[(i * 2) + 1]);
                }
                else
                {
                    lbModelSelect.Items.Add("Reaver w/ " + Weapons[(i * 2) + 1] + " and " + Weapons[(i * 2) + 2]);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Blaster (+10 pts)",
                "Heat Lance (+10 pts)",
                "Splinter Rifle"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Cluster Caltrops (+5 pts)",
                "Grav-talon (+5 pts)"
            });

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Agoniser (+5 pts)",
                "Power Sword (+5 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);
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
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if(currentIndex == 0)
                        {
                            Weapons[0] = cmbOption1.SelectedItem.ToString();
                            if (Weapons[1] == "(None)" && Weapons[2] != "(None)")
                            {
                                lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + " and " + Weapons[2];
                            }
                            else if (Weapons[1] != "(None)" && Weapons[2] == "(None)")
                            {
                                lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + " and " + Weapons[1];
                            }
                            else if (Weapons[1] != "(None)" && Weapons[2] != "(None)")
                            {
                                lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + ", " + Weapons[1] + ", and " + Weapons[2];
                            }
                            else
                            {
                                lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0];
                            }
                        }
                        else
                        {
                            Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();

                            if (Weapons[(currentIndex * 2) + 2] == "(None)")
                            {
                                lbModelSelect.Items[currentIndex] = "Reaver w/ " + Weapons[(currentIndex * 2) + 1];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Reaver w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
                            }
                        }
                    }
                    else
                    {
                        if(currentIndex == 0)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        }
                    }

                    break;
                case 12:
                    if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[1] = cmbOption2.SelectedItem.ToString();
                            if (Weapons[1] == "(None)" && Weapons[2] != "(None)")
                            {
                                lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + " and " + Weapons[2];
                            }
                            else if (Weapons[1] != "(None)" && Weapons[2] == "(None)")
                            {
                                lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + " and " + Weapons[1];
                            }
                            else if (Weapons[1] != "(None)" && Weapons[2] != "(None)")
                            {
                                lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + ", " + Weapons[1] + ", and " + Weapons[2];
                            }
                            else
                            {
                                lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0];
                            }
                        }
                        else
                        {
                            Weapons[(currentIndex * 2) + 2] = cmbOption2.SelectedItem.ToString();

                            if (Weapons[(currentIndex * 2) + 2] == "(None)")
                            {
                                lbModelSelect.Items[currentIndex] = "Reaver w/ " + Weapons[(currentIndex * 2) + 1];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Reaver w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
                            }
                        }
                    }
                    else
                    {
                        if (currentIndex == 0)
                        {
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        }
                        else
                        {
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);
                        }
                    }

                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem.ToString();
                    if (Weapons[1] == "(None)" && Weapons[2] != "(None)")
                    {
                        lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + " and " + Weapons[2];
                    }
                    else if (Weapons[1] != "(None)" && Weapons[2] == "(None)")
                    {
                        lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else if (Weapons[1] != "(None)" && Weapons[2] != "(None)")
                    {
                        lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + ", " + Weapons[1] + ", and " + Weapons[2];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        if (lbModelSelect.Items.Count < UnitSize)
                        {
                            Weapons.Add("Splinter Rifle");
                            Weapons.Add("(None)");
                            lbModelSelect.Items.Add("Reaver w/ Splinter Rifle");
                        }
                    }

                    if (temp > UnitSize)
                    {
                        if (lbModelSelect.Items.Count > UnitSize)
                        {
                            lbModelSelect.Items.RemoveAt(temp - 1);
                            Weapons.RemoveRange((UnitSize * 2) - 1, 2);
                        }
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;
                    restrictedIndexes.Clear();
                    restrictedIndexes2.Clear();

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cmbOption3.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        antiLoop = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cmbOption3.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        panel.Controls["lblOption3"].Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        if (restriction[0] == UnitSize / 3 && Weapons[(currentIndex * 2) + 1] == "Splinter Rifle")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 1 });
                        }

                        antiLoop = false;
                        break;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cmbOption3.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        panel.Controls["lblOption3"].Visible = false;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);

                        if (restriction[0] == UnitSize / 3 && Weapons[(currentIndex * 2) + 1] == "Splinter Rifle")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 1 });
                        }

                        if (restriction[1] == UnitSize / 3 && Weapons[(currentIndex * 2) + 2] == "(None)")
                        {
                            restrictedIndexes2.AddRange(new int[] { 1, 2 });
                        }
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            restriction = new int[2] { 0, 0 };
            foreach (string weapon in Weapons)
            {
                if(weapon == "Blaster (+10 pts)" || weapon == "Heat Lance (+10 pts)")
                {
                    restriction[0]++;
                    Points += 10;
                }

                if(weapon == "Cluster Caltrops (+5 pts)" || weapon == "Grav-talon (+5 pts)")
                {
                    restriction[1]++;
                    Points += 5;
                }

                if(weapon == "Agoniser (+5 pts)" || weapon == "Power Sword (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Reavers - " + Points + "pts";
        }
    }
}