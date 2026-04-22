using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Warbikers : Datasheets
    {
        int currentIndex;
        bool isLoading = false;

        public Warbikers()
        {
            DEFAULT_POINTS = 25;
            UnitSize = 3;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("(None)");
            }
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "BIKER", "SPEED FREEKs", "CORE", "WARBIKERS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Warbikers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Orks;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[0] == "(None)")
            {
                lbModelSelect.Items.Add("Boss Nob on Warbike");
            }
            else
            {
                lbModelSelect.Items.Add("Boss Nob on Warbike w/ " + Weapons[0]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[i] == "(None)")
                {
                    lbModelSelect.Items.Add("Warbiker");
                }
                else
                {
                    lbModelSelect.Items.Add("Warbiker w/ " + Weapons[i]);
                }
            }

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
            if (isLoading)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        if (Weapons[0] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Boss Nob on Warbike";
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Boss Nob on Warbike w/ " + Weapons[0];
                        }
                    }
                    else
                    {
                        if (Weapons[currentIndex] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Warbiker";
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Warbiker w/ " + Weapons[currentIndex];
                        }
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("(None)");
                        lbModelSelect.Items.Add("Warbiker");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        break;
                    }

                    isLoading = true;
                    cmbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption1.Items.Clear();
                    if(currentIndex == 0)
                    {
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Big Choppa (+5 pts)",
                            "Choppa",
                            "Power Klaw (+10 pts)",
                            "Slugga"
                        });
                    }
                    else
                    {
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Choppa",
                            "Slugga"
                        });
                    }
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);

                    isLoading = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] == "Big Choppa (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[0] == "Power Klaw (+10 pts)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Warbikers - " + Points + "pts";
        }
    }
}
