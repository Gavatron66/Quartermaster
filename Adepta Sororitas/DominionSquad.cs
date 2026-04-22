using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class DominionSquad : Datasheets
    {
        int currentIndex = 0;
        int stdIndex = -1;
        bool isLoading = false;
        int restrictArray = 0;

        public DominionSquad()
        {
            DEFAULT_POINTS = 14;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m2k";
            Weapons.Add("Boltgun and Bolt Pistol");
            Weapons.Add("(None)");
            Weapons.Add(""); //Incensor Cherub (+5 pts)
            Weapons.Add(""); //Simulacrum Imperialis (+5 pts)
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS", "<ORDER>",
                "INFANTRY", "CORE", "DOMINION SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new DominionSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdeptaSororitas;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 60);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 60);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Dominion Superior with " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Dominion Superior with " + Weapons[0] + " and " + Weapons[1]);
            }
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Dominion with " + Weapons[i + 3]);
            }

            cmbOption1.Items.Clear();
            cmbOption2.Items.Clear();

            cbOption1.Text = "Incensor Cherub (+5 pts)";
            cbOption2.Text = "Simulacrum Imperialis (+5 pts)";
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

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Dominion Superior w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Dominion Superior w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                    }
                    else
                    {
                        Weapons[currentIndex + 3] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Dominion with " + Weapons[currentIndex + 3];
                    }
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    if (Weapons[1] == "(None)")
                    {
                        lbModelSelect.Items[0] = "Dominion Superior w/ " + Weapons[0];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Dominion Superior w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[3] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                        stdIndex = currentIndex;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        Weapons[2] = "";
                        stdIndex = -1;
                        cmbOption1.Enabled = true;
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Boltgun");
                        lbModelSelect.Items.Add("Dominion with " + Weapons[temp + 3]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize + 3, 1);
                    }

                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0 && !isLoading)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    isLoading = true;

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbOption1.Visible = true;
                        cbOption2.Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Boltgun and Bolt Pistol",
                            "Combi-melta and Bolt Pistol (+10 pts)",
                            "Combi-plasma and Bolt Pistol (+10 pts)",
                            "Condemnor Boltgun and Bolt Pistol (+10 pts)",
                            "Inferno Pistol (+5 pts)",
                            "Ministorum Combi-flamer and Bolt Pistol (+10 pts)",
                            "Ministorum Hand Flamer (+5 pts)",
                            "Plasma Pistol (+5 pts)"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Chainsword",
                            "Power Maul (+5 pts)",
                            "Power Sword (+5 pts)"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);
                        isLoading = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption1.Enabled = true;
                    cmbOption2.Visible = false;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = false;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;

                    cmbOption1.Items.Clear();
                    cmbOption1.Items.AddRange(new string[]
                    {
                        "Artificer-crafted Storm Bolter (+5 pts)", //s
                        "Boltgun",
                        "Meltagun (+10 pts)", //s
                        "Ministorum Flamer (+5 pts)", //s
                    });
                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 3]);

                    if (restrictArray == 4 && Weapons[currentIndex + 3] == "Boltgun")
                    {
                        cmbOption1.Items.Remove("Artificer-crafted Storm Bolter (+5 pts)");
                        cmbOption1.Items.Remove("Meltagun (+10 pts)");
                        cmbOption1.Items.Remove("Ministorum Flamer (+5 pts)");
                    }

                    if (Weapons[currentIndex + 3] == "Boltgun" && (stdIndex == -1 || stdIndex == currentIndex))
                    {
                        cbOption2.Enabled = true;
                    }
                    else
                    {
                        cbOption2.Enabled = false;
                    }

                    if (stdIndex == currentIndex)
                    {
                        cmbOption1.Enabled = false;
                    }

                    isLoading = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            restrictArray = 0; //Special

            foreach (var weapon in Weapons)
            {
                if (weapon == "Artificer-crafted Storm Bolter (+5 pts)" || weapon == "Meltagun (+10 pts)" || weapon == "Ministorum Flamer (+5 pts)")
                {
                    restrictArray++;
                }

                if (weapon == "Artificer-crafted Storm Bolter (+5 pts)" || weapon == "Incensor Cherub (+5 pts)" || weapon == "Inferno Pistol (+5 pts)"
                    || weapon == "Ministorum Flamer (+5 pts)" || weapon == "Ministorum Hand Flamer (+5 pts)" || weapon == "Plasma Pistol (+5 pts)"
                    || weapon == "Power Maul (+5 pts)" || weapon == "Power Sword (+5 pts)" || weapon == "Simulacrum Imperialis (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon.Contains("Combi-melta") || weapon.Contains("Combi-plasma") || weapon.Contains("Condemnor Boltgun")
                    || weapon == "Meltagun (+10 pts)" || weapon.Contains("Ministorum Combi-flamer"))
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Dominion Squad - " + Points + "pts";
        }
    }
}
