using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Raider : Datasheets
    {
        public Raider()
        {
            DEFAULT_POINTS = 95;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2m";
            Weapons.Add("Dark Lance (+10 pts)");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<KABAL>", "<WYCH CULT>", "<HAEMONCULUS COVEN>",
                "VEHICLE", "TRANSPORT", "FLY", "RAIDER"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new Raider();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Dark Lance (+10 pts)",
                "Disintegrator Cannon (+5 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Chain-snares (+5 pts)",
                "Grisly Trophies (+5 pts)",
                "Phantasm Grenade Launcher (+5 pts)",
                "Shock Prow (+5 pts)",
                "Splinter Racks (+10 pt)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] != "Dark Lance (+10 pts)")
            {
                Points += 5;
            }
            else
            {
                Points += 10;
            }

            if (Weapons[1] != "(None)")
            {
                Points += 5;
            }

            if (Weapons[1] == "Splinter Racks (+10 pt)")
            {
                Points += 5; //The above statement adds 5 already
            }
        }

        public override string ToString()
        {
            return "Raider - " + Points + "pts";
        }
    }
}
