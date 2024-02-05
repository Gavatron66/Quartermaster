using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class SkorpiusDisintegrator : Datasheets
    {
        public SkorpiusDisintegrator()
        {
            DEFAULT_POINTS = 125;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("Belleros Energy Cannon");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "VEHICLE", "DATA-TETHER", "SKORPIUS ENGINE", "SKORPIUS DISINTEGRATOR"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new SkorpiusDisintegrator();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Belleros Energy Cannon",
                "Ferrumite Cannon"
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
        }

        public override string ToString()
        {
            return "Skorpius Disintegrator - " + Points + "pts";
        }
    }
}
