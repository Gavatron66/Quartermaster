﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Imperial_Knights
{
    public class CanisRex : Datasheets
    {
        public CanisRex()
        {
            DEFAULT_POINTS = 440;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "ncp";
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "IMPERIAL KNIGHTS", "QUESTOR IMPERIALIS", "AGENT OF THE IMPERIUM", "FREEBLADE",
                "VEHICLE", "TITANIC", "CHARACTER", "QUESTORIS-CLASS", "MENTOR", "KNIGHT PRECEPTOR", "CANIS REX",
                "INFANTRY", "CHARACTER", "SIR HEKHTUR"
            });
            PsykerPowers = new string[2] { string.Empty, string.Empty };
            WarlordTrait = "Revered Knight";
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new CanisRex();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ImperialKnights;
            Template.LoadTemplate(TemplateCode, panel);

            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            lblPsyker.Text = "Select two of the following:";
            clbPsyker.ClearSelected();
            for (int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
            }
            if (PsykerPowers[1] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[1]), true);
            }

            cmbWarlord.Enabled = false;
            cmbWarlord.Items.Clear();
            cmbWarlord.Items.Add(WarlordTrait);
            cmbWarlord.SelectedIndex = 0;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
            }
            else
            {
                cbWarlord.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
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
                    else { this.isWarlord = false; }
                    break;
                default: break;
                case 60:
                    if (clbPsyker.CheckedItems.Count < 3)
                    {
                        break;
                    }
                    else if (clbPsyker.CheckedItems.Count == 3)
                    {
                        PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                    }
                    else
                    {
                        clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                    }

                    break;
            }
        }

        public override string ToString()
        {
            return "Canis Rex - " + Points + "pts";
        }
    }
}
