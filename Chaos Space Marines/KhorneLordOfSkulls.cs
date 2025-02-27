using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class KhorneLordOfSkulls : Datasheets
    {
        public KhorneLordOfSkulls()
        {
            DEFAULT_POINTS = 575;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "2m";
            Weapons.Add("Gorestorm Cannon");
            Weapons.Add("Hades Gatling Cannon");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "VEHICLE", "TITANIC", "DAEMON", "DAEMON ENGINE", "WARP LOCUS", "LORD OF SKULLS"
            });
            Role = "Lord of War";
        }

        public override Datasheets CreateUnit()
        {
            return new KhorneLordOfSkulls();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Daemongore Cannon",
                "Gorestorm Cannon",
                "Ichor Cannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Hades Gatling Cannon",
                "Skullhurler"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Khorne Lord of Skulls - " + Points + "pts";
        }
    }
}
