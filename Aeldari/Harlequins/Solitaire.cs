using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari.Harlequins
{
    public class Solitaire : Datasheets
    {
        public Solitaire()
        {
            DEFAULT_POINTS = 110;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "AELDARI", "HARLEQUINS", "<SAEDATH>",
                "CHARACTER", "INFANTRY", "DEATH JESTER"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Solitaire();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Harlequins;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            panel.Controls["lblFactionupgrade"].Visible = true;
            panel.Controls["cmbFactionupgrade"].Visible = true;
            panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(298, 25);
            panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(298, 52);

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
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
            }

            Points = DEFAULT_POINTS;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Solitaire - " + Points + "pts";
        }
    }
}
