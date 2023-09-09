using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class PrimarisCaptain : Datasheets
    {
        public PrimarisCaptain()
        {
            DEFAULT_POINTS = 90;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m1k_c";
            Weapons.Add("Master-crafted Auto Bolt Rifle");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CHARACTER", "PRIMARIS", "CAPTAIN"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new PrimarisCaptain();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
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
                "Heavy Bolt Pistol, Master-crafted Power Sword and Relic Shield",
                "Master-crafted Auto Bolt Rifle",
                "Master-crafted Stalker Bolt Rifle",
                "Plasma Pistol and Power Fist",
            });
            if(f.currentSubFaction == "Dark Angels")
            {
                cmbOption1.Items.Add("Special Issue Bolt Carbine");
            }
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            if(f.currentSubFaction == "Dark Angels")
            {
                cbOption1.Text = "Power Fist";
            }
            else
            {
                cbOption1.Text = "Master-crafted Power Sword";
            }

            if (Weapons[1] == "Master-crafted Power Sword")
            {
                cbOption1.Checked = true;
            }
            else if (Weapons[1] == "Power Fist")
            {
                cbOption1.Text = "Power Fist";
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
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

            panel.Controls["lblFactionupgrade"].Visible = true;
            cmbFactionupgrade.Visible = true;
            cmbFactionupgrade.Items.Clear();
            cmbFactionupgrade.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();

                    if (Weapons[0] == "Plasma Pistol and Power Fist" || Weapons[0] == "Heavy Bolt Pistol, Master-crafted Power Sword and Relic Shield")
                    {
                        cbOption1.Checked = false;
                        cbOption1.Enabled = false;
                    }
                    else
                    {
                        cbOption1.Enabled = true;
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
                    Relic = cmbRelic.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; }
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

            if (Weapons[0] == "Heavy Bolt Pistol, Master-crafted Power Sword and Relic Shield" || Weapons[1] == "Power Fist")
            {
                Points += 10;
            }

            if (Weapons[1] == "Master-crafted Power Sword")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Primaris Captain - " + Points + "pts";
        }
    }
}
