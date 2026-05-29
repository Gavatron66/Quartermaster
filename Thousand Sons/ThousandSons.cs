using Roster_Builder.Grey_Knights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class ThousandSons : Faction
    {
        public ThousandSons()
        {
            subFactionName = "<Great Cult>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Legion Command";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: High Acolytes",
                "Stratagem: Sorcererous Arcana",
                "Stratagem: Aspiring Magister"
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
            return new List<Datasheets>
            {
                //---------- HQ ----------
                new Ahriman(),
                new TS_DaemonPrince(),
                new InfernalMaster(),
                new TS_Sorcerer(),
                new ExaltedSorcerer(),
                new TS_TerminatorSorcerer(),
                //---------- Troops ----------
                new TS_Cultists(),
                new Tzaangors(),
                new RubricMarines(),
                //---------- Elites ----------
                new TS_Helbrute(),
                new ScarabOccult(),
                new TzaangorShaman(),
                //---------- Fast Attack ----------
                new TzaangorEnlightened(),
                new TS_ChaosSpawn(),
                //---------- Transport ----------
                new TS_ChaosRhino(),
                //---------- Heavy Support ----------
                new MutalithVortexBeast(),
                new TS_ChaosVindicator(),
                new TS_ChaosLandRaider(),
                new TS_Defiler(),
                new TS_Forgefiend(),
                new TS_Maulerfiend(),
                new TS_ChaosPredatorDestructor(),
                new TS_ChaosPredatorAnnihilator(),
                //---------- Flyer ----------
                new TS_Heldrake(),
                //---------- Lord of War ----------
                new MagnusTheRed()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            string[] thirty = new string[]
            {
                "Dilettante (+30 pts)"
            };

            string[] twenty = new string[]
            {
                "Rehati (+20 pts)"
            };

            string[] fifteen = new string[]
            {
                "Ardent Automata (+15 pts)"
            };

            string[] ten = new string[]
            {
                "Paradigm of Change (+10 pts)",
                "Loyal Thrall (+10 pts)",
                "Witch-warrior (+10 pts)",
                "Rites of Coalescence (+10 pts)"
            };

            string[] five = new string[]
            {
                "Battle-psyker (+5 pts)",
                "Protégé (+5 pts)"
            };

            if(thirty.Contains(upgrade))
            {
                return 30;
            }

            if (twenty.Contains(upgrade))
            {
                return 20;
            }

            if (fifteen.Contains(upgrade))
            {
                return 15;
            }

            if (ten.Contains(upgrade))
            {
                return 10;
            }

            if (five.Contains(upgrade))
            {
                return 5;
            }

            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>()
            {
                "(None)"
            };

            if(keywords.Contains("EXALTED SORCERER"))
            {
                upgrades.AddRange(new string[]
                {
                    "Rehati (+20 pts)",
                    "Paradigm of Change (+10 pts)",
                    "Dilettante (+30 pts)"
                });
            }

            if(keywords.Contains("SORCERER"))
            {
                upgrades.AddRange(new string[]
                {
                    "Loyal Thrall (+10 pts)",
                    "Witch-warrior (+10 pts)",
                    "Battle-psyker (+5 pts)"
                });
            }

            if(keywords.Contains("CORE"))
            {
                upgrades.AddRange(new string[]
                {
                    "Ardent Automata (+15 pts)",
                    "Protégé (+5 pts)",
                    "Rites of Coalescence (+10 pts)"
                });
            }

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            if(keywords == "Change")
            {
                return new List<string>
                {
                    "Gaze of Hate",
                    "Twist of Fate",
                    "Dark Blessing",
                    "Presage",
                    "Swelled by the Warp",
                    "Temporal Surge",
                    "Empyric Guidance",
                    "Psychic Stalk",
                    "Desecration of Worlds"
                };
            }
            else if(keywords == "Dark Pacts")
            {
                return new List<string>
                {
                    "Bladed Maelstrom",
                    "Fires of the Abyss",
                    "Capering Imps",
                    "Diabolic Savant",
                    "Glimpse of Eternity",
                    "Malefic Maelstrom"
                };
            }
            else
            {
                return new List<string>
                {
                    "Tzeentch's Firestorm",
                    "Glamour of Tzeentch",
                    "Doombolt",
                    "Temporal Manipulation",
                    "Weaver of Fates",
                    "Baleful Devolution",
                    "Cacodaemonic Curse",
                    "Pyric Flux",
                    "Perplex",
                    "Gaze of Hate",
                    "Twist of Fate",
                    "Dark Blessing",
                    "Presage",
                    "Swelled by the Warp",
                    "Temporal Surge",
                    "Empyric Guidance",
                    "Psychic Stalk",
                    "Desecration of Worlds"
                };
            }
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>()
            {
                "(None)"
            };

            //Rubric Marine Aspiring Sorcerer or Scarab Occult Sorcerer
            if(keywords.Contains("RUBRIC MARINES") || keywords.Contains("SCARAB OCCULT TERMINATORS"))
            {
                if(!keywords.Contains("TERMINATOR"))
                {
                    relics.Add("Coruscator");
                }

                relics.Add("Skaeloch's Talon");
                relics.Add("Incandaeum");
                relics.Add("The Stave Abominus");

                return relics;
            }

            if(keywords.Contains("SORCERER") || keywords.Contains("EXALTED SORCERER"))
            {
                relics.Add("Seer's Bane");
            }

            relics.Add("Umbralefic Crystal");
            relics.Add("Helm of the Daemon's Eye");

            if(keywords.Contains("EXALTED SORCERER") || keywords.Contains("INFERNAL MASTER")
                || (keywords.Contains("SORCERER") && !keywords.Contains("TERMINATOR")))
            {
                relics.Add("Coruscator");
            }

            if(keywords.Contains("EXALTED SORCERER"))
            {
                relics.Add("Athenaean Scrolls");
            }

            if(keywords.Contains("EXALTED SORCERER") || keywords.Contains("TZAANGOR"))
            {
                relics.Add("Thrydderghyre");
            }

            relics.Add("Egleighen's Orrery");
            relics.Add("The Chronos Tutorum");

            if(!keywords.Contains("DAEMON PRINCE"))
            {
                relics.Add("Skaeloch's Talon");
            }

            if(!keywords.Contains("TZAANGOR"))
            {
                relics.Add("Conniving Plate");
            }

            relics.Add("Warpweave Mantle");

            if(keywords.Contains("INFANTRY"))
            {
                relics.Add("Paradoxical Chatterfowl");
            }

            if(keywords.Contains("TZAANGOR"))
            {
                relics.Add("The Change-Wrought Chalice");
            }

            if(!keywords.Contains("DAEMON PRINCE"))
            {
                relics.Add("Incandaeum");
            }

            if(keywords.Contains("INFERNAL MASTER"))
            {
                relics.Add("Pentakairic Armour");
            }

            relics.Add("The Prism of Echoes");

            if (!keywords.Contains("DAEMON PRINCE"))
            {
                relics.Add("The Stave Abominus");
            }

            if (keywords.Contains("ARCANA ASTARTES"))
            {
                //if(currentSubFaction == "Cult of Mutation" && keywords.Contains("SORCERER"))
                if (keywords.Contains("SORCERER"))
                {
                    relics.Add("Exalted Mutation");
                }

                //if (currentSubFaction == "Cult of Prophecy")
                //{
                relics.Add("Oraculae Brazier");
                //}

                //if (currentSubFaction == "Cult of Time")
                //{
                relics.Add("Hourglass of Manat");
                //}

                //if (currentSubFaction == "Cult of Scheming")
                //{
                relics.Add("Cha'qi'thl's Theorem");
                //}

                //if (currentSubFaction == "Cult of Magic")
                //{
                relics.Add("Arcane Focus");
                //}

                //if (currentSubFaction == "Cult of Knowledge" && (keywords.Contains("EXALTED SORCERER")
                //    || (keywords.Contains("SORCERER") && !keywords.Contains("TERMINATOR"))))
                if (keywords.Contains("EXALTED SORCERER")
                    || (keywords.Contains("SORCERER") && !keywords.Contains("TERMINATOR")))
                {
                    relics.Add("Incaladion's Cry");
                }

                //if (currentSubFaction == "Cult of Change")
                //{
                relics.Add("Capricious Crest");
                //}

                //if (currentSubFaction == "Cult of Duplicity")
                //{
                relics.Add("Perfidious Tome");
                //}

                //if (currentSubFaction == "Cult of Manipulation")
                //{
                relics.Add("Sorthis' Mirror");
                //}
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Cult of Mutation",
                "Cult of Prophecy",
                "Cult of Time",
                "Cult of Scheming",
                "Cult of Magic",
                "Cult of Knowledge",
                "Cult of Change",
                "Cult of Duplicity",
                "Cult of Manipulation"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>();

            traits.AddRange(new string[]
            {
                "Arrogance of Aeons",
                "Seeker After Shadows",
                "Undying Form",
                "Lord of Forbidden Lore",
                "Otherworldly Prescience",
                "Aetherstride"
            });

            if(currentSubFaction == "Cult of Mutation")
            {
                traits.Add("Touch of Vicissitude");
            }
            if (currentSubFaction == "Cult of Prophecy")
            {
                traits.Add("Guided by the Whispers");
            }
            if (currentSubFaction == "Cult of Time")
            {
                traits.Add("Immaterial Echo");
            }
            if (currentSubFaction == "Cult of Scheming")
            {
                traits.Add("Grand Schemer");
            }
            if (currentSubFaction == "Cult of Magic")
            {
                traits.Add("Devastating Sorcery");
            }
            if (currentSubFaction == "Cult of Knowledge")
            {
                traits.Add("Ardent Scholar");
            }
            if (currentSubFaction == "Cult of Change")
            {
                traits.Add("Fickle Nature");
            }
            if (currentSubFaction == "Cult of Duplicity")
            {
                traits.Add("Master Misinformator");
            }
            if (currentSubFaction == "Cult of Manipulation")
            {
                traits.Add("Beguiling Influence");
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
            return "Thousand Sons";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
