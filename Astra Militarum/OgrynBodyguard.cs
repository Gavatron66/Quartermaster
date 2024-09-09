using Roster_Builder.Grey_Knights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class OgrynBodyguard : Datasheets
    {
        public OgrynBodyguard()
        {
            DEFAULT_POINTS = 50;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m1k";
            Weapons.Add("Ripper Gun");
            Weapons.Add("Huge Knife");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "MILITARUM AUXILLA", "ABHUMAN", "BODYGUARD", "OGRYNS"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new OgrynBodyguard();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AstraMilitarum;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bullgryn Maul (+5 pts)",
                "Grenadier Gauntlet",
                "Ripper Gun"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Brute Shield",
                "Huge Knife",
                "Slabshield (+5 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Bullgryn Plate (+5 pts)";
            if (Weapons[2] != string.Empty)
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
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            if(Weapons.Contains("Bullgryn Maul (+5 pts)"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Bullgryn Plate (+5 pts)"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Slabshield (+5 pts)"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Ogryn Bodyguard - " + Points + "pts";
        }
    }
}
