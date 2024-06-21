using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Battlewagon : Datasheets
    {
        public Battlewagon()
        {
            DEFAULT_POINTS = 105;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m5k1N";
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("0");
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "VEHICLE", "TRANSPORT", "WAGON", "BATTLEWAGON"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Battlewagon();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Orks;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            CheckBox cbOption5 = panel.Controls["cbOption5"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Kannon (+5 pts)",
                "Killkannon (+15 pts)",
                "Zzap Gun (+5 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Lobba (+5 pts)";
            if (Weapons[1] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "'Ard Case (+15 pts)";
            if (Weapons[2] == cbOption2.Text)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Deff Rolla (+15 pts)";
            if (Weapons[3] == cbOption3.Text)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }

            cbOption4.Text = "Grabbin' Klaw (+5 pts)";
            if (Weapons[4] == cbOption4.Text)
            {
                cbOption4.Checked = true;
            }
            else
            {
                cbOption4.Checked = false;
            }

            cbOption5.Text = "Wreckin' Ball (+5 pts)";
            if (Weapons[5] == cbOption5.Text)
            {
                cbOption5.Checked = true;
            }
            else
            {
                cbOption5.Checked = false;
            }

            panel.Controls["lblnud1"].Text = "Big Shootas (+5 pts/each):";
            panel.Controls["lblnud1"].Location = new System.Drawing.Point(cbOption1.Location.X - 200, panel.Controls["lblnud1"].Location.Y);

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = 4;
            nudOption1.Value = Convert.ToInt32(Weapons[6].ToString());

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            CheckBox cbOption5 = panel.Controls["cbOption5"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    if (Factionupgrade == "Da Booma")
                    {
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[3] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                case 24:
                    if (cbOption4.Checked)
                    {
                        Weapons[4] = cbOption4.Text;
                    }
                    else
                    {
                        Weapons[4] = "";
                    }
                    break;
                case 26:
                    if (cbOption5.Checked)
                    {
                        Weapons[5] = cbOption5.Text;
                    }
                    else
                    {
                        Weapons[5] = "";
                    }
                    break;
                case 31:
                    Weapons[6] = nudOption1.Value.ToString();
                    break;
            }

            Points = DEFAULT_POINTS;

            if(Weapons.Contains("'Ard Case (+15 pts)"))
            {
                Points += 15;
            }
            if (Weapons.Contains("Deff Rolla (+15 pts)"))
            {
                Points += 15;
            }
            if (Weapons.Contains("Grabbin' Klaw (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Kannon (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Killkannon (+15 pts)"))
            {
                Points += 15;
            }
            if (Weapons.Contains("Lobba (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Wreckin' Ball (+5 pts)"))
            {
                Points += 5;
            }
            if (Weapons.Contains("Zzap Gun (+5 pts)"))
            {
                Points += 5;
            }

            Points += (int)nudOption1.Value * 5;
        }

        public override string ToString()
        {
            return "Battlewagon - " + Points + "pts";
        }
    }
}
