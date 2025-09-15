using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Grey_Knights
{
    public class GKVenerableDreadnought : Datasheets
    {
        public GKVenerableDreadnought()
        {
            DEFAULT_POINTS = 145;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Assault Cannon");
            Weapons.Add("DCW and Storm Bolter");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "SANCTIC ASTARTES", "GREY KNIGHTS",
                "VEHICLE", "CORE", "PSYKER", "DREADNOUGHT", "SMOKESCREEN", "HONOURED KNIGHT", "VENERABLE DREADNOUGHT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new GKVenerableDreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as GreyKnights;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Assault Cannon",
                "Heavy Plasma Cannon",
                "Multi-melta",
                "Twin Lascannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "DCW and Heavy Flamer",
                "DCW and Storm Bolter",
                "Missile Launcher"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbStratagem4.Location = new System.Drawing.Point(cmbOption2.Location.X, cmbOption2.Location.Y + 32);
            cbStratagem4.Text = f.StratagemList[2];

            if (repo.currentSubFaction == "Preservers")
            {
                cbStratagem4.Visible = true;
            }
            else
            {
                cbStratagem4.Visible = false;
                if (Stratagem.Contains(cbStratagem4.Text))
                {
                    Stratagem.Remove(cbStratagem4.Text);
                }
            }

            if (Stratagem.Contains(cbStratagem4.Text))
            {
                cbStratagem4.Checked = true;
                cbStratagem4.Enabled = true;
            }
            else
            {
                cbStratagem4.Checked = false;
                cbStratagem4.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem4.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 74:
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
                        }
                    }
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Venerable Dreadnought - " + Points + "pts";
        }
    }
}
