using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Autarch : Datasheets
    {
        public Autarch()
        {
            DEFAULT_POINTS = 85;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m3k_c";
            Weapons.Add("Shuriken Pistol");
            Weapons.Add("Star Glaive");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "CHARACTER", "INFANTRY", "AUTARCH"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Autarch();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            Label lblRelic = panel.Controls["lblRelic"] as Label;

            lblRelic.Location = new System.Drawing.Point(lblRelic.Location.X, lblRelic.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cmbRelic.Location.X, cmbRelic.Location.Y + 30);

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Death Spinner (+10 pts)",
                "Dragon Fusion Gun (+15 pts)",
                "Reaper Launcher (+15 pts)",
                "Shuriken Pistol"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Scorpion Chainsword",
                "Star Glaive"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Autarch Jump Generator (+20 pts)";
            if (Weapons[2] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Howling Banshee Mask (+5 pts)";
            if (Weapons[3] == cbOption1.Text)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Fusion Pistol, Banshee Blade, Mandiblasters \nand Swooping Hawk Wing (+25 pts)";
            if (Weapons[4] == cbOption1.Text)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
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
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
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
                    if(chosenRelic == "Kurnous' Bow")
                    {
                        cmbOption1.SelectedIndex = 3;
                        cmbOption1.Enabled = false;
                        cbOption3.Enabled = false;
                    }
                    else if (chosenRelic == "Shard of Anaris")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Firesabre")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        cbOption3.Enabled = true;
                    }
                    Relic = chosenRelic;
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
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[3] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[4] = cbOption3.Text;
                        Weapons[0] = string.Empty;
                        Weapons[1] = string.Empty;
                        Weapons[2] = string.Empty;
                        Weapons[3] = string.Empty;

                        cmbOption1.Enabled = false;
                        cmbOption2.Enabled = false;
                        cbOption1.Enabled = false;
                        cbOption2.Enabled = false;

                        cbOption1.Checked = false;
                        cbOption2.Checked = false;

                        cmbOption1.SelectedItem = "Shuriken Pistol";
                        cmbOption2.SelectedItem = "Star Glaive";
                    }
                    else
                    {
                        Weapons[4] = "";

                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        cbOption1.Enabled = true;
                        cbOption2.Enabled = true;
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
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

            if(Weapons.Contains("Howling Banshee Mask (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Death Spinner (+10 pts)"))
            {
                Points += 10;
            }
            if (Weapons.Contains("Dragon Fusion Gun (+15 pts)") || Weapons.Contains("Reaper Launcher (+15 pts)"))
            {
                Points += 15;
            }
            if (Weapons.Contains("Autarch Jump Generator (+20 pts)"))
            {
                Points += 20;
            }
            if (Weapons.Contains(cbOption3.Text))
            {
                Points += 25;
            }
        }

        public override string ToString()
        {
            return "Autarch - " + Points + "pts";
        }
    }
}