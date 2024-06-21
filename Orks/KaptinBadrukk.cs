using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
	public class KaptinBadrukk : Datasheets
	{
		public KaptinBadrukk()
		{
			DEFAULT_POINTS = 95;
			Points = DEFAULT_POINTS;
			TemplateCode = "1k_c";
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"ORKS", "FREEBOOTERZ",
				"CHARACTER", "INFANTRY", "WARBOSS", "FLASH GITZ", "KAPTIN BADRUKK"
			});
			Role = "HQ";
			WarlordTrait = "Killa Reputation";
		}

		public override Datasheets CreateUnit()
		{
			return new KaptinBadrukk();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as Orks;
			Template.LoadTemplate(TemplateCode, panel);

			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

			panel.Controls["lblRelic"].Visible = false;
			panel.Controls["cmbRelic"].Visible = false;
			panel.Controls["cbStratagem1"].Visible = false;
			panel.Controls["cbStratagem2"].Visible = false;

			cbOption1.Text = "Ammo Runt (+5 pts)";
			if (Weapons[0] == cbOption1.Text)
			{
				cbOption1.Checked = true;
			}
			else
			{
				cbOption1.Checked = false;
			}

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
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

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

			Points = DEFAULT_POINTS;

			Points += repo.GetFactionUpgradePoints(Factionupgrade);

			if (cbOption1.Checked)
			{
				Points += 5;
			}
		}

		public override string ToString()
		{
			return "Kaptin Badrukk - " + Points + "pts";
		}
	}
}