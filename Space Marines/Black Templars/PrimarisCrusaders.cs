using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Black_Templars
{
    public class PrimarisCrusaders : Datasheets
    {
        int currentIndex = 0;
        const int neophytePts = 14;
        int numNeophytes = 4;
        int restriction = 0;

        public PrimarisCrusaders()
        {
            DEFAULT_POINTS = 17;
            UnitSize = 6;
            Points = DEFAULT_POINTS * UnitSize + numNeophytes * neophytePts;
            TemplateCode = "crusaders";
            Weapons.Add("Bolt Pistols and Astartes Chainswords");
            Weapons.Add("Heavy Bolt Pistol and Power Sword");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Auto Bolt Rifle");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "BLACK TEMPLARS",
                "INFANTRY", "CORE", "PRIMARIS", "CRUSADER SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new PrimarisCrusaders();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudUnitSize2 = panel.Controls["nudUnitSize2"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";
            panel.Controls["lblOption5"].Text = "(+" + neophytePts + " pts/model)";
            panel.Controls["lblOption5"].Location = new System.Drawing.Point(panel.Controls["lblModelPoints"].Location.X, nudUnitSize2.Location.Y);
            panel.Controls["lblOption5"].Visible = true;
            panel.Controls["lblUnitSize2"].Text = "Number of Neophytes:";
            panel.Controls["lblExtra2"].Location = new System.Drawing.Point(panel.Controls["lblNumModels"].Location.X, panel.Controls["lblNumModels"].Location.Y);
            panel.Controls["lblExtra2"].Text = "Number of Initiates:";
            panel.Controls["lblExtra2"].Visible = true;
            panel.Controls["lblNumModels"].Visible = false;

            panel.Controls["lblOption6"].Visible = true;
            panel.Controls["lblOption6"].Location = panel.Controls["lblOption1"].Location;
            panel.Controls["lblOption6"].Text = "Select one of the following for all Neophytes:";

            cmbOption1.Visible = true;
            panel.Controls["lblOption1"].Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 6;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 12;
            nudUnitSize.Value = currentSize;

            currentSize = UnitSize;
            nudUnitSize2.Minimum = 4;
            antiLoop = true;
            nudUnitSize2.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize2.Maximum = 8;
            nudUnitSize2.Value = numNeophytes;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Primaris Sword Brother w/ " + Weapons[1]);

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Primaris Initiate w/ " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[] 
            {
                "Astartes Shotguns",
                "Bolt Carbines",
                "Bolt Pistols and Astartes Chainswords"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(cmbOption2.Location.X - 40, cmbOption2.Location.Y + 60);
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

            if (!Weapons.Contains("Power Fist") && !Weapons.Contains("Pyreblaster"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 5, 6 }, cmbFaction);
            }
            else if (!Weapons.Contains("Power Fist"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 5 }, cmbFaction);
            }
            else if (!Weapons.Contains("Pyreblaster"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 6 }, cmbFaction);
            }
            else
            {
                this.DrawItemWithRestrictions(new List<int>(), cmbFaction);
            }
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
            NumericUpDown nudUnitSize2 = panel.Controls["nudUnitSize2"] as NumericUpDown;
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
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    if (!restrictedIndexes.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();

                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = "Primaris Sword Brother w/ " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Primaris Initiate w/ " + Weapons[currentIndex + 1];
                        }
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    if (!Weapons.Contains("Pyreblaster") && Factionupgrade.Contains("Beastpyre"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    if (!Weapons.Contains("Power Fist") && Factionupgrade.Contains("Fist of Balthus"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    break;
                case 16:
                    if (!Weapons.Contains("Power Fist") && cmbFaction.Text.Contains("Fist"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    else if (!Weapons.Contains("Pyreblaster") && cmbFaction.Text.Contains("Beastpyre"))
                    {
                        cmbFaction.SelectedIndex = 0;
                    }
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    restrictedIndexes.Clear();

                    if (Relic == "Witchseeker Bolts")
                    {
                        restrictedIndexes.AddRange(new int[] { 2, 3 });
                        cmbOption2.SelectedIndex = 0;
                    }
                    else if (Relic == "Sword of Judgement")
                    {
                        restrictedIndexes.AddRange(new int[] { 0, 2 });
                        cmbOption2.SelectedIndex = 1;
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Auto Bolt Rifle");
                        lbModelSelect.Items.Insert(UnitSize - 1, "Primaris Initiate w/ " + Weapons[UnitSize]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize - 1, 1);
                    }
                    break;
                case 31:
                    numNeophytes = int.Parse(nudUnitSize2.Value.ToString());
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }
                    antiLoop = true;

                    cmbOption2.Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();

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

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Heavy Bolt Pistol and Power Axe",
                            "Heavy Bolt Pistol and Power Sword",
                            "Pyre Pistol and Power Axe",
                            "Pyre Pistol and Power Sword"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);

                        if(Relic == "Witchseeker Bolts")
                        {
                            restrictedIndexes.AddRange(new int[] { 2, 3 });
                        }
                        else if (Relic == "Sword of Judgement")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 2 });
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                    }
                    else //Initiates
                    {
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbStratagem4.Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Auto Bolt Rifle",
                            "Heavy Bolt Pistol and Astartes Chainsword",
                            "Heavy Bolt Pistol and Power Fist",
                            "Pyreblaster"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);

                        if(restriction == ((UnitSize + numNeophytes) / 10) * 2)
                        {
                            restrictedIndexes.AddRange(new int[] { 2, 3 });
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
                    }

                    antiLoop = false;
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
            Points += neophytePts * numNeophytes;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            restriction = 0;
            foreach(var weapon in Weapons)
            {
                if(weapon == "Pyreblaster" || weapon == "Heavy Bolt Pistol and Power Fist")
                {
                    restriction++;
                }
            }

            if (!Weapons.Contains("Power Fist") && !Weapons.Contains("Pyreblaster"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 5, 6 }, cmbFaction);
            }
            else if (!Weapons.Contains("Power Fist"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 5 }, cmbFaction);
            }
            else if (!Weapons.Contains("Pyreblaster"))
            {
                this.DrawItemWithRestrictions(new List<int>() { 6 }, cmbFaction);
            }
            else
            {
                this.DrawItemWithRestrictions(new List<int>(), cmbFaction);
            }
        }

        public override string ToString()
        {
            return "Primaris Crusader Squad - " + Points + "pts";
        }
    }
}
