using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class BrôkhyrThunderkyn : Datasheets
    {
        public BrôkhyrThunderkyn()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1m";
            Weapons.Add("Bolt Cannons");
            Keywords.AddRange(new string[]
            {
                "VOTANN", "<LEAGUE>",
                "INFANTRY", "CORE", "EXO-FRAME", "BRÔKHYR", "THUNDERKYN"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new BrôkhyrThunderkyn();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as LeaguesOfVotann;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bolt Cannons",
                "Graviton Blast Cannons",
                "SP Conversion Beamers (+5 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] == "SP Conversion Beamers (+5 pts)")
            {
                Points += 5 * UnitSize;
            }
        }

        public override string ToString()
        {
            return "Brôkhyr Thunderkyn - " + Points + "pts";
        }
    }
}
