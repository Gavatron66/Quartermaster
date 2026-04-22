using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class TerminatorChaosLord : Datasheets
    {
        private string stratWarlordTrait;
        public TerminatorChaosLord()
        {
            DEFAULT_POINTS = 110;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m_c";
            Weapons.Add("Combi-bolter");
            Weapons.Add("Exalted Power Axe");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVIDED", "<LEGION>",
                "CHARACTER", "INFANTRY", "TERMINATOR", "CHAOS LORD"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new TerminatorChaosLord();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Combi-bolter",
                "Combi-flamer",
                "Combi-melta",
                "Combi-plasma",
                "Lightning Claw"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Chainfist",
                "Exalted Power Axe",
                "Lightning Claw",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Tainted Chainaxe"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

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

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox; // For Stratagem 3

            if (repo.currentSubFaction == "Black Legion")
            {
                cbStratagem3.Visible = true;
                cbStratagem3.Text = f.StratagemList[3];
            }

            cbStratagem3.Location = new System.Drawing.Point(cbStratagem2.Location.X, cbStratagem2.Location.Y + 32);

            panel.Controls["lblOption6"].Visible = false;
            panel.Controls["lblOption6"].Location = new System.Drawing.Point(cmbWarlord.Location.X, cmbWarlord.Location.Y + 32);
            cmbOption6.Visible = false;
            cmbOption6.Location = new System.Drawing.Point(panel.Controls["lblOption6"].Location.X, panel.Controls["lblOption6"].Location.Y + 23);
            cmbOption6.Items.Clear();
            cmbOption6.Items.AddRange(repo.GetWarlordTraits("").ToArray());

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
                stratWarlordTrait = "";
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox; // For Stratagem 3

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
                    cmbFaction.Enabled = true;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (chosenRelic == "Zaall, the Wrathful" || chosenRelic == "Talisman of Burning Blood")
                    {
                        //Khorne
                        cmbFaction.SelectedIndex = 1;
                        cmbFaction.Enabled = false;
                    }
                    else if (chosenRelic == "G'holl'ax, the Decayed" || chosenRelic == "Orb of Unlife")
                    {
                        //Nurgle
                        cmbFaction.SelectedIndex = 3;
                        cmbFaction.Enabled = false;
                    }
                    else if (chosenRelic == "Q'o'ak, the Boundless")
                    {
                        //Tzeentch
                        cmbFaction.SelectedIndex = 2;
                        cmbFaction.Enabled = false;
                    }
                    else if (chosenRelic == "Thaa'ris and Rhi'ol, the Rapacious" || chosenRelic == "Intoxicating Elixir")
                    {
                        //Slaanesh
                        cmbFaction.SelectedIndex = 4;
                        cmbFaction.Enabled = false;
                    }
                    else if(chosenRelic == "Blade of the Relentless" || chosenRelic == "Flayer" || chosenRelic == "Distortion")
                    {
                        //Power Sword
                        cmbOption2.SelectedIndex = 6;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "The Black Mace" || chosenRelic == "Eightfold-cursed Crozius" || chosenRelic == "Siegebreaker Mace")
                    {
                        //Power Maul
                        cmbOption2.SelectedIndex = 5;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Ashen Axe" || chosenRelic == "Axe of the Forgemaster")
                    {
                        //Exalted Power Axe
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Claw of the Stygian Count")
                    {
                        //Lightning Claw
                        cmbOption2.SelectedIndex = 2;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Maelstrom's Bite")
                    {
                        //Combi-melta
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }

                    if (chosenRelic == "Loyalty's Reward" || chosenRelic == "Spitespitter" || chosenRelic == "Hydra's Teeth"
                        || chosenRelic == "Hyper-Growth Bolts")
                    {
                        //Bolt Weapon
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Items.Remove("Lightning Claw");
                    }
                    else
                    {
                        if(!cmbOption1.Items.Contains("Lightning Claw"))
                        {
                            cmbOption1.Items.Add("Lightning Claw");
                        }
                    }

                    Relic = chosenRelic;
                    break;
                case 19:
                    stratWarlordTrait = cmbOption6.SelectedItem as string;
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
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Terminator Chaos Lord - " + Points + "pts";
        }
    }
}