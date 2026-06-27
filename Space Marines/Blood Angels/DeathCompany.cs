using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Blood_Angels
{
    public class DeathCompany : Datasheets
    {
        int currentIndex;
        bool jumpPacks;

        public DeathCompany()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m2k";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Bolt Pistol");
                Weapons.Add("Astartes Chainsword");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "BLOOD ANGELS",
                "INFANTRY", "CORE", "DEATH COMPANY", "DEATH COMPANY MARINES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathCompany();
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
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cbOption1.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y + 60);
            cbOption2.Location = new System.Drawing.Point(cbOption2.Location.X, cbOption2.Location.Y + 60);

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Death Company Marine w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Bolt Pistol",
                "Hand Flamer",
                "Inferno Pistol",
                "Plasma Pistol"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Power Axe",
                "Power Fist (+5 pts)",
                "Power Maul",
                "Power Sword"
            });

            cbOption1.Text = "Replace both weapons with a Boltgun and Thunder Hammer (+10 pts)";
            cbOption2.Text = "Jump Packs (+3 pts/model) (All Models)";

            cbOption2.Checked = jumpPacks;
            cbOption2.Visible = true;
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
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Death Company Marine w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Death Company Marine w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[currentIndex * 2] = "Boltgun and Thunder Hammer (+10 pts)";
                        Weapons[(currentIndex * 2) + 1] = "";
                        lbModelSelect.Items[currentIndex] = "Death Company Marine w/ " + Weapons[currentIndex * 2];

                        cmbOption1.Enabled = false;
                        cmbOption2.Enabled = false;
                    }
                    else
                    {
                        Weapons[currentIndex * 2] = "Bolt Pistol";
                        Weapons[(currentIndex * 2) + 1] = "Astartes Chainsword";
                        lbModelSelect.Items[currentIndex] = "Death Company Marine w/ " + Weapons[currentIndex * 2] + " and " + Weapons[(currentIndex * 2) + 1];

                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                    }
                    break;
                case 22:
                    jumpPacks = cbOption2.Checked;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Bolt Pistol");
                        Weapons.Add("Astartes Chainsword");
                        lbModelSelect.Items.Add("Death Company Marine w/ " + Weapons[((UnitSize - 1) * 2)] + " and " + Weapons[((UnitSize - 1) * 2) + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(((UnitSize - 1) * 2) + 1, 2);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbOption1.Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;
                        cbOption1.Visible = true;

                        if (Weapons[(currentIndex * 2) + 1] == "")
                        {
                            cbOption1.Checked = true;
                            cmbOption1.Enabled = false;
                            cmbOption1.SelectedIndex = -1;
                            cmbOption2.Enabled = false;
                            cmbOption2.SelectedIndex = -1;
                        }
                        else
                        {
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);
                            cbOption1.Checked = false;
                        }
                    }

                    antiLoop = false;
                    break;
                default: break;
            }

            Points = (DEFAULT_POINTS * UnitSize) + (jumpPacks ? 3 * UnitSize : 0);

            foreach(var weapon in Weapons)
            {
                if(weapon == "Boltgun and Thunder Hammer (+10 pts)")
                {
                    Points += 10;
                }
                else if(weapon == "Power Fist (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Death Company Marines - " + Points + "pts";
        }
    }
}