using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class LionElJonson : Datasheets
    {
        public LionElJonson()
        {
            DEFAULT_POINTS = 320;
            UnitSize = 1;
            TemplateCode = "nc";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DARK ANGELS",
                "MONSTER", "CHARACTER", "PRIMARCH", "SUPREME COMMANDER", "INNER CIRCLE", "LION EL'JONSON"
            });
            WarlordTrait = "No Hiding from the Watchers";
        }

        public override Datasheets CreateUnit()
        {
            return new LionElJonson();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            cbWarlord.Checked = true;
            cbWarlord.Enabled = false;
            cmbWarlord.Enabled = false;
            cmbWarlord.Text = WarlordTrait;
        }

        public override void SaveDatasheets(int code, Panel panel) { }

        public override string ToString()
        {
            return "Lion El'Jonson - " + Points + "pts";
        }
    }
}
