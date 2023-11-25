using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class ScoutBikeSquad : Datasheets
    {
        int currentIndex;
        public ScoutBikeSquad()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 3;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Bolt Pistol");
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Twin Boltgun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "BIKER", "CORE", "SCOUT", "SMOKESCREEN", "SCOUT BIKE SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new ScoutBikeSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Scout Biker Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Scout Biker w/ " + Weapons[i + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Astartes Chainsword",
                "Bolt Pistol",
                "Combi-flamer",
                "Combi-grav",
                "Combi-melta",
                "Combi-plasma",
                "Grav-pistol",
                //Hand Flamer
                //Inferno Pistol
                "Lightning Claw",
                "Plasma Pistol",
                "Power Axe",
                "Power Fist",
                "Power Maul",
                "Power Sword",
                "Storm Bolter",
                "Thunder Hammer"
            });
            if (repo.currentSubFaction == "Deathwatch" || repo.currentSubFaction == "Blood Angels")
            {
                cmbOption1.Items.Insert(7, "Hand Flamer");
                cmbOption1.Items.Insert(8, "Inferno Pistol");
            }

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Astartes Grenade Launcher",
                "Twin Boltgun"
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

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Scout Biker Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    break;
                case 12:
                    Weapons[currentIndex + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Scout Biker Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Scout Biker w/ " + Weapons[currentIndex + 1];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Twin Boltgun");
                        lbModelSelect.Items.Add("Scout Biker w/ " + Weapons[temp + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize - 1, 1);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        break;
                    }

                    if(currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);
                    }
                    else
                    {
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[currentIndex + 1]);
                    }


                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

            foreach (string weapon in Weapons)
            {
                if(weapon == "Grav-pistol" || weapon == "Hand Flamer" || weapon == "Inferno Pistol" || weapon == "Lightning Claw"
                    || weapon == "Plasma Pistol" || weapon == "Power Axe" || weapon == "Power Maul" || weapon == "Power Sword"
                    || weapon == "Storm Bolter")
                {
                    Points += 5;
                }

                if(weapon == "Combi-flamer" || weapon == "Combi-grav" || weapon == "Combi-melta" || weapon == "Combi-plasma"
                    || weapon == "Power Fist")
                {
                    Points += 10;
                }

                if(weapon == "Thunder Hammer")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Scout Bike Squad - " + Points + "pts";
        }
    }
}
