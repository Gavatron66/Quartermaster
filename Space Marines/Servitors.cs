using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Servitors : Datasheets
    {
        public Servitors()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 4;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2m";
            Weapons.Add("Servo-arm");
            Weapons.Add("Servo-arm");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "SERVITORS"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new Servitors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Bolter",
                "Multi-melta",
                "Plasma Cannon",
                "Servo-arm"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Bolter",
                "Multi-melta",
                "Plasma Cannon",
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
                if(weapon == "Heavy Bolter")
                {
                    Points += 5;
                }
                else if (weapon == "Plasma Cannon")
                {
                    Points += 10;
                }
                else if(weapon == "Multi-melta")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Servitors - " + Points + "pts";
        }
    }
}
