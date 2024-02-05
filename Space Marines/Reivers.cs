using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Reivers : Datasheets
    {
        public Reivers()
        {
            DEFAULT_POINTS = 16;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1m2k";
            Weapons.Add("Combat Knives");
            Weapons.Add(""); //Grav-chute
            Weapons.Add(""); //Grapnel Launcher
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "PHOBOS", "REIVER", "SHOCK GRENADES", "REIVER SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Reivers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bolt Carbines",
                "Combat Knives"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Reiver Grav-chutes";
            if (Weapons[1] != "")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Grapnel Launchers";
            if (Weapons[2] != "")
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
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch(code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Reiver Squad - " + Points + "pts";
        }
    }
}
