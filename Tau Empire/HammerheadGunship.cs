using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
	public class HammerheadGunship : Datasheets
	{
		public HammerheadGunship()
		{
			DEFAULT_POINTS = 145;
			UnitSize = 1;
			Points = DEFAULT_POINTS;
			TemplateCode = "2m2k";
			Weapons.Add("Railgun");
			Weapons.Add("Two Gun Drones");
			Weapons.Add("");
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"T'AU EMPIRE", "<SEPT>",
				"VEHICLE", "FLY", "HAMMERHEAD", "HAMMERHEAD GUNSHIP"
			});
			Role = "Heavy Support";
		}

		public override Datasheets CreateUnit()
		{
			return new HammerheadGunship();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as T_au;

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Ion Cannon (+10 pts)",
				"Railgun"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Two Accelerator Burst Cannons (+10 pts)",
				"Two Gun Drones",
				"Two Smart Missile Systems (+10 pts)"
			});
			cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

			cbOption1.Text = "Seeker Missile (+5 pts)";
			if (Weapons[2] != string.Empty)
			{
				cbOption1.Checked = true;
			}
			else
			{
				cbOption1.Checked = false;
			}

			cbOption2.Text = "Seeker Missile (+5 pts)";
			if (Weapons[3] != string.Empty)
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
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

			switch (code)
			{
				case 11:
					Weapons[0] = cmbOption1.SelectedItem as string;
					break;
				case 12:
					Weapons[1] = cmbOption2.SelectedItem as string;
					break;
				case 21:
					if (cbOption1.Checked)
					{
						Weapons[2] = cbOption1.Text;
					}
					else { Weapons[2] = string.Empty; }
					break;
				case 22:
					if (cbOption2.Checked)
					{
						Weapons[3] = cbOption2.Text;
					}
					else { Weapons[3] = string.Empty; }
					break;
			}

			Points = DEFAULT_POINTS;

			foreach (var weapon in Weapons)
			{
				if(weapon == "Two Accelerator Burst Cannons (+10 pts)")
				{
					Points += 10;
				}

				if(weapon == "Ion Cannon (+10 pts)")
				{
					Points += 10;
				}

				if(weapon == "Two Smart Missile Systems (+10 pts)")
				{
					Points += 10;
				}

				if(weapon == "Seeker Missile (+5 pts)")
				{
					Points += 5;
				}
			}
		}

		public override string ToString()
		{
			return "Hammerhead Gunship - " + Points + "pts";
		}
	}
}
