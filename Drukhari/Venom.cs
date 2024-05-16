using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Venom : Datasheets
    {
        public Venom()
        {
            DEFAULT_POINTS = 65;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2m";
            Weapons.Add("Twin Splinter Rifle");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<KABAL>", "<WYCH CULT>", "<HAEMONCULUS COVEN>",
                "VEHICLE", "TRANSPORT", "FLY", "VENOM"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new Venom();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Splinter Cannon (+10 pts)",
                "Twin Splinter Rifle"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Chain-snares (+5 pts)",
                "Grisly Trophies (+5 pts)"
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

            if (Weapons[0] != "Twin Splinter Rifle")
            {
                Points += 10;
            }

            if (Weapons[1] != "(None)")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Venom - " + Points + "pts";
        }
    }
}
