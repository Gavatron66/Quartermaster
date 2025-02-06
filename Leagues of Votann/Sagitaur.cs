using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class Sagitaur : Datasheets
    {
        public Sagitaur()
        {
            DEFAULT_POINTS = 130;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("HYLas Beam Cannon (+20 pts)");
            Keywords.AddRange(new string[]
            {
                "VOTANN", "<LEAGUE>",
                "TRANSPORT", "VEHICLE", "ACCELERATED", "SAGITAUR"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Sagitaur();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as LeaguesOfVotann;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "HYLas Beam Cannon (+20 pts)",
                "L7 and Sagitaur Missile Launcher (+10 pts)",
                "MATR Autocannon"
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

            if (Weapons[0] == "HYLas Beam Cannon (+20 pts)")
            {
                Points += 20;
            }
            else if (Weapons[0] == "L7 and Sagitaur Missile Launcher (+10 pts)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Sagitaur - " + Points + "pts";
        }
    }
}
