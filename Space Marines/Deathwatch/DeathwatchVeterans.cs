using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class DeathwatchVeterans : Datasheets
    {
        int currentIndex = 0;
        bool isLoading = false;
        int[] restrict = new int[2] { 0, 0 };

        public DeathwatchVeterans()
        {
            DEFAULT_POINTS = 27;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m2k";
            Weapons.Add(""); //Combat Shield
            Weapons.Add("-1"); //Black Shield Index Location
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Deathwatch Boltgun");
                Weapons.Add("Power Sword");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DEATHWATCH",
                "INFANTRY", "CORE", "VETERANS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathwatchVeterans();
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
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            Label lblOption1 = panel.Controls["lblOption1"] as Label;
            lblExtra1.Location = new System.Drawing.Point(lblOption1.Location.X, lblOption1.Location.Y - 25);
            lblExtra1.Text = "If a weapon contains a *, then it can only taken by itself";
            lblExtra1.Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[0] == "")
            {
                lbModelSelect.Items.Add("Watch Sergeant w/ " + Weapons[2] + " and " + Weapons[3]);
            }
            else
            {
                lbModelSelect.Items.Add("Watch Sergeant w/ " + Weapons[2] + ", " + Weapons[3] + " and a " + Weapons[0]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Veteran w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3]);
            }

            cmbOption1.Items.Clear();
            cmbOption2.Items.Clear();

            cbOption1.Text = "Combat Shield";
            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cmbOption2.Location.Y + 30);
            cbOption2.Text = "Upgrade to Black Shield";
            cbOption2.Location = cbOption1.Location;
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 2) + 2] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[0] = "Watch Sergeant w/ " + Weapons[2] + " and " + Weapons[3];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Watch Sergeant w/ " + Weapons[2] + ", " + Weapons[3] + " and a " + Weapons[0];
                        }
                    }
                    else
                    {
                        if (Weapons[(currentIndex * 2) + 2].Contains('*'))
                        {
                            cmbOption2.Enabled = false;
                            cmbOption2.SelectedIndex = -1;
                            lbModelSelect.Items[currentIndex] = "Veteran w/ " + Weapons[(currentIndex * 2) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Veteran w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3];
                        }
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 3] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[0] = "Watch Sergeant w/ " + Weapons[2] + " and " + Weapons[3];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Watch Sergeant w/ " + Weapons[2] + ", " + Weapons[3] + " and a " + Weapons[0];
                        }
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Veteran w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3];
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[0] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[1] = currentIndex.ToString();
                        lbModelSelect.Items[currentIndex] = "Black Shield w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3];
                    }
                    else
                    {
                        Weapons[1] = "-1";
                        lbModelSelect.Items[currentIndex] = "Veteran w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Deathwatch Boltgun");
                        Weapons.Add("Power Sword");
                        lbModelSelect.Items.Add("Veteran w/ " + Weapons[(temp * 2) + 2] + " and " + Weapons[(temp * 2) + 3]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 2) + 2, 2);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0 && !isLoading)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    isLoading = true;

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbOption1.Visible = true;
                        cbOption2.Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption2.Items.Clear();

                        cmbOption1.Items.AddRange(GetWeapons(1));
                        cmbOption2.Items.AddRange(GetWeapons(2));
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[2]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[3]);
                        if (Weapons[2].Contains('*'))
                        {
                            cmbOption2.Enabled = false;
                            cmbOption2.SelectedIndex = -1;
                        }
                        else
                        {
                            cmbOption2.Enabled = true;
                        }
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbOption1.Visible = false;
                        cbOption2.Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption2.Items.Clear();

                        cmbOption1.Items.AddRange(GetWeapons(3));
                        cmbOption2.Items.AddRange(GetWeapons(4));
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);
                        if(Weapons[(currentIndex * 2) + 2].Contains('*'))
                        {
                            cmbOption2.Enabled = false;
                            cmbOption2.SelectedIndex = -1;
                        }
                        else
                        {
                            cmbOption2.Enabled = true;
                        }
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 3]);

                        if(currentIndex == Convert.ToInt32(Weapons[1]))
                        {
                            cbOption2.Enabled = true;
                            cbOption2.Checked = true;
                        }
                        else
                        {
                            cbOption2.Checked = false;
                            if (Convert.ToInt32(Weapons[1]) != -1)
                            {
                                cbOption2.Enabled = false;
                            }
                        }
                    }

                    isLoading = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            restrict[0] = 0;
            restrict[1] = 0;
            foreach (var weapon in Weapons)
            {
                if (weapon == "*Deathwatch Frag Cannon" || weapon == "*Infernus Heavy Bolter"
                    || weapon == "*Heavy Bolter" || weapon == "*Heavy Flamer" || weapon == "*Missile Launcher")
                {
                    restrict[0]++;
                }

                if (weapon == "*Heavy Thunder Hammer (+12 pts)")
                {
                    restrict[1]++;
                    Points += 12;
                }

                if(weapon == "Thunder Hammer (+10 pts)")
                {
                    Points += 10;
                }

                if (weapon == "Xenophase Blade (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Deathwatch Veterans - " + Points + "pts";
        }

        private string[] GetWeapons(int comboBox)
        {
            List<string> weapons = new List<string>();

            if (comboBox == 1)
            {
                weapons.AddRange(new string[]
                {
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
                    "Thunder Hammer (+10 pts)",
                    "Xenophase Blade (+5 pts)"
                });
            }
            else if (comboBox == 2)
            {
                weapons.AddRange(new string[]
                {
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
                    "Thunder Hammer (+10 pts)",
                    "Xenophase Blade (+5 pts)"
                });
            }
            else if (comboBox == 3)
            {
                weapons.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Deathwatch Boltgun",
                    "Deathwatch Combi-flamer",
                    "Deathwatch Combi-grav",
                    "Deathwatch Combi-melta",
                    "Deathwatch Combi-plasma",
                    "*Deathwatch Frag Cannon",
                    "*Deathwatch Shotgun",
                    "Flamer",
                    "Grav-gun",
                    "Grav-pistol",
                    "Hand Flamer",
                    "*Heavy Bolter",
                    "*Heavy Flamer",
                    "*Heavy Thunder Hammer (+12 pts)",
                    "Inferno Pistol",
                    "*Infernus Heavy Bolter",
                    "Lightning Claw",
                    "Meltagun",
                    "*Missile Launcher",
                    "Plasma Gun",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist",
                    "Power Maul",
                    "Power Sword",
                    "*Stalker-pattern Boltgun",
                    "Storm Bolter",
                    "Storm Shield",
                    "Thunder Hammer (+10 pts)",
                });
            }
            else if (comboBox == 4)
            {
                weapons.AddRange(new string[]
                {
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
                    "Thunder Hammer (+10 pts)"
                });
            }

            if (restrict[0] == 4)
            {
                if(Weapons[(currentIndex * 2) + 2] != "*Deathwatch Frag Cannon")
                {
                    weapons.Remove("*Deathwatch Frag Cannon");
                }

                if (Weapons[(currentIndex * 2) + 2] != "*Heavy Bolter")
                {
                    weapons.Remove("*Heavy Bolter");
                }

                if (Weapons[(currentIndex * 2) + 2] != "*Heavy Flamer")
                {
                    weapons.Remove("*Heavy Flamer");
                }

                if (Weapons[(currentIndex * 2) + 2] != "*Infernus Heavy Bolter")
                {
                    weapons.Remove("*Infernus Heavy Bolter");
                }

                if (Weapons[(currentIndex * 2) + 2] != "*Missile Launcher")
                {
                    weapons.Remove("*Missile Launcher");
                }
            }
            if (restrict[1] == UnitSize / 5 && Weapons[(currentIndex * 2) + 2] != "*Heavy Thunder Hammer (+12 pts)")
            {
                weapons.Remove("*Heavy Thunder Hammer (+12 pts)");
            }

            return weapons.ToArray();
        }
    }
}
