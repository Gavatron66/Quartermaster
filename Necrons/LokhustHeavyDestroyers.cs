using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    internal class LokhustHeavyDestroyers : Datasheets
    {
        public LokhustHeavyDestroyers()
        {
            DEFAULT_POINTS = 50;
            UnitSize = 1;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "3N";
            Weapons.Add("1");  //Enmitic Exterminator
            Weapons.Add("0");  //Gauss Destructor
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "INFANTRY", "FLY", "DESTROYER CULT", "LOKHUST HEAVY DESTROYERS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new LokhustHeavyDestroyers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            panel.Controls["lblnud1"].Text = "Models with Enmitic Exterminators:";
            panel.Controls["lblnud2"].Text = "Models with Gauss Destructors:";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;
            nudOption1.Location = new System.Drawing.Point(359, 59);

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;
            nudOption2.Location = new System.Drawing.Point(359, 91);

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
                    break;
                case 31:
                    if (nudOption1.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value <= nudUnitSize.Value)
                    {
                        Weapons[0] = Convert.ToString(nudOption1.Value);
                    }
                    else
                    {
                        nudOption1.Value -= 1;
                    }
                    break;
                case 32:
                    if (nudOption2.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value <= nudUnitSize.Value)
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
        }

        public override string ToString()
        {
            return "Lokhust Heavy Destroyers - " + Points + "pts";
        }
    }
}
