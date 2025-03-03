using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Chosen : Datasheets
    {
        int currentIndex;
        bool icon;
        List<int> restrictedIndexes2 = new List<int>();

        public Chosen()
        {
            DEFAULT_POINTS = 25;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
                Weapons.Add("Bolt Pistol");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVDED", "<LEGION>",
                "INFANTRY", "CORE", "CHOSEN"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Chosen();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Chosen Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Chosen w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Accursed Weapon",
                "Boltgun",
                "Combi-flamer (+10 pts)",
                "Combi-melta (+10 pts)",
                "Combi-plasma (+10 pts)"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Bolt Pistol",
                "Plasma Pistol (+5 pts)",
            });

            cbOption1.Text = "Chaos Icon (+5 pts)";
            cbOption1.Visible = true;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            if (icon)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
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
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Chosen Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        break;
                    }

                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Chosen w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Chosen Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        break;
                    }

                    if (!restrictedIndexes2.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Chosen w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFactionupgrade.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
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
                        Weapons.Add("Bolt Pistol");
                        lbModelSelect.Items.Add("Legionary w/ " + Weapons[temp + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 2) + 1, 2);
                    }
                    break;
                case 61:
                    if (antiLoop)
                    {
                        break;
                    }

                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        LoadOptions(cmbOption1, cmbOption2);
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach(var item in Weapons)
            {
                if(item == "Plasma Pistol (+5 pts)")
                {
                    Points += 5;
                }
                else if (item.Contains("Combi"))
                {
                    Points += 10;
                }
            }

            if(icon) { Points += 5; }
        }

        public override string ToString()
        {
            return "Chosen - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1, ComboBox cmbOption2)
        {
            restrictedIndexes.Clear();
            int[] restrict = new int[3];

            foreach (var item in Weapons)
            {
                if(item == "Plasma Pistol (+5 pts)")
                {
                    restrict[0]++;
                }
                else if (item == "Combi-flamer (+10 pts)" || item == "Combi-melta (+10 pts)" || item == "Combi-plasma (+10 pts)")
                {
                    restrict[1]++;
                }
                else if (item == "Accursed Weapon")
                {
                    restrict[2]++;
                }
            }

            if (restrict[0] == (UnitSize / 5) * 2 && Weapons[(currentIndex * 2) + 1] != "Plasma Pistol (+5 pts)")
            {
                restrictedIndexes2.Add(1);
            }
            if (restrict[1] == (UnitSize / 5) * 2 && (Weapons[currentIndex * 2] == "Boltgun" || Weapons[currentIndex * 2] == "Accursed Weapon"))
            {
                restrictedIndexes.Add(2);
                restrictedIndexes.Add(3);
                restrictedIndexes.Add(4);
            }
            if (restrict[2] == UnitSize / 5 && Weapons[currentIndex * 2] != "Accursed Weapon")
            {
                restrictedIndexes.Add(0);
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
            this.DrawItemWithRestrictions(restrictedIndexes2, cmbOption2);
        }
    }
}
