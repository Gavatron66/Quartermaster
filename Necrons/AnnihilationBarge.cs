using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class AnnihilationBarge : Datasheets
    {
        public AnnihilationBarge()
        {
            DEFAULT_POINTS = 120;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("Gauss Cannon (+5 pts)");
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "VEHICLE", "QUANTUM SHIELDING", "FLY", "ANNIHILATION BARGE"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new AnnihilationBarge();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Gauss Cannon (+5 pts)",
                "Tesla Cannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Gauss Cannon (+5 pts)"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Annihilation Barge - " + Points + "pts";
        }
    }
}
