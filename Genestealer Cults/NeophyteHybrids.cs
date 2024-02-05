using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class NeophyteHybrids : Datasheets
    {
        int currentIndex;
        public NeophyteHybrids()
        {
            DEFAULT_POINTS = 6;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add("Autogun");
            Weapons.Add("Autopistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Autogun");
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
            return new NeophyteHybrids();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as GSC;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Neophyte Leader w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Neophyte Hybrid w/ " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Autogun",
                "Cult Shotgun",
                "Flamer",
                "Grenade Lancher",
                "Heavy Stubber",
                "Mining Laser (+15 pts)",
                "Seismic Cannon (+15 pts)",
                "Webber"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Autopistol",
                "Bolt Pistol",
                "Web Pistol"
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
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Neophyte Leader w/ " + Weapons[0] + " and " + Weapons[1];
                        break;
                    }

                    Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Neophyte Hybrid w/ " + Weapons[currentIndex + 1];

                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Neophyte Leader w/ " + Weapons[0] + " and " + Weapons[1];
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
                        Weapons[currentIndex + 1] = "Autogun";
                        cmbOption1.Enabled = true;
                    }
                    lbModelSelect.Items[currentIndex] = "Neophyte Hybrid w/ " + Weapons[currentIndex + 1];
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Autogun");
                        lbModelSelect.Items.Add("Neophyte Hybrid w/ Autogun");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize) + 1, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    string[] tempcoll = new string[8]
                    {
                        "Autogun",
                        "Cult Shotgun",
                        "Flamer",
                        "Grenade Launcher",
                        "Heavy Stubber",
                        "Mining Laser (+15 pts)",
                        "Seismic Cannon (+15 pts)",
                        "Webber"
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
                            "Autogun",
                            "Chainsword",
                            "Power Maul",
                            "Power Pick"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Items.Clear();
                    for (int i = 0; i < tempcoll.Length; i++)
                    {
                        cmbOption1.Items.Add(tempcoll[i]);
                    }

                    int[] weaponsCheck = new int[2] { 0, 0 };
                    for (int i = 0; i < Weapons.Count; i++)
                    {
                        if (Weapons[i] == "Heavy Stubber")
                        {
                            weaponsCheck[0]++;
                        }
                        if (Weapons[i] == "Mining Laser (+15 pts)")
                        {
                            weaponsCheck[0]++;
                        }
                        if (Weapons[i] == "Seismic Cannon (+15 pts)")
                        {
                            weaponsCheck[0]++;
                        }
                        if (Weapons[i] == "Flamer")
                        {
                            weaponsCheck[1]++;
                        }
                        if (Weapons[i] == "Grenade Launcher")
                        {
                            weaponsCheck[1]++;
                        }
                        if (Weapons[i] == "Webber")
                        {
                            weaponsCheck[1]++;
                        }
                    }

                    if (weaponsCheck[0] == (UnitSize / 10) * 2)
                    {
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Heavy Stubber"));
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Mining Laser (+15 pts)"));
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Seismic Cannon (+15 pts)"));
                    }
                    if (weaponsCheck[1] == (UnitSize / 10) * 2)
                    {
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Flamer"));
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Grenade Launcher"));
                        cmbOption1.Items.RemoveAt(cmbOption1.Items.IndexOf("Webber"));
                    }

                    cmbOption1.Visible = true;
                    cmbOption1.Enabled = true;
                    cbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption2.Visible = false;
                    panel.Controls["lblOption2"].Visible = false;

                    if (Weapons.Contains("Cult Icon (+10 pts)"))
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

                    if ((Weapons[currentIndex + 1] == "Heavy Stubber"
                        || Weapons[currentIndex + 1] == "Mining Laser (+15 pts)"
                        || Weapons[currentIndex + 1] == "Seismic Cannon (+15 pts)")
                        && !cmbOption1.Items.Contains("Heavy Stubber"))
                    {
                        cmbOption1.Items.Add("Heavy Stubber");
                        cmbOption1.Items.Add("Mining Laser (+15 pts)");
                        cmbOption1.Items.Add("Seismic Cannon (+15 pts)");

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    if ((Weapons[currentIndex + 1] == "Flamer"
                        || Weapons[currentIndex + 1] == "Grenade Launcher"
                        || Weapons[currentIndex + 1] == "Webber")
                        && !cmbOption1.Items.Contains("Flamer"))
                    {
                        cmbOption1.Items.Add("Flamer");
                        cmbOption1.Items.Add("Grenade Launcher");
                        cmbOption1.Items.Add("Webber");

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }
                    antiLoop = false;

                    Points = UnitSize * DEFAULT_POINTS;

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var item in Weapons)
            {
                if (item == "Cult Icon (+10 pts)")
                {
                    Points += 10;
                }

                if (item == "Mining Laser (+15 pts)"
                    || item == "Seismic Cannon (+15 pts)")
                {
                    Points += 15;
                }
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Neophyte Hybrids - " + Points + "pts";
        }
    }
}
