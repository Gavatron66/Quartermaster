using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Wyches : Datasheets
    {
        int currentIndex;
        int[] restriction = new int[] { 0, 0, 0 };
        public Wyches()
        {
            DEFAULT_POINTS = 10;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("");
            Weapons.Add("Splinter Pistol");
            Weapons.Add("Hekatarii Blade");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<WYCH CULT>",
                "INFANTRY", "CORE", "HAYWIRE GRENADES", "WYCHES"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Wyches();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Drukhari;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Hekatrix w/ " + Weapons[1] + " and " + Weapons[2]);

            if(UnitSize >= 10)
            {
                Weapons.Add("Splinter Pistol and Hekatarii Blade");
                Weapons.Add("Splinter Pistol and Hekatarii Blade");
                Weapons.Add("Splinter Pistol and Hekatarii Blade");
                lbModelSelect.Items.Add("Wych w/ " + Weapons[3]);
                lbModelSelect.Items.Add("Wych w/ " + Weapons[4]);
                lbModelSelect.Items.Add("Wych w/ " + Weapons[5]);
            }
            else if (UnitSize == 20)
            {
                Weapons.Add("Splinter Pistol and Hekatarii Blade");
                Weapons.Add("Splinter Pistol and Hekatarii Blade");
                Weapons.Add("Splinter Pistol and Hekatarii Blade");
                lbModelSelect.Items.Add("Wych w/ " + Weapons[6]);
                lbModelSelect.Items.Add("Wych w/ " + Weapons[7]);
                lbModelSelect.Items.Add("Wych w/ " + Weapons[8]);
            }

            cbOption1.Text = "Phantasm Grenade Launcher (+5 pts)";

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
            panel.Controls["lblFactionUpgrade"].Text = "Favoured Retinue";

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            panel.Controls["lblFactionUpgrade"].Visible = true;
            cmbFaction.Visible = true;
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
                        Weapons[1] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Hekatrix w/ " + Weapons[1] + " and " + Weapons[2];
                        break;
                    }

                    Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Wych w/ " + Weapons[currentIndex + 2];

                    break;
                case 12:
                    Weapons[2] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Hekatrix w/ " + Weapons[1] + " and " + Weapons[2];
                    break;
                case 16:
                    Factionupgrade = cmbFactionupgrade.Text;
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

                    if(UnitSize >= 10 && temp < 10)
                    {
                        Weapons.Add("Splinter Pistol and Hekatarii Blade");
                        Weapons.Add("Splinter Pistol and Hekatarii Blade");
                        Weapons.Add("Splinter Pistol and Hekatarii Blade");
                        lbModelSelect.Items.Add("Wych w/ " + Weapons[3]);
                        lbModelSelect.Items.Add("Wych w/ " + Weapons[4]);
                        lbModelSelect.Items.Add("Wych w/ " + Weapons[5]);
                    }

                    if(UnitSize == 20 && temp < 20)
                    {
                        Weapons.Add("Splinter Pistol and Hekatarii Blade");
                        Weapons.Add("Splinter Pistol and Hekatarii Blade");
                        Weapons.Add("Splinter Pistol and Hekatarii Blade");
                        lbModelSelect.Items.Add("Wych w/ " + Weapons[6]);
                        lbModelSelect.Items.Add("Wych w/ " + Weapons[7]);
                        lbModelSelect.Items.Add("Wych w/ " + Weapons[8]);
                    }

                    if (temp > UnitSize)
                    {
                        if(UnitSize < 20 && temp == 20)
                        {
                            lbModelSelect.Items.RemoveAt(6);
                            lbModelSelect.Items.RemoveAt(5);
                            lbModelSelect.Items.RemoveAt(4);
                            Weapons.RemoveRange(Weapons.Count - 3, 3);
                        }

                        if (UnitSize < 10 && temp >= 10)
                        {
                            lbModelSelect.Items.RemoveAt(3);
                            lbModelSelect.Items.RemoveAt(2);
                            lbModelSelect.Items.RemoveAt(1);
                            Weapons.RemoveRange(Weapons.Count - 3, 3);
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
                        cbOption1.Visible = false;
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
                        cbOption1.Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Blast Pistol (+5 pts)",
                            "Splinter Pistol",
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Agoniser (+5 pts)",
                            "Hekatarii Blade",
                            "Power Sword (+5 pts)"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[2]);

                        antiLoop = false;
                        break;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cbOption1.Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Hydra Gauntlets (+5 pts)",
                            "Razorflails (+5 pts)",
                            "Shardnet and Impaler (+10 pts)",
                            "Splinter Pistol and Hekatarii Blade"
                        });

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);

                        if (restriction[0] == UnitSize / 10 && Weapons[currentIndex + 2] != "Hydra Gauntlets (+5 pts)")
                        {
                            cmbOption1.Items.Remove("Hydra Gauntlets (+5 pts)");
                        }

                        if (restriction[1] == UnitSize / 10 && Weapons[currentIndex + 2] != "Razorflails (+5 pts)")
                        {
                            cmbOption1.Items.Remove("Razorflails (+5 pts)");
                        }

                        if (restriction[2] == UnitSize / 10 && Weapons[currentIndex + 2] != "Shardnet and Impaler (+10 pts)")
                        {
                            cmbOption1.Items.Remove("Shardnet and Impaler (+10 pts)");
                        }
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            restriction[0] = 0;
            restriction[1] = 0;
            restriction[2] = 0;

            foreach (var item in Weapons)
            {
                if (item == "Hydra Gauntlets (+5 pts)")
                {
                    restriction[0]++;
                }

                if (item == "Razorflails (+5 pts)")
                {
                    restriction[1]++;
                }

                if (item == "Shardnet and Impaler (+10 pts)")
                {
                    restriction[2]++;
                }

                if(item != "Splinter Pistol and Hekatarii Blade" && item != "Splinter Pistol" && item != "Hekatarii Blade" && item != "")
                {
                    if(item == "Shardnet and Impaler (+10 pts)")
                    {
                        Points += 10;
                    }
                    else
                    {
                        Points += 5;
                    }
                }
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade) * UnitSize;
        }

        public override string ToString()
        {
            return "Wyches - " + Points + "pts";
        }
    }
}