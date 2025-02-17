using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class TheSilentKing : Datasheets
    {
        public TheSilentKing()
        {
            DEFAULT_POINTS = 400;
            TemplateCode = "nc";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "SZAREKHAN",
                "VEHICLE", "CHARACTER", "FLY", "SUPREME COMMANDER", "PHAERON", "NOBLE",
                "DYNASTIC AGENT", "THE SILENT KING", "SZAREKH",
                "TRIARCHAL MENHIRS"
            });
            WarlordTrait = "The Triarch's Will";
            Role = "Lord of War";
        }
        public override Datasheets CreateUnit()
        {
            return new TheSilentKing();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            cbWarlord.Checked = true;
            cbWarlord.Enabled = false;
            cmbWarlord.Enabled = false;
            cmbWarlord.Items.Clear();
            cmbWarlord.Items.Add("The Triarch's Will");
            cmbWarlord.SelectedIndex = 0;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "The Silent King - " + Points + "pts";
        }
    }
}
