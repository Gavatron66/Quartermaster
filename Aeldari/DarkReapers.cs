using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class DarkReapers : Datasheets
    {
        public DarkReapers()
        {
            DEFAULT_POINTS = 27;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "1m";
            Weapons.Add("Reaper Launcher");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "INFANTRY", "CORE", "ASPECT WARRIOR", "DARK REAPERS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new DarkReapers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblRelic = panel.Controls["lblRelic"] as Label;

            panel.Controls["lblOption1"].Text = "Dark Reapers Exarch Weapons:";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Aeldari Missile Launcher (+5 pts)",
                "Reaper Launcher",
                "Shuriken Cannon",
                "Tempest Launcher (+10 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            panel.Controls["lblFactionupgrade"].Visible = true;
            cmbFaction.Visible = true;
            lblRelic.Visible = true;
            lblRelic.Location = new System.Drawing.Point(cmbFaction.Location.X, cmbFaction.Location.Y + 34);
            cmbRelic.Visible = true;
            cmbRelic.Location = new System.Drawing.Point(lblRelic.Location.X, lblRelic.Location.Y + 23);

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
                    if(Relic != "(None)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if (Weapons[0] == "Aeldari Missile Launcher (+5 pts)")
            {
                Points += 5;
            }
            if (Weapons[0] == "Tempest Launcher (+10 pts)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Dark Reapers - " + Points + "pts";
        }
    }
}
