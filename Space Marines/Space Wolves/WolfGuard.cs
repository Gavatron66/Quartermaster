using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class WolfGuard : Datasheets
	{
		int currentIndex = 0;
        List<int> restrictedIndexes2 = new List<int>();

        public WolfGuard()
		{
			DEFAULT_POINTS = 20;
			UnitSize = 5;
			Points = DEFAULT_POINTS * UnitSize;
			TemplateCode = "NL2m1k";
			Weapons.Add(""); //Jump Packs
			for (int i = 0; i < UnitSize; i++)
			{
				Weapons.Add("Boltgun");
				Weapons.Add("Bolt Pistol");
			}
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "CORE", "WOLF GUARD"
			});
			Role = "Elites";
		}

		public override Datasheets CreateUnit()
		{
			return new WolfGuard();
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
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            //cbOption1.Location = new System.Drawing.Point(243, 208);

            int currentSize = UnitSize;
			nudUnitSize.Minimum = 5;
			antiLoop = true;
			nudUnitSize.Value = nudUnitSize.Minimum;
			antiLoop = false;
			nudUnitSize.Maximum = 10;
			nudUnitSize.Value = currentSize;

			lbModelSelect.Items.Clear();
			lbModelSelect.Items.Add("Wolf Guard Pack Leader w/ " + Weapons[1] + " and " + Weapons[2]);

			for (int i = 1; i < UnitSize; i++)
			{
				lbModelSelect.Items.Add("Wolf Guard w/ " + Weapons[(i * 2) + 1] + " and " + Weapons[(i * 2) + 2]);
			}

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Astartes Chainsword",
				"Boltgun",
				"Combi-flamer",
				"Combi-grav",
				"Combi-melta",
				"Combi-plasma",
				"Lightning Claw (+3 pts)",
				"Plasma Pistol",
				"Power Axe",
				"Power Fist (+5 pts)",
				"Power Maul",
				"Power Sword",
				"Storm Bolter",
				"Storm Shield (+5 pts)",
				"Thunder Hammer (+10 pts)"
			});

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Astartes Chainsword",
				"Bolt Pistol",
				"Lightning Claw (+3 pts)",
				"Plasma Pistol",
				"Power Axe",
				"Power Fist (+5 pts)",
				"Power Maul",
				"Power Sword",
				"Storm Shield (+5 pts)",
				"Thunder Hammer (+10 pts)"
			});

			cbOption1.Text = "Jump Pack (+3 pts/model) (All Models)";

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption2"].Location.X + 20, panel.Controls["cmbOption2"].Location.Y + 60);
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
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
			{
				case 11:
					if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
					{
						Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
						if (currentIndex == 0)
						{
							lbModelSelect.Items[currentIndex] = "Wolf Guard Pack Leader w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
						}
						else
						{
							lbModelSelect.Items[currentIndex] = "Wolf Guard w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
						}
					}
					else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
					break;
				case 12:
					if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
					{
						Weapons[(currentIndex * 2) + 2] = cmbOption2.SelectedItem.ToString();
						if (currentIndex == 0)
						{
							lbModelSelect.Items[currentIndex] = "Wolf Guard Pack Leader w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
						}
						else
						{
							lbModelSelect.Items[currentIndex] = "Wolf Guard w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
						}
					}
					else
					{
						cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);
					}
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption2.Enabled = true;
                    restrictedIndexes.Clear();
                    restrictedIndexes2.Clear();

					if(chosenRelic == "Frost Weapon")
					{
						cmbOption2.SelectedIndex = 4;
						restrictedIndexes2.AddRange(new int[] { 0, 1, 3, 5, 6, 8, 9 });
					}
					else if (chosenRelic == "Morkai's Teeth Bolts")
					{
						cmbOption1.SelectedIndex = 1;
						restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 13, 14 });
					}

					this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
                    Relic = chosenRelic;
                    break;
                case 21:
					if (cbOption1.Checked)
					{
						Weapons[0] = "Jump Packs";
					}
					else
					{
						Weapons[0] = "";
					}
					break;
				case 30:
					int temp = UnitSize;
					UnitSize = int.Parse(nudUnitSize.Value.ToString());

					if (temp < UnitSize)
					{
						Weapons.Add("Boltgun");
						Weapons.Add("Bolt Pistol");
						lbModelSelect.Items.Add("Wolf Guard w/ " + Weapons[((UnitSize - 1) * 2) + 1] + " and " + Weapons[((UnitSize - 1) * 2) + 2]);
					}

					if (temp > UnitSize)
					{
						lbModelSelect.Items.RemoveAt(temp - 1);
						Weapons.RemoveRange(((UnitSize - 1) * 2) + 1, 2);
					}
					break;
				case 61:
					currentIndex = lbModelSelect.SelectedIndex;

					if (currentIndex < 0)
					{
						cmbOption1.Visible = false;
						cmbOption2.Visible = false;
						cbOption1.Visible = false;
						panel.Controls["lblOption1"].Visible = false;
						panel.Controls["lblOption2"].Visible = false;
						cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
					}
					else if (currentIndex == -1)
					{
						break;
					}
					antiLoop = true;

					cmbOption1.Visible = true;
					cmbOption2.Visible = true;
					panel.Controls["lblOption1"].Visible = true;
					panel.Controls["lblOption2"].Visible = true;
					cbOption1.Visible = true;
					cbStratagem5.Visible = false;
                    cmbRelic.Visible = false;
                    panel.Controls["lblRelic"].Visible = false;

                    restrictedIndexes.Clear();
                    restrictedIndexes2.Clear();

					if(currentIndex == 0)
                    {
                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        if (Relic == "Frost Weapon")
                        {
                            restrictedIndexes2.AddRange(new int[] { 0, 1, 3, 5, 6, 8, 9 });
                            cmbOption2.SelectedIndex = 4;
                        }
                        else if (Relic == "Morkai's Teeth Bolts")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 6, 7, 8, 9, 10, 11, 13, 14 });
                            cmbOption1.SelectedIndex = 1;
                        }
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);

					cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
					cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);
					antiLoop = false;

					lbModelSelect.SelectedIndex = currentIndex;

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
			if (Weapons[0] != "")
			{
				Points += UnitSize * 3;
			}

			foreach (var weapon in Weapons)
			{
				if (weapon == "Lightning Claw (+3 pts)")
				{
					Points += 3;
				}

				if (weapon == "Storm Shield (+5 pts)")
				{
					Points += 5;
				}

				if (weapon == "Power Fist (+5 pts)")
				{
					Points += 5;
				}

				if (weapon == "Thunder Hammer (+10 pts)")
				{
					Points += 10;
				}
			}
		}

		public override string ToString()
		{
			return "Wolf Guard - " + Points + "pts";
		}
	}
}
