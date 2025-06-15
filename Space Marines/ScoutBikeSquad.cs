using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class ScoutBikeSquad : Datasheets
    {
        int currentIndex;
        public ScoutBikeSquad()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Bolt Pistol");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Twin Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "BIKER", "CORE", "SCOUT", "SMOKESCREEN", "SCOUT BIKE SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new ScoutBikeSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Scout Biker Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Scout Biker w/ " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Combi-flamer",
                "Combi-grav",
                "Combi-melta",
                "Combi-plasma",
                "Grav-pistol",
                //Hand Flamer
                //Inferno Pistol
                "Lightning Claw",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Storm Bolter",
                "Thunder Hammer"
            });
            if (repo.customSubFactionTraits[2] == "Deathwatch" || repo.customSubFactionTraits[2] == "Blood Angels")
            {
                cmbOption1.Items.Insert(7, "Hand Flamer");
                cmbOption1.Items.Insert(8, "Inferno Pistol");
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Grenade Launcher",
                "Twin Boltgun"
            });
            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, panel.Controls["cmbOption2"].Location.Y + 60);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

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
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Scout Biker Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                    }
                    break;
                case 12:
                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Scout Biker Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Scout Biker w/ " + Weapons[currentIndex + 1];
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    cmbOption1.Enabled = true;
                    restrictedIndexes.Clear();

                    if (chosenRelic == "Hellfury Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                        cmbOption1.SelectedIndex = 1;
                    }
                    else if (chosenRelic == "Sunwrath Pistol")
                    {
                        cmbOption1.SelectedIndex = 8;
                        cmbOption1.Enabled = false;
                    }
                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                    Relic = chosenRelic;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Twin Boltgun");
                        lbModelSelect.Items.Add("Scout Biker w/ " + Weapons[temp + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize - 1, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }

                    if(currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);

                        restrictedIndexes.Clear();
                        if (Relic == "Hellfury Bolts")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 12, 14 });
                        }
                        else if (Relic == "Sunwrath Pistol")
                        {
                            cmbOption1.Enabled = false;
                        }
                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else
                    {
                        restrictedIndexes.Clear();

                        cmbOption1.Visible = false;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = true;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);
                    }
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

            Points = UnitSize * DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Scout Bike Squad - " + Points + "pts";
        }
    }
}
