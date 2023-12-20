using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Repulsor : Datasheets
    {
        public Repulsor()
        {
            DEFAULT_POINTS = 220;
            UnitSize = 1;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "6m1k";
            Weapons.Add("Twin Heavy Bolter");
            Weapons.Add("Heavy Onslaught Gatling Cannon");
            Weapons.Add("Ironhail Heavy Stubber");
            Weapons.Add("Two Storm Bolters");
            Weapons.Add("Auto Launchers");
            Weapons.Add("Icarus Ironhail Heavy Stubber");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "TRANSPORT", "MACHINE SPIRIT", "REPULSOR FIELD", "REPULSOR"
            });
            role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Repulsor();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Twin Heavy Bolter",
                "Twin Lascannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Onslaught Gatling Cannon",
                "Las-talon"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Ironhail Heavy Stubber",
                "Onslaught Gatling Cannon"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "Two Fragstorm Grenade Launchers",
                "Two Storm Bolters"
            });
            cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);

            cmbOption5.Items.Clear();
            cmbOption5.Items.AddRange(new string[]
            {
                "Auto Launchers",
                "Two Fragstorm Grenade Launchers"
            });
            cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(Weapons[4]);

            cmbOption6.Items.Clear();
            cmbOption6.Items.AddRange(new string[]
            {
                "Fragstorm Grenade Launcher",
                "Icarus Ironhail Heavy Stubber",
                "Icarus Rocket Pod",
                "Storm Bolter"
            });
            cmbOption6.SelectedIndex = cmbOption6.Items.IndexOf(Weapons[5]);

            cbOption1.Text = "Ironhail Heavy Stubber";
            if (Weapons[6] != string.Empty)
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
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch(code)
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
                    else { Weapons[6] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Repulsor - " + Points + "pts";
        }
    }
}
