﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class Obelisk : Datasheets
    {
        public Obelisk()
        {
            DEFAULT_POINTS = 270;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "VEHICLE", "TITANIC", "FLY", "OBELISK"
            });
            Role = "Lord of War";
        }
        public override Datasheets CreateUnit()
        {
            return new Obelisk();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
        }

        public override string ToString()
        {
            return "Obelisk - " + Points + "pts";
        }
    }
}
