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
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Shock Trooper Sergeant w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Shock Trooper w/ " + Weapons[i]);
            }

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, panel.Controls["cmbOption1"].Location.Y + 32);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            antiLoop = true;
            if (Stratagem.Contains(cbStratagem5.Text))
            {
                cbStratagem5.Checked = true;
                cbStratagem5.Enabled = true;

                panel.Controls["lblRelic"].Visible = true;
                cmbRelic.Visible = true;

                if (Relic == "(None)")
                {
                    cmbRelic.SelectedIndex = 0;
                }
                else
                {
                    if (Relic != null && cmbRelic.Items.Contains(Relic))
                    {
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                    }
                    else
                    {
                        cmbRelic.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                cbStratagem5.Checked = false;
                cmbRelic.SelectedIndex = 0;
            }

            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
            antiLoop = false;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (isLoading)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();

                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = "Shock Trooper Sergeant w/ " + Weapons[currentIndex];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Shock Trooper w/ " + Weapons[currentIndex];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                    }

                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    restrictedIndexes.Clear();

                    if(chosenRelic == "Legacy of Kalladius")
                    {
                        cmbOption1.SelectedIndex = 2;
                        restrictedIndexes.Add(1);
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                    Relic = chosenRelic;
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
                    }
                    else
                    {
                        isLoading = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        restrictedIndexes.Clear();
                        cmbOption1.Items.Clear();
                        if(currentIndex == 0)
                        {
                            cbStratagem5.Visible = true;

                            if (Stratagem.Contains(cbStratagem5.Text))
                            {
                                panel.Controls["lblRelic"].Visible = true;
                                cmbRelic.Visible = true;
                            }

                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Bolt Pistol and Chainsword",
                                "Drum-fed Autogun",
                                "Laspistol and Chainsword"
                            });

                            if(Relic == "Legacy of Kalladius")
                            {
                                restrictedIndexes.Add(1);
                            }
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
                                "Plasma Gun"
                            });

                            if(specialWeapons.Count == 2 && Weapons[currentIndex].Contains("Lasgun"))
                            {
                                restrictedIndexes.AddRange(new int[] { 0, 1, 4, 5 });
                            }
                            else if (specialWeapons.Count == 2)
                            {
                                if (specialWeapons.Contains(Weapons[currentIndex]))
                                {
                                    restrictedIndexes.Remove(cmbOption1.Items.IndexOf(Weapons[currentIndex]));
                                }

                                if (specialWeapons[0] == Weapons[currentIndex])
                                {
                                    restrictedIndexes.Add(cmbOption1.Items.IndexOf(specialWeapons[1]));
                                }
                                else if (specialWeapons[1] == Weapons[currentIndex])
                                {
                                    restrictedIndexes.Add(cmbOption1.Items.IndexOf(specialWeapons[0]));
                                }
                            }
                            else if (specialWeapons.Count == 1 && Weapons[currentIndex] != specialWeapons[0])
                            {
                                restrictedIndexes.Add(cmbOption1.Items.IndexOf(specialWeapons[0]));
                            }
                        }

                        if(vox && !(Weapons[currentIndex] == "Lasgun and Vox-caster"))
                        {
                            restrictedIndexes.Add(cmbOption1.Items.IndexOf("Lasgun and Vox-caster"));
                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        isLoading = false;
                        break;
                    }
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        Stratagem.Add(cbStratagem5.Text);
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            Stratagem.Remove(cbStratagem5.Text);
                        }
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.SelectedIndex = 0;
                    }
                    break;
            }

            Points = DEFAULT_POINTS;

            vox = false;
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
