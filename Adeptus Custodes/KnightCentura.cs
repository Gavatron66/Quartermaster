using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Custodes
{
    public class KnightCentura : Datasheets
    {
        public KnightCentura()
        {
            DEFAULT_POINTS = 50;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m_c";
            Weapons.Add("Executioner Greatblade");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ANATHEM PSYKANA",
                "INFANTRY", "CHARACTER", "KNIGHT-CENTURA"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new KnightCentura();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Executioner Greatblade",
                "Master-crafted Boltgun",
                "Witchseeker Flamer"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbWarlord.Items.Clear();
            cmbWarlord.Items.AddRange(new string[]
            {
                "Oblivion Knight",
                "Silent Judge",
                "Mistrss of Persecution"
            });

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = false;
                cmbWarlord.Text = WarlordTrait;
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(new string[]
            {
                "Raptor Blade",
                "Excruciatus Flamer",
                "Enhanced Voidsheen Cloak"
            });

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

                    if (cmbRelic.SelectedItem.ToString() == "Excruciatus Flamer"
                        || cmbRelic.SelectedItem.ToString() == "Raptor Blade")
                    {
                        if(cmbRelic.SelectedItem.ToString() == "Excruciatus Flamer")
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Witchseeker Flamer");
                        }

                        if (cmbRelic.SelectedItem.ToString() == "Raptor Blade")
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Executioner Greatblade");
                        }

                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
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

            if(Weapons.Contains("Executioner Greatblade"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Knight-Centura - " + Points + "pts";
        }
    }
}
