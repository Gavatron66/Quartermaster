using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class LeaguesOfVotann : Faction
    {
        public LeaguesOfVotann()
        {
            subFactionName = "<League>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "The Votannic Council";
            customSubFactionTraits = new string[3];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Legend of the League",  //Warlord Trait
                "Stratagem: In The Right Hands",    //Relic
                "Stratagem: Bequest of the Votann", //Extra relic for Theyn/Hesyr
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>()
            {
                "Vengeful",
                "Brutal Efficiency",
                "Close Quarters Prioritisation",
                "Taking it Personally",
                "Quick to Judge",
                "Unwavering Discipline"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>()
            {
                "Martial Cloneskeins",
                "Stoic",
                "Honour in Toil",
                "War Songs",
                "Refined Power Cores",
                "Superior Beam Capacitors",
                "Void Hardened",
                "Warrior Pride",
                "Weaponsmiths"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            throw new NotImplementedException();
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == "High Kahl (+40 pts)")
            {
                return 40;
            }
            else if (upgrade != "High Kahl (+40 pts)")
            {
                return 20;
            }
            else
            {
                return 0;
            }
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            return new List<string>()
            {
                "(None)",
                "High Kahl (+40 pts)",
                "Lord Grimnyr (+25 pts)",
                "Brokhyr Forge-master (+25 pts)"
            };
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
            relics.Add("Aktol's Fortress");
            relics.Add("Ancestral Crest");
            relics.Add("Exactor");
            relics.Add("The First Knife");
            relics.Add("Flayre");
            relics.Add("Wayfarer's Grace");
            relics.Add("The Grey Crest");
            relics.Add("Grudge's End");
            relics.Add("Warpestryk");
            relics.Add("The Hearthfist");
            relics.Add("The Murmuring Stave");
            relics.Add("Thyrikite Plate");
            relics.Add("Volumm's Master Artifice");
            relics.Add("Ymma's Shield");

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Greater Thurian League",
                "Trans-Hyperian Alliance",
                "Kronus Hegemony",
                "Ymyr Conglomerate",
                "Urani-Surtr Regulates",
                "<Custom>"
            };
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

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {
            throw new NotImplementedException();
        }
    }
}
