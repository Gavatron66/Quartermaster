using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Raveners : Datasheets
    {
        int currentIndex;

        public Raveners()
        {
            DEFAULT_POINTS = 45;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Two Scything Talons");
                Weapons.Add("(None)");
            }
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "CORE", "BURROWERS", "RAVENERS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Raveners();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                if (Weapons[(i*2)+1] == "(None)")
                {
                    lbModelSelect.Items.Add("Ravener w/ " + Weapons[(i * 2)]);
                }
                else
                {
                    lbModelSelect.Items.Add("Ravener w/ " + Weapons[(i * 2)] + " and " + Weapons[(i * 2) + 1]);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new object[]
            {
                "Two Rending Claws",
                "Two Scything Talons"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new object[]
            {
                "(None)",
                "Deathspitter",
                "Devourer",
                "Thoracic Spinefists",
            });
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 2)] = cmbOption1.SelectedItem.ToString();
                    if (Weapons[(currentIndex * 2) + 1] == "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = "Ravener w/ " + Weapons[(currentIndex * 2)];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Ravener w/ " + Weapons[(currentIndex * 2)] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    if (Weapons[(currentIndex * 2) + 1] == "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = "Ravener w/ " + Weapons[(currentIndex * 2)];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Ravener w/ " + Weapons[(currentIndex * 2)] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Two Scything Talons");
                            Weapons.Add("(None)");
                            lbModelSelect.Items.Add("Ravener w/ " + Weapons[(i * 2)]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2) + 1, 2);
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
                    else if (currentIndex == -1)
                    {
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2)]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Raveners - " + Points + "pts";
        }
    }
}
