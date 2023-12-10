using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class StormtalonGunship : Datasheets
    {
        public StormtalonGunship()
        {
            DEFAULT_POINTS = 165;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("Skyhammer Missile Launcher");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "AIRCRAFT", "FLY", "STORMTALON GUNSHIP"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new StormtalonGunship();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Skyhammer Missile Launcher",
                "Two Heavy Bolters",
                "Two Lascannons",
                "Typhoon Missile Launcher"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Two Heavy Bolters"))
            {
                Points += 10;
            }

            if (Weapons.Contains("Typhoon Missile Launcher"))
            {
                Points += 20;
            }

            if (Weapons.Contains("Two Lascannons"))
            {
                Points += 20;
            }
        }

        public override string ToString()
        {
            return "Stormtalon Gunship - " + Points + "pts";
        }
    }
}
