using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class TyranidWarriors : Datasheets
    {
        int currentIndex;
        int venomcannons;
        int stranglers;

        public TyranidWarriors()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m3k";
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Bonesword and Lash Whip (+5 pts)");
                Weapons.Add("Devourer");
            }
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "SYNAPSE", "CORE", "TYRANID WARRIORS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new TyranidWarriors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 50);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 45);
            cbOption3.Location = new System.Drawing.Point(cbOption3.Location.X, cbOption3.Location.Y + 45);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Tyranid Warrior w/ " + Weapons[(i * 2) + 3] + " and " + Weapons[(i * 2) + 4]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new object[]
            {
                "Bonesword and Lash Whip (+5 pts)",
                "Dual Boneswords (+10 pts)",
                "Two Rending Claws",
                "Two Scything Talons"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new object[]
            {
                "Barbed Strangler (+10 pts)",
                "Bonesword and Lash Whip (+5 pts)",
                "Deathspitter (+5 pts)",
                "Devourer",
                "Dual Boneswords (+10 pts)",
                "Spinefists",
                "Two Rending Claws",
                "Two Scything Talons",
                "Venom Cannon (+5 pts)"
            });

            cbOption1.Text = "Adrenal Glands (+15 pts)";
            if (Weapons[0] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Flesh Hooks (+5 pts)";
            if (Weapons[1] == cbOption2.Text)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Toxin Sacs (+10 pts)";
            if (Weapons[2] == cbOption3.Text)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }

        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 2) + 3] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Tyranid Warrior w/ " + Weapons[(currentIndex * 2) + 3] + " and " + Weapons[(currentIndex * 2) + 4];
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 4] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Tyranid Warrior w/ " + Weapons[(currentIndex * 2) + 3] + " and " + Weapons[(currentIndex * 2) + 4];
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
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[2] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Bonesword and Lash Whip (+5 pts)");
                            Weapons.Add("Devourer");
                            lbModelSelect.Items.Add("Tyranid Warrior w/ " + Weapons[(i * 2) + 3] + " and " + Weapons[(i * 2) + 4]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2) + 1, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    if(stranglers == UnitSize/3 && Weapons[(currentIndex * 2) + 4] != "Barbed Strangler (+10 pts)")
                    {
                        if(cmbOption2.Items.Contains("Barbed Strangler (+10 pts)"))
                        {
                            cmbOption2.Items.RemoveAt(0);
                        }
                    }
                    else if (!cmbOption2.Items.Contains("Barbed Strangler (+10 pts)"))
                    {
                        cmbOption2.Items.Insert(0, "Barbed Strangler (+10 pts)");
                    }

                    if (venomcannons == UnitSize / 3 && Weapons[(currentIndex * 2) + 4] != "Venom Cannon (+5 pts)")
                    {
                        if (cmbOption2.Items.Contains("Venom Cannon (+5 pts)"))
                        {
                            cmbOption2.Items.Remove("Venom Cannon (+5 pts)");
                        }
                    }
                    else if (!cmbOption2.Items.Contains("Venom Cannon (+5 pts)"))
                    {
                        cmbOption2.Items.Add("Venom Cannon (+5 pts)");
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2) + 3]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 4]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            stranglers = 0;
            venomcannons = 0;
            foreach (var weapon in Weapons)
            {
                if(weapon == "Barbed Strangler (+10 pts)")
                {
                    stranglers++;
                    Points += 10;
                }

                if(weapon == "Venom Cannon (+5 pts)")
                {
                    venomcannons++;
                    Points += 5;
                }

                if(weapon == "Bonesword and Lash Whip (+5 pts)" || weapon == "Deathspitter (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Dual Boneswords (+10 pts)")
                {
                    Points += 10;
                }
            }

            if(cbOption1.Checked)
            {
                Points += 15;
            }
            if (cbOption2.Checked)
            {
                Points += 5;
            }
            if (cbOption3.Checked)
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Tyranid Warriors - " + Points + "pts";
        }
    }
}
