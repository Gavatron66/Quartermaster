using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class HiveTyrant : Datasheets
    {
        public HiveTyrant()
        {
            DEFAULT_POINTS = 180;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m2k_pc";
            Weapons.Add("Monstrous Bonesword and Lash Whip");
            Weapons.Add("Two Monstrous Scything Talons");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "MONSTER", "CHARACTER", "SYNAPSE", "PSYKER", "HORNED CHITIN", "HIVE TYRANT"
            });
            PsykerPowers = new string[2] { string.Empty, string.Empty };
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new HiveTyrant();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Venom Cannon (+15 pts)",
                "Monstrous Bonesword and Lash Whip",
                "Stranglethorn Cannon (+10 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Venom Cannon (+15 pts)",
                "Stranglethorn Cannon (+10 pts)",
                "Two Monstrous Scything Talons"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Adrenal Glands (+15 pts)";
            if (Weapons[2] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Toxin Sacs (+5 pts)";
            if (Weapons[3] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
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
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    if (Weapons[0] == "Stranglethorn Cannon (+10 pts)" || Weapons[0] == "Heavy Venom Cannon (+15 pts)")
                    {
                        cmbOption2.Items.Remove("Stranglethorn Cannon (+10 pts)");
                        cmbOption2.Items.Remove("Heavy Venom Cannon (+15 pts)");
                    }
                    else if (!cmbOption2.Items.Contains("Heavy Venom Cannon (+15 pts)"))
                    {
                        cmbOption2.Items.Insert(0, "Heavy Venom Cannon (+15 pts)");
                        cmbOption2.Items.Insert(1, "Stranglethorn Cannon (+10 pts)");
                    }
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    if (Weapons[1] == "Stranglethorn Cannon (+10 pts)" || Weapons[1] == "Heavy Venom Cannon (+15 pts)")
                    {
                        cmbOption1.Items.Remove("Stranglethorn Cannon (+10 pts)");
                        cmbOption1.Items.Remove("Heavy Venom Cannon (+15 pts)");
                    }
                    else if (!cmbOption1.Items.Contains("Heavy Venom Cannon (+15 pts)"))
                    {
                        cmbOption1.Items.Insert(0, "Heavy Venom Cannon (+15 pts)");
                        cmbOption1.Items.Insert(2, "Stranglethorn Cannon (+10 pts)");
                    }
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
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    cbOption1.Enabled = true;
                    cbOption2.Enabled = true;
                    if(chosenRelic == "The Reaper of Obliterax")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Scythes of Tyran")
                    {
                        cmbOption2.SelectedIndex = 2;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Balethorn Cannon")
                    {
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Shardgullet")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Passenger")
                    {
                        cbOption1.Checked = true;
                        cbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Searhive")
                    {
                        cbOption2.Checked = true;
                        cbOption2.Enabled = false;
                    }
                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[3] = cbOption2.Text;
                    }
                    else { Weapons[3] = string.Empty; }
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
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons.Contains("Adrenal Glands (+15 pts)"))
            {
                Points += 15;
            }
            if (Weapons.Contains("Heavy Venom Cannon (+15 pts)"))
            {
                Points += 15;
            }
            if (Weapons.Contains("Stranglethorn Cannon (+10 pts)"))
            {
                Points += 10;
            }
            if (Weapons.Contains("Toxin Sacs (+5 pts)"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Hive Tyrant - " + Points + "pts";
        }
    }
}
