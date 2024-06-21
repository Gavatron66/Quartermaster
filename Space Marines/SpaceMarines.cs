using Roster_Builder.Genestealer_Cults;
using Roster_Builder.Space_Marines;
using Roster_Builder.Space_Marines.Ultramarines;
using Roster_Builder.Space_Marines.Salamanders;
using Roster_Builder.Space_Marines.Raven_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Roster_Builder.Space_Marines.Iron_Hands;
using Roster_Builder.Space_Marines.White_Scars;
using Roster_Builder.Space_Marines.Imperial_Fists;
using Roster_Builder.Space_Marines.Crimson_Fists;
using Roster_Builder.Space_Marines.Deathwatch;
using Roster_Builder.Space_Marines.Space_Wolves;
using Roster_Builder.Space_Marines.Dark_Angels;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class SpaceMarines : Faction
    {
        public SpaceMarines()
        {
            subFactionName = "<Chapter>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Masters of the Chapter";
            customSubFactionTraits = new string[2];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Hero of the Chapter",
                "Stratagem: Relic of the Chapter"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>()
            {
                "Bolter Fusillades",
                "Born Heroes",
                "Duellists",
                "Fearsome Aspect",
                "Hungry for Battle",
                "Indomitable",
                "Knowledge is Power",
                "Long-range Marksmen",
                "Master Artisans",
                "Preferred Enemy",
                "Rapid Assault",
                "Scions of the Forge",
                "Stalwart",
                "Stealthy",
                "Stoic",
                "Tactical Withdrawal",
                "Warded",
                "Whirlwind of Rage"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>()
            {
                "Bolter Fusillades",
                "Born Heroes",
                "Duellists",
                "Fearsome Aspect",
                "Hungry for Battle",
                "Indomitable",
                "Knowledge is Power",
                "Long-range Marksmen",
                "Master Artisans",
                "Preferred Enemy",
                "Rapid Assault",
                "Scions of the Forge",
                "Stalwart",
                "Stealthy",
                "Stoic",
                "Tactical Withdrawal",
                "Warded",
                "Whirlwind of Rage"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
                //---------- HQ ----------
                new PrimarisCaptain(),
                new GravisHeavyCaptain(),
                new PhobosCaptain(),
                new GravisCaptain(),
                new TerminatorCaptain(),
                new Captain(),
                new BikeCaptain(),
                new PrimarisLieutenant(),
                new ReiverLieutenant(),
                new Lieutenant(),
                new PhobosLieutenant(),
                new PrimarisLibrarian(),
                new Librarian(),
                new PhobosLibrarian(),
                new TerminatorLibrarian(),
                new PrimarisChaplain(),
                new PrimarisBikeChaplain(),
                new TerminatorChaplain(),
                new Chaplain(),
                new PrimarisTechmarine(),
                new Servitors(),
                new Techmarine(),   //[21]
                //---------- Troops ----------
                new Intercessors(),
                new AssaultIntercessors(),
                new HeavyIntercessors(),
                new Infiltrators(),
                new Incursors(),
                new TacticalSquad(), //[27]
                //---------- Elites ----------
                new CompanyChampion(),
                new ScoutSquad(),
                new PrimarisApothecary(),
                new Apothecary(),
                new CompanyAncient(),
                new PrimarisAncient(),
                new BladeguardAncient(),
                new TerminatorAncient(),
                new VeteranIntercessors(),
                new BladeguardVeterans(),
                new Veterans(),
                new VanguardVeterans(),
                new SternguardVeterans(),
                new Judiciar(),
                new Reivers(),
                new Aggressors(),
                new AssaultTerminators(),
                new Terminators(),
                new RelicTerminators(),
                new CenturionAssault(),
                new Invictor(),
                new Dreadnought(),
                new ContemptorDreadnought(),
                new VenerableDreadnought(),
                new IroncladDreadnought(),
                new RedemptorDreadnought(),
                new BrutalisDreadnought(), //[54]
                //---------- Fast Attack ----------
                new AssaultSquad(),
                new Outriders(),
                new InvaderATVs(),
                new BikeSquad(),
                new ScoutBikeSquad(),
                new AttackBikeSquad(),
                new Suppressors(),
                new Inceptors(),
                new StormSpeederHailstrike(),
                new StormSpeederThunderstrike(),
                new StormSpeederHammerstrike(),
                new LandSpeeders(),
                new LandSpeederTornadoes(),
                new LandSpeederTyphoons(), //[68]
                //---------- Heavy Support ----------
                new Hellblasters(),
                new Eliminators(),
                new CenturionDevastators(),
                new Eradicators(),
                new Devastators(),
                new DesolationSquad(),
                new Thunderfire(),
                new FirestrikeTurret(),
                new Hunter(),
                new Stalker(),
                new Whirlwind(),
                new PredatorDestructor(),
                new PredatorAnnihilator(),
                new GladiatorLancer(),
                new GladiatorReaper(),
                new GladiatorValiant(),
                new Vindicator(),
                new LandRaider(),
                new LandRaiderCrusader(),
                new LandRaiderRedeemer(),
                new Repulsor(),
                new RepulsorExecutioner(), //[90]
                //---------- Transport ----------
                new Rhino(),
                new Razorback(),
                new Impulsor(),
                new DropPod(),
                new LandSpeederStorm(), //[95]
                //---------- Flyers ----------
                new StormhawkInterceptor(),
                new StormtalonGunship(),
                new StormravenGunship(), //[98]
                //---------- Lords of War ----------
                new RobouteGuilliman(),
                new LionElJonson(),
                //---------- Fortification ----------
                new HammerfallBunker()
            };

            if (currentSubFaction == "Ultramarines")
            {
                datasheets.Insert(22, new MarneusCalgar()); //Marneus Calgar
                datasheets.Insert(56, new VictrixGuard()); //Victrix Guard
                datasheets.Insert(23, new ChiefLibrarianTigurius()); //Tigurius
                datasheets.Insert(24, new ChaplainCassius()); //Cassius
                datasheets.Insert(25, new CaptainSicarius()); //Sicarius
                datasheets.Insert(26, new SergeantTelion()); //Telion
                datasheets.Insert(27, new SergeantChronus()); //Chronus
                datasheets.Insert(62, new ChapterAncient()); //Chapter Ancient
                datasheets.Insert(63, new ChapterChampion()); //Chapter Champion
                datasheets.Insert(64, new HonourGuard()); //Honour Guard
                datasheets.Insert(65, new TyrannicVeterans()); //Tyrannic War Veterans
            }
            else if (currentSubFaction == "Salamanders")
            {
                datasheets.Insert(22, new VulkanHestan());
                datasheets.Insert(23, new AdraxAgatone());
            }
            else if (currentSubFaction == "Raven Guard")
            {
                datasheets.Insert(22, new KayvaanShrike());
            }
            else if (currentSubFaction == "Iron Hands")
            {
                datasheets.Insert(22, new IronFatherFeirros());
            }
            else if (currentSubFaction == "White Scars")
            {
                datasheets.Insert(22, new KorsarroKhan());
                datasheets.Insert(23, new Khan());
            }
            else if (currentSubFaction == "Imperial Fists")
            {
                datasheets.Insert(22, new CaptainLysander());
                datasheets.Insert(23, new TorGaradon());
            }
            else if (currentSubFaction == "Crimson Fists")
            {
                datasheets.Insert(22, new PedroKantor());
            }
            else if (currentSubFaction == "Deathwatch")
            {
                StratagemList.Clear();
                StratagemList.AddRange(new string[]
                {
                    "Stratagem: Hero of the Chapter",
                    "Stratagem: Relic of the Chapter",
                    "Stratagem: Sanction of the Black Vault",
                    "Stratagem: A Vigil Unmatched"
                });

                datasheets.Insert(28, new ProteusKillTeam());
                datasheets.Insert(29, new FortisKillTeam());
                datasheets.Insert(30, new IndomitorKillTeam());
                datasheets.Insert(31, new SpectrusKillTeam());
                datasheets.Insert(22, new WatchMaster());
                datasheets.Insert(23, new WatchCaptainArtemis());
                datasheets.Insert(24, new DWChaplainCassius());
                datasheets.Insert(25, new CodicierNatorian());
                datasheets.Insert(36, new DeathwatchVeterans());
                datasheets.Insert(37, new KillTeamCassius());
                datasheets.Insert(57, new DeathwatchTerminators());
                datasheets.Insert(69, new VeteranBikeSquad());
                datasheets.Insert(111, new CorvusBlackstar()); //111
                datasheets.RemoveAt(66);
                datasheets.RemoveAt(71);
                datasheets.RemoveAt(69);
                datasheets.RemoveAt(82);
                datasheets.RemoveAt(50);
                datasheets.RemoveAt(31);
                datasheets.RemoveAt(38);
                datasheets.RemoveAt(66);
                datasheets.RemoveAt(99);
            }
            else if (currentSubFaction == "Space Wolves")
            {
                StratagemList.Clear();
                StratagemList.AddRange(new string[]
                {
                    "Stratagem: Hero of the Chapter",
                    "Stratagem: Relic of the Chapter",
                    "Stratagem: A Trophy Bestowed",
                    "Stratagem: Thane of the Retinue",
                    "Stratagem: Warrior of Legend"
                });

                datasheets.RemoveAt(73);
				datasheets.RemoveAt(55);
				datasheets.RemoveAt(40);
				datasheets.RemoveAt(39);
				datasheets.RemoveAt(31);
				datasheets.RemoveAt(30);
				datasheets.RemoveAt(27);
				datasheets.Insert(22, new LoganGrimnar());
				datasheets.Insert(23, new NjalStormcaller());
				datasheets.Insert(24, new BjornTheFellHanded());
				datasheets.Insert(25, new ArjacRockfist());
				datasheets.Insert(26, new UlrikTheSlayer());
				datasheets.Insert(27, new RagnarBlackmane());
				datasheets.Insert(28, new KromDragongaze());
				datasheets.Insert(29, new HaraldDeathwolf());
				datasheets.Insert(30, new CanisWolfborn());
				datasheets.Insert(31, new TerminatorWolfGuard());
				datasheets.Insert(32, new WolfLordThunderwolf());
				datasheets.Insert(33, new ThunderwolfWolfGuard());
				datasheets.Insert(39, new BloodClaws());
				datasheets.Insert(40, new GreyHunters());
				datasheets.Insert(50, new WolfGuard());
				datasheets.Insert(51, new WolfGuardTerminators());
				datasheets.Insert(54, new LukasTheTrickster());
				datasheets.Insert(55, new HoundsOfMorkai());
				datasheets.Insert(56, new Wulfen());
				datasheets.Insert(69, new WulfenDreadnought());
				datasheets.Insert(70, new Murderfang());
				datasheets.Insert(71, new Skyclaws());
				datasheets.Insert(72, new Cyberwolves());
				datasheets.Insert(73, new ThunderwolfCavalry());
				datasheets.Insert(74, new FenrisianWolves());
				datasheets.Insert(92, new LongFangs());
				datasheets.Insert(118, new StormfangGunship());
				datasheets.Insert(119, new Stormwolf());
            }
            else if (currentSubFaction == "Dark Angels")
            {
                datasheets.RemoveRange(39, 2);
                datasheets.Insert(11, new RavenwingTalonmaster());
                datasheets.Insert(12, new DeathwingStrikemaster());
                datasheets.Insert(21, new InterrogatorChaplain());
                datasheets.Insert(22, new TerminatorInterrogator());
                datasheets.Insert(26, new Azrael());
                datasheets.Insert(27, new Belial());
                datasheets.Insert(28, new Sammael());
                datasheets.Insert(29, new Ezekiel());
                datasheets.Insert(30, new Asmodai());
                datasheets.Insert(31, new Lazarus());
                datasheets.Insert(39, new DeathwingChampion());
                datasheets.Insert(40, new RavenwingChampion());
                datasheets.Insert(44, new DeathwingApothecary());
                datasheets.Insert(45, new RavenwingApothecary());
                datasheets.Insert(50, new RavenwingAncient());
                datasheets.Insert(60, new DeathwingTerminators());
                datasheets.Insert(61, new DeathwingKnights());
                datasheets.Insert(62, new DeathwingCommand());
                datasheets.Insert(63, new RavenwingBlackKnights());
                datasheets.Insert(86, new RavenwingDarkshroud());
                datasheets.Insert(87, new RavenwingVengeance());
                datasheets.Insert(118, new RavenwingDarkTalon());
                datasheets.Insert(119, new NephlilimJetfighter());

                // The Book Order:
                //Azrael
                //Belial
                //Sammael
                //Ezekiel
                //Asmodai
                //Interrogator-Chaplain
                //Ravenwing Talonmaster
                //Lazarus
                //Deathwing Strikemaster
                //Interrogator-Chaplain in Terminator Armour
                //Deathwing Apothecary
                //Deathwing Champion
                //Deathwing Terminator Squad
                //Deathwing Knights
                //Deathwing Command Squad
                //Ravenwing Apothecary
                //Ravenwing Champion
                //Ravenwing Ancient
                //Ravenwing Black Knights
                //Ravenwing Darkshroud
                //Ravenwing Land Speeder Vengeance
                //Ravenwing Dark Talon
                //Nephilim Jetfighter
            }
            else if (currentSubFaction == "Black Templars")
            {
            }
            else if (currentSubFaction == "Blood Angels")
            {
            }

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] fifty = new string[]
            {
                   "Deathwing Chapter Master (+50 pts)"
            };

            string[] forty = new string[]
            {
            };

            string[] thirtyfive = new string[]
            {
                "Chapter Master (+35 pts)"
            };

            string[] thirty = new string[]
            {
                "Chief Apothecary (+30 pts)",
                "Malleus Kill Team (+30 pts)",
                "Furor Kill Team (+30 pts)",
            };

            string[] twenty = new string[]
            {
                "Master of Sanctity (+20 pts)",
                "Chief Librarian (+20 pts)",
                "Venator Kill Team (+20 pts)",
                "Dominatus Kill Team (+20 pts)",
                "Purgatus Kill Team (+20 pts)"
            };

            string[] fifteen = new string[]
            {
                "Chapter Ancient (+15 pts)",
                "Master of the Forge (+15 pts)",
                "Aquila Kill Team (+15 pts)",
                "Promote to Deathwing (+15 pts)"
        };

            string[] ten = new string[]
            {
                "Chapter Champion (+10 pts)",
                "Promote to Deathwing (+10 pts)"
            };

            string[] five = new string[]
            {
                "Promote to Deathwing (+5 pts)"
            };

            if (fifty.Contains(upgrade))
            {
                points = 50;
            }

            if (forty.Contains(upgrade))
            {
                points = 40;
            }

            if (thirtyfive.Contains(upgrade))
            {
                points = 35;
            }

            if (thirty.Contains(upgrade))
            {
                points = 30;
            }

            if (twenty.Contains(upgrade))
            {
                points = 20;
            }

            if (fifteen.Contains(upgrade))
            {
                points = 15;
            }

            if (ten.Contains(upgrade))
            {
                points = 10;
            }

            if(five.Contains(upgrade))
            {
                points = 5;
            }

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>() { "(None)" };

            if (keywords.Contains("CAPTAIN"))
            {
                upgrades.Add("Chapter Master (+35 pts)");
            }

            if (keywords.Contains("CHAPLAIN"))
            {
                upgrades.Add("Master of Sanctity (+20 pts)");
            }

            if (keywords.Contains("TECHMARINE"))
            {
                upgrades.Add("Master of the Forge (+15 pts)");
            }

            if (keywords.Contains("LIBRARIAN"))
            {
                upgrades.Add("Chief Librarian (+20 pts)");
            }

            if (keywords.Contains("APOTHECARY"))
            {
                upgrades.Add("Chief Apothecary (+30 pts)");
            }

            if (keywords.Contains("ANCIENT"))
            {
                upgrades.Add("Chapter Ancient (+15 pts)");
            }

            if (keywords.Contains("COMPANY CHAMPION"))
            {
                upgrades.Add("Chapter Champion (+10 pts)");
            }

            if(currentSubFaction == "Deathwatch" && keywords.Contains("KILL TEAM"))
            {
                upgrades.AddRange(new string[]
                {
                    "Aquila Kill Team (+15 pts)",
                    "Venator Kill Team (+20 pts)",
                    "Malleus Kill Team (+30 pts)",
                    "Dominatus Kill Team (+20 pts)",
                    "Furor Kill Team (+30 pts)",
                    "Purgatus Kill Team (+20 pts)"
                });
            }

            if(currentSubFaction == "Dark Angels")
            {
                if(keywords.Contains("CAPTAIN"))
                {
                    upgrades.Add("Promote to Deathwing (+15 pts)");
                    upgrades.Add("Deathwing Chapter Master (+50 pts)");
                }

                if(keywords.Contains("PRIMARIS LIEUTENANT") && !(keywords.Contains("PHOBOS")))
                {
                    // Only if it's equipped with a Storm Shield
                    upgrades.Add("Promote to Deathwing (+10 pts)");
                }

                if(keywords.Contains("DREADNOUGHT"))
                {
                    upgrades.Add("Promote to Deathwing (+10 pts)");
                }

                if (keywords.Contains("LAND RAIDER"))
                {
                    upgrades.Add("Promote to Deathwing (+5 pts)");
                }

                if (keywords.Contains("REPULSOR"))
                {
                    upgrades.Add("Promote to Deathwing (+5 pts)");
                }

                if (keywords.Contains("STORMRAVEN GUNSHIP"))
                {
                    upgrades.Add("Promote to Deathwing (+5 pts)");
                }

                if (keywords.Contains("REPULSOR"))
                {
                    upgrades.Add("Promote to Deathwing (+5 pts)");
                }

                if (keywords.Contains("STORMRAVEN GUNSHIP"))
                {
                    upgrades.Add("Promote to Deathwing (+5 pts)");
                }
            }

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            /*
            if (StratagemCount[index] < StratagemLimit[index])
            {
                return true;
            }

            return false;
            */
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            List<string> PsychicPowers = new List<string>();

            if(keywords == "Librarius")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Veil of Time",
                    "Might of Heroes",
                    "Null Zone",
                    "Psychic Scourge",
                    "Fury of the Ancients",
                    "Psychic Fortress"
                });
            }

            if(keywords == "Obscuration")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Shrouding",
                    "Soul Sight",
                    "Mind Raid",
                    "Hallucination",
                    "Tenebrous Curse",
                    "Temporal Corridor"
                });
            }

            if (keywords == "Litanies")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Litany of Faith",
                    "Catechism of Fire",
                    "Exhortation of Rage",
                    "Mantra of Strength",
                    "Recitation of Focus",
                    "Canticle of Hate"
                });
            }

            if(keywords == "Indomitus")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Precognition",
                    "Scryer's Gaze",
                    "Telepathic Assault",
                    "Storm of the Emperor's Wrath",
                    "Psychic Shackles",
                    "Empyric Channelling"
                });
            }

            if(keywords == "Promethean")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Flaming Blast",
                    "Fire Shield",
                    "Burning Hands",
                    "Drakeskin",
                    "Fury of Nocturne",
                    "Draconic Aspect"
                });
            }

            if(keywords == "Umbramancy")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Umbral Form",
                    "Enveloping Darkness",
                    "Spectral Blade",
                    "Shadowstep",
                    "The Abyss",
                    "The Darkness Within"
                });
            }

            if(keywords == "Technomancy")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Blessing of the Machine God",
                    "Objuration Mechanicum",
                    "Fury of Medusa",
                    "Psysteel Armour",
                    "Reforge",
                    "Machine Flense"
                });
            }

            if(keywords == "Stormspeaking")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Blasting Gale",
                    "Lightning Call",
                    "Ride the Winds",
                    "Storm-wreathed",
                    "Spirits of Chogoris",
                    "Eye of the Storm"
                });
            }

            if(keywords == "Geokinesis")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Tectonic Purge",
                    "Wrack and Ruin",
                    "Iron Inferno",
                    "Fortify",
                    "Aspect of Stone",
                    "Chasm"
                });
            }

            if (keywords == "Xenopurge")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Premorphic Resonance",
                    "Fortified with Contempt",
                    "Neural Void",
                    "Psychic Cleanse",
                    "Mantle of Shadow",
                    "Severance"
                });
            }

            if(keywords == "Tempestas")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Living Lightning",
                    "Murderous Hurricane",
                    "Tempest's Wrath",
                    "Instincts Awoken",
                    "Storm Caller",
                    "Jaws of the World Wolf"
                });
            }

            if (keywords == "Interromancy")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Mind Worm",
                    "Aversion",
                    "Righteous Repugnance",
                    "Trephination",
                    "Engulfing Fear",
                    "Mind Wipe"
                });
            }

            return PsychicPowers;
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            relics.Add("The Armour Indomitus");

            if ((keywords.Contains("CAPTAIN") && !(keywords.Contains("PHOBOS") || keywords.Contains("MK X GRAVIS"))) ||
                ((keywords.Contains("LIEUTENANT") && keywords.Contains("PRIMARIS")) && !keywords.Contains("PHOBOS")) ||
                (keywords.Contains("TERMINATOR") && keywords.Contains("ANCIENT") && currentSubFaction == "Dark Angels") ||
                keywords.Contains("COMPANY CHAMPION"))
            {
                relics.Add("The Shield Eternal");
            }

            if (keywords.Contains("ANCIENT"))
            {
                relics.Add("Standard of the Emperor Ascendant");
            }

            if ((keywords.Contains("CAPTAIN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))) ||
                (keywords.Contains("PRIMARIS") && keywords.Contains("MK X GRAVIS") && !keywords.Contains("HBR")) ||
                (keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS")) ||
                (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                (keywords.Contains("APOTHECARY") && !keywords.Contains("PRIMARIS")) ||
                keywords.Contains("ANCIENT") && keywords.Contains("COMMAND SQUAD"))
            {
                relics.Add("The Teeth of Terra");
            }

            if ((keywords.Contains("CAPTAIN") && !(keywords.Contains("PHOBOS") || keywords.Contains("MK X GRAVIS") || keywords.Contains("TERMINATOR") || currentSubFaction != "Dark Angels")) ||
                (keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS")) || 
                (keywords.Contains("PRIMARIS") && keywords.Contains("LIEUTENANT") && currentSubFaction == "Space Wolves") ||
                (keywords.Contains("LIBRARIAN") && !(keywords.Contains("TERMINATOR") || keywords.Contains("PRIMARIS"))) ||
                (keywords.Contains("CHAPLAIN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))) ||
                (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                keywords.Contains("ANCIENT") && keywords.Contains("COMMAND SQUAD"))
            {
                relics.Add("Primarch's Wrath");
            }

            if ((keywords.Contains("CAPTAIN") && !keywords.Contains("PHOBOS")) ||
                (keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS")) ||
                (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                keywords.Contains("COMPANY CHAMPION") || keywords.Contains("COMPANY ANCIENT") ||
                keywords.Contains("CHAPTER ANCIENT") || keywords.Contains("CHAPTER CHAMPION") || 
                keywords.Contains("ANCIENT") && keywords.Contains("PRIMARIS") && !keywords.Contains("BLADEGUARD"))
            {
                relics.Add("The Burning Blade");
            }

            if (!(keywords.Contains("TERMINATOR") || keywords.Contains("MK X GRAVIS") || (keywords.Contains("TECHMARINE") && keywords.Contains("PRIMARIS"))))
            {
                relics.Add("Purgatorus");
            }

            if (keywords.Contains("PRIMARIS"))
            {
                relics.Add("Reliquary of Gathalamor");
            }

            if (keywords.Contains("PRIMARIS") && (keywords.Contains("LIEUTENANT") || keywords.Contains("CAPTAIN")) &&
                !(keywords.Contains("MK X GRAVIS") || keywords.Contains("PHOBOS")))
            {
                relics.Add("Bellicos Bolt Rifle");
            }

            if (keywords.Contains("PRIMARIS") && (keywords.Contains("LIEUTENANT") || keywords.Contains("CAPTAIN")) &&
                !(keywords.Contains("MK X GRAVIS") || keywords.Contains("PHOBOS")))
            {
                relics.Add("Lament");
            }

            if (keywords.Contains("PHOBOS") && (keywords.Contains("LIBRARIAN") || keywords.Contains("CAPTAIN")))
            {
                relics.Add("Ghostweave Cloak");
            }

            if (keywords.Contains("LIBRARIAN"))
            {
                relics.Add("Tome of Malcador");
            }

            if (keywords.Contains("CHAPLAIN"))
            {
                relics.Add("Benediction of Fury");
            }

            relics.Add("The Honour Vehement");

            if (keywords.Contains("PRIMARIS"))
            {
                relics.Add("The Vox Espiritum");
            }

            #region Black Templars Relics
            if (currentSubFaction == "Black Templars")
            {
            }
            #endregion
            #region Blood Angels Relics
            if (currentSubFaction == "Blood Angels")
            {
            }
            #endregion
            #region Crimson Fists Relics
            if (currentSubFaction == "Crimson Fists")
            {
                if ((keywords.Contains("CAPTAIN") && keywords.Contains("PRIMARIS") && !(keywords.Contains("PHOBOS") || keywords.Contains("MK X GRAVIS"))) ||
                    (keywords.Contains("LIEUTENANT") && keywords.Contains("PRIMARIS") && !keywords.Contains("PHOBOS"))
                    )
                {
                    relics.Add("Duty's Burden"); //MC Auto/Stalker Bolt Rifle
                }

                if ((keywords.Contains("CAPTAIN") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("ANCIENT") && !keywords.Contains("BLADEGUARD"))
                    )
                {
                    relics.Add("Fist of Vengeance"); //Power Fist
                }

                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");

                if ((keywords.Contains("CAPTAIN") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("ANCIENT") && !keywords.Contains("BLADEGUARD"))
                    )
                {
                    relics.Add("Fist of Terra"); //Power Fist
                }

                relics.Add("Gatebreaker Bolts");
                relics.Add("Auric Aquila");
                relics.Add("Warden's Cuirass");
            }
            #endregion
            #region Dark Angels Relics
            if (currentSubFaction == "Dark Angels")
            {
                relics.Add("Mace of Redemption");
                relics.Add("Pennant of Remembrance");
                relics.Add("Shroud of Heroes");
                relics.Add("Reliquary of the Repentant");
                relics.Add("Foe-Smiter");
                relics.Add("Eye of the Unseen");
                relics.Add("Cup of Retribution");
                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");
                relics.Add("Heavenfall Blade");
                relics.Add("Arbiter's Gaze");
                relics.Add("Atonement");
                relics.Add("Bolts of Judgement");
            }
            #endregion
            #region Deathwatch Relics
            if (currentSubFaction == "Deathwatch")
            {
                relics.Add("The Beacon Angelis");

                if(keywords.Contains("CAPTAIN") && !(keywords.Contains("PHOBOS") || keywords.Contains("GRAVIS"))
                    || (keywords.Contains("LIEUTENANT") && keywords.Contains("PRIMARIS") && !(keywords.Contains("PHOBOS")))
                    || keywords.Contains("COMPANY CHAMPION") || (keywords.Contains("TERMINATOR") && keywords.Contains("ANCIENT")))
                {
					relics.Add("Dominus Aegis");
				}

                if(keywords.Contains("WATCH MASTER"))
                {
					relics.Add("Osseus Key");
				}

				if ((keywords.Contains("CAPTAIN") && !keywords.Contains("PHOBOS")) ||
					(keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS")) ||
					(keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
					keywords.Contains("COMPANY CHAMPION") || keywords.Contains("COMPANY ANCIENT") ||
					keywords.Contains("CHAPTER ANCIENT") || keywords.Contains("CHAPTER CHAMPION") ||
					keywords.Contains("ANCIENT") && keywords.Contains("PRIMARIS") && !keywords.Contains("BLADEGUARD"))
                {
					relics.Add("The Thief of Secrets");
                }

                relics.Add("The Tome of Ectoclades");
                relics.Add("Adamantine Mantle");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");
                relics.Add("Artificer Armour");
                relics.Add("The Blackweave Shroud");

                if(keywords.Contains("WATCH MASTER"))
                {
                    relics.Add("Spear of the First Vigil");
                }

				if (keywords.Contains("LIBRARIAN"))
				{
					relics.Add("The Soul Fortress");
				}

                relics.Add("Banebolts of Eryxia");
                relics.Add("Vhorkan-pattern Auspicator");
                relics.Add("Artificer Bolt Cache");
                relics.Add("Eye of Abiding");
            }
            #endregion
            #region Flesh Tearers Relics
            #endregion
            #region Imperial Fists Relics
                if (currentSubFaction == "Imperial Fists")
            {
                if ((keywords.Contains("CAPTAIN") && !(keywords.Contains("MK X GRAVIS") || keywords.Contains("TERMINATOR"))) ||
                    (keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIBRARIAN") && !keywords.Contains("TERMINATOR")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("REIVER")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("APOTHECARY") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("ANCIENT") && !keywords.Contains("TERMINATOR")) ||
                    keywords.Contains("COMPANY CHAMPION")
                    )
                {
                    relics.Add("The Spartean"); //Bolt Pistol, Heavy Bolt Pistol
                }

                if(keywords.Contains("ANCIENT"))
                {
                    relics.Add("The Banner of Staganda");
                }

                relics.Add("The Eye of Hypnoth");

                if (keywords.Contains("LIBRARIAN"))
                {
                    relics.Add("The Bones of Osrak");
                }

                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");

                if ((keywords.Contains("CAPTAIN") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("ANCIENT") && !keywords.Contains("BLADEGUARD"))
                    )
                {
                    relics.Add("Fist of Terra"); //Power Fist
                }

                relics.Add("Gatebreaker Bolts");
                relics.Add("Auric Aquila");
                relics.Add("Warden's Cuirass");
            }
            #endregion
            #region Iron Hands Relics
            if (currentSubFaction == "Iron Hands")
            {
                if((keywords.Contains("CAPTAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    keywords.Contains("COMPANY ANCIENT")
                    )
                {
                    relics.Add("The Axe of Medusa"); //Power Axe
                }

                if(keywords.Contains("PRIMARIS"))
                {
                    relics.Add("The Aegis Ferrum");
                }

                if (keywords.Contains("LIBRARIAN"))
                {
                    relics.Add("The Mindforge"); //Force Sword/Axe/Stave
                }

                if (keywords.Contains("CAPTAIN") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("LIBRARIAN") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("ANCIENT") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))
                    )
                {
                    relics.Add("Betrayer's Bane"); //Combi-melta
                }

                relics.Add("The Ironstone");
                relics.Add("The Tempered Helm");
                relics.Add("The Gorgon's Chain");
                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");
                relics.Add("Auto-Medicae Bionics");

                if ((keywords.Contains("CAPTAIN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))) ||
                    (keywords.Contains("PRIMARIS") && keywords.Contains("MK X GRAVIS") && !keywords.Contains("HBR")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("APOTHECARY") && !keywords.Contains("PRIMARIS")) ||
                    keywords.Contains("ANCIENT") && keywords.Contains("COMMAND SQUAD"))
                {
                    relics.Add("Teeth of Mars"); //Chainsword
                }

                relics.Add("Haywire Bolts");

                if(keywords.Contains("TECHMARINE"))
                {
                    relics.Add("Fortis-pattern Data Spike");
                }
            }
            #endregion
            #region Salamander Relics
            if (currentSubFaction == "Salamanders")
            {
                relics.Add("Vulkan's Sigil");

                if(keywords.Contains("CAPTAIN") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("ANCIENT") && !keywords.Contains("PRIMARIS")
                )
                {
                    relics.Add("Drake-smiter"); //Thunder Hammer
                }

                if(keywords.Contains("CAPTAIN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR")) ||
                    keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("LIBRARIAN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR")) ||
                    keywords.Contains("CHAPLAIN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR")) ||
                    keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("ANCIENT") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))
                    )
                {
                    relics.Add("Wrath of Prometheus"); //Boltgun only
                }

                if(keywords.Contains("LIBRARIAN"))
                {
                    relics.Add("The Tome of Vel'cona");
                }

                relics.Add("The Salamander's Mantle");

                if(keywords.Contains("CAPTAIN") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("LIBRARIAN") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("ANCIENT") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))
                    )
                {
                    relics.Add("Nocturne's Vengeance"); //Combi-flamer
                }

                if (keywords.Contains("PRIMARIS"))
                {
                    relics.Add("Helm of Draklos");
                }

                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");
                relics.Add("Obsidian Aquila");
                relics.Add("Promethean Plate");
                relics.Add("Dragonrage Bolts");

                if((keywords.Contains("CAPTAIN") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS")) ||
                    keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS") ||
                    keywords.Contains("ANCIENT") && !(keywords.Contains("TERMINATOR") || keywords.Contains("BLADEGUARD")) ||
                    keywords.Contains("COMPANY CHAMPION")
                    )
                {
                    relics.Add("Drakeblade"); // Power Sword, MC Power Sword or Combat Knife
                }
            }
            #endregion
            #region Space Wolves Relics
            if (currentSubFaction == "Space Wolves")
            {
                relics.Add("The Armour of Russ");
                relics.Add("The Wulfen Stone");

                if(keywords.Contains("CAPTAIN") && !(keywords.Contains("TERMINATOR") || keywords.Contains("MK X GRAVIS") || keywords.Contains("PHOBOS"))
                    || keywords.Contains("CAVALRY")
                    || (keywords.Contains("LIEUTENANT") && !(keywords.Contains("PHOBOS")))
                    || (keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS"))
					|| (keywords.Contains("LIBRARIAN") && !keywords.Contains("PRIMARIS"))
					|| (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS"))
					|| keywords.Contains("COMPANY ANCIENT"))

				{
                    relics.Add("Fireheart");
                }

                if ((keywords.Contains("CAPTAIN") && !keywords.Contains("PRIMARIS"))
                    || keywords.Contains("WOLF GUARD")
                    || (keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS"))
                    || (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS"))
                    || keywords.Contains("WOLF LORD")
                    || keywords.Contains("COMPANY ANCIENT"))
                {
                    relics.Add("Black Death");
                }

                relics.Add("Mountain-breaker Helm");

                if(keywords.Contains("LIBRARIAN"))
				{
					relics.Add("The Storm's Eye");
				}

                relics.Add("The Pelt of Balewolf");
                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");
                relics.Add("Morkai's Teeth Bolts");
                relics.Add("Wolf Tail Talisman");
                relics.Add("Frost Weapon");

				if (keywords.Contains("LIBRARIAN"))
				{
					relics.Add("Runic Weapon");
				}
            }
            #endregion
            #region Raven Guard Relics
                if (currentSubFaction == "Raven Guard")
            {
                if((keywords.Contains("CAPTAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS"))
                    )
                {
                    relics.Add("The Ebonclaws"); //Two Lightning Claws
                }

                relics.Add("The Armour of Shadows");
                relics.Add("The Raven Skull of Korvaad");

                if((keywords.Contains("CAPTAIN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR") || keywords.Contains("BIKER"))) ||
                    (keywords.Contains("CHAPLAIN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))) ||
                    (keywords.Contains("LIBRARIAN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS"))
                    )
                {
                    relics.Add("Raven's Fury"); //Jump Pack only
                }

                if((keywords.Contains("CAPTAIN") && keywords.Contains("PRIMARIS") && !keywords.Contains("MK X GRAVIS")) ||
                    (keywords.Contains("LIEUTENANT") && keywords.Contains("PRIMARIS") && !keywords.Contains("REIVER"))
                    )
                {
                    relics.Add("Ex Tenebris"); //MC Stalker Bolt Rifle, MC Occulus Bolt Carbine, MC Instigator Bolt Carbine
                }

                if((keywords.Contains("CAPTAIN") && keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("LIEUTENANT") && keywords.Contains("REIVER"))
                    )
                {
                    relics.Add("Oppressor's End"); //Combat Knife
                }

                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");
                relics.Add("Shadowmaster Cloak");

                if((keywords.Contains("CAPTAIN") && !(keywords.Contains("MK X GRAVIS") || keywords.Contains("TERMINATOR"))) ||
                    (keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIBRARIAN") && !keywords.Contains("TERMINATOR")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("REIVER")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("APOTHECARY") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("ANCIENT") && !keywords.Contains("TERMINATOR")) ||
                    keywords.Contains("COMPANY CHAMPION")
                    )
                {
                    relics.Add("Silentus Pistol"); //Bolt Pistol, Heavy Bolt Pistol
                }

                relics.Add("Korvidari Bolts");
                relics.Add("Shard of Isstvan");
            }
            #endregion
            #region Ultramarines Relics
            if (currentSubFaction == "Ultramarines")
            {
                if (keywords.Contains("CAPTAIN") ||
                    keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS") ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    keywords.Contains("COMPANY CHAMPION") || keywords.Contains("CHAPTER CHAMPION") ||
                    (keywords.Contains("ANCIENT") && !(keywords.Contains("BLADEGUARD") || keywords.Contains("TERMINATOR"))))
                {
                    relics.Add("Soldier's Blade");
                }

                if(keywords.Contains("CAPTAIN"))
                {
                    relics.Add("The Sanctic Halo");
                }

                if(keywords.Contains("ANCIENT"))
                {
                    relics.Add("The Standard of Macragge Inviolate");
                }

                if(keywords.Contains("TERMINATOR"))
                {
                    relics.Add("Armour of Konor");
                }

                relics.Add("Helm of Censure");

                if((keywords.Contains("CAPTAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIBRARIAN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("ANCIENT") && !(keywords.Contains("PRIMARIS") || keywords.Contains("CHAPTER ANCIENT")))) 
                {
                    relics.Add("Vengeance of Ultramar"); //Firstborn only
                }

                relics.Add("Tarentian Cloak");
                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");
                relics.Add("Reliquary of Vengeance");
                relics.Add("Seal of Oath");
                relics.Add("Hellfury Bolts");

                if((keywords.Contains("CAPTAIN") && !(keywords.Contains("MK X GRAVIS") || keywords.Contains("PHOBOS") || keywords.Contains("TERMINATOR"))) ||
                    (keywords.Contains("CHAPLAIN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))) ||
                    keywords.Contains("COMPANY ANCIENT") ||
                    (keywords.Contains("LIBRARIAN") && !(keywords.Contains("PRIMARIS") || keywords.Contains("TERMINATOR"))) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS"))
                    )
                {
                    relics.Add("Sunwrath Pistol");
                }
            }
            #endregion
            #region White Scars Relics
            if(currentSubFaction == "White Scars")
            {
                if(keywords.Contains("PSYKER"))
                {
                    relics.Add("Mantle of the Stormseer");
                }

                relics.Add("The Hunter's Eye");

                if(keywords.Contains("ANCIENT"))
                {
                    relics.Add("Banner of the Eagle");
                }

                if(keywords.Contains("BIKER"))
                {
                    relics.Add("Wrath of the Heavens");
                }

                if ((keywords.Contains("CAPTAIN") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("PHOBOS")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    keywords.Contains("COMPANY CHAMPION") || keywords.Contains("COMPANY ANCIENT") ||
                    keywords.Contains("ANCIENT") && keywords.Contains("PRIMARIS") && !keywords.Contains("BLADEGUARD"))
                {
                    relics.Add("Scimitar of the Great Khan"); //Power Sword, MC Power Sword, Relic Blade
                }

                relics.Add("Plume of the Plainsrunner");

                if(keywords.Contains("KHAN"))
                {
                    relics.Add("Glaive of Vengeance");
                }

                relics.Add("Adamantine Mantle");
                relics.Add("Artificer Armour");
                relics.Add("Master-crafted Weapon");
                relics.Add("Digital Weapons");

                if ((keywords.Contains("CAPTAIN") && !(keywords.Contains("MK X GRAVIS") || keywords.Contains("TERMINATOR"))) ||
                    (keywords.Contains("CHAPLAIN") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("LIBRARIAN") && !keywords.Contains("TERMINATOR")) ||
                    (keywords.Contains("LIEUTENANT") && !keywords.Contains("REIVER")) ||
                    (keywords.Contains("TECHMARINE") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("APOTHECARY") && !keywords.Contains("PRIMARIS")) ||
                    (keywords.Contains("ANCIENT") && !keywords.Contains("TERMINATOR")) ||
                    keywords.Contains("COMPANY CHAMPION")
                    )
                {
                    relics.Add("Equis-pattern Bolt Pistol"); //Bolt Pistol, Heavy Bolt Pistol
                }

                relics.Add("Headtaker's Trophies");
                relics.Add("Stormwrath Bolts");
                relics.Add("Cyber-eagle Helm");
            }
            #endregion

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Dark Angels",
                "White Scars",
                "Space Wolves",
                "Imperial Fists",
                "Crimson Fists",
                "Black Templars",
                "Blood Angels",
                "Flesh Tearers",
                "Iron Hands",
                "Ultramarines",
                "Salamanders",
                "Raven Guard",
                "Deathwatch",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>();

            if(keyword == "Phobos")
            {
                traits.AddRange(new string[]
                {
                    "Shoot and Fade",
                    "Lord of Deciet",
                    "Master of the Vanguard",
                    "Stealth Adept",
                    "Target Priority",
                    "Master Marksman"
                });
            }
            else
            {
                traits.AddRange(new string[]
                {
                    "Fear Made Manifest",
                    "The Imperium's Sword",
                    "Iron Resolve",
                    "Champion of Humanity",
                    "Storm of Fire",
                    "Rites of War"
                });
            }

            if (currentSubFaction == "Dark Angels") {

                if(keyword == "Ravenwing")
                {
                    traits.Add("Lightning-Fast Reactions");
                    traits.Add("Master of Manoeuvre");
                }
                else if (keyword == "Deathwing")
                {
                    traits.Add("Watched");
                    traits.Add("Inexorable");
                }
                else
                {
                    traits.Add("Brilliant Strategist");
                    traits.Add("Fury of the Lion");
                    traits.Add("Calibanite Knight");
                    traits.Add("Stubborn Tenacity");
                    traits.Add("Decisive Tactician");
                    traits.Add("Honour of the First Legion");
                }
            }
            else if (currentSubFaction == "White Scars") 
            { 
                traits.Add("Deadly Hunter");
                traits.Add("Chogorian Storm");
                traits.Add("Trophy Taker");
                traits.Add("Master Rider");
                traits.Add("Hunter's Instincts");
                traits.Add("Master of Snares");
            }
            else if (currentSubFaction == "Space Wolves") 
            { 
                traits.Add("Beastslayer");
                traits.Add("Wolfkin");
                traits.Add("Warrior Born");
                traits.Add("Hunter");
                traits.Add("Aura of Majesty");
                traits.Add("Resolve of the Bear");
            }
            else if (currentSubFaction == "Imperial Fists") 
            {
                traits.Add("Siege Master");
                traits.Add("Indomitable");
                traits.Add("Fleetmaster");
                traits.Add("Stubborn Heroism");
                traits.Add("Architect of War");
                traits.Add("Hand of Dorn");
            }
            else if (currentSubFaction == "Crimson Fists") 
            { 
                traits.Add("Refuse to Die");
                traits.Add("Tenacious Opponent");
                traits.Add("Stoic Defender");
            }
            else if (currentSubFaction == "Black Templars") { traits.Add("Oathkeeper"); }
            else if (currentSubFaction == "Blood Angels") { traits.Add("Speed of the Primarch"); }
            else if (currentSubFaction == "Flesh Tearers") { traits.Add("Merciless Butcher"); }
            else if (currentSubFaction == "Iron Hands") 
            { 
                traits.Add("Adept of the Omnissiah");
                traits.Add("Will of Iron");
                traits.Add("All Flesh is Weakness");
                traits.Add("Student of History");
                traits.Add("Merciless Logic");
                traits.Add("Target Protocols");
            }
            else if (currentSubFaction == "Ultramarines") 
            { 
                traits.Add("Adept of the Codex");
                traits.Add("Master of Stratgey");
                traits.Add("Calm Under Fire");
                traits.Add("Paragon of War");
                traits.Add("Nobility Made Manifest");
                traits.Add("Warden of Macragge");
            }
            else if (currentSubFaction == "Salamanders") 
            { 
                traits.Add("Anvil of Strength");
                traits.Add("Miraculous Constitution");
                traits.Add("Never Give Up");
                traits.Add("Forge Master");
                traits.Add("Lord of Fire");
                traits.Add("Patient and Determined");
            }
            else if (currentSubFaction == "Raven Guard") 
            { 
                traits.Add("Shadowmaster");
                traits.Add("Master of Ambush");
                traits.Add("Swift and Deadly");
                traits.Add("Master of Vigilance");
                traits.Add("Feigned Flight");
                traits.Add("Echo of the Ravenspire");
            }
            else if (currentSubFaction == "Deathwatch")
            {
                traits.Add("Vigilance Incarnate");
                traits.Add("Paragon of Their Chapter");
                traits.Add("Nowhere to Hide");
                traits.Add("Optimised Priority");
                traits.Add("Castellan of the Black Vault");
                traits.Add("The Ties That Bind");
            }

            return traits;
        }

        public override void SetPoints(int points)
        {
            StratagemCount = new int[] { 0, 0 };
            StratagemLimit = new int[] { points / 1000, points / 1000 };

            if (points < 1000)
            {
                StratagemLimit[0] = 1;
                StratagemLimit[1] = 1;
            }
        }

        public override void SetSubFactionPanel(Panel panel)
        {
            if(antiLoop)
            {
                return;
            }

            antiLoop = true;
            Template template = new Template();
            template.LoadFactionTemplate(3, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            ComboBox cmbSubCustom2 = panel.Controls["cmbSubCustom2"] as ComboBox;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;
            Label lblSubCustom2 = panel.Controls["lblSubCustom2"] as Label;

            if (currentSubFaction != "<Custom>")
            {
                cmbSubCustom1.Visible = false;
                cmbSubCustom2.Visible = false;
                lblSubCustom1.Visible = false;
                lblSubCustom2.Visible = false;
            }
            else
            {
                cmbSubCustom1.Visible = true;
                cmbSubCustom2.Visible = true;
                lblSubCustom1.Visible = true;
                lblSubCustom2.Visible = true;
            }

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
            panel.BringToFront();

            cmbSubCustom1.Items.Clear();
            cmbSubCustom2.Items.Clear();

            cmbSubCustom1.Items.AddRange(new string[]
            {
                "Bolter Fusillades",
                "Born Heroes",
                "Duellists",
                "Fearsome Aspect",
                "Hungry for Battle",
                "Indomitable",
                "Knowledge is Power",
                "Long-range Marksmen",
                "Master Artisans",
                "Preferred Enemy",
                "Rapid Assault",
                "Scions of the Forge",
                "Stalwart",
                "Stealthy",
                "Stoic",
                "Tactical Withdrawal",
                "Warded",
                "Whirlwind of Rage"
            });

            cmbSubCustom2.Items.AddRange(new string[]
            {
                "Bolter Fusillades",
                "Born Heroes",
                "Duellists",
                "Fearsome Aspect",
                "Hungry for Battle",
                "Indomitable",
                "Knowledge is Power",
                "Long-range Marksmen",
                "Master Artisans",
                "Preferred Enemy",
                "Rapid Assault",
                "Scions of the Forge",
                "Stalwart",
                "Stealthy",
                "Stoic",
                "Tactical Withdrawal",
                "Warded",
                "Whirlwind of Rage"
            });

            if (customSubFactionTraits[0] != null)
            {
                cmbSubCustom1.SelectedIndex = cmbSubCustom1.Items.IndexOf(customSubFactionTraits[0]);
                cmbSubCustom2.SelectedIndex = cmbSubCustom2.Items.IndexOf(customSubFactionTraits[1]);
            }
            antiLoop = false;
        }

        public override void SaveSubFaction(int code, Panel panel)
        {
            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            ComboBox cmbSubCustom2 = panel.Controls["cmbSubCustom2"] as ComboBox;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;
            Label lblSubCustom2 = panel.Controls["lblSubCustom2"] as Label;

            switch (code)
            {
                case 50:
                    currentSubFaction = cmbSubFaction.SelectedItem.ToString();
                    if(currentSubFaction == "<Custom>")
                    {
                        cmbSubCustom1.Visible = true;
                        cmbSubCustom2.Visible = true;
                        lblSubCustom1.Visible = true;
                        lblSubCustom2.Visible = true;
                    }
                    else
                    {
                        cmbSubCustom1.Visible = false;
                        cmbSubCustom2.Visible = false;
                        lblSubCustom1.Visible = false;
                        lblSubCustom2.Visible = false;
                        customSubFactionTraits = new string[2];
                    }
                    break;
                case 51:
                    customSubFactionTraits[0] = cmbSubCustom1.SelectedItem.ToString();
                    break;
                case 52:
                    customSubFactionTraits[1] = cmbSubCustom2.SelectedItem.ToString();
                    break;
            }
        }

        public override string ToString()
        {
            return "Space Marines";
        }
    }
}
