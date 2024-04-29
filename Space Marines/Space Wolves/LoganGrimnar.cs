using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class LoganGrimnar : Datasheets
	{
		public LoganGrimnar()
		{
			DEFAULT_POINTS = 140;
			UnitSize = 1;
			Points = DEFAULT_POINTS;
			TemplateCode = "1k_c";
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "CHARACTER", "TERMINATOR", "CHAPTER MASTER", "LOGAN GRIMNAR",
				"CHARIOT", "CHARACTER", "STORMRIDER", "CHAPTER MASTER", "LOGAN GRIMNAR"
			});
			Role = "HQ";
			WarlordTrait = "Aura of Majesty";
		}

		public override Datasheets CreateUnit()
		{
			return new LoganGrimnar();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as SpaceMarines;
			Template.LoadTemplate(TemplateCode, panel);

			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

			cmbFaction.Visible = false;
			cmbRelic.Visible = false;
			panel.Controls["cbStratagem1"].Visible = false;
			panel.Controls["cbStratagem2"].Visible = false;
			panel.Controls["lblRelic"].Visible = false;

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

			cbOption1.Text = "Riding Stormrider (+20 pts)";
			if (Weapons[0] == "")
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
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;

			switch (code)
			{
				case 21:
					if (cbOption1.Checked)
					{
						Weapons[0] = cbOption1.Text;
					}
					else { Weapons[0] = string.Empty; }
					break;
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

			Points = DEFAULT_POINTS;

			if (cbOption1.Checked)
			{
				Points += 20;
			}
		}

		public override string ToString()
		{
			return "Logan Grimnar - " + Points + "pts";
		}
	}
}
