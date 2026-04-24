using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Legionaries : Datasheets
    {
        int currentIndex;
        bool icon;
        bool balefire = false;
        bool khorne = false;

        public Legionaries()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVDED", "<LEGION>",
                "INFANTRY", "CORE", "LEGIONARIES"
            });
            Role = "Troops";
            PsykerPowers = new string[1] { string.Empty };
        }

        public override Datasheets CreateUnit()
        {
            return new Legionaries();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            lblPsyker.Location = new System.Drawing.Point(panel.Controls["lblFactionUpgrade"].Location.X, panel.Controls["lblFactionUpgrade"].Location.Y + 52);
            clbPsyker.Location = new System.Drawing.Point(cmbFaction.Location.X, cmbFaction.Location.Y + 52);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Aspiring Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Legionary w/ " + Weapons[i + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Tainted Chainaxe"
            });

            cbOption1.Text = "Chaos Icon";
            cbOption1.Visible = true;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

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

            if(icon)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("DH");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            lblPsyker.Text = "Select one of the following:";
            clbPsyker.ClearSelected();
            for (int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
            }

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(cmbFaction.Location.X + 128, cbOption1.Location.Y);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cmbFaction.Location.X + cmbFaction.Width + 32, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cmbFaction.Location.X + cmbFaction.Width + 32, cbStratagem5.Location.Y + 50);

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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[0] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[0] = "Aspiring Champion w/ " + Weapons[0] + " and " + Weapons[1];
                            break;
                        }

                        Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Legionary w/ " + Weapons[currentIndex + 1];
                    }
                    else
                    {
                        if (currentIndex == 0)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);

                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        }
                    }

                    if (Weapons[currentIndex + 1] == "Balefire Tome (+20 pts)" || balefire)
                    {
                        clbPsyker.Visible = true;
                        lblPsyker.Visible = true;
                    }
                    else
                    {
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;

                        if (PsykerPowers[0] != string.Empty)
                        {
                            clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), false);
                            PsykerPowers[0] = string.Empty;
                        }
                    }
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Aspiring Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    break;
                case 16:
                    Factionupgrade = cmbFactionupgrade.Text;

                    if(Factionupgrade == "Mark of Khorne (+15 pts)")
                    {
                        khorne = true;

                        if(balefire)
                        {
                            int tempIndex = Weapons.IndexOf("Balefire Tome (+20 pts)");
                            Weapons[tempIndex] = "Boltgun";
                            lbModelSelect.Items[tempIndex - 1] = "Legionary w/ " + Weapons[tempIndex];

                            PsykerPowers[0] = string.Empty;
                            clbPsyker.Visible = false;
                            lblPsyker.Visible = false;
                        }
                    }
                    else
                    {
                        khorne = false;
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();

                    if (chosenRelic == "Hyper-Growth Bolts (Slot 1)" || chosenRelic == "Loyalty's Reward (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if(chosenRelic == "Hyper-Growth Bolts (Slot 2)" || chosenRelic == "Loyalty's Reward (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Ashen Axe (Slot 1)" || chosenRelic == "Axe of the Forgemaster (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 4;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Ashen Axe (Slot 2)" || chosenRelic == "Axe of the Forgemaster (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 3;
                        cmbOption2.Enabled = false;
                    }
                    else if(chosenRelic == "Viper's Spite" || chosenRelic == "The Warp's Malice")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Distortion (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 7;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Distortion (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 6;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "The Black Mace (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 6;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Black Mace (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 5;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Blade of the Relentless (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 2;
                        restrictedIndexes.AddRange(new int[] { 0, 1, 3, 4, 5, 6 });
                    }
                    else if (chosenRelic == "Blade of the Relentless (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 6;
                        cmbOption2.Enabled = false;
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    //Relic = chosenRelic;
                    antiLoop = false;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        icon = true;
                    }
                    else
                    {
                        icon = false;
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Boltgun");
                        lbModelSelect.Items.Add("Legionary w/ " + Weapons[temp + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize) + 1, 1);
                    }
                    break;
                case 60:
                    if (clbPsyker.CheckedItems.Count < 1)
                    {
                        break;
                    }
                    else if (clbPsyker.CheckedItems.Count == 1)
                    {
                        PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                    }
                    else
                    {
                        clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                    }
                    break;
                case 61:
                    if(antiLoop)
                    {
                        break;
                    }

                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        restrictedIndexes.Clear();
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbStratagem5.Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Boltgun",
                            "Daemon Blade",
                            "Plasma Pistol",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        restrictedIndexes.Clear();

                        #region Champion Relics
                        if (Relic == "Hyper-Growth Bolts (Slot 1)" || Relic == "Loyalty's Reward (Slot 1)")
                        {
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Hyper-Growth Bolts (Slot 2)" || Relic == "Loyalty's Reward (Slot 2)")
                        {
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Ashen Axe (Slot 1)" || Relic == "Axe of the Forgemaster (Slot 1)")
                        {
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Ashen Axe (Slot 2)" || Relic == "Axe of the Forgemaster (Slot 2)")
                        {
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Viper's Spite" || Relic == "The Warp's Malice")
                        {
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Distortion (Slot 1)")
                        {
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Distortion (Slot 2)")
                        {
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "The Black Mace (Slot 1)")
                        {
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "The Black Mace (Slot 2)")
                        {
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Blade of the Relentless (Slot 1)")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 1, 3, 4, 5, 6 });
                        }
                        else if (Relic == "Blade of the Relentless (Slot 2)")
                        {
                            cmbOption2.Enabled = false;
                        }
                        #endregion

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Balefire Tome (+20 pts)",
                            "Boltgun",
                            "Flamer",
                            "Havoc Autocannon",
                            "Heavy Bolter",
                            "Heavy Chainaxe",
                            "Lascannon",
                            "Meltagun",
                            "Missile Launcher",
                            "Plasma Gun",
                            "Plasma Pistol",
                            "Reaper Chaincannon"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        LoadOptions(cmbOption1);
                    }

                    if (balefire)
                    {
                        clbPsyker.Visible = true;
                        lblPsyker.Visible = true;
                    }
                    else
                    {
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;
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

            if(Weapons.Contains("Balefire Tome (+20 pts)"))
            {
                Points += 20;
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Legionaries - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1)
        {
            restrictedIndexes.Clear();
            bool chainaxe = false;
            balefire = false;
            List<string> specialHeavy = new List<string>();

            for(int i = 2; i < Weapons.Count; i++)
            {
                if (Weapons[i] == "Heavy Chainaxe")
                {
                    chainaxe = true;
                }
                else if(Weapons[i] == "Balefire Tome (+20 pts)")
                {
                    balefire = true;
                }
                else if(Weapons[i] != "Boltgun" && Weapons[i] != "Astartes Chainsword")
                {
                    specialHeavy.Add(Weapons[i]);
                }
            }

            if(chainaxe && Weapons[currentIndex + 1] != "Heavy Chainaxe")
            {
                restrictedIndexes.Add(6);
            }
            if((balefire && Weapons[currentIndex + 1] != "Balefire Tome (+20 pts)") || khorne)
            {
                restrictedIndexes.Add(1);
            }
            if(specialHeavy.Count > 0 && !specialHeavy.Contains(Weapons[currentIndex + 1]))
            {
                for(int i = 0; i < specialHeavy.Count; i++)
                {
                    if (specialHeavy[i] != "Plasma Pistol")
                    {
                        restrictedIndexes.Add(cmbOption1.Items.IndexOf(specialHeavy[i]));
                    }
                }
            }

            if(specialHeavy.Count == UnitSize / 5 && !specialHeavy.Contains(Weapons[currentIndex + 1]))
            {
                restrictedIndexes.AddRange(new int[] { 3, 4, 5, 7, 8, 9, 10, 11, 12 });
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }
}
