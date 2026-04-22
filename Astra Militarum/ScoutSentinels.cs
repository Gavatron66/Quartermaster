using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class ScoutSentinels : Datasheets
    {
        int currentIndex;

        public ScoutSentinels()
        {
            DEFAULT_POINTS = 40;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m2k";
            Weapons.Add("Militarum Multi-laser");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "VEHICLE", "SENTINEL", "CORE", "PLATOON", "SMOKE", "REGIMENTAL", "SQUADRON", "SCOUT SENTINELS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new ScoutSentinels();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Scout Sentinel w/ " + Weapons[i * 3]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new object[]
            {
                "Autocannon",
                "Heavy Flamer",
                "Lascannon",
                "Militarum Multi-laser",
                "Militarum Plasma Cannon",
                "Missile Launcher"
            });

            cbOption1.Text = "Hunter-killer Missile (+5 pts)";
            if (Weapons[0] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Sentinel Chainsaw (+5 pts)";
            if (Weapons[1] == cbOption2.Text)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 3] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Scout Sentinel w/ " + Weapons[currentIndex * 3];
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 3) + 1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 1] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 3) + 2] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 2] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Militarum Multi-laser");
                            Weapons.Add("");
                            Weapons.Add("");
                            lbModelSelect.Items.Add("Scout Sentinel w/ " + Weapons[i * 3]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((temp - 1) * 3, 3);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }

                    cmbOption1.Visible = true;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);

                    if (Weapons[(currentIndex * 3) + 1] == "Hunter-killer Missile (+5 pts)")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                    }

                    if (Weapons[(currentIndex * 3) + 2] == "Sentinel Chainsaw (+5 pts)")
                    {
                        cbOption2.Checked = true;
                    }
                    else
                    {
                        cbOption2.Checked = false;
                    }


                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Sentinel Chainsaw (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon == "Hunter-killer Missile (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Scout Sentinels - " + Points + "pts";
        }
    }
}
