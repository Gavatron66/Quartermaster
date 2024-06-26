﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class Castigator : Datasheets
    {
        public Castigator()
        {
            DEFAULT_POINTS = 135;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "1m2k";
            Weapons.Add("Castigator Autocannons");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS", "<ORDER>",
                "VEHICLE", "HALLOWED", "SMOKESCREEN", "CASTIGATOR"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Castigator();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Castigator Autocannons",
                "Castigator Battle Cannon (+5 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Hunter-killer Missile (+5 pts)";
            if (Weapons[1] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Storm Bolter (+5 pts)";
            if (Weapons[2] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption1.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons[0] == "Castigator Battle Cannon (+5 pts)")
            {
                Points += 5;
            }

            if (cbOption1.Checked)
            {
                Points += 5;
            }
            if (cbOption2.Checked)
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Castigator - " + Points + "pts";
        }
    }
}
