using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class Jakhals : Datasheets
    {
        public Jakhals()
        {
            DEFAULT_POINTS = 7;
            UnitSize = 9;
            Points = DEFAULT_POINTS * UnitSize + 7;
            TemplateCode = "5N";
            Weapons.Add("1"); //Number of Dishonoured
            Weapons.Add("0"); //Skullsmashers
            Weapons.Add("0"); //Mauler Chainblades
            Weapons.Add("0"); //Jakhal Icons
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "WORLD EATERS",
                "INFANTRY", "CULTISTS", "JAKHALS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Jakhals();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as WorldEaters;
            Template.LoadTemplate(TemplateCode, panel);

            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;
            Label lblnud3 = panel.Controls["lblnud3"] as Label;
            Label lblnud4 = panel.Controls["lblnud4"] as Label;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
            NumericUpDown nudOption4 = panel.Controls["nudOption4"] as NumericUpDown;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(lblnud1.Location.X - 80, nudUnitSize.Location.Y + 32);
            panel.Controls["lblExtra1"].Text = "May take one Dishonoured for every 9 Jakhals:";
            panel.Controls["lblExtra1"].Visible = true;

            lblnud1.Text = "Dishonoured:";
            lblnud1.Location = new System.Drawing.Point(lblnud1.Location.X + 40, lblnud1.Location.Y + 32);
            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X, nudOption1.Location.Y + 32);

            lblnud2.Text = "Skullsmashers (Dishonoured, +5 pts per):";
            lblnud2.Location = new System.Drawing.Point(lblnud2.Location.X - 80, lblnud2.Location.Y + 32);
            nudOption2.Location = new System.Drawing.Point(nudOption2.Location.X + 40, nudOption2.Location.Y + 32);

            panel.Controls["lblExtra2"].Location = new System.Drawing.Point(lblnud1.Location.X, nudOption1.Location.Y + 64);
            panel.Controls["lblExtra2"].Text = "May take up to one of each of the following for every 10 models:";
            panel.Controls["lblExtra2"].Visible = true;

            lblnud3.Text = "Mauler Chainblades (+5 pts):";
            lblnud3.Location = new System.Drawing.Point(lblnud3.Location.X - 40, lblnud3.Location.Y + 64);
            nudOption3.Location = new System.Drawing.Point(nudOption3.Location.X, nudOption3.Location.Y + 64);

            lblnud4.Text = "Jakhal Icons (+5 pts):";
            lblnud4.Location = new System.Drawing.Point(lblnud4.Location.X + 5, lblnud4.Location.Y + 64);
            nudOption4.Location = new System.Drawing.Point(nudOption4.Location.X, nudOption4.Location.Y + 64);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 9;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 18;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 1;
            nudOption1.Value = 1;
            nudOption1.Maximum = 2;
            nudOption1.Value = Convert.ToDecimal(Weapons[0]);

            nudOption2.Minimum = 0;
            nudOption2.Value = 0;
            nudOption2.Maximum = 1;
            nudOption2.Value = Convert.ToDecimal(Weapons[1]);

            nudOption3.Minimum = 0;
            nudOption3.Value = 0;
            nudOption3.Maximum = 1;
            nudOption3.Value = Convert.ToDecimal(Weapons[2]);

            nudOption4.Minimum = 0;
            nudOption4.Value = 0;
            nudOption4.Maximum = 1;
            nudOption4.Value = Convert.ToDecimal(Weapons[3]);

            if(UnitSize < 18)
            {
                nudOption1.Enabled = false;
            }
            else
            {
                nudOption1.Enabled = true;
            }

            if (nudOption1.Value == 2)
            {
                nudOption2.Maximum += 1;
            }

            if (UnitSize + nudOption1.Value == 20)
            {
                nudOption3.Maximum += 1;
                nudOption4.Maximum += 1;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
            NumericUpDown nudOption4 = panel.Controls["nudOption4"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize < 18)
                    {
                        nudOption1.Enabled = false;
                        if(nudOption1.Value == 2)
                        {
                            nudOption1.Value -= 1;
                        }
                    }
                    else
                    {
                        nudOption1.Enabled = true;
                    }

                    if (UnitSize + nudOption1.Value == 20)
                    {
                        nudOption3.Maximum = 2;
                        nudOption4.Maximum = 2;
                    }
                    else
                    {
                        nudOption3.Maximum = 1;
                        nudOption4.Maximum = 1;
                    }

                    break;
                case 31:
                    Weapons[0] = nudOption1.Value.ToString();

                    if (nudOption1.Value == 2)
                    {
                        nudOption2.Maximum = 2;
                    }
                    else
                    {
                        nudOption2.Maximum = 1;
                    }

                    if (UnitSize + nudOption1.Value == 20)
                    {
                        nudOption3.Maximum = 2;
                        nudOption4.Maximum = 2;
                    }
                    else
                    {
                        nudOption3.Maximum = 1;
                        nudOption4.Maximum = 1;
                    }
                    break;
                case 32:
                    Weapons[1] = nudOption2.Value.ToString();
                    break;
                case 33:
                    Weapons[2] = nudOption3.Value.ToString();
                    break;
                case 34:
                    Weapons[3] = nudOption4.Value.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * Convert.ToInt32(UnitSize + nudOption1.Value);

            Points += Convert.ToInt32(nudOption2.Value) * 5;
            Points += Convert.ToInt32(nudOption3.Value) * 5;
            Points += Convert.ToInt32(nudOption4.Value) * 5;
        }

        public override string ToString()
        {
            return "Jakhals - " + Points + "pts";
        }
    }
}
