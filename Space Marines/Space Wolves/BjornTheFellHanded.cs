using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class BjornTheFellHanded : Datasheets
	{
		public BjornTheFellHanded()
		{
			DEFAULT_POINTS = 155;
			UnitSize = 1;
			Points = DEFAULT_POINTS;
			TemplateCode = "1m_c";
			Weapons.Add("Assault Cannon");
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"VEHICLE", "CHARACTER", "DREADNOUGHT", "SMOKESCREEN", "BJORN THE FELL-HANDED"
			});
			Role = "HQ";
			WarlordTrait = "Aura of Majesty";
		}

		public override Datasheets CreateUnit()
		{
			return new BjornTheFellHanded();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as SpaceMarines;
			Template.LoadTemplate(TemplateCode, panel);

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
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

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Assault Cannon",
				"Heavy Plasma Cannon",
				"Helfrost Cannon",
				"Multi-melta",
				"Twin Lascannon"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;

			switch (code)
			{
				case 11:
					Weapons[0] = cmbOption1.SelectedItem.ToString();
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
		}

		public override string ToString()
		{
			return "Bjorn the Fell-Handed- " + Points + "pts";
		}
	}
}
