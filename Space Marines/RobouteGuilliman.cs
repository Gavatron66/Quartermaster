using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class RobouteGuilliman : Datasheets
    {
        public RobouteGuilliman()
        {
            DEFAULT_POINTS = 300;
            UnitSize = 1;
            TemplateCode = "nc";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "ULTRAMARINES",
                "CHARACTER", "MONSTER", "PRIMARCH", "ROBOUTE GUILLIMAN"
            });
            WarlordTrait = "Nobility Made Manifest";
            Role = "Lord of War";
        }

        public override Datasheets CreateUnit()
        {
            return new RobouteGuilliman();
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
            return "Roboute Guilliman - " + Points + "pts";
        }
    }
}
