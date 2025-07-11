using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class PrimarisLibrarian : Datasheets
    {
        private string stratWarlordTrait;
        string disciplineSelected;

        public PrimarisLibrarian()
        {
            DEFAULT_POINTS = 85;
            Points = DEFAULT_POINTS;
            TemplateCode = "pc";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CHARACTER", "PRIMARIS", "PSYKER", "LIBRARIAN"
            });
            PsykerPowers = new string[2] { string.Empty, string.Empty };
            Role = "HQ";
        }
        public override Datasheets CreateUnit()
        {
            return new PrimarisLibrarian();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;
            panel.Controls["lblPsykerList"].Visible = true;
            panel.Controls["cmbDiscipline"].Visible = true;

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox; // For Stratagem 3

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }
            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
                if (Factionupgrade != "(None)")
                {
                    cmbWarlord.Items.Add("Psychic Mastery");
                    cmbRelic.Items.Add("Neural Shroud");
                }
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

            cmbDiscipline.Visible = true;
            panel.Controls["lblPsykerList"].Visible = true;
            cmbDiscipline.Items.Clear();
            cmbDiscipline.Items.Add("Librarius");

            switch (repo.currentSubFaction)
            {
                case "Blood Angels":
                    cmbDiscipline.Items.Add("Sanguinary");
                    disciplineSelected = "Sanguinary";
                    break;
                case "Dark Angels":
                    cmbDiscipline.Items.Add("Interromancy");
                    disciplineSelected = "Interromancy";
                    break;
                case "Imperial Fists":
                    cmbDiscipline.Items.Add("Geokinesis");
                    disciplineSelected = "Geokinesis";
                    break;
                case "Iron Hands":
                    cmbDiscipline.Items.Add("Technomancy");
                    disciplineSelected = "Technomancy";
                    break;
                case "Raven Guard":
                    cmbDiscipline.Items.Add("Umbramancy");
                    disciplineSelected = "Umbramancy";
                    break;
                case "Salamanders":
                    cmbDiscipline.Items.Add("Promethean");
                    disciplineSelected = "Promethean";
                    break;
                case "Space Wolves":
                    cmbDiscipline.Items.Add("Tempestas");
                    disciplineSelected = "Tempestas";
                    break;
                case "Ultramarines":
                    cmbDiscipline.Items.Add("Indomitus");
                    disciplineSelected = "Indomitus";
                    break;
                case "White Scars":
                    cmbDiscipline.Items.Add("Stormspeaking");
                    disciplineSelected = "Stormspeaking";
                    break;
                default:
                    disciplineSelected = "Librarius";
                    break;
            }

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

            if (!(Factionupgrade == "(None)" || Factionupgrade == null) ^ Relic == "Tome of Malcador")
            {
                lblPsyker.Text = "Select three of the following:";
                if (PsykerPowers.Length != 3)
                {
                    PsykerPowers = new string[3] { string.Empty, string.Empty, string.Empty };
                }

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
            }
            else if (!(Factionupgrade == "(None)" || Factionupgrade == null) && Relic == "Tome of Malcador")
            {
                lblPsyker.Text = "Select four of the following:";
                if (PsykerPowers.Length != 4)
                {
                    PsykerPowers = new string[4] { string.Empty, string.Empty, string.Empty, string.Empty };
                }

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
            else
            {
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

            if (Relic != null && cmbRelic.Items.Contains(Relic))
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = 0;
            }

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;

            cbStratagem3.Visible = true;
            cbStratagem3.Location = new System.Drawing.Point(cbStratagem2.Location.X, cbStratagem2.Location.Y + 32);
            cbStratagem3.Text = f.StratagemList[2];

            if (f.currentSubFaction == "<Custom>" && f.customSubFactionTraits[2] != "Unknown")
            {
                cbStratagem4.Visible = true;
            }
            else
            {
                cbStratagem4.Visible = false;
            }

            cbStratagem4.Location = new System.Drawing.Point(cbStratagem3.Location.X, cbStratagem3.Location.Y + 32);
            cbStratagem4.Text = f.StratagemList[3];

            panel.Controls["lblOption6"].Visible = false;
            panel.Controls["lblOption6"].Location = new System.Drawing.Point(panel.Controls["lblPsykerList"].Location.X, cmbDiscipline.Location.Y + 33);
            cmbOption6.Visible = false;
            cmbOption6.Location = new System.Drawing.Point(panel.Controls["lblOption6"].Location.X, panel.Controls["lblOption6"].Location.Y + 23);
            cmbOption6.Items.Clear();
            cmbOption6.Items.AddRange(repo.GetWarlordTraits("Strat").ToArray());

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

            if (Stratagem.Contains(cbStratagem3.Text))
            {
                cbStratagem3.Checked = true;
                cbStratagem3.Enabled = true;
                cmbOption6.Visible = true;
                panel.Controls["lblOption6"].Visible = true;
                cmbOption6.SelectedIndex = cmbOption6.Items.IndexOf(stratWarlordTrait);
            }
            else
            {
                cbStratagem3.Checked = false;
                cbStratagem3.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem3.Text));
                cmbOption6.Visible = false;
                panel.Controls["lblOption6"].Visible = false;
            }

            if (Stratagem.Contains(cbStratagem4.Text))
            {
                cbStratagem4.Checked = true;
                cbStratagem4.Enabled = true;
            }
            else
            {
                cbStratagem4.Checked = false;
                cbStratagem4.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem4.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;

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
                case 16:
                    Factionupgrade = cmbFaction.Text;

                    if (Factionupgrade != "(None)" && Factionupgrade != null)
                    {
                        cmbWarlord.Items.Add("Psychic Mastery");
                        cmbRelic.Items.Add("Neural Shroud");

                        if(Relic == "Tome of Malcador")
                        {
                            panel.Controls["lblPsyker"].Text = "Select four of the following:";
                            if (PsykerPowers.Length != 4)
                            {
                                PsykerPowers = new string[4] { PsykerPowers[0], PsykerPowers[1], PsykerPowers[2], string.Empty };
                            }
                        }
                        else
                        {
                            panel.Controls["lblPsyker"].Text = "Select three of the following:";
                            if (PsykerPowers.Length != 3)
                            {
                                PsykerPowers = new string[3] { PsykerPowers[0], PsykerPowers[1], string.Empty };
                            }
                        }
                    }
                    else
                    {
                        if (Relic == "Psychic Mastery")
                        {
                            cmbRelic.SelectedIndex = 0;
                        }

                        if (WarlordTrait == "Neural Shroud")
                        {
                            cmbWarlord.SelectedIndex = -1;
                        }

                        cmbWarlord.Items.Remove("Psychic Mastery");
                        cmbRelic.Items.Remove("Neural Shroud");

                        if(PsykerPowers.Length > 2)
                        {
                            if(Relic == "Tome of Malcador")
                            {
                                panel.Controls["lblPsyker"].Text = "Select three of the following:";
                                var temp = PsykerPowers;

                                if (PsykerPowers[2] != string.Empty)
                                {
                                    PsykerPowers = new string[3] { string.Empty, string.Empty, string.Empty };

                                    for (int i = 0; i < clbPsyker.Items.Count; i++)
                                    {
                                        clbPsyker.SetItemChecked(i, false);
                                    }

                                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp[0]), true);
                                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp[1]), true);
                                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp[2]), true);
                                    PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                                    PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                                    PsykerPowers[2] = clbPsyker.CheckedItems[2] as string;
                                }
                            }
                            else
                            {
                                panel.Controls["lblPsyker"].Text = "Select two of the following:";
                                var temp = PsykerPowers;

                                if (PsykerPowers[1] != string.Empty)
                                {
                                    PsykerPowers = new string[2] { string.Empty, string.Empty };

                                    for (int i = 0; i < clbPsyker.Items.Count; i++)
                                    {
                                        clbPsyker.SetItemChecked(i, false);
                                    }

                                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp[0]), true);
                                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(temp[1]), true);
                                    PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                                    PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                                }
                            }
                        }
                    }
                    break;
                case 17:
                    Relic = cmbRelic.SelectedItem.ToString();

                    if (!(Factionupgrade == "(None)" || Factionupgrade == null) ^ Relic == "Tome of Malcador")
                    {
                        panel.Controls["lblPsyker"].Text = "Select three of the following:";
                        if (PsykerPowers.Length != 3)
                        {
                            PsykerPowers = new string[3] { string.Empty, string.Empty, string.Empty };
                        }

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
                    }
                    else if (!(Factionupgrade == "(None)" || Factionupgrade == null) && Relic == "Tome of Malcador")
                    {
                        panel.Controls["lblPsyker"].Text = "Select four of the following:";
                        if (PsykerPowers.Length != 4)
                        {
                            PsykerPowers = new string[4] { string.Empty, string.Empty, string.Empty, string.Empty };
                        }

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
                    else
                    {
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
                    break;
                case 19:
                    stratWarlordTrait = cmbOption6.SelectedItem as string;
                    break;
                case 111:
                    if (cmbDiscipline.SelectedItem.ToString() == disciplineSelected)
                    {
                        break;
                    }

                    disciplineSelected = cmbDiscipline.SelectedItem.ToString();
                    clbPsyker.Items.Clear();
                    clbPsyker.Items.AddRange(repo.GetPsykerPowers(disciplineSelected).ToArray());
                    PsykerPowers = new string[2] { string.Empty, string.Empty };
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
                    break;
                case 60:
                    if (!(Factionupgrade == "(None)" || Factionupgrade == null) ^ Relic == "Tome of Malcador")
                    {
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
                    }
                    else if (!(Factionupgrade == "(None)" || Factionupgrade == null) && Relic == "Tome of Malcador")
                    {
                        if (clbPsyker.CheckedItems.Count < 4)
                        {
                            break;
                        }
                        else if (clbPsyker.CheckedItems.Count == 4)
                        {
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                            PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                            PsykerPowers[2] = clbPsyker.CheckedItems[2] as string;
                            PsykerPowers[3] = clbPsyker.CheckedItems[3] as string;
                        }
                        else
                        {
                            clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                        }
                    }
                    else
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
                case 73:
                    if (cbStratagem3.Checked && !Stratagem.Contains(cbStratagem3.Text))
                    {
                        Stratagem.Add(cbStratagem3.Text);
                        cmbOption6.Visible = true;
                        panel.Controls["lblOption6"].Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Remove(cbStratagem3.Text);
                        }
                        cmbOption6.Visible = false;
                        panel.Controls["lblOption6"].Visible = false;
                        cmbOption6.SelectedIndex = -1;
                    }
                    break;
                case 74:
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                        cmbRelic.Items.Clear();
                        Keywords.Add("Strat");
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        Keywords.Remove("Strat");
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
                        }

                        cmbRelic.Items.Clear();
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

                        if (cmbRelic.Items.Contains(Relic))
                        {
                            cmbRelic.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
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
            return "Primaris Librarian - " + Points + "pts";
        }
    }
}
