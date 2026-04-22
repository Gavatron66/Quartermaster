using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class MunitorumServitors : Datasheets
    {
        public MunitorumServitors()
        {
            DEFAULT_POINTS = 40;
            UnitSize = 4;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2m";
            Weapons.Add("Servo-arm");
            Weapons.Add("Servo-arm");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "MILITARUM AUXILLA", "SERVITORS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new MunitorumServitors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Bolter (+5 pts)",
                "Multi-melta (+15 pts)",
                "Plasma Cannon (+10 pts)",
                "Servo-arm"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Bolter (+5 pts)",
                "Multi-melta (+15 pts)",
                "Plasma Cannon (+10 pts)",
                "Servo-arm"
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

            foreach (string weapon in Weapons)
            {
                if (weapon == "Heavy Bolter (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Plasma Cannon (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Multi-melta (+15 pts)")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Munitorum Servitors - " + Points + "pts";
        }
    }
}
