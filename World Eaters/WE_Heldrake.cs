using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class WE_Heldrake : Datasheets
    {
        public WE_Heldrake()
        {
            DEFAULT_POINTS = 165;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1m";
            Weapons.Add("Hades Autocannon");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "WORLD EATERS",
                "VEHICLE", "AIRCRAFT", "DAEMON", "DAEMON ENGINE", "FLY", "HELDRAKE"
            });
            Role = "Flyer";
        }

        public override Datasheets CreateUnit()
        {
            return new WE_Heldrake();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as WorldEaters;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Baleflamer",
                "Hades Autocannon"
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
        }

        public override string ToString()
        {
            return "Heldrake - " + Points + "pts";
        }
    }
}
