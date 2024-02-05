using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class IroncladDreadnought : Datasheets
    {
        public IroncladDreadnought()
        {
            DEFAULT_POINTS = 135;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m1k1N";
            Weapons.Add("Seismic Hammer");
            Weapons.Add("Ironclad CW and Storm Bolter");
            Weapons.Add("Meltagun");
            Weapons.Add("0");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "CORE", "DREADNOUGHT", "SMOKESCREEN", "IRONCLAD DREADNOUGHT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new IroncladDreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Dreadnought Chainfist",
                "Seismic Hammer"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Hurricane Bolter",
                "Ironclad CW and Heavy Flamer",
                "Ironclad CW and Storm Bolter"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Heavy Flamer",
                "Meltagun"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            panel.Controls["lblnud1"].Text = "Hunter-killer Missiles:";
            panel.Controls["lblnud1"].Location = new System.Drawing.Point(132, 129);

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = 2;
            nudOption1.Value = Convert.ToInt32(Weapons[3].ToString());

            cbOption1.Text = "Ironclad Assault Launchers";
            if (Weapons[4] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch(code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem as string;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[4] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[4] = "";
                    }
                    break;
                case 31:
                    Weapons[3] = nudOption1.Value.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Ironclad Dreadnought - " + Points + "pts";
        }
    }
}
