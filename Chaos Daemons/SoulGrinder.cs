using Roster_Builder.Aeldari.Ynnari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class SoulGrinder : Datasheets
    {
        public SoulGrinder()
        {
            DEFAULT_POINTS = 190;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "1m";
            Weapons.Add("Warpsword");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "<ALLEGIANCE>",
                "VEHICLE", "DAEMON", "SOUL GRINDER"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new SoulGrinder();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosDaemons;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblRelic = panel.Controls["lblRelic"] as Label;

            panel.Controls["lblFactionupgrade"].Visible = false;
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(panel.Controls["lblFactionupgrade"].Location.X, panel.Controls["lblFactionupgrade"].Location.Y);
            panel.Controls["lblExtra1"].Visible = true;
            panel.Controls["lblExtra1"].Text = "Daemonic Allegiance";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Warpclaw",
                "Warpsword"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

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

        public override void SaveDatasheets(int code, Panel panel)
        {
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
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Soul Grinder - " + Points + "pts";
        }
    }
}
