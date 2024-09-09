using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Bullgryns : Datasheets
    {
        int currentIndex;
        public Bullgryns()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            for(int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Grenadier Gauntlet");
                Weapons.Add("Slabshield");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "MILITARUM AUXILLA", "ABHUMAN", "BULLGRYNS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Bullgryns();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Bullgryn Bone 'ead w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Bullgryn w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bullgryn Maul (+5 pts)",
                "Grenadier Gauntlet"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Brute Shield",
                "Slabshield"
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

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Bullgryn Bone 'ead w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Bullgryn w/ " + Weapons[(currentIndex) * 2] + " and " + Weapons[((currentIndex) * 2) + 1];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Bullgryn Bone 'ead w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Bullgryn w/ " + Weapons[(currentIndex) * 2] + " and " + Weapons[((currentIndex) * 2) + 1];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Grenadier Gauntlet");
                        Weapons.Add("Slabshield");
                        lbModelSelect.Items.Add("Bullgryn w/ " + Weapons[(temp) * 2] + " and " + Weapons[(temp * 2) + 1]);
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

            foreach(var weapon in Weapons)
            {
                if(weapon == "Bullgryn Maul (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Bullgryns - " + Points + "pts";
        }
    }
}
