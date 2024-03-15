using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class SpectrusKillTeam : Datasheets
    {
        int currentIndex;

        public SpectrusKillTeam()
        {
            DEFAULT_POINTS = 23;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            Weapons.Add("");
            Weapons.Add("Infiltrator Sergeant");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("");
                Weapons.Add("Infiltrator");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DEATHWATCH",
                "INFANTRY", "CORE", "PHOBOS", "PRIMARIS", "KILL TEAM", "SPECTRUS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new SpectrusKillTeam();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add(Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[i * 2] != "")
                {
                    lbModelSelect.Items.Add(Weapons[(i * 2) + 1] + " w/ " + Weapons[i * 2]);
                }
                else
                {
                    lbModelSelect.Items.Add(Weapons[(i * 2) + 1]);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Eliminator",
                "Incursor",
                "Infiltrator",
                "Reiver"
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

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (cmbOption1.SelectedIndex == -1)
                    {
                        break;
                    }

                    Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
                    if (Weapons[(currentIndex * 2) + 1] == "Infiltrator")
                    {
                        Weapons[(currentIndex * 2)] = "";
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Incursor")
                    {
                        Weapons[(currentIndex * 2)] = "";
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Eliminator")
                    {
                        Weapons[(currentIndex * 2)] = "Bolt Sniper Rifle";
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Reiver")
                    {
                        Weapons[(currentIndex * 2)] = "Combat Blade";
                    }

                    if (Weapons[currentIndex * 2] != "")
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1] + " w/ " + Weapons[(currentIndex * 2)];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1];
                    }

                    lbModelSelect.SelectedIndex = currentIndex;
                    break;
                case 12:
                    Weapons[currentIndex * 2] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[currentIndex * 2] != "")
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1] + " w/ " + Weapons[(currentIndex * 2)];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
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
                            lbModelSelect.Items.Add("<--- Select a Model --->");
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((temp * 2) - 2, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    cmbOption2.Items.Clear();

                    if (Weapons[(currentIndex * 2) + 1] == "Infiltrator Sergeant")
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Infiltrator")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;

                        if (currentIndex < 5)
                        {
                            panel.Controls["lblOption1"].Visible = false;
                            cmbOption1.Visible = false;
                        }
                        else
                        {
                            panel.Controls["lblOption1"].Visible = true;
                            cmbOption1.Visible = true;
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        }

                        if ((Weapons.Contains("Helix Gauntlet") || Weapons.Contains("Infiltrator Comms Array"))
                            && Weapons[currentIndex * 2] == "")
                        {
                            panel.Controls["lblOption2"].Visible = false;
                            cmbOption2.Visible = false;
                        }

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "",
                            "Helix Gauntlet",
                            "Infiltrator Comms Array"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);

                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Incursor")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        if (Weapons.Contains("Haywire Mine") && Weapons[currentIndex * 2] == "")
                        {
                            panel.Controls["lblOption2"].Visible = false;
                            cmbOption2.Visible = false;
                        }

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "",
                            "Haywire Mine"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Reiver")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Bolt Carbine",
                            "Combat Blade"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Eliminator")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Bolt Sniper Rifle",
                            "Las Fusil"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    else
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption3"].Visible = false;

                        cmbOption1.SelectedIndex = -1;
                    }

                    antiLoop = false;
                    break;
            }

            Points = 0;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (var weapon in Weapons)
            {
                if (weapon == "Infiltrator Sergeant" || weapon == "Infiltrator")
                {
                    Points += 20;
                }
                if (weapon == "Eliminator")
                {
                    Points += 25;
                }
                if (weapon == "Incursor")
                {
                    Points += 18;
                }
                if (weapon == "Reiver")
                {
                    Points += 16;
                }
            }
        }

        public override string ToString()
        {
            return "Spectrus Kill Team - " + Points + "pts";
        }
    }
}
