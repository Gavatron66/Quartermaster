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

            antiLoop = true;
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
            antiLoop = false;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    int oldSize = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    antiLoop = true;
                    if (UnitSize > oldSize)
                    {
                        nudOption1.Value += UnitSize - oldSize;
                    }

                    if (UnitSize < oldSize)
                    {
                        if (nudOption1.Value >= oldSize - UnitSize)
                        {
                            nudOption1.Value -= oldSize - UnitSize;
                        }
                        else
                        {
                            nudOption2.Value -= oldSize - UnitSize;
                        }
                    }
                    antiLoop = false;
                    break;
                case 31:
                    int temp = Convert.ToInt32(Weapons[0]);
                    antiLoop = true;

                    if (nudOption1.Value < 0)
                    {
                        nudOption1.Value++;
                    }
                    else if (nudOption1.Value > UnitSize)
                    {
                        nudOption1.Value--;
                    }
                    else if (temp < nudOption1.Value)
                    {
                        nudOption2.Value--;
                    }
                    else if (temp > nudOption1.Value)
                    {
                        nudOption2.Value++;
                    }
                    antiLoop = false;

                    Weapons[0] = Convert.ToString(nudOption1.Value);
                    Weapons[1] = Convert.ToString(nudOption2.Value);
                    break;
                case 32:
                    int temp2 = Convert.ToInt32(Weapons[1]);
                    antiLoop = true;

                    if (nudOption2.Value < 0)
                    {
                        nudOption2.Value++;
                    }
                    else if (nudOption2.Value > UnitSize)
                    {
                        nudOption2.Value--;
                    }
                    else if (temp2 < nudOption2.Value)
                    {
                        nudOption1.Value--;
                    }
                    else if (temp2 > nudOption2.Value)
                    {
                        nudOption1.Value++;
                    }
                    antiLoop = false;

                    Weapons[0] = Convert.ToString(nudOption1.Value);
                    Weapons[1] = Convert.ToString(nudOption2.Value);
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
