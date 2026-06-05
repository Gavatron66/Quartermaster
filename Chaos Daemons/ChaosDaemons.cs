using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Roster_Builder.Chaos_Daemons
{
    public class ChaosDaemons : Faction
    {
        public ChaosDaemons()
        {
            subFactionName = "Chaos Daemons";
            currentSubFaction = "Legiones Daemonica";
            factionUpgradeName = "Exalted Greater Daemon";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Relic to a Character (PLACEHOLDER)",
                "Stratagem: YOU SHOULDN'T SEE THIS"
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
                new Skarbrand(),
                new Bloodthirster(),
                new Skulltaker(),
                new Bloodmaster(),
                new Skullmaster(),
                new Rendmaster(),
                new Karanak(),
                new KairosFateweaver(),
                new LordOfChange(),
                new TheChangeling(),
                new Fateskimmer(),
                new Fluxmaster(),
                new TheBlueScribes(),
                new Changecaster(),
                new Rotigus(),
                new GreatUncleanOne(),
                new Poxbringer(),
                new SpoilpoxScrivener(),
                new Epidemius(),
                new SloppityBilepiper(),
                new HorticulousSlimux(),
                new ShalaxiHelbane(),
                new KeeperOfSecrets(),
                new InfernalEnrapturess(),
                new MasqueOfSlaanesh(),
                new SyllEsske(),
                new ContortedEpitome(),
                new TormentbringerExalted(),
                new Tranceweaver(),
                new TormentbringerChariot(),
                new TormentbringerHellflayer(),
                new Belakor(),
                new ChaosDaemonPrince(),
                //---------- Troops ----------
                new Bloodletters(),
                new BlueHorrors(),
                new PinkHorrors(),
                new Plaguebearers(),
                new Nurglings(),
                new Daemonettes(),
                //---------- Elites ----------
                new Bloodcrushers(),
                new Flamers(),
                new ExaltedFlamer(),
                new BeastsOfNurgle(),
                new Fiends(),
                //---------- Fast Attack ----------
                new FleshHounds(),
                new Screamers(),
                new PlagueDrones(),
                new Seekers(),
                new Hellflayer(),
                new SeekerChariot(),
                //---------- Heavy Support ----------
                new SkullCannon(),
                new BurningChariot(),
                new ExaltedSeekerChariot(),
                new SoulGrinder(),
                //---------- Fortifications ----------
                new SkullAltar(),
                new FeculentGnarlmaw()
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            string[] fifty = new string[]
            {
                "Indomitable Onslaught (+50 pts)"
            };

            string[] thirtyfive = new string[]
            {
                "Rage Unchained (+35 pts)",
                "Architect of Deception (+35 pts)",
                "Master Mutator (+35 pts)",
                "Diaphonus Panoply (+35 pts)"
            };

            string[] thirty = new string[]
            {
                "Revoltingly Resilient (+30 pts)"
            };

            string[] twentyfive = new string[]
            {
                "Bountiful Gifts (+25 pts)",
                "Epicurean of Agonies (+25 pts)"
            };

            string[] twenty = new string[]
            {
                "Master of the Blood Tithe (+20 pts)",
                "Nexus of Fate (+20 pts)",
                "Hideous Visage (+20 pts)",
                "Insatiable Onslaught (+20 pts)"
            };

            if (fifty.Contains(upgrade))
            {
                return 50;
            }
            else if (thirtyfive.Contains(upgrade))
            {
                return 35;
            }
            else if (thirty.Contains(upgrade))
            {
                return 30;
            }
            else if (twentyfive.Contains(upgrade))
            {
                return 25;
            }
            else if (twenty.Contains(upgrade))
            {
                return 20;
            }
            else
            {
                return 0;
            }
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            if(keywords.Contains("KHORNE"))
            {
                return new List<string>()
                {
                    "(None)",
                    "Indomitable Onslaught (+50 pts)",
                    "Master of the Blood Tithe (+20 pts)",
                    "Rage Unchained (+35 pts)"
                };
            }
            else if(keywords.Contains("TZEENTCH"))
            {
                return new List<string>()
                {
                    "(None)",
                    "Architect of Deception (+35 pts)",
                    "Master Mutator (+35 pts)",
                    "Nexus of Fate (+20 pts)"
                };
            }
            else if(keywords.Contains("NURGLE"))
            {
                return new List<string>()
                {
                    "(None)",
                    "Bountiful Gifts (+25 pts)",
                    "Hideous Visage (+20 pts)",
                    "Revoltingly Resilient (+30 pts)"
                };
            }
            else if(keywords.Contains("SLAANESH"))
            {
                return new List<string>()
                {
                    "(None)",
                    "Diaphonus Panoply (+35 pts)",
                    "Epicurean of Agonies (+25 pts)",
                    "Insatiable Onslaught (+20 pts)"
                };
            }
            else if(keywords.Contains("<ALLEGIANCE>"))
            {
                return new List<string>()
                {
                    "Khorne",
                    "Tzeentch",
                    "Nurgle",
                    "Slaanesh"
                };
            }
            else
            {
                return new List<string>();
            }
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            if(keywords.Contains("TZEENTCH"))
            {
                return new List<string>()
                {
                    "Boon of Change",
                    "Bolt of Change",
                    "Gaze of Fate",
                    "Treason of Tzeentch",
                    "Infernal Flames",
                    "Infernal Gateway"
                };
            }
            else if (keywords.Contains("NURGLE"))
            {
                return new List<string>()
                {
                    "Stream of Corruption",
                    "Fleshy Abundance",
                    "Nurgle's Rot",
                    "Shrivelling Pox",
                    "Virulent Blessing",
                    "Malodorous Pall"
                };
            }
            else if (keywords.Contains("SLAANESH"))
            {
                return new List<string>()
                {
                    "Cacophonic Choir",
                    "Symphony of Pain",
                    "Hysterical Frenzy",
                    "Delightful Agonies",
                    "Pavane of Slaanesh",
                    "Phantasmagoria"
                };
            }
            else if(keywords.Contains("NOCTIC"))
            {
                return new List<string>()
                {
                    "Shrouded Step",
                    "Wreathed in Shades",
                    "Pall of Despair",
                    "Voidslivers",
                    "Penumbral Curse",
                    "Betraying Shades"
                };
            }
            else
            {
                return new List<string>();
            }
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>() { "(None)" };

            if (keywords.Contains("KHORNE"))
            {
                if(keywords.Contains("MONSTER"))
                {
                    relics.Add("Armour of Scorn");
                }

                relics.AddRange(new string[]
                {
                    "The Crimson Crown",
                    "Rune of Brass",
                    "Blood-drinker Talisman",
                    "A'rgath, the King of Blades"
                });

                if(keywords.Contains("BLOODTHIRSTER"))
                {
                    relics.Add("Skullreaver");
                }
            }
            else if (keywords.Contains("TZEENTCH"))
            {
                if(keywords.Contains("PSYKER"))
                {
                    relics.Add("The Endless Grimoire");
                }

                relics.Add("The Impossible Robe");

                if(keywords.Contains("PSYKER"))
                {
                    relics.Add("The Everstave");
                }

                relics.Add("Warpfire Blade");

                if(keywords.Contains("LORD OF CHANGE"))
                {
                    relics.Add("Soulbane");
                }

                if (keywords.Contains("PSYKER"))
                {
                    relics.Add("Soul-eater Stave");
                }
            }
            else if (keywords.Contains("NURGLE"))
            {
                relics.Add("Horn of Nurgle's Rot");
                relics.Add("The Entropic Knell");
                
                if(keywords.Contains("PSYKER"))
                {
                    relics.Add("Tome of a Thousand Poxes");
                }

                relics.Add("Corruption");

                if(keywords.Contains("GREAT UNCLEAN ONE"))
                {
                    relics.Add("Effluvior");
                }

                relics.Add("The Endless Gift");
            }
            else if (keywords.Contains("SLAANESH"))
            {
                relics.Add("The Forbidden Gem");
                relics.Add("The Mark of Excess");

                if(keywords.Contains("KEEPER OF SECRETS") || keywords.Contains("DAEMON PRINCE"))
                {
                    relics.Add("Soulstealer");
                }

                if(!keywords.Contains("DAEMON PRINCE"))
                {
                    relics.Add("Slothful Claws");
                }

                if(keywords.Contains("KEEPER OF SECRETS"))
                {
                    relics.Add("Silverstrike");
                }

                if (keywords.Contains("KEEPER OF SECRETS") || keywords.Contains("TORMENTBRINGER"))
                {
                    relics.Add("Whip of Agony");
                }
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>();
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            if(keyword.Contains("KHORNE"))
            {
                return new List<string>()
                {
                    "Aspect of Death",
                    "Brazen Hide",
                    "Devastating Blow",
                    "Glory of Battle",
                    "Immense Power",
                    "Rage Incarnate"
                };
            }
            else if (keyword.Contains("TZEENTCH"))
            {
                return new List<string>()
                {
                    "Born of Sorcery",
                    "Incorporeal Form",
                    "Fractal Mind",
                    "Warp Tether",
                    "Lorekeeper of Tzeentch",
                    "Tyrant of the Warp"
                };
            }
            else if (keyword.Contains("NURGLE"))
            {
                return new List<string>()
                {
                    "Heaving Mass",
                    "Acidic Ichor",
                    "Virulent Touch",
                    "Plague Fly Hive",
                    "Overflowing Fecundity",
                    "Pestilent Miasma"
                };
            }
            else if (keyword.Contains("SLAANESH"))
            {
                return new List<string>()
                {
                    "Warp Mists",
                    "Fatal Caress",
                    "The Murderdance",
                    "Quicksilver Duellist",
                    "Savage Hedonist",
                    "Aura of Bewitchment"
                };
            }
            else
            {
                return new List<string>();
            }
        }

        public override void SaveSubFaction(int code, Panel panel)
        {

        }

        public override void SetPoints(int points)
        {

        }

        public override void SetSubFactionPanel(Panel panel)
        {
            Template template = new Template();
            template.LoadFactionTemplate(-1, panel);
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }

        public override string ToString()
        {
            return "Chaos Daemons";
        }
    }
}
