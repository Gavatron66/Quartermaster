using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class AssaultSquad : Datasheets
    {
        int currentIndex = 0;
        int[] restrictArray = new int[] { 0, 0 };

        public AssaultSquad()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m2k";
            Weapons.Add("Bolt Pistol");
            Weapons.Add("Astartes Chainsword");
            Weapons.Add(""); //Combat Shield
            Weapons.Add(""); //Jump Packs
            for(int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Pistol and Astartes Chainsword");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "MELTA BOMBS", "ASSAULT SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new AssaultSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            cbOption1.Location = new System.Drawing.Point (cbOption1.Location.X, cbOption1.Location.Y + 60);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 60);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[2] == "")
            {
                lbModelSelect.Items.Add("Assault Marine Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            }
            else
            {
                lbModelSelect.Items.Add("Assault Marine Sergeant w/ " + Weapons[0] + ", " + Weapons[1] + " and a " + Weapons[2]);
            }
            for(int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Assault Marine w/ " + Weapons[i + 3]);
            }

            cmbOption1.Items.Clear();
            cmbOption2.Items.Clear();

            cbOption1.Text = "Jump Packs (All Models)";
            cbOption2.Text = "Combat Shield";

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["cbOption1"].Location.X, panel.Controls["cbOption1"].Location.Y + 60);
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
                        antiLoop = true;
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        antiLoop = false;
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[0] = "Assault Marine Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Assault Marine Sergeant w/ " + Weapons[0] + ", " + Weapons[1] + " and a " + Weapons[2];
                        }
                    }
                    else
                    {
                        if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                        {
                            Weapons[currentIndex + 3] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Assault Marine w/ " + Weapons[currentIndex + 3];
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 3]);
                        }
                    }
                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[0] = "Assault Marine Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Assault Marine Sergeant w/ " + Weapons[0] + ", " + Weapons[1] + " and a " + Weapons[2];
                        }
                    }
                    else
                    {
                        Weapons[currentIndex + 4] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Assault Marine w/ " + Weapons[currentIndex + 4];
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();

                    #region Codex Supplement: Ultramarines
                    if (chosenRelic == "Hellfury Bolts")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Sunwrath Pistol")
                    {
                        cmbOption1.SelectedIndex = 4;
                        cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    else if (chosenRelic == "Dragonrage Bolts")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Drakeblade")
                    {
                        cmbOption2.SelectedIndex = 5;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard
                    else if (chosenRelic == "Silentus Pistol" || chosenRelic == "Korvidari Bolts")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    #endregion

                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[3] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                        lbModelSelect.Items[0] = "Assault Marine Sergeant w/ " + Weapons[0] + ", " + Weapons[1] + " and a " + Weapons[2];
                    }
                    else
                    {
                        Weapons[2] = "";
                        lbModelSelect.Items[0] = "Assault Marine Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Bolt Pistol and Astartes Chainsword");
                        lbModelSelect.Items.Add("Assault Marine w/ " + Weapons[temp + 3]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize + 3, 1);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0 && !antiLoop)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        break;
                    }
                    antiLoop = true;

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbOption1.Visible = true;
                        cbOption2.Visible = true;
                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Bolt Pistol",
                            //Hand Flamer
                            "Grav-pistol",
                            //Inferno Pistol
                            "Lightning Claw",
                            "Plasma Pistol",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword",
                            "Thunder Hammer"
                        });
                        if (repo.customSubFactionTraits[2] == "Blood Angels" || repo.customSubFactionTraits[2] == "Deathwatch")
                        {
                            cmbOption1.Items.Insert(2, "Hand Flamer");
                            cmbOption1.Items.Insert(4, "Inferno Pistol");
                        }
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Lightning Claw",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword",
                            "Thunder Hammer"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        this.DrawItemWithRestrictions(new List<int>(), cmbOption1);

                        #region Codex Supplement: Ultramarines
                        if (Relic == "Hellfury Bolts")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Sunwrath Pistol")
                        {
                            cmbOption1.SelectedIndex = 4;
                            cmbOption1.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Salamanders
                        else if (Relic == "Dragonrage Bolts")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Drakeblade")
                        {
                            cmbOption2.SelectedIndex = 5;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Raven Guard
                        else if (Relic == "Silentus Pistol" || Relic == "Korvidari Bolts")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        #endregion

                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = false;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = false;
                    cbOption1.Visible = true;
                    cbOption2.Visible = false;
                    cbStratagem5.Visible = false;
                    cmbRelic.Visible = false;
                    panel.Controls["lblRelic"].Visible = false;

                    cmbOption1.Items.Clear();
                    cmbOption1.Items.AddRange(new string[]
                    {
                        "Bolt Pistol and Astartes Chainsword",
                        "Bolt Pistol and Eviscerator",
                        "Flamer",
                        "Plasma Pistol and Astartes Chainsword",
                        "Plasma Pistol and Eviscerator"
                    });
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 3]);

                    restrictedIndexes.Clear();
                    if (restrictArray[0] == 2 && !(Weapons[currentIndex + 3].Contains("Plasma Pistol") || Weapons[currentIndex + 3] == "Flamer"))
                    {
                        restrictedIndexes.Add(2);
                        restrictedIndexes.Add(3);
                        restrictedIndexes.Add(4);
                    }

                    if (restrictArray[1] == UnitSize / 5 && !Weapons[currentIndex + 3].Contains("Eviscerator"))
                    {
                        restrictedIndexes.Add(1);
                        restrictedIndexes.Add(4);
                    }
                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

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

            restrictArray[0] = 0;
            restrictArray[1] = 0;

            foreach (var weapon in Weapons)
            {
                if(weapon.Contains("Plasma Pistol") || weapon == "Flamer")
                {
                    restrictArray[0]++;
                }

                if(weapon.Contains("Eviscerator"))
                {
                    restrictArray[1]++;
                }
            }
        }

        public override string ToString()
        {
            return "Assault Squad - " + Points + "pts";
        }
    }
}
