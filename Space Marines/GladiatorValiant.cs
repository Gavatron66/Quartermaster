using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class GladiatorValiant : Datasheets
    {
        public GladiatorValiant()
        {
            DEFAULT_POINTS = 170;
            UnitSize = 1;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "4k";
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "GLADIATOR", "REPULSOR FIELD", "GLADIATOR VALIANT"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new GladiatorValiant();
        }
        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;

            cbOption1.Text = "Ironhail Heavy Stubber";
            if (Weapons[0] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Icarus Rocket Pod";
            if (Weapons[1] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Auto Launchers";
            if (Weapons[2] != string.Empty)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }

            cbOption4.Text = "Multi-melta";
            if (Weapons[3] != string.Empty)
            {
                cbOption4.Checked = true;
            }
            else
            {
                cbOption4.Checked = false;
            }

            if (repo.currentSubFaction != "Black Templars")
            {
                cbOption4.Visible = false;
            }

            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            cbStratagem5.Location = new System.Drawing.Point(cbOption3.Location.X, cbOption3.Location.Y + 32);

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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            switch (code)
            {
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                        cbOption4.Enabled = false;
                        cbOption4.Checked = false;
                    }
                    else { Weapons[0] = string.Empty; cbOption4.Enabled = true; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[1] = cbOption2.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[2] = cbOption3.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
                case 24:
                    if (cbOption4.Checked)
                    {
                        Weapons[3] = cbOption4.Text;
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
            return "Gladiator Valiant - " + Points + "pts";
        }
    }
}
