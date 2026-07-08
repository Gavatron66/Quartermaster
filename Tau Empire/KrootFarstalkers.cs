using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class KrootFarstalkers : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;

        public KrootFarstalkers()
        {
            DEFAULT_POINTS = 80;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Pulse Rifle and Ritual Blade");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Kroot Rifle");
            }
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "KROOT",
                "INFANTRY", "T'AU AUXILIARY", "KROOT FARSTALKERS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new KrootFarstalkers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as T_au;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Kroot Kill-broker w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Kroot Farstalker w/ " + Weapons[i]);
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
                            lbModelSelect.Items[currentIndex] = "Kroot Kill-broker w/ " + Weapons[currentIndex];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Kroot Farstalker w/ " + Weapons[currentIndex];
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
                                "Kroot Rifle",
                                "Pulse Carbine",
                                "Pulse Rifle and Ritual Blade"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Accelerator Bow",
                                "Dvorgite Skinner (+10 pts)",
                                "Kroot Hunting Rifle",
                                "Kroot Rifle",
                                "Kroot Scattergun",
                                "Londaxi Tribalest (+10 pts)",
                                "Pech'ra (+10 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                            if (specialW == 2 && Weapons[currentIndex] != "Kroot Scattergun")
                            {
                                restrictedIndexes.Add(4);
                            }
                            
                            if(Weapons.Contains("Kroot Hunting Rifle") && Weapons[currentIndex] != "Kroot Hunting Rifle")
                            {
                                restrictedIndexes.Add(2);
                            }

                            if (Weapons.Contains("Accelerator Bow") && Weapons[currentIndex] != "Accelerator Bow")
                            {
                                restrictedIndexes.Add(0);
                            }

                            if ((Weapons.Contains("Dvorgite Skinner (+10 pts)") || Weapons.Contains("Londaxi Tribalest (+10 pts)")) 
                                && (Weapons[currentIndex] != "Dvorgite Skinner (+10 pts)" && Weapons[currentIndex] != "Londaxi Tribalest (+10 pts)"))
                            {
                                restrictedIndexes.Add(1);
                                restrictedIndexes.Add(5);
                            }

                            if (Weapons.Contains("Pech'ra (+10 pts)") && Weapons[currentIndex] != "Pech'ra (+10 pts)")
                            {
                                restrictedIndexes.Add(6);
                            }

                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS;

            specialW = 0;
            foreach (var weapon in Weapons)
            {
                if (weapon == "Kroot Scattergun")
                {
                    specialW++;
                }
                else if (weapon.Contains("(+10 pts)"))
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Kroot Farstalkers - " + Points + "pts";
        }
    }
}
