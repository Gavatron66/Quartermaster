using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class Retributors : Datasheets
    {
        int currentIndex = 0;
        int stdIndex = -1;
        int[] restrictArray = new int[] { 0, 0 };
        List<int> restrictedIndexes2 = new List<int>();

        public Retributors()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m3k";
            Weapons.Add("Boltgun and Bolt Pistol");
            Weapons.Add("(None)");
            Weapons.Add(""); //Incensor Cherub (+5 pts)
            Weapons.Add(""); //Incensor Cherub (+5 pts)
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS", "<ORDER>",
                "INFANTRY", "CORE", "RETRIBUTOR SQUAD"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Retributors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdeptaSororitas;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";
            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 60);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 60);
            cbOption3.Location = new System.Drawing.Point(cbOption3.Location.X, cbOption3.Location.Y + 60);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Retributor Superior with " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Retributor Superior with " + Weapons[0] + " and " + Weapons[1]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Retributor with " + Weapons[i + 3]);
            }

            cmbOption1.Items.Clear();
            cmbOption2.Items.Clear();

            cbOption1.Text = "Incensor Cherub (+5 pts)";
            cbOption2.Text = cbOption1.Text;
            cbOption3.Text = "Simulacrum Imperialis (+5 pts)";

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["cbOption2"].Location.X, panel.Controls["cbOption2"].Location.Y + 30);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            antiLoop = true;
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
            antiLoop = false;
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
                            if (Weapons[1] == "(None)")
                            {
                                lbModelSelect.Items[0] = "Retributor Superior w/ " + Weapons[0];
                            }
                            else
                            {
                                lbModelSelect.Items[0] = "Retributor Superior w/ " + Weapons[0] + " and " + Weapons[1];
                            }
                        }
                        else
                        {
                            Weapons[currentIndex + 3] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Retributor with " + Weapons[currentIndex + 3];
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
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 3]);
                        }
                    }
                    break;
                case 12:
                    if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Retributor Superior w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Retributor Superior w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (chosenRelic == "The Ecclesiarch's Fury")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Redemption")
                    {
                        cmbOption1.SelectedIndex = 7;
                        cmbOption1.Enabled = false;
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[3] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[currentIndex + 3] = cbOption3.Text;
                        stdIndex = currentIndex;
                        lbModelSelect.Items[currentIndex] = "Retributor with " + Weapons[currentIndex + 3];
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        Weapons[currentIndex + 3] = "Boltgun";
                        stdIndex = -1;
                        lbModelSelect.Items[currentIndex] = "Retributor with " + Weapons[currentIndex + 3];
                        cmbOption1.Enabled = true;
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Boltgun");
                        lbModelSelect.Items.Add("Retributor with " + Weapons[temp + 3]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize + 3, 1);
                    }

                    break;
                case 61:
                    antiLoop = true;
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0 && !antiLoop)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
                    }
                    antiLoop = true;

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbOption1.Visible = true;
                        cbOption2.Visible = true;
                        cbOption3.Visible = false;

                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Boltgun and Bolt Pistol",
                            "Combi-melta and Bolt Pistol (+10 pts)",
                            "Combi-plasma and Bolt Pistol (+10 pts)",
                            "Condemnor Boltgun and Bolt Pistol (+10 pts)",
                            "Inferno Pistol (+5 pts)",
                            "Ministorum Combi-flamer and Bolt Pistol (+10 pts)",
                            "Ministorum Hand Flamer (+5 pts)",
                            "Plasma Pistol (+5 pts)"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Chainsword",
                            "Power Maul (+5 pts)",
                            "Power Sword (+5 pts)"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        if (Relic == "The Ecclesiarch's Fury")
                        {
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Redemption")
                        {
                            cmbOption1.Enabled = false;
                        }

                        this.DrawItemWithRestrictions(new List<int>(), cmbOption1);
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption1.Enabled = true;
                    cmbOption2.Visible = false;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = false;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;
                    cbStratagem5.Visible = false;
                    cmbRelic.Visible = false;
                    panel.Controls["lblRelic"].Visible = false;

                    cmbOption1.Items.Clear();
                    cmbOption1.Items.AddRange(new string[]
                    {
                        "Boltgun",
                        "Heavy Bolter (+10 pts)", //h
                        "Ministorum Heavy Flamer (+10 pts)", //h
                        "Multi-melta (+20 pts)" //h
                    });
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 3]);
                    if (Weapons[currentIndex + 3] == cbOption2.Text)
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Boltgun");
                    }

                    restrictedIndexes.Clear();
                    if (restrictArray[0] == 4 && Weapons[currentIndex + 3] == "Boltgun")
                    {
                        restrictedIndexes.AddRange(new int[] { 1, 2, 3 });
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                    if ((Weapons[currentIndex + 3] == "Boltgun" || Weapons[currentIndex + 3] == cbOption3.Text) && (stdIndex == -1 || stdIndex == currentIndex))
                    {
                        cbOption3.Enabled = true;
                    }
                    else
                    {
                        cbOption3.Enabled = false;
                    }

                    if (stdIndex == currentIndex)
                    {
                        cmbOption1.Enabled = false;
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

            restrictArray[0] = 0;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Heavy Bolter (+10 pts)" || weapon == "Multi-melta (+20 pts)" || weapon == "Ministorum Heavy Flamer (+10 pts)")
                {
                    restrictArray[0]++;
                }

                if (weapon == "Incensor Cherub (+5 pts)" || weapon == "Inferno Pistol (+5 pts)"
                    || weapon == "Ministorum Flamer (+5 pts)" || weapon == "Ministorum Hand Flamer (+5 pts)" || weapon == "Plasma Pistol (+5 pts)"
                    || weapon == "Power Maul (+5 pts)" || weapon == "Power Sword (+5 pts)" || weapon == "Simulacrum Imperialis (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon.Contains("Combi-melta") || weapon.Contains("Combi-plasma") || weapon.Contains("Condemnor Boltgun")
                    || weapon == "Heavy Bolter (+10 pts)" || weapon.Contains("Ministorum Combi-flamer"))
                {
                    Points += 10;
                }

                if (weapon == "Multi-melta (+20 pts)")
                {
                    Points += 20;
                }
            }
        }

        public override string ToString()
        {
            return "Retributor Squad - " + Points + "pts";
        }
    }
}
