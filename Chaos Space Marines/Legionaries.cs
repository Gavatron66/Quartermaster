using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Legionaries : Datasheets
    {
        int currentIndex;
        bool icon;
        bool balefire = false;
        bool khorne = false;

        public Legionaries()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add("Boltgun");
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVDED", "<LEGION>",
                "INFANTRY", "CORE", "LEGIONARIES"
            });
            Role = "Troops";
            PsykerPowers = new string[1] { string.Empty };
        }

        public override Datasheets CreateUnit()
        {
            return new Legionaries();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            cbOption1.Location = new System.Drawing.Point(282, 184);
            lblPsyker.Location = new System.Drawing.Point(panel.Controls["lblFactionUpgrade"].Location.X, panel.Controls["lblFactionUpgrade"].Location.Y + 52);
            clbPsyker.Location = new System.Drawing.Point(cmbFaction.Location.X, cmbFaction.Location.Y + 52);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Aspiring Champion w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Legionary w/ " + Weapons[i + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Tainted Chainaxe"
            });

            cbOption1.Text = "Chaos Icon";

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

            if(icon)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("DH");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            lblPsyker.Text = "Select one of the following:";
            clbPsyker.ClearSelected();
            for (int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
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
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Aspiring Champion w/ " + Weapons[0] + " and " + Weapons[1];
                        break;
                    }

                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Legionary w/ " + Weapons[currentIndex + 1];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    if (Weapons[currentIndex + 1] == "Balefire Tome (+20 pts)" || balefire)
                    {
                        clbPsyker.Visible = true;
                        lblPsyker.Visible = true;
                    }
                    else
                    {
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;

                        if (PsykerPowers[0] != string.Empty)
                        {
                            clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), false);
                            PsykerPowers[0] = string.Empty;
                        }
                    }
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Aspiring Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    break;
                case 16:
                    Factionupgrade = cmbFactionupgrade.Text;

                    if(Factionupgrade == "Mark of Khorne (+15 pts)")
                    {
                        khorne = true;

                        if(balefire)
                        {
                            int tempIndex = Weapons.IndexOf("Balefire Tome (+20 pts)");
                            Weapons[tempIndex] = "Boltgun";
                            lbModelSelect.Items[tempIndex - 1] = "Legionary w/ " + Weapons[tempIndex];

                            PsykerPowers[0] = string.Empty;
                            clbPsyker.Visible = false;
                            lblPsyker.Visible = false;
                        }
                    }
                    else
                    {
                        khorne = false;
                    }
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
                        lbModelSelect.Items.Add("Legionary w/ " + Weapons[temp + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize) + 1, 1);
                    }
                    break;
                case 60:
                    if (clbPsyker.CheckedItems.Count < 1)
                    {
                        break;
                    }
                    else if (clbPsyker.CheckedItems.Count == 1)
                    {
                        PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                    }
                    else
                    {
                        clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                    }
                    break;
                case 61:
                    if(antiLoop)
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
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        restrictedIndexes.Clear();
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Boltgun",
                            "Daemon Blade",
                            "Plasma Pistol",
                            "Power Axe",
                            "Power Fist",
                            "Power Maul",
                            "Power Sword"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        break;
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
                            "Astartes Chainsword",
                            "Balefire Tome (+20 pts)",
                            "Boltgun",
                            "Flamer",
                            "Havoc Autocannon",
                            "Heavy Bolter",
                            "Heavy Chainaxe",
                            "Lascannon",
                            "Meltagun",
                            "Missile Launcher",
                            "Plasma Gun",
                            "Plasma Pistol",
                            "Reaper Chaincannon"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                        LoadOptions(cmbOption1);
                    }

                    if (balefire)
                    {
                        clbPsyker.Visible = true;
                        lblPsyker.Visible = true;
                    }
                    else
                    {
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if(Weapons.Contains("Balefire Tome (+20 pts)"))
            {
                Points += 20;
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Legionaries - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1)
        {
            restrictedIndexes.Clear();
            bool chainaxe = false;
            balefire = false;
            List<string> specialHeavy = new List<string>();

            for(int i = 2; i < Weapons.Count; i++)
            {
                if (Weapons[i] == "Heavy Chainaxe")
                {
                    chainaxe = true;
                }
                else if(Weapons[i] == "Balefire Tome (+20 pts)")
                {
                    balefire = true;
                }
                else if(Weapons[i] != "Boltgun" && Weapons[i] != "Astartes Chainsword")
                {
                    specialHeavy.Add(Weapons[i]);
                }
            }

            if(chainaxe && Weapons[currentIndex + 1] != "Heavy Chainaxe")
            {
                restrictedIndexes.Add(6);
            }
            if((balefire && Weapons[currentIndex + 1] != "Balefire Tome (+20 pts)") || khorne)
            {
                restrictedIndexes.Add(1);
            }
            if(specialHeavy.Count > 0 && !specialHeavy.Contains(Weapons[currentIndex + 1]))
            {
                for(int i = 0; i < specialHeavy.Count; i++)
                {
                    if (specialHeavy[i] != "Plasma Pistol")
                    {
                        restrictedIndexes.Add(cmbOption1.Items.IndexOf(specialHeavy[i]));
                    }
                }
            }

            if(specialHeavy.Count == UnitSize / 5 && !specialHeavy.Contains(Weapons[currentIndex + 1]))
            {
                restrictedIndexes.AddRange(new int[] { 3, 4, 5, 7, 8, 9, 10, 11, 12 });
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }
}
