using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class ChaosSpaceMarines : Faction
    {
        public ChaosSpaceMarines()
        {
            subFactionName = "<Legion>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Marks of Chaos";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Aspiring Lord",  //Warlord Trait
                "Stratagem: Gifts of Chaos",    //Relic
                "Stratagem: Trophies of the Long War", //Extra relic for an 'Aspiring' or 'Champion'
                "Stratagem: Lord of the Ezekarion", //Extra Warlord Trait for a Black Legion Warlord
                "Stratagem: Apostle of the Dark Council", //Buff to a Word Bearers Priest
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
            return new List<Datasheets>()
            {
                //---------- HQ ----------
                new AbaddonTheDespoiler(),
                new HaarkenWorldclaimer(),
                new HuronBlackheart(),
                new DaemonPrince(),
                new FabiusBile(),
                new Cypher(),
                new MasterOfPossession(),
                new ChaosLord(),
                new TerminatorChaosLord(),
                new Sorcerer(),
                new TerminatorSorcerer(),
                new LuciusTheEternal(),
                new LordDiscordant(),
                new Warpsmith(),
                new DarkApostle(),
                new ExaltedChampion(),
                new DarkCommune(),
                //---------- Troops ----------
                new Legionaries(),
                new CulistsMob(),
                new AccursedCultists(),
                //---------- Elites ----------
                new ChaosTerminators(),
                new MasterOfExecutions(),
                new Possessed(),
                new Chosen(),
                new Helbrute(),
                new KhorneBerzerkersCSM(),
                new RubricMarinesCSM(),
                new PlagueMarinesCSM(),
                new NoiseMarines(),
                //---------- Fast Attack ----------
                new Venomcrawler(),
                new ChaosSpawn(),
                new ChaosBikers(),
                new Raptors(),
                new WarpTalons(),
                //---------- Heavy Support ----------
                new Havocs(),
                new Obliterators(),
                new ChaosLandRaider(),
                new ChaosVindicator(),
                new ChaosPredatorDestructor(),
                new ChaosPredatorAnnihilator(),
                new Defiler(),
                new Forgefiend(),
                new Maulerfiend(),
                //---------- Transport ----------
                new ChaosRhino(),
                //---------- Flyer ----------
                new Heldrake(),
                //---------- Lord of War ----------
                new KhorneLordOfSkulls(),
                //---------- Fortification ----------
                new NoctilithCrown()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == "Mark of Slaanesh (+20 pts)")
            {
                return 20;
            }
            else if (upgrade == "Mark of Khorne (+15 pts)" || upgrade == "Mark of Tzeentch (+15 pts)" || upgrade == "Mark of Nurgle (+15 pts)")
            {
                return 15;
            }
            else
            {
                return 0;
            }
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            if(keywords.Contains("PSYKER"))
            {
                return new List<string>
                {
                    "(None)",
                    "Mark of Tzeentch (+15 pts)",
                    "Mark of Nurgle (+15 pts)",
                    "Mark of Slaanesh (+20 pts)"
                };
            }
            else
            {
                return new List<string>
                {
                    "(None)",
                    "Mark of Khorne (+15 pts)",
                    "Mark of Tzeentch (+15 pts)",
                    "Mark of Nurgle (+15 pts)",
                    "Mark of Slaanesh (+20 pts)"
                };
            }
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            if(keywords == "DH")
            {
                return new List<string>
                {
                    "Infernal Gaze",
                    "Prescience",
                    "Diabolic Strength",
                    "Death Hex",
                    "Gift of Chaos",
                    "Warptime"
                };
            }
            else if(keywords == "Malefic")
            {
                return new List<string>
                {
                    "Warp Marked",
                    "Pact of Flesh",
                    "Cursed Earth",
                    "Possession",
                    "Infernal Power",
                    "Mutated Invigoration"
                };
            }
            else if(keywords == "Prayer")
            {
                return new List<string>
                {
                    "Benediction of Darkness",
                    "Litany of Despair",
                    "Omen of Potency",
                    "Warp-sight Plea",
                    "Soultearer Portent",
                    "Illusory Supplication"
                };
            }

            return new List<string>();
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            if(!keywords.Contains("DARK COMMUNE"))
            {
                relics.Add("Ul'o'cca, the Black");
                relics.Add("Zaall, the Wrathful");
                relics.Add("G'holl'ax, the Decayed");
                relics.Add("Q'o'ak, the Boundless");
                relics.Add("Thaa'ris and Rhi-ol, the Rapacious");
            }
            
            if(keywords.Contains("PRIEST"))
            {
                relics.Add("Inferno Tome");
            }

            if (!keywords.Contains("DARK COMMUNE"))
            {
                relics.Add("Gorget of Eternal Hate");
            }

            relics.Add("Black Rune of Damnation");

            if (!keywords.Contains("DARK COMMUNE"))
            {
                relics.Add("Mantle of Traitors");
            }

            if(keywords.Contains("CHAOS LORD"))
            {
                relics.Add("Blade of the Relentless");
            }

            if (keywords.Contains("CHAOS LORD") || keywords.Contains("DARK APOSTLE"))
            {
                relics.Add("The Black Mace");
            }

            if((keywords.Contains("CHAOS LORD") && !keywords.Contains("TERMINATOR")) || keywords.Contains("DARK APOSTLE") ||
                keywords.Contains("EXALTED CHAMPION") || keywords.Contains("LORD DISCORDANT") || keywords.Contains("MASTER OF POSSESSION") ||
                (keywords.Contains("SORCERER") && !keywords.Contains("TERMINATOR")) || keywords.Contains("MASTER OF EXECUTIONS"))
            {
                relics.Add("The Warp's Malice");
            }

            if (!keywords.Contains("DARK COMMUNE"))
            {
                relics.Add("Talisman of Burning Blood");
            }

            if (keywords.Contains("PSYKER") && !keywords.Contains("DARK COMMUNE"))
            {
                relics.Add("Eye of Tzeentch");
            }

            if (!keywords.Contains("DARK COMMUNE"))
            {
                relics.Add("Orb of Unlife");
                relics.Add("Intoxicating Elixir");
            }

            if (keywords.Contains("PSYKER") && !keywords.Contains("DARK COMMUNE"))
            {
                relics.Add("Liber Hereticus");
            }

            if (currentSubFaction == "Black Legion")
            {
                if (keywords.Contains("CHAOS LORD"))
                {
                    relics.Add("Ghorisvex's Teeth");
                }

                if (!keywords.Contains("DARK COMMUNE") && !keywords.Contains("DAEMON PRINCE") && !keywords.Contains("WARPSMITH"))
                {
                    relics.Add("Loyalty's Reward");
                }

                if (keywords.Contains("TERMINATOR"))
                {
                    relics.Add("Veilbreaker Plate");
                }

                if (!keywords.Contains("DARK COMMUNE"))
                {
                    relics.Add("Cloak of Conquest");
                }

                relics.Add("Sightless Helm");

                if (!keywords.Contains("DARK COMMUNE"))
                {
                    relics.Add("Trophies of Slaughter");
                }

                if ((keywords.Contains("CHAOS LORD") && !keywords.Contains("TERMINATOR")) || keywords.Contains("WARPSMITH"))
                {
                    relics.Add("Wrath of the Abyss");
                }
            }

            if (currentSubFaction == "Word Bearers")
            {
                if (keywords.Contains("CHAOS LORD") || keywords.Contains("DARK APOSTLE"))
                {
                    relics.Add("Eightfold-cursed Crozius");
                }

                relics.Add("Crown of the Blasphemer");

                if (keywords.Contains("PSYKER") && !keywords.Contains("DARK COMMUNE"))
                {
                    relics.Add("Malefic Tome");
                }

                if (keywords.Contains("DARK APOSTLE"))
                {
                    relics.Add("Epistle of Lorgar");
                }

                if (keywords.Contains("CHAOS LORD") || keywords.Contains("EXALTED CHAMPION") || keywords.Contains("DAEMON PRINCE") ||
                    keywords.Contains("WARPSMITH") || keywords.Contains("MASTER OF EXECUTIONS"))
                {
                    relics.Add("Ashen Axe");
                }

                if (!keywords.Contains("DARK COMMUNE"))
                {
                    relics.Add("The Armour Diabolus");
                    relics.Add("Baleful Icon");
                }
            }

            if (currentSubFaction == "Night Lords")
            {
                if ((keywords.Contains("CHAOS LORD") && keywords.Contains("TERMINATOR")) || keywords.Contains("DAEMON PRINCE"))
                {
                    relics.Add("Claw of the Stygian Count");
                }

                relics.Add("Vox Daemonicus");

                if (keywords.Contains("DAEMON PRINCE"))
                {
                    relics.Add("Talons of the Night Terror");
                }

                if (!keywords.Contains("DARK COMMUNE")) {
                    relics.Add("Scourging Chains");
                    relics.Add("Misery of the Meek");
                }

                if (!keywords.Contains("DARK COMMUNE") && keywords.Contains("INFANTRY"))
                {
                    relics.Add("Stormbolt Plate");
                }

                if (keywords.Contains("CHAOS LORD") || (keywords.Contains("SORCERER") && keywords.Contains("TERMINATOR"))) {
                    relics.Add("Flayer");
                }
            }

            if (currentSubFaction == "Iron Warriors")
            {
                if (keywords.Contains("CHAOS LORD") || keywords.Contains("EXALTED CHAMPION") || keywords.Contains("DAEMON PRINCE") ||
                    keywords.Contains("WARPSMITH") || keywords.Contains("MASTER OF EXECUTIONS"))
                {
                    relics.Add("Axe of the Forgemaster");
                }

                if (keywords.Contains("CHAOS LORD") || keywords.Contains("DARK APOSTLE") || keywords.Contains("WARPSMITH"))
                {
                    relics.Add("Siegebreaker Mace");
                }

                if (!keywords.Contains("DARK COMMUNE") && keywords.Contains("INFANTRY"))
                {
                    relics.Add("Fleshmetal Exoskeleton");
                }

                relics.Add("Cranium Malevolus");

                if (!keywords.Contains("DARK COMMUNE") && keywords.Contains("INFANTRY"))
                {
                    relics.Add("Insidium");
                }

                if ((keywords.Contains("CHAOS LORD") && keywords.Contains("TERMINATOR")) || keywords.Contains("EXALTED CHAMPION") ||
                    (keywords.Contains("SORCERER") && keywords.Contains("TERMINATOR")))
                {
                    relics.Add("Spitespitter");
                }

                if (keywords.Contains("WARPSMITH"))
                {
                    relics.Add("Techno-venomous Mechatendrils");
                }
            }

            if (currentSubFaction == "Alpha Legion")
            {
                if (keywords.Contains("CHAOS LORD"))
                {
                    relics.Add("Blade of the Hydra");
                }

                if (!keywords.Contains("DARK COMMUNE") && keywords.Contains("INFANTRY"))
                {
                    relics.Add("Drakescale Plate");
                }

                if (!keywords.Contains("DARK COMMUNE"))
                {
                    relics.Add("Hydra's Wail");
                }

                if ((keywords.Contains("CHAOS LORD") && !keywords.Contains("TERMINATOR")) || keywords.Contains("DARK APOSTLE") ||
                    keywords.Contains("EXALTED CHAMPION") || keywords.Contains("LORD DISCORDANT") || keywords.Contains("MASTER OF POSSESSION") ||
                    (keywords.Contains("SORCERER") && !keywords.Contains("TERMINATOR")) || keywords.Contains("MASTER OF EXECUTIONS"))
                {
                    relics.Add("Viper's Spite");
                }

                if (!keywords.Contains("DARK COMMUNE") && !keywords.Contains("DAEMON PRINCE") && !keywords.Contains("WARPSMITH"))
                {
                    relics.Add("Hydra's Teeth");
                }

                relics.Add("Icon of the Hydra Cult");

                if (!keywords.Contains("DARK COMMUNE") && keywords.Contains("INFANTRY"))
                {
                    relics.Add("Mindveil");
                }
            }

            if (currentSubFaction == "Emperor's Children")
            {

                if (!keywords.Contains("DARK COMMUNE") && keywords.Contains("INFANTRY"))
                {
                    relics.Add("The Endless Grin");
                }

                if (!keywords.Contains("DARK COMMUNE"))
                {
                    relics.Add("Fatal Sonacy");
                    relics.Add("Armour of Abhorrence");
                }

                if (keywords.Contains("DARK APOSTLE"))
                {
                    relics.Add("Remnant of the Maraviglia");
                }

                if (keywords.Contains("CHAOS LORD") || (keywords.Contains("SORCERER") && keywords.Contains("TERMINATOR"))) 
                {
                    relics.Add("Distortion");
                }

                relics.Add("Raiment Revulsive");
            }

            if (currentSubFaction == "Red Corsairs")
            {

                if ((keywords.Contains("CHAOS LORD") && keywords.Contains("TERMINATOR")) || keywords.Contains("EXALTED CHAMPION") ||
                    (keywords.Contains("SORCERER") && keywords.Contains("TERMINATOR")))
                {
                    relics.Add("Maelstrom's Bite");
                }

                if (keywords.Contains("TERMINATOR"))
                {
                    relics.Add("Armour of Badab");
                }

                if (!keywords.Contains("DARK COMMUNE"))
                {
                    relics.Add("Traitor's Laurels");
                }
            }

            if (currentSubFaction == "Creations of Bile")
            {
                if (!keywords.Contains("DARK COMMUNE"))
                {
                    relics.Add("Helm of All-Seeing");
                }

                if (!keywords.Contains("DARK COMMUNE") && keywords.Contains("INFANTRY"))
                {
                    relics.Add("Living Carapace");
                }

                if (!keywords.Contains("DARK COMMUNE") && !keywords.Contains("DAEMON PRINCE") && !keywords.Contains("WARPSMITH"))
                {
                    relics.Add("Hyper-Growth Bolts");
                }
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Black Legion",
                "Word Bearers",
                "Night Lords",
                "Iron Warriors",
                "Alpha Legion",
                "Emperor's Children",
                "Red Corsairs",
                "Creations of Bile"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>();

            traits.AddRange(new string[]
            {
                "Flames of Spite",
                "Unholy Fortitude",
                "Hatred Incarnate",
                "Lord of Terror",
                "Eternal Vendetta",
                "Gaze of the Gods"
            });

            if (currentSubFaction == "Black Legion")
            {
                traits.AddRange(new string[]
                {
                    "Veteran Raiders",
                    "Indomitable",
                    "Merciless Overseer",
                    "Soul-eater",
                    "Trusted War Leader",
                    "Paragon of Hatred"
                });
            }

            if (currentSubFaction == "Word Bearers")
            {
                traits.AddRange(new string[]
                {
                    "The Voice of Lorgar",
                    "Exalted Possession",
                    "Daemonic Whispers",
                    "Master of the Union",
                    "Diabolist",
                    "Hate-fuelled Demagogue"
                });
            }

            if (currentSubFaction == "Night Lords")
            {
                traits.AddRange(new string[]
                {
                    "Night Haunter's Curse",
                    "One Piece at a Time",
                    "Murderous Reputation",
                    "Killing Fury",
                    "One With the Shadows",
                    "Dirty Fighter"
                });
            }

            if (currentSubFaction == "Iron Warriors")
            {
                traits.AddRange(new string[]
                {
                    "Siege Lord",
                    "Daemonsmith",
                    "Unyielding Mettle",
                    "Bastion",
                    "Architect of Destruction",
                    "Implacable Taskmaster"
                });
            }

            if (currentSubFaction == "Alpha Legion")
            {
                traits.AddRange(new string[]
                {
                    "I am Alpharius",
                    "Clandestine",
                    "Headhunter",
                    "Master of Diversion",
                    "Cult Leader",
                    "Covert Control"
                });
            }

            if (currentSubFaction == "Emperor's Children")
            {
                traits.AddRange(new string[]
                {
                    "Stimulated By Pain",
                    "Intoxicating Musk",
                    "Unbound Arrogance",
                    "Faultless Duellist",
                    "Glutton for Punishment",
                    "Loathsome Grace"
                });
            }

            if (currentSubFaction == "Red Corsairs")
            {
                traits.AddRange(new string[]
                {
                    "Reaver Lord",
                    "Angel of Hatred",
                    "Dark Raider"
                });
            }

            if (currentSubFaction == "Creations of Bile")
            {
                traits.AddRange(new string[]
                {
                    "Surgical Precision",
                    "Prime Test Subject",
                    "Twisted Regeneration"
                });
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

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }

        public override string ToString()
        {
            return "Chaos Space Marines";
        }
    }
}
