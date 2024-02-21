using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class Longstrike : Datasheets
    {
        public Longstrike()
        {
            DEFAULT_POINTS = 160;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m2k_c";
            Weapons.Add("Railgun");
            Weapons.Add("Two Gun Drones");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "T'AU SEPT",
                "VEHICLE", "CHARACTER", "FLY", "HAMMERHEAD", "LONGSTRIKE"
            });
            Role = "HQ";
            WarlordTrait = "Through Boldness, Victory";
        }

        public override Datasheets CreateUnit()
        {
            return new Longstrike();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as T_au;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            panel.Controls["lblRelic"].Visible = false;
            panel.Controls["cmbRelic"].Visible = false;
            panel.Controls["cbStratagem1"].Visible = false;
            panel.Controls["cbStratagem2"].Visible = false;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Ion Cannon (+10 pts)",
                "Railgun"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Two Accelerator Burst Cannons (+10 pts)",
                "Two Gun Drones",
                "Two Smart Missile Systems (+10 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Seeker Missile (+5 pts)";
            if (Weapons[2] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Seeker Missile (+5 pts)";
            if (Weapons[3] == cbOption2.Text)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cmbWarlord.Items.Clear();
            cmbWarlord.Items.Add(WarlordTrait);
            cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
            cmbWarlord.Enabled = false;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
                case 15:
                    if (cmbWarlord.SelectedIndex != -1)
                    {
                        WarlordTrait = cmbWarlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[3] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Seeker Missile (+5 pts)")
                {
                    Points += 5;
                }
                if(weapon == "Ion Cannon (+10 pts)" || weapon == "Two Smart Missile Systems (+10 pts)" ||
                    weapon == "Two Accelerator Burst Cannons (+10 pts)")
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Longstrike - " + Points + "pts";
        }
    }
}