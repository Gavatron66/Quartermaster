using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class LokhustDestroyers : Datasheets
    {
        public LokhustDestroyers()
        {
            DEFAULT_POINTS = 40;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1kS(1m)";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "INFANTRY", "FLY", "DESTROYER CULT", "LOKHUST DESTROYERS"
            });
            role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new LokhustDestroyers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            if (Weapons[0] != "")
            {
                nudUnitSize.Maximum++;
                nudUnitSize.Minimum++;
            }
            nudUnitSize.Value = currentSize;

            cbOption1.Text = "Include 1 Lokhust Heavy Destroyer (+45 pts)";
            if (Weapons[0] == "")
            {
                cbOption1.Checked = false;
            }
            else
            {
                cbOption1.Checked = true;
            }

            gbUnitLeader.Text = "Lokhust Heavy Destroyer";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Enmitic Exterminator",
                "Gauss Destructor"
            });
            if (Weapons[0] != "" )
            {
                gbUnitLeader.Enabled= true;
                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
            } 
            else
            {
                gbUnitLeader.Enabled = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;

            switch (code)
            {
                case 21:
                    if(cbOption1.Checked)
                    {
                        nudUnitSize.Maximum = 7;
                        gbUnitLeader.Enabled = true;
                        if(Weapons[0] == "")
                        {
                            nudUnitSize.Value = UnitSize + 1;                        
                            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf("Gauss Destructor");
                        }
                        nudUnitSize.Minimum = 2;
                    }
                    else
                    {
                        gbUnitLeader.Enabled = false;
                        Weapons[0] = "";
                        gb_cmbOption1.SelectedIndex = -1;
                        if(UnitSize != 1)
                        {
                            nudUnitSize.Minimum = 1;
                            nudUnitSize.Value = (Decimal)(UnitSize - 1);
                            nudUnitSize.Maximum = 6;
                        }
                    }
                    break;
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
                case 411:
                    if (gb_cmbOption1.SelectedIndex != -1)
                    {
                        Weapons[0] = gb_cmbOption1.SelectedItem.ToString();
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (cbOption1.Checked)
            {
                Points += 5;
            }
            
        }

        public override string ToString()
        {
            return "Lokhust Destroyers - " + Points + "pts";
        }
    }
}
