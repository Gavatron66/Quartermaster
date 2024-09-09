using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Chimera : Datasheets
    {
        public Chimera()
        {
            DEFAULT_POINTS = 85;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m3k";
            Weapons.Add("Heavy Bolter");
            Weapons.Add("Multi-laser");
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "VEHICLE", "TRANSPORT", "SMOKE", "REGIMENTAL", "SQUADRON", "CHIMERA"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new Chimera();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Bolter",
                "Heavy Flamer"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Bolter",
                "Heavy Flamer",
                "Multi-laser"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Heavy Stubber (+5 pts)",
                "Storm Bolter (+5 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cbOption1.Text = "Armoured Tracks (+5 pts)";
            if (Weapons[3] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Dozer Blade (+5 pts)";
            if (Weapons[4] == cbOption2.Text)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Hunter-killer Missile (+5 pts)";
            if (Weapons[5] == cbOption3.Text)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;

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
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[4] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[4] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[5] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[5] = "";
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            if(cbOption1.Checked)
            {
                Points += 5;
            }

            if (cbOption2.Checked)
            {
                Points += 5;
            }

            if (cbOption3.Checked)
            {
                Points += 5;
            }

            if (Weapons[2] != "(None)")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Chimera - " + Points + "pts";
        }
    }
}