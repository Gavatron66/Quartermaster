using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Rangers : Datasheets
    {
        public Rangers()
        {
            DEFAULT_POINTS = 13;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "3N";
            Weapons.Add("0"); 
            Weapons.Add("0");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "INFANTRY", "CORE", "RESONATOR SHARD", "OUTCASTS", "RANGERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Rangers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;

            lblnud1.Text = "Gloom Fields (+10 pts):";
            lblnud2.Text = "Wireweave Nets (+10 pts):";

            lblnud1.Location = new System.Drawing.Point(lblnud1.Location.X - 40, lblnud1.Location.Y);
            lblnud2.Location = new System.Drawing.Point(lblnud2.Location.X - 40, lblnud2.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = 1;

            nudOption2.Minimum = 0;
            nudOption2.Maximum = 1;

            if (UnitSize == 10)
            {
                nudOption1.Maximum++;
                nudOption2.Maximum++;
            }

            nudOption1.Value = int.Parse(Weapons[0]);
            nudOption2.Value = int.Parse(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize == 10)
                    {
                        nudOption1.Maximum = 2;
                        nudOption2.Maximum = 2;
                    }

                    if (UnitSize < 10 && nudOption1.Value + nudOption2.Value == 2)
                    {
                        if(nudOption1.Value == 2)
                        {
                            nudOption1.Value--;
                        }
                        else
                        {
                            nudOption2.Value--;
                        }

                        nudOption1.Maximum = 1;
                        nudOption2.Maximum = 1;
                    }

                    break;
                case 31:
                    if (nudOption1.Value + nudOption2.Value <= nudOption1.Maximum)
                    {
                        Weapons[0] = Convert.ToString(nudOption1.Value);
                    }
                    else
                    {
                        nudOption1.Value -= 1;
                    }
                    break;
                case 32:
                    if (nudOption1.Value + nudOption2.Value <= nudOption2.Maximum)
                    {
                        Weapons[1] = Convert.ToString(nudOption2.Value);
                    }
                    else
                    {
                        nudOption2.Value -= 1;
                    }
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

            Points += Convert.ToInt32(nudOption1.Value + nudOption2.Value) * 10;
        }

        public override string ToString()
        {
            return "Rangers - " + Points + "pts";
        }
    }
}
