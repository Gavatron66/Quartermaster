using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Custodes
{
    public class GoliathTruck : Datasheets
    {
        public GoliathTruck()
        {
            DEFAULT_POINTS = 90;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1k";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "TYRANIDS", "GENESTEALER CULTS", "<CULT>",
                "VEHICLE", "TRANSPORT", "CROSSFIRE", "GOLIATH TRUCK"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new GoliathTruck();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cbOption1.Text = "Cache of Demolition Charges";
            if (Weapons[0] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            switch (code)
            {
                case 21:
                    CheckBox cb = panel.Controls["cbOption1"] as CheckBox;
                    if (cb.Checked)
                    {
                        Weapons[0] = cb.Text;
                    }
                    else { Weapons[0] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Cache of Demolition Charges"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Goliath Truck - " + Points + "pts";
        }
    }
}
