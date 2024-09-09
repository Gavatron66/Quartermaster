using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class CadianShockTroops : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;
        bool vox;
        List<string> specialWeapons;

        public CadianShockTroops()
        {
            DEFAULT_POINTS = 65;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Laspistol and Chainsword");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "CADIAN",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "INFANTRY SQUAD", "SHOCK TROOPS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new CadianShockTroops();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Shock Trooper Sergeant w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Shock Trooper w/ " + Weapons[i]);
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
                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Shock Trooper Sergeant w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Shock Trooper w/ " + Weapons[currentIndex];
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
                        if(currentIndex == 0)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Bolt Pistol and Chainsword",
                                "Drum-fed Autogun",
                                "Laspistol and Chainsword"
                            });
                        }
                        else if (!Weapons[currentIndex].Contains("Lasgun") || specialW < 2)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Lasgun",
                                "Lasgun and Vox-caster",
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
                                "Lasgun and Vox-caster",
                            });
                        }

                        if(vox && !(Weapons[currentIndex] == "Lasgun and Vox-caster"))
                        {
                            cmbOption1.Items.Remove("Lasgun and Vox-caster");
                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS;

            vox = false;
            specialW = 0;
            specialWeapons = new List<string>();
            foreach (var weapon in Weapons)
            {
                if(weapon != Weapons[0])
                {
                    if(weapon.Contains("Vox-caster"))
                    {
                        vox = true;
                    }

                    if(!weapon.Contains("Lasgun"))
                    {
                        specialW++;
                        specialWeapons.Add(weapon);
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Cadian Shock Troops - " + Points + "pts";
        }
    }
}
