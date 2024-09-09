using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class RogalDorn : Datasheets
    {
        public RogalDorn()
        {
            DEFAULT_POINTS = 250;
            Points = DEFAULT_POINTS;
            TemplateCode = "4m1k";
            Weapons.Add("Twin Battle Cannon");
            Weapons.Add("Castigator Gatling Cannon");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "VEHICLE", "BATTLE TANK", "SMOKE", "REGIMENTAL", "SQUADRON", "ROGAL DORN BATTLE TANK"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new RogalDorn();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Oppressor Cannon and Co-axial Autocannon",
                "Twin Battle Cannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Castigator Gatling Cannon",
                "Pulveriser Cannon"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Two Heavy Stubbers (+10 pts)",
                "Two Meltaguns (+10 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "(None)",
                "Two Heavy Bolters (+10 pts)",
                "Two Multi-meltas (+30 pts)"
            });
            cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);

            cbOption1.Text = "Armoured Tracks (+5 pts)";
            if (Weapons[4] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

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
            panel.Controls["lblFactionupgrade"].Visible = true;
            cmbFaction.Visible = true;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
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
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[4] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[4] = "";
                    }
                    break;
            }

            Points = DEFAULT_POINTS;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[2] != "(None)")
            {
                Points += 10;
            }

            if (Weapons[3] == "Two Heavy Bolters (+10 pts)")
            {
                Points += 10;
            }

            if (Weapons[3] == "Two Multi-meltas (+30 pts)")
            {
                Points += 30;
            }

            if (Weapons[4] != "")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Rogal Dorn Battle Tank - " + Points + "pts";
        }
    }
}