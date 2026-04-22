using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class CelestianSacresants : Datasheets
    {
        int currentIndex;

        public CelestianSacresants()
        {
            DEFAULT_POINTS = 14;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Bolt Pistol");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Hallowed Mace");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS", "<ORDER>",
                "INFANTRY", "CORE", "CELESTIAN", "CELESTIAN SACRESANTS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new CelestianSacresants();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AdeptaSororitas;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Sacresant Superior w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Celestian Sacresant w/ " + Weapons[i + 1]);
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Bolt Pistol",
                "Inferno Pistol (+5 pts)",
                "Ministorum Hand Flamer (+5 pts)",
                "Plasma Pistol (+5 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[0]);
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

            switch (code)
            {
                case 11:
                    Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Sacresant Superior w/" + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Celestian Sacresant w/ " + Weapons[currentIndex + 1];
                    }
                    break;
                case 12:
                    Weapons[0] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Sacresant Superior w/ " + Weapons[0] + " and " + Weapons[1];
                    break;
                case 30:
                    UnitSize = Decimal.ToInt16(nudUnitSize.Value);
                    int temp = Weapons.Count - 2;

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Boltgun");
                            lbModelSelect.Items.Add("Celestian Sacresant w/ " + Weapons[i]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        for (int i = temp; i > UnitSize; i--)
                        {
                            lbModelSelect.Items.RemoveAt(i - 1);
                            Weapons.RemoveAt(i - 1);
                        }
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
                        antiLoop = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    if (currentIndex == 0)
                    {
                        cmbOption1.Enabled = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[0]);

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Hallowed Mace",
                            "Spear of the Faithful (+5 pts)"
                        });
                    }
                    else
                    {
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Anointed Halberd",
                            "Hallowed Mace"
                        });
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);

                    antiLoop = false;
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

            foreach (string item in Weapons)
            {
                if (item == "Inferno Pistol (+5 pts)" || item == "Ministorum Hand Flamer (+5 pts)" || item == "Plasma Pistol (+5 pts)"
                    || item == "Spear of the Faithful (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Celestian Sacresants - " + Points + "pts";
        }
    }
}
