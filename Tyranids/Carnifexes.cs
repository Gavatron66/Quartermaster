using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Carnifexes : Datasheets
    {
        int currentIndex;

        public Carnifexes()
        {
            DEFAULT_POINTS = 125;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "carnifex";
            Weapons.Add("Two Scything Talons");
            Weapons.Add("Two Scything Talons");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "MONSTER", "CORE", "CARNIFEXES"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Carnifexes();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            antiLoop = true;
            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;
            antiLoop = false;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Carnifex w/ " + Weapons[i * 9] + " and " + Weapons[(i * 9) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Stranglethorn Cannon (+10 pts)",
                "Two Crushing Claws (+10 pts)",
                "Two Deathspitters w/ Slimer Maggots (+10 pts)",
                "Two Devourers w/ Brainleech Worms (+10 pts)",
                "Two Scything Talons"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Venom Cannon (+10 pts)",
                "Two Crushing Claws (+10 pts)",
                "Two Deathspitters w/ Slimer Maggots (+10 pts)",
                "Two Devourers w/ Brainleech Worms (+10 pts)",
                "Two Scything Talons"
            });

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Bone Mace (+5 pts)",
                "Thresher Scythe (+5 pts)"
            });

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "(None)",
                "Acid Maw (+5 pts)",
                "Bio-plasma (+10 pts)",
                "Enhanced Senses (+10 pts)",
                "Tusks (+5 pts)"
            });

            cmbOption5.Items.Clear();
            cmbOption5.Items.AddRange(new string[]
            {
                "(None)",
                "Spine Banks (+5 pts)",
                "Spore Cysts (+15 pts)"
            });

            cbOption1.Text = "Adrenal Glands (+10 pts)";
            cbOption2.Text = "Chitin Thorns (+5 pts)";
            cbOption3.Text = "Toxin Sacs (+5 pts)";

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop)
            {
                return;
            }

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch(code)
            {
                case 11:
                    Weapons[currentIndex * 9] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Carnifex w/ " + Weapons[currentIndex * 9] + " and " + Weapons[(currentIndex * 9) + 1];
                    if (Weapons[(currentIndex * 9)] == "Stranglethorn Cannon (+10 pts)")
                    {
                        cmbOption2.Items.RemoveAt(0);
                    }
                    else if (!cmbOption2.Items.Contains("Heavy Venom Cannon (+10 pts)"))
                    {
                        cmbOption2.Items.Insert(0, "Heavy Venom Cannon (+10 pts)");
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 9) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Carnifex w/ " + Weapons[currentIndex * 9] + " and " + Weapons[(currentIndex * 9) + 1];
                    if (Weapons[(currentIndex * 9) + 1] == "Heavy Venom Cannon (+10 pts)")
                    {
                        cmbOption1.Items.RemoveAt(0);
                    }
                    else if (!cmbOption1.Items.Contains("Stranglethorn Cannon (+10 pts)"))
                    {
                        cmbOption1.Items.Insert(0, "Stranglethorn Cannon (+10 pts)");
                    }
                    break;
                case 13:
                    Weapons[(currentIndex * 9) + 2] = cmbOption3.SelectedItem.ToString();
                    break;
                case 14:
                    Weapons[(currentIndex * 9) + 3] = cmbOption4.SelectedItem.ToString();
                    break;
                case 16:
                    Weapons[(currentIndex * 9) + 8] = cmbFaction.SelectedItem.ToString();
                    break;
                case 18:
                    Weapons[(currentIndex * 9) + 4] = cmbOption5.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 9) + 5] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 9) + 5] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 9) + 6] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 9) + 6] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[(currentIndex * 9) + 7] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 9) + 7] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Two Scything Talons");
                            Weapons.Add("Two Scything Talons");
                            Weapons.Add("(None)");
                            Weapons.Add("(None)");
                            Weapons.Add("(None)");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("(None)");
                            lbModelSelect.Items.Add("Carnifex w/ " + Weapons[(i * 9)] + " and " + Weapons[(i * 9) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2) + 1, 9);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cmbOption3.Visible = false;
                        cmbOption4.Visible = false;
                        cmbOption5.Visible = false;
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        cmbFaction.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        panel.Controls["lblOption4"].Visible = false;
                        panel.Controls["lblOption5"].Visible = false;
                        panel.Controls["lblFactionUpgrade"].Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    cmbOption3.Visible = true;
                    cmbOption4.Visible = true;
                    cmbOption5.Visible = true;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;
                    cmbFaction.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption4"].Visible = true;
                    panel.Controls["lblOption5"].Visible = true;
                    panel.Controls["lblFactionUpgrade"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 9)]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 9) + 1]);
                    cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 9) + 2]);
                    cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[(currentIndex * 9) + 3]);
                    cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(Weapons[(currentIndex * 9) + 4]);
                    cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Weapons[(currentIndex * 9) + 8]);

                    if (Weapons[(currentIndex * 9) + 5] != "")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                    }

                    if (Weapons[(currentIndex * 9) + 6] != "")
                    {
                        cbOption2.Checked = true;
                    }
                    else
                    {
                        cbOption2.Checked = false;
                    }

                    if (Weapons[(currentIndex * 9) + 7] != "")
                    {
                        cbOption3.Checked = true;
                    }
                    else
                    {
                        cbOption3.Checked = false;
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Acid Maw (+5 pts)" || weapon == "Bone Mace (+5 pts)"
                    || weapon == "Chitin Thorns (+5 pts)" || weapon == "Spine Banks (+5 pts)"
                    || weapon == "Thresher Scythe (+5 pts)" || weapon == "Toxin Sacs (+5 pts)"
                    || weapon == "Tusks (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Adrenal Glands (+10 pts)" || weapon == "Bio-plasma (+10 pts)"
                    || weapon == "Two Crushing Claws (+10 pts)" || weapon == "Two Devourers w/ Brainleech Worms (+10 pts)"
                    || weapon == "Two Deathspitters w/ Slimer Maggots (+10 pts)" || weapon == "Enhanced Senses (+10 pts)"
                    || weapon == "Heavy Venom Cannon (+10 pts)" || weapon == "Stranglethorn Cannon (+10 pts)")
                {
                    Points += 10;
                }

                if(weapon == "Spore Cysts (+15 pts)")
                {
                    Points += 15;
                }
            }

            for(int i = 8; i < UnitSize*9; i += 9)
            {
                Points += repo.GetFactionUpgradePoints(Weapons[i]);
            }
        }

        public override string ToString()
        {
            return "Carnifexes - " + Points + "pts";
        }
    }
}
