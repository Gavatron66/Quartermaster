using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class CorpuscariiElectroPriests : Datasheets
    {
        public CorpuscariiElectroPriests()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "CULT MECHANICUS", "<FORGE WORLD>",
                "INFANTRY", "CORE", "ELECTRO-PRIESTS", "CORPUSCARII ELECTRO-PRIESTS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new CorpuscariiElectroPriests();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;
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
            return "Corpuscarii Electro-Priests - " + Points + "pts";
        }
    }
}
