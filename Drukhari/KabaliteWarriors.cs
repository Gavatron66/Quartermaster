using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class KabaliteWarriors : Datasheets
    {
        int currentIndex;
        int[] restriction = new int[] { 0, 0 };

        public KabaliteWarriors()
        {
            DEFAULT_POINTS = 8;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m1k";
            Weapons.Add("");
            Weapons.Add("Splinter Rifle");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i += 5)
            {
                Weapons.Add("Splinter Rifle");
            }
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<KABAL>",
                "INFANTRY", "CORE", "KABALITE WARRIORS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new KabaliteWarriors();
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
            if (Weapons[2] == "(None)")
            {
                lbModelSelect.Items.Add("Sybarite w/ " + Weapons[1]);
            }
            else
            {
                lbModelSelect.Items.Add("Sybarite w/ " + Weapons[1] + " and " + Weapons[2]);
            }

            for (int i = 1; i < (UnitSize / 5) + 1; i++)
            {
                lbModelSelect.Items.Add("Kabalite Warrior w/ " + Weapons[i + 2]);
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

                        if (Weapons[2] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Sybarite w/ " + Weapons[1];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Sybarite w/ " + Weapons[1] + " and " + Weapons[2];
                        }
                        break;
                    }

                    Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Kabalite Warrior w/ " + Weapons[currentIndex + 2];

                    break;
                case 12:
                    Weapons[2] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[2] == "(None)")
                    {
                        lbModelSelect.Items[0] = "Sybarite w/ " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Sybarite w/ " + Weapons[1] + " and " + Weapons[2];
                    }
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

                    if (temp < UnitSize)
                    {
                        if(lbModelSelect.Items.Count < (UnitSize / 5) + 1)
                        {
                            Weapons.Add("Splinter Rifle");
                            lbModelSelect.Items.Add("Kabalite Warrior w/ " + Weapons[(UnitSize / 5) + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        if (lbModelSelect.Items.Count > (UnitSize / 5) + 1)
                        {
                            lbModelSelect.Items.RemoveAt(temp / 5);
                            Weapons.RemoveRange((UnitSize / 5), 1);
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
                        panel.Controls["lblFaction"].Visible = false;
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
                            "Splinter Rifle"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[1]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Agoniser (+5 pts)",
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
                            "Blaster (+10 pts)",
                            "Dark Lance (+15 pts)",
                            "Shredder (+5 pts)",
                            "Splinter Cannon (+10 pts)",
                            "Splinter Rifle",
                        });

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);

                        if(((UnitSize / 10) == restriction[0]) && cmbOption1.SelectedItem != null
                            && !(cmbOption1.SelectedItem.ToString() == "Dark Lance (+15 pts)" || cmbOption1.SelectedItem.ToString() == "Splinter Cannon (+10 pts)"))
                        {
                            cmbOption1.Items.Remove("Dark Lance (+15 pts)");
                            cmbOption1.Items.Remove("Splinter Cannon (+10 pts)");
                        }

                        if (((UnitSize / 5) == restriction[1]) && cmbOption1.SelectedItem != null
                            && !(cmbOption1.SelectedItem.ToString() == "Blaster (+10 pts)" || cmbOption1.SelectedItem.ToString() == "Shredder (+5 pts)"))
                        {
                            cmbOption1.Items.Remove("Blaster (+10 pts)");
                            cmbOption1.Items.Remove("Shredder (+5 pts)");
                        }
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            restriction[0] = 0;
            restriction[1] = 0;
            foreach (var item in Weapons)
            {
                if(item == "Splinter Cannon (+10 pts)" || item == "Dark Lance (+15 pts)")
                {
                    restriction[0]++;
                }

                if (item == "Blaster (+10 pts)" || item == "Shredder (+5 pts)")
                {
                    restriction[1]++;
                }

                if(item == "Agoniser (+5 pts)" || item == "Blast Pistol (+5 pts)" || item == "Phantasm Grenade Launcher (+5 pts)"
                    || item == "Power Sword (+5 pts)" || item == "Shredder (+5 pts)")
                {
                    Points += 5;
                }

                if(item == "Blaster (+10 pts)" || item == "Splinter Cannon (+10 pts)")
                {
                    Points += 10;
                }

                if (item == "Dark Lance (+15 pts)")
                {
                    Points += 15;
                }
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade) * UnitSize;
        }

        public override string ToString()
        {
            return "Kabalite Warriors - " + Points + "pts";
        }
    }
}