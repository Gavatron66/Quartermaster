using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class TombBlades : Datasheets
    {
        int currentIndex;
        public TombBlades()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m1k";

            for(int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Twin Gauss Blaster (+5 pts)"); // Twin Gauss Blaster (+5 pts), Particle Beamer or Twin Tesla Carbine (+5 pts)
                Weapons.Add(""); // Shieldvanes (+3 pts)
                Weapons.Add("(None)"); // Nebuloscope (+3 pts), Shadowloom (+3 pts) or (None)
            }

            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "BIKER", "FLY", "CORE", "TOMB BLADES"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new TombBlades();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Necrons;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Tomb Blade w/ " + Weapons[i * 3]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Particle Beamer",
                "Twin Gauss Blaster (+5 pts)",
                "Twin Tesla Carbine (+5 pts)"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Nebuloscope (+3 pts)",
                "Shadowloom (+3 pts)"
            });

            cbOption1.Text = "Shieldvanes (+3 pts)";
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
                    Weapons[currentIndex * 3] = cmbOption1.SelectedItem.ToString();

                    lbModelSelect.Items[currentIndex] = "Tomb Blade w/ " + Weapons[currentIndex * 3];
                    break;
                case 12:
                    Weapons[(currentIndex * 3) + 2] = cmbOption2.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 3) + 1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 3) + 1] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Twin Gauss Blaster (+5 pts)");
                        Weapons.Add("");
                        Weapons.Add("(None)");
                        lbModelSelect.Items.Add("Tomb Blade w/ " + Weapons[temp * 3]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 3) - 1, 3);
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

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    cbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 3) + 2]);

                    if (Weapons[(currentIndex * 3) + 1] == "")
                    {
                        cbOption1.Checked = false;
                    }
                    else
                    {
                        cbOption1.Checked = true;
                    }

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Nebuloscope (+3 pts)" || weapon == "Shieldvanes (+3 pts)")
                {
                    Points += 3;
                }

                if(weapon == "Shadowloom (+3 pts)" || weapon == "Twin Gauss Blaster (+5 pts)" || weapon == "Twin Tesla Carbine (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Tomb Blades - " + Points + "pts";
        }
    }
}
