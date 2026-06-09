using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class Eightbound : Datasheets
    {
        public Eightbound()
        {
            DEFAULT_POINTS = 40;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1m";
            Weapons.Add("Lacerators");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "BUTCHER ASTARTES", "WORLD EATERS",
                "INFANTRY", "CORE", "DAEMON", "EIGHTBOUND"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Eightbound();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as WorldEaters;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            lblExtra1.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, panel.Controls["lblOption1"].Location.Y);
            lblExtra1.Text = "Eightbound Champion Weapons:";
            lblExtra1.Visible = true;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Chainglaive",
                "Lacerators"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            if (repo.currentSubFaction == "Disciples of the Red Angel")
            {
                panel.Controls["lblFactionupgrade"].Visible = true;
                cmbFaction.Visible = true;

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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Eightbound - " + Points + "pts";
        }
    }
}
