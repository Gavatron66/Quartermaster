using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Ultramarines
{
    public class ChiefLibrarianTigurius : Datasheets
    {
        string disciplineSelected;

        public ChiefLibrarianTigurius()
        {
            DEFAULT_POINTS = 120;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "ncp";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "ULTRAMARINES",
                "CHARACTER", "INFANTRY", "PRIMARIS", "PSYKER", "LIBRARIAN", "CHIEF LIBRARIAN TIGURIUS"
            });
            PsykerPowers = new string[3] { string.Empty, string.Empty, string.Empty };
            WarlordTrait = "Master of Strategy";
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new ChiefLibrarianTigurius();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            cmbDiscipline.Visible = true;
            panel.Controls["lblPsykerList"].Visible = true;
            cmbDiscipline.Items.Clear();
            cmbDiscipline.Items.Add("Librarius");
            cmbDiscipline.Items.Add("Indomitus");

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("Librarius");
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
                disciplineSelected = "Librarius";
            }

            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            cmbDiscipline.SelectedItem = disciplineSelected;

            lblPsyker.Text = "Select three of the following:";
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

            cmbWarlord.Enabled = false;
            cmbWarlord.Items.Clear();
            cmbWarlord.Items.Add("Master of Strategy");
            cmbWarlord.SelectedIndex = 0;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
            }
            else
            {
                cbWarlord.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

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
                    PsykerPowers = new string[3] { string.Empty, string.Empty, string.Empty };
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                        cmbWarlord.Text = WarlordTrait;
                        cmbWarlord.Enabled = false;
                    }
                    else { this.isWarlord = false; }
                    break;
                default: break;
                case 60:
                    if (clbPsyker.CheckedItems.Count < 3)
                    {
                        break;
                    }
                    else if (clbPsyker.CheckedItems.Count == 3)
                    {
                        PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                        PsykerPowers[2] = clbPsyker.CheckedItems[2] as string;
                    }
                    else
                    {
                        clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                    }
                    break;
            }
        }

        public override string ToString()
        {
            return "Chief Librarian Tigurius - " + Points + "pts";
        }
    }
}
