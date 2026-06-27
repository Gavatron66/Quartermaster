using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Blood_Angels
{
    public class DeathCompanyDreadnought : Datasheets
    {
        public DeathCompanyDreadnought()
        {
            DEFAULT_POINTS = 125;
            Points = DEFAULT_POINTS;
            TemplateCode = "4m";
            Weapons.Add("Two Furioso Fists");
            Weapons.Add("Storm Bolter");
            Weapons.Add("Meltagun");
            Weapons.Add("Smoke Launchers");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "BLOOD ANGELS",
                "VEHICLE", "DREADNOUGHT", "DEATH COMPANY", "DEATH COMPANY DREADNOUGHT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathCompanyDreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Two Blood Talons",
                "Two Furioso Fists"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Flamer",
                "Storm Bolter"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Heavy Flamer",
                "Meltagun"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "Magna-grapple",
                "Smoke Launchers"
            });
            cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;

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
                case 14:
                    Weapons[3] = cmbOption4.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Death Company Dreadnought - " + Points + " pts";
        }
    }
}
