using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class RipperSwarms : Datasheets
    {
        public RipperSwarms()
        {
            DEFAULT_POINTS = 15;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1k";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "SWARM", "RIPPER SWARMS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new RipperSwarms();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            cbOption1.Text = "Spinemaws (+10 pts)";
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

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] != "")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Ripper Swarms - " + Points + "pts";
        }
    }
}
