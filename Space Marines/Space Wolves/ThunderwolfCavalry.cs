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

			switch (code)
			{
				case 11:
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
					break;
				case 12:
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

						antiLoop = false;
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