using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DG_Defiler : Datasheets
    {
        public DG_Defiler()
        {
            DEFAULT_POINTS = 175;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m";
            Weapons.Add("Twin Heavy Flamer");
            Weapons.Add("Reaper Autocannon");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "VEHICLE", "DAEMON", "DAEMON ENGINE", "SMOKESCREEN", "DEFILER"
            });
            Role = "Heavy Support";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmb1 = panel.Controls["cmbOption1"] as ComboBox;
            cmb1.Items.Clear();
            cmb1.Items.AddRange(new string[] {
                "Defiler Scourge",
                "Havoc Launcher",
                "Twin Heavy Flamer"
            });
            cmb1.SelectedIndex = cmb1.Items.IndexOf(Weapons[0]);

            ComboBox cmb2 = panel.Controls["cmbOption2"] as ComboBox;
            cmb2.Items.Clear();
            cmb2.Items.AddRange(new string[] {
                "Reaper Autocannon",
                "Twin Heavy Bolter",
                "Twin Lascannon"
            });
            cmb2.SelectedIndex = cmb2.Items.IndexOf(Weapons[1]);

            ComboBox cmb3 = panel.Controls["cmbOption3"] as ComboBox;
            cmb3.Items.Clear();
            cmb3.Items.AddRange(new string[] {
                "(None)",
                "Combi-bolter",
                "Combi-flamer",
                "Combi-melta",
                "Combi-plasma"
            });
            cmb3.SelectedIndex = cmb3.Items.IndexOf(Weapons[2]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            switch (code)
            {
                case 11:
                    ComboBox cmb = panel.Controls["cmbOption1"] as ComboBox;
                    Weapons[0] = cmb.SelectedItem.ToString();
                    break;
                case 12:
                    ComboBox cmb2 = panel.Controls["cmbOption2"] as ComboBox;
                    Weapons[1] = cmb2.SelectedItem.ToString();
                    break;
                case 13:
                    ComboBox cmb3 = panel.Controls["cmbOption3"] as ComboBox;
                    Weapons[2] = cmb3.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Defiler - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new DG_Defiler();
        }
    }
}
