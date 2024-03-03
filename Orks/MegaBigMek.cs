using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
	public class MegaBigMek : Datasheets
	{
		public MegaBigMek()
		{
			DEFAULT_POINTS = 85;
			Points = DEFAULT_POINTS;
			TemplateCode = "2m1k_c";
			Weapons.Add("Kustom Mega-blasta");
			Weapons.Add("(None)");
			Weapons.Add("");
			Keywords.AddRange(new string[]
			{
				"ORKS", "<CLAN>",
				"CHARACTER", "INFANTRY", "MEGA ARMOUR", "BIG MEK"
			});
			Role = "HQ";
		}

		public override Datasheets CreateUnit()
		{
			return new MegaBigMek();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as Orks;
			Template.LoadTemplate(TemplateCode, panel);
			panel.Controls["cmbFactionUpgrade"].Visible = true;
			panel.Controls["lblFactionUpgrade"].Visible = true;

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
			ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Killsaw (+5 pts)",
				"Kombi-rokkit",
				"Kombi-scorcha",
				"Kustom Mega-blasta",
				"Kustom Shoota"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"(None)",
				"Kustom Force Field (+30 pts)",
				"Tellyport Blasta (+10 pts)"
			});
			cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

			cbOption1.Text = "Grot Oiler (+5 pts)";
			if (Weapons[2] == cbOption1.Text)
			{
				cbOption1.Checked = true;
			}
			else
			{
				cbOption1.Checked = false;
			}

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
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
			ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
			CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
			CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

			switch (code)
			{
				case 11:
					Weapons[0] = cmbOption1.SelectedItem.ToString();
					break;
				case 12:
					Weapons[1] = cmbOption2.SelectedItem.ToString();
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
					Factionupgrade = cmbFaction.Text;
                    cbOption1.Enabled = true;
					cmbOption1.Enabled = true;
                    if (Factionupgrade == "Bionik Oiler")
					{
						cbOption1.Checked = true;
						cbOption1.Enabled = false;
                    }
                    else if (Factionupgrade == "Extra-Kustom Weapon")
                    {
						cmbOption1.SelectedIndex = 3;
						cmbOption1.Enabled = false;
                    }
                    break;
				case 17:
					string chosenRelic = cmbRelic.SelectedItem.ToString();
					Relic = chosenRelic;
					break;
				case 21:
					if (cbOption1.Checked)
					{
						Weapons[2] = cbOption1.Text;
					}
					else
					{
						Weapons[2] = "";
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
				default: break;
			}

			Points = DEFAULT_POINTS;

			Points += repo.GetFactionUpgradePoints(Factionupgrade);

			if (cbOption1.Checked)
			{
				Points += 5;
			}

			if (Weapons.Contains("Killsaw (+5 pts)"))
			{
				Points += 5;
			}

			if (Weapons.Contains("Kustom Force Field (+30 pts)"))
			{
				Points += 30;
			}

			if (Weapons.Contains("Tellyport Blasta (+10 pts)"))
			{
				Points += 10;
			}
		}

		public override string ToString()
		{
			return "Big Mek in Mega Armour - " + Points + "pts";
		}
	}
}