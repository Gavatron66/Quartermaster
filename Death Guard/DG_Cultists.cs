using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DG_Cultists : Datasheets
    {
        public DG_Cultists()
        {
            DEFAULT_POINTS = 50;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "cultist";
            Weapons.Add("9"); //Autoguns
            Weapons.Add("0"); //Autopistols and Brutal Assault Weapons
            Weapons.Add("0"); //Flamers
            Weapons.Add("0"); //Heavy Stubbers
            Weapons.Add("Autogun"); //Champion Weapon
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "DEATH GUARD",
                "INFANTRY", "PLAGUE FOLLOWERS", "CULTISTS"
            });
        }
        public override Datasheets CreateUnit()
        {
            return new DG_Cultists();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
            NumericUpDown nudOption4 = panel.Controls["nudOption4"] as NumericUpDown;

            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;
            Label lblnud3 = panel.Controls["lblnud3"] as Label;
            Label lblnud4 = panel.Controls["lblnud4"] as Label;
            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;

            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox cmbFactionupgrade = gbUnitLeader.Controls["gb_cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 30;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;
            nudOption2.Location = new System.Drawing.Point(328, 91);

            nudOption3.Minimum = 0;
            nudOption3.Maximum = nudUnitSize.Value / 10;
            nudOption3.Value = 0;

            nudOption4.Minimum = 0;
            nudOption4.Maximum = nudUnitSize.Value / 10;
            nudOption4.Value = 0;

            int temp = int.Parse(Weapons[0]);
            nudOption1.Value = temp;
            temp = int.Parse(Weapons[1]);
            nudOption2.Value = temp;
            temp = int.Parse(Weapons[2]);
            nudOption3.Value = temp;
            temp = int.Parse(Weapons[3]);
            nudOption4.Value = temp;

            lblnud1.Text = "Models with Autoguns:";
            lblnud2.Text = "Models with Autopistols and BAWs:";
            lblnud2.Location = new System.Drawing.Point(86, 91);
            lblExtra1.Text = "For every 10x models, one of the following may be taken:";
            lblnud3.Text = "Flamer:";
            lblnud4.Text = "Heavy Stubber:";

            gbUnitLeader.Text = "Cultist Champion";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "Autogun",
                "Shotgun",
                "Autopistol and BAW"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[4]);

            cmbFactionupgrade.Items.Clear();
            cmbFactionupgrade.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFactionupgrade.SelectedIndex = cmbFactionupgrade.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFactionupgrade.SelectedIndex = 0;
            }

            gbUnitLeader.Controls["gb_lblFactionupgrade"].Visible = true;
            gbUnitLeader.Controls["gb_cmbFactionupgrade"].Visible = true;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
            NumericUpDown nudOption4 = panel.Controls["nudOption4"] as NumericUpDown;

            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox cmbFactionupgrade = gbUnitLeader.Controls["gb_cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 30:
                    //int oldValue = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    /*
                    if(UnitSize > oldValue)
                    {
                        nudOption1.Value += UnitSize - oldValue;
                    }

                    if(UnitSize < oldValue)
                    {
                        nudOption1.Value += oldValue - UnitSize;
                    }
                    */
                    break;
                case 416:
                    Factionupgrade = cmbFactionupgrade.Text;
                    break;
                case 411:
                    Weapons[4] = gb_cmbOption1.SelectedItem.ToString();
                    break;
                case 31:
                    if(nudOption1.Value == 0)
                    {
                        break;
                    }
                    else if(nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1)
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
                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1)
                    {
                        Weapons[1] = Convert.ToString(nudOption2.Value);
                    }
                    else
                    {
                        nudOption2.Value -= 1;
                    }
                    break;
                case 33:
                    if (nudOption3.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1
                        && nudOption3.Value + nudOption4.Value <= nudUnitSize.Value/10)
                    {
                        Weapons[2] = Convert.ToString(nudOption3.Value);
                    }
                    else
                    {
                        nudOption3.Value -= 1;
                    }
                    break;
                case 34:
                    if (nudOption4.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1
                        && nudOption3.Value + nudOption4.Value <= nudUnitSize.Value / 10)
                    {
                        Weapons[3] = Convert.ToString(nudOption4.Value);
                    }
                    else
                    {
                        nudOption4.Value -= 1;
                    }
                    break;
            }

            nudOption3.Maximum = nudUnitSize.Value / 10;
            nudOption4.Maximum = nudUnitSize.Value / 10;

            Points = (Decimal.ToInt16(nudUnitSize.Value) * 5) + (Decimal.ToInt16(nudOption3.Value) * 5)
                + (Decimal.ToInt16(nudOption4.Value) * 5) + repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Death Guard Cultists - " + Points + "pts";
        }
    }
}
