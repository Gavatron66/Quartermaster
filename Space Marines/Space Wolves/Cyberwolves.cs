using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class Cyberwolves : Datasheets
	{
		public Cyberwolves()
		{
			UnitSize = 1;
			DEFAULT_POINTS = 15;
			Points = UnitSize * DEFAULT_POINTS;
			TemplateCode = "N";
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"BEAST", "CYBERWOLVES"
			});
			Role = "Fast Attack";
		}

		public override Datasheets CreateUnit()
		{
			return new Cyberwolves();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as SpaceMarines;

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

			int currentSize = UnitSize;
			nudUnitSize.Minimum = 1;
			nudUnitSize.Value = nudUnitSize.Minimum;
			nudUnitSize.Maximum = 5;
			nudUnitSize.Value = currentSize;
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

			switch (code)
			{
				case 30:
					UnitSize = int.Parse(nudUnitSize.Value.ToString());
					break;
			}

			Points = UnitSize * DEFAULT_POINTS;
		}

		public override string ToString()
		{
			return "Cyberwolves - " + Points + "pts";
		}
	}
}