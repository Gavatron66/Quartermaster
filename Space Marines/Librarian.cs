﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Librarian : Datasheets
    {
        string disciplineSelected;
        public Librarian()
        {
            DEFAULT_POINTS = 80;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m1k_pc";
            Weapons.Add("Bolt Pistol");
            Weapons.Add("Force Stave");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CHARACTER", "PSYKER", "LIBRARIAN"
            });
            PsykerPowers = new string[2] { string.Empty, string.Empty };
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Librarian();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;
            panel.Controls["lblPsykerList"].Visible = true;
            panel.Controls["cmbDiscipline"].Visible = true;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Boltgun",
                "Bolt Pistol",
                "Combi-flamer",
                "Combi-grav",
                "Combi-melta",
                "Combi-plasma",
                "Grav Pistol",
                "Plasma Pistol",
                "Storm Bolter"
            });
            if (f.currentSubFaction == "Blood Angels" || f.currentSubFaction == "Deathwatch")
            {
                cmbOption1.Items.Insert(7, "Hand Flamer");
                cmbOption1.Items.Insert(8, "Inferno Pistol");
            }
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Force Axe",
                "Force Stave",
                "Force Sword"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Jump Pack (+25 pts)";
            if (Weapons[2] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

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

            if (Relic != null)
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = -1;
            }

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
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
            foreach(var power in psykerpowers)
            {
                if (power == PsykerPowers[0])
                {
                    doesContain = true;
                }
            }

            if(!doesContain)
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
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
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
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    if (chosenRelic == "Primarch's Wrath")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Boltgun");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Purgatorus")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Vengeance of Ultramar")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Storm Bolter");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Sunwrath Pistol")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Plasma Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Wrath of Prometheus")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Boltgun");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Nocturne's Vengeance")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Combi-flamer");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Raven's Fury")
                    {
                        cbOption1.Checked = true;
                        cbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Silentus Pistol")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Betrayer's Bane")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Combi-melta");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Equis-pattern Bolt Pistol")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Spartean")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Bolt Pistol");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                        cbOption1.Enabled = true;
                    }
                    Relic = chosenRelic;
                    break;
                case 111:
                    if(cmbDiscipline.SelectedItem.ToString() == disciplineSelected)
                    {
                        break;
                    }

                    disciplineSelected = cmbDiscipline.SelectedItem.ToString();
                    clbPsyker.Items.Clear();
                    clbPsyker.Items.AddRange(repo.GetPsykerPowers(disciplineSelected).ToArray());
                    PsykerPowers = new string[2] {string.Empty, string.Empty};
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
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

            if (Weapons[2] == "Jump Pack (+25 pts)")
            {
                Points += 25;
            }
        }

        public override string ToString()
        {
            return "Librarian - " + Points + "pts";
        }
    }
}
