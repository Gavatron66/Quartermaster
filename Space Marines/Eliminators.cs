using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Eliminators : Datasheets
    {
        public Eliminators()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2m";
            Weapons.Add("Bolt Sniper Rifle");
            Weapons.Add("Bolt Sniper Rifle");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "PHOBOS", "ELIMINATOR SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new Eliminators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bolt Sniper Rifle",
                "Las Fusil"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Bolt Sniper Rifle",
                "Instigator Bolt Carbine",
                "Las Fusil"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            panel.Controls["lblExtra1"].Text = "(Eliminator Sergeant)";
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(307, 91);
            panel.Controls["lblExtra1"].Visible = true;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch(code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] == "Las Fusil")
            {
                Points += 20;
            }
            if (Weapons[1] == "Las Fusil")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Eliminator Squad - " + Points + "pts";
        }
    }
}
