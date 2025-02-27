using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Forgefiend : Datasheets
    {
        public Forgefiend()
        {
            DEFAULT_POINTS = 110;
            Points = DEFAULT_POINTS + 30;
            UnitSize = 1;
            TemplateCode = "2m";
            Weapons.Add("Two Heavy Hades Autocannons (+50 pts)");
            Weapons.Add("Forgefiend Jaws");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "VEHICLE", "DAEMON", "DAEMON ENGINE", "FORGEFIEND"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Forgefiend();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Two Ectoplasma Cannons (+30 pts)",
                "Two Heavy Hades Autocannons (+50 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Ectoplasma Cannon (+15 pts)",
                "Forgefiend Jaws"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0] == "Two Ectoplasma Cannons (+30 pts)")
            {
                Points += 30;
            }
            else if (Weapons[0] == "Two Heavy Hades Autocannons (+50 pts)")
            {
                Points += 50;
            }

            if (Weapons[1] == "Ectoplasma Cannon (+15 pts)")
            {
                Points += 15;
            }
        }

        public override string ToString()
        {
            return "Forgefiend - " + Points + "pts";
        }
    }
}
