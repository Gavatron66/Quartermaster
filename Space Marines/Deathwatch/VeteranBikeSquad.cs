using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class VeteranBikeSquad : Datasheets
    {
        int currentIndex = 0;
        bool isLoading = false;
        int attackIndex = -1;

        public VeteranBikeSquad()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add(""); // Attack Bike Squad?
            Weapons.Add(""); // Attack Bike Weapon
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("(None)");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DEATHWATCH",
                "BIKER", "VETERAN BIKE SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new VeteranBikeSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            Label lblOption1 = panel.Controls["lblOption1"] as Label;
            lblExtra1.Location = new System.Drawing.Point(lblOption1.Location.X, lblOption1.Location.Y - 25);
            lblExtra1.Text = "If a weapon contains a *, then it can only taken by itself";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 6;

            lbModelSelect.Items.Clear();

            if (Weapons[2] == "(None)" && Weapons[3] == "(None)")
            {
                lbModelSelect.Items.Add("Veteran Biker Sergeant");
            }
            else if (Weapons[2] != "(None)" && Weapons[3] == "(None)")
            {
                lbModelSelect.Items.Add("Veteran Biker Sergeant w/ " + Weapons[2]);
            }
            else if (Weapons[2] == "(None)" && Weapons[3] != "(None)")
            {
                lbModelSelect.Items.Add("Veteran Biker Sergeant w/ " + Weapons[3]);
            }
            else
            {
                lbModelSelect.Items.Add("Veteran Biker Sergeant w/ " + Weapons[2] + " and " + Weapons[3]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[i + 3] == "(None)")
                {
                    lbModelSelect.Items.Add("Veteran Biker");
                }
                else
                {
                    lbModelSelect.Items.Add("Veteran Biker w/ " + Weapons[i + 3]);
                }
            }

            cbOption1.Text = "Include an Attack Bike (+35 pts)";
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
            if (isLoading) { return; }
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;

            switch (code)
            {
                case 11:
                    isLoading = true;
                    if (currentIndex == attackIndex)
                    {
                        Weapons[1] = cmbOption1.SelectedItem.ToString();
                    }
                    else if (currentIndex == 0)
                    {
                        Weapons[2] = cmbOption1.SelectedItem.ToString();
                    }
                    else
                    {
                        Weapons[currentIndex + 3] = cmbOption1.SelectedItem.ToString();
                    }

                    if (currentIndex == 0)
                    {
                        if (Weapons[2].Contains('*'))
                        {
                            cmbOption2.Enabled = false;
                            cmbOption2.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbOption2.Enabled = true;
                        }

                        if (Weapons[2] == "(None)" && Weapons[3] == "(None)")
                        {
                            lbModelSelect.Items[0] = ("Veteran Biker Sergeant");
                        }
                        else if (Weapons[2] != "(None)" && Weapons[3] == "(None)")
                        {
                            lbModelSelect.Items[0] = ("Veteran Biker Sergeant w/ " + Weapons[2]);
                        }
                        else if (Weapons[2] == "(None)" && Weapons[3] != "(None)")
                        {
                            lbModelSelect.Items[0] = ("Veteran Biker Sergeant w/ " + Weapons[3]);
                        }
                        else
                        {
                            lbModelSelect.Items[0] = ("Veteran Biker Sergeant w/ " + Weapons[2] + " and " + Weapons[3]);
                        }
                    }
                    else if (currentIndex == attackIndex)
                    {
                        lbModelSelect.Items[attackIndex] = "Veteran Attack Bike w/ " + Weapons[1];
                    }
                    else
                    {
                        if (Weapons[currentIndex + 3] == "(None)")
                        {
                            lbModelSelect.Items[currentIndex] = ("Veteran Biker");
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = ("Veteran Biker w/ " + Weapons[currentIndex + 3]);
                        }
                    }
                    break;
                case 12:
                    Weapons[3] = cmbOption2.SelectedItem.ToString();
                    if (Weapons[2] == "(None)" && Weapons[3] == "(None)")
                    {
                        lbModelSelect.Items[0] = ("Veteran Biker Sergeant");
                    }
                    else if (Weapons[2] != "(None)" && Weapons[3] == "(None)")
                    {
                        lbModelSelect.Items[0] = ("Veteran Biker Sergeant w/ " + Weapons[2]);
                    }
                    else if (Weapons[2] == "(None)" && Weapons[3] != "(None)")
                    {
                        lbModelSelect.Items[0] = ("Veteran Biker Sergeant w/ " + Weapons[3]);
                    }
                    else
                    {
                        lbModelSelect.Items[0] = ("Veteran Biker Sergeant w/ " + Weapons[2] + " and " + Weapons[3]);
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
                            Weapons.Add("(None)");
                            lbModelSelect.Items.Add("Veteran Biker w/ " + Weapons[temp + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(temp + 2, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    isLoading = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        lblExtra1.Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        lblExtra1.Visible = false;

                        if (currentIndex == 0)
                        {
                            cmbOption2.Visible = true;
                            panel.Controls["lblOption2"].Visible = true;
                            lblExtra1.Visible = true;

                            if (Weapons[2].Contains('*'))
                            {
                                cmbOption2.Enabled = false;
                            }
                            else
                            {
                                cmbOption2.Enabled = true;
                            }

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "(None)",
                                "Astartes Chainsword",
                                "Bolt Pistol",
                                "Deathwatch Boltgun",
                                "Deathwatch Combi-flamer",
                                "Deathwatch Combi-grav",
                                "Deathwatch Combi-melta",
                                "Deathwatch Combi-plasma",
                                "*Deathwatch Shotgun",
                                "Flamer",
                                "Grav-gun",
                                "Grav-pistol",
                                "Hand Flamer",
                                "Inferno Pistol",
                                "Lightning Claw",
                                "Meltagun",
                                "Plasma Gun",
                                "Plasma Pistol",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "*Stalker-pattern Boltgun",
                                "Storm Bolter",
                                "Storm Shield",
                                "Thunder Hammer",
                                "Xenophase Blade"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[2]);

                            cmbOption2.Items.Clear();
                            cmbOption2.Items.AddRange(new string[]
                            {
                                "(None)",
                                "Astartes Chainsword",
                                "Bolt Pistol",
                                "Grav-pistol",
                                "Hand Flamer",
                                "Inferno Pistol",
                                "Lightning Claw",
                                "Plasma Pistol",
                                "Power Axe",
                                "Power Fist",
                                "Power Maul",
                                "Power Sword",
                                "Thunder Hammer",
                                "Xenophase Blade"
                            });
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[3]);
                        }
                        else if (currentIndex == attackIndex)
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Heavy Bolter",
                                "Multi-melta"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);
                        }
                        else
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "(None)",
                                "Astartes Chainsword",
                                "Bolt Pistol",
                                "Power Axe",
                                "Power Maul",
                                "Power Sword"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 3]);
                        }
                    }
                    break;
            }

            isLoading = false;

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons.Contains("Attack Bike"))
            {
                Points += 35;
            }
        }

        public override string ToString()
        {
            return "Veteran Bike Squad - " + Points + "pts";
        }
    }
}
