using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Grey_Knights
{
    public class PurgationSquad : Datasheets
    {
        int currentIndex;
        int restriction = 1;
        public PurgationSquad()
        {
            DEFAULT_POINTS = 22;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Storm Bolter");
                Weapons.Add("Nemesis Force Sword");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "SANCTIC ASTARTES", "GREY KNGIHTS", "<BROTHERHOOD>",
                "INFANTRY", "CORE", "PSYKER", "PSYK-OUT GRENADES", "PURGATION SQUAD"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new PurgationSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as GreyKnights;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Purgator Justicar w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Purgator w/ " + Weapons[(i * 2)] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Incinerator",
                "Psilencer",
                "Psycannon",
                "Storm Bolter"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Nemesis Daemon Hammer",
                "Nemesis Force Halberd",
                "Nemesis Force Sword",
                "Nemesis Warding Stave",
                "Two Nemesis Falchions"
            });
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 2)] = cmbOption1.SelectedItem.ToString();

                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Purgator Justicar w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Purgator w/ " + Weapons[(currentIndex * 2)]
                            + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Purgator Justicar w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Purgator w/ " + Weapons[(currentIndex * 2)]
                            + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Storm Bolter");
                            Weapons.Add("Nemesis Force Sword");
                            lbModelSelect.Items.Add("Purgator w/ " + Weapons[(currentIndex * 2)]
                                + " and " + Weapons[(currentIndex * 2) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 2) + 1, 2);
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
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2)]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        if (restriction == (UnitSize / 5) * 4 && Weapons[(currentIndex * 2)] == "Storm Bolter")
                        {
                            cmbOption1.Enabled = false;
                        }
                        else if (!(Weapons[(currentIndex * 2)] == "Storm Bolter"))
                        {
                            cmbOption2.Enabled = false;
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            restriction = 0;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Incinerator" || weapon == "Psilencer" || weapon == "Psycannon")
                {
                    restriction++;
                }
            }
        }

        public override string ToString()
        {
            return "Purgation Squad - " + Points + "pts";
        }
    }
}