using Roster_Builder.Aeldari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Genestealers : Datasheets
    {
        int acidMaws;
        int fleshHooks;

        public Genestealers()
        {
            DEFAULT_POINTS = 16;
            UnitSize = 5;
            TemplateCode = "3N3k";
            Points = DEFAULT_POINTS * UnitSize;
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "FLESH HOOKS", "FEEDER TENDRILS", "LICTOR"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Genestealers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = UnitSize / 4;
            nudOption1.Value = acidMaws;
            lblnud1.Text = "Acid Maws (1/4 model):";

            nudOption2.Minimum = 0;
            nudOption2.Value = 0;
            nudOption2.Maximum = UnitSize / 4;
            nudOption2.Value = fleshHooks;
            lblnud2.Text = "Flesh Hooks (1/4 model):";
            lblnud2.Location = new System.Drawing.Point(lblnud2.Location.X - 10, lblnud2.Location.Y);

            cbOption1.Text = "Extended Carapace (+1 pts/model)";
            if (Weapons[0] != "")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Toxin Sacs (+3 pts/model)";
            if (Weapons[1] != "")
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Infestation Node (+20 pts)";
            if (Weapons[2] != "")
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;

            switch(code)
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
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[2] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    nudOption1.Maximum = UnitSize / 4;
                    nudOption2.Maximum = UnitSize / 4;
                    break;
                case 31:
                    acidMaws = int.Parse(nudOption1.Value.ToString());
                    break;
                case 32:
                    fleshHooks = int.Parse(nudOption2.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (!(Weapons[0] == ""))
            {
                Points += UnitSize;
            }
            if (!(Weapons[1] == ""))
            {
                Points += UnitSize * 3;
            }
            if (!(Weapons[2] == ""))
            {
                Points += 20;
            }
        }

        public override string ToString()
        {
            return "Genestealers - " + Points + "pts";
        }
    }
}
