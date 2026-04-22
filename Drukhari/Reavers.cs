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

        public Reavers()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 3;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Splinter Rifle");
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 12;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Arena Champion w/ " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Arena Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i*2) + 1] == "(None)")
                {
                    lbModelSelect.Items.Add("Reaver w/ " + Weapons[i * 2]);
                }
                else
                {
                    lbModelSelect.Items.Add("Reaver w/ " + Weapons[(i * 2) + 1]);
                }
            }
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
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        break;
                    }

                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();

                    if (Weapons[(currentIndex * 2) + 1] == "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = "Reaver w/ " + Weapons[currentIndex * 2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Reaver w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }

                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();

                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Arena Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        break;
                    }

                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[(currentIndex * 2) + 1] == "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = "Reaver w/ " + Weapons[currentIndex * 2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Reaver w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
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

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        antiLoop = false;
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
                            "Blaster",
                            "Heat Lance",
                            "Splinter Rifle"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Agoniser (+5 pts)",
                            "Power Sword (+5 pts)"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        if (restriction[0] == UnitSize / 3 && Weapons[currentIndex * 2] == "Splinter Rifle")
                        {
                            cmbOption1.Items.RemoveAt(1);
                            cmbOption1.Items.RemoveAt(0);
                        }

                        antiLoop = false;
                        break;
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
                            "Blaster (+10 pts)",
                            "Heat Lance (+10 pts)",
                            "Splinter Rifle"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Cluster Caltrops (+5 pts)",
                            "Grav-talon (+5 pts)"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        if (restriction[0] == UnitSize / 3 && Weapons[currentIndex * 2] == "Splinter Rifle")
                        {
                            cmbOption1.Items.RemoveAt(1);
                            cmbOption1.Items.RemoveAt(0);
                        }

                        if (restriction[1] == UnitSize / 3 && Weapons[(currentIndex * 2) + 1] == "(None)")
                        {
                            cmbOption2.Items.RemoveAt(2);
                            cmbOption2.Items.RemoveAt(1);
                        }
                    }

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