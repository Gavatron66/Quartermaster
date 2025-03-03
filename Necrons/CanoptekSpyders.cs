using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class CanoptekSpyders : Datasheets
    {
        int currentIndex;

        public CanoptekSpyders()
        {
            DEFAULT_POINTS = 60;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL3k";
            Weapons.Add(""); // Particle Beamers (+5 pts)
            Weapons.Add(""); // Fabricator Claw Array (+5 pts)
            Weapons.Add(""); // Gloom Prism (+5 pts)
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "MONSTER", "FLY", "CANOPTEK", "CANOPTEK SPYDERS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new CanoptekSpyders();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Necrons;

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
            for(int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Canoptek Spyder - " + CalcPoints(i) + " pts");
            }

            cbOption1.Text = "Two Particle Beamers (+5 pts)";
            cbOption2.Text = "Fabricator Claw Array (+5 pts)";
            cbOption3.Text = "Gloom Prism (+5 pts)";
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop)
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
                    if(cbOption1.Checked)
                    {
                        Weapons[currentIndex * 3] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[currentIndex * 3] = "";
                    }
                    lbModelSelect.Items[currentIndex] = "Canoptek Spyder - " + CalcPoints(currentIndex) + " pts";
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 3) + 1] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 1] = "";
                    }
                    lbModelSelect.Items[currentIndex] = "Canoptek Spyder - " + CalcPoints(currentIndex) + " pts";
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[(currentIndex * 3) + 2] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 2] = "";
                    }
                    lbModelSelect.Items[currentIndex] = "Canoptek Spyder - " + CalcPoints(currentIndex) + " pts";
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if(temp < UnitSize)
                    {
                        Weapons.Add(""); 
                        Weapons.Add("");
                        Weapons.Add("");
                        lbModelSelect.Items.Add("Canoptek Spyder - " + CalcPoints(temp) + " pts");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 3) - 1, 3);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if(currentIndex < 0)
                    {
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        break;
                    }

                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;

                    if (Weapons[(currentIndex * 3)] == "")
                    {
                        cbOption1.Checked = false;
                    }
                    else
                    {
                        cbOption1.Checked = true;
                    }

                    if (Weapons[(currentIndex * 3) + 1] == "")
                    {
                        cbOption2.Checked = false;
                    }
                    else
                    {
                        cbOption2.Checked = true;
                    }

                    if (Weapons[(currentIndex * 3) + 2] == "")
                    {
                        cbOption3.Checked = false;
                    }
                    else
                    {
                        cbOption3.Checked = true;
                    }

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach(string weapon in Weapons)
            {
                if(weapon != "")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Canoptek Spyders - " + Points + "pts";
        }

        private int CalcPoints(int index)
        {
            int val = DEFAULT_POINTS;

            for(int i = index * 3; i < (index + 1) * 3; i++)
            {
                if (Weapons[i] != "")
                {
                    val += 5;
                }
            }

            return val;
        }
    }
}
