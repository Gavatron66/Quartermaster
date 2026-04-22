using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class HowlingBanshees : Datasheets
    {
        public HowlingBanshees()
        {
            DEFAULT_POINTS = 19;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1m";
            Weapons.Add("Banshee Blade");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "INFANTRY", "CORE", "ASPECT WARRIOR", "HOWLING BANSHEES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new HowlingBanshees();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            panel.Controls["lblOption1"].Text = "Howling Banshee Exarch Weapons:";
            panel.Controls["lblOption1"].Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X - 15,
                panel.Controls["lblOption1"].Location.Y);

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Banshee Blade",
                "Executioner (+5 pts)",
                "Mirrorswords",
                "Triskele"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            panel.Controls["lblFactionupgrade"].Visible = true;
            cmbFaction.Visible = true;
            panel.Controls["lblRelic"].Visible = true;
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(293, 146);
            cmbRelic.Visible = true;
            cmbRelic.Location = new System.Drawing.Point(293, 169);

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
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[0] == "Executioner (+5 pts)")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Howling Banshees - " + Points + "pts";
        }
    }
}
