using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class LukasTheTrickster : Datasheets
	{
		public LukasTheTrickster()
		{
			DEFAULT_POINTS = 70;
			UnitSize = 1;
			Points = DEFAULT_POINTS * UnitSize;
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "CHARACTER", "BLOOD CLAWS", "LUKAS THE TRICKSTER"
			});
			Role = "Elites";
		}

		public override Datasheets CreateUnit()
		{
			return new LukasTheTrickster();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
		}

		public override string ToString()
		{
			return "Lukas the Trickster - " + Points + "pts";
		}
	}
}
