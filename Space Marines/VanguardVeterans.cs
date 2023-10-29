using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class VanguardVeterans : Datasheets
    {
        int currentIndex = 0;

        public VanguardVeterans()
        {
            DEFAULT_POINTS = 19;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m2k";
            Weapons.Add(""); //Jump Packs
            for(int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Pistol");
                Weapons.Add("Astartes Chainsword");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "MELTA BOMBS", "VANGAURD VETERAN SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new VanguardVeterans();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cbOption1.Location = new System.Drawing.Point(243, 208);
            cbOption2.Location = new System.Drawing.Point(243, 238);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Vanguard Veteran Sergeant w/ " + Weapons[1] + " and " + Weapons[2]);

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Vanguard Veteran w/ " + Weapons[(i*2) + 1] + " and " + Weapons[(i * 2) + 2]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Grav-pistol",
                "Lightning Claw",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Storm Shield",
                "Thunder Hammer"
            });
            if (f.currentSubFaction == "Blood Angels" || f.currentSubFaction == "Deathwatch")
            {
                cmbOption1.Items.Insert(3, "Hand Flamer");
                cmbOption1.Items.Insert(4, "Inferno Pistol");
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Grav-pistol",
                "Lightning Claw",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                //Relic Blade - Sergeant only
                "Storm Shield",
                "Thunder Hammer"
            });
            if (f.currentSubFaction == "Blood Angels" || f.currentSubFaction == "Deathwatch")
            {
                cmbOption2.Items.Insert(3, "Hand Flamer");
                cmbOption2.Items.Insert(4, "Inferno Pistol");
            }

            cbOption1.Text = "Heavy Thunder Hammer";
            cbOption2.Text = "Jump Packs (All Models)";
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
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Vanguard Veteran Sergeant w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Vanguard Veteran w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Vanguard Veteran Sergeant w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Vanguard Veteran w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 2) + 1] = "Heavy Thunder Hammer";
                        Weapons[(currentIndex * 2) + 2] = "";
                        if(currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = "Vanguard Veteran Sergeant w/ " + Weapons[(currentIndex * 2) + 1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Vanguard Veteran w/ " + Weapons[(currentIndex * 2) + 1];
                        }

                        cmbOption1.Enabled = false;
                        cmbOption2.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        Weapons[(currentIndex * 2) + 1] = "Bolt Pistol";
                        Weapons[(currentIndex * 2) + 2] = "Astartes Chainsword";
                        cmbOption1.SelectedText = "Bolt Pistol";
                        cmbOption2.SelectedText = "Astartes Chainsword";
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = "Vanguard Veteran Sergeant w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Vanguard Veteran w/ " + Weapons[(currentIndex * 2) + 1] + " and " + Weapons[(currentIndex * 2) + 2];
                        }
                    }
                    break;
                case 22:
                    if(cbOption2.Checked)
                    {
                        Weapons[0] = "Jump Packs";
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
                        Weapons.Add("Bolt Pistol");
                        Weapons.Add("Astartes Chainsword");
                        lbModelSelect.Items.Add("Vanguard Veteran w/ " + Weapons[((UnitSize - 1) * 2) + 1] + " and " + Weapons[((UnitSize - 1) * 2) + 2]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2) + 1, 2);
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
                    else if (currentIndex == -1)
                    {
                        break;
                    }
                    antiLoop = true;

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    cbOption2.Visible = true;

                    if (Weapons[(currentIndex * 2) + 1] != "Heavy Thunder Hammer")
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                    }
                    else
                    {
                        cmbOption1.Enabled = false;
                        cmbOption2.Enabled = false;
                    }

                    if (repo.currentSubFaction == "Deathwatch")
                    {
                        cbOption1.Visible = true;
                    }

                    if(currentIndex == 0)
                    {
                        cmbOption2.Items.Insert(9, "Relic Blade");
                    }
                    else
                    {
                        if(cmbOption2.Items.Contains("Relic Blade"))
                        {
                            cmbOption2.Items.Remove("Relic Blade");
                        }
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);
                    if (Weapons[(currentIndex * 2) + 1] == "Heavy Thunder Hammer")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                    }
                    antiLoop = false;

                    lbModelSelect.SelectedIndex = currentIndex;

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            if (Weapons[0] != "")
            {
                Points += UnitSize * 2;
            }

            foreach (var weapon in Weapons)
            {
                if (weapon == "Lightning Claw" || weapon == "Power Axe" ||
                    weapon == "Power Maul" || weapon == "Power Sword")
                {
                    Points += 3;
                }

                if (weapon == "Storm Shield")
                {
                    Points += 4;
                }

                if (weapon == "Grav-pistol" || weapon == "Hand Flamer" ||
                    weapon == "Inferno Pistol" || weapon == "Plasma Pistol")
                {
                    Points += 5;
                }

                if (weapon == "Power Fist")
                {
                    Points += 8;
                }

                if (weapon == "Relic Blade")
                {
                    Points += 10;
                }

                if (weapon == "Thunder Hammer")
                {
                    Points += 12;
                }

                if (weapon == "Heavy Thunder Hammer")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Vanguard Veteran Squad - " + Points + "pts";
        }
    }
}
