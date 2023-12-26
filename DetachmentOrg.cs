using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder
{
    public class DetachmentOrg
    {
        Detachment detachment {  get; set; }

        public DetachmentOrg(Detachment detachment)
        {
            this.detachment = detachment;
        }

        public Detachment DetachmentSetUp(string detach)
        {
            switch (detach)
            {
                case "Arks of Omen Detachment":
                    break;
            }

            return detachment;
        }
    }
}
