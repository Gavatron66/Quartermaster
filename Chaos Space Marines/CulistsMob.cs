using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class CulistsMob : Datasheets
    {
        int currentIndex;

        public CulistsMob()
        {
            DEFAULT_POINTS = 5;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Autopistol and BAW");
            }

            Keywords.AddRange(new string[]
            {
                "CHAOS", "TRAITORIS ASTARTES", "<LEGION>",
                "INFANTRY", "CULTISTS", "CULTISTS MOB"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new CulistsMob();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Cultist Champion w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Chaos Cultist w/ " + Weapons[i]);
            }

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X + 20, cmbOption1.Location.Y + 60);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            if (Stratagem.Contains(cbStratagem5.Text))
            {
                cbStratagem5.Checked = true;
                cbStratagem5.Enabled = true;

                panel.Controls["lblRelic"].Visible = true;
                cmbRelic.Visible = true;

                if (Relic == "(None)")
                {
                    cmbRelic.SelectedIndex = 0;
                }
                else
                {
                    if (Relic != null && cmbRelic.Items.Contains(Relic))
                    {
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                    }
                    else
                    {
                        cmbRelic.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                cbStratagem5.Checked = false;
                cmbRelic.SelectedIndex = 0;
            }

            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
        }


        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

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
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    Relic = chosenRelic;

                    if(chosenRelic == "Viper's Spite" || chosenRelic == "The Warp's Malice")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Autogun");
                        lbModelSelect.Items.Add("Chaos Cultist w/ Autopistol and BAW");
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

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cbStratagem5.Visible = true;
                        cmbOption1.Enabled = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Autopistol and BAW",
                            "Bolt Pistol and BAW",
                            "Cultist Firearm"
                        });
                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        if(Relic == "Viper's Spite" || Relic == "The Warp's Malice")
                        {
                            cmbOption1.Enabled = false;
                        }
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbOption1.Enabled = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Autopistol and BAW",
                            "Cultist Firearm",
                            "Cultist Grenade Launcher",
                            "Flamer",
                            "Heavy Stubber"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        LoadOptions(cmbOption1);
                    }

                    antiLoop = false;
                    break;
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        Stratagem.Add(cbStratagem5.Text);
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            Stratagem.Remove(cbStratagem5.Text);
                        }
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.SelectedIndex = 0;
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Culists Mob - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1)
        {
            restrictedIndexes.Clear();
            int[] restricted = new int[3] { 0, 0, 0 };

            foreach (var weapon in Weapons)
            {
                if(weapon == "Flamer")
                {
                    restricted[0]++;
                }
                else if (weapon == "Heavy Stubber")
                {
                    restricted[1]++;
                }
                else if (weapon == "Cultist Grenade Launcher")
                {
                    restricted[2]++;
                }
            }

            if (restricted[0] == Convert.ToInt32(UnitSize / 7.5) && Weapons[currentIndex] != "Flamer")
            {
                restrictedIndexes.Add(3);
            }
            if (restricted[1] == Convert.ToInt32(UnitSize / 7.5) && Weapons[currentIndex] != "Heavy Stubber")
            {
                restrictedIndexes.Add(4);
            }
            if (restricted[2] == Convert.ToInt32(UnitSize / 7.5) && Weapons[currentIndex] != "Cultist Grenade Launcher")
            {
                restrictedIndexes.Add(2);
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }
}
