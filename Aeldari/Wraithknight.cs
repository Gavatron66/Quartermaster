using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Wraithknight : Datasheets
    {
        public Wraithknight()
        {
            DEFAULT_POINTS = 400;
            Points = DEFAULT_POINTS;
            TemplateCode = "4m";
            Weapons.Add("Titanic Ghostglaive");
            Weapons.Add("Scattershield");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "SPIRIT HOST", "<CRAFTWORLD>",
                "MONSTER", "TITANIC", "WRAITH CONSTRUCT", "WRAITHKNIGHT"
            });
            Role = "Lord of War";
        }

        public override Datasheets CreateUnit()
        {
            return new Wraithknight();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Wraithcannon (+30 pts)",
                "Suncannon (+50 pts)",
                "Titanic Ghostglaive"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Wraithcannon (+30 pts)",
                "Scattershield"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Scatter Laser (+5 pts)",
                "Shuriken Cannon (+10 pts)",
                "Starcannon (+15 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "(None)",
                "Scatter Laser (+5 pts)",
                "Shuriken Cannon (+10 pts)",
                "Starcannon (+15 pts)"
            });
            cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;

            switch(code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem.ToString();
                    break;
                case 14:
                    Weapons[3] = cmbOption4.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Scatter Laser (+5 pts)")
                {
                    Points += 5;
                }
                if(weapon == "Shuriken Cannon (+10 pts)")
                {
                    Points += 10;
                }
                if (weapon == "Starcannon (+15 pts)")
                {
                    Points += 15;
                }
                if (weapon == "Heavy Wraithcannon (+30 pts)")
                {
                    Points += 30;
                }
                if (weapon == "Suncannon (+50 pts)")
                {
                    Points += 50;
                }
            }
        }

        public override string ToString()
        {
            return "Wraithknight - " + Points + " pts";
        }
    }
}
