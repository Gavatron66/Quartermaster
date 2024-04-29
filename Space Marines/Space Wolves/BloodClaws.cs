using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class BloodClaws : Datasheets
	{
		int currentIndex;
		int[] restrict = new int[2] { 0, 0 };
		int wolfGuard = 0;
		// 0 = False, 1 = Pack Leader, 2 = Terminator Pack Leader

		public BloodClaws()
		{
			DEFAULT_POINTS = 17;
			UnitSize = 5;
			Points = DEFAULT_POINTS * UnitSize;
			TemplateCode = "NL2m";
			Weapons.Add("");
			Weapons.Add("");
			Weapons.Add("Astartes Chainsword");
			for (int i = 1; i < UnitSize; i++)
			{
				Weapons.Add("Astartes Chainsword");
			}
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "CORE", "BLOOD CLAWS"
			});
			Role = "Troops";
		}

		public override Datasheets CreateUnit()
		{
			return new BloodClaws();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as SpaceMarines;
			Template.LoadTemplate(TemplateCode, panel);

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			ComboBox cmbFaction = panel.Controls["cmbFactionUpgrade"] as ComboBox;

			panel.Controls["lblExtra1"].Visible = true;
			cmbFaction.Visible = true;

			int currentSize = UnitSize;
			nudUnitSize.Minimum = 5;
			antiLoop = true;
			nudUnitSize.Value = nudUnitSize.Minimum;
			antiLoop = false;
			nudUnitSize.Maximum = 15;

			lbModelSelect.Items.Clear();
			lbModelSelect.Items.Add("Pack Leader w/ " + Weapons[2]);
			if(wolfGuard == 1)
			{
				Weapons[0] = "Boltgun";
				Weapons[1] = "Bolt Pistol";
				lbModelSelect.Items.Add("Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
				for (int i = 1; i < UnitSize; i++)
				{
					lbModelSelect.Items.Add("Blood Claw w/ " + Weapons[i + 2]);
				}
			}
			else if (wolfGuard == 2)
			{
				Weapons[0] = "Storm Bolter";
				Weapons[1] = "Power Sword";
				lbModelSelect.Items.Add("Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
				for (int i = 1; i < UnitSize; i++)
				{
					lbModelSelect.Items.Add("Blood Claw w/ " + Weapons[i + 2]);
				}
			}
			else if (wolfGuard == 0)
			{
				for (int i = 1; i < UnitSize; i++)
				{
					lbModelSelect.Items.Add("Blood Claw w/ " + Weapons[i + 2]);
				}
			}

			cmbFaction.Items.Clear();
			cmbFaction.Items.AddRange(new string[]
			{
				"(None)",
				"Wolf Guard Pack Leader (+18 pts)",
				"Wolf Guard Terminator Pack Leader (+34 pts)"
			});
			cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(wolfGuard);

			panel.Controls["lblExtra1"].Location = panel.Controls["lblFactionUpgrade"].Location;
			panel.Controls["lblExtra1"].Text = "May contain one of the following: ";
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			if (antiLoop)
			{
				return;
			}

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			ComboBox cmbFaction = panel.Controls["cmbFactionUpgrade"] as ComboBox;

			switch (code)
			{
				case 11:
					if(wolfGuard != 0 && currentIndex == 1)
					{
						Weapons[0] = cmbOption1.SelectedItem.ToString();
						if(wolfGuard == 1)
						{
							lbModelSelect.Items[1] = "Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
						}
						else if (wolfGuard == 2)
						{
							lbModelSelect.Items[1] = "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
						}
					}
					else
					{
						Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
						if(currentIndex == 0)
						{
							lbModelSelect.Items[0] = "Pack Leader w/ " + Weapons[2];
						}
						else
						{
							lbModelSelect.Items[currentIndex] = "Blood Claw w/ " + Weapons[currentIndex + 2];
						}
					}

					break;
				case 12:
					Weapons[1] = cmbOption2.SelectedItem.ToString();
					if (wolfGuard == 1)
					{
						lbModelSelect.Items[1] = "Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
					}
					else if (wolfGuard == 2)
					{
						lbModelSelect.Items[1] = "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
					}
					break;
				case 16:
					wolfGuard = cmbFaction.SelectedIndex;
					string temp2 = lbModelSelect.Items[1].ToString();
					if (temp2.Contains("Blood Claw")) {
						if(wolfGuard == 1)
						{
							Weapons[0] = "Boltgun";
							Weapons[1] = "Bolt Pistol";
							lbModelSelect.Items.Insert(1, "Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
						}
						if (wolfGuard == 2)
						{
							Weapons[0] = "Storm Bolter";
							Weapons[1] = "Power Sword";
							lbModelSelect.Items.Insert(1, "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
						}
					}
					else
					{
						if(wolfGuard == 1)
						{
							Weapons[0] = "Boltgun";
							Weapons[1] = "Bolt Pistol";
							lbModelSelect.Items[1] = "Wolf Guard Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
						}
						else if (wolfGuard == 2)
						{
							Weapons[0] = "Storm Bolter";
							Weapons[1] = "Power Sword";
							lbModelSelect.Items[1] = "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
						}
						else if (wolfGuard == 0)
						{
							if(temp2.Contains("Wolf Guard"))
							{
								lbModelSelect.Items.RemoveAt(1);
								Weapons[0] = "";
								Weapons[1] = "";
							}
						}
					}
					break;
				case 30:
					int temp = UnitSize;
					UnitSize = int.Parse(nudUnitSize.Value.ToString());

					if (temp < UnitSize)
					{
						for (int i = temp; i < UnitSize; i++)
						{
							Weapons.Add("Astartes Chainsword");
							lbModelSelect.Items.Add("Blood Claw w/ " + Weapons[temp + 2]);
						}
					}

					if (temp > UnitSize)
					{
						lbModelSelect.Items.RemoveAt(temp - 1);
						Weapons.RemoveRange(temp - 1, 1);
					}
					break;
				case 61:
					currentIndex = lbModelSelect.SelectedIndex;
					antiLoop = true;

					if (currentIndex < 0)
					{
						panel.Controls["lblOption1"].Visible = false;
						cmbOption1.Visible = false;
						panel.Controls["lblOption2"].Visible = false;
						cmbOption2.Visible = false;
						antiLoop = false;
						break;
					}
					else if (currentIndex == 0)
					{
						panel.Controls["lblOption1"].Visible = true;
						cmbOption1.Visible = true;
						panel.Controls["lblOption2"].Visible = false;
						cmbOption2.Visible = false;

						cmbOption1.Items.Clear();
						cmbOption1.Items.AddRange(new string[]
						{
							"Astartes Chainsword",
							"Power Axe",
							"Power Fist",
							"Power Sword"
						});
						cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[2]);
					}
					else if (currentIndex == 1 && wolfGuard != 0)
					{
						panel.Controls["lblOption1"].Visible = true;
						cmbOption1.Visible = true;
						panel.Controls["lblOption2"].Visible = true;
						cmbOption2.Visible = true;

						cmbOption1.Items.Clear();
						cmbOption2.Items.Clear();

						if(wolfGuard == 1)
						{
							cmbOption1.Items.AddRange(new string[]
							{
								"Astartes Chainsword",
								"Boltgun",
								"Combi-flamer",
								"Combi-grav",
								"Combi-melta",
								"Combi-plasma",
								"Lightning Claw",
								"Plasma Pistol",
								"Power Axe",
								"Power Fist",
								"Power Maul",
								"Power Sword",
								"Storm Bolter",
								"Storm Shield",
								"Thunder Hammer (+10 pts)"
							});

							cmbOption2.Items.AddRange(new string[]
							{
								"Astartes Chainsword",
								"Bolt Pistol",
								"Lightning Claw",
								"Plasma Pistol",
								"Power Axe",
								"Power Fist",
								"Power Maul",
								"Power Sword",
								"Storm Shield",
								"Thunder Hammer (+10 pts)"
							});
						}
						else if (wolfGuard == 2)
						{
							cmbOption1.Items.AddRange(new string[]
							{
								"Chainfist",
								"Combi-flamer",
								"Combi-grav",
								"Combi-melta",
								"Combi-plasma",
								"Lightning Claw",
								"Power Axe",
								"Power Fist",
								"Power Maul",
								"Power Sword",
								"Storm Bolter",
								"Thunder Hammer"
							});

							cmbOption2.Items.AddRange(new string[]
							{
								"Chainfist",
								"Lightning Claw",
								"Power Axe",
								"Power Fist",
								"Power Maul",
								"Power Sword",
								"Storm Shield",
								"Thunder Hammer"
							});
						}

						cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
						cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
					}
					else
					{
						panel.Controls["lblOption1"].Visible = true;
						cmbOption1.Visible = true;
						panel.Controls["lblOption2"].Visible = false;
						cmbOption2.Visible = false;

						cmbOption1.Items.Clear();
						cmbOption1.Items.AddRange(new string[]
						{
							"Astartes Chainsword",
							"Flamer",
							"Grav-gun",
							"Meltagun",
							"Plasma Gun",
							"Plasma Pistol"
						});
						cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);

						if (restrict[0] == (UnitSize / 15) + 1 && 
							(Weapons[currentIndex + 2] == "Astartes Chainsword" || Weapons[currentIndex + 2] == "Plasma Pistol"))
						{
							cmbOption1.Items.Remove("Flamer");
							cmbOption1.Items.Remove("Grav-gun");
							cmbOption1.Items.Remove("Meltagun");
							cmbOption1.Items.Remove("Plasma Gun");
						}

						if (restrict[1] == 1 && Weapons[currentIndex + 2] != "Plasma Pistol")
						{
							cmbOption1.Items.Remove("Plasma Pistol");
						}
					}


					antiLoop = false;
					break;
			}

			Points = DEFAULT_POINTS * UnitSize;

			if (Weapons[0].Contains("Thunder Hammer (+10 pts)") || Weapons[1].Contains("Thunder Hammer (+10 pts)"))
			{
				Points += 10;
			}
			if(wolfGuard == 1)
			{
				Points += 18;
			}
			if (wolfGuard == 2)
			{
				Points += 34;
			}

			restrict[0] = 0;
			restrict[1] = 0;

			for (int i = 3; i < Weapons.Count; i++)
			{
				if (Weapons[i] == "Plasma Pistol")
				{
					restrict[1]++;
				}

				if (Weapons[i] != "Astartes Chainsword" && Weapons[i] != "Plasma Pistol")
				{
					restrict[0]++;
				}
			}
		}

		public override string ToString()
		{
			return "Blood Claws - " + Points + "pts";
		}
	}
}
