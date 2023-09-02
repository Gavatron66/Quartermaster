using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class HybridMetamorphs : Datasheets
    {
        public HybridMetamorphs()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "3N1kS(2m)";
            Weapons.Add("5"); //AutopistolS
            Weapons.Add("0"); //Hand FlamerS
            Weapons.Add("0"); //Cult Icon
            Weapons.Add("Autopistol"); //Leader Option 1
            Weapons.Add("Metamorph Mutations"); //Leader Option 2
            Keywords.AddRange(new string[]
            {
                "TYRANIDS", "GENESTEALER CULTS", "<CULT>",
                "INFANTRY", "CORE", "CROSSFIRE", "HYBRID METAMORPHS"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new HybridMetamorphs();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            panel.Controls["lblFactionUpgrade"].Visible = true;
            panel.Controls["cmbFactionUpgrade"].Visible = true;

            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            GroupBox groupBox = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = groupBox.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = groupBox.Controls["gb_cmbOption2"] as ComboBox;

            lblnud1.Text = "Models with Autopistols:";
            lblnud2.Text = "Models with Hand Flamer:";

            cbOption1.Text = "Cult Icon";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 15;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;

            int temp = int.Parse(Weapons[0]);
            nudOption1.Value = temp;
            temp = int.Parse(Weapons[1]);
            nudOption2.Value = temp;

            antiLoop = true;
            if (int.Parse(Weapons[2]) == 1)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
            antiLoop = false;

            groupBox.Text = "Metamorph Leader";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[] {
                "Autopistol",
                "Cult Bonesword",
                "Cult Lash Whip"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[3]);

            gb_cmbOption2.Items.Clear();
            gb_cmbOption2.Items.AddRange(new string[] {
                "Cult Bonesword",
                "Cult Lash Whip",
                "Metamorph Mutations"
            });
            gb_cmbOption2.SelectedIndex = gb_cmbOption2.Items.IndexOf(Weapons[4]);

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
            if(antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            GroupBox groupBox = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = groupBox.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = groupBox.Controls["gb_cmbOption2"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = "1";
                    }
                    else { Weapons[2] = "0"; }
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
                case 31:
                    if (nudOption1.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value + int.Parse(Weapons[2]) <= nudUnitSize.Value - 1)
                    {
                        Weapons[0] = Convert.ToString(nudOption1.Value);
                    }
                    else
                    {
                        nudOption1.Value -= 1;
                    }
                    break;
                case 32:
                    if (nudOption2.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value + int.Parse(Weapons[2]) <= nudUnitSize.Value - 1)
                    {
                        Weapons[1] = Convert.ToString(nudOption2.Value);
                    }
                    else
                    {
                        nudOption2.Value -= 1;
                    }
                    break;
                case 411:
                    Weapons[3] = gb_cmbOption1.SelectedItem.ToString();
                    break;
                case 412:
                    Weapons[4] = gb_cmbOption2.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += (int.Parse(Weapons[1]) * 3);
            Points += (int.Parse(Weapons[2]) * 20);

            for(int i = 3; i < 5; i++)
            {
                if (Weapons[i] == "Cult Bonesword")
                {
                    Points += 5;
                }
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Hybrid Metamorphs - " + Points + "pts";
        }
    }
}
