using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Astropath : Datasheets
    {
        public Astropath()
        {
            DEFAULT_POINTS = 40;
            Points = DEFAULT_POINTS;
            TemplateCode = "p";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "MILITARUM AUXILLA", "ATTACHE", "PSYKER", "ASTROPATH"
            });
            PsykerPowers = new string[] { string.Empty };
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Astropath();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AstraMilitarum;

            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("Battle");

            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            lblPsyker.Text = "Select one of the following:";
            clbPsyker.ClearSelected();
            for (int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            switch (code)
            {
                case 60:
                    if (clbPsyker.CheckedItems.Count < 1)
                    {
                        break;
                    }
                    else if (clbPsyker.CheckedItems.Count == 1)
                    {
                        PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                    }
                    else
                    {
                        clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Astropath - " + Points + "pts";
        }
    }
}
