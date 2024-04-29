using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class FenrisianWolves : Datasheets
	{
		public FenrisianWolves()
		{
			DEFAULT_POINTS = 7;
			UnitSize = 5;
			Points = DEFAULT_POINTS * UnitSize;
			TemplateCode = "N1k";
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"BEAST", "FENRISIAN WOLVES"
			});
			Role = "Fortification";
		}

		public override Datasheets CreateUnit()
		{
			return new FenrisianWolves();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as SpaceMarines;
			Template.LoadTemplate(TemplateCode, panel);

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

			nudUnitSize.Minimum = 5;
			nudUnitSize.Value = nudUnitSize.Minimum;
			nudUnitSize.Maximum = 15;
			nudUnitSize.Value = Convert.ToInt32(UnitSize);

			cbOption1.Text = "Include a Cyberwolf (+15 pts)";
			if (Weapons.Contains(""))
			{
				cbOption1.Checked = false;
			}
			else
			{
				cbOption1.Checked = true;
			}
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

			switch (code)
			{
				case 21:
					if (cbOption1.Checked)
					{
						Weapons[0] = cbOption1.Text;
					}
					else
					{
						Weapons[0] = "";
					}
					break;
				case 30:
					UnitSize = int.Parse(nudUnitSize.Value.ToString());
					break;
			}

			Points = DEFAULT_POINTS * UnitSize;

			if (cbOption1.Checked)
			{
				Points += 15;
			}
		}

		public override string ToString()
		{
			return "Fenrisian Wolves - " + Points + "pts";
		}
	}
}
