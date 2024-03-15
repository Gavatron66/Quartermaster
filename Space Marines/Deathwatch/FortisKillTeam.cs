using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class FortisKillTeam : Datasheets
    {
        int currentIndex;
        int restrict = 0;

        public FortisKillTeam()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add("Bolt Rifle");
            Weapons.Add("Intercessor Sergeant");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Rifle");
                Weapons.Add("Intercessor");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DEATHWATCH",
                "INFANTRY", "CORE", "PRIMARIS", "KILL TEAM", "FORTIS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new FortisKillTeam();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
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
                "Assault Intercessor",
                "Hellblaster",
                "Intercessor",
                "Outrider"
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

            cbOption1.Text = "Astartes Grenade Launcher";
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
                    if (Weapons[(currentIndex * 2) + 1] == "Intercessor")
                    {
                        Weapons[(currentIndex * 2)] = "Bolt Rifle";
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Assault Intercessor")
                    {
                        Weapons[(currentIndex * 2)] = "(None)";
                    }
                    else if(Weapons[(currentIndex * 2) + 1] == "Hellblaster")
                    {
                        Weapons[(currentIndex * 2)] = "Plasma Incinerator";
                    }
                    else if(Weapons[(currentIndex * 2) + 1] == "Outrider")
                    {
                        Weapons[(currentIndex * 2)] = "(None)";
                    }

                    if (Weapons[(currentIndex * 2) + 1] == "Outrider" || Weapons[(currentIndex * 2) + 1] == "Assault Intercessor")
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1] + " w/ " + Weapons[(currentIndex * 2)];
                    }

                    lbModelSelect.SelectedIndex = currentIndex;
                    break;
                case 12:
                    if (Weapons[currentIndex * 2].Contains("Grenade"))
                    {
                        Weapons[currentIndex * 2] = cmbOption2.SelectedItem.ToString() + " and " + cbOption1.Text;
                    }
                    else
                    {
                        Weapons[currentIndex * 2] = cmbOption2.SelectedItem.ToString();
                    }
                    lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1] + " w/ " + Weapons[(currentIndex * 2)];
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 2)] = Weapons[currentIndex * 2] + " and " + cbOption1.Text;
                        restrict++;
                    }
                    else
                    {
                        Weapons[currentIndex * 2] = Weapons[currentIndex * 2].Remove(Weapons[currentIndex * 2].IndexOf("and") - 1);
                        restrict--;
                    }
                    lbModelSelect.Items[currentIndex] = Weapons[(currentIndex * 2) + 1] + " w/ " + Weapons[(currentIndex * 2)];
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
                        cbOption1.Visible = false;
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    cmbOption2.Items.Clear();

                    if (Weapons[(currentIndex * 2) + 1] == "Intercessor Sergeant")
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = false;

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Bolt Rifle",
                            "Hand Flamer",
                            "Plasma Pistol",
                            "Power Fist",
                            "Power Sword",
                            "Thunder Hammer"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Intercessor")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;

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

                        if(restrict == 1 && !Weapons[currentIndex * 2].Contains("Grenade"))
                        {
                            cbOption1.Enabled = false;
                        }
                        else
                        {
                            cbOption1.Enabled = true;
                        }

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Auto Bolt Rifle",
                            "Bolt Rifle",
                            "Stalker Bolt Rifle"
                        });
                        if (Weapons[currentIndex * 2].Contains("Grenade"))
                        {
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2].Substring(0, Weapons[currentIndex * 2].IndexOf("and") - 1));
                            cbOption1.Checked = true;
                        }
                        else
                        {
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);
                            cbOption1.Checked = false;
                        }
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Assault Intercessor")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        cbOption1.Visible = false;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Outrider")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        cbOption1.Visible = false;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
                    else if (Weapons[(currentIndex * 2) + 1] == "Hellblaster")
                    {
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = false;

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Assault Plasma Incinerator",
                            "Heavy Plasma Incinerator",
                            "Plasma Incinerator"
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
                        cbOption1.Visible = false;

                        cmbOption1.SelectedIndex = -1;
                    }

                    antiLoop = false;
                    break;
            }

            Points = 0;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (var weapon in Weapons)
            {
                if(weapon == "Intercessor" || weapon == "Intercessor Sergeant")
                {
                    Points += 18;
                }
                if (weapon == "Hellblaster")
                {
                    Points += 30;
                }
                if (weapon == "Outrider")
                {
                    Points += 35;
                }
                if (weapon == "Assault Intercessor")
                {
                    Points += 17;
                }
            }
        }

        public override string ToString()
        {
            return "Fortis Kill Team - " + Points + "pts";
        }
    }
}
