using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Impulsor : Datasheets
    {
        public Impulsor()
        {
            DEFAULT_POINTS = 100;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m2k";
            Weapons.Add("Two Storm Bolters");
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "TRANSPORT", "REPULSOR FIELD", "IMPULSOR"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new Impulsor();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Two Fragstorm Grenade Launchers",
                "Two Storm Bolters"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Bellicatus Missile Array",
                "Ironhail Skytalon Array",
                "Orbital Comms Array",
                "Shield Dome"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Ironhail Heavy Stubber";
            if (Weapons[2] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Multi-melta";
            if (Weapons[3] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            if (repo.currentSubFaction != "Black Templars")
            {
                cbOption2.Visible = false;
            }

            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            cbStratagem5.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 32);

            if (repo.customSubFactionTraits[2] == "Blood Angels")
            {
                cbStratagem5.Visible = true;
                cbStratagem5.Text = "Stratagem: Lucifer-pattern Engine";
            }
            else
            {
                cbStratagem5.Visible = false;
                if (Stratagem.Contains(cbStratagem5.Text))
                {
                    Stratagem.Remove(cbStratagem5.Text);
                }
            }

            if (Stratagem.Contains(cbStratagem5.Text))
            {
                cbStratagem5.Checked = true;
                cbStratagem5.Enabled = true;
            }
            else
            {
                cbStratagem5.Checked = false;
                cbStratagem5.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem5.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

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
                        Weapons[1] = cbOption1.Text;
                        cbOption2.Enabled = false;
                        cbOption2.Checked = false;
                    }
                    else { Weapons[1] = string.Empty; cbOption2.Enabled = true; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[3] = cbOption2.Text;
                        cbOption1.Enabled = false;
                        cbOption1.Checked = false;
                    }
                    else { Weapons[3] = string.Empty; cbOption1.Enabled = true; }
                    break;
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        Stratagem.Add(cbStratagem5.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            Stratagem.Remove(cbStratagem5.Text);
                        }
                    }
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Impulsor - " + Points + "pts";
        }
    }
}
