using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Kasrkin : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;
        List<string> specialWeapons;
        bool[] special;

        public Kasrkin()
        {
            DEFAULT_POINTS = 100;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Hot-shot Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "CADIAN",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "KASRKIN"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Kasrkin();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Kasrkin Sergeant w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Kasrkin Trooper w/ " + Weapons[i]);
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
                    Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Kasrkin Sergeant w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Kasrkin Trooper w/ " + Weapons[currentIndex];
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

                        cmbOption1.Items.Clear();
                        if (currentIndex == 0)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Bolt Pistol",
                                "Bolt Pistol and Chainsword",
                                "Bolt Pistol and Power Sword (+5 pts)",
                                "Hot-shot Laspistol",
                                "Hot-shot Laspistol and Chainsword",
                                "Hot-shot Laspistol and Power Sword",
                                "Plasma Pistol (+5 pts)",
                                "Plasma Pistol and Chainsword (+5 pts)",
                                "Plasma Pistol and Power Sword (+10 pts)",
                            });
                        }
                        else if (!Weapons[currentIndex].Contains("Lasgun") || specialW < 4)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Hot-shot Lasgun",
                                "Hot-shot Lasgun and Vox-caster",
                                "Hot-shot Laspistol and Melta Mine",
                                "Hot-shot Marksman Rifle",
                                "Hot-shot Volley Gun",
                                "Meltagun",
                                "Plasma Gun"
                            });

                            foreach (var weapon in Weapons)
                            {
                                if (Weapons[currentIndex] != weapon)
                                {
                                    cmbOption1.Items.Remove(weapon);
                                }
                            }
                        }
                        else
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Hot-shot Lasgun",
                                "Hot-shot Lasgun and Vox-caster",
                                "Hot-shot Laspistol and Melta Mine",
                                "Hot-shot Marksman Rifle",
                            });
                        }

                        if (special[0] && !(Weapons[currentIndex] == "Hot-shot Lasgun and Vox-caster"))
                        {
                            cmbOption1.Items.Remove("Hot-shot Lasgun and Vox-caster");
                        }
                        if (special[1] && !(Weapons[currentIndex] == "Hot-shot Marksman Rifle"))
                        {
                            cmbOption1.Items.Remove("Hot-shot Marksman Rifle");
                        }
                        if (special[2] && !(Weapons[currentIndex] == "Hot-shot Laspistol and Melta Mine"))
                        {
                            cmbOption1.Items.Remove("Hot-shot Laspistol and Melta Mine");
                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS;

            specialW = 0;
            special = new bool[3];
            specialWeapons = new List<string>();
            foreach (var weapon in Weapons)
            {
                if (weapon != Weapons[0])
                {
                    if (weapon.Contains("Vox-caster"))
                    {
                        special[0] = true;
                    }

                    if(weapon.Contains("Marksman"))
                    {
                        special[1] = true;
                    }

                    if (weapon.Contains("Melta Mine"))
                    {
                        special[2] = true;
                    }

                    if (!weapon.Contains("Lasgun"))
                    {
                        specialW++;
                        specialWeapons.Add(weapon);
                    }
                }
            }

            if (Weapons[0].Contains("Plasma Pistol"))
            {
                Points += 5;
            }

            if (Weapons[0].Contains("Power Sword"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Kasrkin - " + Points + "pts";
        }
    }
}
