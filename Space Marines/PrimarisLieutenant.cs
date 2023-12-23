using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class PrimarisLieutenant : Datasheets
    {
        public PrimarisLieutenant()
        {
            DEFAULT_POINTS = 65;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m_c";
            Weapons.Add("Master-crafted Auto Bolt Rifle and Bolt Pistol");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CHARACTER", "PRIMARIS", "LIEUTENANT"
            });
            role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new PrimarisLieutenant();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Master-crafted Auto Bolt Rifle and Bolt Pistol",
                "Master-crafted Stalker Bolt Rifle and Bolt Pistol",
                "Master-crafted Power Sword, Neo-volkite Pistol and Storm Shield",
            });
            if (f.currentSubFaction == "Dark Angels")
            {
                cmbOption1.Items.Add("Master-crafted Auto Bolt Rifle and Plasma Pistol");
                cmbOption1.Items.Add("Master-crafted Stalker Bolt Rifle and Plasma Pistol");
            }
            if (f.currentSubFaction == "Space Wolves")
            {
                cmbOption1.Items.Add("Special Issue Bolt Carbine, Master-crafted Power Axe and Bolt Pistol");
            }
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

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
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
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
                    if (chosenRelic == "Bellicos Bolt Rifle")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Lament")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Primarch's Wrath")
                    {
                        cmbOption1.SelectedIndex = 3;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Purgatorus")
                    {
                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Master-crafted Auto Bolt Rifle and Bolt Pistol",
                            "Master-crafted Stalker Bolt Rifle and Bolt Pistol",
                        });
                    }
                    else if (chosenRelic == "The Burning Blade")
                    {
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Shield Eternal")
                    {
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                        if (!cmbOption1.Items.Contains("Master-crafted Power Sword, Neo-volkite Pistol and Storm Shield"))
                        {

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Master-crafted Auto Bolt Rifle and Bolt Pistol",
                                "Master-crafted Stalker Bolt Rifle and Bolt Pistol",
                                "Master-crafted Power Sword, Neo-volkite Pistol and Storm Shield",
                            });
                            if (repo.currentSubFaction == "Dark Angels")
                            {
                                cmbOption1.Items.Add("Master-crafted Auto Bolt Rifle and Plasma Pistol");
                                cmbOption1.Items.Add("Master-crafted Stalker Bolt Rifle and Plasma Pistol");
                            }
                            if (repo.currentSubFaction == "Space Wolves")
                            {
                                cmbOption1.Items.Add("Special Issue Bolt Carbine, Master-crafted Power Axe and Bolt Pistol");
                            }
                        }
                    }
                    Relic = chosenRelic;
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
            }

            Points = DEFAULT_POINTS;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Primaris Lieutenant - " + Points + "pts";
        }
    }
}
