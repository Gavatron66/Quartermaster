using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class TraitorGuard : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;

        public TraitorGuard()
        {
            DEFAULT_POINTS = 60;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Laspistol and Improvised Blade");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TRAITORIS ASTARTES", "<LEGION>",
                "INFANTRY", "CULTISTS", "TRAITOR GUARDSMEN SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new TraitorGuard();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Traitor Sergeant w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Traitor Guardsman w/ " + Weapons[i]);
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
                            lbModelSelect.Items[currentIndex] = "Traitor Sergeant w/ " + Weapons[currentIndex];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Traitor Guardsman w/ " + Weapons[currentIndex];
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
                                "Autopistol and Improvised Blade",
                                "Autopistol and Power Sword (+5 pts)",
                                "Boltgun",
                                "Bolt Pistol and Chainsword",
                                "Bolt Pistol and Improvised Blade",
                                "Bolt Pistol and Power Sword (+5 pts)",
                                "Laspistol and Chainsword",
                                "Laspistol and Improvised Blade",
                                "Laspistol and Power Sword (+5 pts)",
                                "Plasma Pistol and Chainsword (+5 pts)",
                                "Plasma Pistol and Improvised Blade (+5 pts)",
                                "Plasma Pistol and Power Sword (+10 pts)",
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Cultist Grenade Launcher",
                                "Cultist Sniper Rifle",
                                "Flamer",
                                "Lasgun",
                                "Lasgun and Vox-caster",
                                "Meltagun",
                                "Plasma Gun"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                            if (Weapons.Contains("Lasgun and Vox-caster") && Weapons[currentIndex] != "Lasgun and Vox-caster")
                            {
                                restrictedIndexes.Add(4);
                            }

                            if (specialW == 4 && Weapons[currentIndex].Contains("Lasgun"))
                            {
                                //specialW must be a 4; it counts the Sergeant's weapon too
                                restrictedIndexes.AddRange(new int[] { 0, 1, 2, 5, 6 });
                            }

                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0].Contains("(+5 pts)"))
            {
                Points += 5;
            }
            else if (Weapons[0].Contains("(+10 pts)"))
            {
                Points += 10;
            }

            specialW = 0;
            foreach (var weapon in Weapons)
            {
                if(!weapon.Contains("Lasgun"))
                {
                    specialW++;
                }
            }
        }

        public override string ToString()
        {
            return "Traitor Guardsmen Squad - " + Points + "pts";
        }
    }
}
