using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class VigilantSquad : Datasheets
    {
        bool nuncio = false;
        bool cyberMastif = false;
        List<int> restrictedIndexes2 = new List<int>();

        public VigilantSquad()
        {
            DEFAULT_POINTS = 110;
            Points = DEFAULT_POINTS;
            UnitSize = 10;
            TemplateCode = "2m2k";
            Weapons.Add("Arbites Combat Shotgun");
            Weapons.Add("Arbites Combat Shotgun");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ARBITES", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "CORE", "VIGILANT SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new VigilantSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ImperialAgents;

            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

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

            cbOption1.Text = "Nuncio Aquila (Proctor-Vigilant, +10 pts)";
            cbOption1.Checked = nuncio;

            cbOption2.Text = "Cyber-mastiff (+10 pts)";
            cbOption2.Checked = cyberMastif;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmboption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

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
                    nuncio = cbOption1.Checked;
                    break;
                case 22:
                    cyberMastif = cbOption2.Checked;
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

            Points = DEFAULT_POINTS;

            if (Weapons[0] == "Executioner Shotgun (+5 pts)")
            {
                Points += 5;
            }
            if (Weapons[1] == "Executioner Shotgun (+5 pts)")
            {
                Points += 5;
            }

            Points += (nuncio ? 10 : 0) + (cyberMastif ? 10 : 0);
        }

        public override string ToString()
        {
            return "Vigilant Squad - " + Points + "pts";
        }
    }
}
