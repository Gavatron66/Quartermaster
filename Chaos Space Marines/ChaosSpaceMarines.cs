using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class ChaosSpaceMarines : Faction
    {
        public ChaosSpaceMarines()
        {

        }

        public override List<string> GetCustomSubfactionList1()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetCustomSubfactionList2()
        {
            throw new NotImplementedException();
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>()
            {
                //---------- HQ ----------
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

        public override string ToString()
        {
            return "Chaos Space Marines";
        }
    }
}
