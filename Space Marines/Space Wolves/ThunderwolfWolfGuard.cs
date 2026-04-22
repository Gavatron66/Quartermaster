using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class ThunderwolfWolfGuard : Datasheets
	{
		public ThunderwolfWolfGuard()
		{
			DEFAULT_POINTS = 95;
			Points = DEFAULT_POINTS;
			TemplateCode = "2m_c";
			Weapons.Add("Bolt Pistol");
			Weapons.Add("Astartes Chainsword");
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"CAVALRY", "CHARACTER", "LIEUTENANT", "WOLF GUARD BATTLE LEADER"
			});
			Role = "HQ";
		}

		public override Datasheets CreateUnit()
		{
			return new ThunderwolfWolfGuard();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			repo = f as SpaceMarines;
			Template.LoadTemplate(TemplateCode, panel);

			ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
			ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

			cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Astartes Chainsword",
				"Boltgun",
				"Bolt Pistol",
				"Combi-flamer",
				"Combi-grav",
				"Combi-melta",
				"Combi-plasma",
				"Lightning Claw",
				"Plasma Pistol",
				"Power Axe",
				"Power Fist",
				"Power Maul",
				"Power Sword",
				"Storm Bolter",
				"Thunder Hammer (+10 pts)"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Astartes Chainsword",
				"Lightning Claw",
				"Power Axe",
				"Power Fist",
				"Power Maul",
				"Power Sword",
				"Storm Shield",
				"Thunder Hammer (+10 pts)"
			});
			cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

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
			ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
			CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
			ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
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
				case 17:
					string chosenRelic = cmbRelic.SelectedItem.ToString();
					cmbOption1.Enabled = true;
					cmbOption2.Enabled = true;
					Relic = chosenRelic;
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

			foreach (string weapon in Weapons)
			{
				if (weapon == "Thunder Hammer (+10 pts)")
				{
					Points += 10;
				}
			}
		}

		public override string ToString()
		{
			return "Wolf Guard Battle Leader on Thunderwolf - " + Points + "pts";
		}
	}
}