using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class ArchaeopterTransvector : Datasheets
    {
        public ArchaeopterTransvector()
        {
            DEFAULT_POINTS = 110;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("Command Uplink");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "VEHICLE", "AIRCRAFT", "TRANSPORT", "FLY", "ARCHAEOPTER ENGINE", "ARCHAEOPTER TRANSVECTOR"
            });
            Role = "Flyer";
        }

        public override Datasheets CreateUnit()
        {
            return new ArchaeopterTransvector();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Chaff Launcher (+20 pts)",
                "Command Uplink"
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

            if(Weapons.Contains("Chaff Launcher (+20 pts)"))
            {
                Points += 20;
            }
        }

        public override string ToString()
        {
            return "Archaeopter Transvector - " + Points + "pts";
        }
    }
}
