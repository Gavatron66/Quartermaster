using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class TriarchStalker : Datasheets
    {
        public TriarchStalker()
        {
            DEFAULT_POINTS = 110;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("Heat Ray (+5 pts)");
            Keywords.AddRange(new string[]
            {
                "NECRONS",
                "VEHICLE", "DYNASTIC AGENT", "TRIARCH", "TRIARCH STALKER"
            });
            role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new TriarchStalker();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heat Ray (+5 pts)",
                "Particle Shredder",
                "Twin Heavy Gauss Cannon (+15 pts)"
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

            if (Weapons.Contains("Heat Ray (+5 pts)"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Twin Heavy Gauss Cannon (+15 pts)"))
            {
                Points += 15;
            }
        }

        public override string ToString()
        {
            return "Triarch Stalker - " + Points + "pts";
        }
    }
}
