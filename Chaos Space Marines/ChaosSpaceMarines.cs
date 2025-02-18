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
                //new KhorneBerzerkersCSM(),
                //new RubricMarinesCSM(),
                //new PlagueMarinesCSM(),
                //new NoiseMarines(),
                //---------- Fast Attack ----------
                new Venomcrawler(),
                //new ChaosSpawn(),
                //new ChaosBikers(),
                //new Raptors(),
                //new WarpTalons(),
                //---------- Heavy Support ----------
                //new Havocs(),
                //new Obliterators(),
                //new ChaosLandRaider(),
                //new ChaosVindicator(),
                //new ChaosPredatorDestructor(),
                //new ChaosPredatorAnnihilator(),
                //new Defiler(),
                //new Forgefiend(),
                //new Maulerfiend(),
                //---------- Transport ----------
                //new ChaosRhino(),
                //---------- Flyer ----------
                //new Heldrake(),
                //---------- Lord of War ----------
                //new KhorneLordOfSkulls(),
                //---------- Fortification ----------
                //new NoctilithCrown()
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

            relics.Add("Ul'o'cca, the Black");
            relics.Add("Zaall, the Wrathful");
            relics.Add("G'holl'ax, the Decayed");
            relics.Add("Q'o'ak, the Boundless");
            relics.Add("Thaa'ris and Rhi-ol, the Rapacious");
            relics.Add("Inferno Tome");
            relics.Add("Gorget of Eternal Hate");
            relics.Add("Black Rune of Damnation");
            relics.Add("Mantle of Traitors");
            relics.Add("Blade of the Relentless");
            relics.Add("The Black Mace");
            relics.Add("The Warp's Malice");
            relics.Add("Talisman of Burning Blood");
            relics.Add("Eye of Tzeentch");
            relics.Add("Orb of Unlife");
            relics.Add("Intoxicating Elixir");
            relics.Add("Liber Hereticus");

            if (currentSubFaction == "Black Legion")
            {
                relics.Add("Ghorisvex's Teeth");
                relics.Add("Loyalty's Reward");
                relics.Add("Veilbreaker Plate");
                relics.Add("Cloak of Conquest");
                relics.Add("Sightless Helm");
                relics.Add("Trophies of Slaughter");
                relics.Add("Wrath of the Abyss");
            }

            if (currentSubFaction == "Word Bearers")
            {
                relics.Add("Eightfold-cursed Crozius");
                relics.Add("Crown of the Blasphemer");
                relics.Add("Malefic Tome");
                relics.Add("Epistle of Lorgar");
                relics.Add("Ashen Axe");
                relics.Add("The Armour Diabolus");
                relics.Add("Baleful Icon");
            }

            if (currentSubFaction == "Night Lords")
            {
                relics.Add("Claw of the Stygian Count");
                relics.Add("Vox Daemonicus");
                relics.Add("Talons of the Night Terror");
                relics.Add("Scourging Chains");
                relics.Add("Misery of the Meek");
                relics.Add("Stormbolt Plate");
                relics.Add("Flayer");
            }

            if (currentSubFaction == "Iron Warriors")
            {
                relics.Add("Axe of the Forgemaster");
                relics.Add("Siegebreaker Mace");
                relics.Add("Fleshmetal Exoskeleton");
                relics.Add("Cranium Malevolus");
                relics.Add("Insidium");
                relics.Add("Spitespitter");
                relics.Add("Techno-venomous Mechatendrils");
            }

            if (currentSubFaction == "Alpha Legion")
            {
                relics.Add("Blade of the Hydra");
                relics.Add("Drakescale Plate");
                relics.Add("Hydra's Wail");
                relics.Add("Viper's Spite");
                relics.Add("Hydra's Teeth");
                relics.Add("Icon of the Hydra Cult");
                relics.Add("Mindveil");
            }

            if (currentSubFaction == "Emperor's Children")
            {
                relics.Add("The Endless Grin");
                relics.Add("Fatal Sonacy");
                relics.Add("Armour of Abhorrence");
                relics.Add("Remnant of the Maraviglia");
                relics.Add("Distortion");
                relics.Add("Raiment Revulsive");
            }

            if (currentSubFaction == "Red Corsairs")
            {
                relics.Add("Maelstrom's Bite");
                relics.Add("Armour of Badab");
                relics.Add("Traitor's Laurels");
            }

            if (currentSubFaction == "Creations of Bile")
            {
                relics.Add("Helm of All-Seeing");
                relics.Add("Living Carapace");
                relics.Add("Hyper-Growth Bolts");
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
