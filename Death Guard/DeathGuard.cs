using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DeathGuard : Faction
    {
        public DeathGuard()
        {
            subFactionName = "<Plague Company>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Deadly Pathogen";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Plague-Chosen", //Warlord Stratagem
                "Stratagem: Gifts of Decay", //Relic Stratagem
                "Stratagem: Sevenfold Blessings", //Stratagem Code 1
                "Stratagem: Champion of Disease", //Stratagem Code 2
                "Stratagem: Grandfatherly Influence"
            });
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>() { 
            //---------- HQ ----------
            new DG_DaemonPrince(),
            new Typhus(),
            new DG_ChaosLord(),
            new DG_TerminatorChaosLord(),
            new LordOfVirulence(),
            new LordOfContagion(),
            new DG_TerminatorSorcerer(),
            new MalignantPlaguecaster(),
            //---------- Troops ----------
            new PlagueMarines(),
            new DG_Cultists(),
            new Poxwalkers(),
            //---------- Elite ----------
            new NoxiousBlightbringer(),
            new FoulBlightspawn(),
            new BiologusPutrifier(),
            new Tallyman(),
            new PlagueSurgeon(),
            new BlightlordTerminators(),
            new DeathshroudTerminators(),
            new Helbrute(),
            new DG_Possessed(), 
            //---------- Fast Attack ----------
            new ChaosSpawn(),
            new MyphiticBlightHauler(),
            new FoetidBloatDrone(),
            //---------- Heavy Support ----------
            new PlagueburstCrawler(),
            new ChaosLandRaider(),
            new ChaosPredatorAnnihilator(),
            new ChaosPredatorDestructor(),
            new Defiler(), 
            //---------- Transport ----------
            new ChaosRhino(),
            //---------- Lord of War ----------
            new Mortarion(), 
            //---------- Fortification ----------
            new MiasmicMalignifier(),
        };
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
                "Miasma of Pestilence",
                "Gift of Contagion",
                "Plague Wind",
                "Putrescent Vitality",
                "Curse of the Leper",
                "Gift of Plagues"
            };
        }

        public override List<String> GetWarlordTraits()
        {
            List<string> traits = new List<string>()
            {
                "Revoltingly Resilient",
                "Living Plague [Aura]",
                "Hulking Physique",
                "Arch-Contaminator [Aura]",
                "Rotten Constitution",
                "Foul Effluents [Aura]"
            };

            if (currentSubFaction != string.Empty)
            {
                if (currentSubFaction == "Harbingers") { traits.Add("Shamblerot (Contagion)"); }
                else if (currentSubFaction == "The Inexorable") { traits.Add("Ferric Blight (Contagion)"); }
                else if (currentSubFaction == "Mortarion's Anvil") { traits.Add("Gloaming Bloat (Contagion)"); }
                else if (currentSubFaction == "The Wretched") { traits.Add("Eater Plague (Contagion)"); }
                else if (currentSubFaction == "The Poxmongers") { traits.Add("Sanguous Flux (Contagion)"); }
                else if (currentSubFaction == "The Ferrymen") { traits.Add("The Droning (Contagion)"); }
                else if (currentSubFaction == "Mortarion's Chosen Sons") { traits.Add("Nurgle's Fruit (Contagion)"); }
            }

            return traits;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            return new List<string>()
            {
                "(None)",
                "Acidic Malady",
                "Explosive Outbreak",
                "Virulent Fever",
                "Befouling Runoff",
                "Unstable Sickness",
                "Corrosive Filth",
                "Viscous Death"
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            string[] twenty = new string[]
            {
                "Acidic Malady", "Explosive Outbreak", "Virulent Fever", "Corrosive Filth"
            };

            string[] ten = new string[]
            {
                "Befouling Runoff", "Viscous Death"
            };

            if (twenty.Contains(upgrade))
            {
                return 20;
            }
            else if (ten.Contains(upgrade))
            {
                return 10;
            }
            else if (upgrade == "Unstable Sickness")
            {
                return 15;
            }
            else
            {
                return 0;
            }
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "Harbingers",
                "The Inexorable",
                "Mortarion's Anvil",
                "The Wretched",
                "The Poxmongers",
                "The Ferrymen",
                "Mortarion's Chosen Sons"
            };
        }

        public override List<string> GetRelics(List<string> Keywords)
        {
            List<string> relicsList = new List<string>();

            relicsList.Add("(None)");

            if(Keywords.Contains("LORD OF CONTAGION"))
            {
                relicsList.Add("Reaper of Glorious Entropy");
            }

            relicsList.Add("Plague Skull of Glothila");

            if(Keywords.Contains("NOXIOUS BLIGHTBRINGER"))
            {
                relicsList.Add("Daemon's Toll");
            }

            relicsList.Add("Fugaris' Helm");

            if(Keywords.Contains("SORCERER") || Keywords.Contains("MALIGNANT PLAGUECASTER"))
            {
                relicsList.Add("Putrid Periapt");
            }

            if (Keywords.Contains("TALLYMAN"))
            {
                relicsList.Add("Tollkeeper");
            }

            if (Keywords.Contains("FOUL BLIGHTSPAWN"))
            {
                relicsList.Add("Revolting Stench-vats");
            }

            relicsList.Add("Suppurating Plate");

            if (Keywords.Contains("CHAOS LORD") || Keywords.Contains("BIOLOGUS PUTRIFIER")
                || Keywords.Contains("PLAGUE SURGEON"))
            {
                relicsList.Add("Plaguebringer");
            }

            if (currentSubFaction != string.Empty)
            {
                if (currentSubFaction == "Harbingers") { relicsList.Add("Infected Remains"); }
                else if (currentSubFaction == "The Inexorable") { relicsList.Add("Leechspore Casket"); }
                else if (currentSubFaction == "Mortarion's Anvil") { relicsList.Add("Warp Insect Hive"); }
                else if (currentSubFaction == "The Wretched") { relicsList.Add("The Daemon's Favour"); }
                else if (currentSubFaction == "The Poxmongers") { relicsList.Add("Ironclot Furnace"); }
                else if (currentSubFaction == "The Ferrymen") { relicsList.Add("Ferryman's Scythe"); }
                else if (currentSubFaction == "Mortarion's Chosen Sons") { relicsList.Add("Vomitryx"); }
            }

            return relicsList;
        }

        public override string ToString()
        {
            return "Death Guard";
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>();
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>();
        }

        public override void SetPoints(int points)
        {
            StratagemCount = new int[] { 0, 0, 0, 0, 0 };
            StratagemLimit = new int[] { 1 + points / 1000, 1 + points / 1000, 1, 1 + points / 1000, 99 };

            if (points % 1000 == 0)
            {
                StratagemLimit[0] -= 1;
                StratagemLimit[1] -= 1;
                StratagemLimit[3] -= 1;
            }
        }
    }
}
