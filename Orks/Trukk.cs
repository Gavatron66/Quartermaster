using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Trukk : Datasheets
    {
        public Trukk()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "VEHICLE", "TRANSPORT", "TRUKK"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new Trukk();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Grabbin' Klaw (+5 pts)",
                "Wreckin' Ball (+5 pts)"
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

            if (!Weapons.Contains("(None)"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Trukk - " + Points + "pts";
        }
    }
}
