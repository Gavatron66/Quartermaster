using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class CorsairVoidreavers : Datasheets
    {
        public CorsairVoidreavers()
        {
            DEFAULT_POINTS = 10;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "corsairs";
            Weapons.Add("Shuriken Pistol and Aeldari Power Sword");
            Weapons.Add("0");
            Weapons.Add("0");
            Weapons.Add("Shuriken Rifle");
            Weapons.Add("Shuriken Pistol");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ANHRATHE", "ASURYANI", "DRUKHARI",
                "INFANTRY", "CORSAIR VOIDREAVERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new CorsairVoidreavers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            GroupBox groupBox = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = groupBox.Controls["gb_cmbOption1"] as ComboBox;
            CheckBox cbLeaderOption1 = groupBox.Controls["cbLeaderOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Shuriken Pistol and Aeldari Power Sword",
                "Shuriken Rifle"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Shuriken Cannon (+10 pts)",
                "Shuriken Rifle",
                "Wraithcannon (+15 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[3]);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            panel.Controls["lblExtra1"].Text = "May take one of the following for every 5 models:";
            panel.Controls["lblExtra2"].Text = "If this unit contains 10 models:";
            if(UnitSize == 10)
            {
                cmbOption2.Enabled = true;
            }
            else
            {
                cmbOption2.Enabled = false;
            }

            int temp = Convert.ToInt32(Weapons[1]);
            nudOption1.Minimum = 0;
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Maximum = 1;
            nudOption1.Value = temp;

            panel.Controls["lblnud1"].Text = "Corsair Blaster (+10 pts):";

            temp = Convert.ToInt32(Weapons[2]);
            nudOption2.Minimum = 0;
            nudOption2.Value = nudOption1.Minimum;
            nudOption2.Maximum = 1;
            nudOption2.Value = temp;

            panel.Controls["lblnud2"].Text = "Corsair Shredder (+5 pts):";

            if (UnitSize == 10)
            {
                nudOption1.Maximum++;
                nudOption2.Maximum++;
            }

            groupBox.Text = "Voidreaver Felarch";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "Neuro Disruptor (+5 pts)",
                "Shuriken Pistol",
                "Shuriken Rifle"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[4]);

            cbLeaderOption1.Text = "Mistshield (+5 pts)";
            if (Weapons[5] != string.Empty)
            {
                cbLeaderOption1.Checked = true;
            }
            else
            {
                cbLeaderOption1.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            GroupBox groupBox = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = groupBox.Controls["gb_cmbOption1"] as ComboBox;
            CheckBox cbLeaderOption1 = groupBox.Controls["cbLeaderOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[3] = cmbOption2.SelectedItem.ToString();
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if(UnitSize == 10)
                    {
                        nudOption1.Maximum++;
                        nudOption2.Maximum++;
                        cmbOption2.Enabled = true;
                    }
                    else if (UnitSize < 10 && nudOption1.Maximum != 1)
                    {
                        nudOption1.Maximum--;
                        nudOption2.Maximum--;
                        cmbOption2.Enabled = false;
                        cmbOption2.SelectedItem = "Shuriken Rifle";
                    }
                    break;
                case 31:
                    if(nudOption1.Value + nudOption2.Value <= UnitSize/5)
                    {
                        Weapons[1] = nudOption1.Value.ToString();
                    }
                    else
                    {
                        if (nudOption1.Value != 0)
                        {
                            nudOption1.Value--;
                        }
                    }
                    break;
                case 32:
                    if (nudOption1.Value + nudOption2.Value <= UnitSize / 5)
                    {
                        Weapons[2] = nudOption2.Value.ToString();
                    }
                    else
                    {
                        if(nudOption2.Value != 0)
                        {
                            nudOption2.Value--;
                        }
                    }
                    break;
                case 411:
                    Weapons[4] = gb_cmbOption1.SelectedItem.ToString();
                    break;
                case 421:
                    if (cbLeaderOption1.Checked)
                    {
                        Weapons[5] = cbLeaderOption1.Text;
                    }
                    else { Weapons[5] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += Convert.ToInt32(nudOption1.Value) * 10;
            Points += Convert.ToInt32(nudOption2.Value) * 5;

            if(Weapons.Contains("Mistshield (+5 pts)") || Weapons.Contains("Neuro Disruptor (+5 pts)"))
            {
                Points += 5;
            }

            if(Weapons.Contains("Shuriken Cannon (+10 pts)"))
            {
                Points += 10;
            }

            if (Weapons.Contains("Wraithcannon (+15 pts)"))
            {
                Points += 15;
            }
        }

        public override string ToString()
        {
            return "Corsair Voidreavers - " + Points + "pts";
        }
    }
}
