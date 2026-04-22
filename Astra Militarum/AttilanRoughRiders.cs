using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class AttilanRoughRiders : Datasheets
    {
        public AttilanRoughRiders()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2N1k";
            Weapons.Add("0"); //Goad Lances (1x/5)
            Weapons.Add(""); //Power Sabre (Sergeant)
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "ATTILAN ROUGH RIDERS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new AttilanRoughRiders();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            panel.Controls["lblnud1"].Text = "Number of Goad Lances (1x/5 models):";
            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X + 35, nudOption1.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = UnitSize / 5;
            nudOption1.Value = Convert.ToInt32(Weapons[0]);

            cbOption1.Text = "Power Sabre (Sergeant only)";
            if (Weapons[1] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch(code)
            {
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize == 10)
                    {
                        nudOption1.Maximum = 2;
                    }

                    if (UnitSize < 10 && nudOption1.Value == 2)
                    {
                        nudOption1.Value--;
                        nudOption1.Maximum = 1;
                    }

                    break;
                case 31:
                    Weapons[0] = nudOption1.Value.ToString();
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Attilan Rough Riders - " + Points + "pts";
        }
    }
}
