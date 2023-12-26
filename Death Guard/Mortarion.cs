using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class Mortarion : Datasheets
    {
        public Mortarion()
        {
            DEFAULT_POINTS = 450;
            UnitSize = 1;
            TemplateCode = "ncp";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD",
                "MONSTER", "CHARACTER", "PSYKER", "FLY", "SUPREME COMMANDER", "DAEMON",
                "BUBONIC ASTARTES", "PRIMARCH", "MORTARION"
            });
            PsykerPowers = new string[2] { string.Empty, string.Empty };
            WarlordTrait = "Revolting Resilient; Living Plague; Arch-Contaminator";
            Role = "Lord of War";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            cbWarlord.Checked = true;
            cbWarlord.Enabled = false;
            cmbWarlord.Enabled = false;
            cmbWarlord.Text = WarlordTrait;

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = -1;
            }

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

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
            CheckBox isWarlord = panel.Controls["cbWarlord"] as CheckBox;
            CheckedListBox clb = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox warlord = panel.Controls["cmbWarlord"] as ComboBox;

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

            if (code == -1)
            {
                if (this.isWarlord)
                {
                    warlord.Text = WarlordTrait;
                    warlord.Enabled = false;
                }
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Mortarion - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new Mortarion();
        }
    }
}
