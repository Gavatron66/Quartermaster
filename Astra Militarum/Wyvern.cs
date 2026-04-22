using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Wyvern : Datasheets
    {
        public Wyvern()
        {
            DEFAULT_POINTS = 120;
            UnitSize = 1;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "1m3k";
            Weapons.Add("Heavy Bolter");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "VEHICLE", "ARTILLERY", "SMOKE", "REGIMENTAL", "SQUADRON", "WYVERN"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Manticore();
        }
        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
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

            cbOption1.Text = "Armoured Tracks (+5 pts)";
            if (Weapons[1] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Dozer Blade (+5 pts)";
            if (Weapons[2] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Hunter-killer Missile (+5 pts)";
            if (Weapons[3] != string.Empty)
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[3] = cbOption2.Text;
                    }
                    else { Weapons[3] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            if (cbOption1.Checked)
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
        }

        public override string ToString()
        {
            return "Wyvern - " + Points + "pts";
        }
    }
}
