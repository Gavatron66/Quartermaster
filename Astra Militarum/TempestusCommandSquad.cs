using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class TempestusCommandSquad : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;
        bool vox;
        List<string> specialWeapons;

        public TempestusCommandSquad()
        {
            DEFAULT_POINTS = 95;
            UnitSize = 5;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Hot-shot Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "MILITARUM TEMPESTUS",
                "INFANTRY", "CHARACTER", "OFFICER", "TEMPESTOR PRIME",
                "INFANTRY", "COMMAND SQUAD"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new TempestusCommandSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Tempestor Prime w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Tempestus Scion w/ " + Weapons[i]);
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
                        lbModelSelect.Items[currentIndex] = "Tempestor Prime w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Tempestus Scion w/ " + Weapons[currentIndex];
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
                                "Plasma Pistol (+5 pts)",
                                "Tempestus Command Rod (+5 pts)"
                            });
                        }
                        else
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Grenade Launcher",
                                "Hot-shot Lasgun",
                                "Hot-shot Lasgun and Regimental Standard",
                                "Hot-shot Lasgun and Medi-pack",
                                "Hot-shot Laspistol and Master Vox",
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

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0] != "Bolt Pistol")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Tempestus Command Squad - " + Points + "pts";
        }
    }
}
