using Roster_Builder.Necrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Genestealer_Cults
{
    public class GSC : Faction
    {
        public GSC()
        {
            subFactionName = "<Cult>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Proficient Planning";
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
                new Patriarch(),
                new Primus(),
                new Magus(),
                new AcolyteIconward(),
                new JackalAlphus(),
                //---------- Troops ----------
                //new AcolyteHybrids(),
                //new NeophyteHybrids(),
                //---------- Elites ----------
                new PurestrainGenestealers(),
                //new HybridMetamorphs(),
                //new Aberrants(),
                new Abominant(),
                //new Nexos(),
                //new Clamavus(),
                //new Locus(),
                //new Kelermorph(),
                //new Sanctus(),
                //new ReductusSaboteur(),
                //new Biophagus(),
                //---------- Fast Attack ----------
                //new AtalanJackals(),
                //new AchillesRidgerunners(),
                //---------- Heavy Support ----------
                //new GoliathRockgrinder(),
                //---------- Transport ----------
                //new GoliathTruck(),
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] twenty = new string[]
            {
                "Lying in Wait",
                "Excavate"
            };

            string[] fifteen = new string[]
            {
                "Exacting Planner",
                "Alchemist Supreme",
                "A Trap Sprung",
                "A Perfect Ambush",
                "Meditations in Shadow",
                "Our Time is Nigh"
            };

            string[] ten = new string[]
            {
                "From Every Angle",
                "They Came From Below"
            };

            if(twenty.Contains(upgrade))
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

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            return new List<string>()
            {
                "(None)",
                "Lying in Wait",
                "Exacting Planner",
                "Alchemist Supreme",
                "A Trap Sprung",
                "A Perfect Ambush",
                "From Every Angle",
                "Meditations in Shadow",
                "Excavate",
                "They Came From Below",
                "Our Time is Nigh"
            };
        }

        public override List<string> GetPsykerPowers()
        {
            return new List<string>()
            {
                "Mass Hypnosis",
                "Mind Control",
                "Psionic Blast",
                "Mental Onslaught",
                "Psychic Stimulus",
                "Might From Beyond"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");
            /*
            relics.Add("Sword of the Void's Eye"); //Primus or Locus
            relics.Add("Hand of Aberrance"); //Primus
            relics.Add("Amulet of the Voidwyrm"); //Any
            relics.Add("Oppressor's Bane"); //Acolyte Iconward, Jackal Alphus, Magus, Biophagus, Clamavus, Nexos Reductus Saboteur
            relics.Add("Wyrmtooth Rounds"); //Kelermorph
            relics.Add("Dagger of Swift Sacrifice"); //Magus or Sanctus
            relics.Add("The Crouchling"); //Magus, Patriarch, Abominant, Biophagus, Sanctus
            relics.Add("The Gift From Beyond"); //Jackal Alphus or Sanctus
            relics.Add("The Unwilling Orb"); //Magus or Patriarch
            relics.Add("Cranial Inlay"); //Nexos
            relics.Add("The Voice of the Liberator"); //Clamavus

            relics.Add("Sword of the Four-armed Emperor"); //Cult of the Four-armed Emperor Primus or Locus
            relics.Add("Vockor's Talisman"); // Hivecult
            relics.Add("Mark of the Clawed Omnissiah"); // Bladed Cog
            relics.Add("The Nomad's Mantle"); //Rusted Claw
            relics.Add("Reliquary of Saint Tenndarc"); //Pauper Princes
            relics.Add("Elixir of the Prime Specimen"); //Twisted Helix
            */
            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "Cult of the Four-armed Emperor",
                "The Hivecult",
                "The Bladed Cog",
                "The Rusted Claw",
                "The Pauper Princes",
                "The Twisted Helix",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits()
        {
            List<string> traits = new List<string>()
            {
                "Focus of Adoration",
                "Shadow Stalker",
                "Biomorph Adapation",
                "Prowling Agitant",
                "Alien Majesty",
                "Preternatural Speed"
            };

            if (currentSubFaction == "Cult of the Four-armed Emperor") { traits.Add("Inscrutable Cunning"); }
            else if (currentSubFaction == "The Hivecult") { traits.Add("Hivelord"); }
            else if (currentSubFaction == "The Bladed Cog") { traits.Add("Single-minded Obsession"); }
            else if (currentSubFaction == "The Rusted Claw") { traits.Add("Entropic Touch"); }
            else if (currentSubFaction == "The Pauper Princes") { traits.Add("Xenoprophet"); }
            else if (currentSubFaction == "The Twisted Helix") { traits.Add("Bio-alchemist"); }

            return traits;
        }

        public override string ToString()
        {
            return "Genestealer Cults";
        }
    }
}
