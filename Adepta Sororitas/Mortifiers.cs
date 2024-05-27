using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class Mortifiers : Datasheets
    {
        int currentIndex;
        int anchoriteIndex = -1;

        public Mortifiers()
        {
            DEFAULT_POINTS = 55;
            UnitSize = 1;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Two Heavy Bolters");
                Weapons.Add("Two Penitent Buzz-blades");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS",
                "VEHICLE", "OUTCASTS", "ENGINE OF REDEMPTION", "MORTIFIERS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Mortifiers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AdeptaSororitas;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 4;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Mortifier w/ " + Weapons[(i * 2) + 1] + " and " + Weapons[(i * 2) + 2]);
            }

            cbOption1.Text = "Anchorite Sarcophagus (+5 pts)";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Bolter and Ministorum Flamer",
                "Two Heavy Bolters",
                "Two Ministorum Heavy Flamers"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Penitent Buzz-blade and Penitent Flail",
                "Two Penitent Buzz-blades",
                "Two Penitent Flails"
            });
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Mortifier w/ " + Weapons[(currentIndex * 2) + 1]
                        + " and " + Weapons[(currentIndex * 2) + 2];
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 2] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Mortifier w/ " + Weapons[(currentIndex * 2) + 1]
                        + " and " + Weapons[(currentIndex * 2) + 2];
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.ToString();
                        anchoriteIndex = currentIndex;
                    }
                    else
                    {
                        Weapons[0] = "";
                        anchoriteIndex = -1;
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Two Heavy Bolters");
                            Weapons.Add("Two Penitent Buzz-blades");
                            lbModelSelect.Items.Add("Mortifier w/ " + Weapons[(temp * 2) + 1]
                                + " and " + Weapons[(temp * 2) + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((temp * 2) - 1, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);

                        if (Weapons[0] == "")
                        {
                            cbOption1.Checked = false;
                        }
                        else
                        {
                            cbOption1.Checked = true;
                        }

                        if(currentIndex == anchoriteIndex)
                        {
                            cbOption1.Enabled = true;
                        }
                        else if (currentIndex != anchoriteIndex && Weapons[0] != "")
                        {
                            cbOption1.Enabled = false;
                        }
                        else
                        {
                            cbOption1.Enabled = true;
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if(cbOption1.Checked)
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Mortifiers - " + Points + "pts";
        }
    }
}