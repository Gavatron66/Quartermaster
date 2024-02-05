using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class ChaosRhino : Datasheets
    {
        public ChaosRhino()
        {
            DEFAULT_POINTS = 80;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1m1k";
            Weapons.Add("(None)");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "VEHICLE", "BUBONIC ASTARTES", "TRANSPORT", "SMOKESCREEN", "CHAOS RHINO"
            });
            Role = "Transport";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[] {
                "(None)",
                "Combi-bolter",
                "Combi-flamer",
                "Combi-melta",
                "Combi-plasma"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Havoc Launcher";
            if (Weapons[1] != string.Empty)
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
            switch (code)
            {
                case 11:
                    ComboBox cmb = panel.Controls["cmbOption1"] as ComboBox;
                    Weapons[0] = cmb.SelectedItem.ToString();
                    break;
                case 21:
                    CheckBox cb = panel.Controls["cbOption1"] as CheckBox;
                    if (cb.Checked)
                    {
                        Weapons[1] = cb.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Chaos Rhino - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new ChaosRhino();
        }
    }
}
