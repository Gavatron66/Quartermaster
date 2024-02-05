using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Roster_Builder
{
    public class Roster
    {
        public List<Detachment> Detachments { get; set; }
        public int Points {  get; set; }
        public int CommandPoints { get; set; }
        public List<string> errorList { get; set; }

        public Roster()
        {
            Detachments = new List<Detachment>();
        }

        public void CreateNewDetachment(string detachName, Faction faction, string name)
        {
            int[] forceOrgSlots;
            int[] requiredSlots;
            //[0] HQ,
            //[1] Troops,
            //[2] Elites,
            //[3] Fast Attack,
            //[4] Heavy Support,
            //[5] Lords of War,
            //[6] Flyers
            //[7] Fortifications
            //[8] Transports

            switch (detachName)
            {
                case "Arks of Omen Detachment":
                    forceOrgSlots = new int[9] { 4, 12, 9, 6, 6, 6, 2, 3, 12 };
                    requiredSlots = new int[9] { 1, 3, 3, 3, 3, 3, 0, 0, 0 };
                    break;
                default:
                    //Code should never reach here
                    forceOrgSlots = new int[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                    requiredSlots = new int[9] { 1, 3, 3, 3, 3, 3, 0, 0, 0 };
                    break;
            }

            Detachment temp = new Detachment();
            //temp.ForceOrgSlots = forceOrgSlots;
            //temp.ForceOrgRequired = requiredSlots;
            temp.rosterAbove = this;
            temp.currentFaction = faction;
            temp.DetachmentName = detachName;

            if (name == "<Optional>")
            {
                temp.name = faction.ToString() + " Detachment";
            }
            else
            {
                temp.name = name;
            }

            Detachments.Add(temp);
        }
    }
}
