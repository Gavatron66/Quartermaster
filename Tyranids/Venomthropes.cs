using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Venomthropes : Datasheets
    {
        public Venomthropes()
        {
            UnitSize = 3;
            DEFAULT_POINTS = 40;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "FLY", "CORE", "SPORECASTER", "TOXIC LASHES", "FEEDER TENDRILS", "VENOMTHROPES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Venomthropes();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
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
            return "Venomthropes - " + Points + "pts";
        }
    }
}
