using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class NoiseMarines : Datasheets
    {
        int currentIndex;
        bool icon;
        bool doomSiren;

        public NoiseMarines()
        {
            DEFAULT_POINTS = 21;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize + 20;
            TemplateCode = "NL2m2k";
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "INFANTRY", "CORE", "MARK OF CHAOS", "NOISE MARINES"
            });
            Role = "Elites";
            Factionupgrade = "Mark of Slaanesh (+20 pts)";
        }

        public override Datasheets CreateUnit()
        {
            return new NoiseMarines();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ChaosSpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 60);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 60);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (doomSiren)
            {
                lbModelSelect.Items.Add("Noise Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and Doom Siren");
            }
            else
            {
                lbModelSelect.Items.Add("Noise Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Noise Marine w/ " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Plasma Pistol (+5 pts)",
                "Power Axe (+5 pts)",
                "Power Fist (+10 pts)",
                "Power Maul (+5 pts)",
                "Power Sword (+5 pts)",
                "Sonic Blaster (+5 pts)",
                "Tainted Chainaxe (+5 pts)"
            });

            cbOption1.Text = "Doom Siren (+10 pts)";
            cbOption2.Text = "Icon of Slaanesh (+5 pts)";
            cbOption2.Visible = true;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch(code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[0] = cmbOption1.SelectedItem.ToString();
                            if (doomSiren)
                            {
                                lbModelSelect.Items[0] = "Noise Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and Doom Siren";
                            }
                            else
                            {
                                lbModelSelect.Items[0] = "Noise Champion w/ " + Weapons[0] + " and " + Weapons[1];
                            }
                        }
                        else
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Noise Marine w/ " + Weapons[currentIndex + 1];
                        }
                    }
                    else
                    {
                        if(currentIndex == 0)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        }
                    }
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    if (doomSiren)
                    {
                        lbModelSelect.Items[0] = "Noise Champion w/ " + Weapons[0] + ", " + Weapons[1] + " and Doom Siren";
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Noise Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        doomSiren = true;
                    }
                    else
                    {
                        doomSiren = false;
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        icon = true;
                    }
                    else
                    {
                        icon = false;
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Boltgun");
                        lbModelSelect.Items.Add("Noise Marine w/ " + Weapons[temp + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize + 1, 1);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }

                    antiLoop = true;
                    restrictedIndexes.Clear();

                    if(currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Boltgun",
                            "Power Axe (+5 pts)",
                            "Power Fist (+10 pts)",
                            "Power Maul (+5 pts)",
                            "Power Sword (+5 pts)",
                            "Tainted Chainaxe (+5 pts)"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Blastmaster (+15 pts)",
                            "Boltgun",
                            "Sonic Blaster (+5 pts)"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                        if(Weapons.Contains("Blastmaster (+15 pts)") && Weapons[currentIndex + 1] != "Blastmaster (+15 pts)")
                        {
                            restrictedIndexes.Add(1);
                        }
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1 );

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach(var weapon in Weapons)
            {
                if(weapon == "Plasma Pistol (+5 pts)" || weapon == "Sonic Blaster (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Blastmaster (+15 pts)")
                {
                    Points += 15;
                }
                else if (weapon.Contains("+5 pts"))
                {
                    Points += 5;
                }
                else if(weapon.Contains("+10 pts"))
                {
                    Points += 10;
                }
            }

            if(doomSiren) { Points += 10; }
            if(icon) { Points += 5; }
        }

        public override string ToString()
        {
            return "Noise Marines - " + Points + "pts";
        }
    }
}
