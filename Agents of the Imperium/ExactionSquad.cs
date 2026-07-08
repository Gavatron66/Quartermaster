using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class ExactionSquad : Datasheets
    {
        bool cyberMastif = false;
        bool chiurgant = false;
        bool revelatum = false;
        bool castigator = false;
        bool nuncio = false;
        List<int> restrictedIndexes2 = new List<int>();

        public ExactionSquad()
        {
            DEFAULT_POINTS = 13;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "exaction";
            Weapons.Add("Arbites Combat Shotgun");
            Weapons.Add("Arbites Combat Shotgun");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ARBITES", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "CORE", "EXACTION SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new ExactionSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ImperialAgents;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            CheckBox cbOption5 = panel.Controls["cbOption5"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10 - (chiurgant ? 1 : 0) - (revelatum ? 1 : 0) - (castigator ? 1 : 0);
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Arbites Combat Shotgun",
                "Arbites Grenade Launcher",
                "Executioner Shotgun (+5 pts)",
                "Heavy Stubber",
                "Webber"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Arbites Combat Shotgun",
                "Arbites Grenade Launcher",
                "Executioner Shotgun (+5 pts)",
                "Heavy Stubber",
                "Webber"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cbOption1.Text = "Cyber-mastiff (+10 pts)";
            cbOption1.Checked = cyberMastif;

            cbOption2.Text = "Chiurgent (+13 pts)";
            cbOption2.Checked = chiurgant;

            cbOption3.Text = "Revelatum (+13 pts)";
            cbOption3.Checked = revelatum;

            cbOption4.Text = "Castigator (+23 pts)";
            cbOption4.Checked = castigator;

            cbOption5.Text = "Nuncio Aquila (Proctor-Exactant, +10 pts)";
            cbOption5.Checked = nuncio;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            CheckBox cbOption5 = panel.Controls["cbOption5"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                    }

                    break;
                case 12:
                    if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                    }
                    break;
                case 21:
                    cyberMastif = cbOption1.Checked;
                    break;
                case 22:
                    chiurgant = cbOption2.Checked;

                    if(UnitSize == nudUnitSize.Maximum)
                    {
                        nudUnitSize.Value--;
                    }

                    nudUnitSize.Maximum = 10 - (cbOption2.Checked ? 1 : 0) - (cbOption3.Checked ? 1 : 0) - (cbOption4.Checked ? 1 : 0);
                    break;
                case 23:
                    revelatum = cbOption3.Checked;

                    if (UnitSize == nudUnitSize.Maximum)
                    {
                        nudUnitSize.Value--;
                    }

                    nudUnitSize.Maximum = 10 - (cbOption2.Checked ? 1 : 0) - (cbOption3.Checked ? 1 : 0) - (cbOption4.Checked ? 1 : 0);
                    break;
                case 24:
                    castigator = cbOption4.Checked;

                    if (UnitSize == nudUnitSize.Maximum)
                    {
                        nudUnitSize.Value--;
                    }

                    nudUnitSize.Maximum = 10 - (cbOption2.Checked ? 1 : 0) - (cbOption3.Checked ? 1 : 0) - (cbOption4.Checked ? 1 : 0);
                    break;
                case 26:
                    nuncio = cbOption5.Checked;
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            restrictedIndexes.Clear();
            restrictedIndexes2.Clear();

            if (Weapons[0] != "Arbites Combat Shotgun")
            {
                restrictedIndexes2.Add(cmbOption2.Items.IndexOf(Weapons[0]));
            }

            if (Weapons[1] != "Arbites Combat Shotgun")
            {
                restrictedIndexes.Add(cmbOption1.Items.IndexOf(Weapons[1]));
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
            this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] == "Executioner Shotgun (+5 pts)")
            {
                Points += 5;
            }
            if (Weapons[1] == "Executioner Shotgun (+5 pts)")
            {
                Points += 5;
            }

            Points += (nuncio ? 10 : 0) + (cyberMastif ? 10 : 0) + (chiurgant ? 13 : 0) + (revelatum ? 13 : 0) + (castigator ? 23 : 0);
        }

        public override string ToString()
        {
            return "Exaction Squad - " + Points + "pts";
        }
    }
}
