using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class PlagueburstCrawler : Datasheets
    {
        public PlagueburstCrawler()
        {
            DEFAULT_POINTS = 135;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Two Entropy Cannons (+10 pts)");
            Weapons.Add("Heavy Slugger");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "VEHICLE", "DAEMON", "DAEMON ENGINE", "PLAGUEBURST CRAWLER"
            });
            Role = "Heavy Support";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmboption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Two Entropy Cannons (+10 pts)",
                "Two Plaguespitters"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[] {
                "Heavy Slugger",
                "Rothail Volley Gun"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            switch (code)
            {
                case 11:
                    ComboBox cmb1 = panel.Controls["cmbOption1"] as ComboBox;
                    Weapons[0] = cmb1.SelectedItem as string;
                    break;
                case 12:
                    ComboBox cmb2 = panel.Controls["cmbOption2"] as ComboBox;
                    Weapons[1] = cmb2.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Two Entropy Cannons (+10 pts)"))
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Plagueburst Crawler - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new PlagueburstCrawler();
        }
    }
}

