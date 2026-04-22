using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Wraithlord : Datasheets
    {
        public Wraithlord()
        {
            DEFAULT_POINTS = 100;
            Points = DEFAULT_POINTS;
            TemplateCode = "4m1k";
            Weapons.Add("Shuriken Catapult");
            Weapons.Add("Shuriken Catapult");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "SPIRIT HOST", "<CRAFTWORLD>",
                "MONSTER", "CORE", "WRAITH CONSTRUCT", "WRAITHLORD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Wraithlord();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Aeldari Flamer (+5 pts)",
                "Shuriken Catapult"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Aeldari Flamer (+5 pts)",
                "Shuriken Catapult"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Aeldari Missile Launcher (+15 pts)",
                "Bright Lance (+20 pts)",
                "Scatter Laser (+5 pts)",
                "Shuriken Cannon (+10 pts)",
                "Starcannon (+15 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "(None)",
                "Aeldari Missile Launcher (+15 pts)",
                "Bright Lance (+20 pts)",
                "Scatter Laser (+5 pts)",
                "Shuriken Cannon (+10 pts)",
                "Starcannon (+15 pts)"
            });
            cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);

            cbOption1.Text = "Ghostglaive (+15 pts)";
            if (Weapons[4] == cbOption1.Text)
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
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
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
                case 14:
                    Weapons[3] = cmbOption4.SelectedItem.ToString();
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[4] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[4] = "";
                    }
                    break;
            }

            Points = DEFAULT_POINTS;

            if(Weapons.Contains("Aeldari Flamer (+5 pts)") || Weapons.Contains("Scatter Laser (+5 pts)"))
            {
                Points += 5;
            }

            if(Weapons.Contains("Shuriken Cannon (+10 pts)"))
            {
                Points += 10;
            }

            if(Weapons.Contains("Aeldari Missile Launcher (+15 pts)") || Weapons.Contains("Ghostglaive (+15 pts)") 
                || Weapons.Contains("Starcannon (+15 pts)"))
            {
                Points += 15;
            }

            if(Weapons.Contains("Bright Lance (+20 pts)"))
            {
                Points += 20;
            }
        }

        public override string ToString()
        {
            return "Wraithlord - " + Points + "pts";
        }
    }
}