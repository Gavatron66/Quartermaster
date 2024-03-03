using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Grey_Knights
{
    public class GreyKnights : Faction
    {
        public GreyKnights()
        {
            subFactionName = "<Brotherhood>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Wisdom of the Prognosticars";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Shield of Humanity",
                "Stratagem: Armoury of Titan",
                "Stratagem: Charge of the Ancients",
                "Stratagem: Exemplar of the Silvered Host",
                "Stratagem: Endownment in Extremis"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
        }

        public override List<string> GetCustomSubfactionList2()
        {
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>
            {
                //---------- HQ ----------
                //new KaldorDraigo(),
                //new GMVoldus(),
                //new GrandMaster(),
                //new GMDreadknight(),
                //new CastellanCrowe(),
                //new BrotherCaptainStern(),
                //new BrotherCaptain(),
                //new BrotherhoodChampion(),
                //new BrotherhoodLibrarian(),
                //new BrotherhoodTechmarine(),
                //new BrotherhoodChaplain(),
                //---------- Troops ----------
                //new BrotherhoodTerminators(),
                //new StrikeSquad(),
                //---------- Elites ----------
                //new BrotherhoodApothecary(),
                //new BrotherhoodAncient(),
                //new Paladins(),
                //new Purifiers(),
                //new PaladinAncient(),
                //new GKServitors(),
                //new GKVenerableDreadnought(),
                //new GKDreadnought(),
                //---------- Fast Attack ----------
                //new Interceptors(),
                //---------- Heavy Support ----------
                //new PurgationSquad(),
                //new NemesisDreadknight(),
                //new GKLandRaider(),
                //new GKLandRaiderCrusader(),
                //new GKLandRaiderRedeemer(),
                //---------- Transport ----------
                //new GKRazorback(),
                //new GKRhino(),
                //---------- Flyer ----------
                //new GKStormhawkInterceptor(),
                //new GKStormtalonGunship(),
                //new GKStormravenGunship()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>()
            {
                "(None)"
            };

            upgrades.AddRange(new string[]
            {
                "Augury of Aggression",
                "Heroism's Favour",
                "A Noble Death",
                "Omen of Incursion",
                "Presaged Paralysis",
                "Foretelling of Locus",
                "True Name Shard",
                "Temporal Bombs",
                "Servant of the Throne",
                "Deluminator of Majesty",
                "Gem of Inoktu",
                "Severance Bolt"
            });

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            if (keywords == "Dominus")
            {
                return new List<string>()
                {
                    "Gate of Infinity",
                    "Empyric Amplification",
                    "Sanctuary",
                    "Vortex of Doom",
                    "Warp Shaping",
                    "Ghostly Bonds"
                };
            }
            else if (keywords == "Sanctic")
            {
                return new List<string>()
                {
                    "Astral Aim",
                    "Purge Soul",
                    "Hammerhand",
                    "Purifying Flame",
                    "Armoured Resilience",
                    "Ethereal Castigation"
                };
            }
            else if (keywords == "Litany")
            {
                return new List<string>()
                {
                    "Words of Power",
                    "Intonement for Guidance",
                    "Psalm of Purity",
                    "Refrain of Convergence",
                    "Recitation of Projection",
                    "Invocation of Focus"
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

            relics.AddRange(new string[]
            {
                "Soul Glaive",
                "Destroyer of Crys'yllix",
                "Fury of Deimos",
                "Banner of Refining Flame",
                "Domina Liber Daemonica",
                "Cuirass of Sacrifice",
                "Sanctic Shard",
                "Gyrotemporal Vault",
                "Blade of the Forsworn",
                "Sigil of Exigence",
                "Augurium Scrolls",
                "Stave of Supremacy",
                "Kantu Vambrace",
                "Artisan Nullifier Matrix",
                "Aetheric Conduit"
            });

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "Swordbearers",
                "Blades of Victory",
                "Wardmakers",
                "Prescient Brethren",
                "Preservers",
                "Rapiers",
                "Exactors",
                "Silver Blades"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            throw new NotImplementedException();
        }

        public override void SetPoints(int points)
        {
        }

        public override string ToString()
        {
            return "Grey Knights";
        }
    }
}
