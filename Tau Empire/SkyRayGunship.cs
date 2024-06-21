using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
	public class SkyRayGunship : Datasheets
	{
		public SkyRayGunship()
		{
			DEFAULT_POINTS = 135;
			UnitSize = 1;
			Points = DEFAULT_POINTS;
			TemplateCode = "1m";
			Weapons.Add("Two Gun Drones");
			Keywords.AddRange(new string[]
			{
				"T'AU EMPIRE", "<SEPT>",
				"VEHICLE", "FLY", "SKY RAY GUNSHIP"
			});
			Role = "Heavy Support";
		}

		public override Datasheets CreateUnit()
		{
			return new SkyRayGunship();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as T_au;

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Two Accelerator Burst Cannons (+10 pts)",
				"Two Gun Drones",
				"Two Smart Missile Systems (+10 pts)"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

			switch (code)
			{
				case 11:
					Weapons[0] = cmbOption1.SelectedItem as string;
					break;
			}

			Points = DEFAULT_POINTS;

			if (Weapons.Contains("Two Accelerator Burst Cannons (+10 pts)"))
			{
				Points += 10;
			}

			if (Weapons.Contains("Two Smart Missile Systems (+10 pts)"))
			{
				Points += 10;
			}
		}

		public override string ToString()
		{
			return "Sky Ray Gunship - " + Points + "pts";
		}
	}
}
