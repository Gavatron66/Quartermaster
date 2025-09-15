using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Grey_Knights
{
    public class BrotherhoodTerminators : Datasheets
    {
        int currentIndex;
        int restriction = 1;
        public BrotherhoodTerminators()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Storm Bolter");
                Weapons.Add("Nemesis Force Sword");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "SANCTIC ASTARTES", "GREY KNGIHTS", "<BROTHERHOOD>",
                "INFANTRY", "CORE", "PSYKER", "TERMINATOR", "PSYK-OUT GRENADES", "BROTHERHOOD TERMINATOR SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new BrotherhoodTerminators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as GreyKnights;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Terminator Justicar w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Terminator w/ " + Weapons[(i * 2)] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Incinerator",
                "Psilencer",
                "Psycannon",
                "Storm Bolter"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Nemesis Daemon Hammer (+10 pts)",
                "Nemesis Force Halberd",
                "Nemesis Force Sword",
                "Nemesis Warding Stave",
                "Two Nemesis Falchions"
            });

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, panel.Controls["cmbOption2"].Location.Y + 60);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[(currentIndex * 2)] = cmbOption1.SelectedItem.ToString();

                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Terminator Justicar w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Terminator w/ " + Weapons[(currentIndex * 2)]
                            + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Terminator Justicar w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Terminator w/ " + Weapons[(currentIndex * 2)]
                            + " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption2.Enabled = true;

                    if(chosenRelic == "Stave of Supremacy")
                    {
                        cmbOption2.SelectedIndex = 3;
                        cmbOption2.Enabled = false;
                    }

                    Relic = chosenRelic;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        for (int i = temp; i < UnitSize; i++)
                        {
                            Weapons.Add("Storm Bolter");
                            Weapons.Add("Nemesis Force Sword");
                            lbModelSelect.Items.Add("Terminator w/ " + Weapons[(currentIndex * 2)]
                                + " and " + Weapons[(currentIndex * 2) + 1]);
                        }
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((currentIndex * 2) + 1, 2);
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
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[(currentIndex * 2)]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                        if (restriction == UnitSize / 5 && Weapons[(currentIndex * 2)] == "Storm Bolter")
                        {
                            cmbOption1.Enabled = false;
                        }

                        if(currentIndex == 0)
                        {
                            cbStratagem5.Visible = true;

                            if (Stratagem.Contains(cbStratagem5.Text))
                            {
                                panel.Controls["lblRelic"].Visible = true;
                                cmbRelic.Visible = true;
                            }

                            if (Relic == "Stave of Supremacy")
                            {
                                cmbOption2.SelectedIndex = 3;
                                cmbOption2.Enabled = false;
                            }
                        }
                        else
                        {
                            cbStratagem5.Visible = false;
                            cmbRelic.Visible = false;
                            panel.Controls["lblRelic"].Visible = false;
                            cmbOption2.Enabled = true;
                        }
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
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            restriction = 0;

            foreach (var weapon in Weapons)
            {
                if (weapon == "Incinerator" || weapon == "Psilencer" || weapon == "Psycannon")
                {
                    restriction++;
                }
                else if (weapon == "Nemesis Daemon Hammer (+10 pts)")
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Brotherhood Terminator Squad - " + Points + "pts";
        }
    }
}