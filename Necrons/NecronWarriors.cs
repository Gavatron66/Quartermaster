﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    internal class NecronWarriors : Datasheets
    {
        public NecronWarriors()
        {
            DEFAULT_POINTS = 11;
            UnitSize = 10;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "3N";
            Weapons.Add("10");  //Gauss Flayer
            Weapons.Add("0");   //Gauss Reaper
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "INFANTRY", "CORE", "NECRON WARRIORS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new NecronWarriors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            panel.Controls["lblnud1"].Text = "Models with Gauss Flayers:";
            panel.Controls["lblnud2"].Text = "Models with Gauss Reapers:";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            antiLoop = true;
            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;
            nudOption1.Location = new System.Drawing.Point(349, 59);

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;
            nudOption2.Location = new System.Drawing.Point(349, 91);

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
                    if(UnitSize > oldSize)
                    {
                        nudOption1.Value += UnitSize - oldSize;
                    }

                    if(UnitSize < oldSize)
                    {
                        if(nudOption1.Value >= oldSize - UnitSize)
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

                    if(nudOption1.Value < 0)
                    {
                        nudOption1.Value++;
                    }
                    else if (nudOption1.Value > UnitSize)
                    {
                        nudOption1.Value--;
                    }
                    else if(temp < nudOption1.Value)
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
            return "Necron Warriors - " + Points + "pts";
        }
    }
}
