using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class TranscendentCtan : Datasheets
    {
        public TranscendentCtan()
        {
            DEFAULT_POINTS = 270;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "ncp";
            Keywords.AddRange(new string[]
            {
                "NECRONS",
                "MONSTER", "CHARACTER", "FLY", "C'TAN SHARD", "TRANSCENDENT C'TAN"
            });
            PsykerPowers = new string[2] { string.Empty, string.Empty };
        }

        public override Datasheets CreateUnit()
        {
            return new TranscendentCtan();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            panel.Controls["cbWarlord"].Visible = false;
            panel.Controls["lblWarlord"].Visible = false;
            panel.Controls["cmbWarlord"].Visible = false;
            panel.Controls["lblRelic"].Visible = false;
            panel.Controls["cmbRelic"].Visible = false;

            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            lblPsyker.Text = "Select two of the following:";
            clbPsyker.ClearSelected();
            for (int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
            }
            if (PsykerPowers[1] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[1]), true);
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckedListBox clb = panel.Controls["clbPsyker"] as CheckedListBox;

            switch (code)
            {
                case 60:
                    if (clb.CheckedItems.Count < 2)
                    {
                        break;
                    }
                    else if (clb.CheckedItems.Count == 2)
                    {
                        PsykerPowers[0] = clb.CheckedItems[0] as string;
                        PsykerPowers[1] = clb.CheckedItems[1] as string;
                    }
                    else
                    {
                        clb.SetItemChecked(clb.SelectedIndex, false);
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Transcendent C'tan - " + Points + "pts";
        }
    }
}
