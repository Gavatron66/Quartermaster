using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class Intercessors : Datasheets
    {
        int currentIndex;
        public Intercessors()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";
            Weapons.Add("Bolt Rifle");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Rifle");
                Weapons.Add("(None)");
            }

            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "INTERCESSORS", "INTERCESSOR SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new Intercessors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cbOption1.Location = new System.Drawing.Point(282, 184);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();

            if (Weapons[1] != "(None)")
            {
                lbModelSelect.Items.Add("Intercessor Sergeant  w/ " + Weapons[0] + " and " + Weapons[1]);
            }
            else
            {
                lbModelSelect.Items.Add("Intercessor Sergeant  w/ " + Weapons[0]);
            }

            for (int i = 1; i < UnitSize; i++)
            {
                if (Weapons[(i * 2) + 1] != "(None)")
                {
                    lbModelSelect.Items.Add("Intercessor w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
                }
                else
                {
                    lbModelSelect.Items.Add("Intercessor w/ " + Weapons[i * 2]);
                }
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Auto Bolt Rifle",
                "Bolt Rifle",
                "Stalker Bolt Rifle"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Astartes Chainsword",
                "Power Fist",
                "Power Sword",
                "Thunder Hammer"
            });

            cbOption1.Text = "Astartes Grenade Launcher";
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
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        if (Weapons[1] != "(None)")
                        {
                            lbModelSelect.Items[0] = "Intercessor Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                        } else
                        {
                            lbModelSelect.Items[0] = "Intercessor Sergeant w/ " + Weapons[0];
                        }
                        break;
                    }

                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Intercessor w/ " + Weapons[currentIndex * 2];
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    if (Weapons[1] != "(None)")
                    {
                        lbModelSelect.Items[0] = "Intercessor Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 2) + 1] = cbOption1.Text;
                        lbModelSelect.Items[currentIndex] = "Intercessor w/ " + Weapons[currentIndex * 2] + " and " +
                        Weapons[(currentIndex * 2) + 1];
                    }
                    else
                    {
                        Weapons[(currentIndex * 2) + 1] = "(None)";
                        lbModelSelect.Items[currentIndex] = "Intercessor w/ " + Weapons[currentIndex * 2];
                    }

                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Bolt Rifle");
                        Weapons.Add("(None)");
                        lbModelSelect.Items.Add("Intercessor w/ Bolt Rifle");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(temp-1 * 2, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        cmbOption1.Items.Insert(0, "Astartes Chainsword");
                        cmbOption1.Items.Insert(3, "Hand Flamer");
                        cmbOption1.Items.Insert(4, "Plasma Pistol");
                        cmbOption1.Items.Insert(5, "Power Sword");

                        break;
                    }

                    if(cmbOption1.Items.Contains("Hand Flamer"))
                    {
                        cmbOption1.Items.RemoveAt(5);
                        cmbOption1.Items.RemoveAt(4);
                        cmbOption1.Items.RemoveAt(3);
                        cmbOption1.Items.RemoveAt(0);
                    }

                    panel.Controls["lblOption1"].Visible = true;
                    cmbOption1.Visible = true;
                    cbOption1.Visible = true;
                    cmbOption2.Visible = false;
                    panel.Controls["lblOption2"].Visible = false;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);

                    if (Weapons[(currentIndex * 2) + 1] != "(None)")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                    }

                    if(UnitSize < 10)
                    {
                        if(Weapons.Contains("Astartes Grenade Launcher"))
                        {
                            cbOption1.Enabled = false;
                        }
                    }

                    if(UnitSize == 10)
                    {
                        int AGL = 0;
                        foreach (string weapon in Weapons)
                        {
                            if(weapon == cbOption1.Text)
                            {
                                AGL++;
                            }
                        }

                        if(AGL == 2)
                        {
                            cbOption1.Enabled = false;
                        }
                    }

                    if (Weapons[(currentIndex * 2) + 1] == cbOption1.Text)
                    {
                        cbOption1.Enabled = true;
                    }

                    break;

            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if(weapon == cbOption1.Text || weapon == "Hand Flamer" || weapon == "Plasma Pistol" || weapon == "Power Sword")
                {
                    Points += 5;
                }
                else if (weapon == "Power Fist")
                {
                    Points += 10;
                }
                else if (weapon == "Thunder Hammer")
                {
                    Points += 20;
                }
            }
        }

        public override string ToString()
        {
            return "Intercessors - " + Points + "pts";
        }
    }
}
