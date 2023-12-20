using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class RelicTerminators : Datasheets
    {
        int currentIndex;
        int restrictionCount;
        int restrictionCount2;
        bool isLoading = false;

        public RelicTerminators()
        {
            DEFAULT_POINTS = 33;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Combi-bolter");
                Weapons.Add("Power Fist");
                Weapons.Add(""); //Grenade Harness
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "TERMINATOR", "RELIC TERMINATOR SQUAD"
            });
            role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new RelicTerminators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Relic Terminator Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Relic Terminator w/ " + Weapons[(i * 3)] + " and " + Weapons[(i * 3) + 1]);
            }

            cbOption1.Text = "Grenade Harness";
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(isLoading)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 3] = cmbOption1.SelectedItem.ToString();

                    if(currentIndex == 0)
                    {
                        if (Weapons[2] != "")
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                                " and a " + Weapons[2]; 
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 3) + 2] != "")
                        {
                            lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3] 
                                + ", " + Weapons[(currentIndex * 3) + 1] + " and a " + Weapons[(currentIndex * 3) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3] + 
                                " and " + Weapons[(currentIndex * 3) + 1];
                        }
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 3) + 1] = cmbOption2.SelectedItem.ToString();

                    if (currentIndex == 0)
                    {
                        if (Weapons[2] != "")
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                                " and a " + Weapons[2];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 3) + 2] != "")
                        {
                            lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3]
                                + ", " + Weapons[(currentIndex * 3) + 1] + " and a " + Weapons[(currentIndex * 3) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Relic Terminator w/ " + Weapons[currentIndex * 3] +
                                " and " + Weapons[(currentIndex * 3) + 1];
                        }
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 3) + 2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 2] = "";
                    }

                    if (currentIndex == 0)
                    {
                        if (Weapons[2] != "")
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + ", " + Weapons[1] +
                                " and a " + Weapons[2];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Relic Terminator Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 3) + 2] != "")
                        {
                            lbModelSelect.Items[currentIndex] = "Relic Terminator w/ " + Weapons[currentIndex * 3]
                                + ", " + Weapons[(currentIndex * 3) + 1] + " and a " + Weapons[(currentIndex * 3) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Relic Terminator w/ " + Weapons[currentIndex * 3] +
                                " and " + Weapons[(currentIndex * 3) + 1];
                        }
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
                            Weapons.Add("Power Fist");
                            lbModelSelect.Items.Add("Relic Terminator w/ " + Weapons[(currentIndex * 3)]
                                + " and " + Weapons[(currentIndex * 3) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 3), 3);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if(currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        cbOption1.Enabled = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                    }

                    isLoading = true;

                    if(currentIndex == 0)
                    {
                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Combi-bolter",
                            "Lightning Claw",
                            "Plasma Blaster",
                            "Volkite Charger"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Chainfist",
                            "Lightning Claw",
                            "Power Fist",
                            "Power Sword"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                    }
                    else if (UnitSize / 5 == restrictionCount && 
                        !(Weapons[currentIndex * 3] == "Heavy Flamer") || (Weapons[currentIndex * 3] == "Reaper Autocannon"))
                    {
                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Combi-bolter",
                            "Lightning Claw",
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Chainfist",
                            "Lightning Claw",
                            "Power Fist",
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 3) + 1]);
                    }
                    else
                    {
                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Combi-bolter",
                            "Heavy Flamer",
                            "Lightning Claw",
                            "Reaper Autocannon"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Chainfist",
                            "Lightning Claw",
                            "Power Fist",
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 3) + 1]);
                    }

                    if(UnitSize / 5 == restrictionCount2 && Weapons[(currentIndex * 3) + 2] == "")
                    {
                        cbOption1.Enabled = false;
                        cbOption1.Checked = false;
                    }
                    else
                    {
                        cbOption1.Enabled = true;
                        if(Weapons[(currentIndex * 3) + 2] == "")
                        {
                            cbOption1.Checked = false;
                        }
                        else
                        {
                            cbOption1.Checked = true;
                        }
                    }

                    isLoading = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            restrictionCount = 0;
            restrictionCount2 = 0;

            //Heavy Flamer, Reaper Autocannon, Grenade Harness
        }

        public override string ToString()
        {
            return "Relic Terminator Squad - " + Points + "pts";
        }
    }
}
