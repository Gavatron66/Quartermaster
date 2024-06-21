using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class AcolyteHybrids : Datasheets
    {
        int currentIndex;
        public AcolyteHybrids()
        {
            DEFAULT_POINTS = 9;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add("Autopistol");
            Weapons.Add("Cult Claws and Knife");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Autopistol");
            }

            Keywords.AddRange(new string[]
            {
                "TYRANIDS", "GENESTEALER CULTS", "<CULT>",
                "INFANTRY", "CORE", "CROSSFIRE", "ACOLYTE HYBRIDS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new AcolyteHybrids();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as GSC;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cbOption1.Location = new System.Drawing.Point(282, 184);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 15;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Acolyte Leader w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Acolyte Hybrid w/ " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Autopistol",
                "Demolition Charges (+5 pts)",
                "Hand Flamer (+3 pts)",
                "Heavy Rock Cutter (+10 pts)",
                "Heavy Rock Drill (+10 pts)",
                "Heavy Rock Saw (+5 pts)",
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Cult Bonesword (+5 pts)",
                "Cult Claws and Knife",
                "Cult Lash Whip"
            });

            cbOption1.Text = "Cult Icon (+10 pts)";

            cmbFactionupgrade.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            cmbFactionupgrade.Items.Clear();
            cmbFactionupgrade.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFactionupgrade.SelectedIndex = cmbFactionupgrade.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFactionupgrade.SelectedIndex = 0;
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if(currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Acolyte Leader w/ " + Weapons[0] + " and " + Weapons[1];
                        break;
                    }

                    Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Acolyte Hybrid w/ " + Weapons[currentIndex + 1];

                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Acolyte Leader w/ " + Weapons[0] + " and " + Weapons[1];

                    break;
                case 16:
                    Factionupgrade = cmbFactionupgrade.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[currentIndex + 1] = cbOption1.Text;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        Weapons[currentIndex + 1] = "Autopistol";
                        cmbOption1.Enabled = true;
                    }
                    lbModelSelect.Items[currentIndex] = "Acolyte Hybrid w/ " + Weapons[currentIndex + 1];
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Autopistol");
                        lbModelSelect.Items.Add("Acolyte Hybrid w/ Autopistol");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize) + 1, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    string[] tempcoll = new string[6]
                    {
                        "Autopistol",
                        "Demolition Charges (+5 pts)",
                        "Hand Flamer (+3 pts)",
                        "Heavy Rock Cutter (+10 pts)",
                        "Heavy Rock Drill (+10 pts)",
                        "Heavy Rock Saw (+5 pts)",
                    };

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }

                    if (currentIndex == 0 && !antiLoop)
                    {
                        cmbOption1.Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbOption1.Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Autopistol",
                            "Cult Bonesword (+5 pts)",
                            "Cult Lash Whip",
                            "Hand Flamer (+3 pts)"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Items.Clear();
                    for(int i = 0; i < tempcoll.Length; i++)
                    {
                        cmbOption1.Items.Add(tempcoll[i]);
                    }

                    int weaponsCheck = 0;
                    for(int i = 0; i < Weapons.Count; i++)
                    {
                        if (Weapons[i] == "Demolition Charges (+5 pts)")
                        {
                            weaponsCheck++;
                        }
                        if (Weapons[i] == "Heavy Rock Cutter (+10 pts)")
                        {
                            weaponsCheck++;
                        }
                        if (Weapons[i] == "Heavy Rock Drill (+10 pts)")
                        {
                            weaponsCheck++;
                        }
                        if (Weapons[i] == "Heavy Rock Saw (+5 pts)")
                        {
                            weaponsCheck++;
                        }
                    }

                    if (weaponsCheck == (UnitSize / 5) * 2)
                    {
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Demolition Charges (+5 pts)"));
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Heavy Rock Cutter (+10 pts)"));
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Heavy Rock Drill (+10 pts)"));
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Heavy Rock Saw (+5 pts)"));
                    }

                    cmbOption1.Visible = true;
                    cmbOption1.Enabled = true;
                    cbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption2.Visible = false;
                    panel.Controls["lblOption2"].Visible = false;

                    if(Weapons.Contains("Cult Icon (+10 pts)"))
                    {
                        cbOption1.Enabled = false;
                    }
                    else
                    {
                        cbOption1.Enabled = true;
                    }

                    antiLoop = true;
                    if (Weapons[currentIndex + 1] == "Cult Icon (+10 pts)")
                    {
                        cbOption1.Enabled = true;
                        cbOption1.Checked = true;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    if (Weapons[currentIndex + 1] == "Demolition Charges (+5 pts)" 
                        || Weapons[currentIndex + 1] == "Heavy Rock Cutter (+10 pts)"
                        || Weapons[currentIndex + 1] == "Heavy Rock Drill (+10 pts)" 
                        || Weapons[currentIndex + 1] == "Heavy Saw (+5 pts)")
                    {
                        cmbOption1.Items.Clear();
                        for (int i = 0; i < tempcoll.Length; i++)
                        {
                            cmbOption1.Items.Add(tempcoll[i]);
                        }

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }
                    antiLoop = false;

                    Points = UnitSize * DEFAULT_POINTS;

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var item in Weapons)
            {
                if(item == "Cult Bonesword (+5 pts)"
                    || item == "Heavy Rock Saw (+5 pts)"
                    || item == "Demolition Charges (+5 pts)")
                {
                    Points += 5;
                }

                if(item == "Cult Icon (+10 pts)")
                {
                    Points += 10;
                }

                if(item == "Heavy Rock Cutter (+10 pts)"
                    || item == "Heavy Rock Drill (+10 pts)")
                {
                    Points += 10;
                }

                if(item == "Hand Flamer (+3 pts)")
                {
                    Points += 3;
                }
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Acolyte Hybrids - " + Points + "pts";
        }
    }
}
