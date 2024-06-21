using Roster_Builder.Tau_Empire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
				new GhazghkullThraka(),
				new Makari(),
				new BigMekForceField(),
				new BigMekShokkGun(),
				new Warboss(),
				new MegaWarboss(),
				new Weirdboy(),
				new MegaBigMek(),
				new BossSnikrot(),
				new BossZagstruk(),
				new DeffkillaWartrike(),
				new KaptinBadrukk(),
				new ZodgrodWortsnagga(),
				new Beastboss(),
				new BeastbossSquigosaur(),
				new Painboss(),
				new Wurrboy(),
				new MozrogSkragbad(),
				//---------- Troops ----------
				new Gretchin(),
				new Boyz(),
				new BeastSnaggaBoyz(),
				//---------- Elites ----------
				new BannerNob(),
				new Painboy(),
				new MadDokGrotsnik(),
				new Runtherd(),
				new BurnaBoyz(),
				new Mek(),
				new Tankbustas(),
				new Kommandos(),
				new Meganobz(),
				new Nobz(),
				//---------- Fast Attack ----------
				new ShokkjumpDragstas(),
				new BoomdakkaSnazzwagons(),
				new Warbikers(),
				new KustomBoostaBlastas(),
				new SmashaSquigNob(),
				new RukkatrukkSquigbuggies(),
				new MegatrakkScrapjets(),
				new Stormboyz(),
				new SquighogBoyz(),
				new Deffkoptas(),
				//---------- Heavy Support ----------
				new MekGunz(),
				new Battlewagon(),
				new Bonebreaka(),
				new Gunwagon(),
				new KillaKans(),
				new Lootas(),
				new DeffDreads(),
				new KillRig(),
				new FlashGitz(),
				new HuntaRig(),
				//---------- Transport ----------
				new Trukk(),
				//---------- Flyer ----------
				new BurnaBommer(),
				new Dakkajet(),
				new BlitzaBommer(),
				new WazbomBlastajet(),
				//---------- Lord of War ----------
				new Morkanaut(),
				new Gorkanaut(),
				new Stompa(),
				//---------- Fortification ----------
				new MekboyWorkshop(),
				new BigEdBossbunka()
			};
		}

		public override int GetFactionUpgradePoints(string upgrade)
		{
			int points = 0;

			if(upgrade == null)
			{
				return 0;
			}

			if (upgrade == "Da Booma (+15 pts)")
			{
				points += 15;
			}
			else if (upgrade == "Fortress on Wheels (+20 pts)")
			{
				points += 20;
			}
			else if (upgrade == "Gyroscopic Whirligig (+10 pts)")
			{
				points += 15;
			}
			else if (upgrade == "More Dakka (+15 pts)")
			{
				points += 15;
			}
			else if (upgrade == "More Dakka (+30 pts)")
			{
				points += 30;
			}
			else if (upgrade == "Nitro Squigs (+25 pts)")
			{
				points += 25;
			}
			else if (upgrade == "Red Rolla (+20 pts)")
			{
				points += 20;
			}
			else if (upgrade == "Shokka Hull (+15 pts)")
			{
				points += 15;
			}
			else if (upgrade == "Shokka Hull (+30 pts)")
			{
				points += 30;
			}
			else if (upgrade == "Souped-up Speshul (+10 pts)")
			{
				points += 10;
			}
			else if (upgrade == "Squig-hide Tyres (+15 pts)")
			{
				points += 15;
			}
			else if (upgrade == "Stompamatic Pistons (+15 pts)")
			{
				points += 15;
			}
			else if (upgrade == "Stompamatic Pistons (+30 pts)")
			{
				points += 30;
			}
			else if (upgrade == "Bionik Oiler (+10 pts)")
			{
				points += 10;
			}
			else if (upgrade == "Enhanced Runt-Sucker (+15 pts)")
			{
				points += 15;
			}
			else if (upgrade == "Extra-Kustom Weapon (+10 pts)")
			{
				points += 10;
			}
			else if (upgrade == "(None)" || upgrade.Contains("Mob"))
			{

			}
			else if(upgrade != "")
			{
				points += Convert.ToInt32(upgrade);
			}

            return points;
		}

		public override List<string> GetFactionUpgrades(List<string> keywords)
		{
			List<string> upgrades = new List<string>()
			{
				"(None)"
			};

			if(keywords.Contains("VEHICLE"))
			{
				if(keywords.Contains("WAGON"))
				{
					upgrades.Add("Da Booma (+15 pts)");
				}

				if(keywords.Contains("TRUKK") || keywords.Contains("WAGON"))
				{
					upgrades.Add("Fortress on Wheels (+20 pts)");
				}

                if (keywords.Contains("SHOKKJUMP DRAGSTA"))
                {
                    upgrades.Add("Gyroscopic Whirligig (+10 pts)");
                }

				if(keywords.Contains("WAZBOM BLASTAJET") || keywords.Contains("BONEBREAKA")
					|| keywords.Contains("GUNWAGON") || keywords.Contains("KILL RIG")
					|| keywords.Contains("GORKANAUT") || keywords.Contains("MORKANAUT")
                    || keywords.Contains("STOMPA"))
				{
					upgrades.Add("More Dakka (+30 pts)");
				} 
				else
				{
                    upgrades.Add("More Dakka (+15 pts)");
                }

                if (keywords.Contains("RUKKATRUKK SQUIGBUGGIES"))
                {
                    upgrades.Add("Nitro Squigs (+25 pts)");
                }

                if (keywords.Contains("BONEBREKA"))
                {
                    upgrades.Add("Red Rolla (+20 pts)");
                }

                if (keywords.Contains("WAZBOM BLASTAJET") || keywords.Contains("BONEBREAKA")
                    || keywords.Contains("GUNWAGON") || keywords.Contains("KILL RIG")
                    || keywords.Contains("GORKANAUT") || keywords.Contains("MORKANAUT")
                    || keywords.Contains("STOMPA"))
                {
                    upgrades.Add("Shokka Hull (+30 pts)");
                }
                else
                {
                    upgrades.Add("Shokka Hull (+15 pts)");
                }

                if (keywords.Contains("BOOMDAKKA SNAZZWAGONS"))
                {
                    upgrades.Add("Souped-up Speshul (+10 pts)");
                }

                if (!(keywords.Contains("WALKERZ") || keywords.Contains("AIRCRAFT")))
                {
                    upgrades.Add("Squig-hide Tyres (+15 pts)");
                }

                if (keywords.Contains("GORKANAUT") || keywords.Contains("MORKANAUT"))
                {
                    upgrades.Add("Stompamatic Pistons (+30 pts)");
                }

				if(keywords.Contains("DEFF DREAD"))
                {
                    upgrades.Add("Stompamatic Pistons (+15 pts)");
                }
			}
			else
            {
                if (keywords.Contains("BIG MEK") || keywords.Contains("MEK") && !keywords.Contains("WAZBOM BLASTAJET"))
                {
                    upgrades.Add("Bionik Oiler (+10 pts)");
                }

                if (keywords.Contains("SHOKK ATTACK GUN"))
                {
                    upgrades.Add("Enhanced Runt-Sucker (+15 pts)");
                }

                if ((keywords.Contains("BIG MEK") && keywords.Contains("MEGA ARMOUR")) || keywords.Contains("MEK") && !keywords.Contains("WAZBOM BLASTAJET")
					|| keywords.Contains("BURNA BOYZ")|| keywords.Contains("LOOTAS"))
                {
                    upgrades.Add("Extra-Kustom Weapon (+10 pts)");
                }

                if (keywords.Contains("BURNA BOYZ") || keywords.Contains("LOOTAS"))
                {
                    upgrades.Add("Smoky Gubbinz (+1 pts/model)");
                }

                if (keywords.Contains("BURNA BOYZ") || keywords.Contains("LOOTAS"))
                {
                    upgrades.Add("Zzapkrumpaz (+2 pts/model)");
                }
            }

            //----------------- Specialist Mobs -----------------

            if (keywords.Contains("BURNA BOYZ") || keywords.Contains("BURNA-BOMMER") || keywords.Contains("BOOMDAKKA SNAZZWAGONS")
				|| keywords.Contains("DEFFKILLA WARTRIKE") || keywords.Contains("NOB") || keywords.Contains("WARBOSS") || keywords.Contains("NOBZ")
				|| keywords.Contains("KUSTOM BOOSTA-BLASTAS"))
			{
				upgrades.Add("Pyromaniacs Mob");
			}

			if(keywords.Contains("BIG MEK") || keywords.Contains("DEFFKOPTAS")
				|| keywords.Contains("MEK") && !keywords.Contains("WAZBOM BLASTAJET") || keywords.Contains("NOBZ") || keywords.Contains("NOB")
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
			List<string> relics = new List<string>()
			{
				"(None)"
			};

			if(keywords.Contains("INFANTRY"))
            {
                relics.Add("Supa-Cybork Body");
            }

            if (keywords.Contains("WARBOSS") || keywords.Contains("NOB ON SMASHA SQUIG") 
				|| keywords.Contains("BIG MEK") && !(keywords.Contains("SHOKK ATTACK GUN") || keywords.Contains("MEGA ARMOUR")) 
				|| keywords.Contains("MEK"))
            {
                relics.Add("Headwoppa's Killchoppa");
            }

            if (keywords.Contains("MEGA ARMOUR"))
            {
                relics.Add("Da Krushin' Armour");
            }

            if ((keywords.Contains("MEGA ARMOUR") && keywords.Contains("BIG MEK")) || keywords.Contains("PAINBOY")
				|| (keywords.Contains("WARBOSS") && !(keywords.Contains("MEGA ARMOUR") || keywords.Contains("BEAST SNAGGA"))))
            {
                relics.Add("Da Killa Klaw");
            }

            if ((keywords.Contains("MEGA ARMOUR") && keywords.Contains("BIG MEK")) || (keywords.Contains("NOB") && !keywords.Contains("BEAST SNAGGA"))
                || (keywords.Contains("WARBOSS") && !(keywords.Contains("MEGA ARMOUR") || keywords.Contains("BEAST SNAGGA"))))
            {
                relics.Add("Da Ded Shiny Shoota");
            }

            if (keywords.Contains("PSYKER"))
            {
                relics.Add("Scorched Gitbonez");
            }

            if (keywords.Contains("BEAST SNAGGA"))
            {
                relics.Add("Beasthide Mantle");
            }

			if (currentSubFaction == "Goffs")
			{
				relics.Add("Da Irongob");
			}
			
			if (currentSubFaction == "Bad Moons" && ((keywords.Contains("MEGA ARMOUR") && keywords.Contains("BIG MEK")) 
				|| (keywords.Contains("NOB") && !keywords.Contains("BEAST SNAGGA"))
                || (keywords.Contains("WARBOSS") && !(keywords.Contains("MEGA ARMOUR") || keywords.Contains("BEAST SNAGGA")))))
			{
				relics.Add("Da Gobshot Thunderbuss");
			}
			
			if (currentSubFaction == "Evil Sunz")
			{
				relics.Add("Rezmekka's Redder Paint");
			}
			
			if (currentSubFaction == "Snakebites")
			{
				relics.Add("Brogg's Buzzbomb");
			}
			
			if (currentSubFaction == "Deathskulls" && (keywords.Contains("MEK") || keywords.Contains("BIG MEK")))
			{
				relics.Add("Da Fixer Upperz");
			}
			
			if (currentSubFaction == "Blood Axes")
			{
				relics.Add("Morgog's Finkin' Cap");
			}
			
			if (currentSubFaction == "Freebooterz")
			{
				relics.Add("Da Badskull Banna");
			}

			return relics;
		}

		public override List<string> GetSubFactions()
		{
			return new List<string>()
			{
				string.Empty,
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
					"'Ard as Nails",
					"Brutal but Kunnin'",
					"Kunnin' but Brutal",
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

        public override void SaveSubFaction(int code, Panel panel)
        {
            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;

            switch (code)
            {
                case 50:
                    currentSubFaction = cmbSubFaction.SelectedItem.ToString();
                    break;
            }
        }

        public override void SetPoints(int points)
		{
		}

        public override void SetSubFactionPanel(Panel panel)
        {
            Template template = new Template();
            template.LoadFactionTemplate(1, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
        }

        public override string ToString()
		{
			return "Orks";
		}
	}
}
