using Roster_Builder.Necrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class AtalanJackals : Datasheets
    {
        int currentIndex;
        int wolfquads = 0;
        List<string> wolfquadWeapons = new List<string>();

        public AtalanJackals()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 4;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL3k1m";
            
            for(int i = 0; i < UnitSize; i++)
            {
                Weapons.Add(""); // Atalan Power Weapon
                Weapons.Add(""); // Demolition Charge
                Weapons.Add(""); // Grenade Launcher
            }

            Keywords.AddRange(new string[]
            {
                "TYRANIDS", "GENESTEALER CULTS", "<CULT>",
                "BIKER", "CORE", "CROSSFIRE", "ATALAN", "JACKALS"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new AtalanJackals();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudUnitSize2 = panel.Controls["nudUnitSize2"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86 , 18);
            nudUnitSize.Location = new System.Drawing.Point(243, 16);
            panel.Controls["lblUnitSize2"].Location = new System.Drawing.Point(121, 50);
            nudUnitSize2.Location = new System.Drawing.Point(278, 48);
            panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 130);
            cmbOption1.Location = new System.Drawing.Point(243, 153);

            panel.Controls["lblNumModels"].Visible = true;
            nudUnitSize.Visible = true;
            panel.Controls["lblUnitSize2"].Visible = true;
            nudUnitSize2.Visible = true;

            panel.Controls["lblUnitSize2"].Text = "Number of Wolfquads: ";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 4;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 8;
            nudUnitSize.Value = currentSize;

            nudUnitSize2.Minimum = 0;
            antiLoop = true;
            nudUnitSize2.Value = nudUnitSize.Minimum;
            nudUnitSize2.Maximum = 2;
            nudUnitSize2.Value = wolfquads;
            antiLoop = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Atalan Leader");
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Atalan Jackal");
            }

            cbOption1.Text = "Atalan Power Weapon";
            cbOption2.Text = "Demolition Charge";
            cbOption3.Text = "Grenade Launcher";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Atalan Incinerator",
                "Heavy Stubber",
                "Mining Laser"
            });

            cmbFactionupgrade.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            cmbFactionupgrade.Items.Clear();
            cmbFactionupgrade.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFactionupgrade.SelectedIndex = cmbFactionupgrade.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFactionupgrade.SelectedIndex = 0;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudUnitSize2 = panel.Controls["nudUnitSize2"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    wolfquadWeapons[currentIndex - UnitSize] = cmbOption1.SelectedItem.ToString();
                    break;
                case 16:
                    Factionupgrade = cmbFactionupgrade.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[currentIndex * 3] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[currentIndex * 3] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 3) + 1] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 1] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[(currentIndex * 3) + 2] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 2] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        lbModelSelect.Items.Insert(UnitSize - 1 - wolfquads, "Atalan Jackal");
                        Weapons.Add("");
                        Weapons.Add("");
                        Weapons.Add("");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 3) - 1, 3);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex >= UnitSize)
                    {

                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;

                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(wolfquadWeapons[currentIndex - UnitSize]);
                        break;
                    }
                    else
                    {
                        panel.Controls["lblOption1"].Visible = false;
                        cmbOption1.Visible = false;
                    }

                    if (currentIndex < 0)
                    {
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        break;
                    }

                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;
                    cbOption3.Enabled = true;

                    if (Weapons[(currentIndex * 3)] == "")
                    {
                        cbOption1.Checked = false;
                    }
                    else
                    {
                        cbOption1.Checked = true;
                    }

                    if (Weapons[(currentIndex * 3) + 1] == "")
                    {
                        cbOption2.Checked = false;
                    }
                    else
                    {
                        cbOption2.Checked = true;
                    }

                    if (Weapons[(currentIndex * 3) + 2] == "")
                    {
                        cbOption3.Checked = false;
                    }
                    else
                    {
                        cbOption3.Checked = true;
                    }

                    int grenadecheck = 0;
                    foreach (var weapon in Weapons)
                    {
                        if (weapon == "Grenade Launcher")
                        {
                            grenadecheck++;
                        }
                    }

                    if ((UnitSize == 4 && grenadecheck == 1) || (UnitSize > 4 && grenadecheck == 2))
                    {
                        cbOption3.Enabled = false;
                    }

                    if (Weapons[(currentIndex * 3) + 2] == "Grenade Launcher")
                    {
                        cbOption3.Enabled = true;
                    }

                    break;
                case 62:
                    int temp2 = wolfquads;
                    wolfquads = int.Parse(nudUnitSize2.Value.ToString());

                    if (temp2 < wolfquads)
                    {
                        lbModelSelect.Items.Add("Atalan Wolfquad");
                        wolfquadWeapons.Add("Heavy Stubber");
                    }

                    if (temp2 > wolfquads)
                    {
                        lbModelSelect.Items.Remove("Atalan Wolfquad");
                        wolfquadWeapons.RemoveAt(temp2 - 1);
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);
            Points += wolfquads * 24;

            foreach (var item in Weapons)
            {
                if(item == "Atalan Power Weapon")
                {
                    Points += 3;
                }
                if (item == "Demolition Charge")
                {
                    Points += 5;
                }
                if (item == "Grenade Launcher")
                {
                    Points += 5;
                }
            }

            foreach (var item in wolfquadWeapons)
            {
                if(item == "Atalan Incinerator")
                {
                    Points += 10;
                }

                if(item == "Mining Laser")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Atalan Jackals - " + Points + "pts";
        }
    }
}
