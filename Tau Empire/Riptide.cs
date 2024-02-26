using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
	public class Riptide : Datasheets
	{
		public Riptide()
		{
			DEFAULT_POINTS = 240;
			UnitSize = 1;
			Points = UnitSize * DEFAULT_POINTS;
			TemplateCode = "5m2k";
			Weapons.Add("Heavy Burst Cannon");
			Weapons.Add("Two Plasma Rifles");
			Weapons.Add("(None)");
			Weapons.Add("(None)");
			Weapons.Add("(None)");
			Weapons.Add("");
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"T'AU EMIRE", "<SEPT>",
				"VEHICLE", "BATTLESUIT", "FLY", "JET PACK", "RIPTIDE BATTLESUIT"
			});
			Role = "Heavy Support";
		}

		public override Datasheets CreateUnit()
		{
			return new Riptide();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
			ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
			ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Heavy Burst Cannon",
				"Ion Accelerator (+10 pts)"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Two Fusion Blasters (+20 pts)",
				"Two Plasma Rifles",
				"Two Smart Missile Systems (+10 pts)"
			});
			cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

			cmbOption3.Items.Clear();
			cmbOption3.Items.AddRange(new string[]
			{
				"(None)",
				"Counterfire Defence System",
				"Early Warning Override",
				"Multi-tracker",
				"Target Lock",
				"Velocity Tracker"
			});
			cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

			cmbOption4.Items.Clear();
			cmbOption4.Items.AddRange(new string[]
			{
				"(None)",
				"Counterfire Defence System",
				"Early Warning Override",
				"Multi-tracker",
				"Target Lock",
				"Velocity Tracker"
			});
			cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);

			cmbOption5.Items.Clear();
			cmbOption5.Items.AddRange(new string[]
			{
				"(None)",
				"Counterfire Defence System",
				"Early Warning Override",
				"Multi-tracker",
				"Target Lock",
				"Velocity Tracker"
			});
			cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(Weapons[4]);

			cbOption1.Text = "Shielded Missile Drone (+15 pts)";
			if (Weapons[5] != string.Empty)
			{
				cbOption1.Checked = true;
			}
			else
			{
				cbOption1.Checked = false;
			}

			cbOption2.Text = "Shielded Missile Drone (+15 pts)";
			if (Weapons[6] != string.Empty)
			{
				cbOption2.Checked = true;
			}
			else
			{
				cbOption2.Checked = false;
			}
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
			ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
			ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

			switch (code)
			{
				case 11:
					Weapons[0] = cmbOption1.SelectedItem.ToString();
					break;
				case 12:
					Weapons[1] = cmbOption2.SelectedItem.ToString();
					break;
				case 13:
					Weapons[2] = cmbOption3.SelectedItem.ToString();
					break;
				case 14:
					Weapons[3] = cmbOption4.SelectedItem.ToString();
					break;
				case 18:
					Weapons[4] = cmbOption5.SelectedItem.ToString();
					break;
				case 21:
					if (cbOption1.Checked)
					{
						Weapons[5] = cbOption1.Text;
					}
					else { Weapons[5] = string.Empty; }
					break;
				case 22:
					if (cbOption2.Checked)
					{
						Weapons[6] = cbOption2.Text;
					}
					else { Weapons[6] = string.Empty; }
					break;
			}

			Points = DEFAULT_POINTS;

			foreach (var weapon in Weapons)
			{
				if(weapon == "Two Fusion Blasters (+20 pts)")
				{
					Points += 20;
				}

				if(weapon == "Ion Accelerator (+10 pts)" || weapon == "Two Smart Missile Systems (+10 pts)")
				{
					Points += 10;
				}

				if(weapon == "Shielded Missile Drone (+15 pts)")
				{
					Points += 15;
				}
			}
		}

		public override string ToString()
		{
			return "Riptide Battlesuit - " + Points + "pts";
		}
	}
}
