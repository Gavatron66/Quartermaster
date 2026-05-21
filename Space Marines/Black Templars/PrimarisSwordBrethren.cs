using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class PrimarisSwordBrethren : Datasheets
    {
        int currentIndex = 0;
        int[] restrictionsArray = new int[6];
        List<int> restrictedIndexes2 = new List<int>();

        public PrimarisSwordBrethren()
        {
            DEFAULT_POINTS = 22;
            UnitSize = 4;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Heavy Bolt Pistol");
                Weapons.Add("Astartes Chainsword");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "BLACK TEMPLARS",
                "INFANTRY", "CORE", "PRIMARIS", "SWORD BRETHREN"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new PrimarisSwordBrethren();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 4;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Primaris Sword Brother w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Bolt Pistol",
                "Plasma Pistol",
                "Pyre Pistol"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Power Axe",
                "Power Maul",
                "Power Sword",
                "Thunder Hammer",
                "Two Lightning Claws"
            });

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["cmbOption2"].Location.X, panel.Controls["cmbOption2"].Location.Y + 60);
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

            //I don't know why I need this, but otherwise there's a weird bug that occurs if I don't
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cbStratagem4.Text = repo.StratagemList[3];
            cbStratagem4.Location = new System.Drawing.Point(cmbRelic.Location.X, cmbRelic.Location.Y + 30);

            //Relic Bearers Code
            panel.Controls["lblExtra1"].Visible = true;
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(cbStratagem4.Location.X, cbStratagem4.Location.Y + 30);
            panel.Controls["lblExtra1"].Text = "Relic Bearers";

            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            cmbFaction.Visible = true;
            cmbFaction.Location = new System.Drawing.Point(cbStratagem4.Location.X + 4, cbStratagem4.Location.Y + 54);
            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(this.Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            cmbFaction.Visible = true;

            if (Stratagem.Contains(cbStratagem4.Text))
            {
                cbStratagem4.Checked = true;
                cbStratagem4.Enabled = true;
            }
            else
            {
                cbStratagem4.Checked = false;
                cbStratagem4.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem4.Text));
            }
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
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Primaris Sword Brother w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    break;
                case 12:
                    if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Primaris Sword Brother w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (Relic == "Witchseeker Bolts")
                    {
                        cmbOption1.SelectedIndex = 0;
                        cmbOption1.Enabled = false;
                    }
                    else if (Relic == "Sword of Judgement")
                    {
                        cmbOption2.SelectedIndex = 3;
                        cmbOption2.Enabled = false;
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Heavy Bolt Pistol");
                        Weapons.Add("Astartes Chainsword");
                        lbModelSelect.Items.Add("Primaris Sword Brother w/ " + Weapons[((UnitSize - 1) * 2)] + " and " + Weapons[((UnitSize - 1) * 2) + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2), 2);
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
                    else if (currentIndex == -1)
                    {
                        break;
                    }
                    antiLoop = true;

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (currentIndex == 0)
                    {
                        cbStratagem4.Visible = true;
                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }
                        else
                        {
                            cmbRelic.Visible = false;
                        }

                        if (Relic == "Witchseeker Bolts")
                        {
                            cmbOption1.SelectedIndex = 0;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Sword of Judgement")
                        {
                            cmbOption2.SelectedIndex = 3;
                            cmbOption2.Enabled = false;
                        }
                    }
                    else
                    {
                        cbStratagem4.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                    }

                    restrictedIndexes.Clear();
                    restrictedIndexes2.Clear();

                    #region Weapons Check
                    if (restrictionsArray[0] == (UnitSize / 10) + 1 && Weapons[(currentIndex * 2) + 1] != "Thunder Hammer")
                    {
                        restrictedIndexes2.Add(4);
                    }

                    if (restrictionsArray[1] == (UnitSize / 10) + 1 && Weapons[(currentIndex * 2) + 1] != "Two Lightning Claws")
                    {
                        restrictedIndexes2.Add(5);
                    }

                    if (restrictionsArray[2] == (UnitSize / 10) + 1 && Weapons[(currentIndex * 2) + 1] != "Power Axe")
                    {
                        restrictedIndexes2.Add(1);
                    }

                    if (restrictionsArray[3] == (UnitSize / 10) + 1 && Weapons[(currentIndex * 2) + 1] != "Power Maul")
                    {
                        restrictedIndexes2.Add(2);
                    }

                    if (restrictionsArray[4] == (UnitSize / 10) + 1 && Weapons[currentIndex * 2] != "Plasma Pistol")
                    {
                        restrictedIndexes.Add(1);
                    }

                    if (restrictionsArray[5] == ((UnitSize / 10) + 1) * 2 && Weapons[currentIndex * 2] != "Pyre Pistol")
                    {
                        restrictedIndexes.Add(2);
                    }
                    #endregion

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);

                    antiLoop = false;

                    lbModelSelect.SelectedIndex = currentIndex;
                    break;
                case 74:
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
                        }
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

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            restrictionsArray = new int[6];
            foreach (var weapon in Weapons)
            {
                if(weapon == "Thunder Hammer")
                {
                    restrictionsArray[0]++;
                }

                if (weapon == "Two Lightning Claws")
                {
                    restrictionsArray[1]++;
                }

                if (weapon == "Power Axe")
                {
                    restrictionsArray[2]++;
                }

                if (weapon == "Power Maul")
                {
                    restrictionsArray[3]++;
                }

                if (weapon == "Plasma Pistol")
                {
                    restrictionsArray[4]++;
                }

                if (weapon == "Pyre Pistol")
                {
                    restrictionsArray[5]++;
                }
            }
        }

        public override string ToString()
        {
            return "Primaris Sword Brethren - " + Points + "pts";
        }
    }
}
