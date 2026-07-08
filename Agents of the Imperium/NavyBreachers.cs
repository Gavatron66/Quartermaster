using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class NavyBreachers : Datasheets
    {
        int currentIndex;
        bool isLoading = false;

        public NavyBreachers()
        {
            DEFAULT_POINTS = 110;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Navis Shotgun");
            Weapons.Add("Navis Las-volley");
            Weapons.Add("Navis Heavy Shotgun and Endurant Shield");
            for (int i = 3; i < UnitSize; i++)
            {
                Weapons.Add("Navis Shotgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "NAVIS IMPERIALIS", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "CORE", "IMPERIAL NAVY BREACHERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new NavyBreachers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ImperialAgents;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Navis Sergeant-at-Arms w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Navis Armsman w/ " + Weapons[i]);
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (isLoading)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = "Navis Sergeant-at-Arms w/ " + Weapons[currentIndex];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Navis Armsman w/ " + Weapons[currentIndex];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        break;
                    }
                    else
                    {
                        isLoading = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;

                        restrictedIndexes.Clear();
                        if (currentIndex == 0)
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Autopistol and Chainsword",
                                "Bolt Pistol and Power Sword",
                                "Navis Shotgun"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else if(currentIndex == 1)
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Meltagun (+5 pts)",
                                "Navis Las-volley",
                                "Plasma Gun (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);
                        }
                        else if(currentIndex == 2)
                        {
                            cmbOption1.Visible = false;
                            panel.Controls["lblOption1"].Visible = false;
                        }
                        else
                        {

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Autopistol and Chainfist",
                                "Autopistol and Power Axe",
                                "Navis Shotgun",
                                "Navis Shotgun and Explosives (+10 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                            restrictedIndexes.Clear();
                            if (Weapons.Contains("Autopistol and Chainfist") && Weapons[currentIndex] != "Autopistol and Chainfist")
                            {
                                restrictedIndexes.Add(0);
                            }
                            if (Weapons.Contains("Autopistol and Power Axe") && Weapons[currentIndex] != "Autopistol and Power Axe")
                            {
                                restrictedIndexes.Add(1);
                            }
                            if (Weapons.Contains("Navis Shotgun and Explosives (+10 pts)") && Weapons[currentIndex] != "Navis Shotgun and Explosives (+10 pts)")
                            {
                                restrictedIndexes.Add(3);
                            }

                            if (restrictedIndexes.Count == 3 && Weapons[currentIndex] == "Navis Shotgun")
                            {
                                cmbOption1.Enabled = false;
                            }
                            else
                            {
                                cmbOption1.Enabled = true;
                            }

                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        }
                            
                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS;

            if(Weapons.Contains("Navis Shotgun and Explosives (+10 pts)"))
            {
                Points += 10;
            }

            if (Weapons[1] != "Navis Las-volley")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Imperial Navy Breachers - " + Points + "pts";
        }
    }
}
