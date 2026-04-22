using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Cronos : Datasheets
    {
        int currentIndex;

        public Cronos()
        {
            DEFAULT_POINTS = 75;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL2k";
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<HAEMONCULUS COVEN>",
                "MONSTER", "CORE", "FLY", "CRONOS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Cronos();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Drukhari;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;

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
                if (Weapons[currentIndex * 2] == "" && Weapons[(currentIndex * 2) + 1] == "")
                {
                    lbModelSelect.Items.Add("Cronos");
                }
                else if (Weapons[currentIndex * 2] != "" && Weapons[(currentIndex * 2) + 1] == "")
                {
                    lbModelSelect.Items.Add("Cronos w/ " + Weapons[currentIndex * 2]);
                }
                else if (Weapons[(currentIndex * 2) + 1] != "" && Weapons[currentIndex * 2] == "")
                {
                    lbModelSelect.Items.Add("Cronos w/ " + Weapons[(currentIndex * 2) + 1]);
                }
                else if (Weapons[(currentIndex * 2) + 1] != "" && Weapons[currentIndex * 2] != "")
                {
                    lbModelSelect.Items.Add("Cronos w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1]);
                }
            }

            cbOption1.Text = "Spirit Vortex (+10 pts)";
            cbOption2.Text = "Spirit Probe (+5 pts)";
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;

            switch (code)
            {
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[currentIndex * 2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[currentIndex * 2] = "";
                    }

                    if (Weapons[currentIndex * 2] == "" && Weapons[(currentIndex * 2) + 1] == "")
                    {
                        lbModelSelect.Items[currentIndex] = "Cronos";
                    }
                    else if (Weapons[currentIndex * 2] != "" && Weapons[(currentIndex * 2) + 1] == "")
                    {
                        lbModelSelect.Items[currentIndex] = "Cronos w/ " + Weapons[currentIndex * 2];
                    }
                    else if (Weapons[(currentIndex * 2) + 1] != "" && Weapons[currentIndex * 2] == "")
                    {
                        lbModelSelect.Items[currentIndex] = "Cronos w/ " + Weapons[(currentIndex * 2) + 1];
                    }
                    else if (Weapons[(currentIndex * 2) + 1] != "" && Weapons[currentIndex * 2] != "")
                    {
                        lbModelSelect.Items[currentIndex] = "Cronos w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }

                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 2) + 1] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 1] = "";
                    }

                    if (Weapons[currentIndex * 2] == "" && Weapons[(currentIndex * 2) + 1] == "")
                    {
                        lbModelSelect.Items[currentIndex] = "Cronos";
                    }
                    else if (Weapons[currentIndex * 2] != "" && Weapons[(currentIndex * 2) + 1] == "")
                    {
                        lbModelSelect.Items[currentIndex] = "Cronos w/ " + Weapons[currentIndex * 2];
                    }
                    else if (Weapons[(currentIndex * 2) + 1] != "" && Weapons[currentIndex * 2] == "")
                    {
                        lbModelSelect.Items[currentIndex] = "Cronos w/ " + Weapons[(currentIndex * 2) + 1];
                    }
                    else if (Weapons[(currentIndex * 2) + 1] != "" && Weapons[currentIndex * 2] != "")
                    {
                        lbModelSelect.Items[currentIndex] = "Cronos w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }

                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        lbModelSelect.Items.Add("Cronos");
                        Weapons.Add("");
                        Weapons.Add("");
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
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        break;
                    }

                    cbOption1.Visible = true;
                    cbOption2.Visible = true;

                    if (Weapons[(currentIndex * 2)] == "")
                    {
                        cbOption1.Checked = false;
                    }
                    else
                    {
                        cbOption1.Checked = true;
                    }

                    if (Weapons[(currentIndex * 2) + 1] == "")
                    {
                        cbOption2.Checked = false;
                    }
                    else
                    {
                        cbOption2.Checked = true;
                    }

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (string weapon in Weapons)
            {
                if (weapon == "Spirit Probe (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Spirit Vortex (+10 pts)")
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Cronos - " + Points + "pts";
        }
    }
}
