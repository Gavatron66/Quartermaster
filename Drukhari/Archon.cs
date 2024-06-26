﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
	public class Archon : Datasheets
	{
		public Archon()
		{
			DEFAULT_POINTS = 70;
			TemplateCode = "1m1k_c";
			Points = DEFAULT_POINTS;
			Weapons.Add("Power Sword");
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"AELDARI", "DRUKHARI", "<KABAL>",
				"INFANTRY", "CHARACTER", "ARCHON"
			});
			Role = "HQ";
		}

		public override Datasheets CreateUnit()
		{
			return new Archon();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as Drukhari;
			Template.LoadTemplate(TemplateCode, panel);

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
			ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;

			cmbWarlord.Items.Clear();
			List<string> traits = repo.GetWarlordTraits("<KABAL>");
			foreach (var item in traits)
			{
				cmbWarlord.Items.Add(item);
			}

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Agoniser",
				"Huskblade",
				"Power Sword",
				"Venomblade"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cbOption1.Text = "Blast Pistol";
			if (Weapons[1] == cbOption1.Text)
			{
				cbOption1.Checked = true;
			}
			else
			{
				cbOption1.Checked = false;
			}

			if (isWarlord)
			{
				cbWarlord.Checked = true;
				cmbWarlord.Enabled = true;
				cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
			}
			else
			{
				cbWarlord.Checked = false;
				cmbWarlord.Enabled = false;
			}

			cmbRelic.Items.Clear();
			cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

			if (Relic != null)
			{
				cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
			}
			else
			{
				cmbRelic.SelectedIndex = -1;
			}

			panel.Controls["lblFactionupgrade"].Visible = true;
			cmbFactionupgrade.Visible = true;
			cmbFactionupgrade.Items.Clear();
			cmbFactionupgrade.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

			if (Factionupgrade != null)
			{
				cmbFactionupgrade.SelectedIndex = cmbFactionupgrade.Items.IndexOf(Factionupgrade);
			}
			else
			{
				cmbFactionupgrade.SelectedIndex = 0;
			}

			CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
			CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

			if (Stratagem.Contains(cbStratagem1.Text))
			{
				cbStratagem1.Checked = true;
				cbStratagem1.Enabled = true;
			}
			else
			{
				cbStratagem1.Checked = false;
				cbStratagem1.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem1.Text));
			}

			if (Stratagem.Contains(cbStratagem2.Text))
			{
				cbStratagem2.Checked = true;
				cbStratagem2.Enabled = true;
			}
			else
			{
				cbStratagem2.Checked = false;
				cbStratagem2.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem2.Text));
			}
		}

		public override void SaveDatasheets(int code, Panel panel)
		{
			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
			ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;
			CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
			CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

			switch (code)
			{
				case 11:
					Weapons[0] = cmbOption1.SelectedItem.ToString();
					break;
				case 15:
					if (cmbWarlord.SelectedIndex != -1)
					{
						WarlordTrait = cmbWarlord.SelectedItem.ToString();
					}
					else
					{
						WarlordTrait = string.Empty;
					}
					break;
				case 16:
					Factionupgrade = cmbFactionupgrade.Text;
					break;
				case 17:
					Relic = cmbRelic.SelectedItem.ToString();
					break;
				case 21:
					if (cbOption1.Checked)
					{
						Weapons[1] = cbOption1.Text;
					}
					else
					{
						Weapons[1] = "";
					}
					break;
				case 25:
					if (cbWarlord.Checked)
					{
						this.isWarlord = true;
					}
					else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
					break;
				case 71:
					if (cbStratagem1.Checked)
					{
						Stratagem.Add(cbStratagem1.Text);
					}
					else
					{
						if (Stratagem.Contains(cbStratagem1.Text))
						{
							Stratagem.Remove(cbStratagem1.Text);
						}
					}
					break;
				case 72:
					if (cbStratagem2.Checked)
					{
						Stratagem.Add(cbStratagem2.Text);
					}
					else
					{
						if (Stratagem.Contains(cbStratagem2.Text))
						{
							Stratagem.Remove(cbStratagem2.Text);
						}
					}
					break;
			}

			Points = DEFAULT_POINTS;

			Points += repo.GetFactionUpgradePoints(Factionupgrade);
		}

		public override string ToString()
		{
			return "Archon - " + Points + "pts";
		}
	}
}
