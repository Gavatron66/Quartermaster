using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class PlagueMarinesCSM : Datasheets
    {
        int currentIndex;

        public PlagueMarinesCSM()
        {
            DEFAULT_POINTS = 19;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m3k";

            Weapons.Add("Boltgun");
            Weapons.Add("Plague Knife");
            Weapons.Add("");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }

            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "INFANTRY", "CORE", "MARK OF CHAOS", "PLAGUE MARINES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new PlagueMarinesCSM();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

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
                lbModelSelect.Items.Add("Plague Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            }
            else
            {
                lbModelSelect.Items.Add("Plague Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and " + Weapons[2]);
            }

            for (int i = 3; i < Weapons.Count; i++)
            {
                lbModelSelect.Items.Add("Plague Marine w/ " + Weapons[i]);
            }

            cbOption1.Text = "Icon of Despair";
            cbOption2.Text = "Sigil of Decay";

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(cbOption3.Location.X, cbOption3.Location.Y + 30);
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
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[0] = cmbOption1.SelectedItem.ToString();
                            if (Weapons[2] == "")
                            {
                                lbModelSelect.Items[currentIndex] = ("Plague Champion w/ " + Weapons[0] + " and " + Weapons[1]);
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = ("Plague Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and " + Weapons[2]);
                            }
                            break;
                        }
                        else
                        {
                            Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Plague Marine w/ " + Weapons[currentIndex + 2];
                        }
                    }
                    else
                    {
                        if (currentIndex == 0)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        }
                        else 
                        { 
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]); 
                        }
                    }
                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion w/ " + Weapons[0] + " and " + Weapons[1]);
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and " + Weapons[2]);
                        }
                        break;
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    cmbOption1.Enabled = true;

                    if (chosenRelic == "Hyper-Growth Bolts" || chosenRelic == "Loyalty's Reward")
                    {
                        cmbOption1.SelectedIndex = 0;
                        restrictedIndexes.AddRange(new int[] { 2, 3 });
                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else if (chosenRelic == "Viper's Spite" || chosenRelic == "The Warp's Malice")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }

                    antiLoop = false;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[currentIndex + 2] = cbOption1.Text;
                        cbOption2.Enabled = false;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        Weapons[currentIndex + 2] = "Boltgun";
                        cbOption2.Enabled = true;
                        cmbOption1.Enabled = true;
                    }
                    lbModelSelect.Items[currentIndex] = "Plague Marine w/ " + Weapons[currentIndex + 2];
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[currentIndex + 2] = cbOption2.Text;
                        cbOption1.Enabled = false;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        Weapons[currentIndex + 2] = "Boltgun";
                        cbOption1.Enabled = true;
                        cmbOption1.Enabled = true;
                    }
                    lbModelSelect.Items[currentIndex] = "Plague Marine w/ " + Weapons[currentIndex + 2];
                    break;
                case 23:
                    if (currentIndex == 0)
                    {
                        if (cbOption3.Checked)
                        {
                            Weapons[2] = cbOption3.Text;
                        }
                        else
                        {
                            Weapons[2] = "";
                        }

                        if (Weapons[2] == "")
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion w/ " + Weapons[0] + " and " + Weapons[1]);
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = ("Plague Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and " + Weapons[2]);
                        }
                        break;
                    }
                    break;
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = Weapons.Count - 2;

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Boltgun");
                            lbModelSelect.Items.Add("Plague Marine w/ " + Weapons[i + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        for (int i = temp; i > UnitSize; i--)
                        {
                            lbModelSelect.Items.RemoveAt(i - 1);
                            Weapons.RemoveAt(i - 1);
                        }
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    if (currentIndex == -1)
                    {
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        LoadChampion(panel, true);
                        break;
                    }

                    LoadChampion(panel, false);
                    break;
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        if (!Stratagem.Contains(cbStratagem5.Text))
                        {
                            Stratagem.Add(cbStratagem5.Text);
                        }
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

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Plague Marines - " + Points + " pts";
        }

        private void LoadChampion(Panel panel, bool isChampion)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            panel.Controls["lblOption1"].Visible = true;
            cmbOption1.Visible = true;
            restrictedIndexes = new List<int>();

            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            if (isChampion)
            {
                panel.Controls["lblOption2"].Visible = true;
                cmbOption2.Visible = true;
                cbOption1.Visible = false;
                cbOption2.Visible = false;
                cbOption3.Visible = true;
                cmbOption1.Enabled = true;
                cbStratagem5.Visible = true;

                if (Stratagem.Contains(cbStratagem5.Text))
                {
                    panel.Controls["lblRelic"].Visible = true;
                    cmbRelic.Visible = true;
                }

                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Boltgun",
                    "Bolt Pistol",
                    "Plasma Gun",
                    "Plasma Pistol"
                });
                antiLoop = true;
                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                cmbOption2.Items.Clear();
                cmbOption2.Items.AddRange(new string[]
                {
                    "Plague Knife",
                    "Daemonic Plague Blade"
                });
                cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                antiLoop = false;

                cbOption3.Text = "Power Fist";
                if (Weapons[2] == "Power Fist")
                {
                    cbOption3.Checked = true;
                }
                else
                {
                    cbOption3.Checked = false;
                }

                if(Relic == "Hyper-Growth Bolts" || Relic == "Loyalty's Reward")
                {
                    restrictedIndexes.AddRange(new int[] { 2, 3 });
                }
                else if (Relic == "Viper's Spite" || Relic == "The Warp's Malice")
                {
                    cmbOption1.Enabled = false;
                }
            }
            if (!isChampion)
            {
                panel.Controls["lblOption2"].Visible = false;
                cmbOption2.Visible = false;
                cbOption1.Visible = true;
                cbOption2.Visible = true;
                cbOption3.Visible = false;
                cbStratagem5.Visible = false;

                if (Weapons[currentIndex + 2] == "Icon of Despair")
                {
                    cmbOption1.Enabled = false;
                    cbOption2.Enabled = false;
                    cbOption2.Checked = false;

                    cbOption1.Enabled = true;
                    cbOption1.Checked = true;
                }
                else if (Weapons[currentIndex + 2] == "Sigil of Decay")
                {
                    cmbOption1.Enabled = false;
                    cbOption1.Enabled = false;
                    cbOption1.Checked = false;

                    cbOption2.Enabled = true;
                    cbOption2.Checked = true;
                }
                else
                {
                    cmbOption1.Enabled = true;
                    cbOption1.Enabled = true;
                    cbOption2.Enabled = true;

                    cbOption1.Checked = false;
                    cbOption2.Checked = false;
                }

                if (Weapons.Contains("Icon of Despair"))
                {
                    if (Weapons[currentIndex + 2] != "Icon of Despair")
                    {
                        cbOption1.Enabled = false;
                    }
                    else
                    {
                        cbOption1.Enabled = true;
                    }
                }

                if (Weapons.Contains("Sigil of Decay"))
                {
                    if (Weapons[currentIndex + 2] != "Sigil of Decay")
                    {
                        cbOption2.Enabled = false;
                    }
                    else
                    {
                        cbOption2.Enabled = true;
                    }
                }

                cmbOption1.Items.Clear();
                cmbOption1.Items.AddRange(new string[]
                {
                    "Blight Launcher",
                    "Boltgun",
                    "Bubotic Axe",
                    "Flail of Corruption",
                    "Great Plague Cleaver",
                    "Mace of Contagion and Bubotic Axe",
                    "Meltagun",
                    "Plague Belcher",
                    "Plague Knife",
                    "Plague Spewer",
                    "Plasma Gun"
                });
                antiLoop = true;
                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);
                antiLoop = false;

                int[] constraintArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                //Blight Launcher [0], Plague Spewer [9], Meltagun/Plague Belcher/Plasma Gun [6, 7, 10], Plague Knife [8],
                //Bubotic Axe [2], Mace of Contagion [5], Flail of Corruption [3], Great Plague Cleaver [4]

                foreach (string Weapon in Weapons)
                {
                    if (Weapon == "Blight Launcher")
                    {
                        constraintArray[0] += 1;
                    }

                    if (Weapon == "Plague Spewer")
                    {
                        constraintArray[1] += 1;
                    }

                    if (Weapon == "Meltagun" || Weapon == "Plague Belcher" || Weapon == "Plasma Gun")
                    {
                        constraintArray[2] += 1;
                    }

                    if (Weapon == "Plague Knife")
                    {
                        constraintArray[3] += 1;
                    }

                    if (Weapon == "Bubotic Axe")
                    {
                        constraintArray[4] += 1;
                    }

                    if (Weapon == "Mace of Contagion and Bubotic Axe")
                    {
                        constraintArray[5] += 1;
                    }

                    if (Weapon == "Flail of Corruption")
                    {
                        constraintArray[6] += 1;
                    }

                    if (Weapon == "Great Plague Cleaver")
                    {
                        constraintArray[7] += 1;
                    }
                }

                if (Weapons[0] == "Plasma Gun")
                {
                    constraintArray[2] -= 1;
                }
                if (Weapons[1] == "Plague Knife")
                {
                    constraintArray[3] -= 1;
                }

                if ((UnitSize == 10 && constraintArray[0] + constraintArray[1] == 3) || (UnitSize < 10 && constraintArray[0] == 1) ||
                    (UnitSize == 10 && constraintArray[0] == 2))
                {
                    restrictedIndexes.Add(0);
                }

                if ((UnitSize == 10 && constraintArray[0] + constraintArray[1] == 3) || (UnitSize < 10 && constraintArray[1] == 1) ||
                    (UnitSize == 10 && constraintArray[1] == 2))
                {
                    restrictedIndexes.Add(9);
                }

                if (constraintArray[2] == UnitSize / 5)
                {
                    restrictedIndexes.AddRange(new int[] { 6, 7, 10 });
                }

                if (constraintArray[3] == UnitSize / 5)
                {
                    restrictedIndexes.Add(8);
                }

                if (constraintArray[4] == UnitSize / 5)
                {
                    restrictedIndexes.Add(2);
                }

                if (constraintArray[5] == UnitSize / 5)
                {
                    restrictedIndexes.Add(5);
                }

                if (constraintArray[6] == UnitSize / 5)
                {
                    restrictedIndexes.Add(3);
                }

                if (constraintArray[7] == UnitSize / 5)
                {
                    restrictedIndexes.Add(4);
                }
            }

            if (restrictedIndexes.Contains(cmbOption1.Items.IndexOf(Weapons[currentIndex + 2])))
            {
                if (Weapons[currentIndex + 2] == "Meltagun" || Weapons[currentIndex + 2] == "Plague Belcher"
                    || Weapons[currentIndex + 2] == "Plasma Gun")
                {
                    restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Meltagun"));
                    restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Plague Belcher"));
                    restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Plasma Gun"));
                }
                else if ((Weapons[currentIndex + 2] == "Blight Launcher" || Weapons[currentIndex + 2] == "Plague Spewer")
                    && UnitSize == 10)
                {
                    restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Blight Launcher"));
                    restrictedIndexes.Remove(cmbOption1.Items.IndexOf("Plague Spewer"));
                }
                else
                {
                    restrictedIndexes.Remove(cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]));
                }
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }
}
