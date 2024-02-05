using Roster_Builder.Death_Guard;
using Roster_Builder.Necrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class BladeguardVeterans : Datasheets
    {
        public BladeguardVeterans()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NS(1m)";
            Weapons.Add("Heavy Bolt Pistol");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "BLADEGUARD", "BLADEGUARD VETERAN SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new BladeguardVeterans();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            gbUnitLeader.Text = "Bladeguard Veteran Sergeant";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Bolt Pistol",
                "Neo-volkite Pistol",
                "Plasma Pistol"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;

            switch (code)
            {
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
                case 411:
                    if (gb_cmbOption1.SelectedIndex != -1)
                    {
                        Weapons[0] = gb_cmbOption1.SelectedItem.ToString();
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Bladeguard Veteran Squad - " + Points + "pts";
        }
    }
}