using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class HeavyWeaponsSquad : Datasheets
    {

        public HeavyWeaponsSquad()
        {
            DEFAULT_POINTS = 55;
            UnitSize = 3;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m";
            Weapons.Add("Heavy Bolter");
            Weapons.Add("Heavy Bolter");
            Weapons.Add("Heavy Bolter");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "HEAVY WEAPONS SQUAD"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new HeavyWeaponsSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Autocannon",
                "Heavy Bolter",
                "Lascannon",
                "Missile Launcher",
                "Mortar"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Autocannon",
                "Heavy Bolter",
                "Lascannon",
                "Missile Launcher",
                "Mortar"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Autocannon",
                "Heavy Bolter",
                "Lascannon",
                "Missile Launcher",
                "Mortar"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption1.SelectedItem.ToString();
                    break;
                case 13:
                    Weapons[2] = cmbOption1.SelectedItem.ToString();
                    break;
                    
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Heavy Weapons Squad - " + Points + "pts";
        }
    }
}
