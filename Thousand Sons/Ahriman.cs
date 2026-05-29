using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class Ahriman : Datasheets
    {
        bool onDisc = false;

        public Ahriman()
        {
            DEFAULT_POINTS = 160;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1k_pc";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TZEENTCH", "HERETIC ASTARTES", "ARCANA ASTARTES", "THOUSAND SONS",
                "CHARACTER", "INFANTRY", "PSYKER", "EXALTED SORCERER", "AHRIMAN"
            });
            PsykerPowers = new string[3] { string.Empty, string.Empty, string.Empty };
            WarlordTrait = "Otherworldly Prescience";
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Ahriman();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ThousandSons;
            Template.LoadTemplate(TemplateCode, panel);

            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
            panel.Controls["lblFactionupgrade"].Visible = false;
            cmbFaction.Visible = false;
            panel.Controls["cbStratagem1"].Visible = false;
            panel.Controls["cbStratagem2"].Visible = false;

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

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
            cmbWarlord.Items.Add(WarlordTrait);
            cmbWarlord.SelectedIndex = 0;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
            }
            else
            {
                cbWarlord.Checked = false;
            }

            cbOption1.Text = "Disc of Tzeentch (+20 pts)";
            if(onDisc)
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
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 21:
                    if(cbOption1.Checked)
                    {
                        onDisc = true;
                    }
                    else
                    {
                        onDisc = false;
                    }
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
                default: break;
                }

            Points = DEFAULT_POINTS + (onDisc ? 20 : 0);
        }

        public override string ToString()
        {
            return "Ahriman - " + Points + "pts";
        }
    }
}
