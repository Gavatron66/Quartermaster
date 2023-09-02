﻿using Roster_Builder.Necrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Space_Marines
{
    public class SpaceMarines : Faction
    {
        public SpaceMarines()
        {
            subFactionName = "<Chapter>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Masters of the Chapter";
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
                "Inheritors of the Primarch",
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
            return new List<Datasheets>()
            {
                //---------- HQ ----------
                //new PrimarisCpatain(),
                //new GravisHeavyCaptain(),
                //new PhobosCaptain(),
                //new GravisCaptain(),
                //new TerminatorCaptain(),
                //mew Captain(),
                //new BikeCaptain(),
                //new PrimarisLieutenant(),
                //new ReiverLieutenant(),
                //new Lieutenant(),
                //new PhobosLieutenant(),
                //new PrimarisLibrarian(),
                //new Librarian(),
                //new PhobosLibrarian(),
                //new TerminatorLibrarian(),
                //new PrimarisChaplain(),
                //new PrimarisBikeChaplain(),
                //new TerminatorChaplain(),
                //new Chaplain(),
                //new PrimarisTechmarine(),
                //new Techmarine()
                //---------- Troops ----------
                //new Intercessors(),
                //new AssaultIntercessors(),
                //new HeavyIntercessors(),
                //new Infiltrators(),
                //new Incursors(),
                //new TacticalSquad(),
                //---------- Elites ----------
                //new Servitors(),
                //new CompanyChampion(),
                //new ScoutSquad(),
                //new PrimarisApothecary(),
                //new Apothecary(),
                //new CompanyAncient(),
                //new PrimarisAncient(),
                //new BladeguardAncient(),
                //new TerminatorAncient(),
                //new VeteranIntercessors(),
                //new BladeguardVeterans(),
                //new Veterans(),
                //new VanguardVeterans(),
                //new SternguardVeterans(),
                //new Judicar(),
                //new Reivers(),
                //new Aggressors(),
                //new AssaultTerminators(),
                //new Terminators(),
                //new RelicTerminators(),
                //new CenturionAssault(),
                //new Invictor(),
                //new Dreadnought(),
                //new ContemptorDreadnought(),
                //new VenerableDreadnought(),
                //new IroncladDreadnought(),
                //new RedemptorDreadnought(),
                //---------- Fast Attack ----------
                //new AssaultSquad(),
                //new Outriders(),
                //new InvaderATVs(),
                //new BikeSquad(),
                //new ScoutBikeSquad(),
                //new AttackBikeSquad(),
                //new Suppressors(),
                //new Inceptors(),
                //new StormSpeederHailstrike(),
                //new StormSpeederThunderstrike(),
                //new StormSpeederHammerstrike(),
                //new LandSpeeders(),
                //new LandSpeederTornadoes(),
                //new LandSpeederTyphoons(),
                //---------- Heavy Support ----------
                //new Hellblasters(),
                //new Eliminators(),
                //new CenturionDevastators(),
                //new Eradicators(),
                //new Devastators(),
                //new Thunderfire(),
                //new FirestrikeTurret(),
                //new Hunter(),
                //new Stalker(),
                //new Whirlwind(),
                //new PredatorDestructor(),
                //new PredatorAnnihilator(),
                //new GladiatorLancer(),
                //new GladiatorReaper(),
                //new GladiatorValiant(),
                //new Vindicator(),
                //new LandRaider(),
                //new LandRaiderCrusader(),
                //new LandRaiderRedeemer(),
                //new Repulsor(),
                //new RepulsorExecutioner(),
                //---------- Transport ----------
                //new Rhino(),
                //new Razorback(),
                //new Impulsor(),
                //new DropPod(),
                //new LandSpeederStorm(),
                //---------- Flyers ----------
                //new StormhawkInterceptor(),
                //new StormtalonGunship(),
                //new StormravenGunship(),
                //---------- Lords of War ----------
                //---------- Fortification ----------
                //new Hammerfall Bunker()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] forty = new string[]
            {
                "Chapter Master"
            };

            string[] twentyfive = new string[]
            {
                "Master of Sanctity",
                "Chief Librarian"
            };

            string[] twenty = new string[]
            {
                "Master of the Forge",
                "Chapter Ancient"
            };

            string[] fifteen = new string[]
            {
                "Chief Apothecary",
                "Chapter Champion"
            };

            if (forty.Contains(upgrade))
            {
                points = 40;
            }

            if (twenty.Contains(upgrade))
            {
                points = 20;
            }

            if (twentyfive.Contains(upgrade))
            {
                points = 25;
            }

            if (fifteen.Contains(upgrade))
            {
                points = 15;
            }

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>() { "(None)" };

            if (keywords.Contains("CAPTAIN"))
            {
                upgrades.Add("Chapter Master");
            }

            if (keywords.Contains("CHAPLAIN"))
            {
                upgrades.Add("Master of Sanctity");
            }

            if (keywords.Contains("TECHMARINE"))
            {
                upgrades.Add("Master of the Forge");
            }

            if (keywords.Contains("LIBRARIAN"))
            {
                upgrades.Add("Chief Librarian");
            }

            if (keywords.Contains("APOTHECARY"))
            {
                upgrades.Add("Chief Apothecary");
            }

            if (keywords.Contains("ANCIENT"))
            {
                upgrades.Add("Chapter Ancient");
            }

            if (keywords.Contains("COMPANY CHAMPION"))
            {
                upgrades.Add("Chapter Champion");
            }

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetPsykerPowers()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            //The Armour Indomitus
            //The Shield Eternal - Model w/ Storm/Relic/Combat Shield
            //Standard of the Emperor Ascendant - ANCIENT only
            //Teeth of Terra - Astartes Chainsword
            //Primarch's Wrath - Boltgun, Master-crafted Boltgun or Special Issue Bolt Carbine
            //The Burning Blade - Power Sword or Master-crafted Power Sword
            //Purgatorus - Bolt Pistol, Heavy Bolt Pistol, Master-crafted Special Issue Bolt Pistol or Absolver Pistol
            //Reliquary of Gathalamor - PRIMARIS only
            //Bellicos Bolt Rifle - Master-crafted Auto Bolt Rifle
            //Lament - Master-crafted Stalker Bolt Rifle
            //Ghostweave Cloak - PHOBOS with Camo Claok
            //Tome of Malcador - LIBRARIAN
            //Benediction of Fury - CHAPLAIN
            //The Honour Vehement
            //The Vox Espiritum - PRIMARIS

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
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

        public override List<string> GetWarlordTraits()
        {
            List<string> traits = new List<string>()
            {

            };

            if (currentSubFaction == "Dark Angels") { traits.Add("Brilliant Strategist"); }
            else if (currentSubFaction == "White Scars") { traits.Add("Deadly Hunter"); }
            else if (currentSubFaction == "Space Wolves") { traits.Add("Beastslayer"); }
            else if (currentSubFaction == "Imperial Fists") { traits.Add("Architect of War"); }
            else if (currentSubFaction == "Crimson Fists") { traits.Add("Refuse to Die"); }
            else if (currentSubFaction == "Black Templars") { traits.Add("Oathkeeper"); }
            else if (currentSubFaction == "Blood Angels") { traits.Add("Speed of the Primarch"); }
            else if (currentSubFaction == "Flesh Tearers") { traits.Add("Merciless Butcher"); }
            else if (currentSubFaction == "Iron Hands") { traits.Add("Adept of the Omnissiah"); }
            else if (currentSubFaction == "Ultramarines") { traits.Add("Adept of the Codex"); }
            else if (currentSubFaction == "Salamanders") { traits.Add("Anvil of Strength"); }
            else if (currentSubFaction == "Raven Guard") { traits.Add("Echo of the Ravenspire"); }
            else if (currentSubFaction == "Deathwatch") { traits.Add("Vigilance Incarnate"); }

            return traits;
        }

        public override void SetPoints(int points)
        {
            throw new NotImplementedException();
        }
    }
}