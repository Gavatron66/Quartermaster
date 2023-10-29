using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class AssaultTerminators : Datasheets
    {
        int currentIndex = 0;

        public AssaultTerminators()
        {
            DEFAULT_POINTS = 33;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m1k";
            Weapons.Add("");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Thunder Hammer and Storm Shield");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "TERMINATOR", "TERMINATOR ASSAULT SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new AssaultTerminators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Assault Terminator Sergeant w/ " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Assault Terminator w/ " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "2x Lightning Claws",
                "Thunder Hammer and Storm Shield"
            });

            cbOption1.Text = "Teleport Homer";
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Assault Terminator Sergeant w/ " + Weapons[currentIndex + 1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Assault Terminator w/ " + Weapons[currentIndex + 1];
                    }
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        Weapons[0] = "Teleport Homer";
                    }
                    else
                    {
                        Weapons[0] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Thunder Hammer and Storm Shield");
                            lbModelSelect.Items.Add("Assault Terminator w/ " + Weapons[currentIndex + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(currentIndex + 1, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                        if (Weapons[0] == "")
                        {
                            cbOption1.Checked = false;
                        }
                        else
                        {
                            cbOption1.Checked = true;
                        }
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var item in Weapons)
            {
                if (item == "Thunder Hammer and Storm Shield")
                {
                    Points += 10;
                }
            }

            if(Weapons.Contains("Teleport Homer"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Terminator Assault Squad - " + Points + "pts";
        }
    }
}
