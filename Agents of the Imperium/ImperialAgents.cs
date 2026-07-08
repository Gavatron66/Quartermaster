using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class ImperialAgents : Faction
    {
        public ImperialAgents()
        {
            subFactionName = "Agents of the Imperium";
            factionUpgradeName = "Inquisitorial Ordo";
            StratagemList = new List<string>()
            {
                "Shouldn't see this 1",
                "Shouldn't see this 2"
            };
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
                //---------- Inquisition ----------
                new Inquisitor(),
                new InquisitorCoteaz(),
                new InquisitorEisenhorn(),
                new InquisitorGreyfax(),
                new InquisitorKaramazov(),
                new KyriaDraxus(),
                new Acolytes(),
                new Daemonhost(),
                new Jokaero(),
                //---------- Assassins ----------
                new Callidus(),
                new Culexus(),
                new Eversor(),
                new Vindicare(),
                //---------- Arks of Omen ----------
                new RogueTrader(),
                new NavyBreachers(),
                new Voidsmen(),
                new VigilantSquad(),
                new SubductorSquad(),
                new ExactionSquad(),
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            return (new List<string>()
            {
                "Ordo Hereticus",
                "Ordo Malleus",
                "Ordo Xenos",
                "Ordo Minoris"
            });
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string>
            {
                "Terrify",
                "Psychic Fortitude",
                "Dominate",
                "Mental Interrogation",
                "Psychic Pursuit",
                "Castigation"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            return new List<string>();
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>();
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            return new List<string>() { "Inspiring Leader" };
        }

        public override void SaveSubFaction(int code, Panel panel)
        {

        }

        public override void SetPoints(int points)
        {

        }

        public override void SetSubFactionPanel(Panel panel)
        {
            Template template = new Template();
            template.LoadFactionTemplate(-1, panel);
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }

        public override string ToString()
        {
            return "Agents of the Imperium";
        }
    }
}
