using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class HiveGuard : Datasheets
    {
        int currentIndex;

        public HiveGuard()
        {
            DEFAULT_POINTS = 55;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL1m2k";
            Weapons.Add("");
            Weapons.Add("");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Shockcannon");
            }
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "HIVE GUARD"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new HiveGuard();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Hive Guard w/ " + Weapons[i + 2]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new object[]
            {
                "Impaler Cannon (+10 pts)",
                "Shockcannon"
            });

            cbOption1.Text = "Adrenal Glands (+" + UnitSize * 5 + " pts/unit)";
            if (Weapons[0] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Toxin Sacs (+" + UnitSize * 5 + " pts/unit)";
            if (Weapons[1] == cbOption2.Text)
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
                    Weapons[currentIndex + 2] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Hive Guard w/ " + Weapons[currentIndex + 2];
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
                case 30:
                    int temp = UnitSize;
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Shockcannon");
                            lbModelSelect.Items.Add("Hive Guard w/ " + Weapons[i + 2]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize + 2), 1);
                    }

                    cbOption1.Text = "Adrenal Glands (+" + UnitSize * 5 + " pts/unit)";
                    cbOption2.Text = "Toxin Sacs (+" + UnitSize * 5 + " pts/unit)";
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        break;
                    }
                    else if (currentIndex == -1)
                    {
                        break;
                    }

                    cmbOption1.Visible = true;
                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 2]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Impaler Cannon (+10 pts)")
                {
                    Points += 10;
                }
            }
            if (cbOption1.Checked)
            {
                Points += 5 * UnitSize;
            }
            if (cbOption2.Checked)
            {
                Points += 5 * UnitSize;
            }
        }

        public override string ToString()
        {
            return "Hive Guard - " + Points + "pts";
        }
    }
}
