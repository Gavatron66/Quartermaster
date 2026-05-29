using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class TzaangorShaman : Datasheets
    {
        public TzaangorShaman()
        {
            DEFAULT_POINTS = 60;
            Points = DEFAULT_POINTS;
            TemplateCode = "pc";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TZEENTCH", "THOUSAND SONS",
                "CHARACTER", "CAVALRY", "FLY", "PSYKER", "TZAANGOR", "SHAMAN"
            });
            PsykerPowers = new string[1] { string.Empty };
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new TzaangorShaman();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ThousandSons;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
                cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (Relic != null && cmbRelic.Items.Contains(Relic))
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = 0;
            }

            if (Relic == "The Change-Wrought Chalice")
            {
                List<string> psykerpowers = new List<string>();
                psykerpowers = repo.GetPsykerPowers("");
                clbPsyker.Items.Clear();
                foreach (string power in psykerpowers)
                {
                    clbPsyker.Items.Add(power);
                }

                panel.Controls["lblPsyker"].Text = "Select two of the following:";
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
            else
            {
                List<string> psykerpowers = new List<string>();
                psykerpowers = repo.GetPsykerPowers("Change");
                clbPsyker.Items.Clear();
                foreach (string power in psykerpowers)
                {
                    clbPsyker.Items.Add(power);
                }

                panel.Controls["lblPsyker"].Text = "Select one of the following:";
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

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
            }

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            if (Stratagem.Contains(cbStratagem1.Text))
            {
                cbStratagem1.Checked = true;
                cbStratagem1.Enabled = true;
            }
            else
            {
                cbStratagem1.Checked = false;
                cbStratagem1.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem1.Text));
            }

            if (Stratagem.Contains(cbStratagem2.Text))
            {
                cbStratagem2.Checked = true;
                cbStratagem2.Enabled = true;
            }
            else
            {
                cbStratagem2.Checked = false;
                cbStratagem2.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem2.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 15:
                    if (cmbWarlord.SelectedIndex != -1)
                    {
                        WarlordTrait = cmbWarlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;

                    if (Relic == "The Change-Wrought Chalice")
                    {
                        List<string> psykerpowers = new List<string>();
                        psykerpowers = repo.GetPsykerPowers("");
                        clbPsyker.Items.Clear();
                        foreach (string power in psykerpowers)
                        {
                            clbPsyker.Items.Add(power);
                        }

                        panel.Controls["lblPsyker"].Text = "Select two of the following:";
                        var temp2 = PsykerPowers;

                        PsykerPowers = new string[2] { string.Empty, string.Empty };
                        if (temp2[0] != string.Empty)
                        {

                            for (int i = 0; i < clbPsyker.Items.Count; i++)
                            {
                                clbPsyker.SetItemChecked(i, false);
                            }

                            clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp2[0]), true);
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        }
                    }
                    else
                    {
                        List<string> psykerpowers = new List<string>();
                        psykerpowers = repo.GetPsykerPowers("Change");
                        clbPsyker.Items.Clear();
                        foreach (string power in psykerpowers)
                        {
                            clbPsyker.Items.Add(power);
                        }

                        panel.Controls["lblPsyker"].Text = "Select one of the following:";
                        var temp3 = PsykerPowers;

                        PsykerPowers = new string[1] { string.Empty };
                        if (temp3[0] != string.Empty)
                        {

                            for (int i = 0; i < clbPsyker.Items.Count; i++)
                            {
                                clbPsyker.SetItemChecked(i, false);
                            }

                            if (clbPsyker.Items.Contains(temp3[0]))
                            {
                                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp3[0]), true);
                                PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                            }
                        }
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
                    break;
                case 60:
                    if (Relic == "The Change-Wrought Chalice")
                    {
                        if (clbPsyker.CheckedItems.Count < 2)
                        {
                            break;
                        }
                        else if (clbPsyker.CheckedItems.Count == 2)
                        {
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                            PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                        }
                        else
                        {
                            clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                        }
                    }
                    else
                    {
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
                    }
                    break;
                case 71:
                    if (cbStratagem1.Checked)
                    {
                        Stratagem.Add(cbStratagem1.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem1.Text))
                        {
                            Stratagem.Remove(cbStratagem1.Text);
                        }
                    }
                    break;
                case 72:
                    if (cbStratagem2.Checked)
                    {
                        Stratagem.Add(cbStratagem2.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem2.Text))
                        {
                            Stratagem.Remove(cbStratagem2.Text);
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Tzaangor Shaman - " + Points + "pts";
        }
    }
}
