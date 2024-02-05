using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class DropPod : Datasheets
    {
        public DropPod()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("Storm Bolter");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "TRANSPORT", "DROP POD"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new DropPod();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Deathwind Launcher",
                "Storm Bolter"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
            }
        }

        public override string ToString()
        {
            return "Drop Pod - " + Points + "pts";
        }
    }
}
