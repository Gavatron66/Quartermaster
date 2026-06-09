using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class WE_Defiler : Datasheets
    {
        public WE_Defiler()
        {
            DEFAULT_POINTS = 180;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "3m";
            Weapons.Add("Twin Heavy Flamer (+10 pts)");
            Weapons.Add("Reaper Autocannon");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "WORLD EATERS",
                "VEHICLE", "DAEMON", "DAEMON ENGINE", "SMOKESCREEN", "DEFILER"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new WE_Defiler();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as WorldEaters;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Defiler Scourge (+5 pts)",
                "Havoc Launcher",
                "Twin Heavy Flamer (+10 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Reaper Autocannon",
                "Twin Heavy Bolter (+10 pts)",
                "Twin Lascannon (+20 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Combi-bolter (+5 pts)",
                "Combi-flamer (+10 pts)",
                "Combi-melta (+10 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0] == "Defiler Scourge (+5 pts)")
            {
                Points += 5;
            }
            else if (Weapons[0] == "Twin Heavy Flamer (+10 pts)")
            {
                Points += 10;
            }

            if (Weapons[1] == "Twin Heavy Bolter (+10 pts)")
            {
                Points += 10;
            }
            else if (Weapons[1] == "Twin Lascannon (+20 pts)")
            {
                Points += 20;
            }

            if (Weapons[2] == "Combi-bolter (+5 pts)")
            {
                Points += 5;
            }
            else if (Weapons[2] != "(None)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Defiler - " + Points + "pts";
        }
    }
}
