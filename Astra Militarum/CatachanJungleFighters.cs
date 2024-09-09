using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class CatachanJungleFighters : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;
        bool vox;

        public CatachanJungleFighters()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Laspistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Lasgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "CATACHAN",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "INFANTRY SQUAD", "JUNGLE FIGHTERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new CatachanJungleFighters();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Jungle Fighter Sergeant w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Jungle Fighter w/ " + Weapons[i]);
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
                    lbModelSelect.Items[currentIndex] = "Jungle Fighter w/ " + Weapons[currentIndex];

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex <= 0)
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
                        if (!Weapons[currentIndex].Contains("Lasgun") || specialW < 2)
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Flamer",
                                "Lasgun",
                                "Lasgun and Vox-caster",
                            });
                        }
                        else
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Lasgun",
                                "Lasgun and Vox-caster",
                            });
                        }

                        if (vox && !(Weapons[currentIndex] == "Lasgun and Vox-caster"))
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
            foreach (var weapon in Weapons)
            {
                if (weapon != Weapons[0])
                {
                    if (weapon.Contains("Vox-caster"))
                    {
                        vox = true;
                    }

                    if (!weapon.Contains("Lasgun"))
                    {
                        specialW++;
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Catachan Jungle Fighters - " + Points + "pts";
        }
    }
}
