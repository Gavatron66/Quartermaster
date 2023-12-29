using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Aeldari
{
    public class Aeldari : Faction
    {
        public Aeldari()
        {
            subFactionName = "<Craftworld>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Exarch Powers";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Champion of the Aeldari",
                "Stratagem: Treasures of the Aeldari"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>
            {
                "Children of Khaine",
                "Children of Morai-Heg",
                "Children of Prophecy",
                "Children of the Open Skies",
                "Diviners of Fate",
                "Elite Citizenry",
                "Expert Crafters",
                "Grim",
                "Hail of Doom",
                "Headstrong",
                "Hunters of Ancient Relics",
                "Masterful Shots",
                "Masters of Concealment",
                "Mobile Fighters",
                "Savage Blades",
                "Swift Strikes",
                "Students of Vaul",
                "Superior Shurikens",
                "Vengeful",
                "Warding Runes",
                "Webway Warriors",
                "Wrath of the Dead"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>
            {
                "Children of Khaine",
                "Children of Morai-Heg",
                "Children of Prophecy",
                "Children of the Open Skies",
                "Diviners of Fate",
                "Elite Citizenry",
                "Expert Crafters",
                "Grim",
                "Hail of Doom",
                "Headstrong",
                "Hunters of Ancient Relics",
                "Masterful Shots",
                "Masters of Concealment",
                "Mobile Fighters",
                "Savage Blades",
                "Swift Strikes",
                "Students of Vaul",
                "Superior Shurikens",
                "Vengeful",
                "Warding Runes",
                "Webway Warriors",
                "Wrath of the Dead"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>
            {
                /*
                //---------- HQ ----------
                new AvatarofKhaine(),
                new FarseerSkyrunner(),
                new EldradUlthran(),
                new Farseer(),
                new Autarch(),
                new PrinceYriel(),
                new AutarchSkyrunner(),
                new Asurmen(),
                new Baharroth(),
                new Fuegan(),
                new JainZar(),
                new Karandras(),
                new MauganRa(),
                new IllicNightspear(),
                new Spiritseer(),
                //---------- Troops ----------
                new GuardianDefenders(),
                new Rangers(),
                new StormGuardians(),
                new CorsairVoidreavers(),
                //---------- Elites ----------
                new CorsairVoidscarred(),
                new Warlocks(),
                new WarlockSkyrunners(),
                new DireAvengers(),
                new FireDragons(),
                new HowlingBanshees(),
                new StrikingScorpions(),
                new Wraithblades(),
                new Wraithguard(),
                new Wraithlord(),
                //---------- Fast Attack ----------
                new Windriders(),
                new Vypers(),
                new SwoopingHawks(),
                new WarpSpiders(),
                new ShiningSpears(),
                new ShroudRunners(),
                //---------- Heavy Support ----------
                new WarWalkers(),
                new DarkReapers(),
                new SupportWeapons(),
                new Falcon(),
                new NightSpinner(),
                new FirePrism(),
                //---------- Transport ----------
                new WaveSerpent(),
                //---------- Flyers ----------
                new CrimsonHunter(),
                new HemlockWraithfighter(),
                //---------- Lords of War ----------
                new Wraithknight(),
                //---------- Fortification ----------
                new WebwayGate()
                */
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            throw new NotImplementedException();
        }

        public override bool GetIfEnabled(int index)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetSubFactions()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            throw new NotImplementedException();
        }

        public override void SetPoints(int points)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Aeldari";
        }
    }
}
