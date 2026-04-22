using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Thornbacks : Datasheets
    {
        int currentIndex;

        public Thornbacks()
        {
            DEFAULT_POINTS = 130;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m5k";
            Weapons.Add("Two Scything Talons");
            Weapons.Add("Two Devourers w/ Brainleech Worms");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "MONSTER", "CORE", "CARNIFEX", "THORNBACKS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Thornbacks();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            CheckBox cbOption5 = panel.Controls["cbOption5"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Thornback w/ " + Weapons[i * 8] + " and " + Weapons[(i * 8) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Stranglethorn Cannon (+10 pts)",
                "Two Scything Talons"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Two Deathspitters w/ Slimer Maggots",
                "Two Devourers w/ Brainleech Worms",
            });

            cbOption1.Text = "Adrenal Glands (+10 pts)";
            if (Weapons[0] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Enhanced Senses (+10 pts)";
            if (Weapons[0] == cbOption2.Text)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Spine Banks (+5 pts)";
            if (Weapons[0] == cbOption3.Text)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }

            cbOption4.Text = "Thresher Scythe (+5 pts)";
            if (Weapons[0] == cbOption4.Text)
            {
                cbOption4.Checked = true;
            }
            else
            {
                cbOption4.Checked = false;
            }

            cbOption5.Text = "Toxin Sacs (+5 pts)";
            if (Weapons[0] == cbOption5.Text)
            {
                cbOption5.Checked = true;
            }
            else
            {
                cbOption5.Checked = false;
            }

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            CheckBox cbOption5 = panel.Controls["cbOption5"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 9] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Thornback w/ " + Weapons[currentIndex * 8] + " and " + Weapons[(currentIndex * 8) + 1];
                    break;
                case 12:
                    Weapons[(currentIndex * 9) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Thornback w/ " + Weapons[currentIndex * 8] + " and " + Weapons[(currentIndex * 8) + 1];
                    break;
                case 16:
                    Weapons[(currentIndex * 8) + 7] = cmbFaction.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 8) + 2] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 8) + 2] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 8) + 3] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 8) + 3] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[(currentIndex * 8) + 4] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 8) + 4] = "";
                    }
                    break;
                case 24:
                    if (cbOption4.Checked)
                    {
                        Weapons[(currentIndex * 8) + 5] = cbOption4.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 8) + 5] = "";
                    }
                    break;
                case 26:
                    if (cbOption5.Checked)
                    {
                        Weapons[(currentIndex * 8) + 6] = cbOption5.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 8) + 6] = "";
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
                            Weapons.Add("Two Devourers w/ Brainleech Worms");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("");
                            Weapons.Add("(None)");
                            lbModelSelect.Items.Add("Thornback w/ " + Weapons[(i * 8)] + " and " + Weapons[(i * 8) + 1]);
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
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        cbOption4.Visible = false;
                        cbOption5.Visible = false;
                        cmbFaction.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblFactionUpgrade"].Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;
                    cbOption4.Visible = true;
                    cbOption5.Visible = true;
                    cmbFaction.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblFactionUpgrade"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 8)]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 8) + 1]);
                    cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Weapons[(currentIndex * 8) + 7]);

                    if (Weapons[(currentIndex * 8) + 2] != "")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                    }

                    if (Weapons[(currentIndex * 8) + 3] != "")
                    {
                        cbOption2.Checked = true;
                    }
                    else
                    {
                        cbOption2.Checked = false;
                    }

                    if (Weapons[(currentIndex * 8) + 4] != "")
                    {
                        cbOption3.Checked = true;
                    }
                    else
                    {
                        cbOption3.Checked = false;
                    }

                    if (Weapons[(currentIndex * 8) + 5] != "")
                    {
                        cbOption4.Checked = true;
                    }
                    else
                    {
                        cbOption4.Checked = false;
                    }

                    if (Weapons[(currentIndex * 8) + 6] != "")
                    {
                        cbOption5.Checked = true;
                    }
                    else
                    {
                        cbOption5.Checked = false;
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Spine Banks (+5 pts)" || weapon == "Thresher Scythe (+5 pts)" || weapon == "Toxin Sacs (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon == "Adrenal Glands (+10 pts)" || weapon == "Enhanced Senses (+10 pts)" || weapon == "Stranglethorn Cannon (+10 pts)")
                {
                    Points += 10;
                }
            }

            for (int i = 7; i < UnitSize * 8; i += 8)
            {
                Points += repo.GetFactionUpgradePoints(Weapons[i]);
            }
        }

        public override string ToString()
        {
            return "Thornbacks - " + Points + "pts";
        }
    }
}
