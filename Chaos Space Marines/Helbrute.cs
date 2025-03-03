using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Helbrute : Datasheets
    {
        public Helbrute()
        {
            DEFAULT_POINTS = 105;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Missile Launcher");
            Weapons.Add("Twin Heavy Bolter");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVDED", "<LEGION>",
                "VEHICLE", "CORE", "HELBRUTE"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Helbrute();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Helbrute Fist",
                "Helbrute Fist w/ Combi-bolter",
                "Helbrute Fist w/ Heavy Flamer",
                "Helbrute Hammer",
                "Missile Launcher",
                "Power Scourge"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Helbrute Fist",
                "Helbrute Fist w/ Combi-bolter",
                "Helbrute Fist w/ Heavy Flamer",
                "Helbrute Plasma Cannon",
                "Multi-melta",
                "Reaper Autocannon",
                "Twin Heavy Bolter",
                "Twin Lascannon (+10 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

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
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
            }

            Points = DEFAULT_POINTS;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[1] == "Twin Lascannon (+10 pts)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Helbrute - " + Points + "pts";
        }
    }
}
