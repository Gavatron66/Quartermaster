using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class WE_Helbrute : Datasheets
    {
        public WE_Helbrute()
        {
            DEFAULT_POINTS = 105;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Missile Launcher");
            Weapons.Add("Twin Heavy Bolter (+10 pts)");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "BUTCHER ASTARTES", "WORLD EATERS",
                "VEHICLE", "CORE", "HELBRUTE"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new WE_Helbrute();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as WorldEaters;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Helbrute Fist",
                "Helbrute Fist w/ Combi-bolter (+5 pts)",
                "Helbrute Fist w/ Heavy Flamer (+10 pts)",
                "Helbrute Hammer",
                "Missile Launcher",
                "Power Scourge"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Helbrute Fist",
                "Helbrute Fist w/ Combi-bolter (+5 pts)",
                "Helbrute Fist w/ Heavy Flamer (+10 pts)",
                "Helbrute Plasma Cannon",
                "Multi-melta (+5 pts)",
                "Reaper Autocannon",
                "Twin Heavy Bolter (+10 pts)",
                "Twin Lascannon (+20 pts)"
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

            Points = DEFAULT_POINTS;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[0] == "Helbrute Fist w/ Combi-bolter (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[0] == "Helbrute Fist w/ Heavy Flamer (+10 pts)")
            {
                Points += 10;
            }

            if (Weapons[1] == "Helbrute Fist w/ Combi-bolter (+5 pts)" || Weapons[1] == "Multi-melta (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[1] == "Helbrute Fist w/ Heavy Flamer (+10 pts)" || Weapons[1] == "Twin Heavy Bolter (+10 pts)")
            {
                Points += 10;
            }

            if (Weapons[1] == "Twin Lascannon (+20 pts)")
            {
                Points += 20;
            }
        }

        public override string ToString()
        {
            return "Helbrute - " + Points + "pts";
        }
    }
}
