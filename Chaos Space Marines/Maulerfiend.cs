using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Maulerfiend : Datasheets
    {
        public Maulerfiend()
        {
            DEFAULT_POINTS = 140;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1m";
            Weapons.Add("Lasher Tendrils (+10 pts)");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "VEHICLE", "DAEMON", "DAEMON ENGINE", "MAULERFIEND"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Maulerfiend();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Lasher Tendrils (+10 pts)",
                "Two Magma Cutters"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0] == "Lasher Tendrils (+10 pts)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Maulerfiend - " + Points + "pts";
        }
    }
}
