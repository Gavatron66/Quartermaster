using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class CatacombBarge : Datasheets
    {
        public CatacombBarge()
        {
            DEFAULT_POINTS = 145;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m1k_c";
            Weapons.Add("Gauss Cannon (+5 pts)");
            Weapons.Add("Staff of Light");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "VEHICLE", "CHARACTER", "QUANTUM SHIELDING", "NOBLE", "OVERLORD", "FLY",
                "CATACOMB COMMAND BARGE"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new CatacombBarge();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }


            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Gauss Cannon (+5 pts)",
                "Tesla Cannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Hyperphase Sword",
                "Staff of Light",
                "Voidblade",
                "Warscythe (+5 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Resurrection Orb (+30 pts)";
            if (Weapons[2] == "Resurrection Orb (+30 pts)")
            {
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
            //Gauss Cannon (+5 pts) +5
            //Resurrection Orb (+30 pts) +30
            //Warscythe (+5 pts) +5
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
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
                case 17:
                    Relic = cmbRelic.SelectedItem.ToString();

                    if (cmbRelic.SelectedItem.ToString() == "Blood Scythe")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Warscythe (+5 pts)");
                        cmbOption2.Enabled = false;
                    }
                    else if (cmbRelic.SelectedItem.ToString() == "Solar Staff")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Staff of Light");
                        cmbOption2.Enabled = false;
                    }
                    else if(cmbRelic.SelectedItem.ToString() == "Orb of Eternity")
                    {
                        cbOption1.Checked = true;
                        cbOption1.Enabled = false;
                    }
                    else if(cmbRelic.SelectedItem.ToString() == "Voidreaper")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Warscythe (+5 pts)");
                        cmbOption2.Enabled = false;
                    }
                    else if (cmbRelic.SelectedItem.ToString() == "Voltaic Staff")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Staff of Light");
                        cmbOption2.Enabled = false;
                    }
                    else
                    {
                        cmbOption2.Enabled = true;
                    }
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

            if (Weapons.Contains("Gauss Cannon (+5 pts)"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Warscythe (+5 pts)"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Resurrection Orb (+30 pts)"))
            {
                Points += 30;
            }
        }

        public override string ToString()
        {
            return "Catacomb Command Barge - " + Points + "pts";
        }
    }
}
