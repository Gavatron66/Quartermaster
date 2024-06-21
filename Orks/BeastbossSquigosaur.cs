using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
	public class BeastbossSquigosaur : Datasheets
	{
		public BeastbossSquigosaur()
		{
			DEFAULT_POINTS = 160;
			UnitSize = 1;
			Points = DEFAULT_POINTS;
			TemplateCode = "1k_c";
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"ORKS", "<CLAN>",
				"CHARACTER", "CAVALRY", "SQUIG", "BEAST SNAGGA", "WARBOSS", "BEASTBOSS"
			});
			Role = "HQ";
		}

		public override Datasheets CreateUnit()
		{
			return new BeastbossSquigosaur();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as Orks;
			Template.LoadTemplate(TemplateCode, panel);

			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

			cmbWarlord.Items.Clear();
			List<string> traits = repo.GetWarlordTraits("BS");
			foreach (var item in traits)
			{
				cmbWarlord.Items.Add(item);
			}

			cbOption1.Text = "Thump Gun (+5 pts)";
			if (Weapons[0] != string.Empty)
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

			cmbFaction.Items.Clear();
			cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

			if (Factionupgrade != null)
			{
				cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
			}
			else
			{
				cmbFaction.SelectedIndex = 0;
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
			panel.Controls["cmbFactionupgrade"].Visible = true;

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
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
			CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;

			switch (code)
			{
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
					Factionupgrade = cmbFaction.Text;
					break;
				case 17:
					string chosenRelic = cmbRelic.SelectedItem.ToString();
					Relic = chosenRelic;
					break;
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
			return "Beastboss on Squigosaur - " + Points + "pts";
		}
	}
}
