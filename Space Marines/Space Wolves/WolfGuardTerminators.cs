using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class WolfGuardTerminators : Datasheets
	{
		int currentIndex;
		int restriction = 1;

		public WolfGuardTerminators()
		{
			DEFAULT_POINTS = 34;
			UnitSize = 5;
			Points = UnitSize * DEFAULT_POINTS;
			TemplateCode = "NL2m";
			Weapons.Add("Storm Bolter");
			Weapons.Add("Power Sword");
			for (int i = 1; i < UnitSize; i++)
			{
				Weapons.Add("Storm Bolter");
				Weapons.Add("Power Fist");
			}
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "CORE", "TERMINATOR", "WOLF GUARD"
			});
			Role = "Elites";
		}

		public override Datasheets CreateUnit()
		{
			return new WolfGuardTerminators();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as SpaceMarines;
			Template.LoadTemplate(TemplateCode, panel);

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

			int currentSize = UnitSize;
			nudUnitSize.Minimum = 5;
			nudUnitSize.Value = nudUnitSize.Minimum;
			nudUnitSize.Maximum = 10;
			nudUnitSize.Value = currentSize;

			lbModelSelect.Items.Clear();
			lbModelSelect.Items.Add("Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
			for (int i = 1; i < UnitSize; i++)
			{
				lbModelSelect.Items.Add("Wolf Guard Terminator w/ " + Weapons[(i * 2)] + " and " + Weapons[(i * 2) + 1]);
			}

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Assault Cannon",
				"Chainfist",
				"Combi-flamer",
				"Combi-grav",
				"Combi-melta",
				"Combi-plasma",
				"Cyclone Missile Launcher",
				"Heavy Flamer",
				"Lightning Claw",
				"Power Axe",
				"Power Fist",
				"Power Maul",
				"Power Sword",
				"Storm Bolter",
				"Storm Shield",
				"Thunder Hammer"
			});

			cmbOption2.Items.Clear();
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

		public override void SaveDatasheets(int code, Panel panel)
		{
			if(antiLoop)
			{
				return;
			}

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

			switch (code)
			{
				case 11:
					Weapons[(currentIndex * 2)] = cmbOption1.SelectedItem.ToString();

					if (currentIndex == 0)
					{
						lbModelSelect.Items[0] = "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
					}
					else
					{
						lbModelSelect.Items[currentIndex] = "Wolf Guard Terminator w/ " + Weapons[(currentIndex * 2)]
							+ " and " + Weapons[(currentIndex * 2) + 1];
					}
					break;
				case 12:
					Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
					if (currentIndex == 0)
					{
						lbModelSelect.Items[0] = "Wolf Guard Terminator Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
					}
					else
					{
						lbModelSelect.Items[currentIndex] = "Wolf Guard Terminator w/ " + Weapons[(currentIndex * 2)]
							+ " and " + Weapons[(currentIndex * 2) + 1];
					}
					break;
				case 30:
					int temp = UnitSize;
					UnitSize = int.Parse(nudUnitSize.Value.ToString());

					if (temp < UnitSize)
					{
						for (int i = temp; i < UnitSize; i++)
						{
							Weapons.Add("Storm Bolter");
							Weapons.Add("Power Fist");
							lbModelSelect.Items.Add("Wolf Guard Terminator w/ " + Weapons[(i * 2)]
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
						antiLoop = false;
					}
					else
					{
						cmbOption1.Visible = true;
						cmbOption2.Visible = true;
						panel.Controls["lblOption1"].Visible = true;
						panel.Controls["lblOption2"].Visible = true;
						cmbOption1.Enabled = true;

						cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2)]);
						cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

						if(restriction == UnitSize / 5 
							&& !(Weapons[currentIndex * 2] == "Assault Cannon" || Weapons[currentIndex * 2] == "Cyclone Missile Launcher" ||
							Weapons[currentIndex * 2] == "Heavy Flamer"))
						{
							cmbOption1.Items.Remove("Assault Cannon");
							cmbOption1.Items.Remove("Cyclone Missile Launcher");
							cmbOption1.Items.Remove("Heavy Flamer");
						}
						else if (!cmbOption1.Items.Contains("Heavy Flamer") 
							&& (Weapons[currentIndex * 2] == "Assault Cannon" || Weapons[currentIndex * 2] == "Cyclone Missile Launcher" ||
							Weapons[currentIndex * 2] == "Heavy Flamer" || (UnitSize / 5 > restriction)))
						{
							cmbOption1.Items.Insert(0, "Assault Cannon");
							cmbOption1.Items.Insert(6, "Cyclone Missile Launcher");
							cmbOption1.Items.Insert(7, "Heavy Flamer");
						}

						antiLoop = false;
					}
					break;
				default: break;
			}

			Points = DEFAULT_POINTS * UnitSize;
			restriction = 0;

			foreach (var weapon in Weapons)
			{
				if (weapon == "Assault Cannon" || weapon == "Cyclone Missile Launcher" || weapon == "Heavy Flamer")
				{
					restriction++;
				}
			}
		}

		public override string ToString()
		{
			return "Wolf Guard Terminator Squad - " + Points + "pts";
		}
	}
}