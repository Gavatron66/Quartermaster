using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class SkitariiVanguard : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int[] restrictArray = new int[] { 0, 0, 0 };
        int[] restrictArray2 = new int[] { 0, 0 };
        //Arc Rifle (+5 pts)
        //Plasma Caliver (+5 pts)
        //Transuranic Arquebus (+10 pts)
        //Enhanced Data-tether (+5 pts)
        //Omnispex (+5 pts)

        public SkitariiVanguard()
        {
            DEFAULT_POINTS = 9;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Radium Carbine");
                Weapons.Add("(None)");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "INFANTRY", "CORE", "SKITARII RANGERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new SkitariiVanguard();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdMech;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Skitarii Vanguard Alpha with " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Skitarii Vanguard Alpha with " + Weapons[0] + " and " + Weapons[1]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i * 2) + 1] == "(None)")
                {
                    lbModelSelect.Items.Add("Skitarii Vanguard with " + Weapons[i * 2]);
                }
                else
                {
                    lbModelSelect.Items.Add("Skitarii Vanguard with " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption2.Items.Clear();
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (isLoading)
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
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Skitarii Vanguard Alpha with " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Skitarii Vanguard Alpha with " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        Weapons[(currentIndex * 2)] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[(currentIndex * 2) + 1] == "(None)")
                        {
                            lbModelSelect.Items[currentIndex] = "Skitarii Vanguard with " + Weapons[currentIndex * 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Skitarii Vanguard with " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                        }
                    }
                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Skitarii Vanguard Alpha with " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Skitarii Vanguard Alpha with " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[(currentIndex * 2) + 1] == "(None)")
                        {
                            lbModelSelect.Items[currentIndex] = "Skitarii Vanguard with " + Weapons[currentIndex * 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Skitarii Vanguard with " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                        }

                        if (Weapons[(currentIndex * 2) + 1] != "(None)")
                        {
                            cmbOption1.Enabled = false;
                            cmbOption1.Items.IndexOf("Radium Carbine");
                        }
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Radium Carbine");
                        Weapons.Add("(None)");
                        lbModelSelect.Items.Add("Skitarii Vanguard with " + Weapons[temp * 2]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize * 2, 2);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0 && !isLoading)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    isLoading = true;

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (currentIndex == 0)
                    {
                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Arc Pistol",
                            "Phosphor Blast Pistol",
                            "Radium Carbine",
                            "Radium Pistol"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Arc Maul",
                            "Power Sword",
                            "Taser Goad"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        isLoading = false;
                        break;
                    }

                    cmbOption1.Items.Clear();
                    cmbOption1.Items.AddRange(new string[]
                    {
                        "Arc Rifle (+5 pts)",
                        "Plasma Caliver (+5 pts)",
                        "Radium Carbine",
                        "Transuranic Arquebus (+10 pts)"
                    });
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);

                    cmbOption2.Items.Clear();
                    cmbOption2.Items.AddRange(new string[]
                    {
                        "(None)",
                        "Enhanced Data-tether (+5 pts)",
                        "Omnispex (+5 pts)"
                    });
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    if (Weapons[(currentIndex * 2) + 1] != "(None)")
                    {
                        cmbOption1.Enabled = false;
                    }

                    if (UnitSize < 10 && (restrictArray[0] + restrictArray[1] + restrictArray[2] == 1) &&
                        !(Weapons[currentIndex * 2] == "Arc Rifle (+5 pts)" || Weapons[currentIndex * 2] == "Plasma Caliver (+5 pts)" || Weapons[currentIndex * 2] == "Transuranic Arquebus (+10 pts)"))
                    {
                        if (Weapons[currentIndex * 2] == "Radium Carbine")
                        {
                            cmbOption1.Items.Remove("Arc Rifle (+5 pts)");
                            cmbOption1.Items.Remove("Plasma Caliver (+5 pts)");
                            cmbOption1.Items.Remove("Transuranic Arquebus (+10 pts)");
                        }
                    }
                    else if (UnitSize >= 10)
                    {
                        if (restrictArray[0] == UnitSize / 10 && Weapons[currentIndex * 2] != "Arc Rifle (+5 pts)")
                        {
                            cmbOption1.Items.Remove("Arc Rifle (+5 pts)");
                        }

                        if (restrictArray[1] == UnitSize / 10 && Weapons[currentIndex * 2] != "Plasma Caliver (+5 pts)")
                        {
                            cmbOption1.Items.Remove("Plasma Caliver (+5 pts)");
                        }

                        if (restrictArray[2] == UnitSize / 10 && Weapons[currentIndex * 2] != "Transuranic Arquebus (+10 pts)")
                        {
                            cmbOption1.Items.Remove("Transuranic Arquebus (+10 pts)");
                        }
                    }

                    if (UnitSize != 20 && (restrictArray2[0] + restrictArray2[1] == 1))
                    {
                        if (Weapons[(currentIndex * 2) + 1] == "(None)")
                        {
                            cmbOption2.Items.Clear();
                            cmbOption2.Items.Add("(None)");
                            cmbOption2.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 2) + 1] != "Enhanced Data-tether (+5 pts)" && restrictArray2[0] == 1)
                        {
                            cmbOption2.Items.Remove("Enhanced Data-tether (+5 pts)");
                        }
                        if (Weapons[(currentIndex * 2) + 1] != "Omnispex (+5 pts)" && restrictArray2[1] == 1)
                        {
                            cmbOption2.Items.Remove("Omnispex (+5 pts)");
                        }
                    }

                    isLoading = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            restrictArray[0] = 0;
            restrictArray[1] = 0;
            restrictArray[2] = 0;
            restrictArray2[0] = 0;
            restrictArray2[1] = 0;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Arc Rifle (+5 pts)")
                {
                    restrictArray[0] += 1;
                    Points += 5;
                }

                if (weapon == "Plasma Caliver (+5 pts)")
                {
                    restrictArray[1] += 1;
                    Points += 5;
                }

                if (weapon == "Transuranic Arquebus (+10 pts)")
                {
                    restrictArray[2] += 1;
                    Points += 10;
                }

                if (weapon == "Enhanced Data-tether (+5 pts)")
                {
                    restrictArray2[0] += 1;
                    Points += 5;
                }

                if (weapon == "Omnispex (+5 pts)")
                {
                    restrictArray2[1] += 1;
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Skitarii Vanguard - " + Points + "pts";
        }
    }
}
