using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Grey_Knights
{
    public class NemesisDreadknight : Datasheets
    {
        public NemesisDreadknight()
        {
            DEFAULT_POINTS = 130;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m1k";
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("Dreadfists");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "SANCTIC ASTARTES", "GREY KNIGHTS", "<BROTHERHOOD>",
                "VEHICLE", "CORE", "PSYKER", "NEMESIS DREADKNIGHT"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new NemesisDreadknight();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as GreyKnights;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Heavy Incinerator (+15 pts)",
                "Gatling Psilencer (+20 pts)",
                "Heavy Psycannon (+20 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Heavy Incinerator (+15 pts)",
                "Gatling Psilencer (+20 pts)",
                "Heavy Psycannon (+20 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Dreadfists",
                "Nemesis Daemon Greathammer (+10 pts)",
                "Nemesis Greatsword (+15 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cbOption1.Text = "Dreadknight Teleporter";
            if (Weapons[3] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

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
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[3] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (var weapon in Weapons)
            {
                if (weapon == "Gatling Psilencer (+20 pts)")
                {
                    Points += 20;
                }
                if (weapon == "Heavy Incinerator (+15 pts)")
                {
                    Points += 15;
                }
                if (weapon == "Heavy Psycannon (+20 pts)")
                {
                    Points += 20;
                }
                if (weapon == "Nemesis Daemon Greathammer (+10 pts)")
                {
                    Points += 10;
                }
                if (weapon == "Nemesis Greatsword (+15 pts)")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Nemesis Dreadknight - " + Points + "pts";
        }
    }
}