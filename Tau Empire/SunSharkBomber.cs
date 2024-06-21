using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
	public class SunSharkBomber : Datasheets
	{
		public SunSharkBomber()
		{
			DEFAULT_POINTS = 165;
			Points = DEFAULT_POINTS;
			UnitSize = 1;
			TemplateCode = "1k";
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"T'AU EMPIRE", "<SEPT>",
				"VEHICLE", "AIRCRAFT", "FLY", "SUN SHARK BOMBER"
			});
			Role = "Flyer";
		}

		public override Datasheets CreateUnit()
		{
			return new SunSharkBomber();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);

			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

			cbOption1.Text = "Missile Pod";
			if (Weapons[0] != string.Empty)
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
			switch (code)
			{
				case 21:
					CheckBox cb = panel.Controls["cbOption1"] as CheckBox;
					if (cb.Checked)
					{
						Weapons[0] = cb.Text;
					}
					else { Weapons[0] = string.Empty; }
					break;
			}

			Points = DEFAULT_POINTS;
		}

		public override string ToString()
		{
			return "Sun Shark Bomber - " + Points + "pts";
		}
	}
}
