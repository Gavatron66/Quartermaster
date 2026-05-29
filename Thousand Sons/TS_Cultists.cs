using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Thousand_Sons
{
    public class TS_Cultists : Datasheets
    {
        int currentIndex;

        public TS_Cultists()
        {
            DEFAULT_POINTS = 5;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Autogun");
            }

            Keywords.AddRange(new string[]
            {
                "CHAOS", "TZEENTCH", "THOUSAND SONS",
                "INFANTRY", "THOUSAND SONS CULTISTS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new TS_Cultists();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as ThousandSons;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 30;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("TS Cultist Champion w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("TS Cultist w/ " + Weapons[i]);
            }
        }


        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[0] = "TS Cultist Champion w/ " + Weapons[0];
                        break;
                    }

                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "TS Cultist w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Autogun");
                        lbModelSelect.Items.Add("TS Cultist w/ Autogun");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize) + 1, 1);
                    }
                    break;
                case 61:
                    if (antiLoop)
                    {
                        return;
                    }

                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Enabled = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Autogun",
                            "Autopistol and BAW",
                            "Bolt Pistol and BAW",
                        });
                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        restrictedIndexes.Clear();
                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbOption1.Enabled = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Autogun",
                            "Autopistol and BAW",
                            "Flamer",
                            "Heavy Stubber"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        LoadOptions(cmbOption1);
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Thousand Sons Culists - " + Points + "pts";
        }

        private void LoadOptions(ComboBox cmbOption1)
        {
            restrictedIndexes.Clear();
            int[] restricted = new int[2] { 0, 0 };

            foreach (var weapon in Weapons)
            {
                if (weapon == "Flamer")
                {
                    restricted[0]++;
                }
                else if (weapon == "Heavy Stubber")
                {
                    restricted[1]++;
                }
            }

            if (restricted[0] == Convert.ToInt32(UnitSize / 10) && Weapons[currentIndex] != "Flamer")
            {                                               
                restrictedIndexes.Add(2);                   
            }                                               
            if (restricted[1] == Convert.ToInt32(UnitSize / 10) && Weapons[currentIndex] != "Heavy Stubber")
            {                                               
                restrictedIndexes.Add(3);                   
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }
}
