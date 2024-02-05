using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Roster_Builder.Aeldari
{
    public class WarWalkers : Datasheets
    {
        int currentIndex;
        public WarWalkers()
        {
            DEFAULT_POINTS = 55;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            Weapons.Add("Shuriken Cannon (+5 pts)");
            Weapons.Add("Shuriken Cannon (+5 pts)");
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "VEHICLE", "WAR WALKERS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new WarWalkers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Aeldari;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("War Walker w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Aeldari Missile Launcher (+10 pts)",
                "Bright Lance (+15 pts)",
                "Scatter Laser",
                "Shuriken Cannon (+5 pts)",
                "Starcannon (+10 pts)"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Aeldari Missile Launcher (+10 pts)",
                "Bright Lance (+15 pts)",
                "Scatter Laser",
                "Shuriken Cannon (+5 pts)",
                "Starcannon (+10 pts)"
            });
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

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "War Walker w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "War Walker w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Shuriken Cannon (+5 pts)");
                        Weapons.Add("Shuriken Cannon (+5 pts)");
                        lbModelSelect.Items.Add("War Walker w/ " + Weapons[(UnitSize - 1) * 2] + " and " + Weapons[((UnitSize - 1) * 2) + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 2) - 1, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (var weapon in Weapons)
            {
                if(weapon == "Shuriken Cannon (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Aeldari Missile Launcher (+10 pts)" || weapon == "Starcannon (+10 pts)")
                {
                    Points += 10;
                }

                if(weapon == "Bright Lance (+15 pts)")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "War Walkers - " + Points + "pts";
        }
    }
}
