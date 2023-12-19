using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class TesseractVault : Datasheets
    {
        public TesseractVault()
        {
            DEFAULT_POINTS = 360;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "ncp";
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "VEHICLE", "TITANIC", "FLY", "C'TAN SHARD", "TESSERACT VAULT"
            });
            PsykerPowers = new string[4] { string.Empty, string.Empty, string.Empty, string.Empty };
            role = "Lord of War";
        }

        public override Datasheets CreateUnit()
        {
            return new TesseractVault();
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

            lblPsyker.Text = "Select four of the following:";
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
            if (PsykerPowers[2] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[2]), true);
            }
            if (PsykerPowers[3] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[3]), true);
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckedListBox clb = panel.Controls["clbPsyker"] as CheckedListBox;

            switch (code)
            {
                case 60:
                    if (clb.CheckedItems.Count < 4)
                    {
                        break;
                    }
                    else if (clb.CheckedItems.Count == 4)
                    {
                        PsykerPowers[0] = clb.CheckedItems[0] as string;
                        PsykerPowers[1] = clb.CheckedItems[1] as string;
                        PsykerPowers[2] = clb.CheckedItems[2] as string;
                        PsykerPowers[3] = clb.CheckedItems[3] as string;
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
            return "Tesseract Vault - " + Points + "pts";
        }
    }
}
