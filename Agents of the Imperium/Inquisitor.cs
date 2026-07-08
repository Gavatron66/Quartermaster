using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class Inquisitor : Datasheets
    {
        bool psyker = false;

        public Inquisitor()
        {
            DEFAULT_POINTS = 60;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m1k_pc";
            Weapons.Add("Bolt Pistol");
            Weapons.Add("Chainsword");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "INQUISITION", "<ORDO>", "AGENTS OF THE IMPERIUM",
                "CHARACTER", "INFANTRY", "INQUISITOR"
            });
            PsykerPowers = new string[1] { string.Empty };
            Role = "HQ";
            Factionupgrade = "Ordo Minoris";
        }

        public override Datasheets CreateUnit()
        {
            return new Inquisitor();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ImperialAgents;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            panel.Controls["cbStratagem1"].Visible = false;
            panel.Controls["cbStratagem2"].Visible = false;
            panel.Controls["lblRelic"].Visible = false;
            panel.Controls["cmbRelic"].Visible = false;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Boltgun",
                "Bolt Pistol",
                "Combi-flamer (+10 pts)",
                "Combi-melta (+10 pts)",
                "Combi-plasma (+10 pts)",
                "Condemnor Boltgun",
                "Flamer (+5 pts)",
                "Hot-shot Lasgun",
                "Incinerator",
                "Inferno Pistol (+5 pts)",
                "Meltagun (+10 pts)",
                "Needle Pistol",
                "Plasma Gun (+10 pts)",
                "Plasma Pistol (+5 pts)",
                "Storm Bolter (+5 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Chainsword",
                "Power Fist (+10 pts)",
                "Power Maul (+5 pts)",
                "Power Sword (+5 pts)",
                "Thunder Hammer (+15 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Make this Inquisitor a Psyker?";
            cbOption1.Checked = psyker;

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

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);

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

            lblPsyker.Visible = psyker;
            clbPsyker.Visible = psyker;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

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
                    Factionupgrade = cmbFaction.SelectedItem.ToString();

                    if (clbPsyker.Items.Count == 7)
                    {
                        if (clbPsyker.CheckedIndices.Count > 0 && clbPsyker.CheckedIndices[0] == 6)
                        {
                            clbPsyker.SetItemChecked(6, false);
                        }

                        clbPsyker.Items.Remove(clbPsyker.Items[6]);
                    }

                    switch (cmbFaction.SelectedIndex)
                    {
                        case 0:
                            clbPsyker.Items.Add("Scourging");
                            break;
                        case 1:
                            clbPsyker.Items.Add("Warding Incantation");
                            break;
                        case 2:
                            clbPsyker.Items.Add("Psychic Veil");
                            break;
                        default: break;
                    }
                    break;
                case 21:
                    psyker = cbOption1.Checked;
                    lblPsyker.Visible = psyker;
                    clbPsyker.Visible = psyker;

                    if(!psyker)
                    {
                        PsykerPowers = new string[] { string.Empty };
                    }

                    if (clbPsyker.Items.Count == 7)
                    {
                        if (clbPsyker.CheckedIndices.Count > 0 && clbPsyker.CheckedIndices[0] == 6)
                        {
                            clbPsyker.SetItemChecked(6, false);
                        }

                        clbPsyker.Items.Remove(clbPsyker.Items[6]);
                    }

                    switch (cmbFaction.SelectedIndex)
                    {
                        case 0:
                            clbPsyker.Items.Add("Scourging");
                            break;
                        case 1:
                            clbPsyker.Items.Add("Warding Incantation");
                            break;
                        case 2:
                            clbPsyker.Items.Add("Psychic Veil");
                            break;
                        default: break;
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
                    if (clbPsyker.CheckedItems.Count == 1)
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

            foreach (string weapon in Weapons)
            {
                if (weapon.Contains("(+5 pts)"))
                {
                    Points += 5;
                }
                else if (weapon.Contains("(+10 pts)"))
                {
                    Points += 10;
                }
                else if (weapon.Contains("(+15 pts)"))
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Inquisitor - " + Points + "pts";
        }
    }
}