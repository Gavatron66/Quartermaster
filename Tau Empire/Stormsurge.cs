using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
	public class Stormsurge : Datasheets
	{
		public Stormsurge()
		{
			DEFAULT_POINTS = 400;
			Points = DEFAULT_POINTS;
			TemplateCode = "4m";
			Weapons.Add("Pulse Driver Cannon");
			Weapons.Add("Twin T'au Flamer");
			Weapons.Add("Velocity Tracker");
			Weapons.Add("(None)");
			Keywords.AddRange(new string[]
			{
				"T'AU EMPIRE", "<SEPT>",
				"VEHICLE", "TITANIC", "STORMSURGE"
			});
			Role = "Lord of War";
		}

		public override Datasheets CreateUnit()
		{
			return new Stormsurge();
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
				"Pulse Blastcannon (+10 pts)",
				"Pulse Driver Cannon"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Twin Airbursting Fragmentation Projector (+5 pts)",
				"Twin Burst Cannon (+5 pts)",
				"Twin T'au Flamer",
			});
			cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

			cmbOption3.Items.Clear();
			cmbOption3.Items.AddRange(new string[]
			{
				"Counterfire Defence System",
				"Velocity Tracker"
			});
			cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

			cmbOption4.Items.Clear();
			cmbOption4.Items.AddRange(new string[]
			{
				"(None)",
				"Early Warning Override",
				"Stormsurge Multi-tracker"
			});
			cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[3]);
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
			}

			Points = DEFAULT_POINTS;

			foreach (var weapon in Weapons)
			{
				if (weapon == "Pulse Blastcannon (+10 pts)")
				{
					Points += 10;
				}
				if (weapon == "Twin Airbursting Fragmentation Projector (+5 pts)")
				{
					Points += 5;
				}
				if (weapon == "Twin Burst Cannon (+5 pts)")
				{
					Points += 5;
				}
			}
		}

		public override string ToString()
		{
			return "Stormsurge - " + Points + " pts";
		}
	}
}
