using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class FoetidBloatDrone : Datasheets
    {
        public FoetidBloatDrone()
        {
            DEFAULT_POINTS = 115;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1m";
            Weapons.Add("Fleshmower");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "VEHICLE", "FLY", "DAEMON", "DAEMON ENGINE", "FOETID BLOAT-DRONE"
            });
            Role = "Fast Attack";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Fleshmower",
                "Two Plaguespitters",
                "Heavy Blight Launcher"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmb1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmb1.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Foetid Bloat-drone - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new FoetidBloatDrone();
        }
    }
}
