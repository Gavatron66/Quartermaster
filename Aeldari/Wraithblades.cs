using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Wraithblades : Datasheets
    {
        public Wraithblades()
        {
            DEFAULT_POINTS = 40;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1m";
            Weapons.Add("Ghostswords");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "SPIRIT HOST", "<CRAFTWORLD>",
                "INFANTRY", "CORE", "WRAITH CONSTRUCT", "WRAITHBLADES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Wraithblades();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Ghostaxes and Forceshields (+5 pts)",
                "Ghostswords"
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

            if (Weapons[0] == "Ghostaxes and Forceshields (+5 pts)")
            {
                Points += 5 * UnitSize;
            }
        }

        public override string ToString()
        {
            return "Wraithblades - " + Points + "pts";
        }
    }
}