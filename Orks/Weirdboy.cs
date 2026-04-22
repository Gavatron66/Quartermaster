using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
	public class Weirdboy : Datasheets
	{
		public Weirdboy()
		{
			DEFAULT_POINTS = 70;
			UnitSize = 1;
			Points = DEFAULT_POINTS;
			TemplateCode = "pc";
			Keywords.AddRange(new string[]
			{
				"ORKS", "<CLAN>",
				"CHARACTER", "INFANTRY", "PSYKER", "WEIRDBOY"
			});
			PsykerPowers = new string[2] { string.Empty, string.Empty };
			Role = "HQ";
		}

		public override Datasheets CreateUnit()
		{
			return new Weirdboy();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as Orks;
			Template.LoadTemplate(TemplateCode, panel);

			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			Label lblPsyker = panel.Controls["lblPsyker"] as Label;
			CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
			ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
			CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
			CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

			panel.Controls["cmbFactionUpgrade"].Visible = true;
			panel.Controls["lblFactionUpgrade"].Visible = true;

			cmbWarlord.Items.Clear();
			List<string> traits = repo.GetWarlordTraits("");
			foreach (var item in traits)
			{
				cmbWarlord.Items.Add(item);
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

			List<string> psykerpowers = new List<string>();
			psykerpowers = repo.GetPsykerPowers("Waaagh!");
			clbPsyker.Items.Clear();
			foreach (string power in psykerpowers)
			{
				clbPsyker.Items.Add(power);
			}

			lblPsyker.Text = "Select two of the following:";
			clbPsyker.ClearSelected();
			for (int i = 0; i < clbPsyker.Items.Count; i++)
			{
				clbPsyker.SetItemChecked(i, false);
			}

			if (PsykerPowers[0] != string.Empty)
			{
				clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
			}
			if (PsykerPowers[1] != string.Empty)
			{
				clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[1]), true);
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
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
			CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
			CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

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
				case 17:
					string chosenRelic = cmbRelic.SelectedItem.ToString();
					Relic = chosenRelic;
					break;
				case 25:
					if (cbWarlord.Checked)
					{
						this.isWarlord = true;
					}
					else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
					break;
				case 60:
					if (clbPsyker.CheckedItems.Count < 2)
					{
						break;
					}
					else if (clbPsyker.CheckedItems.Count == 2)
					{
						PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
						PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
					}
					else
					{
						clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
					}

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
			return "Weirdboy - " + Points + "pts";
		}
	}
}
