using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$faction")]
    #region JSON Factions
    #endregion
    public abstract class Faction
    {
        public string subFactionName { get; set; }
        public string currentSubFaction { get; set; }
        public string factionUpgradeName { get; set; }
        public List<string> StratagemList { get; set; }
        public int[] StratagemCount { get; set; }
        public int[] StratagemLimit { get; set; }
        public string[] customSubFactionTraits { get; set; }

        public Faction() { StratagemList = new List<string>(); }

        public abstract List<string> GetPsykerPowers(string keywords);
        public abstract List<string> GetFactionUpgrades(List<string> keywords);
        public abstract List<string> GetSubFactions();
        public abstract List<Datasheets> GetDatasheets();
        public abstract List<string> GetWarlordTraits(string keyword);
        public abstract int GetFactionUpgradePoints(string upgrade);
        public abstract List<string> GetRelics(List<string> keywords);
        public abstract List<string> GetCustomSubfactionList1();
        public abstract List<string> GetCustomSubfactionList2();
        public abstract void SetPoints(int points);
        public abstract bool GetIfEnabled(int index);
        public abstract void SetSubFactionPanel(Panel panel);
    }
}
