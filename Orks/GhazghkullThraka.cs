using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
	public class GhazghkullThraka : Datasheets
	{
		public GhazghkullThraka()
		{
			DEFAULT_POINTS = 300;
			UnitSize = 1;
			TemplateCode = "nc";
			Points = DEFAULT_POINTS;
			Keywords.AddRange(new string[]
			{
				"ORKS", "GOFF",
				"CHARACTER", "MONSTER", "SUPREME COMMANDER", "WARBOSS", "GHAZGHKULL THRAKA"
			});
			WarlordTrait = "Proper Killy";
			Role = "HQ";
		}

		public override Datasheets CreateUnit()
		{
			return new GhazghkullThraka();
		}
		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as Orks;

			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

			cmbWarlord.Enabled = false;
			cmbWarlord.Items.Clear();
			cmbWarlord.Items.Add(WarlordTrait);
			cmbWarlord.SelectedIndex = 0;

			if (isWarlord)
			{
				cbWarlord.Checked = true;
			}
			else
			{
				cbWarlord.Checked = false;
			}
		}

		public override void SaveDatasheets(int code, Panel panel)
		{

			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

			switch (code)
			{
				case 25:
					if (cbWarlord.Checked)
					{
						this.isWarlord = true;
						cmbWarlord.Text = WarlordTrait;
						cmbWarlord.Enabled = false;
					}
					else { this.isWarlord = false; }
					break;
				default: break;
			}

			if (code == -1)
			{
				if (this.isWarlord)
				{
					cmbWarlord.Text = WarlordTrait;
					cmbWarlord.Enabled = false;
				}
			}
		}

		public override string ToString()
		{
			return "Ghazghkull Thraka - " + Points + "pts";
		}
	}
}
