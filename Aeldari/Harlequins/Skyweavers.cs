using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari.Harlequins
{
    public class Skyweavers : Datasheets
    {
        int currentIndex;
        public Skyweavers()
        {
            DEFAULT_POINTS = 45;
            UnitSize = 2;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Shuriken Cannon");
                Weapons.Add("Star Bolas");
            }
            Keywords.AddRange(new string[]
            {
                "AELDARI", "HARLEQUINS", "<SAEDATH>",
                "BIKER", "CORE", "FLY", "SKYWEAVERS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Skyweavers();
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
            nudUnitSize.Minimum = 2;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Skyweaver w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Shuriken Cannon",
                "Skyweaver Haywire Cannon (+5 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Star Bolas",
                "Zephyrglaive (+5 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
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
                    lbModelSelect.Items[currentIndex] = "Skyweaver w/ " + Weapons[(currentIndex) * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Skyweaver w/ " + Weapons[(currentIndex) * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Shuriken Cannon");
                        Weapons.Add("Star Bolas");
                        lbModelSelect.Items.Add("Skyweaver w/ " + Weapons[(UnitSize - 1) * 2] + " and " + Weapons[((UnitSize - 1) * 2) + 1]);
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

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (string weapon in Weapons)
            {
                if(weapon == "Skyweaver Haywire Cannon (+5 pts)")
                {
                    Points += 5;
                }
                if (weapon == "Zephyrglaive (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Skyweavers - " + Points + "pts";
        }
    }
}
