using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class BigEdBossbunka : Datasheets
    {
        public BigEdBossbunka()
        {
            DEFAULT_POINTS = 75;
            UnitSize = 0;
            Points = DEFAULT_POINTS;
            TemplateCode = "N1k";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "BUILDING", "VEHICLE", "TRANSPORT", "BIG'ED BOSSBUNKA"
            });
            Role = "Fortification";
        }

        public override Datasheets CreateUnit()
        {
            return new BigEdBossbunka();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Orks;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            panel.Controls["lblNumModels"].Text = "Number of Big Shootas (+5 pts/each):";

            nudUnitSize.Location = new System.Drawing.Point(nudUnitSize.Location.X + 105, nudUnitSize.Location.Y);

            nudUnitSize.Minimum = 0;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = Convert.ToInt32(UnitSize);

            cbOption1.Text = "Shoutin' Pole (+5 pts)";
            if (Weapons.Contains(""))
            {
                cbOption1.Checked = false;
            }
            else
            {
                cbOption1.Checked = true;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[0] = "";
                    }
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS;
            Points += (int)nudUnitSize.Value * 5;

            if(cbOption1.Checked)
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Big'Ed Bossbunka - " + Points + "pts";
        }
    }
}
