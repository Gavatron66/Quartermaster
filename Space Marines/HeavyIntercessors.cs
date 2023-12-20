using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class HeavyIntercessors : Datasheets
    {
        public HeavyIntercessors()
        {
            DEFAULT_POINTS = 23;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2N1m";
            Weapons.Add("Heavy Bolt Rifle");
            Weapons.Add("0");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "INTERCESSORS", "MK X GRAVIS", "HEAVY INTERCESSOR SQUAD"
            });
            role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new HeavyIntercessors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Executor Bolt Rifle",
                "Heavy Bolt Rifle",
                "Hellstorm Bolt Rifle"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Minimum = 0;
            nudOption1.Maximum = UnitSize / 5;
            nudOption1.Value = Convert.ToDecimal(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            Label lblnud1 = panel.Controls["lblnud1"] as Label;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    
                    switch (Weapons[0])
                    {
                        case "Heavy Bolt Rifle":
                            lblnud1.Text = "Heavy Bolter(s):";
                            lblnud1.Location = new System.Drawing.Point(173, 95);
                            break;
                        case "Hellstorm Bolt Rifle":
                            lblnud1.Text = "Hellstorm Heavy Bolter(s):";
                            lblnud1.Location = new System.Drawing.Point(95, 95);
                            break;
                        case "Executor Bolt Rifle":
                            lblnud1.Text = "Excutor Heavy Bolter(s):";
                            lblnud1.Location = new System.Drawing.Point(102, 95);
                            break;
                        default: break;
                    }

                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if(UnitSize < 10)
                    {
                        if(nudOption1.Value > 1)
                        {
                            nudOption1.Value--;
                        }
                        nudOption1.Maximum = 1;
                    }

                    if(UnitSize == 10)
                    {
                        nudOption1.Maximum = 2;
                    }

                    break;
                case 31:
                    Weapons[1] = nudOption1.Value.ToString();
                    break;
            }

            Points = (DEFAULT_POINTS * UnitSize);
        }

        public override string ToString()
        {
            return "Heavy Intercessor Squad - " + Points + "pts";
        }
    }
}
