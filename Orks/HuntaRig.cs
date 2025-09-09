using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class HuntaRig : Datasheets
    {
        public HuntaRig()
        {
            DEFAULT_POINTS = 160;
            UnitSize = 1;
            TemplateCode = "f";
            Points = UnitSize * DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "VEHICLE", "TRANSPORT", "BEAST SNAGGA", "HUNTA RIG"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new HuntaRig();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Orks;

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

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
            }

        }

        public override string ToString()
        {
            return "Hunta Rig - " + Points + "pts";
        }
    }
}
