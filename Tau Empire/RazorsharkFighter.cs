using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
	public class RazorsharkFighter : Datasheets
	{
		public RazorsharkFighter()
		{
			DEFAULT_POINTS = 155;
			UnitSize = 1;
			Points = DEFAULT_POINTS;
			TemplateCode = "1m";
			Weapons.Add("Accelerator Burst Cannon");
			Keywords.AddRange(new string[]
			{
				"T'AU EMPIRE", "<SEPT>",
				"VEHICLE", "AIRCRAFT", "FLY", "RAZORSHARK STRIKE FIGHTER"
			});
			Role = "Flyer";
		}

		public override Datasheets CreateUnit()
		{
			return new RazorsharkFighter();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as T_au;

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Accelerator Burst Cannon",
				"Missile Pod"
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
		}

		public override string ToString()
		{
			return "Razorshark Strike Fighter - " + Points + "pts";
		}
	}
}
