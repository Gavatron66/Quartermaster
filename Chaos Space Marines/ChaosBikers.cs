using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class ChaosBikers : Datasheets
    {
        bool icon = false;
        int currentIndex;

        public ChaosBikers()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Pistol");
                Weapons.Add("Combi-bolter");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVIDED", "<LEGION>",
                "BIKER", "CORE", "CHAOS BIKERS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new ChaosBikers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 9;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Biker Champion w/ " + Weapons[0]);

            for(int i = 1; i < UnitSize; i++)
            {
                 lbModelSelect.Items.Add("Chaos Biker w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

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

            cbOption1.Text = "Chaos Icon (+5 pts)";
            cbOption1.Visible = true;
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch(code)
            {
                case 11:
                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();

                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Biker Champion w/ " + Weapons[currentIndex * 2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Chaos Biker w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }

                    break;
                case 12:
                    if (!restrictedIndexes.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Chaos Biker w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                    }

                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
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
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Bolt Pistol");
                            Weapons.Add("Combi-bolter");
                            lbModelSelect.Items.Add("Chaos Biker w/ " + Weapons[temp * 2] + " and " + Weapons[(temp * 2) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((temp * 2) + 1, 2);
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

                    if(currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Bolt Pistol",
                            "Plasma Pistol (+5 pts)",
                            "Power Axe (+5 pts)",
                            "Power Fist (+10 pts)",
                            "Power Maul (+5 pts)",
                            "Power Sword (+5 pts)",
                            "Tainted Chainaxe (+5 pts)"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Bolt Pistol",
                        });

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Combi-bolter",
                            "Combi-flamer (+5 pts)",
                            "Combi-melta (+5 pts)",
                            "Combi-plasma (+5 pts)",
                            "Flamer (+5 pts)",
                            "Meltagun (+10 pts)",
                            "Plasma Gun (+10 pts)"
                        });


                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                        LoadDatasheets(cmbOption2);
                    }

                    antiLoop = false;

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach(var weapon in Weapons)
            {
                if(weapon.Contains("+5 pts"))
                {
                    Points += 5;
                }
                else if (weapon.Contains("+10 pts"))
                {
                    Points += 10;
                }
            }

            if(icon) { Points += 5; }
        }

        public override string ToString()
        {
            return "Chaos Bikers - " + Points + "pts";
        }

        private void LoadDatasheets(ComboBox cmbOption2)
        {
            restrictedIndexes.Clear();
            int restrict = 0;

            foreach (var item in Weapons)
            {
                if(item == "Flamer (+5 pts)")
                {
                    restrict++;
                }
                else if (item == "Meltagun (+10 pts)")
                {
                    restrict++;
                }
                else if (item == "Plasma Gun (+10 pts)")
                {
                    restrict++;
                }
                else if(item == "Combi-flamer (+5 pts)")
                {
                    restrict++;
                }
                else if(item == "Combi-melta (+5 pts)")
                {
                    restrict++;
                }
                else if (item == "Combi-plasma (+5 pts)")
                {
                    restrict++;
                }
            }

            if(restrict == 2 && Weapons[(currentIndex * 2) + 1] == "Combi-bolter" )
            {
                restrictedIndexes.AddRange(new int[] { 1, 2, 3, 4, 5, 6 });
                this.DrawItemWithRestrictions(restrictedIndexes, cmbOption2);
            }
        }
    }
}
