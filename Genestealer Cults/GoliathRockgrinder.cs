using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class GoliathRockgrinder : Datasheets
    {
        public GoliathRockgrinder()
        {
            DEFAULT_POINTS = 110;
            Points = DEFAULT_POINTS;
            UnitSize = 1;
            TemplateCode = "1m1k";
            Weapons.Add("");
            Weapons.Add("Heavy Mining Laser");
            Keywords.AddRange(new string[]
            {
                "TYRANIDS", "GENESTEALER CULTS", "<CULT>",
                "VEHICLE", "TRANSPORT", "CROSSFIRE", "GOLIATH ROCKGRINDER"
            });
            role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new GoliathRockgrinder();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[] {
                "Clearance Incinerator",
                "Heavy Mining Laser",
                "Heavy Seismic Cannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Cache of Demolition Charges";
            if (Weapons[0] == string.Empty)
            {
                cbOption1.Checked = false;
            }
            else
            {
                cbOption1.Checked = true;
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
            ComboBox cmb = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cb = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[1] = cmb.SelectedItem.ToString();
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 21:
                    if (cb.Checked)
                    {
                        Weapons[0] = cb.Text;
                    }
                    else { Weapons[0] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Goliath Rockgrinder - " + Points + "pts";
        }
    }
}
