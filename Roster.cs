using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder
{
    public class Roster
    {
        public List<Datasheets> roster;
        public Faction currentFaction;
        public int[] StratagemCount;
        public int[] StratagemLimit;
        public List<string> Stratagems;
        int Points = 0;

        bool[] errorsList = new bool[] { false, false, false, false, false };
        //[0] Points Issue
        //[1] Too many Warlords
        //[2] Too many Relics
        //[3] Missing Warlord
        //[4] Missing Relic

        public Roster(int points, Faction faction)
        {
            roster = new List<Datasheets>();
            Points = points;
            currentFaction = faction;
            faction.SetPoints(Points);
            Stratagems = faction.StratagemList;
            StratagemLimit = faction.StratagemLimit;
            StratagemCount = new int[StratagemLimit.Length];
        }

        public void checkForErrors(int currentPoints)
        {
            int Warlord = 0;
            int Relic = 0;

            int WarlordLimit = 1;
            int RelicLimit = 1;

            foreach (Datasheets datasheet in roster)
            {
                if(datasheet.WarlordTrait != string.Empty)
                {
                    Warlord++;
                    if (datasheet.Stratagem.Contains(Stratagems[0]))
                    {
                        WarlordLimit++;
                    }
                }

                if(datasheet.Relic != "(None)")
                {
                    Relic++;
                    if (datasheet.Stratagem.Contains(Stratagems[1]))
                    {
                        RelicLimit++;
                    }
                }
            }

            if(currentPoints > Points)
            {
                errorsList[0] = true;
            }
            else
            {
                errorsList[0] = false;
            }

            if (Warlord > WarlordLimit)
            {
                errorsList[1] = true;
            }
            else
            {
                errorsList[1] = false;
            }

            if(Relic > RelicLimit)
            {
                errorsList[2] = true;
            }
            else
            {
                errorsList[2] = false;
            }

            if (Warlord == 0)
            {
                errorsList[3] = true;
            }
            else
            {
                errorsList[3] = false;
            }

            if (Relic == 0)
            {
                errorsList[4] = true;
            }
            else
            {
                errorsList[4] = false;
            }
        }

        public int GetErrors(int currentPoints)
        {
            int errorCount = 0;
            this.checkForErrors(currentPoints);

            foreach (bool error in errorsList)
            {
                if (error)
                {
                    errorCount++;
                }
            }

            return errorCount;
        }

        public string debug(int currentPoints)
        {
            return "Errors: " + errorsList[0] + ",\n " + errorsList[1] + ",\n " + errorsList[2] +
                ",\n " + errorsList[3] + ",\n " + errorsList[4];
        }

        public string getErrorTooltip()
        {
            string errorsString = string.Empty;

            if (errorsList[0] == true)
            {
                errorsString += "Roster is over points limit.\n";
            }

            if (errorsList[1] == true)
            {
                errorsString += "Roster contains too many Warlords.\n";
            }

            if (errorsList[2] == true)
            {
                errorsString += "Roster contains too many relics.\n";
            }

            if (errorsList[3] == true)
            {
                errorsString += "Roster does not contain a Warlord.\n";
            }

            if (errorsList[4] == true)
            {
                errorsString += "Roster does not contain any relics.\n";
            }

            return errorsString;
        }

        public void StratagemCheck()
        {
            StratagemCount = new int[StratagemLimit.Length];

            foreach(Datasheets unit in roster)
            {
                if(unit.Stratagem.Count != 0)
                {
                    foreach (string strat in unit.Stratagem)
                    {
                        StratagemCount[Stratagems.IndexOf(strat)] += 1;
                    }
                }
            }

            currentFaction.StratagemCount = this.StratagemCount;
        }
    }
}
