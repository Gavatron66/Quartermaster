using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Toxicrene : Datasheets
    {
        public Toxicrene()
        {
            DEFAULT_POINTS = 150;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "f";
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "MONSTER", "SPORECASTER", "TOXIC LASHES", "ACID BLOOD", "FEEDER TENDRILS", "TOXICRENE"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Toxicrene();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

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

            switch(code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
            }

            Points = DEFAULT_POINTS + repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Toxicrene - " + Points + "pts";
        }
    }
}
