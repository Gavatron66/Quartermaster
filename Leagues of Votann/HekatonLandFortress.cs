using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class HekatonLandFortress : Datasheets
    {
        public HekatonLandFortress()
        {
            DEFAULT_POINTS = 310;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m";
            Weapons.Add("Four Bolt Cannons");
            Weapons.Add("Cyclic Ion Cannon");
            Weapons.Add("Pan Spectral Scanner");
            Keywords.AddRange(new string[]
            {
                "VOTANN", "<LEAGUE>",
                "VEHICLE", "ACCELERATED", "TRANSPORT", "HEKATON LAND FORTRESS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new HekatonLandFortress();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as LeaguesOfVotann;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Four Bolt Cannons",
                "Two Bolt Cannons and Two Ion Beamers (+10 pts)",
                "Four Ion Beamers (+20 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Cyclic Ion Cannon",
                "Heavy Magna-rail Cannon",
                "SP Heavy Conversion Beamer"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Ancestor's Vengeance Warhead (+10 pts)",
                "Kin's Wrath Warhead (+10 pts)",
                "Mountain Breaker Warhead (+10 pts)",
                "Pan Spectral Scanner"
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
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0].Contains("+10 pts"))
            {
                Points += 10;
            }
            else if (Weapons[0].Contains("+20 pts"))
            {
                Points += 20;
            }

            if (Weapons[2].Contains("+10 pts"))
            {
                Points += 10;
            }

        }

        public override string ToString()
        {
            return "Hekaton Land Fortress - " + Points + "pts";
        }
    }
}
