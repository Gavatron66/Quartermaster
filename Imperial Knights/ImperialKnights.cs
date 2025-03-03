using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Imperial_Knights
{
    public class ImperialKnights : Faction
    {
        public ImperialKnights()
        {
            subFactionName = "Noble Household";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Exalted Court";
            customSubFactionTraits = new string[1];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Knight Baron",
                "Stratagem: Heirlooms of the Household",
                "Stratagem: Revered Paragon",
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {   //For Custom Households
            return new List<string>
            {
                "Front-line Fighters",
                "Glorified History",
                "Hunters of Beasts",
                "Noble Combatants",
                "Paragons of Honour",
                "Strike and Shield",
                "Blessed Arms",
                "Fealty to the Cog",
                "Honoured Sacristans",
                "Machine Focus",
                "Steel-Sinewed Aim",
                "Unremitting"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {   //For Freeblades
            return new List<string>
            {
                "Front-line Fighters",
                "Hunters of Beasts",
                "Noble Combatants",
                "Paragons of Honour",
                "Strike and Shield",
                "Blessed Arms",
                "Honoured Sacristans",
                "Machine Focus",
                "Steel-Sinewed Aim",
                "Unremitting",
                "Last of Their Line",
                "Mysterious Guardian",
                "Peerless Warrior",
                "Mythic Hero"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
                //---------- Lord of War ----------
                //new ArmigerHelverin(),
                //new ArmigerWarglaive(),
                //new KnightErrant(),
                //new KnightWarden(),
                //new KnightCrusader(),
                //new KnightGallant(),
                //new KnightPaladin(),
                //new KnightCastellan(),
                //new KnightValiant(),
                //new KnightPreceptor(),
                //new CanisRex(),
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>()
            {
                "(None)",
                "Master of Justice",
                "High Monarch",
                "Monarchsward",
                "Gatekeeper",
                "Herald",
                "Princeps",
                "Forge Master",
                "Master Tactician",
                "Master of Lore",
                "Master of Vox"
            };

            return upgrades;
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
            List<string> relics = new List<string>();

            relics.Add("(None)");
            relics.Add("Sanctuary"); //Questoris/Armiger Class
            relics.Add("Ravager"); //Reaper Chainsword
            relics.Add("Helm of the Nameless Warrior"); //Questor Imperialis Questoris/Armiger Class
            relics.Add("The Helm Dominatus"); //Questoris Class w/ Bondsman ability
            relics.Add("Endless Fury"); //Avenger Gatling Cannon
            relics.Add("The Bastard's Helm"); //Armiger-class
            relics.Add("Mark of the Omnissiah"); //Questor Mechanicus Questoris/Armiger Class
            relics.Add("Banner of Macharius Triumphant"); //Questor Imperialis Questoris Class
            relics.Add("Mentor's Seal"); //Knight Preceptor
            relics.Add("The Heart of Ion"); //Questor Mechanicus Questoris/Armiger Class
            relics.Add("The Paragon Gauntlet"); //Thunderstrike Gauntlet
            relics.Add("Traitor's Pyre"); //Questor Imperialis Knight Valiant
            relics.Add("Cawl's Wrath"); //Questor Mechanicus Knight Castellan
            relics.Add("Judgement"); //Stormspear Rocket Pod

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            throw new NotImplementedException();
        }

        public override void SaveSubFaction(int code, Panel panel)
        {
            throw new NotImplementedException();
        }

        public override void SetPoints(int points)
        {
            throw new NotImplementedException();
        }

        public override void SetSubFactionPanel(Panel panel)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Imperial Knights";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
