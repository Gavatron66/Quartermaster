using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class TempestusScions : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;
        bool vox;
        List<string> specialWeapons;

        public TempestusScions()
        {
            DEFAULT_POINTS = 11;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            Weapons.Add("Hot-shot Laspistol");
            Weapons.Add("Chainsword");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Hot-shot Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "MILITARUM TEMPESTUS",
                "INFANTRY", "CORE", "PLATOON", "TEMPESTUS SCIONS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new TempestusScions();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Tempestor w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 2; i <= UnitSize; i++)
            {
                lbModelSelect.Items.Add("Tempestus Scion w/ " + Weapons[i]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Chainsword",
                "Power Fist (+5 pts)",
                "Power Sword"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (isLoading)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Tempestor w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Tempestus Scion w/ " + Weapons[currentIndex + 1];
                    }

                    break;
                case 12:
                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Tempestor w/ " + Weapons[currentIndex] + " and " + Weapons[currentIndex + 1];
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Hot-shot Lasgun");
                        lbModelSelect.Items.Add("Tempestus Scion w/ " + Weapons[temp + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize + 1, 1);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    else
                    {
                        isLoading = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;

                        cmbOption1.Items.Clear();
                        if (currentIndex == 0)
                        {
                            cmbOption2.Visible = true;
                            panel.Controls["lblOption2"].Visible = true;
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Bolt Pistol",
                                "Hot-shot Laspistol",
                                "Plasma Pistol",
                            });
                        }
                        else if (!Weapons[currentIndex + 1].Contains("Hot-shot Lasgun") || specialW < ((UnitSize / 5) * 2))
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Hot-shot Lasgun",
                                "Hot-shot Laspistol and Vox-caster",
                                "Hot-shot Volley Gun",
                                "Meltagun",
                                "Plasma Gun"
                            });

                            foreach (var weapon in Weapons)
                            {
                                if (Weapons[currentIndex + 1] != weapon)
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
                                "Hot-shot Laspistol and Vox-caster"
                            });
                        }

                        if (vox && !(Weapons[currentIndex + 1] == "Hot-shot Laspistol and Vox-caster"))
                        {
                            cmbOption1.Items.Remove("Hot-shot Laspistol and Vox-caster");
                        }

                        if(currentIndex == 0)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        }

                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS * UnitSize;

            vox = false;
            specialW = 0;
            specialWeapons = new List<string>();
            foreach (var weapon in Weapons)
            {
                if (weapon != Weapons[0] && weapon != Weapons[1])
                {
                    if (weapon.Contains("Vox-caster"))
                    {
                        vox = true;
                    }

                    if (!weapon.Contains("Hot-shot") || weapon == "Hot-shot Volley Gun")
                    {
                        specialW++;
                        specialWeapons.Add(weapon);
                    }
                }
            }

            if (Weapons[1] == "Power Fist (+5 pts)")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Tempestus Scions - " + Points + "pts";
        }
    }
}
