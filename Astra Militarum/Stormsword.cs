using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class Stormsword : Datasheets
    {
        public Stormsword()
        {
            DEFAULT_POINTS = 420;
            Points = DEFAULT_POINTS;
            TemplateCode = "2m";
            Weapons.Add("Two Twin Heavy Flamers");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "VEHICLE", "TITANIC", "SMOKE", "ARMOURED", "REGIMENTAL", "SUPER-HEAVY", "STORMSWORD"
            });
            Role = "Lord of War";
        }

        public override Datasheets CreateUnit()
        {
            return new Stormsword();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Two Twin Heavy Bolters",
                "Two Twin Heavy Flamers"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Two Lascannons and Two Twin Heavy Bolters (+40 pts)",
                "Two Lascannons and Two Twin Heavy Flamers (+40 pts)"
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

            if (Weapons[1] != "(None)")
            {
                Points += 40;
            }
        }

        public override string ToString()
        {
            return "Stormsword - " + Points + "pts";
        }
    }
}
