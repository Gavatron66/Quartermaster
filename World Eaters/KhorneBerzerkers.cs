using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class KhorneBerzerkers : Datasheets
    {
        int currentIndex;
        bool icon;
        int plasma = 0;
        int evis = 0;

        public KhorneBerzerkers()
        {
            DEFAULT_POINTS = 22;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add("Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Pistol");
                Weapons.Add("Berzerker Chainblade");
            }
            Keywords.AddRange(new string[]
            {
                "CHAOS", "KHORNE", "HERETIC ASTARTES", "BUTCHER ASTARTES", "WORLD EATERS",
                "INFANTRY", "CORE", "KHORNE BERZERKERS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new KhorneBerzerkers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as WorldEaters;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            //cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 60);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Khorne Berzerker Champion w/ " + Weapons[0]);

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Khorne Berzerker w/ " + Weapons[(i * 2) - 1] + " and " + Weapons[i * 2]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bolt Pistol",
                "Plasma Pistol (+5 pts)"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Berzerker Chainblade",
                "Khornate Eviscerator (+5 pts)"
            });

            cbOption1.Text = "Berzerker Icon (+5 pts)";
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

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        if (currentIndex == 0)
                        {
                            Weapons[0] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[0] = "Khorne Berzerker Champion w/ " + Weapons[0];
                        }
                        else
                        {
                            Weapons[(currentIndex * 2) - 1] = cmbOption1.SelectedItem.ToString();
                            lbModelSelect.Items[currentIndex] = "Khorne Berzerker w/ " + Weapons[(currentIndex * 2) - 1] + " and " + Weapons[currentIndex * 2];
                        }
                    }
                    else
                    {
                        if (currentIndex == 0)
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) - 1]);
                        }
                    }
                    break;
                case 12:
                    if (!restrictedIndexes.Contains(cmbOption2.SelectedIndex))
                    {
                        Weapons[currentIndex * 2] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Khorne Berzerker w/ " + Weapons[(currentIndex * 2) - 1] + " and " + Weapons[currentIndex * 2];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);
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
                        Weapons.Add("Bolt Pistol");
                        Weapons.Add("Berzerker Chainblade");
                        lbModelSelect.Items.Add("Khorne Berzerker w/ " + Weapons[(temp * 2) - 1] + " and " + Weapons[temp * 2]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((temp * 2) - 1, 2);
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

                    antiLoop = true;
                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) - 1]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex * 2]);

                        if (plasma == (Weapons[0] == "Plasma Pistol (+5 pts)" ? (UnitSize / 5) + 1 : UnitSize / 5)
                            && Weapons[(currentIndex * 2) - 1] == "Bolt Pistol")
                        {
                            cmbOption1.Enabled = false;
                        }

                        if (evis == UnitSize / 5
                            && Weapons[currentIndex * 2] == "Berzerker Chainblade")
                        {
                            cmbOption2.Enabled = false;
                        }

                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            plasma = 0;
            evis = 0;
            foreach (var weapon in Weapons)
            {
                if (weapon == "Plasma Pistol (+5 pts)")
                {
                    Points += 5;
                    plasma++;
                }

                if (weapon == "Khornate Eviscerator (+5 pts)")
                {
                    Points += 5;
                    evis++;
                }
            }

            if (icon) { Points += 5; }
        }

        public override string ToString()
        {
            return "Khorne Berzerkers - " + Points + "pts";
        }
    }
}
