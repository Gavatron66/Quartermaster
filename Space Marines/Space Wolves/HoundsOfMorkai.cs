﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class HoundsOfMorkai : Datasheets
	{
		public HoundsOfMorkai()
		{
			UnitSize = 5;
			DEFAULT_POINTS = 17;
			Points = UnitSize * DEFAULT_POINTS;
			TemplateCode = "N";
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "CORE", "PHOBOS", "PRIMARIS", "REIVER", "HOUNDS OF MORKAI"
			});
			Role = "Elites";
		}

		public override Datasheets CreateUnit()
		{
			return new HoundsOfMorkai();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as SpaceMarines;

			ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;

			int currentSize = UnitSize;
			nudUnitSize.Minimum = 5;
			nudUnitSize.Value = nudUnitSize.Minimum;
			nudUnitSize.Maximum = 10;
			nudUnitSize.Value = currentSize;
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;

			switch (code)
			{
				case 30:
					UnitSize = int.Parse(nud.Value.ToString());
					break;
			}

			Points = UnitSize * DEFAULT_POINTS;
		}

		public override string ToString()
		{
			return "Hounds of Morkai - " + Points + "pts";
		}
	}
}