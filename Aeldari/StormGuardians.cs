using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class StormGuardians : Datasheets
    {
        public StormGuardians()
        {
            DEFAULT_POINTS = 8;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "5N";
            Weapons.Add("0"); //Serpent Scale Platforms
            Weapons.Add("0"); //Aeldari Flamers
            Weapons.Add("0"); //Fusion Guns
            Weapons.Add("0"); //Power Swords
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "INFANTRY", "CORE", "GUARDIANS", "STORM GUARIDANS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new StormGuardians();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
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
            panel.Controls["lblExtra1"].Text = "May take one of the following for every 10 models:";
            panel.Controls["lblExtra1"].Visible = true;

            lblnud1.Text = "Serpent's Scale Platforms (+20 pts):";
            lblnud1.Location = new System.Drawing.Point(lblnud1.Location.X - 80, lblnud1.Location.Y + 32);
            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X, nudOption1.Location.Y + 32);

            panel.Controls["lblExtra2"].Location = new System.Drawing.Point(lblnud1.Location.X, nudOption1.Location.Y + 32);
            panel.Controls["lblExtra2"].Text = "May take up to two of each of the following for every 10 models:";
            panel.Controls["lblExtra2"].Visible = true;

            lblnud2.Text = "Aeldari Flamers (+5 pts):";
            lblnud2.Location = new System.Drawing.Point(lblnud2.Location.X - 10, lblnud2.Location.Y + 64);
            nudOption2.Location = new System.Drawing.Point(nudOption2.Location.X, nudOption2.Location.Y + 64);

            lblnud3.Text = "Guardian Fusion Guns (+10 pts):";
            lblnud3.Location = new System.Drawing.Point(lblnud3.Location.X - 60, lblnud3.Location.Y + 64);
            nudOption3.Location = new System.Drawing.Point(nudOption3.Location.X, nudOption3.Location.Y + 64);

            lblnud4.Text = "Aeldari Power Swords (+5 pts):";
            lblnud4.Location = new System.Drawing.Point(lblnud4.Location.X - 45, lblnud4.Location.Y + 64);
            nudOption4.Location = new System.Drawing.Point(nudOption4.Location.X, nudOption4.Location.Y + 64);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = 1;
            nudOption1.Value = Convert.ToDecimal(Weapons[0]);

            nudOption2.Minimum = 0;
            nudOption2.Value = 0;
            nudOption2.Maximum = 2;
            nudOption2.Value = Convert.ToDecimal(Weapons[1]);

            nudOption3.Minimum = 0;
            nudOption3.Value = 0;
            nudOption3.Maximum = 2;
            nudOption3.Value = Convert.ToDecimal(Weapons[2]);

            nudOption4.Minimum = 0;
            nudOption4.Value = 0;
            nudOption4.Maximum = 2;
            nudOption4.Value = Convert.ToDecimal(Weapons[3]);

            if(UnitSize == 20)
            {
                nudOption2.Maximum += 2;
                nudOption3.Maximum += 2;
                nudOption4.Maximum += 2;
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

                    if (UnitSize == 20)
                    {
                        nudOption1.Maximum += 1;
                        nudOption2.Maximum += 2;
                        nudOption3.Maximum += 2;
                        nudOption4.Maximum += 2;
                    }
                    else if (UnitSize < 20 && nudOption2.Maximum != 2)
                    {
                        nudOption1.Maximum -= 1;
                        nudOption2.Maximum -= 2;
                        nudOption3.Maximum -= 2;
                        nudOption4.Maximum -= 2;

                        if (nudOption1.Value == 2)
                        {
                            nudOption1.Value--;
                        }

                        if (nudOption2.Value > 2)
                        {
                            nudOption2.Value--;
                            if(nudOption2.Value > 2)
                            {
                                nudOption2.Value--;
                            }
                        }

                        if (nudOption3.Value > 2)
                        {
                            nudOption3.Value--;
                            if (nudOption3.Value > 2)
                            {
                                nudOption3.Value--;
                            }
                        }

                        if (nudOption4.Value > 2)
                        {
                            nudOption4.Value--;
                            if (nudOption4.Value > 2)
                            {
                                nudOption4.Value--;
                            }
                        }
                    }

                    break;
                case 31:
                    Weapons[0] = nudOption1.Value.ToString();
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

            Points = DEFAULT_POINTS * UnitSize;

            Points += Convert.ToInt32(nudOption1.Value) * 20;
            Points += Convert.ToInt32(nudOption2.Value) * 5;
            Points += Convert.ToInt32(nudOption3.Value) * 10;
            Points += Convert.ToInt32(nudOption4.Value) * 5;
        }

        public override string ToString()
        {
            return "Storm Guardians - " + Points + "pts";
        }
    }
}
