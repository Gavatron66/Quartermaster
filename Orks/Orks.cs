using Roster_Builder.Tau_Empire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Orks
{
	public class Orks : Faction
	{
		public Orks()
		{
			subFactionName = "<Clan>";
			currentSubFaction = string.Empty;
			factionUpgradeName = "Specialist Mobs/Kustom Jobs";
			StratagemList.AddRange(new string[]
			{
				"Stratagem: Big Boss",
				"Stratagem: Extra Gubbinz",
			});
		}

		public override List<string> GetCustomSubfactionList1()
		{
			return new List<string> { };
		}

		public override List<string> GetCustomSubfactionList2()
		{
			return new List<string> { };
		}

		public override List<Datasheets> GetDatasheets()
		{
			return new List<Datasheets> {
				//---------- HQ ----------
				//new GhazghkullThraka(),
				//new Makari(),
				//new BigMekForceField(),
				//new BigMekShokkGun(),
				//new Warboss(),
				//new MegaWarboss(),
				//new Weirdboy(),
				//new MegaMek(),
				//new BossSnikrot(),
				//new BossZagstruk(),
				//new DeffkillaWartrike(),
				//new KaptinBadrukk(),
				//new ZodgrodWortsnagga(),
				//new Beastboss(),
				//new BeastbossSquigosaur(),
				//new Painboss(),
				//new Wurrboy(),
				//new Mozrog Skragbad(),
				//---------- Troops ----------
				//new Gretchin(),
				//new Boyz(),
				//new BeastSnaggaBoyz(),
				//---------- Elites ----------
				//new BannerNob(),
				//new Painboy(),
				//new MadDokGrotsnik(),
				//new Runtherd(),
				//new BurnaBoyz(),
				//new Mek(),
				//new Tankbustas(),
				//new Kommandos(),
				//new Meganobz(),
				//new Nobz(),
				//---------- Fast Attack ----------
				//new ShokkjumpDragstas(),
				//new DoomdakkaSnazzwagons(),
				//new Warbikers(),
				//new KustomBoostaBlastas(),
				//new SmashaSquigNob(),
				//new RukkatrukkSquigbuggies(),
				//new MegatrakkScrapjets(),
				//new Stormboyz(),
				//new SquighogBoyz(),
				//new Deffkoptas(),
				//---------- Heavy Support ----------
				//new MekGunz(),
				//new Battlewagon(),
				//new Bonebreaka(),
				//new Gunwagon(),
				//new KillKans(),
				//new Lootas(),
				//new DeffDreads(),
				//new KillRig(),
				//new FlashGitz(),
				//new HuntaRig(),
				//---------- Transport ----------
				//new Trukk(),
				//---------- Flyer ----------
				//new BurnaBommer(),
				//new Dakkajet(),
				//new BlitzaBommer(),
				//new WazbomBlastajet(),
				//---------- Lord of War ----------
				//new Morkanaut(),
				//new Gorkanaut(),
				//new Stompa(),
				//---------- Fortification ----------
				//new MekboyWorkshop(),
				//new BigEdBossbunka()
			};
		}

		public override int GetFactionUpgradePoints(string upgrade)
		{
			return 0;
		}

		public override List<string> GetFactionUpgrades(List<string> keywords)
		{
			List<string> upgrades = new List<string>()
			{
				"(None)"
			};

			if(keywords.Contains("VEHICLE"))
			{
				upgrades.Add("Da Booma");
				upgrades.Add("Fortress on Wheels");
				upgrades.Add("Gyroscopic Whirligig");
				upgrades.Add("More Dakka");
				upgrades.Add("Nitro Squigs");
				upgrades.Add("Red Rolla");
				upgrades.Add("Shokka Hull");
				upgrades.Add("Souped-up Speshul");
				upgrades.Add("Squig-hide Tyres");
				upgrades.Add("Stompamatic Pistons");
			}
			else
			{
				upgrades.Add("Bionik Oiler");
				upgrades.Add("Enhanced Runt-Sucker");
				upgrades.Add("Extra-Kustom Weapon");
				upgrades.Add("Smoky Gubbinz");
				upgrades.Add("Zzapkrumpaz");
			}

			if(keywords.Contains("BURNA BOYZ") || keywords.Contains("BURNA-BOMMER") || keywords.Contains("BOOMDAKKA SNAZZWAGON")
				|| keywords.Contains("DEFFKILLA WARTRIKE") || keywords.Contains("NOB") || keywords.Contains("WARBOSS") || keywords.Contains("NOBZ"))
			{
				upgrades.Add("Pyromaniacs Mob");
			}

			if(keywords.Contains("BIG MEK") || keywords.Contains("BLITZA-BOMMER") || keywords.Contains("DEFFKOPTAS")
				|| keywords.Contains("MEK") || keywords.Contains("NOBZ") || keywords.Contains("NOB")
				|| keywords.Contains("TANKBUSTAS") || keywords.Contains("WAGON") || keywords.Contains("WARBOSS"))
			{
				upgrades.Add("Boom Boyz Mob");
			}

			if(keywords.Contains("BLITZA-BOMMER") || keywords.Contains("BURNA-BOMMER") || keywords.Contains("DAKKAJET")
				|| keywords.Contains("DEFFKOPTAS") || keywords.Contains("WAZBOM BLASTAJET"))
			{
				upgrades.Add("Flyboyz Mob");
			}

			if(keywords.Contains("DEFF DREADS") || keywords.Contains("GORKANAUT") || keywords.Contains("MEGA ARMOUR")
				|| keywords.Contains("MORKANAUT"))
			{
				upgrades.Add("Big Krumpaz Mob");
			}

			if(keywords.Contains("BIKER") || keywords.Contains("CAVALRY") || keywords.Contains("CHARACTER")
				|| keywords.Contains("MOB"))
			{
				upgrades.Add("Madboyz Mob");
			}

			if(keywords.Contains("BOYZ") || keywords.Contains("KOMMANDOS") || keywords.Contains("NOB")
				|| keywords.Contains("NOBZ") || keywords.Contains("WARBOSS"))
			{
				upgrades.Add("Sneaky Gitz Mob");
			}

			if(keywords.Contains("BOYZ") || keywords.Contains("NOB") || keywords.Contains("NOBZ")
				|| keywords.Contains("WARBOSS"))
			{
				upgrades.Add("Trukk Boyz Mob");
			}

			if(keywords.Contains("GRETCHIN"))
			{
				upgrades.Add("'Orrible Gitz");
			}

			return upgrades;
		}

		public override bool GetIfEnabled(int index)
		{
			return true;
		}

		public override List<string> GetPsykerPowers(string keywords)
		{
			if(keywords == "Waaagh!")
			{
				return new List<string>()
				{
					"'Eadbanger",
					"Warpath",
					"Da Jump",
					"Fists of Gork",
					"Da Krunch",
					"Jabbin' Fingerz"
				};
			}
			else if (keywords == "Beasthead")
			{
				return new List<string>()
				{
					"Roar of Mork",
					"Frazzle",
					"Bitin' Jawz",
					"Spirit of Gork",
					"Beastscent",
					"Squiggly Curse"
				};
			}

			return new List<string>();
		}

		public override List<string> GetRelics(List<string> keywords)
		{
			List<string> relics = new List<string>();

			relics.Add("Supa-Cybork Body");
			relics.Add("Headwoppa's Killchoppa");
			relics.Add("Da Krushin' Armour");
			relics.Add("Da Killa Klaw");
			relics.Add("Da Ded Shiny Shoota");
			relics.Add("Scorched Gitbonez");
			relics.Add("Beasthide Mantle");

			if (currentSubFaction == "Goffs")
			{
				relics.Add("Da Irongob");
			}
			else if (currentSubFaction == "Bad Moons")
			{
				relics.Add("Da Gobshot Thunderbuss");
			}
			else if (currentSubFaction == "Evil Sunz")
			{
				relics.Add("Rezmekka's Redder Paint");
			}
			else if (currentSubFaction == "Snakebites")
			{
				relics.Add("Brogg's Buzzbomb");
			}
			else if (currentSubFaction == "Deathskulls")
			{
				relics.Add("Da Fixer Upperz");
			}
			else if (currentSubFaction == "Blood Axes")
			{
				relics.Add("Morgog's Finkin' Cap");
			}
			else if (currentSubFaction == "Freebooterz")
			{
				relics.Add("Da Badskull Banna");
			}

			return relics;
		}

		public override List<string> GetSubFactions()
		{
			return new List<string>()
			{
				"Bad Moons",
				"Blood Axes",
				"Deathskulls",
				"Evil Sunz",
				"Freebooterz",
				"Goffs",
				"Snakebites"
			};
		}

		public override List<string> GetWarlordTraits(string keyword)
		{
			List<string> traits = new List<string>();

			if(keyword == "BS")
			{
				traits.AddRange(new string[]
				{
					"Bigkilla Boss",
					"Beastgob",
					"Half-chewed"
				});
			}
			else if (keyword == "SF")
			{
				traits.AddRange(new string[]
				{
					"Roadkilla",
					"Get Up In Their Faces",
					"Junkboss"
				});
			}
			else
			{
				traits.AddRange(new string[]
				{
					"Follow Me Ladz",
					"Big Gob",
					"'Ard As Nails",
					"Brutal But Kunnin'",
					"Kunnin' But Brutal",
					"Might is Right"
				});
			}

			if(currentSubFaction == "Goffs")
			{
				traits.Add("Proper Killy");
			}
			else if (currentSubFaction == "Bad Moons")
			{
				traits.Add("Da Best Armour Teef Can Buy");
			}
			else if (currentSubFaction == "Evil Sunz")
			{
				traits.Add("Fasta Than Yooz");
			}
			else if (currentSubFaction == "Snakebites")
			{
				traits.Add("Surly as a Squiggoth");
			}
			else if (currentSubFaction == "Deathskulls")
			{
				traits.Add("Opportunist");
			}
			else if (currentSubFaction == "Blood Axes")
			{
				traits.Add("I've Got a Plan Ladz!");
			}
			else if (currentSubFaction == "Freebooterz")
			{
				traits.Add("Killa Reputation");
			}

			return traits;
		}

		public override void SetPoints(int points)
		{
		}

		public override string ToString()
		{
			return "Orks";
		}
	}
}
