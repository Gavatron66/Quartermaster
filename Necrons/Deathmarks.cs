using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class Deathmarks : Datasheets
    {
        public Deathmarks()
        {
            UnitSize = 5;
            DEFAULT_POINTS = 13;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "INFANTRY", "CORE", "HYPERPHASE SWORD", "DEATHMARKS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Deathmarks();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nud.Value.ToString());
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

        }

        public override string ToString()
        {
            return "Deathmarks - " + Points + "pts";
        }
    }
}
