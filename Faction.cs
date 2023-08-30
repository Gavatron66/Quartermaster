using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder
{
    public abstract class Faction
    {
        public string subFactionName { get; set; }
        public string currentSubFaction { get; set; }
        public string factionUpgradeName { get; set; }
        public List<string> StratagemList { get; set; }
        public int[] StratagemCount { get; set; }
        public int[] StratagemLimit { get; set; }

        public Faction() { StratagemList = new List<string>(); }

        public abstract List<string> GetPsykerPowers();
        public abstract List<string> GetFactionUpgrades(List<string> keywords);
        public abstract List<string> GetSubFactions();
        public abstract List<Datasheets> GetDatasheets();
        public abstract List<string> GetWarlordTraits();
        public abstract int GetFactionUpgradePoints(string upgrade);
        public abstract List<string> GetRelics(List<string> keywords);
        public abstract List<string> GetCustomSubfactionList1();
        public abstract List<string> GetCustomSubfactionList2();
        public abstract void SetPoints(int points);
        public abstract bool GetIfEnabled(int index);
    }
}
