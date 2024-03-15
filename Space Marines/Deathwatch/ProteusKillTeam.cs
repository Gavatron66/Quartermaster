using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class ProteusKillTeam : Datasheets
    {
        int currentIndex;
        int[] restrict = new int[3] { 0, 0, 0 };

        public ProteusKillTeam()
        {
            DEFAULT_POINTS = 27;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL3m1k";
            Weapons.Add("Deathwatch Boltgun");
            Weapons.Add("Power Sword");
            Weapons.Add("");
            Weapons.Add("Watch Sergeant");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Deathwatch Boltgun");
                Weapons.Add("Power Sword");
                Weapons.Add("");
                Weapons.Add("Deathwatch Veteran");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DEATHWATCH",
                "INFANTRY", "CORE", "KILL TEAM", "PROTEUS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new ProteusKillTeam();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            Label lblOption1 = panel.Controls["lblOption1"] as Label;
            lblExtra1.Location = new System.Drawing.Point(lblOption1.Location.X, lblOption1.Location.Y - 25);
            lblExtra1.Text = "If a weapon contains a *, then it can only taken by itself";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add(Weapons[3] + " w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add(Weapons[(i * 4) + 3] + " w/ " + Weapons[(i * 4)] + " and " + Weapons[(i * 4) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Deathwatch Veteran",
                "Deathwatch Terminator",
                "Vanguard Veteran",
                "Veteran Biker"
            });

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
            cmbFaction.Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Text = "Kill Team Specialism";

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;

            switch (code)
            {
                case 11:
                    if(cmbOption1.SelectedIndex == -1)
                    {
                        break;
                    }

                    Weapons[(currentIndex * 4) + 3] = cmbOption1.SelectedItem.ToString();
                    if (Weapons[(currentIndex * 4) + 3] == "Deathwatch Veteran")
                    {
                        Weapons[(currentIndex * 4)] = "Deathwatch Boltgun";
                        Weapons[(currentIndex * 4) + 1] = "Power Sword";
                        Weapons[(currentIndex * 4) + 2] = "";
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Deathwatch Terminator")
                    {
                        Weapons[(currentIndex * 4)] = "Storm Bolter";
                        Weapons[(currentIndex * 4) + 1] = "Power Fist";
                        Weapons[(currentIndex * 4) + 2] = "";
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Vanguard Veteran")
                    {
                        Weapons[(currentIndex * 4)] = "Bolt Pistol";
                        Weapons[(currentIndex * 4) + 1] = "Astartes Chainsword";
                        Weapons[(currentIndex * 4) + 2] = "";
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Veteran Biker")
                    {
                        Weapons[(currentIndex * 4)] = "(None)";
                        Weapons[(currentIndex * 4) + 1] = "Biker";
                        Weapons[(currentIndex * 4) + 2] = "";
                    }

                    if(Weapons[(currentIndex * 4) + 3] == "Veteran Biker")
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                    }

                    lbModelSelect.SelectedIndex = currentIndex;
                    break;
                case 12:
                    Weapons[currentIndex * 4] = cmbOption2.SelectedItem.ToString();
                    if (Weapons[currentIndex * 4].Contains('*') || Weapons[(currentIndex * 4) + 1] == "Biker")
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)];
                        cmbOption3.Enabled = false;
                    }
                    else if (Weapons[currentIndex * 4] == "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3];
                        cmbOption3.Enabled = true;
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                        cmbOption3.Enabled = true;
                    }
                    break;
                case 13:
                    Weapons[(currentIndex * 4) + 1] = cmbOption3.SelectedItem.ToString();
                    if (Weapons[currentIndex * 4].Contains('*'))
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)];

                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 4) + 3] + " w/ " + Weapons[(currentIndex * 4)] + " and " + Weapons[(currentIndex * 4) + 1];
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex) + 2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex) + 2] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            lbModelSelect.Items.Add("<--- Select a Model --->");
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(temp * 4, 4);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;
                    cmbOption3.Enabled = true;

                    if (currentIndex < 0)
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        cmbOption3.Visible = false;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = false;
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);
                    cmbOption2.Items.Clear();
                    cmbOption3.Items.Clear();

                    if (Weapons[(currentIndex * 4) + 3] == "Watch Sergeant")
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = true;
                        cmbOption3.Visible = true;
                        cbOption1.Visible = true;
                        lblExtra1.Visible = true;

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            cmbOption3.Enabled = false;
                        }

                        cmbOption3.Items.Clear();
                        cmbOption3.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 2));
                        cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);

                        cbOption1.Text = "Combat Shield";
                        if (Weapons[(currentIndex * 4) + 2] == cbOption1.Text)
                        {
                            cbOption1.Checked = true;
                        }
                        else
                        {
                            cbOption1.Checked = false;
                        }
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Deathwatch Veteran")
                    {
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = true;
                        cmbOption3.Visible = true;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = true;

                        if (currentIndex < 5)
                        {
                            panel.Controls["lblOption1"].Visible = false;
                            cmbOption1.Visible = false;
                        }
                        else
                        {
                            panel.Controls["lblOption1"].Visible = true;
                            cmbOption1.Visible = true;
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);
                        }

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            cmbOption3.Enabled = false;
                        }

                        cmbOption3.Items.Clear();
                        cmbOption3.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 2));
                        cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Deathwatch Terminator")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = true;
                        cmbOption3.Visible = true;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            cmbOption3.Enabled = false;
                        }

                        cmbOption3.Items.Clear();
                        cmbOption3.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 2));
                        cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Vanguard Veteran")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = true;
                        cmbOption3.Visible = true;
                        cbOption1.Visible = true;
                        lblExtra1.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);

                        if (Weapons[currentIndex * 4].Contains('*'))
                        {
                            cmbOption3.Enabled = false;
                        }

                        cmbOption3.Items.Clear();
                        cmbOption3.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 2));
                        cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);
                    }
                    else if (Weapons[(currentIndex * 4) + 3] == "Veteran Biker")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption3"].Visible = false;
                        cmbOption3.Visible = false;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = false;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(GetWeapons(Weapons[(currentIndex * 4) + 3], 1));
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 4]);
                    }
                    else
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        cmbOption3.Visible = false;
                        cbOption1.Visible = false;
                        lblExtra1.Visible = false;

                        cmbOption1.SelectedIndex = -1;
                    }

                    antiLoop = false;
                    break;
            }

            Points = 0;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            restrict[0] = 0;
            restrict[1] = 0;
            restrict[2] = 0;

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

                if (weapon == "Thunder Hammer (+10 pts)")
                {
                    Points += 10;
                }

                if (weapon == "Xenophase Blade (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon == "Assault Cannon" || weapon == "Cyclone Missile Launcher and Storm Bolter (+10 pts)" || weapon == "Heavy Flamer"
                    || weapon == "Plasma Cannon")
                {
                    restrict[2]++;
                }

                if (weapon == "Cyclone Missile Launcher and Storm Bolter (+10 pts)")
                {
                    Points += 10;
                }
                if (weapon == "Lightning Claw (+3 pts)")
                {
                    Points += 3;
                }

                if (weapon == "Storm Shield (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon == "Power Fist (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Watch Sergeant" || weapon == "Deathwatch Veteran")
                {
                    Points += 27;
                }
                if (weapon == "Deathwatch Terminator")
                {
                    Points += 35;
                }
                if (weapon == "Vangaurd Veteran")
                {
                    Points += 20;
                }
                if (weapon == "Veteran Biker")
                {
                    Points += 35;
                }
            }

            for(int i = 1; i < Weapons.Count + 1; i *= 4)
            {
                if(Weapons[i-1] == "*Heavy Thunder Hammer (+12 pts)" && Weapons[i + 2] == "Vanguard Veteran")
                {
                    restrict[1]--;
                }
            }
        }

        public override string ToString()
        {
            return "Proteus Kill Team - " + Points + "pts";
        }

        private string[] GetWeapons(string model, int comboBox)
        {
            List<string> weaponsList = new List<string>();

            if (model == "Watch Sergeant" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
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
            else if (model == "Watch Sergeant" && comboBox == 2)
            {
                weaponsList.AddRange(new string[]
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
            else if (model == "Deathwatch Veteran" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
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

                if (restrict[0] == 4)
                {
                    if (Weapons[(currentIndex * 4)] != "*Deathwatch Frag Cannon")
                    {
                        weaponsList.Remove("*Deathwatch Frag Cannon");
                    }

                    if (Weapons[(currentIndex * 4)] != "*Heavy Bolter")
                    {
                        weaponsList.Remove("*Heavy Bolter");
                    }

                    if (Weapons[(currentIndex * 4)] != "*Heavy Flamer")
                    {
                        weaponsList.Remove("*Heavy Flamer");
                    }

                    if (Weapons[(currentIndex * 4)] != "*Infernus Heavy Bolter")
                    {
                        weaponsList.Remove("*Infernus Heavy Bolter");
                    }

                    if (Weapons[(currentIndex * 4)] != "*Missile Launcher")
                    {
                        weaponsList.Remove("*Missile Launcher");
                    }
                }

                if (restrict[1] == UnitSize / 5 && Weapons[(currentIndex * 4)] != "*Heavy Thunder Hammer (+12 pts)")
                {
                    weaponsList.Remove("*Heavy Thunder Hammer (+12 pts)");
                }
            }
            else if (model == "Deathwatch Veteran" && comboBox == 2)
            {
                weaponsList.AddRange(new string[]
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
            else if (model == "Deathwatch Terminator" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
                {
                    "Assault Cannon",
                    "Cyclone Missile Launcher and Storm Bolter (+10 pts)",
                    "Heavy Flamer",
                    "Plasma Cannon",
                    "Storm Bolter",
                    "*Thunder Hammer and Storm Shield",
                    "*Two Lightning Claws"
                });
            }
            else if (model == "Deathwatch Terminator" && comboBox == 2)
            {
                weaponsList.AddRange(new string[]
                {
                    "Chainfist",
                    "Power Axe",
                    "Power Fist",
                    "Power Maul",
                    "Power Sword",
                });
            }
            else if (model == "Vanguard Veteran" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Grav-pistol",
                    "Hand Flamer",
                    "*Heavy Thunder Hammer (+12 pts)",
                    "Inferno Pistol",
                    "Lightning Claw (+3 pts)",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist (+5 pts)",
                    "Power Maul",
                    "Power Sword",
                    "Storm Shield (+5 pts)",
                    "Thunder Hammer (+10 pts)"
                });
            }
            else if (model == "Vanguard Veteran" && comboBox == 2)
            {
                weaponsList.AddRange(new string[]
                {
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Grav-pistol",
                    "Hand Flamer",
                    "Inferno Pistol",
                    "Lightning Claw (+3 pts)",
                    "Plasma Pistol",
                    "Power Axe",
                    "Power Fist (+5 pts)",
                    "Power Maul",
                    "Power Sword",
                    "Storm Shield (+5 pts)",
                    "Thunder Hammer (+10 pts)"
                });
            }
            else if (model == "Veteran Biker" && comboBox == 1)
            {
                weaponsList.AddRange(new string[]
                {
                    "(None)",
                    "Astartes Chainsword",
                    "Bolt Pistol",
                    "Power Axe",
                    "Power Maul",
                    "Power Sword"
                });
            }

            return weaponsList.ToArray();
        }
    }
}
