using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class AccursedCultists : Datasheets
    {
        public AccursedCultists()
        {
            DEFAULT_POINTS = 6;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize + 45;
            TemplateCode = "2N";
            Weapons.Add("3");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TRAITORS ASTARTES", "<LEGION>",
                "INFANTRY", "CULTISTS", "ACCURSED CULTISTS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new AccursedCultists();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;

            panel.Controls["nudOption1"].Location = new System.Drawing.Point(nudUnitSize.Location.X, 59);
            panel.Controls["lblnud1"].Text = "Number of Torments:";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 3;
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Maximum = 6;
            nudOption1.Value = Convert.ToInt32(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if(UnitSize == 10)
                    {
                        nudOption1.Enabled = true;
                    }
                    else
                    {
                        nudOption1.Enabled = false;
                        if(nudOption1.Value > 3)
                        {
                            nudOption1.Value = 3;
                        }
                    }
                    break;
                case 31:
                    Weapons[0] = nudOption1.Value.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += int.Parse(Weapons[0]) * 15;
        }

        public override string ToString()
        {
            return "Accursed Cultists - " + Points + "pts";
        }
    }
}
