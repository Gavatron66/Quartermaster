using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    internal class SicarianInfiltrators : Datasheets
    {
        public SicarianInfiltrators()
        {
            DEFAULT_POINTS = 17;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "3N";
            Weapons.Add("5");  //Stubcarbine and Power Sword
            Weapons.Add("0");   //Flechette Blaster and Taser Goad
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "INFANTRY", "CORE", "SICARIAN", "SICARIAN INFILTRATORS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new SicarianInfiltrators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            panel.Controls["lblnud1"].Text = "Models with Stubcarbines and Power Swords:";
            panel.Controls["lblnud2"].Text = "Models with Flechette Blasters and Taser Goads:";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;
            nudOption1.Location = new System.Drawing.Point(449, 59);

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;
            nudOption2.Location = new System.Drawing.Point(449, 91);

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
                    int oldSize = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

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
            return "Sicarian Infiltrators - " + Points + "pts";
        }
    }
}
