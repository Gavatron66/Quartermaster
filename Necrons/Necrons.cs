using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Necrons
{
    public class Necrons : Faction
    {
        public Necrons()
        {
            subFactionName = "<Dynasty>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Cryptek Arkana";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Rarefied Nobility",
                "Stratagem: Dynastic Heirlooms",
                "Stratagem: Hand of the Phaeron"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>()
            {
                "Eternal Conquerors",
                "Pitiless Hunters",
                "Superior Artisans",
                "Rad-wreathed",
                "Immovable Phalanx",
                "Unyielding",
                "Contemptuous of the Codes",
                "The Unmerciful Horde",
                "Masters of the Martial",
                "Butchers",
                "Severed",
                "Vassal Kingdom"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>()
            {
                "The Ancients Stir",
                "Arise Against the Interlopers",
                "Healthy Paranoia",
                "Relentlessly Expansionist",
                "Isolationists",
                "Warrior Nobles",
                "Interplanetary Invaders"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>()
            {
                //---------- HQ ----------
                new Imotekh(),
                new Orikan(),
                new Anrakyr(),
                new Obyron(),
                new Szeras(),
                new Zahndrekh(),
                new Trazyn(),
                new RoyalWarden(),
                new SkorpekhLord(),
                new LokhustLord(),
                new NecronLord(),
                new CatacombBarge(),
                new Overlord(),
                new Technomancer(),
                new Psychomancer(),
                new Chronomancer(),
                new Plasmancer(),
                //---------- Troops ----------
                new NecronWarriors(),
                new Immortals(),
                //---------- Elites ----------
                new CanoptekReanimator(),
                new HexmarkDestroyer(),
                new Lychguard(),
                new Deathmarks(),
                new FlayedOnes(),
                new Cryptothralls(),
                new SkorpekhDestroyers(),
                new CanoptekPlasmacyte(),
                new TriarchStalker(),
                new CtanDeceiver(),
                new CtanNightbringer(),
                new CtanVoidDragon(),
                new TranscendentCtan(),
                new CanoptekSpyders(),
                //---------- Fast Attack ----------
                new CanoptekScarabs(),
                new OphydianDestroyers(),
                new TombBlades(),
                new TriarchPraetorians(),
                new CanoptekWraiths(),
                //---------- Heavy Support ----------
                new AnnihilationBarge(),
                new DoomsdayArk(),
                new LokhustDestroyers(),
                new LokhustHeavyDestroyers(),
                new CanoptekDoomstalker(),
                //---------- Transport ----------
                new GhostArk(),
                //---------- Flyers ----------
                new DoomScythe(),
                new NightScythe(),
                //---------- Lords of War ----------
                new Obelisk(),
                new TesseractVault(),
                new Monolith(),
                new SilentKing(),
                //---------- Fortification ----------
                new ConvergenceOfDominion(),
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] thirty = new string[]
            {
                "Countertemporal Nanomines", "Fail-Safe Overcharger"
            };

            string[] twentyFive = new string[]
            {
                "Atavindicator", "Hypermaterial Ablator"
            };

            string[] twenty = new string[]
            {
                "Metalodermal Tesla Weave", "Photonic Transubjector", "Phylacterine Hive",
                "Prismatic Obfuscatron", "Quantum Orb"
            };

            string[] fifteen = new string[]
            {
                "Cortical Subjugator Scarabs", "Cryptogeometric Adjustor", "Dimensional Sanctum"
            };

            if(thirty.Contains(upgrade))
            {
                points += 30;
            }
            else if (twentyFive.Contains(upgrade))
            {
                points += 25;
            }
            else if (twenty.Contains(upgrade))
            {
                points += 20;
            }
            else if (fifteen.Contains(upgrade))
            {
                points += 15;
            }

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>() { "(None)" };

            if(keywords.Contains("PSYCHOMANCER"))
            {
                upgrades.Add("Atavindicator");
            }

            upgrades.Add("Cortical Subjugator Scarabs");

            if(keywords.Contains("CHRONOMANCER"))
            {
                upgrades.Add("Countertemporal Nanomines");
            }

            upgrades.Add("Cryptogeometric Adjustor");
            upgrades.Add("Dimensional Sanctum");

            if (keywords.Contains("TECHNOMANCER"))
            {
                upgrades.Add("Fail-Safe Overcharger");
            }

            upgrades.Add("Hypermaterial Ablator");
            upgrades.Add("Metalodermal Tesla Weave");
            upgrades.Add("Photonic Transubjector");

            if (keywords.Contains("TECHNOMANCER"))
            {
                upgrades.Add("Phylacterine Hive");
            }

            upgrades.Add("Prismatic Obfuscatron");

            if (keywords.Contains("PLASMANCER"))
            {
                upgrades.Add("Quantum Orb");
            }

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            if (StratagemCount[index] < StratagemLimit[index])
            {
                return true;
            }

            return false;
        }

        public override List<string> GetPsykerPowers()
        {
            return new List<string>()
            {
                "Antimatter Meteor",
                "Time's Arrow",
                "Sky of Falling Stars",
                "Cosmic Fire",
                "Seismic Assault",
                "Transdimensional Thunderbolt"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            if(keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
               keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD"))
            {
                relics.Add("Orb of Eternity");  //Lokhust Lord, Lord, Catacomb Command Barge, Overlord
            }

            relics.Add("Nanoscarab Casket"); //Any

            relics.Add("Gauntlet of the Conflagrator"); //Any

            relics.Add("Veil of Darkness"); //Any

            if (keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
                keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD") ||
                keywords.Contains("TECHNOMANCER"))
            {
                relics.Add("Voltaic Staff");    //Lokhust Lord, Lord, Catacomb Command Barge, Overlord, Technomancer
            }

            if (keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
                keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD"))
            {
                relics.Add("Voidreaper");   //Lokhust Lord, Lord, Catacomb Command Barge, Overlord
            }

            if (keywords.Contains("LORD") || keywords.Contains("OVERLORD"))
            {
                relics.Add("Sempiternal Weave");    //Lord, Overlord
            }

            if (keywords.Contains("OVERLORD"))
            {
                relics.Add("The Arrow of Infinity");    //Overlord
            }

            if (keywords.Contains("ROYAL WARDEN") && currentSubFaction == "Mephrit")
            {
                relics.Add("Conduit of Stars"); //Mephrit - Royal Warden
            }

            if ((keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
                keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD") ||
                keywords.Contains("TECHNOMANCER")) && currentSubFaction == "Nephrekh")
            {
                relics.Add("Solar Staff");  //Nephrekh - Lokhust Lord, Lord, Catacomb Command Barge, Overlord, Technomancer
            }

            if (currentSubFaction == "Nihilakh")
            {
                relics.Add("Infinity Mantle");  //Nihilakh
            }

            if ((keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
                keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD")) &&
                currentSubFaction == "Novokh")
            {
                relics.Add("Blood Scythe"); //Novokh - Lokhust Lord, Lord, Catacomb Command Barge, Overlord
            }

            if (currentSubFaction == "Sautekh")
            {
                relics.Add("The Vanquisher's Mask"); //Sautekh
            }

            if ((keywords.Contains("LORD") || keywords.Contains("CATACOMB COMMAND BARGE") ||
                keywords.Contains("OVERLORD")) && currentSubFaction == "Szerekhan")
            {
                relics.Add("The Sovereign Coronal"); //Szarekhan - Lord, Catacomb Command Barge, Overlord
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "Mephrit",
                "Novokh",
                "Szarekhan",
                "Nephrekh",
                "Nihilakh",
                "Sautekh",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits()
        {
            List<string> traits = new List<string>()
            {
                "Enduring Will",
                "Eternal Madness",
                "Immortal Pride",
                "Thrall of the Silent King",
                "Implacable Conqueror (Aura)",
                "Honourable Combatant"
            };

            if(currentSubFaction == "Mephrit") { traits.Add("Merciless Tyrant"); }
            else if (currentSubFaction == "Nephrekh") { traits.Add("Skin of Living Gold"); }
            else if (currentSubFaction == "Nihilakh") { traits.Add("Precognitive Strike"); }
            else if (currentSubFaction == "Novokh") { traits.Add("Blood-fuelled Fury"); }
            else if (currentSubFaction == "Sautekh") { traits.Add("Hyperlogical Strategist"); }
            else if (currentSubFaction == "Szarekhan") { traits.Add("The Triarch's Will"); }

            return traits;
        }

        public override void SetPoints(int points)
        {
            StratagemCount = new int[] { 0, 0, 0 };
            StratagemLimit = new int[] { 1 + points / 1000, 1 + points / 1000, 1 };

            if (points % 1000 == 0)
            {
                StratagemLimit[0] -= 1;
                StratagemLimit[1] -= 1;
            }
        }

        public override string ToString()
        {
            return "Necrons";
        }
    }
}
