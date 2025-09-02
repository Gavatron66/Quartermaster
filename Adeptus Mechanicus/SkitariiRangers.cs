using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
	public class SkitariiRangers : Datasheets
	{
		int currentIndex;
        bool isLoading = false;
        int[] restrictArray = new int[] { 0, 0, 0 };
        int[] restrictArray2 = new int[] { 0, 0 };
        List<int> restrictedIndexes2 = new List<int>();
        //Arc Rifle (+5 pts)
        //Plasma Caliver (+5 pts)
        //Transuranic Arquebus (+10 pts)
        //Enhanced Data-tether (+5 pts)
        //Omnispex (+5 pts)

        public SkitariiRangers()
        {
            DEFAULT_POINTS = 9;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Galvanic Rifle");
                Weapons.Add("(None)");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "INFANTRY", "CORE", "SKITARII RANGERS"
            });
            Role = "Troops";
        }

		public override Datasheets CreateUnit()
		{
			return new SkitariiRangers();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdMech;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Skitarii Ranger Alpha with " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Skitarii Ranger Alpha with " + Weapons[0] + " and " + Weapons[1]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i * 2) + 1] == "(None)")
                {
                    lbModelSelect.Items.Add("Skitarii Ranger with " + Weapons[i*2]);
                }
                else
                {
                    lbModelSelect.Items.Add("Skitarii Ranger with " + Weapons[i*2] + " and " + Weapons[(i * 2) + 1]);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption2.Items.Clear();

            cbStratagem3.Text = repo.StratagemList[2];
            cbStratagem3.Location = new System.Drawing.Point(panel.Controls["lblOption2"].Location.X + 20, panel.Controls["cmbOption2"].Location.Y + 60);
            cbStratagem4.Text = repo.StratagemList[3];
            cbStratagem4.Location = new System.Drawing.Point(panel.Controls["lblOption2"].Location.X + 20, panel.Controls["cbStratagem3"].Location.Y + 30);

            panel.Controls["lblWarlord"].Location = new System.Drawing.Point(cbStratagem4.Location.X, cbStratagem4.Location.Y + 30);
            cmbWarlord.Location = new System.Drawing.Point(cbStratagem4.Location.X, cbStratagem4.Location.Y + 50);
            panel.Controls["lblWarlord"].Visible = false;
            cmbRelic.Visible = false;
            cbStratagem3.Visible = true;

            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cmbWarlord.Location.X, cmbWarlord.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cmbWarlord.Location.X, cmbWarlord.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
            cbStratagem4.Visible = true;

            cmbWarlord.Items.Clear();
            cmbWarlord.Items.AddRange(f.GetWarlordTraits("Skitarii").ToArray());

            if (Stratagem.Contains(cbStratagem3.Text))
            {
                cbStratagem3.Checked = true;
                cbStratagem3.Enabled = true;

                cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
                panel.Controls["lblWarlord"].Visible = true;
                cmbWarlord.Visible = true;
            }
            else
            {
                cbStratagem3.Checked = false;
                cmbWarlord.SelectedIndex = -1;
                panel.Controls["lblWarlord"].Visible = false;
                cmbWarlord.Visible = false;
            }

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            if (Stratagem.Contains(cbStratagem4.Text))
            {
                cbStratagem4.Checked = true;
                cbStratagem4.Enabled = true;

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

                panel.Controls["lblRelic"].Visible = true;
                cmbRelic.Visible = true;
            }
            else
            {
                cbStratagem4.Checked = false;
                cmbRelic.SelectedIndex = 0;
                panel.Controls["lblRelic"].Visible = false;
                cmbRelic.Visible = false;
            }

        }

		public override void SaveDatasheets(int code, Panel panel)
        {
            if (isLoading)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Skitarii Ranger Alpha with " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Skitarii Ranger Alpha with " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                        {
                            Weapons[(currentIndex * 2)] = cmbOption1.SelectedItem.ToString();
                            if (Weapons[(currentIndex * 2) + 1] == "(None)")
                            {
                                lbModelSelect.Items[currentIndex] = "Skitarii Ranger with " + Weapons[currentIndex * 2];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Skitarii Ranger with " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                            }
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2)]);
                        }
                    }

                    if (cmbOption1.SelectedIndex != 1)
                    {
                        cmbOption2.SelectedIndex = 0;
                        cmbOption2.Enabled = false;
                    }
                    else
                    {
                        cmbOption2.Enabled = true;
                    }
                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Skitarii Ranger Alpha with " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Skitarii Ranger Alpha with " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                        {
                            Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                            if (Weapons[(currentIndex * 2) + 1] == "(None)")
                            {
                                lbModelSelect.Items[currentIndex] = "Skitarii Ranger with " + Weapons[currentIndex * 2];
                            }
                            else
                            {
                                lbModelSelect.Items[currentIndex] = "Skitarii Ranger with " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                            }
                        }
                        else
                        {
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        }
                    }

                    if(cmbOption2.SelectedIndex != 0)
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }

                    break;
                case 15:
                    if (cmbWarlord.SelectedIndex != -1)
                    {
                        WarlordTrait = cmbWarlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }

                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Galvanic Rifle");
                        Weapons.Add("(None)");
                        lbModelSelect.Items.Add("Skitarii Ranger with " + Weapons[temp * 2]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize * 2, 2);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0 && !isLoading)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    isLoading = true;

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;

                    if (currentIndex == 0)
                    {
                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Arc Pistol",
                            "Galvanic Rifle",
                            "Phosphor Blast Pistol",
                            "Radium Pistol"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Arc Maul",
                            "Power Sword",
                            "Taser Goad"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        isLoading = false;
                        break;
                    }

                    cmbOption1.Items.Clear();
                    cmbOption1.Items.AddRange(new string[]
                    {
                        "Arc Rifle (+5 pts)",
                        "Galvanic Rifle",
                        "Plasma Caliver (+5 pts)",
                        "Transuranic Arquebus (+10 pts)"
                    });
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);

                    cmbOption2.Items.Clear();
                    cmbOption2.Items.AddRange(new string[]
                    {
                        "(None)",
                        "Enhanced Data-tether (+5 pts)",
                        "Omnispex (+5 pts)"
                    });
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    if (Weapons[(currentIndex * 2) + 1] != "(None)")
                    {
                        cmbOption1.Enabled = false;
                    }

                    if (Weapons[(currentIndex * 2)] != "Galvanic Rifle")
                    {
                        cmbOption2.Enabled = false;
                    }

                    restrictedIndexes.Clear();
                    restrictedIndexes2.Clear();

                    if (UnitSize < 10 && (restrictArray[0] + restrictArray[1] + restrictArray[2] == 1) &&
                        !(Weapons[currentIndex * 2] == "Arc Rifle (+5 pts)" || Weapons[currentIndex * 2] == "Plasma Caliver (+5 pts)" || Weapons[currentIndex * 2] == "Transuranic Arquebus (+10 pts)"))
                    {
                        if (Weapons[currentIndex * 2] == "Galvanic Rifle")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 2, 3 });
                        }
                    }
                    else if (UnitSize >= 10)
                    {
                        if (restrictArray[0] == UnitSize/10 && Weapons[currentIndex * 2] != "Arc Rifle (+5 pts)")
                        {
                            restrictedIndexes.Add(0);
                        }

                        if (restrictArray[1] == UnitSize / 10 && Weapons[currentIndex * 2] != "Plasma Caliver (+5 pts)")
                        {
                            restrictedIndexes.Add(2);
                        }

                        if (restrictArray[2] == UnitSize / 10 && Weapons[currentIndex * 2] != "Transuranic Arquebus (+10 pts)")
                        {
                            restrictedIndexes.Add(3);
                        }
                    }

                    if(UnitSize != 20 && (restrictArray2[0] + restrictArray2[1] == 1))
                    {
                        if (Weapons[(currentIndex * 2) + 1] == "(None)")
                        {
                            cmbOption2.SelectedIndex = 0;
                            cmbOption2.Enabled = false;
                        }
                    }
                    else if (UnitSize == 20 && (restrictArray2[0] + restrictArray2[1] == 2))
                    {
                        if (Weapons[(currentIndex * 2) + 1] == "(None)")
                        {
                            cmbOption2.SelectedIndex = 0;
                            cmbOption2.Enabled = false;
                        }
                    }
                    else
                    {
                        if(Weapons[(currentIndex * 2) + 1] != "Enhanced Data-tether (+5 pts)" && restrictArray2[0] == 1)
                        {
                            restrictedIndexes2.Add(1);
                        }
                        if (Weapons[(currentIndex * 2) + 1] != "Omnispex (+5 pts)" && restrictArray2[1] == 1)
                        {
                            restrictedIndexes2.Add(2);
                        }
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
                    isLoading = false;
                    break;
                case 73:
                    if (cbStratagem3.Checked)
                    {
                        Stratagem.Add(cbStratagem3.Text);
                        panel.Controls["lblWarlord"].Visible = true;
                        cmbWarlord.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Remove(cbStratagem3.Text);
                        }
                        cmbWarlord.Visible = false;
                        panel.Controls["lblWarlord"].Visible = false;
                        cmbWarlord.SelectedIndex = -1;
                    }
                    break;
                case 74:
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
                        }
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.SelectedIndex = 0;
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            restrictArray = new int[3];
            restrictArray2 = new int[2];

            foreach (var weapon in Weapons)
            {
                if(weapon == "Arc Rifle (+5 pts)")
                {
                    restrictArray[0] += 1;
                    Points += 5;
                }

                if(weapon == "Plasma Caliver (+5 pts)")
                {
                    restrictArray[1] += 1;
                    Points += 5;
                }

                if(weapon == "Transuranic Arquebus (+10 pts)")
                {
                    restrictArray[2] += 1;
                    Points += 10;
                }

                if(weapon == "Enhanced Data-tether (+5 pts)")
                {
                    restrictArray2[0] += 1;
                    Points += 5;
                }

                if(weapon == "Omnispex (+5 pts)")
                {
                    restrictArray2[1] += 1;
                    Points += 5;
                }
            }
        }

		public override string ToString()
		{
			return "Skitarii Rangers - " + Points + "pts";
		}
	}
}
