using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class BrutalisDreadnought : Datasheets
    {
        public BrutalisDreadnought()
        {
            DEFAULT_POINTS = 170;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Two Heavy Bolters");
            Weapons.Add("Two Brutalis Fists w/ Twin Bolt Rifles");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "CORE", "DREADNOUGHT", "BRUTALIS DREADNOUGHT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new BrutalisDreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Two Heavy Bolters",
                "Two Multi-meltas"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Two Brutalis Fists w/ Twin Bolt Rifles",
                "Two Brutalis Talons"
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

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Brutalis Dreadnought - " + Points + "pts";
        }
    }
}
