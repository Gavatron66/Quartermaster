using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class ChaosVindicator : Datasheets
    {
        public ChaosVindicator()
        {
            DEFAULT_POINTS = 130;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1m2k";
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "VEHICLE", "SMOKESCREEN", "CHAOS VINDICATOR"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new ChaosVindicator();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

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

            cbOption2.Text = "Vindicator Siege Shield";
            if (Weapons[2] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

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
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Chaos Vindicator - " + Points + "pts";
        }
    }
}
