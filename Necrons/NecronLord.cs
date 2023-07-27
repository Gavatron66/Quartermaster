using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class NecronLord : Datasheets
    {
        Necrons repo = new Necrons();
        public NecronLord()
        {
            DEFAULT_POINTS = 70;
            TemplateCode = "1m1k_c";
            Points = DEFAULT_POINTS;
            Weapons.Add("Staff of Light");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "INFANTRY", "CHARACTER", "NOBLE", "LORD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new NecronLord();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Hyperphase Sword",
                "Staff of Light",
                "Voidblade",
                "Warscythe"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Resurrection Orb";
            if (Weapons[1] == "Resurrection Orb")
            {
                cbOption1.Checked = true;
            } else
            {
                cbOption1.Checked = false;
            }

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
                cmbWarlord.SelectedText = WarlordTrait;
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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

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
                    Relic = cmbRelic.SelectedItem.ToString();

                    if (cmbRelic.SelectedItem.ToString() == "Blood Scythe")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Warscythe");
                        cmbOption1.Enabled = false;
                    } else
                    {
                        cmbOption1.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Solar Staff")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Staff of Light");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Orb of Eternity")
                    {
                        cbOption1.Checked = true;
                        cbOption1.Enabled = false;
                    }
                    else
                    {
                        cbOption1.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Voidreaper")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Warscythe");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Voltaic Staff")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Staff of Light");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }
                    break;
                case 21:
                    if(cbOption1.Checked)
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
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Warscythe"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Resurrection Orb"))
            {
                Points += 30;
            }
        }

        public override string ToString()
        {
            return "Lord - " + Points + "pts";
        }
    }
}
