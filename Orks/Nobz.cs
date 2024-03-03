using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Nobz : Datasheets
    {
        int currentIndex = 0;
        bool isLoading = false;

        public Nobz()
        {
            DEFAULT_POINTS = 17;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m2k";
            Weapons.Add(""); //Ammo Runt 1
            Weapons.Add(""); //Ammo Runt 2
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Slugga");
                Weapons.Add("Choppa");
            }
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "INFANTRY", "MOB", "CORE", "NOBZ"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Nobz();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Orks;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 60);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 60);
            panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(panel.Controls["lblFactionUpgrade"].Location.X, panel.Controls["lblFactionUpgrade"].Location.Y + 60);
            cmbFaction.Location = new System.Drawing.Point(cmbFaction.Location.X, cmbFaction.Location.Y + 60);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Boss Nob w/ " + Weapons[2] + " and " + Weapons[3]);
            for (int i = 4; i < Weapons.Count(); i = i + 2)
            {
                lbModelSelect.Items.Add("Nob w/ " + Weapons[i] + " and " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Big Choppa",
                "Choppa",
                "Killsaw (+5 pts)",
                "Kombi-rokkit (+5 pts)",
                "Kombi-skorcha (+5 pts)",
                "Power Klaw (+5 pts)",
                "Power Stabba",
                "Slugga"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Big Choppa",
                "Choppa",
                "Killsaw (+5 pts)",
                "Power Klaw (+5 pts)",
                "Power Stabba",
                "Slugga"
            });

            cbOption1.Text = "Ammo Runt";
            if(Weapons.Contains(cbOption1.Text))
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Ammo Runt (10x models only)";
            if (Weapons.Contains(cbOption2.Text))
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption1.Visible = true; cbOption2.Visible = true;

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
            if (isLoading)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[2] = cmbOption1.SelectedItem.ToString();
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 2] = cmbOption1.SelectedItem.ToString();
                    }

                    if(cmbOption1.SelectedItem.ToString() == "Kombi-rokkit (+5 pts)" || cmbOption1.SelectedItem.ToString() == "Kombi-skorcha (+5 pts)")
                    {
                        cmbOption2.Enabled = false;
                        cmbOption2.Items.Add("");
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("");

                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = "Boss Nob w/ " + Weapons[(currentIndex * 2) + 2];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Nob w/ " + Weapons[(currentIndex * 2) + 2];
                        }
                    }
                    else if (cmbOption2.Items.Contains(""))
                    {
                        cmbOption2.Items.Remove("");
                        cmbOption2.Enabled = true;
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Choppa");

                        if(currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = "Boss Nob w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Nob w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3];
                        }
                    }

                    break;
                case 12:
                    if (currentIndex == 0)
                    {
                        Weapons[3] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Boss Nob w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3];
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 3] = cmbOption2.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Nob w/ " + Weapons[(currentIndex * 2) + 2] + " and " + Weapons[(currentIndex * 2) + 3];
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
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
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[1] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Slugga");
                        Weapons.Add("Choppa");
                        lbModelSelect.Items.Add("Nob w/ " + Weapons[((UnitSize - 1) * 2)] + " and " + Weapons[((UnitSize - 1) * 2) + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2) + 1, 2);
                    }

                    if(UnitSize < 10)
                    {
                        cbOption2.Enabled = false;
                        cbOption2.Checked = false;
                    }
                    else
                    {
                        cbOption2.Enabled = true;
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0 && !isLoading)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    isLoading = true;

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 2]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 3]);

                    if (cmbOption1.SelectedItem.ToString() == "Kombi-rokkit (+5 pts)" || cmbOption1.SelectedItem.ToString() == "Kombi-skorcha (+5 pts)")
                    {
                        cmbOption2.Enabled = false;
                        cmbOption2.Items.Add("");
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("");
                    }
                    else if (cmbOption2.Items.Contains(""))
                    {
                        cmbOption2.Items.Remove("");
                        cmbOption2.Enabled = true;
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf("Choppa");
                    }

                    isLoading = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Killsaw (+5 pts)" || weapon == "Kombi-rokkit (+5 pts)" || weapon == "Kombi-skorcha (+5 pts)"
                    || weapon == "Power Klaw (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Nobz - " + Points + "pts";
        }
    }
}
