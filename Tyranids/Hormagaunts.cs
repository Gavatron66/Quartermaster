using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Hormagaunts : Datasheets
    {
        public Hormagaunts()
        {
            DEFAULT_POINTS = 8;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N2k";
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "CORE", "ENDLESS MULTITUDE", "HORMAGAUNTS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Hormagaunts();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 30;
            nudUnitSize.Value = currentSize;

            cbOption1.Text = "Adrenal Glands (+2 pts/model)";
            if (Weapons[0] != "")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Toxin Sacs (+1 pts/model)";
            if (Weapons[1] != "")
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

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
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[1] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (cbOption1.Checked)
            {
                Points += UnitSize * 2;
            }
            if (cbOption2.Checked)
            {
                Points += UnitSize;
            }
        }

        public override string ToString()
        {
            return "Hormagaunts - " + Points + "pts";
        }
    }
}
