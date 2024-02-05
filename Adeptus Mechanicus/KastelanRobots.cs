using Roster_Builder.Genestealer_Cults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class KastelanRobots : Datasheets
    {
        int currentIndex;
        public KastelanRobots()
        {
            DEFAULT_POINTS = 85;
            UnitSize = 2;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL3m";
            for(int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Kastelan Fist");
                Weapons.Add("Kastelan Phosphor Blaster");
                Weapons.Add("Incendine Combustor");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "CULT MECHANICUS", "<FORGE WORLD>",
                "VEHICLE", "KASTELAN ROBOTS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new KastelanRobots();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdMech;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 2;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Kastelan Robot w/ " + Weapons[i * 3] + ", " + Weapons[(i * 3) + 1] + " and " + Weapons[(i * 3) + 2]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Kastelan Fist",
                "Kastelan Phosphor Blaster"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Kastelan Fist",
                "Kastelan Phosphor Blaster"
            });

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Heavy Phosphor Blaster",
                "Incendine Combustor"
            });
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
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 3] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Kastelan Robot w / " + Weapons[currentIndex * 3] + ", " + Weapons[(currentIndex * 3) + 1] + " and " + Weapons[(currentIndex * 3) + 2];
                    break;
                case 12:
                    Weapons[(currentIndex * 3) + 1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Kastelan Robot w / " + Weapons[currentIndex * 3] + ", " + Weapons[(currentIndex * 3) + 1] + " and " + Weapons[(currentIndex * 3) + 2];
                    break;
                case 13:
                    Weapons[(currentIndex * 3) + 2] = cmbOption3.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Kastelan Robot w / " + Weapons[currentIndex * 3] + ", " + Weapons[(currentIndex * 3) + 1] + " and " + Weapons[(currentIndex * 3) + 2];
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Kastelan Fist");
                        Weapons.Add("Kastelan Phosphor Blaster");
                        Weapons.Add("Incendine Combustor");
                        lbModelSelect.Items.Add("Kastelan Robot w/ " + Weapons[temp * 3] + ", " + Weapons[(temp * 3) + 1] + " and " + Weapons[(temp * 3) + 2]);
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
                        cmbOption3.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    cmbOption3.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption3"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 3]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 3) + 1]);
                    cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 3) + 2]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Kastelan Robots - " + Points + "pts";
        }
    }
}
