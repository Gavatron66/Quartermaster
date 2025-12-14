using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class ParagonWarsuits : Datasheets
    {
        int currentIndex;

        public ParagonWarsuits()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL3m";
            Weapons.Add("Two Storm Bolters");
            Weapons.Add("Heavy Bolter");
            Weapons.Add("Paragon War Blade");
            Weapons.Add("Two Storm Bolters");
            Weapons.Add("Heavy Bolter");
            Weapons.Add("Paragon War Blade");
            Weapons.Add("Two Storm Bolters");
            Weapons.Add("Heavy Bolter");
            Weapons.Add("Paragon War Blade");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS", "<ORDER>",
                "VEHICLE", "CORE", "CELESTIAN", "PARAGON WARSUITS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new ParagonWarsuits();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdeptaSororitas;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            nudUnitSize.Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Paragon Superior - " + CalcPoints(0) + " pts");
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Paragon - " + CalcPoints(i) + " pts");
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Paragon Grenade Launcher",
                "Two Storm Bolters"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Heavy Bolter",
                "Ministorum Heavy Flamer",
                "Multi-melta (+10 pts)"
            });

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Paragon War Blade",
                "Paragon War Mace"
            });

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, panel.Controls["cmbOption3"].Location.Y + 32);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            antiLoop = true;
            if (Stratagem.Contains(cbStratagem5.Text))
            {
                cbStratagem5.Checked = true;
                cbStratagem5.Enabled = true;

                panel.Controls["lblRelic"].Visible = true;
                cmbRelic.Visible = true;

                if (Relic == "(None)")
                {
                    cmbRelic.SelectedIndex = 0;
                }
                else
                {
                    if (Relic != null && cmbRelic.Items.Contains(Relic))
                    {
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                    }
                    else
                    {
                        cmbRelic.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                cbStratagem5.Checked = false;
                cmbRelic.SelectedIndex = 0;
            }

            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
            antiLoop = false;
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
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 3)] = cmbOption1.SelectedItem.ToString();
                    if(currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Paragon Superior - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Paragon - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 3) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Paragon Superior - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Paragon - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 13:
                    Weapons[(currentIndex * 3) + 2] = cmbOption3.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Paragon Superior - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Paragon - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 17:
                    Relic = cmbRelic.SelectedItem.ToString();
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

                    if(currentIndex == 0)
                    {

                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }
                    }
                    else
                    {
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                    }

                    break;
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        Stratagem.Add(cbStratagem5.Text);
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            Stratagem.Remove(cbStratagem5.Text);
                        }
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.SelectedIndex = 0;
                    }
                    break;
            }

            Points = (DEFAULT_POINTS * UnitSize) + CalcPoints(-1);
        }

        public override string ToString()
        {
            return "Paragon Warsuits - " + Points + "pts";
        }

        private int CalcPoints(int index)
        {
            int points = 0;

            if (index <= -1)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    if (Weapons[i] == "Multi-melta (+10 pts)")
                    {
                        points += 10;
                    }
                }

                return points;
            }

            for (int i = 3 * index; i < 3 * (index + 1); i++)
            {
                if (Weapons[i] == "Multi-melta (+10 pts)")
                {
                    points += 10;
                }
            }

            return 70 + points;
        }
    }
}
