using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari.Harlequins
{
    public class Troupe : Datasheets
    {
        int currentIndex;
        public Troupe()
        {
            DEFAULT_POINTS = 13;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            for(int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Shuriken Pistol");
                Weapons.Add("Harlequin's Blade");
            }
            Keywords.AddRange(new string[]
            {
                "AELDARI", "HARLEQUINS", "<SAEDATH>",
                "INFANTRY", "CORE", "HAYWIRE GRENADES", "TROUPE"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Troupe();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Harlequins;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 12;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Lead Player w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Player w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption2.Items.Clear();
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Lead Player w/ " + Weapons[(currentIndex) * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Player w/ " + Weapons[(currentIndex) * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Lead Player w/ " + Weapons[(currentIndex) * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Player w/ " + Weapons[(currentIndex) * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Shuriken Pistol");
                        Weapons.Add("Harlequin's Blade");
                        lbModelSelect.Items.Add("Player w/ " + Weapons[(UnitSize - 1) * 2] + " and " + Weapons[((UnitSize - 1) * 2) + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 2) - 1, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    WeaponsCheck(cmbOption1, cmbOption2);

                    if(cmbOption2.Items.Contains("Aeldari Power Sword (+5 pts)") && !(currentIndex == 0))
                    {
                        cmbOption2.Items.Remove("Aeldari Power Sword (+5 pts)");
                    }
                    else if (!(cmbOption2.Items.Contains("Aeldari Power Sword (+5 pts)")) && currentIndex == 0)
                    {
                        cmbOption2.Items.Insert(0, "Aeldari Power Sword (+5 pts)");
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach(string weapon in Weapons)
            {
                if(weapon == "Aeldari Power Sword (+5 pts)" || weapon == "Fusion Pistol (+5 pts)" || weapon == "Harlequin's Caress (+5 pts)"
                     || weapon == "Harlequin's Embrace (+5 pts)" || weapon == "Harlequin's Kiss (+5 pts)" || weapon == "Neuro Disruptor (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Troupe - " + Points + "pts";
        }

        private void WeaponsCheck(ComboBox cmbOption1, ComboBox cmbOption2)
        {
            int[] weaponsCheck = new int[4] { 0, 0, 0, 0 };
            //Neuro Disruptor (+5 pts)
            //Fusion Pistol (+5 pts)
            //Harlequin's Caress (+5 pts)
            //Harlequin's Embrace (+5 pts)

            if(!(cmbOption1.Items.Contains("Shuriken Pistol")))
            {
                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Fusion Pistol (+5 pts)",
                    "Neuro Disruptor (+5 pts)",
                    "Shuriken Pistol"
                });

                cmbOption2.Items.Clear();
                cmbOption2.Items.AddRange(new string[]
                {
                    "Harlequin's Blade",
                    "Harlequin's Caress (+5 pts)",
                    "Harlequin's Embrace (+5 pts)",
                    "Harlequin's Kiss (+5 pts)"
                });
            }

            foreach (string weapon in Weapons)
            {
                if(weapon == "Neuro Disruptor (+5 pts)")
                {
                    weaponsCheck[0]++;
                }

                if (weapon == "Fusion Pistol (+5 pts)")
                {
                    weaponsCheck[1]++;
                }

                if (weapon == "Harlequin's Caress (+5 pts)")
                {
                    weaponsCheck[2]++;
                }

                if (weapon == "Harlequin's Embrace (+5 pts)")
                {
                    weaponsCheck[3]++;
                }
            }

            if(UnitSize <= 10)
            {
                if (weaponsCheck[0] == 2 && Weapons[currentIndex * 2] != "Neuro Disruptor (+5 pts)")
                {
                    cmbOption1.Items.Remove("Neuro Disruptor (+5 pts)");
                }

                if (weaponsCheck[1] == 2 && Weapons[currentIndex * 2] != "Fusion Pistol (+5 pts)")
                {
                    cmbOption1.Items.Remove("Fusion Pistol (+5 pts)");
                }

                if (weaponsCheck[2] == 2 && Weapons[(currentIndex * 2) + 1] != "Harlequin's Caress (+5 pts)")
                {
                    cmbOption2.Items.Remove("Harlequin's Caress (+5 pts)");
                }

                if (weaponsCheck[3] == 2 && Weapons[(currentIndex * 2) + 1] != "Harlequin's Embrace (+5 pts)")
                {
                    cmbOption2.Items.Remove("Harlequin's Embrace (+5 pts)");
                }
            }
            else if (UnitSize > 10)
            {
                if (weaponsCheck[0] == 4 && Weapons[currentIndex * 2] != "Neuro Disruptor (+5 pts)")
                {
                    cmbOption1.Items.Remove("Neuro Disruptor (+5 pts)");
                }

                if (weaponsCheck[1] == 4 && Weapons[currentIndex * 2] != "Fusion Pistol (+5 pts)")
                {
                    cmbOption1.Items.Remove("Fusion Pistol (+5 pts)");
                }

                if (weaponsCheck[2] == 4 && Weapons[(currentIndex * 2) + 1] != "Harlequin's Caress (+5 pts)")
                {
                    cmbOption2.Items.Remove("Harlequin's Caress (+5 pts)");
                }

                if (weaponsCheck[3] == 4 && Weapons[(currentIndex * 2) + 1] != "Harlequin's Embrace (+5 pts)")
                {
                    cmbOption2.Items.Remove("Harlequin's Embrace (+5 pts)");
                }
            }
        }
    }
}
