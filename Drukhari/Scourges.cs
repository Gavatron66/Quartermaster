using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Scourges : Datasheets
    {
        int currentIndex;
        int restriction;

        public Scourges()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Shardcarbine");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Shardcarbine");
            }
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<HAEMONCULUS COVEN>",
                "INFANTRY", "FLY", "HAYWIRE GRENADE", "CORE", "BLADES FOR HIRE", "SCOURGES"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Scourges();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Drukhari;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Solarite w/ " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Solarite w/ " + Weapons[0] + " and " + Weapons[1]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Scourge w/ " + Weapons[i + 1]);
            }
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
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Solarite w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Solarite w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        break;
                    }

                    Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Scourge w/ " + Weapons[currentIndex + 1];

                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[1] == "(None)")
                    {
                        lbModelSelect.Items[0] = "Solarite w/ " + Weapons[0];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Solarite w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        if (lbModelSelect.Items.Count < UnitSize)
                        {
                            Weapons.Add("Shardcarbine");
                            lbModelSelect.Items.Add("Scourge w/ Shardcarbine");
                        }
                    }

                    if (temp > UnitSize)
                    {
                        if (lbModelSelect.Items.Count > UnitSize)
                        {
                            lbModelSelect.Items.RemoveAt(temp - 1);
                            Weapons.RemoveRange(UnitSize, 1);
                        }
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

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Blast Pistol (+5 pts)",
                            "Shardcarbine",
                            "Splinter Pistol"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Agoniser (+5 pts)",
                            "Power Lance (+5 pts)",
                            "Venom Blade (+5 pts)"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        antiLoop = false;
                        break;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;

                        cmbOption1.Items.Clear();
                        if(restriction == 4 && Weapons[currentIndex + 1] == "Shardcarbine")
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Shardcarbine"
                            });
                        }
                        else
                        {
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Blaster (+10 pts)",
                                "Dark Lance (+15 pts)",
                                "Drukhari Haywire Blaster (+10 pts)",
                                "Heat Lance (+10 pts)",
                                "Shardcarbine",
                                "Shredder (+5 pts)",
                                "Splinter Cannon (+10 pts)"
                            });

                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            restriction = 0;
            foreach (var item in Weapons)
            {
                if(item == "Agoniser (+5 pts)" || item == "Power Lance (+5 pts)" || item == "Venom Blade (+5 pts)" ||
                    item == "Blast Pistol (+5 pts)")
                {
                    Points += 5;
                }

                if(item == "Shredder (+5 pts)")
                {
                    Points += 5;
                    restriction++;
                }

                if(item == "Blaster (+10 pts)" || item == "Drukhari Haywire Blaster (+10 pts)" || item == "Heat Lance (+10 pts)"
                    || item == "Splinter Cannon (+10 pts)")
                {
                    Points += 10;
                    restriction++;
                }

                if(item == "Dark Lance (+15 pts)")
                {
                    Points += 15;
                    restriction++;
                }
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade) * UnitSize;
        }

        public override string ToString()
        {
            return "Scourges - " + Points + "pts";
        }
    }
}