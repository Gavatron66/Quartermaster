using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class CtanNightbringer : Datasheets
    {
        public CtanNightbringer()
        {
            DEFAULT_POINTS = 350;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "ncp";
            Keywords.AddRange(new string[]
            {
                "NECRONS",
                "MONSTER", "CHARACTER", "FLY", "C'TAN SHARD", "C'TAN SHARD OF THE NIGHTBRINGER"
            });
            PsykerPowers = new string[1] { string.Empty };
        }

        public override Datasheets CreateUnit()
        {
            return new CtanNightbringer();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
            Template.LoadTemplate(TemplateCode, panel);

            panel.Controls["cbWarlord"].Visible = false;
            panel.Controls["lblWarlord"].Visible = false;
            panel.Controls["cmbWarlord"].Visible = false;
            panel.Controls["lblRelic"].Visible = false;
            panel.Controls["cmbRelic"].Visible = false;

            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("");
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
            CheckedListBox clb = panel.Controls["clbPsyker"] as CheckedListBox;

            switch (code)
            {
                case 60:
                    if (clb.CheckedItems.Count == 1)
                    {
                        PsykerPowers[0] = clb.SelectedItem.ToString();
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
            return "C'tan Shard of the Nightbringer - " + Points + "pts";
        }
    }
}
