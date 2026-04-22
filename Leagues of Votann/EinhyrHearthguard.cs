using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class EinhyrHearthguard : Datasheets
    {
        public EinhyrHearthguard()
        {
            DEFAULT_POINTS = 45;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N2mS(2m)";
            Weapons.Add("EtaCarn Plasma Guns");
            Weapons.Add("Concussion Gauntlets");
            Weapons.Add("Concussion Gauntlet");
            Weapons.Add("Weavefield Crest");
            Keywords.AddRange(new string[]
            {
                "VOTANN", "<LEAGUE>",
                "INFANTRY", "CORE", "EXO-ARMOUR", "EINHYR", "HEARTHGUARD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new EinhyrHearthguard();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as LeaguesOfVotann;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gbUnitLeader.Controls["gb_cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "EtaCarn Plasma Guns",
                "Volkanite Disintegrators"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Concussion Gauntlets",
                "Plasma Blade Gauntlets"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            gbUnitLeader.Text = "Hesyr";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "Concussion Gauntlet",
                "Concussion Hammer (+10 pts)",
                "Plasma Blade Gauntlet"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[2]);

            gb_cmbOption2.Items.Clear();
            gb_cmbOption2.Items.AddRange(new string[]
            {
                "Teleport Crest",
                "Weavefield Crest"
            });
            gb_cmbOption2.SelectedIndex = gb_cmbOption2.Items.IndexOf(Weapons[3]);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gbUnitLeader.Controls["gb_cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
                case 411:
                    Weapons[2] = gb_cmbOption1.SelectedItem as string;
                    break;
                case 412:
                    Weapons[3] = gb_cmbOption2.SelectedItem as string;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[2] == "Concussion Hammer (+10 pts)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Einhyr Hearthguard - " + Points + "pts";
        }
    }
}
