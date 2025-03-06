using Roster_Builder.Chaos_Space_Marines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    //This code is copied from CultistsMob in Chaos Space Marines with a few adjustments where necessary
    //Previous code is found commented out below for legacy sake
    //The CultistsMob code is much more clean and readable than the mess below

    public class DG_Cultists : Datasheets
    {
        int currentIndex;

        public DG_Cultists()
        {
            DEFAULT_POINTS = 5;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Autogun");
            }

            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "DEATH GUARD",
                "INFANTRY", "PLAGUE FOLLOWERS", "CULTISTS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new DG_Cultists();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as DeathGuard;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 30;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Cultist Champion w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Chaos Cultist w/ " + Weapons[i]);
            }
        }


        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Cultist Champion w/ " + Weapons[0];
                        break;
                    }

                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Chaos Cultist w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Autogun");
                        lbModelSelect.Items.Add("Chaos Cultist w/ Autogun");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize) + 1, 1);
                    }
                    break;
                case 61:
                    if (antiLoop)
                    {
                        return;
                    }

                    currentIndex = lbModelSelect.SelectedIndex;
                    restrictedIndexes.Clear();

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Autogun",
                            "Autopistol and BAW",
                            "Shotgun"
                        });
                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Autogun",
                            "Autopistol and BAW",
                            "Flamer",
                            "Heavy Stubber"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        LoadOptions(cmbOption1);
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Death Guard Cultists - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1)
        {
            int[] restricted = new int[2] { 0, 0 };

            foreach (var weapon in Weapons)
            {
                if (weapon == "Flamer")
                {
                    restricted[0]++;
                }
                else if (weapon == "Heavy Stubber")
                {
                    restricted[1]++;
                }
            }

            if (restricted[0] == Convert.ToInt32(UnitSize / 10) && Weapons[currentIndex] != "Flamer")
            {
                restrictedIndexes.Add(2);
            }
            if (restricted[1] == Convert.ToInt32(UnitSize / 10) && Weapons[currentIndex] != "Heavy Stubber")
            {
                restrictedIndexes.Add(3);
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }

    //    public class DG_Cultists : Datasheets
    //    {
    //        public DG_Cultists()
    //        {
    //            DEFAULT_POINTS = 5;
    //            UnitSize = 10;
    //            Points = DEFAULT_POINTS * UnitSize;
    //            TemplateCode = "cultist";
    //            Weapons.Add("9"); //Autoguns
    //            Weapons.Add("0"); //Autopistols and Brutal Assault Weapons
    //            Weapons.Add("0"); //Flamers
    //            Weapons.Add("0"); //Heavy Stubbers
    //            Weapons.Add("Autogun"); //Champion Weapon
    //            Keywords.AddRange(new string[]
    //            {
    //                "CHAOS", "NURGLE", "DEATH GUARD",
    //                "INFANTRY", "PLAGUE FOLLOWERS", "CULTISTS"
    //            });
    //            Role = "Troops";
    //        }
    //        public override Datasheets CreateUnit()
    //        {
    //            return new DG_Cultists();
    //        }

    //        public override void LoadDatasheets(Panel panel, Faction f)
    //        {
    //            repo = f as DeathGuard;
    //            Template.LoadTemplate(TemplateCode, panel);
    //            factionsRestrictions = repo.restrictedItems;

    //            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
    //            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
    //            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
    //            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
    //            NumericUpDown nudOption4 = panel.Controls["nudOption4"] as NumericUpDown;

    //            Label lblnud1 = panel.Controls["lblnud1"] as Label;
    //            Label lblnud2 = panel.Controls["lblnud2"] as Label;
    //            Label lblnud3 = panel.Controls["lblnud3"] as Label;
    //            Label lblnud4 = panel.Controls["lblnud4"] as Label;
    //            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;

    //            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
    //            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
    //            ComboBox cmbFactionupgrade = gbUnitLeader.Controls["gb_cmbFactionupgrade"] as ComboBox;

    //            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

    //            int currentSize = UnitSize;
    //            nudUnitSize.Minimum = 10;
    //            nudUnitSize.Value = nudUnitSize.Minimum;
    //            nudUnitSize.Maximum = 30;
    //            nudUnitSize.Value = currentSize;

    //            nudOption1.Minimum = 0;
    //            nudOption1.Maximum = nudUnitSize.Maximum;
    //            nudOption1.Value = 0;

    //            nudOption2.Minimum = 0;
    //            nudOption2.Maximum = nudUnitSize.Maximum;
    //            nudOption2.Value = 0;
    //            nudOption2.Location = new System.Drawing.Point(328, 91);

    //            nudOption3.Minimum = 0;
    //            nudOption3.Maximum = nudUnitSize.Value / 10;
    //            nudOption3.Value = 0;

    //            nudOption4.Minimum = 0;
    //            nudOption4.Maximum = nudUnitSize.Value / 10;
    //            nudOption4.Value = 0;

    //            int temp = int.Parse(Weapons[0]);
    //            nudOption1.Value = temp;
    //            temp = int.Parse(Weapons[1]);
    //            nudOption2.Value = temp;
    //            temp = int.Parse(Weapons[2]);
    //            nudOption3.Value = temp;
    //            temp = int.Parse(Weapons[3]);
    //            nudOption4.Value = temp;

    //            lblnud1.Text = "Models with Autoguns:";
    //            lblnud2.Text = "Models with Autopistols and BAWs:";
    //            lblnud2.Location = new System.Drawing.Point(86, 91);
    //            lblExtra1.Text = "For every 10x models, one of the following may be taken:";
    //            lblnud3.Text = "Flamer:";
    //            lblnud4.Text = "Heavy Stubber:";

    //            gbUnitLeader.Text = "Cultist Champion";

    //            gb_cmbOption1.Items.Clear();
    //            gb_cmbOption1.Items.AddRange(new string[]
    //            {
    //                "Autogun",
    //                "Shotgun",
    //                "Autopistol and BAW"
    //            });
    //            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[4]);

    //            cmbFactionupgrade.Items.Clear();
    //            cmbFactionupgrade.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

    //            if (Factionupgrade != null)
    //            {
    //                cmbFactionupgrade.SelectedIndex = cmbFactionupgrade.Items.IndexOf(Factionupgrade);
    //            }
    //            else
    //            {
    //                cmbFactionupgrade.SelectedIndex = 0;
    //            }

    //            restrictedIndexes = new List<int>();
    //            for (int i = 0; i < cmbFactionupgrade.Items.Count; i++)
    //            {
    //                if (repo.restrictedItems.Contains(cmbFactionupgrade.Items[i]) && Factionupgrade != cmbFactionupgrade.Items[i].ToString())
    //                {
    //                    restrictedIndexes.Add(i);
    //                }
    //            }
    //            this.DrawItemWithRestrictions(restrictedIndexes, cmbFactionupgrade);

    //            gbUnitLeader.Controls["gb_lblFactionupgrade"].Visible = true;
    //            gbUnitLeader.Controls["gb_cmbFactionupgrade"].Visible = true;
    //        }

    //        public override void SaveDatasheets(int code, Panel panel)
    //        {
    //            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
    //            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
    //            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
    //            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
    //            NumericUpDown nudOption4 = panel.Controls["nudOption4"] as NumericUpDown;

    //            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
    //            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
    //            ComboBox cmbFactionupgrade = gbUnitLeader.Controls["gb_cmbFactionupgrade"] as ComboBox;

    //            switch (code)
    //            {
    //                case 30:
    //                    int oldSize = UnitSize;
    //                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

    //                    if (UnitSize > oldSize)
    //                    {
    //                        nudOption1.Value += UnitSize - oldSize;
    //                    }

    //                    if (UnitSize < oldSize)
    //                    {
    //                        if (nudOption1.Value >= oldSize - UnitSize)
    //                        {
    //                            nudOption1.Value -= oldSize - UnitSize;
    //                        }
    //                        else
    //                        {
    //                            nudOption2.Value -= oldSize - UnitSize;
    //                        }
    //                    }
    //                    break;
    //                case 416:
    //                    if (!factionsRestrictions.Contains(cmbFactionupgrade.Text))
    //                    {
    //                        if (Factionupgrade == "(None)")
    //                        {
    //                            Factionupgrade = cmbFactionupgrade.Text;
    //                            if (Factionupgrade != "(None)")
    //                            {
    //                                repo.restrictedItems.Add(Factionupgrade);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            repo.restrictedItems.Remove(Factionupgrade);
    //                            Factionupgrade = cmbFactionupgrade.Text;
    //                            if (Factionupgrade != "(None)")
    //                            {
    //                                repo.restrictedItems.Add(Factionupgrade);
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        cmbFactionupgrade.SelectedIndex = cmbFactionupgrade.Items.IndexOf(Factionupgrade);
    //                    }
    //                    break;
    //                case 411:
    //                    Weapons[4] = gb_cmbOption1.SelectedItem.ToString();
    //                    break;
    //                case 31:
    //                    if(nudOption1.Value == 0)
    //                    {
    //                        break;
    //                    }
    //                    else if(nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1)
    //                    {
    //                        Weapons[0] = Convert.ToString(nudOption1.Value);
    //                    }
    //                    else
    //                    {
    //                        nudOption1.Value -= 1;
    //                    }
    //                    break;
    //                case 32:
    //                    if (nudOption2.Value == 0)
    //                    {
    //                        break;
    //                    }
    //                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1)
    //                    {
    //                        Weapons[1] = Convert.ToString(nudOption2.Value);
    //                    }
    //                    else
    //                    {
    //                        nudOption2.Value -= 1;
    //                    }
    //                    break;
    //                case 33:
    //                    if (nudOption3.Value == 0)
    //                    {
    //                        break;
    //                    }
    //                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1
    //                        && nudOption3.Value + nudOption4.Value <= nudUnitSize.Value/10)
    //                    {
    //                        Weapons[2] = Convert.ToString(nudOption3.Value);
    //                    }
    //                    else
    //                    {
    //                        nudOption3.Value -= 1;
    //                    }
    //                    break;
    //                case 34:
    //                    if (nudOption4.Value == 0)
    //                    {
    //                        break;
    //                    }
    //                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1
    //                        && nudOption3.Value + nudOption4.Value <= nudUnitSize.Value / 10)
    //                    {
    //                        Weapons[3] = Convert.ToString(nudOption4.Value);
    //                    }
    //                    else
    //                    {
    //                        nudOption4.Value -= 1;
    //                    }
    //                    break;
    //            }

    //            nudOption3.Maximum = nudUnitSize.Value / 10;
    //            nudOption4.Maximum = nudUnitSize.Value / 10;

    //            Points = DEFAULT_POINTS * UnitSize;
    //            Points += repo.GetFactionUpgradePoints(Factionupgrade);
    //        }

    //        public override string ToString()
    //        {
    //            return "Death Guard Cultists - " + Points + "pts";
    //        }
    //    }
}
