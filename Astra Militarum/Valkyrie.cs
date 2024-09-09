using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Valkyrie : Datasheets
    {
        public Valkyrie()
        {
            DEFAULT_POINTS = 140;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m1k";
            Weapons.Add("Multi-laser");
            Weapons.Add("Hellstrike Missiles");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "AERONAUTICA IMPERIALIS", "MILITARUM TEMPESTUS",
                "VEHICLE", "TRANSPORT", "AIRCRAFT", "FLY", "VALKYRIE",
            });
            Role = "Flyer";
        }

        public override Datasheets CreateUnit()
        {
            return new Valkyrie();
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
                "Lascannon (+5 pts)",
                "Multi-laser"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Hellstrike Missiles",
                "Two Multiple Rocket Pods"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Two Heavy Bolters (+10 pts)";
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

            if(cbOption1.Checked)
            {
                Points += 10;
            }

            if (Weapons[0] == "Lascannon (+5 pts)")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Valkyrie - " + Points + "pts";
        }
    }
}
