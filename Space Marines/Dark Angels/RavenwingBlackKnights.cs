using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Dark_Angels
{
    internal class RavenwingBlackKnights : Datasheets
    {
        public RavenwingBlackKnights()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "3NS(1m)";
            Weapons.Add("0");  //Corvus Hammers
            Weapons.Add("0");   //Astartes Grenade Launchers
            Weapons.Add("(None)"); //Huntmaster
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DARK ANGELS",
                "BIKER", "CORE", "MELTA BOMBS", "INNER CIRCLE", "RAVENWING", "RAVENWING BLACK KNIGHTS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new RavenwingBlackKnights();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;

            panel.Controls["lblnud1"].Text = "Models with Corvus Hammers:";
            panel.Controls["lblnud2"].Text = "Models with Astartes Grenade Launchers \n(1x/3 models):";
            panel.Controls["lblnud2"].Location = new System.Drawing.Point(panel.Controls["lblnud2"].Location.X, panel.Controls["lblnud2"].Location.Y - 10);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;
            nudOption1.Location = new System.Drawing.Point(429, 59);

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;
            nudOption2.Location = new System.Drawing.Point(429, 91);

            nudOption1.Value = int.Parse(Weapons[0]);
            nudOption2.Value = int.Parse(Weapons[1]);

            gb.Text = "Ravenwing Huntmaster";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Corvus Hammer",
                "Power Sword",
                "Power Maul"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[2]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;

            switch (code)
            {
                case 30:
                    int oldSize = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
                case 31:
                    if(nudOption1.Value > nudUnitSize.Value - 1)
                    {
                        nudOption1.Value--;
                    }
                    else
                    {
                        Weapons[0] = nudOption1.Value.ToString();
                    }
                    break;
                case 32:
                    if (nudOption2.Value > nudUnitSize.Value / 3)
                    {
                        nudOption2.Value--;
                    }
                    else
                    {
                        Weapons[1] = nudOption2.Value.ToString();
                    }
                    break;
                case 411:
                    Weapons[2] = gb_cmbOption1.SelectedItem.ToString();
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Ravenwing Black Knights - " + Points + "pts";
        }
    }
}
