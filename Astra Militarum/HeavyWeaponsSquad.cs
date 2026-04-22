using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class HeavyWeaponsSquad : Datasheets
    {
        int currentIndex;
        bool isLoading = false;

        public HeavyWeaponsSquad()
        {
            DEFAULT_POINTS = 55;
            UnitSize = 3;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Heavy Bolter");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM",
                "INFANTRY", "CORE", "PLATOON", "REGIMENTAL", "HEAVY WEAPONS SQUAD"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new HeavyWeaponsSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["nudUnitSize"].Visible = false;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Heavy Weapons Team w/ " + Weapons[i]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Autocannon",
                "Heavy Bolter",
                "Lascannon",
                "Missile Launcher",
                "Mortar"
            });
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (isLoading)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[currentIndex] = "Heavy Weapons Team w/ " + Weapons[currentIndex];
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        break;
                    }
                    else
                    {
                        isLoading = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        isLoading = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Heavy Weapons Squad - " + Points + "pts";
        }
    }
}
