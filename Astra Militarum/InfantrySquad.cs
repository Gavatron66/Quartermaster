using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class InfantrySquad : Datasheets
    {
        int currentIndex;
        bool voxCaster = false;
        bool specialW = false;

        public InfantrySquad()
        {
            DEFAULT_POINTS = 65;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("Laspistol");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "CORE", "PLATOON", "INFANTRY SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new InfantrySquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            lbModelSelect.Items.Clear();

            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Sergeant w/ " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Guardsman w/ " + Weapons[i + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Chainsword",
                "Power Sword (+5 pts)"
            });

            cbOption1.Visible = true;
            cbOption1.Text = "Include a Heavy Weapons Team";

            if (Weapons[9] == "HWT")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Sergeant w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else if (currentIndex == 8 && Weapons[9] == "HWT")
                    {
                        Weapons[10] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[8] = "Heavy Weapons Team w/ " + Weapons[10];
                    }
                    else
                    {
                        Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[currentIndex + 1] != "Lasgun" || Weapons[currentIndex + 1] != "Lasgun and Vox-caster")
                        {
                            specialW = true;
                        }
                        else
                        {
                            specialW = false;
                        }

                        if (Weapons[currentIndex + 1] == "Lasgun and Vox-caster")
                        {
                            voxCaster = true;
                        }
                        else
                        {
                            voxCaster = false;
                        }

                        lbModelSelect.Items[currentIndex] = "Guardsman w/ " + Weapons[currentIndex + 1];
                    }
                    break;
                case 12:
                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[1] == "(None)")
                    {
                        lbModelSelect.Items[0] = "Sergeant w/ " + Weapons[0];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[9] = "HWT";
                        Weapons[10] = "Heavy Bolter";
                        lbModelSelect.Items.RemoveAt(9);

                        lbModelSelect.Items[8] = "Heavy Weapons Team w/ " + Weapons[10];
                    }
                    else
                    {
                        Weapons[9] = "Lasgun";
                        Weapons[10] = "Lasgun";
                        lbModelSelect.Items.RemoveAt(9);

                        lbModelSelect.Items.Add("Veteran Guardsman w/ " + Weapons[9]);
                        lbModelSelect.Items.Add("Veteran Guardsman w/ " + Weapons[10]);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;

                        if (currentIndex == 0)
                        {
                            cmbOption2.Visible = true;
                            panel.Controls["lblOption2"].Visible = true;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Bolt Pistol",
                                "Boltgun",
                                "Laspistol",
                                "Plasma Pistol (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        }
                        else if (currentIndex == 8 && Weapons[9] == "HWT")
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Autocannon",
                                "Heavy Bolter",
                                "Lascannon",
                                "Missile Launcher",
                                "Mortar"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[10]);
                        }
                        else
                        {
                            cmbOption2.Visible = false;
                            panel.Controls["lblOption2"].Visible = false;

                            cmbOption1.Items.Clear();

                            if(!specialW)
                            {
                                cmbOption1.Items.AddRange(new string[]
                                {
                                    "Flamer",
                                    "Grenade Launcher",
                                    "Lasgun",
                                    "Lasgun and Vox-caster",
                                    "Meltagun",
                                    "Plasma Gun",
                                    "Sniper Rifle"
                                });
                            }
                            else
                            {
                                if (Weapons[currentIndex + 1].Contains("Lasgun"))
                                {
                                    cmbOption1.Items.AddRange(new string[]
                                    {
                                        "Lasgun",
                                        "Lasgun and Vox-caster",
                                    });
                                }
                                else
                                {
                                    cmbOption1.Items.AddRange(new string[]
                                    {
                                    "Flamer",
                                    "Grenade Launcher",
                                    "Lasgun",
                                    "Lasgun and Vox-caster",
                                    "Meltagun",
                                    "Plasma Gun",
                                    "Sniper Rifle"
                                    });
                                }
                              
                            }

                            if(voxCaster && !Weapons[currentIndex + 1].Contains("Vox-caster"))
                            {
                                cmbOption1.Items.Remove("Lasgun and Vox-caster");
                            }

                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                        }

                        if(cmbOption1.Items.Count == 1)
                        {
                            cmbOption1.Enabled = false;
                        }
                        else
                        {
                            cmbOption1.Enabled = true;
                        }
                    }

                    antiLoop = false;
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Plasma Pistol (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Power Sword (+5 pts)"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Infantry Squad - " + Points + "pts";
        }
    }
}