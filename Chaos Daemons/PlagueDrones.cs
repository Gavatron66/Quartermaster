using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Daemons
{
    public class PlagueDrones : Datasheets
    {
        int currentIndex;
        bool instrument;
        bool icon;

        public PlagueDrones()
        {
            DEFAULT_POINTS = 40;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m2k";
            Weapons.Add("Foul Mouthparts");
            Weapons.Add("Foul Mouthparts");
            Weapons.Add("Foul Mouthparts");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "LEGIONES DAEMONICA", "NURGLE",
                "CAVALRY", "DAEMON", "CORE", "FLY", "PLAGUEBEARERS", "PLAGUE DRONES"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new PlagueDrones();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosDaemons;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Plaguebringer w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Plague Drone w/ " + Weapons[i]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new object[]
            {
                "Foul Mouthparts",
                "Prehensile Proboscis"
            });

            cbOption1.Text = "Instrument of Chaos";
            if (instrument)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Daemonic Icon";
            if (icon)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();

                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Plaguebringer w/ " + Weapons[0];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Plague Drone w/ " + Weapons[currentIndex];
                    }
                    break;
                case 21:
                    instrument = cbOption1.Checked;
                    break;
                case 22:
                    icon = cbOption2.Checked;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Foul Mouthparts");
                            lbModelSelect.Items.Add("Plague Drone w/ " + Weapons[i]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((temp - 1), 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }

                    cmbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Plague Drones - " + Points + "pts";
        }
    }
}
