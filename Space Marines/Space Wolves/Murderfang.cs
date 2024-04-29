using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class Murderfang : Datasheets
	{
		public Murderfang()
		{
			DEFAULT_POINTS = 140;
			Points = DEFAULT_POINTS;
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"VEHICLE", "CHARACTER", "DREADNOUGHT", "WULFEN", "MURDERFANG"
			});
			Role = "Elites";
		}

		public override Datasheets CreateUnit()
		{
			return new Murderfang();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
		}

		public override string ToString()
		{
			return "Murderfang - " + Points + "pts";
		}
	}
}
