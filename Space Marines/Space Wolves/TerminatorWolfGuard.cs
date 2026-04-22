using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Space_Wolves
{
	public class TerminatorWolfGuard : Datasheets
    {
        List<int> restrictedIndexes2 = new List<int>();
        private string stratWarlordTrait;

        public TerminatorWolfGuard()
		{
			DEFAULT_POINTS = 75;
			Points = DEFAULT_POINTS;
			TemplateCode = "2m_c";
			Weapons.Add("Storm Bolter");
			Weapons.Add("Power Sword");
			Keywords.AddRange(new string[]
			{
				"IMPERIUM", "ADEPTUS ASTARTES", "SPACE WOLVES",
				"INFANTRY", "CHARACTER", "TERMINATOR", "LIEUTENANT", "WOLF GUARD", "BATTLE LEADER"
			});
			Role = "HQ";
		}

		public override Datasheets CreateUnit()
		{
			return new TerminatorWolfGuard();
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
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox; // For Stratagem 3

            cmbOption1.Items.Clear();
			cmbOption1.Items.AddRange(new string[]
			{
				"Chainfist",
				"Combi-flamer",
				"Combi-grav",
				"Combi-melta",
				"Combi-plasma",
				"Lightning Claw",
				"Power Axe",
				"Power Fist",
				"Power Maul",
				"Power Sword",
				"Storm Bolter",
				"Thunder Hammer (+5 pts)"
			});
			cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

			cmbOption2.Items.Clear();
			cmbOption2.Items.AddRange(new string[]
			{
				"Chainfist",
				"Lightning Claw",
				"Power Axe",
				"Power Fist",
				"Power Maul",
				"Power Sword",
				"Relic Blade",
				"Storm Shield",
				"Thunder Hammer (+5 pts)"
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

			if (Relic != null && cmbRelic.Items.Contains(Relic))
			{
				cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
			}
			else
			{
				cmbRelic.SelectedIndex = 0;
            }

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;

            cbStratagem3.Visible = true;
            cbStratagem3.Location = new System.Drawing.Point(cbStratagem2.Location.X, cbStratagem2.Location.Y + 32);
            cbStratagem3.Text = f.StratagemList[2];

            if (f.currentSubFaction != f.customSubFactionTraits[2] && f.customSubFactionTraits[2] != "Unknown")
            {
                cbStratagem4.Visible = true;
            }
            else
            {
                cbStratagem4.Visible = false;
            }

            cbStratagem4.Location = new System.Drawing.Point(cbStratagem3.Location.X, cbStratagem3.Location.Y + 32);
            cbStratagem4.Text = f.StratagemList[3];

            panel.Controls["lblOption6"].Visible = false;
            panel.Controls["lblOption6"].Location = new System.Drawing.Point(panel.Controls["lblWarlord"].Location.X, cmbWarlord.Location.Y + 33);
            cmbOption6.Visible = false;
            cmbOption6.Location = new System.Drawing.Point(panel.Controls["lblOption6"].Location.X, panel.Controls["lblOption6"].Location.Y + 23);
            cmbOption6.Items.Clear();
            cmbOption6.Items.AddRange(repo.GetWarlordTraits("Strat").ToArray());

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

            if (Stratagem.Contains(cbStratagem3.Text))
            {
                cbStratagem3.Checked = true;
                cbStratagem3.Enabled = true;
                cmbOption6.Visible = true;
                panel.Controls["lblOption6"].Visible = true;
                cmbOption6.SelectedIndex = cmbOption6.Items.IndexOf(stratWarlordTrait);
            }
            else
            {
                cbStratagem3.Checked = false;
                cbStratagem3.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem3.Text));
                cmbOption6.Visible = false;
                panel.Controls["lblOption6"].Visible = false;
            }

            if (Stratagem.Contains(cbStratagem4.Text))
            {
                cbStratagem4.Checked = true;
                cbStratagem4.Enabled = true;
            }
            else
            {
                cbStratagem4.Checked = false;
                cbStratagem4.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem4.Text));
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
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbOption6 = panel.Controls["cmbOption6"] as ComboBox;

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

                    if (chosenRelic == "The Burning Blade")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Sword");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "The Shield Eternal")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Storm Shield");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Black Death")
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Axe");
                        cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Morkai's Teeth Bolts")
                    {
                        //See the end of SaveDatasheets
                    }
                    else if (chosenRelic == "Frost Weapon")
                    {
                        restrictedIndexes2.AddRange(new int[] { 0, 3, 4, 6, 7, 8 });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Power Axe");
                        //See the end of SaveDatasheets
                    }
                    Relic = chosenRelic;
					break;
                case 19:
                    stratWarlordTrait = cmbOption6.SelectedItem as string;
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
                case 73:
                    if (cbStratagem3.Checked && !Stratagem.Contains(cbStratagem3.Text))
                    {
                        Stratagem.Add(cbStratagem3.Text);
                        cmbOption6.Visible = true;
                        panel.Controls["lblOption6"].Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Remove(cbStratagem3.Text);
                        }
                        cmbOption6.Visible = false;
                        panel.Controls["lblOption6"].Visible = false;
                        cmbOption6.SelectedIndex = -1;
                    }
                    break;
                case 74:
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                        cmbRelic.Items.Clear();
                        Keywords.Add("Strat");
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        Keywords.Remove("Strat");
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
                        }

                        cmbRelic.Items.Clear();
                        cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

                        if (cmbRelic.Items.Contains(Relic))
                        {
                            cmbRelic.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        }
                    }
                    break;
                default: break;
			}

			Points = DEFAULT_POINTS;

			Points += repo.GetFactionUpgradePoints(Factionupgrade);

			foreach (string weapon in Weapons)
			{
				if (weapon == "Thunder Hammer (+5 pts)")
				{
					Points += 5;
				}
            }

            restrictedIndexes.Clear();
            restrictedIndexes2.Clear();

            if (Relic == "Frost Weapon")
            {
                restrictedIndexes2.AddRange(new int[] { 0, 3, 4, 6, 7, 8 });
            }

            #region Bolt Relics
            if (Relic == "Morkai's Teeth Bolts")
            {
                restrictedIndexes.AddRange(new int[] { 0, 5, 6, 7, 8, 9, 11 });
                cmbOption1.SelectedIndex = 10;
            }
            #endregion

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
            this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
        }

		public override string ToString()
		{
			return "Terminator Wolf Guard Battle Leader - " + Points + "pts";
		}
	}
}