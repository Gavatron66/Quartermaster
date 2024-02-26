using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
	public class Broadsides : Datasheets
	{
		int currentIndex;

		public Broadsides()
		{
			DEFAULT_POINTS = 85;
			UnitSize = 1;
			Points = DEFAULT_POINTS * UnitSize;
			TemplateCode = "NL5m";
			Weapons.Add("Heavy Rail Rifle");
			Weapons.Add("(None)");
			Weapons.Add("(None)");
			Weapons.Add("(None)"); // Two Gun Drones
			Weapons.Add("(None)");
			Keywords.AddRange(new string[]
			{
				"T'AU EMPIRE", "<SEPT>",
				"INFANTRY", "CORE", "BATTLESUIT", "BROADSIDE BATTLESUITS"
			});
			Role = "Heavy Support";
		}

		public override Datasheets CreateUnit()
		{
			return new Broadsides();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as T_au;

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
			ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
			ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;

			int currentSize = UnitSize;
			nudUnitSize.Minimum = 1;
			antiLoop = true;
			nudUnitSize.Value = nudUnitSize.Minimum;
			antiLoop = false;
			nudUnitSize.Maximum = 3;
			nudUnitSize.Value = currentSize;

			lbModelSelect.Items.Clear();
			lbModelSelect.Items.Add("Broadside Shas'vre - " + CalcPoints(0) + " pts");
			for (int i = 1; i < UnitSize; i++)
			{
				lbModelSelect.Items.Add("Broadside Shas'ui - " + CalcPoints(i) + " pts");
			}

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Heavy Rail Rifle",
				"Two High-yield Missile Pods (+20 pts)"
			});

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"(None)",
				"Advanced Targeting System",
				"Early Warning Override",
				"Multi-tracker",
				"Seeker Missile (+5 pts)",
				"Stabilised Optics",
				"Twin Plasma Rifle (+10 pts)",
				"Twin Smart Missile System (+15 pts)",
				"Velocity Tracker"
			});

			cmbOption3.Items.Clear();
			cmbOption3.Items.AddRange(new string[]
			{
				"(None)",
				"Advanced Targeting System",
				"Multi-tracker",
				"Seeker Missile (+5 pts)",
				"Twin Smart Missile System (+15 pts)",
			});

			cmbOption4.Items.Clear();
			cmbOption4.Items.AddRange(new string[]
			{
				"(None)",
				"Gun Drone (+10 pts)",
				"Marker Drone (+10 pts)",
				"Missile Drone (+15 pts)",
				"Shield Drone (+15 pts)"
			});

			cmbOption5.Items.Clear();
			cmbOption5.Items.AddRange(new string[]
			{
				"(None)",
				"Gun Drone (+10 pts)",
				"Marker Drone (+10 pts)",
				"Missile Drone (+15 pts)",
				"Shield Drone (+15 pts)"
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
			ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
			ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
			ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;

			switch (code)
			{
				case 11:
					Weapons[(currentIndex * 5)] = cmbOption1.SelectedItem.ToString();
					if (currentIndex == 0)
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'vre - " + CalcPoints(currentIndex) + " pts");
					}
					else
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'ui - " + CalcPoints(currentIndex) + " pts");
					}
					break;
				case 12:
					Weapons[(currentIndex * 5) + 1] = cmbOption2.SelectedItem.ToString();
					if (currentIndex == 0)
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'vre - " + CalcPoints(currentIndex) + " pts");
					}
					else
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'ui - " + CalcPoints(currentIndex) + " pts");
					}
					break;
				case 13:
					Weapons[(currentIndex * 5) + 2] = cmbOption3.SelectedItem.ToString();
					if (currentIndex == 0)
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'vre - " + CalcPoints(currentIndex) + " pts");
					}
					else
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'ui - " + CalcPoints(currentIndex) + " pts");
					}
					break;
				case 14:
					Weapons[(currentIndex * 5) + 3] = cmbOption4.SelectedItem.ToString();
					if (currentIndex == 0)
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'vre - " + CalcPoints(currentIndex) + " pts");
					}
					else
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'ui - " + CalcPoints(currentIndex) + " pts");
					}
					break;
				case 18:
					Weapons[(currentIndex * 5) + 4] = cmbOption5.SelectedItem.ToString();
					if (currentIndex == 0)
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'vre - " + CalcPoints(currentIndex) + " pts");
					}
					else
					{
						lbModelSelect.Items[currentIndex] = ("Broadside Shas'ui - " + CalcPoints(currentIndex) + " pts");
					}
					break;
				case 30:
					int temp = UnitSize;
					UnitSize = int.Parse(nudUnitSize.Value.ToString());

					if (temp < UnitSize)
					{
						Weapons.Add("Heavy Rail Rifle");
						Weapons.Add("(None)");
						Weapons.Add("(None)");
						Weapons.Add("(None)"); //Two Drones
						Weapons.Add("(None)");
						lbModelSelect.Items.Add("Broadside Shas'ui - " + CalcPoints(UnitSize - 1) + " pts");
					}

					if (temp > UnitSize)
					{
						lbModelSelect.Items.RemoveAt(temp - 1);
						Weapons.RemoveRange((UnitSize * 5) - 1, 5);
					}
					break;
				case 61:
					currentIndex = lbModelSelect.SelectedIndex;

					if (currentIndex < 0)
					{
						cmbOption1.Visible = false;
						cmbOption2.Visible = false;
						cmbOption3.Visible = false;
						cmbOption4.Visible = false;
						cmbOption5.Visible = false;
						panel.Controls["lblOption1"].Visible = false;
						panel.Controls["lblOption2"].Visible = false;
						panel.Controls["lblOption3"].Visible = false;
						panel.Controls["lblOption4"].Visible = false;
						panel.Controls["lblOption5"].Visible = false;
						break;
					}

					cmbOption1.Visible = true;
					cmbOption2.Visible = true;
					cmbOption3.Visible = true;
					cmbOption4.Visible = true;
					cmbOption5.Visible = true;
					panel.Controls["lblOption1"].Visible = true;
					panel.Controls["lblOption2"].Visible = true;
					panel.Controls["lblOption3"].Visible = true;
					panel.Controls["lblOption4"].Visible = true;
					panel.Controls["lblOption5"].Visible = true;

					cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 5]);
					cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 5) + 1]);
					cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 5) + 2]);
					cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[(currentIndex * 5) + 3]);
					cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(Weapons[(currentIndex * 5) + 4]);

					break;
			}

			Points = (DEFAULT_POINTS * UnitSize) + CalcPoints(-1);

			Points += repo.GetFactionUpgradePoints(Factionupgrade);
		}

		public override string ToString()
		{
			return "Broadside Battlesuits - " + Points + "pts";
		}

		private int CalcPoints(int index)
		{
			int points = 0;

			if (index <= -1)
			{
				for (int i = 0; i < Weapons.Count; i++)
				{
					if (Weapons[i] == "Gun Drone (+10 pts)")
					{
						points += 10;
					}
					else if (Weapons[i] == "Two High-yield Missile Pods (+20 pts)")
					{
						points += 20;
					}
					else if (Weapons[i] == "Marker Drone (+10 pts)")
					{
						points += 10;
					}
					else if (Weapons[i] == "Missile Drone (+15 pts)")
					{
						points += 15;
					}
					else if (Weapons[i] == "Seeker Missile (+5 pts)")
					{
						points += 5;
					}
					else if (Weapons[i] == "Shield Drone (+15 pts)")
					{
						points += 15;
					}
					else if (Weapons[i] == "Twin Plasma Rifle (+10 pts)")
					{
						points += 10;
					}
					else if (Weapons[i] == "Twin Smart Missile System (+15 pts)")
					{
						points += 15;
					}
				}

				return points;
			}

			for (int i = 5 * index; i < 5 * (index + 1); i++)
			{
				if (Weapons[i] == "Gun Drone (+10 pts)")
				{
					points += 10;
				}
				else if (Weapons[i] == "Two High-yield Missile Pods (+20 pts)")
				{
					points += 20;
				}
				else if (Weapons[i] == "Marker Drone (+10 pts)")
				{
					points += 10;
				}
				else if (Weapons[i] == "Missile Drone (+15 pts)")
				{
					points += 15;
				}
				else if (Weapons[i] == "Seeker Missile (+5 pts)")
				{
					points += 5;
				}
				else if (Weapons[i] == "Shield Drone (+15 pts)")
				{
					points += 15;
				}
				else if (Weapons[i] == "Twin Plasma Rifle (+10 pts)")
				{
					points += 10;
				}
				else if (Weapons[i] == "Twin Smart Missile System (+15 pts)")
				{
					points += 15;
				}
			}

			return 85 + points;
		}
	}
}
