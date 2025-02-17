using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Custodes
{
    public class Prosecutors : Datasheets
    {
        public Prosecutors()
        {
            UnitSize = 5;
            DEFAULT_POINTS = 11;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ANATHEMA PSYKANA",
                "INFANTRY", "CORE", "PROSECUTORS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Prosecutors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdeptusCustodes;    

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
            return "Prosecutors - " + Points + "pts";
        }
    }
}
