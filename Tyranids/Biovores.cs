using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Biovores : Datasheets
    {
        public Biovores()
        {
            DEFAULT_POINTS = 45;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "carnifex";
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "BIOVORES"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Biovores();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;



            panel.Controls["lblOption1"].Visible = true;

            panel.Controls["cmbOption1"].Visible = true;

            panel.Controls["lblOption2"].Visible = true;

            panel.Controls["cmbOption2"].Visible = true;

            panel.Controls["lblOption3"].Visible = true;

            panel.Controls["cmbOption3"].Visible = true;

            panel.Controls["lblOption4"].Visible = true;

            panel.Controls["cmbOption4"].Visible = true;

            panel.Controls["lblOption5"].Visible = true;

            panel.Controls["cmbOption5"].Visible = true;

            panel.Controls["cbOption1"].Visible = true;

            panel.Controls["cbOption2"].Visible = true;

            panel.Controls["cbOption3"].Visible = true;

            panel.Controls["lblFactionUpgrade"].Visible = true;

            panel.Controls["cmbFactionUpgrade"].Visible = true;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Biovores - " + Points + "pts";
        }
    }
}
