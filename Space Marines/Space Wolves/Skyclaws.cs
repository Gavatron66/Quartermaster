using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class Skyclaws : Datasheets
	{
		int currentIndex;
		int restrict = 0;
		int wolfGuard = 0;
		// 0 = False, 1 = Pack Leader

		public Skyclaws()
		{
			DEFAULT_POINTS = 18;
			UnitSize = 5;
			Points = DEFAULT_POINTS * UnitSize;
			TemplateCode = "NL2m1k";
			Weapons.Add("");
			Weapons.Add("");
			Weapons.Add("Astartes Chainsword");
			for (int i = 1; i < UnitSize; i++)
			{
				Weapons.Add("Bolt Pistol");
			}
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "CORE", "BLOOD CLAWS", "JUMP PACK", "MELTA BOMB", "FLY", "SKYCLAWS"
			});
			Role = "Fast Attack";
		}

		public override Datasheets CreateUnit()
		{
			return new Skyclaws();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as SpaceMarines;
			Template.LoadTemplate(TemplateCode, panel);

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

			int currentSize = UnitSize;
			nudUnitSize.Minimum = 5;
			antiLoop = true;
			nudUnitSize.Value = nudUnitSize.Minimum;
			antiLoop = false;
			nudUnitSize.Maximum = 15;

			lbModelSelect.Items.Clear();
			lbModelSelect.Items.Add("Skyclaw Pack Leader w/ " + Weapons[2]);
			if (wolfGuard == 1)
			{
				Weapons[0] = "Bolt Pistol";
				Weapons[1] = "Astartes Chainsword";
				lbModelSelect.Items.Add("Wolf Guard Skyclaw Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
				for (int i = 1; i < UnitSize; i++)
				{
					lbModelSelect.Items.Add("Skyclaw w/ " + Weapons[i + 2]);
				}
			}
			else if (wolfGuard == 0)
			{
				for (int i = 1; i < UnitSize; i++)
				{
					lbModelSelect.Items.Add("Skyclaw w/ " + Weapons[i + 2]);
				}
			}

			cbOption1.Text = "Include a Wolf Guard Skyclaw Pack Leader (+20 pts)";
			if(wolfGuard == 1)
			{
				cbOption1.Checked = true;
			}
			else
			{
				cbOption1.Checked = false;
			}
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
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

			switch (code)
			{
				case 11:
					if (wolfGuard != 0 && currentIndex == 1)
					{
						Weapons[0] = cmbOption1.SelectedItem.ToString();
						lbModelSelect.Items[1] = "Wolf Guard Skyclaw Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
					}
					else
					{
						Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
						if (currentIndex == 0)
						{
							lbModelSelect.Items[0] = "Skyclaw Pack Leader w/ " + Weapons[2];
						}
						else
						{
							lbModelSelect.Items[currentIndex] = "Skyclaw w/ " + Weapons[currentIndex + 2];
						}
					}

					break;
				case 12:
					Weapons[1] = cmbOption2.SelectedItem.ToString();
					lbModelSelect.Items[1] = "Wolf Guard Skyclaw Pack Leader w/ " + Weapons[0] + " and " + Weapons[1];
					break;
				case 21:
					if(cbOption1.Checked)
					{
						Weapons[0] = "Bolt Pistol";
						Weapons[1] = "Astartes Chainsword";
						lbModelSelect.Items.Insert(1, "Wolf Guard Skyclaw Pack Leader w/ " + Weapons[0] + " and " + Weapons[1]);
						wolfGuard = 1;
					}
					else if (Weapons[0] != "" && cbOption1.Checked == false)
					{
						Weapons[0] = "";
						Weapons[1] = "";
						if (lbModelSelect.Items[1].ToString().Contains("Wolf Guard"))
						{
							lbModelSelect.Items.RemoveAt(1);
						}
						wolfGuard = 0;
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
							lbModelSelect.Items.Add("Skyclaw w/ " + Weapons[temp + 2]);
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
						cbOption1.Visible = false;
						antiLoop = false;
						break;
					}
					else if (currentIndex == 0)
					{
						panel.Controls["lblOption1"].Visible = true;
						cmbOption1.Visible = true;
						panel.Controls["lblOption2"].Visible = false;
						cmbOption2.Visible = false;
						cbOption1.Visible = true;

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
						cbOption1.Visible = true;

						cmbOption1.Items.Clear();
						cmbOption2.Items.Clear();

						cmbOption1.Items.AddRange(new string[]
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
							"Thunder Hammer"
						});

						cmbOption2.Items.AddRange(new string[]
						{
							"Astartes Chainsword",
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
							"Thunder Hammer"
						});

						cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
						cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
					}
					else
					{
						panel.Controls["lblOption1"].Visible = true;
						cmbOption1.Visible = true;
						panel.Controls["lblOption2"].Visible = false;
						cmbOption2.Visible = false;
						cbOption1.Visible = true;

						cmbOption1.Items.Clear();
						cmbOption1.Items.AddRange(new string[]
						{
							"Bolt Pistol",
							"Flamer",
							"Grav-gun",
							"Meltagun",
							"Plasma Gun",
							"Plasma Pistol"
						});
						cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);

						if (restrict == 2 && (Weapons[currentIndex + 2] == "Bolt Pistol"))
						{
							cmbOption1.Items.Remove("Flamer");
							cmbOption1.Items.Remove("Grav-gun");
							cmbOption1.Items.Remove("Meltagun");
							cmbOption1.Items.Remove("Plasma Gun");
							cmbOption1.Items.Remove("Plasma Pistol");
						}
					}


					antiLoop = false;
					break;
			}

			Points = DEFAULT_POINTS * UnitSize;

			if (wolfGuard == 1)
			{
				Points += 20;
			}

			restrict = 0;

			for (int i = 3; i < Weapons.Count; i++)
			{

				if (Weapons[i] != "Bolt Pistol")
				{
					restrict++;
				}
			}
		}

		public override string ToString()
		{
			return "Skyclaws - " + Points + "pts";
		}
	}
}
