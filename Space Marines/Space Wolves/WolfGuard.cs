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

			switch (code)
			{
				case 11:
					Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
					if (currentIndex == 0)
					{
						lbModelSelect.Items[currentIndex] = "Wolf Guard Pack Leader w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
					}
					else
					{
						lbModelSelect.Items[currentIndex] = "Wolf Guard w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
					}
					break;
				case 12:
					Weapons[(currentIndex * 2) + 2] = cmbOption2.SelectedItem.ToString();
					if (currentIndex == 0)
					{
						lbModelSelect.Items[currentIndex] = "Wolf Guard Pack Leader w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
					}
					else
					{
						lbModelSelect.Items[currentIndex] = "Wolf Guard w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
					}
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

					cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
					cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);
					antiLoop = false;

					lbModelSelect.SelectedIndex = currentIndex;

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
