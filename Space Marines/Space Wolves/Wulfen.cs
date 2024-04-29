using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class Wulfen : Datasheets
	{
		int currentIndex = 0;

		public Wulfen()
		{
			DEFAULT_POINTS = 20;
			UnitSize = 5;
			Points = DEFAULT_POINTS * UnitSize;
			TemplateCode = "NL1m1k";
			for (int i = 0; i < UnitSize; i++)
			{
				Weapons.Add("Wulfen Claws");
				Weapons.Add("");
			}
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "WULFEN"
			});
			Role = "Elites";
		}

		public override Datasheets CreateUnit()
		{
			return new Wulfen();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as SpaceMarines;

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

			int currentSize = UnitSize;
			nudUnitSize.Minimum = 5;
			antiLoop = true;
			nudUnitSize.Value = nudUnitSize.Minimum;
			antiLoop = false;
			nudUnitSize.Maximum = 10;
			nudUnitSize.Value = currentSize;

			lbModelSelect.Items.Clear();
			if (Weapons[1] != "")
			{
				lbModelSelect.Items.Add("Wulfen Pack Leader w/ " + Weapons[0]
					+ " and a " + Weapons[1]);
			}
			else
			{
				lbModelSelect.Items.Add("Wulfen Pack Leader w/ " + Weapons[0]);
			}
			for (int i = 1; i < UnitSize; i++)
			{
				if (Weapons[(i * 2) + 1] != "")
				{
					lbModelSelect.Items.Add("Wulfen w/ " + Weapons[i * 2]
						+ " and a " + Weapons[(i * 2) + 1]);
				}
				else
				{
					lbModelSelect.Items.Add("Wulfen w/ " + Weapons[i * 2]);
				}
			}

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Great Frost Axe (+5 pts)",
				"Wulfen Claws",
				"Wulfen Frost Claws (+5 pts)",
				"Thunder Hammer + Storm Shield (+10 pts)"
			});

			cbOption1.Text = "Stormfrag Auto-launcher";
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

			switch (code)
			{
				case 11:
					Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();

					if(currentIndex == 0)
					{
						if (Weapons[1] != "")
						{
							lbModelSelect.Items[currentIndex] = ("Wulfen Pack Leader w/ " + Weapons[0]
								+ " and a " + Weapons[1]);
						}
						else
						{
							lbModelSelect.Items[currentIndex] = ("Wulfen Pack Leader w/ " + Weapons[0]);
						}
					}
					else
                    {
                        if (Weapons[(currentIndex * 2) + 1] != "")
                        {
                            lbModelSelect.Items[currentIndex] = ("Wulfen w/ " + Weapons[currentIndex * 2]
                                + " and a " + Weapons[(currentIndex * 2) + 1]);
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = ("Wulfen w/ " + Weapons[currentIndex * 2]);
                        }
                    }
					break;
				case 21:
					if (cbOption1.Checked)
					{
						Weapons[(currentIndex * 2) + 1] = cbOption1.Text;
					}
					else
					{
						Weapons[(currentIndex * 2) + 1] = "";
					}

					if (currentIndex == 0)
					{
						if (Weapons[1] != "")
						{
							lbModelSelect.Items[currentIndex] = ("Wulfen Pack Leader w/ " + Weapons[0]
								+ " and a " + Weapons[1]);
						}
						else
						{
							lbModelSelect.Items[currentIndex] = ("Wulfen Pack Leader w/ " + Weapons[0]);
						}
					}

					if (Weapons[(currentIndex * 2) + 1] != "")
					{
						lbModelSelect.Items[currentIndex] = ("Wulfen w/ " + Weapons[currentIndex * 2]
							+ " and a " + Weapons[(currentIndex * 2) + 1]);
					}
					else
					{
						lbModelSelect.Items[currentIndex] = ("Wulfen w/ " + Weapons[currentIndex * 2]);
					}
					break;
				case 30:
					int temp = UnitSize;
					UnitSize = int.Parse(nudUnitSize.Value.ToString());

					if (temp < UnitSize)
					{
						for (int i = temp; i < UnitSize; i++)
						{
							Weapons.Add("Wulfen Claws");
							Weapons.Add("");
							lbModelSelect.Items.Add("Wulfen w/ " + Weapons[currentIndex * 2]);
						}
					}

					if (temp > UnitSize)
					{
						lbModelSelect.Items.RemoveAt(temp - 1);
						Weapons.RemoveRange((UnitSize * 2) - 1, 2);
					}
					break;
				case 61:
					currentIndex = lbModelSelect.SelectedIndex;

					if (currentIndex < 0)
					{
						cmbOption1.Visible = false;
						cbOption1.Visible = false;
						panel.Controls["lblOption1"].Visible = false;
					}
					else
					{
						cmbOption1.Visible = true;
						cbOption1.Visible = true;
						panel.Controls["lblOption1"].Visible = true;

						cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);

						if (Weapons[(currentIndex * 2) + 1] == "")
						{
							cbOption1.Checked = false;
						}
						else
						{
							cbOption1.Checked = true;
						}
					}
					break;
			}

			Points = DEFAULT_POINTS * UnitSize;

			foreach (var weapon in Weapons)
			{
				if(weapon == "Thunder Hammer + Storm Shield (+10 pts)")
				{
					Points += 10;
				}

				if(weapon == "Great Frost Axe (+5 pts)" || weapon == "Wulfen Frost Claws (+5 pts)")
				{
					Points += 5;
				}
			}
		}

		public override string ToString()
		{
			return "Wulfen - " + Points + "pts";
		}
	}
}
