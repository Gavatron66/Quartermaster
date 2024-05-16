using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Drukhari
{
	public class Drukhari : Faction
	{
		string wychCult;
		string coven;

		public Drukhari()
		{
			subFactionName = "<Kabal>";
			currentSubFaction = string.Empty;
			wychCult = "<Wych Cult>";
			coven = "<Haemonculus Coven>";
			factionUpgradeName = "Lords of Commorragh";
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
			return new List<string>();
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

		public override void SetPoints(int points)
		{

		}

		public override string ToString()
		{
			return "Drukhari";
		}
	}
}
