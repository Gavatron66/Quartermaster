using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Termagants : Datasheets
    {
        public Termagants()
        {
            DEFAULT_POINTS = 7;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1m2k";
            Weapons.Add("Fleshborers");
            Weapons.Add(""); //Adrenal Glands
            Weapons.Add(""); //Toxin Sacs
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "CORE", "ENDLESS MULTITUDE", "TERMAGANTS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Termagants();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 30;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Devourers (+1 pts/model)",
                "Fleshborers",
                "Spinefists"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Adrenal Glands (+1 pts/model)";
            if (Weapons[1] != "")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Toxin Sacs (+1 pts/model)";
            if (Weapons[2] != "")
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
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[2] = "";
                    }
                    break;
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] == "Devourers (+1 pts/model)")
            {
                Points += UnitSize;
            }

            if(cbOption1.Checked)
            {
                Points += UnitSize;
            }
            if (cbOption2.Checked)
            {
                Points += UnitSize;
            }
        }

        public override string ToString()
        {
            return "Termagants - " + Points + "pts";
        }
    }
}
