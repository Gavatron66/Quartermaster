using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
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
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "VEHICLE", "TRANSPORT", "SMOKESCREEN", "CHAOS RHINO"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new ChaosRhino();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Combi-bolter",
                "Combi-flamer",
                "Combi-melta"
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
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

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
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Chaos Rhino - " + Points + "pts";
        }
    }
}
