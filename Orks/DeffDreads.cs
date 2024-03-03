using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class DeffDreads : Datasheets
    {
        int currentIndex;

        public DeffDreads()
        {
            DEFAULT_POINTS = 85;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL4m";
            Weapons.Add("Big Shoota");
            Weapons.Add("Big Shoota");
            Weapons.Add("Dread Klaw");
            Weapons.Add("Dread Klaw");
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "VEHICLE", "WALKERZ", "DEFF DREADS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new DeffDreads();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Orks;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

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
                lbModelSelect.Items.Add("Deff Dread - " + CalcPoints(i) + " pts");
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Big Shoota",
                "Dread Klaw",
                "Kustom Mega-blasta (+10 pts)",
                "Rokkit Launcha (+10 pts)",
                "Skorcha (+5 pts)"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Big Shoota",
                "Dread Klaw",
                "Kustom Mega-blasta (+10 pts)",
                "Rokkit Launcha (+10 pts)",
                "Skorcha (+5 pts)"
            });

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Big Shoota",
                "Dread Klaw",
                "Kustom Mega-blasta (+10 pts)",
                "Rokkit Launcha (+10 pts)",
                "Skorcha (+5 pts)"
            });

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "Big Shoota",
                "Dread Klaw",
                "Kustom Mega-blasta (+10 pts)",
                "Rokkit Launcha (+10 pts)",
                "Skorcha (+5 pts)"
            });

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
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 4)] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = ("Deff Dread - " + CalcPoints(currentIndex) + " pts");
                    break;
                case 12:
                    Weapons[(currentIndex * 4) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = ("Deff Dread - " + CalcPoints(currentIndex) + " pts");
                    break;
                case 13:
                    Weapons[(currentIndex * 4) + 2] = cmbOption3.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = ("Deff Dread - " + CalcPoints(currentIndex) + " pts");
                    break;
                case 14:
                    Weapons[(currentIndex * 4) + 3] = cmbOption4.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = ("Deff Dread - " + CalcPoints(currentIndex) + " pts");
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Big Shoot");
                        Weapons.Add("Big Shoot");
                        Weapons.Add("Dread Klaw");
                        Weapons.Add("Dread Klaw");
                        lbModelSelect.Items.Add("Deff Dread - " + CalcPoints(UnitSize - 1) + " pts");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 4) - 1, 4);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cmbOption3.Visible = false;
                        cmbOption4.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        panel.Controls["lblOption4"].Visible = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    cmbOption3.Visible = true;
                    cmbOption4.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption4"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 4]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 4) + 1]);
                    cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 4) + 2]);
                    cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);

                    break;
            }

            Points = (DEFAULT_POINTS * UnitSize) + CalcPoints(-1);

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Deff Dreads - " + Points + "pts";
        }
        private int CalcPoints(int index)
        {
            int points = 0;

            if (index <= -1)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    if (Weapons[i] == "Kustom Mega-blasta (+10 pts)")
                    {
                        points += 10;
                    }
                    else if (Weapons[i] == "Skorcha (+5 pts)")
                    {
                        points += 5;
                    }
                    else if (Weapons[i] == "Rokkit Launcha (+10 pts)")
                    {
                        points += 10;
                    }
                }

                return points;
            }

            for (int i = 4 * index; i < 4 * (index + 1); i++)
            {
                if (Weapons[i] == "Kustom Mega-blasta (+10 pts)")
                {
                    points += 10;
                }
                else if (Weapons[i] == "Skorcha (+5 pts)")
                {
                    points += 5;
                }
                else if (Weapons[i] == "Rokkit Launcha (+10 pts)")
                {
                    points += 10;
                }
            }

            return 85 + points;
        }
    }
}
