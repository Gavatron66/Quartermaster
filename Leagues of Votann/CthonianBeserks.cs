using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class CthonianBeserks : Datasheets
    {
        public CthonianBeserks()
        {
            DEFAULT_POINTS = 33;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "3N1m";
            Weapons.Add("Heavy Plasma Axes");
            Weapons.Add("0"); //Twin Concussion Gauntlet
            Weapons.Add("0"); //Mole Grenade Launcher
            Keywords.AddRange(new string[]
            {
                "VOTANN", "<LEAGUE>",
                "INFANTRY", "CORE", "BESERKS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new CthonianBeserks();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;

            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X + 115, nudOption1.Location.Y);
            nudOption2.Location = new System.Drawing.Point(nudOption2.Location.X + 115, nudOption2.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Concussion Mauls",
                "Heavy Plasma Axes"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            lblnud1.Text = "Twin Concussion Gauntlet(s) (+5 pts) (1x/5 model):";
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Minimum = 0;
            nudOption1.Maximum = UnitSize / 5;
            nudOption1.Value = Convert.ToDecimal(Weapons[1]);

            lblnud2.Text = "Mole Greande Launcher(s) (+10 pts) (1x/5 model):";
            nudOption2.Value = nudOption2.Minimum;
            nudOption2.Minimum = 0;
            nudOption2.Maximum = UnitSize / 5;
            nudOption2.Value = Convert.ToDecimal(Weapons[2]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize < 10)
                    {
                        if (nudOption1.Value > 1)
                        {
                            nudOption1.Value--;
                        }
                        nudOption1.Maximum = 1;

                        if (nudOption2.Value > 1)
                        {
                            nudOption2.Value--;
                        }
                        nudOption2.Maximum = 1;
                    }

                    if (UnitSize == 10)
                    {
                        nudOption1.Maximum = 2;
                        nudOption2.Maximum = 2;
                    }

                    break;
                case 31:
                    Weapons[1] = nudOption1.Value.ToString();
                    break;
                case 32:
                    Weapons[2] = nudOption2.Value.ToString();
                    break;
            }

            Points = (DEFAULT_POINTS * UnitSize);

            Points += (Convert.ToInt32(nudOption1.Value * 5)) + (Convert.ToInt32(nudOption2.Value * 10));
        }

        public override string ToString()
        {
            return "Cthonian Beserks - " + Points + "pts";
        }
    }
}
