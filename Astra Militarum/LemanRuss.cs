using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class LemanRuss : Datasheets
    {
        int currentIndex;

        public LemanRuss()
        {
            DEFAULT_POINTS = 155;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "carnifex";
            Weapons.Add("Battle Cannon");
            Weapons.Add("Lascannon");
            Weapons.Add("(None)");
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("(None)"); //Tank Ace Upgrade
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "VEHICLE", "BATTLE TANK", "SQUADRON", "REGIMENTAL", "SMOKE", "LEMAN RUSS BATTLE TANK"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new LemanRuss();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
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
                lbModelSelect.Items.Add("Leman Russ w/ " + Weapons[i * 8] + " - " + CalcPoints(i) + "pts");
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Battle Cannon",
                "Demolisher Battle Cannon",
                "Eradicator Nova Cannon",
                "Executioner Plasma Cannon",
                "Exterminator Autocannon",
                "Punisher Gatling Cannon",
                "Vanquisher Battle Cannon"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Bolter",
                "Heavy Flamer",
                "Lascannon"
            });

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Two Heavy Bolters (+10 pts)",
                "Two Heavy Flamers (+10 pts)",
                "Two Militarum Plasma Cannons (+20 pts)",
                "Two Multi-meltas (+30 pts)"
            });

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "(None)",
                "Heavy Stubber (+5 pts)",
                "Storm Bolter (+5 pts)"
            });

            cbOption1.Text = "Armoured Tracks (+5 pts)";
            cbOption2.Text = "Dozer Blade";
            cbOption3.Text = "Hunter-killer Missile (+5 pts)";

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
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
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 8] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Leman Russ w/ " + Weapons[currentIndex * 8] + " - " + CalcPoints(currentIndex) + "pts";
                    break;
                case 12:
                    Weapons[(currentIndex * 8) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Leman Russ w/ " + Weapons[currentIndex * 8] + " - " + CalcPoints(currentIndex) + "pts";
                    break;
                case 13:
                    Weapons[(currentIndex * 8) + 2] = cmbOption3.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Leman Russ w/ " + Weapons[currentIndex * 8] + " - " + CalcPoints(currentIndex) + "pts";
                    break;
                case 14:
                    Weapons[(currentIndex * 8) + 3] = cmbOption4.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Leman Russ w/ " + Weapons[currentIndex * 8] + " - " + CalcPoints(currentIndex) + "pts";
                    break;
                case 16:
                    Weapons[(currentIndex * 8) + 7] = cmbFaction.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Leman Russ w/ " + Weapons[currentIndex * 8] + " - " + CalcPoints(currentIndex) + "pts";
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 8) + 4] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 8) + 4] = "";
                    }
                    lbModelSelect.Items[currentIndex] = "Leman Russ w/ " + Weapons[currentIndex * 8] + " - " + CalcPoints(currentIndex) + "pts";
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 8) + 5] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 8) + 5] = "";
                    }
                    lbModelSelect.Items[currentIndex] = "Leman Russ w/ " + Weapons[currentIndex * 8] + " - " + CalcPoints(currentIndex) + "pts";
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[(currentIndex * 8) + 6] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 8) + 6] = "";
                    }
                    lbModelSelect.Items[currentIndex] = "Leman Russ w/ " + Weapons[currentIndex * 8] + " - " + CalcPoints(currentIndex) + "pts";
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Battle Cannon");
                            Weapons.Add("Lascannon");
                            Weapons.Add("(None)");
                            Weapons.Add("(None)");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("(None)");
                            lbModelSelect.Items.Add("Leman Russ w/ " + Weapons[(i * 8)] + " - " + CalcPoints(temp) + "pts");
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2) + 1, 8);
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
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        cmbFaction.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        panel.Controls["lblOption4"].Visible = false;
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
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;
                    cmbFaction.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption4"].Visible = true;
                    panel.Controls["lblFactionUpgrade"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 8)]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 8) + 1]);
                    cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 8) + 2]);
                    cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[(currentIndex * 8) + 3]);
                    cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Weapons[(currentIndex * 8) + 7]);

                    if (Weapons[(currentIndex * 8) + 4] != "")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                    }

                    if (Weapons[(currentIndex * 8) + 5] != "")
                    {
                        cbOption2.Checked = true;
                    }
                    else
                    {
                        cbOption2.Checked = false;
                    }

                    if (Weapons[(currentIndex * 8) + 6] != "")
                    {
                        cbOption3.Checked = true;
                    }
                    else
                    {
                        cbOption3.Checked = false;
                    }
                    break;
            }

            Points = (DEFAULT_POINTS * UnitSize) + CalcPoints(-1);
        }

        public override string ToString()
        {
            return "Leman Russ Battle Tanks - " + Points + "pts";
        }

        private int CalcPoints(int index)
        {
            int points = 0;

            if (index <= -1)
            {
                for (int i = 0; i < UnitSize * 8; i++)
                {
                    if (Weapons[i] == "Armoured Tracks (+5 pts)" || Weapons[i] == "Heavy Stubber (+5 pts)" || Weapons[i] == "Hunter-killer Missile (+5 pts)"
                        || Weapons[i] == "Storm Bolter (+5 pts)")
                    {
                        points += 5;
                    }

                    if (Weapons[i] == "Two Heavy Bolters (+10 pts)" || Weapons[i] == "Two Heavy Flamers (+10 pts)")
                    {
                        points += 10;
                    }

                    if (Weapons[i] == "Two Militarum Plasma Cannons (+20 pts)")
                    {
                        points += 20;
                    }

                    if (Weapons[i] == "Two Multi-meltas (+30 pts)")
                    {
                        points += 30;
                    }
                }

                for(int i = 7; i < Weapons.Count; i += 7)
                {
                    if (Weapons[i] != "(None)")
                    {
                        points += repo.GetFactionUpgradePoints(Weapons[i]);
                    }
                }

                return points;
            }

            for(int i = index * 8; i < 8 + (8 * index); i++)
            {
                if (Weapons[i] == "Armoured Tracks (+5 pts)" || Weapons[i] == "Heavy Stubber (+5 pts)" || Weapons[i] == "Hunter-killer Missile (+5 pts)"
                    || Weapons[i] == "Storm Bolter (+5 pts)")
                {
                    points += 5;
                }

                if(Weapons[i] == "Two Heavy Bolters (+10 pts)" || Weapons[i] == "Two Heavy Flamers (+10 pts)")
                {
                    points += 10;
                }

                if (Weapons[i] == "Two Militarum Plasma Cannons (+20 pts)")
                {
                    points += 20;
                }

                if (Weapons[i] == "Two Multi-meltas (+30 pts)")
                {
                    points += 30;
                }
            }

            if (Weapons[7] != "(None)")
            {
                points += repo.GetFactionUpgradePoints(Weapons[7]);
            }

            return points + DEFAULT_POINTS;
        }
    }
}
