using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class CourtOfTheArchon : Datasheets
    {
        public CourtOfTheArchon()
        {
            UnitSize = 0;     //Lhamaean
            Points = 0;
            TemplateCode = "4N";
            Weapons.Add("0"); //Medusae
            Weapons.Add("0"); //Sslyth
            Weapons.Add("0"); //Ur-Ghul
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<KABAL>",
                "INFANTRY", "CORE", "COURT OF THE ARCHON"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new CourtOfTheArchon();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Drukhari;
            Template.LoadTemplate(TemplateCode, panel);

            Label lblNumModels = panel.Controls["lblNumModels"] as Label;
            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;
            Label lblnud3 = panel.Controls["lblnud3"] as Label;
            Label lblnud4 = panel.Controls["lblnud4"] as Label;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;

            lblNumModels.Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lblnud1.Text = "Lhamaean (+16 pts):";
            lblnud1.Location = new System.Drawing.Point(lblNumModels.Location.X, lblNumModels.Location.Y);

            lblnud2.Text = "Medusae (+22 pts):";
            lblnud2.Location = new System.Drawing.Point(lblNumModels.Location.X, nudOption1.Location.Y);
            nudOption1.Location = new System.Drawing.Point(nudUnitSize.Location.X, nudOption1.Location.Y);

            lblnud3.Text = "Ssylth (+18 pts):";
            lblnud3.Location = new System.Drawing.Point(lblNumModels.Location.X, nudOption2.Location.Y);
            nudOption2.Location = new System.Drawing.Point(nudUnitSize.Location.X, nudOption2.Location.Y);

            lblnud4.Text = "Ur-Ghul (+16 pts):";
            lblnud4.Location = new System.Drawing.Point(lblNumModels.Location.X, nudOption3.Location.Y);
            nudOption3.Location = new System.Drawing.Point(nudUnitSize.Location.X, nudOption3.Location.Y);
            lblnud4.Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 0;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 4;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = 4;
            nudOption1.Value = Convert.ToDecimal(Weapons[0]);

            nudOption2.Minimum = 0;
            nudOption2.Value = 0;
            nudOption2.Maximum = 4;
            nudOption2.Value = Convert.ToDecimal(Weapons[1]);

            nudOption3.Minimum = 0;
            nudOption3.Value = 0;
            nudOption3.Maximum = 4;
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
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
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
            }

            Points = 0;
            Points += Convert.ToInt32(nudUnitSize.Value) * 16;
            Points += Convert.ToInt32(nudOption1.Value) * 22;
            Points += Convert.ToInt32(nudOption2.Value) * 18;
            Points += Convert.ToInt32(nudOption3.Value) * 16;
        }

        public override string ToString()
        {
            return "Court of the Archon - " + Points + "pts";
        }
    }
}
