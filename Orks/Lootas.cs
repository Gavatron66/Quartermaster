using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Lootas : Datasheets
    {
        int currentIndex;
        bool isLoading = false;

        public Lootas()
        {
            DEFAULT_POINTS = 14;
            UnitSize = 4;
            Points = UnitSize * DEFAULT_POINTS + 14;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize / 4; i++)
            {
                Weapons.Add("Big Shoota");
            }
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "INFANTRY", "MOB", "CORE", "LOOTAS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Lootas();
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

            panel.Controls["lblExtra1"].Visible = true;
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, lbModelSelect.Location.Y - 15);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 4;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 12;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize / 4; i++)
            {
                lbModelSelect.Items.Add("Spanner w/ " + Weapons[i]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Big Shoota",
                "Kustom Mega-blasta (+5 pts)",
                "Rokkit Launcha (+5 pts)"
            });

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
                    lbModelSelect.Items[currentIndex] = "Spanner w/ " + Weapons[currentIndex];
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    int temp = UnitSize / 4;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize / 4)
                    {
                        Weapons.Add("Big Shoota");
                        lbModelSelect.Items.Add("Spanner w/ " + Weapons[temp]);
                    }

                    if (temp > UnitSize / 4)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize / 4, 1);
                    }

                    panel.Controls["lblExtra1"].Text = "One Spanner is included for every 4 Lootas \n" +
                                                        "True Unit Size: " + (UnitSize + Weapons.Count);
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        break;
                    }
                    else
                    {
                        isLoading = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS * UnitSize;
            if (Factionupgrade == "Smoky Gubbinz (+1 pts/model)")
            {
                Points += repo.GetFactionUpgradePoints((UnitSize + Weapons.Count).ToString());
            }
            else if (Factionupgrade == "Zzapkrumpaz (+2 pts/model)")
            {
                Points += repo.GetFactionUpgradePoints(((UnitSize + Weapons.Count) * 2).ToString());
            }
            else
            {
                Points += repo.GetFactionUpgradePoints(Factionupgrade);
            }

            Points += Weapons.Count * 14;

            foreach (var weapon in Weapons)
            {
                if (weapon != "Big Shoota")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Lootas - " + Points + "pts";
        }
    }
}
