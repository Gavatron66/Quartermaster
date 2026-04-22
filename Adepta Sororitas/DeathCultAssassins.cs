using Roster_Builder.Death_Guard;
using Roster_Builder.Necrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class DeathCultAssassins : Datasheets
    {
        public DeathCultAssassins()
        {
            UnitSize = 2;
            DEFAULT_POINTS = 13;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM",
                "INFANTRY", "CULT IMPERIALIS", "CORE", "ECCLESIARCHY BATTLE CONCLAVE", "DEATH CULT ASSASSINS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathCultAssassins();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 2;
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
            return "Death Cult Assassins - " + Points + "pts";
        }
    }
}