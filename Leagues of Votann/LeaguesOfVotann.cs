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
            return new List<Datasheets>()
            {
                //---------- HQ ----------
                //new ÛtharTheDestined(),
                //new Kâhl(),
                //new EinhyrChampion(),
                //new Grimnyr(),
                //new BrôkhyrIronMaster(),
                //---------- Troops ----------
                //new HearthkynWarriors(),
                //---------- Elites ----------
                //new EinhyrHearthguard(),
                //new CthonianBeserks(),
                //---------- Fast Attack ----------
                //new HernkynPioneers(),
                //new Sagitaur(),
                //---------- Heavy Support ----------
                //new BrôkhyrThunderkyn(),
                //new HekatonLandFortress()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == "High Kâhl (+40 pts)")
            {
                return 40;
            }
            else if (upgrade != "High Kâhl (+40 pts)")
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
                "High Kâhl (+40 pts)",
                "Lord Grimnyr (+25 pts)",
                "Brôkhyr Forge-master (+25 pts)"
            };
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string>()
            {
                "Interface Echo",
                "Fortify",
                "Ancestral Wrath",
                "Grudgepyre",
                "Null Vortex",
                "Crushing Contempt"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();
            
            relics.Add("(None)");
            relics.Add("Aktôl's Fortress");
            relics.Add("Ancestral Crest");
            relics.Add("Exactor");
            relics.Add("The First Knife");
            relics.Add("Flâyre");
            relics.Add("Wayfarer's Grace");
            relics.Add("The Grey Crest");
            relics.Add("Grudge's End");
            relics.Add("Wârpestryk");
            relics.Add("The Hearthfist");
            relics.Add("The Murmuring Stave");
            relics.Add("Thyrikite Plate");
            relics.Add("Vôlumm's Master Artifice");
            relics.Add("Ymmâ's Shield");

            if (currentSubFaction == "Greater Thurian Legaue") { relics.Add("Kôrvyk's Cuirass"); }
            else if (currentSubFaction == "Trans-Hyperian Alliance") { relics.Add("The CORV Duas"); }
            else if (currentSubFaction == "Kronus Hegemony") { relics.Add("The Just Blade"); }
            else if (currentSubFaction == "Ymyr Conglomerate") { relics.Add("The Last Crest of Jâluk"); }
            else if (currentSubFaction == "Urani-Surtr Regulates") { relics.Add("The Abiding Mantle"); }

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
            List<string> traits = new List<string>();

            traits.AddRange(new string[]
            {
                "Ancestral Bearing",
                "Warrior Lord",
                "A Long List",
                "Guild Affiliate",
                "Unrelenting Toil",
                "Grim Demeanour"
            });

            if (currentSubFaction == "Greater Thurian League") { traits.Add("Pragmatic Wisdom"); }
            else if (currentSubFaction == "Trans-Hyperian Alliance") { traits.Add("Nomad Strategist"); }
            else if (currentSubFaction == "Kronus Hegemony") { traits.Add("Exemplary Hero"); }
            else if (currentSubFaction == "Ymyr Conglomerate") { traits.Add("Guild Connections"); }
            else if (currentSubFaction == "Urani-Surtr Regulates") { traits.Add("Grim Pragmatism"); }

            return traits;
        }

        public override void SaveSubFaction(int code, Panel panel)
        {

        }

        public override void SetPoints(int points)
        {

        }

        public override void SetSubFactionPanel(Panel panel)
        {

        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
