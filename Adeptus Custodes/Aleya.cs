﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Custodes
{
    public class Aleya : Datasheets
    {
        public Aleya()
        {
            DEFAULT_POINTS = 75;
            Points = DEFAULT_POINTS;
            TemplateCode = "nc";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ANATHEMA PSYKANA",
                "INFANTRY", "CHARACTER", "KNIGHT-CENTURA", "ALEYA"
            });
            WarlordTrait = "Oblivion Knight";
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new Aleya();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdeptusCustodes;

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            cmbWarlord.Items.Clear();
            cmbWarlord.Items.Add(WarlordTrait);
            cmbWarlord.SelectedIndex = 0;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = false;
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            switch (code)
            {
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                        cmbWarlord.Text = WarlordTrait;
                        cmbWarlord.Enabled = false;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
                    break;
                default: break;
            }

            if (code == -1)
            {
                if (this.isWarlord)
                {
                    cmbWarlord.Text = WarlordTrait;
                    cmbWarlord.Enabled = false;
                }
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Aleya - " + Points + "pts";
        }
    }
}
