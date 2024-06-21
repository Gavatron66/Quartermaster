using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class VoidravenBomber : Datasheets
    {
        public VoidravenBomber()
        {
            DEFAULT_POINTS = 175;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "1m1k";
            Weapons.Add("Two Void Lances");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<KABAL>", "<WYCH CULT>",
                "VEHICLE", "FLY", "AIRCRAFT", "VOIDRAVEN BOMBER"
            });
            Role = "Flyer";
        }

        public override Datasheets CreateUnit()
        {
            return new VoidravenBomber();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Two Dark Scythes (+10 pts)",
                "Two Void Lances"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Voidraven Missiles (+15 pts)";
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
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else { Weapons[0] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] != "Two Void Lances")
            {
                Points += 10;
            }

            if(cbOption1.Checked)
            {
                Points += 15;
            }
        }

        public override string ToString()
        {
            return "Voidraven Bomber - " + Points + "pts";
        }
    }
}
