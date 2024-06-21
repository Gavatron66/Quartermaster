using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Ravager : Datasheets
    {
        public Ravager()
        {
            DEFAULT_POINTS = 130;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2m";
            Weapons.Add("Dark Lances");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<KABAL>",
                "VEHICLE", "FLY", "RAVAGER"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Ravager();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Dark Lances",
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
                "Shock Prow (+5 pts)"
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

            if (Weapons[0] != "Dark Lances")
            {
                Points += 5;
            }

            if (Weapons[1] != "(None)")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Ravager - " + Points + "pts";
        }
    }
}
