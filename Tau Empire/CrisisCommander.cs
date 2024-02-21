using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class CrisisCommander : Datasheets
    {
        public CrisisCommander()
        {
            DEFAULT_POINTS = 110;
            Points = DEFAULT_POINTS;
            TemplateCode = "6m1k_c";
            Weapons.Add("Burst Cannon (+10/+15/+20 pts)");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("(None)"); //Two drones
            Weapons.Add("(None)");
            Weapons.Add(""); //Iridium
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "<SEPT>",
                "INFANTRY", "CHARACTER", "BATTLESUIT", "FLY", "JET PACK", "CRISIS", "COMMANDER"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new CrisisCommander();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as T_au;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Airbursting Fragmentation Projector (+10/+15/+20 pts)",
                "Burst Cannon (+10/+15/+20 pts)",
                "Counterfire Defence System",
                "Cyclic Ion Blaster (+10/+20/+25 pts)",
                "Early Warning Override",
                "Fusion Blaster (+15/+20/+25 pts)",
                "Missile Pod (+10/+15/+25 pts)",
                "Multi-tracker",
                "Plasma Rifle (+10/+15/+25 pts)",
                "Shield Generator (+10 pts)",
                "Target Lock",
                "T'au Flamer (+5/+10/+15 pts)",
                "Velocity Tracker"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Airbursting Fragmentation Projector (+10/+15/+20 pts)",
                "Burst Cannon (+10/+15/+20 pts)",
                "Counterfire Defence System",
                "Cyclic Ion Blaster (+10/+20/+25 pts)",
                "Early Warning Override",
                "Fusion Blaster (+15/+20/+25 pts)",
                "Missile Pod (+10/+15/+25 pts)",
                "Multi-tracker",
                "Plasma Rifle (+10/+15/+25 pts)",
                "Shield Generator (+10 pts)",
                "Target Lock",
                "T'au Flamer (+5/+10/+15 pts)",
                "Velocity Tracker"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Airbursting Fragmentation Projector (+10/+15/+20 pts)",
                "Burst Cannon (+10/+15/+20 pts)",
                "Counterfire Defence System",
                "Cyclic Ion Blaster (+10/+20/+25 pts)",
                "Early Warning Override",
                "Fusion Blaster (+15/+20/+25 pts)",
                "Missile Pod (+10/+15/+25 pts)",
                "Multi-tracker",
                "Plasma Rifle (+10/+15/+25 pts)",
                "Shield Generator (+10 pts)",
                "Target Lock",
                "T'au Flamer (+5/+10/+15 pts)",
                "Velocity Tracker"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "(None)",
                "Airbursting Fragmentation Projector (+10/+15/+20 pts)",
                "Burst Cannon (+10/+15/+20 pts)",
                "Counterfire Defence System",
                "Cyclic Ion Blaster (+10/+20/+25 pts)",
                "Early Warning Override",
                "Fusion Blaster (+15/+20/+25 pts)",
                "Missile Pod (+10/+15/+25 pts)",
                "Multi-tracker",
                "Plasma Rifle (+10/+15/+25 pts)",
                "Shield Generator (+10 pts)",
                "Target Lock",
                "T'au Flamer (+5/+10/+15 pts)",
                "Velocity Tracker"
            });
            cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);

            cmbOption5.Items.Clear();
            cmbOption5.Items.AddRange(new string[]
            {
                "(None)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(Weapons[4]);

            cmbOption6.Items.Clear();
            cmbOption6.Items.AddRange(new string[]
            {
                "(None)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });
            cmbOption6.SelectedIndex = cmbOption6.Items.IndexOf(Weapons[5]);

            cbOption1.Text = "Iridium Battlesuit (+10 pts)";
            if (Weapons[6] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("T'au");
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
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
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
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem.ToString();
                    break;
                case 14:
                    Weapons[3] = cmbOption4.SelectedItem.ToString();
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
                    Relic = chosenRelic;
                    break;
                case 18:
                    Weapons[4] = cmbOption5.SelectedItem.ToString();
                    break;
                case 19:
                    Weapons[5] = cmbOption6.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[6] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[6] = "";
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

            int[] weapons = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

            foreach (string weapon in Weapons)
            {
                if(weapon == "Airbursting Fragmentation Projector (+10/+15/+20 pts)")
                {
                    weapons[0] += 1;
                    if (weapons[0] == 1)
                    {
                        Points += 10;
                    }
                    else if (weapons[0] == 2)
                    {
                        Points += 15;
                    }
                    else if (weapons[0] > 2)
                    {
                        Points += 20;
                    }
                }
                else if(weapon == "Burst Cannon (+10/+15/+20 pts)")
                {
                    weapons[1] += 1;
                    if (weapons[1] == 1)
                    {
                        Points += 10;
                    }
                    else if (weapons[1] == 2)
                    {
                        Points += 15;
                    }
                    else if (weapons[1] > 2)
                    {
                        Points += 20;
                    }
                }
                else if (weapon == "Cyclic Ion Blaster (+10/+20/+25 pts)")
                {
                    weapons[2] += 1;
                    if (weapons[2] == 1)
                    {
                        Points += 10;
                    }
                    else if (weapons[2] == 2)
                    {
                        Points += 20;
                    }
                    else if (weapons[2] > 2)
                    {
                        Points += 25;
                    }
                }
                else if (weapon == "Fusion Blaster (+15/+20/+25 pts)")
                {
                    weapons[3] += 1;
                    if (weapons[3] == 1)
                    {
                        Points += 15;
                    }
                    else if (weapons[3] == 2)
                    {
                        Points += 20;
                    }
                    else if (weapons[3] > 2)
                    {
                        Points += 25;
                    }
                }
                else if (weapon == "Missile Pod (+10/+15/+25 pts)")
                {
                    weapons[4] += 1;
                    if (weapons[4] == 1)
                    {
                        Points += 10;
                    }
                    else if (weapons[4] == 2)
                    {
                        Points += 15;
                    }
                    else if (weapons[4] > 2)
                    {
                        Points += 25;
                    }
                }
                else if (weapon == "Plasma Rifle (+10/+15/+25 pts)")
                {
                    weapons[5] += 1;
                    if (weapons[5] == 1)
                    {
                        Points += 10;
                    }
                    else if (weapons[5] == 2)
                    {
                        Points += 15;
                    }
                    else if (weapons[5] > 2)
                    {
                        Points += 25;
                    }
                }
                else if (weapon == "T'au Flamer (+5/+10/+15 pts)")
                {
                    weapons[6] += 1;
                    if (weapons[6] == 1)
                    {
                        Points += 5;
                    }
                    else if (weapons[6] == 2)
                    {
                        Points += 10;
                    }
                    else if (weapons[6] >= 2)
                    {
                        Points += 15;
                    }
                }
                else if (weapon == "Gun Drone (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Iridium Battlesuit (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Marker Drone (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Shield Drone (+15 pts)")
                {
                    Points += 15;
                }
                else if (weapon == "Shield Generator (+10 pts)")
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Crisis Commander - " + Points + "pts";
        }
    }
}