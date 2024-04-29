using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class WulfenDreadnought : Datasheets
	{
		public WulfenDreadnought()
		{
			DEFAULT_POINTS = 120;
			Points = DEFAULT_POINTS;
			TemplateCode = "4m";
			Weapons.Add("Fenrisian Great Axe");
			Weapons.Add("Great Wolf Claw");
			Weapons.Add("Storm Bolter");
			Weapons.Add("(None)");
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"VEHICLE", "DREADNOUGHT", "WULFEN"
			});
			Role = "Elites";
		}

		public override Datasheets CreateUnit()
		{
			return new WulfenDreadnought();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
			ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Fenrisian Great Axe",
				"Blizzard Shield"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Great Wolf Claw",
				"Blizzard Shield"
			});
			cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

			cmbOption3.Items.Clear();
			cmbOption3.Items.AddRange(new string[]
			{
				"Heavy Flamer",
				"Storm Bolter"
			});
			cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

			cmbOption4.Items.Clear();
			cmbOption4.Items.AddRange(new string[]
			{
				"(None)",
				"Heavy Flamer",
				"Storm Bolter"
			});
			cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);

			if(Weapons.Contains("Blizzard Shield"))
			{
				if (Weapons[0].Contains("Blizzard Shield"))
				{
					cmbOption2.Items.Remove("Blizzard Shield");
				}
				else if (Weapons[1].Contains("Blizzard Shield"))
				{
					cmbOption1.Items.Remove("Blizzard Shield");
				}
			}

			if(Weapons.Contains("Fenrisian Great Axe"))
			{
				cmbOption4.Visible = false;
				panel.Controls["lblOption4"].Visible = false;
			}
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
			ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;

			switch (code)
			{
				case 11:
					Weapons[0] = cmbOption1.SelectedItem.ToString();

					if(cmbOption1.SelectedItem.ToString() == "Blizzard Shield")
					{
						cmbOption2.Items.Remove("Blizzard Shield");

						cmbOption4.Visible = true;
						panel.Controls["lblOption4"].Visible = true;
						cmbOption4.SelectedIndex = 2;
					}
					else if (!cmbOption2.Items.Contains("Blizzard Shield") && cmbOption4.Items.Count > 0)
					{
						cmbOption2.Items.Insert(0, "Blizzard Shield");
						cmbOption4.Visible = false;
						panel.Controls["lblOption4"].Visible = false;

						if(cmbOption4.Items.Count > 0)
						{
							cmbOption4.SelectedIndex = 0;
						}
					}
					else
					{
						cmbOption4.Visible = false;
						panel.Controls["lblOption4"].Visible = false;

						if (cmbOption4.Items.Count > 0)
						{
							cmbOption4.SelectedIndex = 0;
						}
					}
					break;
				case 12:
					Weapons[1] = cmbOption2.SelectedItem.ToString();

					if (cmbOption2.SelectedItem.ToString() == "Blizzard Shield")
					{
						cmbOption1.Items.Remove("Blizzard Shield");
					}
					else if (!cmbOption1.Items.Contains("Blizzard Shield"))
					{
						cmbOption1.Items.Insert(0, "Blizzard Shield");
					}

					break;
				case 13:
					Weapons[2] = cmbOption3.SelectedItem.ToString();
					break;
				case 14:
					Weapons[3] = cmbOption4.SelectedItem.ToString();
					break;
			}

			Points = DEFAULT_POINTS;
		}

		public override string ToString()
		{
			return "Wulfen Dreadnought - " + Points + "pts";
		}
	}
}
