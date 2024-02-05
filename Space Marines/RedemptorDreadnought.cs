using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class RedemptorDreadnought : Datasheets
    {
        public RedemptorDreadnought()
        {
            DEFAULT_POINTS = 185;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m1k";
            Weapons.Add("Heavy Onslaught Gatling Cannon");
            Weapons.Add("Heavy Flamer");
            Weapons.Add("Fragstorm Grenade Launchers");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "CORE", "DREADNOUGHT", "REDEMPTOR DREADNOUGHT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new RedemptorDreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Onslaught Gatling Cannon",
                "Macro Plasma Incinerator"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Flamer",
                "Onslaught Gatling Cannon"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Fragstorm Grenade Launchers",
                "Storm Bolters"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cbOption1.Text = "Icarus Rocket Pod";
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
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem as string;
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
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Redemptor Dreadnought - " + Points + "pts";
        }
    }
}
