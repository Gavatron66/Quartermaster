using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Talos : Datasheets
    {
        int currentIndex;

        public Talos()
        {
            DEFAULT_POINTS = 100;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL3m";
            Weapons.Add("Macro-scalpel");
            Weapons.Add("Macro-scalpel");
            Weapons.Add("Two Splinter Cannons");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<HAEMONCULUS COVEN>",
                "MONSTER", "CORE", "FLY", "TALOS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Talos();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Drukhari;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Talos - " + CalcPoints(i) + " pts");
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Macro-scalpel",
                "Talos Ichor Injector",
                "Twin Liquifier Gun (+15 pts)"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Chain-flails",
                "Macro-scalpel",
                "Talos Gauntlet (+5 pts)"
            });

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Stinger Pod",
                "Two Drukhari Haywire Blasters",
                "Two Heat Lances",
                "Two Splinter Cannons"
            });
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
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 3)] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = ("Talos - " + CalcPoints(currentIndex) + " pts");
                    break;
                case 12:
                    Weapons[(currentIndex * 3) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = ("Talos - " + CalcPoints(currentIndex) + " pts");
                    break;
                case 13:
                    Weapons[(currentIndex * 3) + 2] = cmbOption3.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = ("Talos - " + CalcPoints(currentIndex) + " pts");
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Macro-scalpel");
                        Weapons.Add("Macro-scalpel");
                        Weapons.Add("Two Splinter Cannons");
                        lbModelSelect.Items.Add("Talos - " + CalcPoints(UnitSize - 1) + " pts");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 3) - 1, 3);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cmbOption3.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    cmbOption3.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption3"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 3) + 1]);
                    cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 3) + 2]);

                    break;
            }

            Points = (DEFAULT_POINTS * UnitSize) + CalcPoints(-1);
        }

        public override string ToString()
        {
            return "Talos - " + Points + "pts";
        }

        private int CalcPoints(int index)
        {
            int points = 0;

            if (index <= -1)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    if (Weapons[i] == "Talos Gauntlet (+5 pts)")
                    {
                        points += 5;
                    }
                    else if (Weapons[i] == "Twin Liquifier Gun (+15 pts)")
                    {
                        points += 15;
                    }
                }

                return points;
            }

            for (int i = 3 * index; i < 3 * (index + 1); i++)
            {
                if (Weapons[i] == "Talos Gauntlet (+5 pts)")
                {
                    points += 5;
                }
                else if (Weapons[i] == "Twin Liquifier Gun (+15 pts)")
                {
                    points += 15;
                }
            }

            return 100 + points;
        }
    }
}
