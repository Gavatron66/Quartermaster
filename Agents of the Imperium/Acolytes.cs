using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Agents_of_the_Imperium
{
    public class Acolytes : Datasheets
    {
        int currentIndex;

        public Acolytes()
        {
            DEFAULT_POINTS = 10;
            UnitSize = 1;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Laspistol");
                Weapons.Add("Chainsword");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "INQUISITION", "<ORDO>", "AGENTS OF THE IMPERIUM",
                "INFANTRY", "ACOLYTES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Acolytes();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ImperialAgents;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Acolyte w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bolt Pistol",
                "Laspistol",
                "Needle Pistol",
                "Plasma Pistol (+5 pts)"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Boltgun",
                "Chainsword",
                "Combi-flamer (+10 pts)",
                "Combi-melta (+10 pts)",
                "Combi-plasma (+10 pts)",
                "Flamer (+5 pts)",
                "Hot-shot Lasgun",
                "Meltagun (+10 pts)",
                "Plasma Gun (+10 pts)",
                "Power Fist (+10 pts)",
                "Power Maul (+5 pts)",
                "Power Sword (+5 pts)",
                "Storm Bolter (+5 pts)",
                "Thunder Hammer (+15 pts)"
            });
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Acolytes w/" + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Acolytes w/" + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Laspistol");
                        Weapons.Add("Chainsword");
                        lbModelSelect.Items.Add("Acolyte w/ " + Weapons[(UnitSize - 1) * 2] + " and " + Weapons[((UnitSize - 1) * 2) + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize * 2, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    antiLoop = false;
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

            foreach (string weapon in Weapons)
            {
                if (weapon.Contains("(+5 pts)"))
                {
                    Points += 5;
                }
                else if (weapon.Contains("(+10 pts)"))
                {
                    Points += 10;
                }
                else if (weapon.Contains("(+15 pts)"))
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Acolytes - " + Points + "pts";
        }
    }
}
