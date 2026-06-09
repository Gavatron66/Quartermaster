using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class WE_ChaosPredatorAnnihilator : Datasheets
    {
        public WE_ChaosPredatorAnnihilator()
        {
            DEFAULT_POINTS = 130;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "2m1k";
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "BUTCHER ASTARTES", "WORLD EATERS",
                "VEHICLE", "SMOKESCREEN", "CHAOS PREDATOR ANNIHILATOR"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new WE_ChaosPredatorAnnihilator();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as WorldEaters;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Two Heavy Bolters (+20 pts)",
                "Two Lascannons (+40 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Combi-bolter (+5 pts)",
                "Combi-flamer (+10 pts)",
                "Combi-melta (+10 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Havoc Launcher (+5 pts)";
            if (Weapons[2] != string.Empty)
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
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0] == "Two Heavy Bolters (+20 pts)")
            {
                Points += 20;
            }
            else if (Weapons[0] == "Two Lascannons (+40 pts)")
            {
                Points += 40;
            }

            if (Weapons[1] == "Combi-bolter (+5 pts)")
            {
                Points += 5;
            }
            else if (Weapons[1] != "(None)")
            {
                Points += 10;
            }

            Points += cbOption1.Checked ? 5 : 0;
        }

        public override string ToString()
        {
            return "Chaos Predator Annihilator - " + Points + "pts";
        }
    }
}
