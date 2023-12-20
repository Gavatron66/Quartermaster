using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Eradicators : Datasheets
    {
        public Eradicators()
        {
            DEFAULT_POINTS = 45;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2N1m";
            Weapons.Add("Melta Rifle");
            Weapons.Add("0"); //Multi-meltas
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "MK X GRAVIS", "ERADICATOR SQUAD"
            });
            role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Eradicators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            Label lblnud1 = panel.Controls["lblnud1"] as Label;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Melta Rifle",
                "Melta Rifle"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            lblnud1.Text = "Multi-melta(s):";
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Minimum = 0;
            nudOption1.Maximum = UnitSize / 3;
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
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize < 6)
                    {
                        if (nudOption1.Value > 1)
                        {
                            nudOption1.Value--;
                        }
                        nudOption1.Maximum = 1;
                    }

                    if (UnitSize == 6)
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
            return "Eradicator Squad - " + Points + "pts";
        }
    }
}
