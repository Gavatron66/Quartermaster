using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class SwoopingHawks : Datasheets
    {
        public SwoopingHawks()
        {
            DEFAULT_POINTS = 25;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1m1k";
            Weapons.Add("Lasblaster");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "INFANTRY", "CORE", "JUMP PACK", "ASPECT WARRIOR", "SWOOPING HAWKS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new SwoopingHawks();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            panel.Controls["lblOption1"].Text = "Swooping Hawk Exarch Weapons:";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Hawk's Talon (+10 pts)",
                "Lasblaster"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Aeldari Power Sword (+5 pts)";
            if (Weapons[1] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            panel.Controls["lblFactionupgrade"].Visible = true;
            cmbFaction.Visible = true;
            panel.Controls["lblRelic"].Visible = true;
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(293, 176);
            cmbRelic.Visible = true;
            cmbRelic.Location = new System.Drawing.Point(293, 199);

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (Relic != null)
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = -1;
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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 17:
                    Relic = cmbRelic.SelectedItem.ToString();
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
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if(Weapons.Contains("Aeldari Power Sword (+5 pts)"))
            {
                Points += 5;
            }

            if(Weapons.Contains("Hawk's Talon (+10 pts)"))
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Swooping Hawks - " + Points + "pts";
        }
    }
}
