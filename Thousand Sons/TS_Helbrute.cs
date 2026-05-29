using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class TS_Helbrute : Datasheets
    {
        public TS_Helbrute()
        {
            DEFAULT_POINTS = 105;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Missile Launcher");
            Weapons.Add("Twin Heavy Bolter");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVDED", "<LEGION>",
                "VEHICLE", "CORE", "HELBRUTE"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new TS_Helbrute();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ThousandSons;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Helbrute Fist",
                "Helbrute Fist w/ Heavy Flamer",
                "Helbrute Fist w/ Inferno Combi-bolter",
                "Helbrute Hammer",
                "Missile Launcher",
                "Power Scourge"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Helbrute Fist",
                "Helbrute Fist w/ Heavy Flamer",
                "Helbrute Fist w/ Inferno Combi-bolter",
                "Helbrute Plasma Cannon",
                "Multi-melta",
                "Reaper Autocannon",
                "Twin Heavy Bolter",
                "Twin Lascannon (+10 pts)"
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

            if (Weapons[1] == "Twin Lascannon (+10 pts)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Helbrute - " + Points + "pts";
        }
    }
}
