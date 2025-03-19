using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class NeophyteHybrids : Datasheets
    {
        int currentIndex;
        int iconIndex = -1;

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
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

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
                if(i != iconIndex)
                {
                    lbModelSelect.Items.Add("Neophyte Hybrid w/ " + Weapons[i + 1]);
                }
                else
                {
                    lbModelSelect.Items.Add("Neophyte Hybrid w/ " + Weapons[currentIndex + 1] + " and Cult Icon");
                }
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Autopistol",
                "Bolt Pistol",
                "Web Pistol"
            });

            cbOption1.Text = "Cult Icon (+10 pts)";
            if(iconIndex < 0)
            {
                cbOption1.Enabled = true;
                cbOption1.Checked = false;
            }
            else
            {
                cbOption1.Enabled = false;
                int temp = iconIndex;
                cbOption1.Checked = true;
                iconIndex = temp;
            }

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

            cbStratagem3.Text = repo.StratagemList[2].ToString();
            cbStratagem3.Location = new System.Drawing.Point(cmbFactionupgrade.Location.X, cmbFactionupgrade.Location.Y + 32);
            cbStratagem3.Visible = true;

            if (repo.currentSubFaction == "The Bladed Cog")
            {
                if (Stratagem.Contains(cbStratagem3.Text))
                {
                    cbStratagem3.Checked = true;
                    cbStratagem3.Enabled = true;
                }
                else
                {
                    cbStratagem3.Checked = false;
                    cbStratagem3.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem3.Text));
                }
            }
            else
            {
                cbStratagem3.Enabled = false;
                cbStratagem3.Checked = false;
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
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "Neophyte Leader w/ " + Weapons[0] + " and " + Weapons[1];
                        break;
                    }

                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                        if(currentIndex != iconIndex)
                        {
                            lbModelSelect.Items[currentIndex] = "Neophyte Hybrid w/ " + Weapons[currentIndex + 1];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Neophyte Hybrid w/ " + Weapons[currentIndex + 1] + " and Cult Icon";
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }
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
                        iconIndex = currentIndex;
                    }
                    else
                    {
                        iconIndex = -1;
                    }
                    lbModelSelect.Items[currentIndex] = "Neophyte Hybrid w/ " + Weapons[currentIndex + 1] + " and Cult Icon";
                    restrictedIndexes.Clear();
                    restrictedIndexes.AddRange(new int[] { 2, 3, 4, 5, 6, 7 });
                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
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

                    restrictedIndexes.Clear();
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

                    if ((weaponsCheck[0] == (UnitSize / 10) * 2) &&
                        !(Weapons[currentIndex + 1] == "Heavy Stubber" || Weapons[currentIndex + 1] == "Mining Laser (+15 pts)" || Weapons[currentIndex + 1] == "Seismic Cannon (+15 pts)"))
                    {
                        restrictedIndexes.AddRange(new int[] { 4, 5, 6 });
                    }

                    if ((weaponsCheck[1] == (UnitSize / 10) * 2) &&
                        !(Weapons[currentIndex + 1] == "Flamer" || Weapons[currentIndex + 1] == "Grenade Launcher" || Weapons[currentIndex + 1] == "Webber"))
                    {
                        restrictedIndexes.AddRange(new int[] { 2, 3, 7 });
                    }

                    if(currentIndex == iconIndex)
                    {
                        restrictedIndexes.AddRange(new int[] { 2, 3, 4, 5, 6, 7 });
                        cbOption1.Enabled = true;
                    }
                    else
                    {
                        if(iconIndex < 0)
                        {
                            cbOption1.Enabled = true;
                        }
                        else
                        {
                            cbOption1.Enabled = false;
                        }
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                    cmbOption1.Visible = true;
                    cmbOption1.Enabled = true;
                    cbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption2.Visible = false;
                    panel.Controls["lblOption2"].Visible = false;

                    antiLoop = true;
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    antiLoop = false;

                    Points = UnitSize * DEFAULT_POINTS;

                    break;
                case 73:
                    if (cbStratagem3.Checked)
                    {
                        Stratagem.Add(cbStratagem3.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Remove(cbStratagem3.Text);
                        }
                    }
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
