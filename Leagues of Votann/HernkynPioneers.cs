using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class HernkynPioneers : Datasheets
    {
        int currentIndex;
        bool scanner = false;
        bool searchlight = false;
        bool comms = false;
        int HYLas_Beamer = 0;

        public HernkynPioneers()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 3;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("(None)");
            }
            Keywords.AddRange(new string[]
            {
                "VOTANN", "<LEAGUE>",
                "BIKER", "CORE", "FLY", "ACCELERATED", "CONCUSSION", "HERNKYN PIONEERS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new HernkynPioneers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as LeaguesOfVotann;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                if (Weapons[i] != "(None)")
                {
                    lbModelSelect.Items.Add("Pioneer w/ " + Weapons[i]);
                }
                else
                {
                    lbModelSelect.Items.Add("Pioneer");
                }
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "HYLas Rotary Cannon (+10 pts)",
                "Ion Beamer (+10 pts)",
                "Multiwave Comms Array (+5 pts)",
                "Pan Spectral Scanner (+5 pts)",
                "Rollbar Searchlight (+5 pts)"
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

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                    }

                    if (Weapons[currentIndex] != "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = "Pioneer w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Pioneer";
                    }

                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("(None)");
                        lbModelSelect.Items.Add("Pioneer");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange(UnitSize, 1);
                    }
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
                        antiLoop = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        LoadRestrict(cmbOption1);
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        antiLoop = false;
                        break;
                    }
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach(var weapon in Weapons)
            {
                if (weapon == "HYLas Rotary Cannon (+10 pts)" || weapon == "Ion Beamer (+10 pts)")
                {
                    Points += 10;
                }
                else if (weapon == "Rollbar Searchlight (+5 pts)" || weapon == "Multiwave Comms Array (+5 pts)" || weapon == "Pan Spectral Scanner (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Hernkyn Pioneers - " + Points + "pts";
        }

        private void LoadRestrict(ComboBox cmbOption1)
        {
            HYLas_Beamer = 0;
            searchlight = false;
            comms = false;
            scanner = false;
            restrictedIndexes = new List<int>();

            foreach(var weapon in Weapons)
            {
                if(weapon == "HYLas Rotary Cannon (+10 pts)" || weapon == "Ion Beamer (+10 pts)")
                {
                    HYLas_Beamer++;
                }
                else if(weapon == "Rollbar Searchlight (+5 pts)")
                {
                    searchlight = true;
                }
                else if(weapon == "Multiwave Comms Array (+5 pts)")
                {
                    comms = true;
                }
                else if(weapon == "Pan Spectral Scanner (+5 pts)")
                {
                    scanner = true;
                }
            }

            if(HYLas_Beamer == UnitSize / 3 && (Weapons[currentIndex] != "HYLas Rotary Cannon (+10 pts)" && Weapons[currentIndex] != "Ion Beamer (+10 pts)"))
            {
                restrictedIndexes.Add(1);
                restrictedIndexes.Add(2);
            }

            if (searchlight && Weapons[currentIndex] != "Rollbar Searchlight (+5 pts)") { restrictedIndexes.Add(5); };
            if (comms && Weapons[currentIndex] != "Multiwave Comms Array (+5 pts)") { restrictedIndexes.Add(3); };
            if (scanner && Weapons[currentIndex] != "Pan Spectral Scanner (+5 pts)") { restrictedIndexes.Add(4); };

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }
    }
}
