using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class SubductorSquad : Datasheets
    {
        bool nuncio = false;
        bool cyberMastif = false;

        public SubductorSquad()
        {
            DEFAULT_POINTS = 120;
            Points = DEFAULT_POINTS;
            UnitSize = 10;
            TemplateCode = "2k";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ARBITES", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "CORE", "SUBDUCTOR SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new SubductorSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ImperialAgents;

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cbOption1.Text = "Nuncio Aquila (Proctor-Subductor, +10 pts)";
            cbOption1.Checked = nuncio;

            cbOption2.Text = "Cyber-mastiff (+10 pts)";
            cbOption2.Checked = cyberMastif;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch (code)
            {
                case 21:
                    nuncio = cbOption1.Checked;
                    break;
                case 22:
                    cyberMastif = cbOption2.Checked;
                    break;
            }

            Points = DEFAULT_POINTS + (nuncio ? 10 : 0) + (cyberMastif ? 10 : 0);
        }

        public override string ToString()
        {
            return "Subductor Squad - " + Points + "pts";
        }
    }
}
