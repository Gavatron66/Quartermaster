using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class GuardianDefenders : Datasheets
    {
        public GuardianDefenders()
        {
            DEFAULT_POINTS = 9;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2N2m";
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("0");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "INFANTRY", "CORE", "GUARDIANS", "GUARIDAN DEFENDERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new GuardianDefenders();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;

            lblnud1.Text = "Heavy Weapon Platforms (+20 pts):";
            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X + 35, nudOption1.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            int temp = Convert.ToInt32(Weapons[2]);
            nudOption1.Minimum = 0;
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Maximum = 2;
            nudOption1.Value = temp;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Aeldari Missile Launcher (+5 pts)",
                "Bright Lance (+10 pts)",
                "Scatter Laser",
                "Shuriken Cannon",
                "Starcannon (+5 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Aeldari Missile Launcher (+5 pts)",
                "Bright Lance (+10 pts)",
                "Scatter Laser",
                "Shuriken Cannon",
                "Starcannon (+5 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            if (Weapons[2] == "1")
            {
                cmbOption1.Enabled = true;
                cmbOption2.Enabled = false;
            }
            else if (Weapons[2] == "2")
            {
                cmbOption1.Enabled = true;
                cmbOption2.Enabled = true;
            }
            else
            {
                cmbOption1.Enabled = false;
                cmbOption2.Enabled = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;

            switch (code)
            {
                case 11:
                    if(cmbOption1.SelectedItem != null)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                    }
                    break;
                case 12:
                    if (cmbOption2.SelectedItem != null)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                    }
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
                case 31:
                    Weapons[2] = nudOption1.Value.ToString();

                    if (Weapons[2] == "1")
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = false;

                        cmbOption2.SelectedIndex = -1;
                        Weapons[1] = "";
                    }
                    else if (Weapons[2] == "2")
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                    }
                    else
                    {
                        cmbOption1.Enabled = false;
                        cmbOption2.Enabled = false;

                        cmbOption1.SelectedIndex = -1;
                        Weapons[1] = "";
                        cmbOption2.SelectedIndex = -1;
                        Weapons[0] = "";
                    }

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += Convert.ToInt32(Weapons[2]) * 20;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Aeldari Missile Launcher (+5 pts)" || weapon == "Starcannon (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Bright Lance (+10 pts)")
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Guardian Defenders - " + Points + "pts";
        }
    }
}
