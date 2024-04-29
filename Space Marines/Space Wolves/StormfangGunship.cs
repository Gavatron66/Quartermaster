using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class StormfangGunship : Datasheets
	{
		public StormfangGunship()
		{
			DEFAULT_POINTS = 225;
			UnitSize = 1;
			Points = DEFAULT_POINTS * UnitSize;
			TemplateCode = "2m";
			Weapons.Add("Skyhammer Missile Launcher");
			Weapons.Add("Two Stormstrike Missile Launchers");
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"VEHICLE", "TRANSPORT", "AIRCRAFT", "MACHINE SPIRIT", "FLY", "STORMFANG GUNSHIP"
			});
			Role = "Flyer";
		}

		public override Datasheets CreateUnit()
		{
			return new StormfangGunship();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Skyhammer Missile Launcher",
				"Two Melta Arrays",
				"Two Twin Heavy Bolters"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Two Lascannons",
				"Two Stormstrike Missile Launchers"
			});
			cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

			switch (code)
			{
				case 11:
					Weapons[0] = cmbOption1.SelectedItem as string;
					break;
				case 12:
					Weapons[1] = cmbOption2.SelectedItem as string;
					break;
			}

			Points = DEFAULT_POINTS * UnitSize;
		}

		public override string ToString()
		{
			return "Stormfang Gunship - " + Points + "pts";
		}
	}
}
