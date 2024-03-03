using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Kommandos : Datasheets
    {
        int currentIndex;

        public Kommandos()
        {
            DEFAULT_POINTS = 11;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m2k";
            Weapons.Add("");
            Weapons.Add("");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Slugga and Choppa");
            }
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "INFANTRY", "MOB", "CORE", "TANKBUSTA BOMBS", "KOMMANDOS"
            });
            Role = "Elites";

            antiLoop = false;
        }

        public override Datasheets CreateUnit()
        {
            return new Kommandos();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Orks;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 15;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Boss Nob w/ " + Weapons[2]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Kommando w/ " + Weapons[i + 2]);
            }

            cmbOption1.Items.Clear();

            cbOption1.Text = "Bomb Squig (+5 pts)";
            if (Weapons[0] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }
            cbOption1.Visible = true;

            cbOption2.Text = "Distraction Grot (+10 pts)";
            if (Weapons[1] == cbOption2.Text)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }
            cbOption2.Visible = true;

            lblExtra1.Text = "More options are unlocked at Unit Size 10";
            lblExtra1.Location = new System.Drawing.Point(lbModelSelect.Location.X + 80, lbModelSelect.Location.Y - 25);

            if(UnitSize < 10)
            {
                cbOption1.Enabled = false;
                cbOption2.Enabled = false;
                lblExtra1.Visible = true;
            }
            else
            {
                cbOption1.Enabled = true;
                cbOption2.Enabled = true;
                lblExtra1.Visible = false;
            }

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
            if(antiLoop)
            {
                return;
            }

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Boss Nob w/ " + Weapons[currentIndex + 2];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Kommando w/ " + Weapons[currentIndex + 2];
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
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Slugga and Choppa");
                            lbModelSelect.Items.Add("Kommando w/ " + Weapons[i + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize + 2), 1);
                    }

                    if (UnitSize < 10)
                    {
                        cbOption1.Enabled = false;
                        cbOption2.Enabled = false;
                        cbOption1.Checked = false;
                        cbOption2.Checked = false;
                        lblExtra1.Visible = true;
                    }
                    else
                    {
                        cbOption1.Enabled = true;
                        cbOption2.Enabled = true;
                        lblExtra1.Visible = false;
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex == -1)
                    {
                        antiLoop = false;
                        break;
                    }
                    else if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption1.Items.Clear();
                    if(currentIndex == 0)
                    {
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Slugga and Big Choppa (+5 pts)",
                            "Slugga and Choppa",
                            "Slugga and Power Klaw (+5 pts)"
                        });
                    }
                    else
                    {
                        cmbOption1.Items.AddRange(weaponsCheck());
                    }
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if(cbOption1.Checked)
            {
                Points += 5;
            }
            if (cbOption2.Checked)
            {
                Points += 10;
            }

            foreach (var weapon in Weapons)
            {
                if(weapon == "Big Choppa (+5 pts)" || weapon == "Big Shoota (+5 pts)" || weapon == "Breacha Ram (+5 pts)"
                    || weapon == "Burna (+5 pts)" || weapon == "Kustom Shoota (+5 pts)" || weapon == "Slugga and Power Klaw (+5 pts)"
                    || weapon == "Slugga and Big Choppa (+5 pts)" || weapon == "Shokka Pistol and Choppa (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Rokkit Launcha (+10 pts)")
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Kommandos - " + Points + "pts";
        }

        public string[] weaponsCheck()
        {
            List<string> weapons = new List<string>
            {
                "Big Shoota (+5 pts)",
                "Breacha Ram (+5 pts)",
                "Burna (+5 pts)",
                "Kustom Shoota (+5 pts)",
                "Rokkit Launcha (+10 pts)",
                "Shokka Pistol and Choppa (+5 pts)",
                "Slugga and Choppa"
            };

            if(UnitSize < 10)
            {
                return new string[] { "Slugga and Choppa" };
            }

            if(Weapons.Contains("Big Shoota (+5 pts)") && Weapons[currentIndex + 2] != "Big Shoota (+5 pts)")
            {
                weapons.Remove("Big Shoota (+5 pts)");
            }

            if (Weapons.Contains("Breacha Ram (+5 pts)") && Weapons[currentIndex + 2] != "Breacha Ram (+5 pts)")
            {
                weapons.Remove("Breacha Ram (+5 pts)");
            }

            if (Weapons.Contains("Shokka Pistol and Choppa (+5 pts)") && Weapons[currentIndex + 2] != "Shokka Pistol and Choppa (+5 pts)")
            {
                weapons.Remove("Shokka Pistol and Choppa (+5 pts)");
            }

            if (Weapons.Contains("Kustom Shoota (+5 pts)") && Weapons[currentIndex + 2] != "Kustom Shoota (+5 pts)")
            {
                weapons.Remove("Kustom Shoota (+5 pts)");
            }

            if (Weapons.Contains("Rokkit Launcha (+10 pts)") && Weapons[currentIndex + 2] != "Rokkit Launcha (+10 pts)")
            {
                weapons.Remove("Rokkit Launcha (+10 pts)");
            }

            if (Weapons.Contains("Burna (+5 pts)") && Weapons[currentIndex + 2] != "Burna (+5 pts)")
            {
                weapons.Remove("Burna (+5 pts)");
            }

            return weapons.ToArray();
        }
    }
}
