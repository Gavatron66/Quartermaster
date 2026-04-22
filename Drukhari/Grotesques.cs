using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Grotesques : Datasheets
    {
        public Grotesques()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "3N";
            Weapons.Add("0"); //Liquifier Guns
            Weapons.Add("3"); //Monstrous Cleavers
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<HAEMONCULUS COVEN>",
                "INFANTRY", "CORE", "GROTESQUES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Grotesques();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            panel.Controls["lblnud1"].Text = "Models with Liquifier Guns (+5 pts):";
            panel.Controls["lblnud2"].Text = "Models with Monstrous Cleavers:";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;
            nudOption1.Location = new System.Drawing.Point(365, 59);

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;
            nudOption2.Location = new System.Drawing.Point(345, 91);

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
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if(temp < UnitSize)
                    {
                        nudOption2.Value++;
                    }
                    else if (temp > UnitSize)
                    {
                        if(nudOption2.Value > 0)
                        {
                            nudOption2.Value--;
                        }
                        else
                        {
                            nudOption1.Value--;
                        }
                    }
                    break;
                case 31:
                    if (nudOption1.Value + nudOption2.Value <= nudUnitSize.Value)
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
            Points += Convert.ToInt32(nudOption1.Value * 5);
        }

        public override string ToString()
        {
            return "Grotesques - " + Points + "pts";
        }
    }
}
