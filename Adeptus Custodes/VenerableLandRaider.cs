using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Custodes
{
    public class VenerableLandRaider : Datasheets
    {
        public VenerableLandRaider()
        {
            DEFAULT_POINTS = 285;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "2k";
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS CUSTODES", "<SHIELD HOST>",
                "VEHICLE", "TRANSPORT", "MACHINE SPIRIT", "SMOKESCREEN", "VENERABLE LAND RAIDER"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new VenerableLandRaider();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cbOption1.Text = "Hunter-killer Missile";
            if (Weapons[0] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Storm Bolter";
            if (Weapons[1] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
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
                case 22:
                    CheckBox cb2 = panel.Controls["cbOption2"] as CheckBox;
                    if (cb2.Checked)
                    {
                        Weapons[1] = cb2.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            if(Weapons.Contains("Hunter-killer Missile"))
            {
                Points += 5;
            }

            if(Weapons.Contains("Storm Bolter"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Venerable Land Raider - " + Points + "pts";
        }
    }
}
