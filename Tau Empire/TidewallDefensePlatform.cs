using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
	public class TidewallDefensePlatform : Datasheets
	{
		public TidewallDefensePlatform()
		{
			DEFAULT_POINTS = 80;
			Points = DEFAULT_POINTS;
			UnitSize = 1;
			Keywords.AddRange(new string[]
			{
				"T'AU EMPIRE", "<SEPT>",
				"TERRAIN", "BUILDING", "VEHICLE", "TRANSPORT", "TIDEWALL", "DEFENCE PLATFORM"
			});
			Role = "Fortification";
		}

		public override Datasheets CreateUnit()
		{
			return new TidewallDefensePlatform();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{

		}

		public override void SaveDatasheets(int code, Panel panel)
		{

		}

		public override string ToString()
		{
			return "Tidewall Defence Platform - " + Points + "pts";
		}
	}
}
