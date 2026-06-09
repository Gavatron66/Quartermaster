using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class ExaltedEightbound : Datasheets
    {
        public ExaltedEightbound()
        {
            DEFAULT_POINTS = 135;
            Points = DEFAULT_POINTS;
            UnitSize = 3;
            TemplateCode = "1m";
            Weapons.Add("Eightbound Eviscerator and Chainfist");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "BUTCHER ASTARTES", "WORLD EATERS",
                "INFANTRY", "DAEMON", "EIGHTBOUND", "EXALTED EIGHTBOUND"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new ExaltedEightbound();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as WorldEaters;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            lblExtra1.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X - 60, panel.Controls["lblOption1"].Location.Y);
            lblExtra1.Text = "Exalted Eightbound Champion Weapons:";
            lblExtra1.Visible = true;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Eightbound Eviscerator and Chainfist",
                "Heavy Chainglaive",
                "Two Eightbound Chainfists"
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
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Exalted Eightbound - " + Points + "pts";
        }
    }
}
