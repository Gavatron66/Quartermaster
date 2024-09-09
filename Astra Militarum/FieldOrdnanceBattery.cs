using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class FieldOrdnanceBattery : Datasheets
    {
        public FieldOrdnanceBattery()
        {
            DEFAULT_POINTS = 65;
            UnitSize = 2;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2m";
            Weapons.Add("Malleus Rocket Launcher");
            Weapons.Add("Malleus Rocket Launcher");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "ARTILLERY", "PLATOON", "REGIMENTAL", "FIELD ORDNANCE BATTERY"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new FieldOrdnanceBattery();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bombast Field Gun",
                "Heavy Lascannon",
                "Malleus Rocket Launcher"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Bombast Field Gun",
                "Heavy Lascannon",
                "Malleus Rocket Launcher"
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
        }

        public override string ToString()
        {
            return "Field Ordnance Battery - " + Points + "pts";
        }
    }
}
