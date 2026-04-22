using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class TacticalDrones : Datasheets
    {
        public TacticalDrones()
        {
            DEFAULT_POINTS = 10;
            UnitSize = 4;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "4N";
            Weapons.Add("4"); //Gun Drones
            Weapons.Add("0"); //Marker Drones
            Weapons.Add("0"); //Shield Drones
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "<SEPT>",
                "DRONE", "FLY", "TACTICAL DRONES"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new TacticalDrones();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as T_au;
            Template.LoadTemplate(TemplateCode, panel);

            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;
            Label lblnud3 = panel.Controls["lblnud3"] as Label;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;

            lblnud1.Text = "Gun Drones (+10 pts):";
            //lblnud1.Location = new System.Drawing.Point(lblnud1.Location.X - 80, lblnud1.Location.Y);

            lblnud2.Text = "Marker Drones (+10 pts):";
            lblnud2.Location = new System.Drawing.Point(lblnud2.Location.X - 10, lblnud2.Location.Y);

            lblnud3.Text = "Shield Drones (+15 pts):";
            lblnud3.Location = new System.Drawing.Point(lblnud3.Location.X - 5, lblnud3.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 4;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 12;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = nudUnitSize.Value;
            nudOption1.Value = Convert.ToDecimal(Weapons[0]);

            nudOption2.Minimum = 0;
            nudOption2.Value = 0;
            nudOption2.Maximum = nudUnitSize.Value;
            nudOption2.Value = Convert.ToDecimal(Weapons[1]);

            nudOption3.Minimum = 0;
            nudOption3.Value = 0;
            nudOption3.Maximum = nudUnitSize.Value;
            nudOption3.Value = Convert.ToDecimal(Weapons[2]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    int oldSize = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    nudOption1.Maximum = UnitSize;
                    nudOption2.Maximum = UnitSize;
                    nudOption3.Maximum = UnitSize;
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
                    Weapons[0] = nudOption1.Value.ToString();
                    if(nudOption1.Value + nudOption2.Value + nudOption3.Value > UnitSize)
                    {
                        nudOption1.Value--;
                    }
                    break;
                case 32:
                    Weapons[1] = nudOption2.Value.ToString();
                    if (nudOption1.Value + nudOption2.Value + nudOption3.Value > UnitSize)
                    {
                        nudOption2.Value--;
                    }
                    break;
                case 33:
                    Weapons[2] = nudOption3.Value.ToString();
                    if (nudOption1.Value + nudOption2.Value + nudOption3.Value > UnitSize)
                    {
                        nudOption3.Value--;
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += Convert.ToInt32(nudOption3.Value) * 5;
        }

        public override string ToString()
        {
            return "Tactical Drones - " + Points + "pts";
        }
    }
}
