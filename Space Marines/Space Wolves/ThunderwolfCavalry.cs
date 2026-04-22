using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class ThunderwolfCavalry : Datasheets
	{
		int currentIndex;
        List<int> restrictedIndexes2 = new List<int>();

        public ThunderwolfCavalry()
		{
			DEFAULT_POINTS = 35;
			UnitSize = 3;
			Points = UnitSize * DEFAULT_POINTS;
			TemplateCode = "NL2m";
			Weapons.Add("Bolt Pistol");
			Weapons.Add("Astartes Chainsword");
			for (int i = 1; i < UnitSize; i++)
			{
				Weapons.Add("Bolt Pistol");
				Weapons.Add("Astartes Chainsword");
			}
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"CAVALRY", "CORE", "WOLF GUARD", "THUNDERWOLF CAVALRY"
			});
			Role = "Fast Attack";
		}

		public override Datasheets CreateUnit()
		{
			return new ThunderwolfCavalry();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as SpaceMarines;
			Template.LoadTemplate(TemplateCode, panel);

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
			nudUnitSize.Minimum = 3;
			nudUnitSize.Value = nudUnitSize.Minimum;
			nudUnitSize.Maximum = 6;
			nudUnitSize.Value = currentSize;

			lbModelSelect.Items.Clear();
			lbModelSelect.Items.Add("Thunderwolf Cavalry Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
			for (int i = 1; i < UnitSize; i++)
			{
				lbModelSelect.Items.Add("Thunderwolf Cavalry w/ " + Weapons[(i * 2)] + " and " + Weapons[(i * 2) + 1]);
			}

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Astartes Chainsword",
				"Bolt Pistol",
				"Boltgun",
				"Lightning Claw",
				"Plasma Pistol",
				"Power Axe",
				"Power Fist (+5 pts)",
				"Power Maul",
				"Power Sword",
				"Thunder Hammer (+10 pts)"
			});

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Astartes Chainsword",
				"Lightning Claw",
				"Power Axe",
				"Power Fist (+5 pts)",
				"Power Maul",
				"Power Sword",
				"Storm Shield",
				"Thunder Hammer (+10 pts)"
			});

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
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
			{
				case 11:
					if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
					{
						Weapons[(currentIndex * 2)] = cmbOption1.SelectedItem.ToString();

						if (currentIndex == 0)
						{
							lbModelSelect.Items[0] = "Thunderwolf Cavalry Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
						}
						else
						{
							lbModelSelect.Items[currentIndex] = "Thunderwolf Cavalry w/ " + Weapons[(currentIndex * 2)]
								+ " and " + Weapons[(currentIndex * 2) + 1];
						}
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
						if (currentIndex == 0)
						{
							lbModelSelect.Items[0] = "Thunderwolf Cavalry Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
						}
						else
						{
							lbModelSelect.Items[currentIndex] = "Thunderwolf Cavalry w/ " + Weapons[(currentIndex * 2)]
								+ " and " + Weapons[(currentIndex * 2) + 1];
						}
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();
                    restrictedIndexes2.Clear();

                    if (chosenRelic == "Frost Weapon")
                    {
                        cmbOption2.SelectedIndex = 2;
                        restrictedIndexes2.AddRange(new int[] { 0, 3, 4, 6, 7 });
                    }
                    else if (chosenRelic == "Morkai's Teeth Bolts")
                    {
                        cmbOption1.SelectedIndex = 1;
						restrictedIndexes.AddRange(new int[] { 0, 3, 4, 5, 6, 7, 8, 9 });
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
                    Relic = chosenRelic;
                    break;
                case 30:
					int temp = UnitSize;
					UnitSize = int.Parse(nudUnitSize.Value.ToString());

					if (temp < UnitSize)
					{
						for (int i = temp; i < UnitSize; i++)
						{
							Weapons.Add("Bolt Pistol");
							Weapons.Add("Astartes Chainsword");
							lbModelSelect.Items.Add("Thunderwolf Cavalry w/ " + Weapons[(i * 2)]
								+ " and " + Weapons[(i * 2) + 1]);
						}
					}

					if (temp > UnitSize)
					{
						lbModelSelect.Items.RemoveAt(temp - 1);
						Weapons.RemoveRange((currentIndex * 2) + 1, 2);
					}
					break;
				case 61:
					currentIndex = lbModelSelect.SelectedIndex;
					antiLoop = true;

					if (currentIndex < 0)
					{
						cmbOption1.Visible = false;
						cmbOption2.Visible = false;
						panel.Controls["lblOption1"].Visible = false;
						panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        antiLoop = false;
					}
					else
					{
						cmbOption1.Visible = true;
						cmbOption2.Visible = true;
						panel.Controls["lblOption1"].Visible = true;
						panel.Controls["lblOption2"].Visible = true;
						cmbOption1.Enabled = true;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        if (currentIndex == 0)
                        {
                            cbStratagem5.Visible = true;

                            if (Stratagem.Contains(cbStratagem5.Text))
                            {
                                panel.Controls["lblRelic"].Visible = true;
                                cmbRelic.Visible = true;
                            }

                            if (Relic == "Frost Weapon")
                            {
                                cmbOption2.SelectedIndex = 2;
                                restrictedIndexes2.AddRange(new int[] { 0, 3, 4, 6, 7 });
                            }
                            else if (Relic == "Morkai's Teeth Bolts")
                            {
                                cmbOption1.SelectedIndex = 1;
                                restrictedIndexes.AddRange(new int[] { 0, 3, 4, 5, 6, 7, 8, 9 });
                            }
                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2)]);
						cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

						antiLoop = false;
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
                default: break;
			}

			Points = DEFAULT_POINTS * UnitSize;

			foreach (var weapon in Weapons)
			{
				if(weapon == "Power Fist (+5 pts)")
				{
					Points += 5;
				}

				if(weapon == "Thunder Hammer (+10 pts)")
				{
					Points += 10;
				}
			}
		}

		public override string ToString()
		{
			return "Thunderwolf Cavalry - " + Points + "pts";
		}
	}
}