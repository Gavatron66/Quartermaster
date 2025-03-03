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
    public class Raptors : Datasheets
    {
        int currentIndex;

        public Raptors()
        {
            DEFAULT_POINTS = 21;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            Weapons.Add("Bolt Pistol");
            Weapons.Add("Astartes Chainsword");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Pistol and Astartes Chainsword");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVIDED", "<LEGION>",
                "INFANTRY", "CORE", "JUMP PACK", "FLY", "RAPTORS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Raptors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Raptor Champion w/ " + Weapons[0] + " and " + Weapons[1]);

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Raptor w/ " + Weapons[i + 1]);
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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Raptor Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                        {
                            Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Raptor w/ " + Weapons[currentIndex + 1];
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        }
                    }

                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Raptor Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Bolt Pistol and Astartes Chainsword");
                            lbModelSelect.Items.Add("Raptor w/ " + Weapons[temp + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(temp + 1, 1);
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

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Bolt Pistol",
                            "Plasma Pistol (+5 pts)"
                        });

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Power Fist (+10 pts)",
                            "Power Sword (+5 pts)"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Bolt Pistol and Astartes Chainsword",
                            "Plasma Pistol and Astartes Chainsword (+5 pts)",
                            "Flamer (+5 pts)",
                            "Meltagun (+10 pts)",
                            "Plasma Gun (+10 pts)"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        LoadDatasheets(cmbOption1);
                    }

                    antiLoop = false;

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (var weapon in Weapons)
            {
                if (weapon == "Plasma Pistol and Astartes Chainsword (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Flamer (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Meltagun (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Plasma Gun (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Plasma Pistol (+5 pts)")
                {
                    Points += 5;
                }
                else if (weapon == "Power Fist (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Power Sword (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Raptors - " + Points + "pts";
        }

        private void LoadDatasheets(ComboBox cmbOption1)
        {
            restrictedIndexes.Clear();
            int restrict = 0;

            foreach (var item in Weapons)
            {
                if (item == "Plasma Pistol and Astartes Chainsword (+5 pts)")
                {
                    restrict++;
                }
                else if (item == "Flamer (+5 pts)")
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
            }

            if (restrict == 2 && Weapons[currentIndex + 1] == "Bolt Pistol and Astartes Chainsword")
            {
                restrictedIndexes.AddRange(new int[] { 1, 2, 3, 4 });
                this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
            }
        }
    }
}
