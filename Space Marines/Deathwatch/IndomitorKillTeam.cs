using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class IndomitorKillTeam : Datasheets
    {
        int currentIndex;
        int[] restrict = new int[4] { 0, 0, 0, 0 };

        public IndomitorKillTeam()
        {
            DEFAULT_POINTS = 23;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            Weapons.Add("Heavy Bolt Rifle");
            Weapons.Add("Heavy Intercessor Sergeant");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Heavy Bolt Rifle");
                Weapons.Add("Heavy Intercessor");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DEATHWATCH",
                "INFANTRY", "CORE", "PRIMARIS", "MK X GRAVIS", "KILL TEAM", "INDOMITOR"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new IndomitorKillTeam();
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
            lbModelSelect.Items.Add(Weapons[1] + " w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add(Weapons[(i * 2) + 1] + " w/ " + Weapons[(i * 2)]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Aggressor",
                "Eradicator",
                "Heavy Intercessor",
                "Inceptor",
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
                    if (Weapons[(currentIndex * 2) + 1] == "Heavy Intercessor")
                    {
                        Weapons[(currentIndex * 2)] = "Heavy Bolt Rifle";
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Aggressor")
                    {
                        Weapons[(currentIndex * 2)] = "Auto Boltstorm Gauntlets";
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Eradicator")
                    {
                        Weapons[(currentIndex * 2)] = "Melta Rifle";
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Inceptor")
                    {
                        Weapons[(currentIndex * 2)] = "Assault Bolters";
                    }
                    lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1] + " w/ " + Weapons[(currentIndex * 2)];

                    lbModelSelect.SelectedIndex = currentIndex;
                    break;
                case 12:
                    Weapons[currentIndex * 2] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1] + " w/ " + Weapons[(currentIndex * 2)];
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

                    if (Weapons[(currentIndex * 2) + 1] == "Heavy Intercessor Sergeant")
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Heavy Auto Bolt Rifle",
                            "Heavy Bolt Rifle",
                            "Heavy Stalker Bolt Rifle"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Heavy Intercessor")
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

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Executor Heavy Bolter",
                            "Heavy Auto Bolt Rifle",
                            "Heavy Bolter",
                            "Heavy Bolt Rifle",
                            "Heavy Stalker Bolt Rifle",
                            "Hellstorm Heavy Bolter"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);

                        if (!Weapons[currentIndex * 2].Contains("Bolter") && restrict[0] == restrict[3] / 5)
                        {
                            cmbOption2.Items.Remove("Executor Heavy Bolter");
                            cmbOption2.Items.Remove("Heavy Bolter");
                            cmbOption2.Items.Remove("Hellstorm Heavy Bolter");
                        }
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Aggressor")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Auto Boltstorm Gauntlets",
                            "Flamestorm Gauntlets"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Eradicator")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Heavy Melta Rifle",
                            "Melta Rifle",
                            "Multi-melta"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);

                        if (!(Weapons[currentIndex * 2] == "Multi-melta") && restrict[1] == restrict[2] / 3)
                        {
                            cmbOption2.Items.Remove("Multi-melta");
                        }
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Inceptor")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Assault Bolters",
                            "Plasma Exterminators"
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

            restrict[0] = 0;
            restrict[1] = 0;
            restrict[2] = 0;
            restrict[3] = 0;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Heavy Intercessor" || weapon == "Heavy Intercessor Sergeant")
                {
                    Points += 23;
                    restrict[3]++;
                }
                if (weapon == "Inceptor")
                {
                    Points += 40;
                }
                if (weapon == "Eradicator")
                {
                    Points += 45;
                    restrict[2]++;
                }
                if (weapon == "Aggressor")
                {
                    Points += 30;
                }

                if(weapon == "Heavy Bolter" || weapon == "Hellstorm Heavy Bolter" || weapon == "Executor Heavy Bolter")
                {
                    restrict[0]++;
                }

                if(weapon == "Multi-melta")
                {
                    restrict[1]++;
                }
            }

            if (restrict[1] != 0 && restrict[2] < 3)
            {
                int temp = Weapons.IndexOf("Multi-melta");
                Weapons[temp] = "Melta Rifle";
                restrict[1]--;
                lbModelSelect.Items[(temp / 2)] = Weapons[temp + 1] + " w/ " + Weapons[temp];
            }
        }

        public override string ToString()
        {
            return "Indomitor Kill Team - " + Points + "pts";
        }
    }
}
