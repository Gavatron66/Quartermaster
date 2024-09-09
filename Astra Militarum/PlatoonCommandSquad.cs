using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class PlatoonCommandSquad : Datasheets
    {
        int currentIndex;
        List<string> restrictionArray = new List<string>();

        public PlatoonCommandSquad()
        {
            DEFAULT_POINTS = 15;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
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
                "INFANTRY", "CHARACTER", "OFFICER", "REGIMENTAL", "COMMANDER",
                "INFANTRY", "REGIMENTAL", "COMMAND SQUAD"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new PlatoonCommandSquad();
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
                lbModelSelect.Items.Add("Platoon Commander w/ " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Platoon Commander w/ " + Weapons[0] + " and " + Weapons[1]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Veteran Guardsman w/ " + Weapons[i + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Chainsword",
                "Power Fist (+5 pts)",
                "Power Sword"
            });

            cbOption1.Visible = true;
            cbOption1.Text = "Include a Veteran Heavy Weapons Team";

            if (Weapons[4] == "VHWT")
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
            if(antiLoop)
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
                            lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else if (currentIndex == 3 && Weapons[4] == "VHWT")
                    {
                        Weapons[5] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[3] = "Veteran Heavy Weapons Team w/ " + Weapons[5];
                    }
                    else
                    {
                        Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Veteran Guardsman w/ " + Weapons[currentIndex + 1];
                    }
                    break;
                case 12:
                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[1] == "(None)")
                    {
                        lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Platoon Commander w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[4] = "VHWT";
                        Weapons[5] = "Heavy Bolter";
                        lbModelSelect.Items.RemoveAt(4);

                        lbModelSelect.Items[3] = "Veteran Heavy Weapons Team w/ " + Weapons[5];
                    }
                    else
                    {
                        Weapons[4] = "Lasgun";
                        Weapons[5] = "Lasgun";
                        lbModelSelect.Items.RemoveAt(3);

                        lbModelSelect.Items.Add("Veteran Guardsman w/ " + Weapons[4]);
                        lbModelSelect.Items.Add("Veteran Guardsman w/ " + Weapons[5]);
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

                        if(currentIndex == 0)
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
                        else if (currentIndex == 3 && Weapons[4] == "VHWT")
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
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[5]);
                        }
                        else
                        {
                            cmbOption2.Visible = false;
                            panel.Controls["lblOption2"].Visible = false;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Chainsword",
                                "Chainsword and Master Vox",
                                "Chainsword and Medi-pack",
                                "Chainsword and Regimental Standard",
                                "Flamer",
                                "Grenade Launcher",
                                "Heavy Flamer",
                                "Lasgun",
                                "Lasgun and Master Vox",
                                "Lasgun and Medi-pack",
                                "Lasgun and Regimental Standard",
                                "Meltagun",
                                "Plasma Gun",
                                "Sniper Rifle"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                            foreach(var weapon in restrictionArray)
                            {
                                if(weapon != Weapons[currentIndex + 1])
                                {
                                    if(weapon == "Master Vox" && !Weapons[currentIndex + 1].Contains(weapon))
                                    {
                                        cmbOption1.Items.Remove("Lasgun and Master Vox");
                                        cmbOption1.Items.Remove("Chainsword and Master Vox");
                                    }
                                    else if (weapon == "Medi-pack" && !Weapons[currentIndex + 1].Contains(weapon))
                                    {
                                        cmbOption1.Items.Remove("Lasgun and Medi-pack");
                                        cmbOption1.Items.Remove("Chainsword and Medi-pack");
                                    }
                                    else if (weapon == "Regimental Standard" && !Weapons[currentIndex + 1].Contains(weapon))
                                    {
                                        cmbOption1.Items.Remove("Lasgun and Regimental Standard");
                                        cmbOption1.Items.Remove("Chainsword and Regimental Standard");
                                    }
                                    else
                                    {
                                        cmbOption1.Items.Remove(weapon);
                                    }
                                }

                            }
                        }
                    }

                    antiLoop = false;
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if(Weapons.Contains("Plasma Pistol (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Power Fist (+5 pts)"))
            {
                Points += 5;
            }

            restrictionArray.Clear();
            for(int i = 2; i < Weapons.Count; i++)
            {
                if (Weapons[i] != "Lasgun" && Weapons[i] != "Chainsword")
                {
                    if (Weapons[i].Contains("Master Vox"))
                    {
                        restrictionArray.Add("Master Vox");
                    }
                    else if (Weapons[i].Contains("Medi-pack"))
                    {
                        restrictionArray.Add("Medi-pack");
                    }
                    else if (Weapons[i].Contains("Regimental Standard"))
                    {
                        restrictionArray.Add("Regimental Standard");
                    }
                    else
                    {
                        restrictionArray.Add(Weapons[i]);
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Platoon Command Squad - " + Points + "pts";
        }
    }
}