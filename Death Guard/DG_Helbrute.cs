using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DG_Helbrute : Datasheets
    {
        public DG_Helbrute()
        {
            DEFAULT_POINTS = 105;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Multi-melta");
            Weapons.Add("Helbrute Fist");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "VEHICLE", "CORE", "BUBONIC ASTARTES", "HELBRUTE"
            });
            Role = "Elites";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Helbrute Fist",
                "Helbrute Fist w/ Combi-bolter",
                "Helbrute Fist w/ Heavy Flamer",
                "Helbrute Hammer",
                "Helbrute Plasma Cannon",
                "Missile Launcher",
                "Multi-melta",
                "Power Scourge",
                "Reaper Autocannon",
                "Twin Heavy Bolter",
                "Twin Lascannon (+10 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Helbrute Fist",
                "Helbrute Fist w/ Combi-bolter",
                "Helbrute Fist w/ Heavy Flamer",
                "Helbrute Hammer",
                "Missile Launcher",
                "Power Scourge"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmb = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmb2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmb.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmb2.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS;

            if (cmb2.Items.Count > 0)
            {
                if (!cmb2.Items.Contains("Missile Launcher"))
                {
                    cmb2.Items.Insert(cmb2.Items.IndexOf("Helbrute Fist w/ Heavy Flamer") + 2, "Missile Launcher");
                }
                if (!cmb.Items.Contains("Missile Launcher"))
                {
                    cmb.Items.Insert(cmb.Items.IndexOf("Helbrute Plasma Cannon") + 1, "Missile Launcher");
                }
                if (!cmb2.Items.Contains("Helbrute Hammer") &&
                    !cmb2.Items.Contains("Power Scourge"))
                {
                    cmb2.Items.Insert(cmb2.Items.IndexOf("Helbrute Fist w/ Heavy Flamer") + 1, "Helbrute Hammer");
                    cmb2.Items.Insert(cmb2.Items.IndexOf("Helbrute Fist w/ Heavy Flamer") + 3, "Power Scourge");
                }
                if (!cmb.Items.Contains("Helbrute Hammer") &&
                    !cmb.Items.Contains("Power Scourge"))
                {
                    cmb.Items.Insert(cmb2.Items.IndexOf("Helbrute Fist w/ Heavy Flamer") + 1, "Helbrute Hammer");
                    cmb.Items.Insert(cmb2.Items.IndexOf("Multi-melta") + 1, "Power Scourge");
                }

                if (cmb.SelectedItem as string == "Missile Launcher")
                {
                    cmb2.Items.Remove("Missile Launcher");
                }

                if (cmb2.SelectedItem as string == "Missile Launcher")
                {
                    cmb.Items.Remove("Missile Launcher");
                }

                if (cmb.SelectedItem as string == "Helbrute Hammer" ||
                    cmb.SelectedItem as string == "Power Scourge")
                {
                    cmb2.Items.Remove("Helbrute Hammer");
                    cmb2.Items.Remove("Power Scourge");
                }

                if (cmb2.SelectedItem as string == "Helbrute Hammer" ||
                    cmb2.SelectedItem as string == "Power Scourge")
                {
                    cmb.Items.Remove("Helbrute Hammer");
                    cmb.Items.Remove("Power Scourge");
                }
            }


            for (int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i].Contains("Twin Lascannon (+10 pts)"))
                {
                    Points += 10;
                }
            }

        }

        public override string ToString()
        {
            return "Helbrute - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new DG_Helbrute();
        }
    }
}
