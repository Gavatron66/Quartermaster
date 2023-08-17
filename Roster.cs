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

        int Warlord = 0;
        int Relic = 0;
        int Points = 0;

        bool[] errorsList = new bool[] { false, false, false, false, false };
        //[0] Points Issue
        //[1] Too many Warlords
        //[2] Too many Relics
        //[3] Missing Warlord
        //[4] Missing Relic

        public Roster(int points)
        {
            roster = new List<Datasheets>();
            Points = points;
        }

        public void checkForErrors(int currentPoints)
        {
            Warlord = 0;
            Relic = 0;

            foreach (Datasheets datasheet in roster)
            {
                if(datasheet.WarlordTrait != string.Empty)
                {
                    Warlord++;
                }

                if(datasheet.Relic != "(None)")
                {
                    Relic++;
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

            if (Warlord > 1)
            {
                errorsList[1] = true;
            }
            else
            {
                errorsList[1] = false;
            }

            if(Relic > 1)
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
    }
}
