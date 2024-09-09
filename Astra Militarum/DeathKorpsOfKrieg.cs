using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class DeathKorpsOfKrieg : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;
        bool mediPack;
        List<string> specialWeapons;

        public DeathKorpsOfKrieg()
        {
            DEFAULT_POINTS = 75;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Laspistol and Chainsword");
            Weapons.Add("Plasma Gun");
            for (int i = 2; i < UnitSize; i++)
            {
                Weapons.Add("Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "KRIEG",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "INFANTRY SQUAD", "DEATH KORPS OF KRIEG"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathKorpsOfKrieg();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Death Korps Watchmaster w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Death Korps Trooper w/ " + Weapons[i]);
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
                        lbModelSelect.Items[currentIndex] = "Death Korps Watchmaster w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Death Korps Trooper w/ " + Weapons[currentIndex];
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
                                "Boltgun",
                                "Bolt Pistol and Chainsword",
                                "Bolt Pistol and Power Sword (+5 pts)",
                                "Laspistol and Chainsword",
                                "Laspistol and Power Sword (+5 pts)",
                                "Plasma Pistol and Chainsword (+5 pts)",
                                "Plasma Pistol and Power Sword (+10 pts)",
                            });
                        }
                        else if (currentIndex == 1)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Lasgun and Vox-caster",
                                "Plasma Gun"
                            });
                        }
                        else if (!Weapons[currentIndex].Contains("Lasgun") || specialW < 2)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Lasgun",
                                "Lasgun and Medi-pack (+5 pts)",
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
                                "Lasgun",
                                "Lasgun and Medi-pack (+5 pts)",
                            });
                        }

                        if (mediPack && !(Weapons[currentIndex] == "Lasgun and Medi-pack (+5 pts)"))
                        {
                            cmbOption1.Items.Remove("Lasgun and Medi-pack (+5 pts)");
                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS;

            mediPack = false;
            specialW = 0;
            specialWeapons = new List<string>();

            foreach (var weapon in Weapons)
            {
                if (weapon != Weapons[0] && weapon != Weapons[1])
                {
                    if (weapon.Contains("Medi-pack"))
                    {
                        mediPack = true;
                        Points += 5;
                    }

                    if (!weapon.Contains("Lasgun"))
                    {
                        specialW++;
                        specialWeapons.Add(weapon);
                    }
                }
                else
                {
                    if (Weapons[0].Contains("Plasma Pistol"))
                    {
                        Points += 5;
                    }

                    if (Weapons[0].Contains("Power Sword"))
                    {
                        Points += 5;
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Death Korps of Krieg - " + Points + "pts";
        }
    }
}
