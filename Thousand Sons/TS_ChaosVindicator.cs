using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class TS_ChaosVindicator : Datasheets
    {
        public TS_ChaosVindicator()
        {
            DEFAULT_POINTS = 120;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1m2k";
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "TZEENTCH", "HERETIC ASTARTES", "ARCANA ASTARTES", "THOUSAND SONS", "<GREAT CULT>",
                "VEHICLE", "SMOKESCREEN", "CHAOS VINDICATOR"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new TS_ChaosVindicator();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ThousandSons;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Inferno Combi-bolter (+5 pts)",
                "Inferno Combi-flamer (+10 pts)",
                "Inferno Combi-melta (+10 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Havoc Launcher (+5 pts)";
            if (Weapons[1] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Vindicator Siege Shield (+10 pts)";
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

            if (Weapons[0] == "Inferno Combi-bolter (+5 pts)")
            {
                Points += 5;
            }
            else if (Weapons[0] == "Inferno Combi-flamer (+10 pts)" || Weapons[0] == "Inferno Combi-melta (+10 pts)")
            {
                Points += 10;
            }

            if (Weapons[1] != "")
            {
                Points += 5;
            }

            if (Weapons[2] != "")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Chaos Vindicator - " + Points + "pts";
        }
    }
}
