using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Dark_Angels
{
    public class DeathwingCommand : Datasheets
    {
        int currentIndex;
        int restriction = 0;

        public DeathwingCommand()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 2;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Storm Bolter");
                Weapons.Add("Power Fist");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "DARK ANGELS",
                "INFANTRY", "CORE", "TERMINATOR", "DEATHWING", "INNER CIRCLE", "COMMAND SQUAD", "DEATHWING COMMAND SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathwingCommand();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 2;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 5;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Deathwing Sergeant w/ " + Weapons[1] + " and " + Weapons[2]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Deathwing Terminator w/ " + Weapons[(i * 2) + 1] + " and " + Weapons[(i * 2) + 2]);
            }

            cbOption1.Text = "Watcher in the Dark";

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Chainfist",
                "Power Fist",
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();

                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Deathwing Sergeant w/ " + Weapons[1] + " and " + Weapons[2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Deathwing Terminator w/ " + Weapons[(currentIndex * 2) + 1]
                            + " and " + Weapons[(currentIndex * 2) + 2];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 2] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Deathwing Sergeant w/ " + Weapons[1] + " and " + Weapons[2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Deathwing Terminator w/ " + Weapons[(currentIndex * 2) + 1]
                            + " and " + Weapons[(currentIndex * 2) + 2];
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[0] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Storm Bolter");
                            Weapons.Add("Power Fist");
                            lbModelSelect.Items.Add("Deathwing Terminator w/ " + Weapons[(currentIndex * 2) + 1]
                                + " and " + Weapons[(currentIndex * 2) + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 2) + 1, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);

                        if (Weapons[0] == "")
                        {
                            cbOption1.Checked = false;
                        }
                        else
                        {
                            cbOption1.Checked = true;
                        }

                        if (Weapons[(currentIndex * 2) + 1] == "Storm Bolter" && restriction == UnitSize / 5)
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Storm Bolter",
                                "Thunder Hammer and Storm Shield",
                                "Two Lightning Claws"
                            });
                        }
                        else
                        {
                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Assault Cannon",
                                "Cyclone Missile Launcher and Storm Bolter",
                                "Heavy Flamer",
                                "Plasma Cannon",
                                "Storm Bolter",
                                "Thunder Hammer and Storm Shield",
                                "Two Lightning Claws"
                            });
                        }
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        if (Weapons[(currentIndex * 2) + 1] == "Thunder Hammer and Storm Shield" || Weapons[(currentIndex * 2) + 1] == "Two Lightning Claws")
                        {
                            cmbOption2.Enabled = false;
                        }
                        else
                        {
                            cmbOption2.Enabled = true;
                        }

                    }
                    break;
                default: break;
            }

            antiLoop = false;

            Points = DEFAULT_POINTS * UnitSize;
            restriction = 0;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Assault Cannon" || weapon == "Cyclone Missile Launcher and Storm Bolter" || weapon == "Heavy Flamer"
                    || weapon == "Plasma Cannon")
                {
                    restriction++;
                }
            }
        }

        public override string ToString()
        {
            return "Deathwing Command Squad - " + Points + "pts";
        }
    }
}