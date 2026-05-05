using Roster_Builder.Aeldari.Ynnari;
using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class HemlockWraithfighter : Datasheets
    {
        string disciplineSelected;
        public HemlockWraithfighter()
        {
            DEFAULT_POINTS = 215;
            Points = DEFAULT_POINTS;
            TemplateCode = "p";
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "SPIRIT HOST", "<CRAFTWORLD>",
                "VEHICLE", "AIRCRAFT", "FLY", "PSYKER", "WRAITH CONSTRUCT", "HEMLOCK WRAITHFIGHTER"
            });
            PsykerPowers = new string[] { string.Empty };
            Role = "Flyer";
        }

        public override Datasheets CreateUnit()
        {
            return new HemlockWraithfighter();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            if (f is YnnariFaction)
            {
                repo = f as YnnariFaction;
            }
            else
            {
                repo = f as Aeldari;
            }

            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;

            if (repo is YnnariFaction)
            {
                panel.Controls["lblPsykerList"].Visible = true;
                panel.Controls["cmbDiscipline"].Visible = true;
            }

            cmbDiscipline.Items.Clear();
            cmbDiscipline.Items.Add("Battle");
            if (repo is YnnariFaction)
            {
                cmbDiscipline.Items.Add("Revenant");
            }
            disciplineSelected = "Battle";

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("Battle");
            bool doesContain = false;
            foreach (var power in psykerpowers)
            {
                if (power == PsykerPowers[0])
                {
                    doesContain = true;
                }
            }

            if (!doesContain)
            {
                psykerpowers = repo.GetPsykerPowers(disciplineSelected);
            }
            else
            {
                disciplineSelected = "Battle";
            }

            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }
            cmbDiscipline.SelectedItem = disciplineSelected;

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
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;

            switch (code)
            {
                case 111:
                    if (cmbDiscipline.SelectedItem.ToString() == disciplineSelected)
                    {
                        break;
                    }

                    disciplineSelected = cmbDiscipline.SelectedItem.ToString();
                    clbPsyker.Items.Clear();
                    clbPsyker.Items.AddRange(repo.GetPsykerPowers(disciplineSelected).ToArray());
                    PsykerPowers = new string[1] { string.Empty };
                    break;
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
            return "Hemlock Wraithfighter - " + Points + "pts";
        }
    }
}
