using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
	public class Drukhari : Faction
	{
		string kabal;
		string wychCult;
		string coven;

		public Drukhari()
		{
			subFactionName = "<Realspace Raid>";
			currentSubFaction = string.Empty;
			factionUpgradeName = "Lords of Commorragh";
            customSubFactionTraits = new string[6] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
            StratagemList.AddRange(new string[]
			{
				"Stratagem: Tolerated Ambition",
				"Stratagem: Prizes From the Dark City",
				"Stratagem: Alliance of Agony"
			});
		}

		public override List<string> GetCustomSubfactionList1()
		{
			return new List<string>();
		}

		public override List<string> GetCustomSubfactionList2()
		{
			return new List<string>();
		}

		public override List<Datasheets> GetDatasheets()
		{
			var datasheets = new List<Datasheets>()
			{
				//---------- HQ ----------
				new Archon(),
				new Succubus(),
				new Haemonculus(),
				new LelithHesperax(),
				new Drazhar(),
				new UrienRakarth(),
				//---------- Troops ----------
				new KabaliteWarriors(),
				new Wyches(),
				new Wracks(),
				//---------- Elites ----------
				new CourtOfTheArchon(),
				new Incubi(),
				new Mandrakes(),
				new Grotesques(),
				new Beastmaster(),
				//---------- Fast Attack ----------
				new ClawedFiends(),
				new RazorwingFlock(),
				new Khymerae(),
				new Reavers(),
				new Hellions(),
				new Scourges(),
				//---------- Heavy Support ----------
				new Talos(),
				new Cronos(),
				new Ravager(),
				//---------- Transport ----------
				new Raider(),
				new Venom(),
				//---------- Flyer ----------
				new RazorwingJetfighter(),
				new VoidravenBomber()
			};

			return datasheets;
		}

		public override int GetFactionUpgradePoints(string upgrade)
		{
			if(upgrade == "Splintered Genius (+15 pts)" || upgrade == "Show Stealer (+15 pts)")
			{
				return 15;
			}

			if(upgrade == "Alchemical Maestro (+20 pts)")
			{
				return 20;
			}

			if(upgrade == "Kabalite Trueborn (+3 pts/model)" || upgrade == "Hekatrix Bloodbrides (+3 pts/model")
			{
				return 3;
			}

			if(upgrade == "Haemoxytes (+2 pts/model)")
			{
				return 2;
			}

			return 0;
		}

		public override List<string> GetFactionUpgrades(List<string> keywords)
		{
			List<string> list = new List<string>() { "(None)" };

			if(keywords.Contains("ARCHON"))
			{
				list.Add("Splintered Genius (+15 pts)");
			}
			if (keywords.Contains("SUCCUBUS"))
			{
				list.Add("Show Stealer (+15 pts)");
			}
			if (keywords.Contains("HAEMONCULUS"))
			{
				list.Add("Alchemical Maestro (+20 pts)");
			}
			if (keywords.Contains("KABALITE WARRIORS"))
			{
				list.Add("Kabalite Trueborn (+3 pts/model)");
			}
			if (keywords.Contains("WYCHES"))
			{
				list.Add("Hekatrix Bloodbrides (+3 pts/model");
			}
			if (keywords.Contains("WRACKS"))
			{
				list.Add("Haemoxytes (+2 pts/model)");
			}

			return list;
		}

		public override bool GetIfEnabled(int index)
		{
			return true;
		}

		public override List<string> GetPsykerPowers(string keywords)
		{
			return new List<string>();
		}

		public override List<string> GetRelics(List<string> keywords)
		{
			List<string> relics = new List<string>();

			relics.Add("(None)");

			if(keywords.Contains("ARCHON") || keywords.Contains("SUCCUBUS"))
            {
                relics.Add("Parasite's Kiss");
            }

			relics.Add("The Helm of Spite");

			if(keywords.Contains("HAEMONCULUS"))
            {
                relics.Add("The Nightmare Doll");
            }

            if (keywords.Contains("ARCHON"))
            {
                relics.Add("The Djin Blade");
            }

			relics.Add("The Animus Vitae");

            if (keywords.Contains("SUCCUBUS"))
            {
                relics.Add("The Triptych Whip");
            }

			return relics;
		}

		public override List<string> GetSubFactions()
		{
			return new List<string>() 
			{ 
				string.Empty,
			};
		}

		public override List<string> GetWarlordTraits(string keyword)
		{
			List<string> traits = new List<string>();

			if(keyword.Contains("<KABAL>"))
			{
				traits.AddRange(new string[]
				{
					"Hatred Eternal",
					"Soul Thirst",
					"Ancient Evil"
				});
			}
			if (keyword.Contains("<WYCH CULT>"))
			{
				traits.AddRange(new string[]
				{
					"Quicksilver Fighter",
					"Stimm Addict",
					"Precision Blows"
				});
			}
			if (keyword.Contains("<HAEMONCULUS COVEN>"))
			{
				traits.AddRange(new string[]
				{
					"Master Regenesist",
					"Master Nemesine",
					"Master Artisan"
				});
			}

			return traits;
		}

        public override void SaveSubFaction(int code, Panel panel)
        {
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            ComboBox cmbSubCustom2 = panel.Controls["cmbSubCustom2"] as ComboBox;
            ComboBox cmbSubCustom3 = panel.Controls["cmbSubCustom3"] as ComboBox;
            ComboBox cmbSubCustom4 = panel.Controls["cmbSubCustom4"] as ComboBox;
            ComboBox cmbSubCustom5 = panel.Controls["cmbSubCustom5"] as ComboBox;
            ComboBox cmbSubCustom6 = panel.Controls["cmbSubCustom6"] as ComboBox;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;
            Label lblSubCustom2 = panel.Controls["lblSubCustom2"] as Label;
            Label lblSubCustom3 = panel.Controls["lblSubCustom3"] as Label;
            Label lblSubCustom4 = panel.Controls["lblSubCustom4"] as Label;
            Label lblSubCustom5 = panel.Controls["lblSubCustom5"] as Label;
            Label lblSubCustom6 = panel.Controls["lblSubCustom6"] as Label;

            switch (code)
            {
                case 51:
					kabal = cmbSubCustom1.SelectedItem.ToString();
					if(kabal == "<Custom Kabal>" && !cmbSubCustom4.Items.Contains("<Custom Kabal>"))
					{
						cmbSubCustom4.Items.Insert(cmbSubCustom4.Items.Count, kabal);
					}
					else if (cmbSubCustom4.Items.Contains("<Custom Kabal>"))
					{
						cmbSubCustom4.Items.Remove("<Custom Kabal>");
					}
                    break;
                case 52:
                    wychCult = cmbSubCustom2.SelectedItem.ToString();
                    if (wychCult == "<Custom Wych Cult>" && !cmbSubCustom4.Items.Contains("<Custom Wych Cult>"))
                    {
                        cmbSubCustom4.Items.Insert(cmbSubCustom4.Items.Count, wychCult);
                    }
                    else if (cmbSubCustom4.Items.Contains("<Custom Wych Cult>"))
                    {
                        cmbSubCustom4.Items.Remove("<Custom Wych Cult>");
                    }
                    break;
                case 53:
                    coven = cmbSubCustom3.SelectedItem.ToString();
                    if (coven == "<Custom Coven>" && !cmbSubCustom4.Items.Contains("<Custom Coven>"))
                    {
                        cmbSubCustom4.Items.Insert(cmbSubCustom4.Items.Count, coven);
                    }
                    else if (cmbSubCustom4.Items.Contains("<Custom Coven>"))
                    {
                        cmbSubCustom4.Items.Remove("<Custom Coven>");
                    }
                    break;
				case 54:
                    if (cmbSubCustom4.SelectedItem.ToString() == "<Custom Kabal>")
                    {
						cmbSubCustom5.Visible = true;
                        cmbSubCustom6.Visible = true;
						lblSubCustom5.Visible = true;
                        lblSubCustom6.Visible = true;

                        cmbSubCustom5.Items.Clear();
						cmbSubCustom5.Items.AddRange(new string[]
                        {
                            string.Empty,
                            "Dark Mirth",
                            "Deadly Deceivers (AC)",
                            "Disdain for Lesser Beings",
                            "Merciless Razorkin",
                            "Torturous Efficiency",
                            "Mobile Raiders",
                            "Soul Bond",
                            "Toxin Crafters (AC)",
                            "Twisted Hunters",
                            "Webway Raiders"
                        });

                        cmbSubCustom6.Items.Clear();
                        cmbSubCustom6.Items.AddRange(new string[]
                        {
                            string.Empty,
                            "Dark Mirth",
                            "Disdain for Lesser Beings",
                            "Merciless Razorkin",
                            "Torturous Efficiency",
                            "Mobile Raiders",
                            "Soul Bond",
                            "Twisted Hunters",
                            "Webway Raiders"
                        });

                        cmbSubCustom5.SelectedIndex = cmbSubCustom5.Items.IndexOf(customSubFactionTraits[0]);
                        cmbSubCustom6.SelectedIndex = cmbSubCustom6.Items.IndexOf(customSubFactionTraits[1]);
                    }
                    else if (cmbSubCustom4.SelectedItem.ToString() == "<Custom Wych Cult>")
                    {
                        cmbSubCustom5.Visible = true;
                        cmbSubCustom6.Visible = true;
                        lblSubCustom5.Visible = true;
                        lblSubCustom6.Visible = true;

                        cmbSubCustom5.Items.Clear();
                        cmbSubCustom5.Items.AddRange(new string[]
                        {
							string.Empty,
                            "Acrobatic Display",
                            "The Art of Pain",
                            "Berserk Fugue (AC)",
                            "Precise Killers",
                            "Slashing Impact",
                            "Stimulant Innovators",
                            "Test of Skill",
                            "Trophy Takers",
                            "Agile Hunters (AC)"
                        });

                        cmbSubCustom6.Items.Clear();
                        cmbSubCustom6.Items.AddRange(new string[]
                        {
                            string.Empty,
                            "Acrobatic Display",
                            "The Art of Pain",
                            "Precise Killers",
                            "Slashing Impact",
                            "Stimulant Innovators",
                            "Test of Skill",
                            "Trophy Takers",
                        });

                        cmbSubCustom5.SelectedIndex = cmbSubCustom5.Items.IndexOf(customSubFactionTraits[2]);
                        cmbSubCustom6.SelectedIndex = cmbSubCustom6.Items.IndexOf(customSubFactionTraits[3]);
                    }
                    else if (cmbSubCustom4.SelectedItem.ToString() == "<Custom Coven>")
                    {
                        cmbSubCustom5.Visible = true;
                        cmbSubCustom6.Visible = true;
                        lblSubCustom5.Visible = true;
                        lblSubCustom6.Visible = true;

                        cmbSubCustom5.Items.Clear();
                        cmbSubCustom5.Items.AddRange(new string[]
                        {
                            string.Empty,
                            "Artists of the Flesh (AC)",
                            "Dark Harvest",
                            "Dark Technomancers (AC)",
                            "Experimental Creations",
                            "Hungry for Flesh",
                            "Masters of Mutagens",
                            "Master Torturers",
                            "Obsessive Collectors",
                            "Enhanced Sensory Organs",
                            "Splinterblades"
                        });

                        cmbSubCustom6.Items.Clear();
                        cmbSubCustom6.Items.AddRange(new string[]
                        {
                            string.Empty,
                            "Dark Harvest",
                            "Experimental Creations",
                            "Hungry for Flesh",
                            "Masters of Mutagens",
                            "Master Torturers",
                            "Obsessive Collectors",
                            "Enhanced Sensory Organs",
                            "Splinterblades"
                        });

                        cmbSubCustom5.SelectedIndex = cmbSubCustom5.Items.IndexOf(customSubFactionTraits[4]);
                        cmbSubCustom6.SelectedIndex = cmbSubCustom6.Items.IndexOf(customSubFactionTraits[5]);
                    }
                    else
                    {
                        cmbSubCustom5.Visible = false;
                        cmbSubCustom6.Visible = false;
                        lblSubCustom5.Visible = false;
                        lblSubCustom6.Visible = false;
                    }

                    break;
				case 55:
					if(cmbSubCustom4.SelectedItem.ToString() == "<Custom Kabal>")
					{
						customSubFactionTraits[0] = cmbSubCustom5.SelectedItem.ToString();

						if (customSubFactionTraits[0].Contains("(AC)"))
						{
							cmbSubCustom6.SelectedIndex = 0;
							cmbSubCustom6.Enabled = false;
						}
						else
						{
							cmbSubCustom6.Enabled = true;
						}
					}
					else if (cmbSubCustom4.SelectedItem.ToString() == "<Custom Wych Cult>")
                    {
                        customSubFactionTraits[2] = cmbSubCustom5.SelectedItem.ToString();

                        if (customSubFactionTraits[2].Contains("(AC)"))
                        {
                            cmbSubCustom6.SelectedIndex = 0;
                            cmbSubCustom6.Enabled = false;
                        }
                        else
                        {
                            cmbSubCustom6.Enabled = true;
                        }
                    }
					else if (cmbSubCustom4.SelectedItem.ToString() == "<Custom Coven>")
                    {
                        customSubFactionTraits[4] = cmbSubCustom5.SelectedItem.ToString();

                        if (customSubFactionTraits[4].Contains("(AC)"))
                        {
                            cmbSubCustom6.SelectedIndex = 0;
                            cmbSubCustom6.Enabled = false;
                        }
                        else
                        {
                            cmbSubCustom6.Enabled = true;
                        }
                    }

                    break;
                case 56:
                    if (cmbSubCustom4.SelectedItem.ToString() == "<Custom Kabal>")
                    {
                        customSubFactionTraits[1] = cmbSubCustom6.SelectedItem.ToString();
                    }
                    else if (cmbSubCustom4.SelectedItem.ToString() == "<Custom Wych Cult>")
                    {
                        customSubFactionTraits[3] = cmbSubCustom6.SelectedItem.ToString();
                    }
                    else if (cmbSubCustom4.SelectedItem.ToString() == "<Custom Coven>")
                    {
                        customSubFactionTraits[5] = cmbSubCustom6.SelectedItem.ToString();
                    }

                    break;
            }

			if(cmbSubCustom4.Items.Count > 1)
			{
				cmbSubCustom4.Visible = true;
				lblSubCustom4.Visible = true;
			}
			else
            {
				cmbSubCustom4.Visible = false;
                lblSubCustom4.Visible = false;
            }
        }

        public override void SetPoints(int points)
		{

		}

        public override void SetSubFactionPanel(Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            antiLoop = true;
            Template template = new Template();
            template.LoadFactionTemplate(6, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            ComboBox cmbSubCustom2 = panel.Controls["cmbSubCustom2"] as ComboBox;
            ComboBox cmbSubCustom3 = panel.Controls["cmbSubCustom3"] as ComboBox;
            ComboBox cmbSubCustom4 = panel.Controls["cmbSubCustom4"] as ComboBox;
            ComboBox cmbSubCustom5 = panel.Controls["cmbSubCustom5"] as ComboBox;
            ComboBox cmbSubCustom6 = panel.Controls["cmbSubCustom6"] as ComboBox;
            Label lblSubfaction = panel.Controls["lblSubfaction"] as Label;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;
            Label lblSubCustom2 = panel.Controls["lblSubCustom2"] as Label;
            Label lblSubCustom3 = panel.Controls["lblSubCustom3"] as Label;
            Label lblSubCustom4 = panel.Controls["lblSubCustom4"] as Label;
            Label lblSubCustom5 = panel.Controls["lblSubCustom5"] as Label;
            Label lblSubCustom6 = panel.Controls["lblSubCustom6"] as Label;

			cmbSubFaction.Visible = false;
			lblSubfaction.Visible = false;

			cmbSubCustom1.Visible = true;
            cmbSubCustom2.Visible = true;
            cmbSubCustom3.Visible = true;
            lblSubCustom1.Visible = true;
            lblSubCustom2.Visible = true;
            lblSubCustom3.Visible = true;
			lblSubCustom4.Text = "Select a Custom: ";

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
            panel.BringToFront();

            cmbSubCustom1.Items.Clear();
            cmbSubCustom2.Items.Clear();

            cmbSubCustom1.Items.AddRange(new string[]
            {
                string.Empty,
				"Kabal of the Black Heart",
				"Kabal of the Poisoned Tongue",
				"Kabal of the Flayed Skull",
				"Kabal of the Obsidian Rose",
				"<Custom Kabal>"

            });

            cmbSubCustom2.Items.AddRange(new string[]
            {
                string.Empty,
                "Cult of Strife",
                "Cult of the Cursed Blade",
                "Cult of the Red Grief",
                "<Custom Wych Cult>"
            });

            cmbSubCustom3.Items.AddRange(new string[]
            {
				string.Empty,
                "The Prophets of Flesh",
                "The Dark Creed",
                "The Coven of Twelve",
                "<Custom Coven>"
            });

			cmbSubCustom4.Items.AddRange(new string[]
			{
				string.Empty
			});

            cmbSubCustom5.Items.AddRange(new string[]
            {
                string.Empty
            });

            cmbSubCustom6.Items.AddRange(new string[]
            {
                string.Empty
            });

            antiLoop = false;
        }

        public override string ToString()
		{
			return "Drukhari";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
