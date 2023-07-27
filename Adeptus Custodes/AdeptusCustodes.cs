using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Adeptus_Custodes
{
    public class AdeptusCustodes : Faction
    {
        public AdeptusCustodes()
        {
            subFactionName = "<Shield Host>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Captain-Commander";
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
                new TrajannValoris(),
                new ShieldCaptain(),
                new AllarusShieldCaptain(),
                new VertusShieldCaptain(),
                new BladeChampion(),
                new Valerian(),
                new Aleya(),
                new KnightCentura(),
                //---------- Troops ----------
                new CustodianGuard(),
                new Prosecutors(),
                //---------- Elites ----------
                new CustodianWardens(),
                new AllarusCustodians(),
                new VexilusPraetor(),
                new AllarusVexilusPraetor(),
                new VCDreadnought(),
                new Vigilators(),
                //---------- Fast Attack ----------
                new VertusPraetors(),
                new Witchseekers(),
                //---------- Heavy Support ----------
                new VenerableLandRaider(),
                //---------- Dedicated Transport ----------
                new AnathemaPsykanaRhino()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] twentyFive = new string[]
            {
                "Master of the Stances"
            };

            string[] twenty = new string[]
            {
                "Defiant to the Last", "Ceaseless Hunter"
            };

            string[] fifteen = new string[]
            {
                "Swift as the Eagle", "Inspirational Exemplar", "Bane of Abominations", "Fierce Conqueror", "Tip of the Spear"
            };

            string[] ten = new string[]
            {
                "Unstoppable Destroyer"
            };

            if (twentyFive.Contains(upgrade))
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
            else if (ten.Contains(upgrade))
            {
                points += 10;
            }

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>() { "(None)" };

            if (keywords.Contains("SHIELD-CAPTAIN") && keywords.Contains("GUARDIAN"))
            {
                upgrades.Add("Inspirational Exemplar");
                upgrades.Add("Master of the Stances");
                upgrades.Add("Swift as the Eagle");
            }

            if (keywords.Contains("SHIELD-CAPTAIN") && keywords.Contains("ALLARUS"))
            {
                upgrades.Add("Bane of Abominations");
                upgrades.Add("Defiant to the Last");
                upgrades.Add("Unstoppable Destroyer");
            }

            if (keywords.Contains("SHIELD-CAPTAIN") && keywords.Contains("VERTUS"))
            {
                upgrades.Add("Ceaseless Hunter");
                upgrades.Add("Fierce Conqueror");
                upgrades.Add("Tip of the Spear");
            }

            return upgrades;
        }

        public override List<string> GetPsykerPowers()
        {
            return new List<string>() { };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>() { "(None)" };

            if(keywords.Contains("ADEPTUS CUSTODES"))
            {
                if (keywords.Contains("INFANTRY"))
                {
                    relics.Add("Eagle's Eye");
                }

                if (keywords.Contains("BIKER"))
                {
                    relics.Add("Auric Aquilas");
                }

                if ((keywords.Contains("SHIELD-CAPTAIN") && !keywords.Contains("VERTUS")) || 
                    (keywords.Contains("VEXILUS PRAETOR") && !keywords.Contains("ALLARUS"))) {
                    relics.Add("Gatekeeper");
                }

                if (keywords.Contains("SHIELD-CAPTAIN") && keywords.Contains("GUARDIAN"))
                {
                    relics.Add("Veiled Blade");
                }

                if (keywords.Contains("ALLARUS"))
                {
                    relics.Add("Obliteratum");
                }

                if (keywords.Contains("VEXILUS PRAETOR"))
                {
                    relics.Add("Fulminaris Aggressor");
                }

                relics.Add("Castellan's Mark");

                if (keywords.Contains("VEXILUS PRAETOR"))
                {
                    relics.Add("Wrath Angelis");
                }
            }

            if (keywords.Contains("ANATHEMA PSYKANA"))
            {
                relics.Add("Raptor Blade");
                relics.Add("Excruciatus Flamer");
                relics.Add("Enhanced Voidsheen Cloak");
            }

            if (currentSubFaction != string.Empty)
            {
                if (currentSubFaction == "Emperor's Chosen" 
                    && (keywords.Contains("SHIELD-CAPTAIN") && !keywords.Contains("VERTUS")) 
                    || (keywords.Contains("VEXILUS PRAETOR") && !keywords.Contains("ALLARUS")))
                {
                    relics.Add("Paragon Spear");
                }
                else if (currentSubFaction == "Shadowkeepers")
                {
                    relics.Add("Stasis Oubliette");
                }
                else if (currentSubFaction == "Dread Host" 
                    && (keywords.Contains("SHIELD-CAPTAIN") && !keywords.Contains("VERTUS")) 
                    || (keywords.Contains("VEXILUS PRAETOR") && !keywords.Contains("ALLARUS")))
                {
                    relics.Add("Admonimortis");
                }
                else if (currentSubFaction == "Aquilan Shield" 
                    && (keywords.Contains("SHIELD-CAPTAIN") && keywords.Contains("GUARDIAN"))
                    || (keywords.Contains("VEXILUS PRAETOR") && !keywords.Contains("ALLARUS")))
                {
                    relics.Add("Praesidius");
                }
                else if (currentSubFaction == "Solar Watch"
                    && (keywords.Contains("SHIELD-CAPTAIN") && !keywords.Contains("VERTUS"))
                    || (keywords.Contains("VEXILUS PRAETOR") && !keywords.Contains("ALLARUS")))
                {
                    relics.Add("Swiftsilver Talon");
                }
                else if (currentSubFaction == "Emissaries Imperatus")
                {
                    relics.Add("Halo of the Torchbearer");
                }
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "Emperor's Chosen",
                "Shadowkeepers",
                "Dread Host",
                "Aquilan Shield",
                "Solar Watch",
                "Emissaries Imperatus"
            };
        }

        public override List<string> GetWarlordTraits()
        {
            List<string> traits = new List<string>() { "(None)" };

            traits.AddRange(new string[]
            {
                "Master of Martial Strategy",
                "Champion of the Imperium",
                "Superior Creation",
                "Impregnable Mind",
                "Radiant Mantle",
                "Peerless Warrior"
            });

            if (currentSubFaction != string.Empty)
            {
                if (currentSubFaction == "Emperor's Chosen") { traits.Add("Auric Exemplar"); }
                else if (currentSubFaction == "Shadowkeepers") { traits.Add("Lockwarden"); }
                else if (currentSubFaction == "Dread Host") { traits.Add("All-Seeing Annihilator"); }
                else if (currentSubFaction == "Aquilan Shield") { traits.Add("Revered Companion"); }
                else if (currentSubFaction == "Solar Watch") { traits.Add("Sally Forth"); }
                else if (currentSubFaction == "Emissaries Imperatus") { traits.Add("Voice of the Emperor"); }
            }

            return traits;

            //return new List<string>()
            //{
            //"Oblivion Knight",
            //"Silent Judge (Aura)",
            //"Mistress of Persecution (Aura)"
            //};
        }

        public override string ToString()
        {
            return "Adeptus Custodes";
        }
    }
}
