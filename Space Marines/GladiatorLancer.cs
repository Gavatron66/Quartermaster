using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class GladiatorLancer : Datasheets
    {
        public GladiatorLancer()
        {
            DEFAULT_POINTS = 125;
            UnitSize = 1;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "1m3k";
            Weapons.Add("Two Storm Bolters");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "GLADIATOR", "REPULSOR FIELD", "GLADIATOR LANCER"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new GladiatorLancer();
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
                "Two Fragstorm Grenade Launchers",
                "Two Storm Bolters"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Ironhail Heavy Stubber";
            if (Weapons[1] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Icarus Rocket Pod";
            if (Weapons[2] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Auto Launchers";
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
        }

        public override string ToString()
        {
            return "Gladiator Lancer - " + Points + "pts";
        }
    }
}
