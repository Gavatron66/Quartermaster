using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class Seraphim : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int restriction;

        public Seraphim()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL1m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Two Bolt Pistols");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS", "<ORDER>",
                "INFANTRY", "CORE", "JUMP PACK", "FLY", "SERAPHIM SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Seraphim();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AdeptaSororitas;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Seraphim Superior w/ " + Weapons[0]);

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Seraphim w/ " + Weapons[i]);
            }

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, panel.Controls["cmbOption1"].Location.Y + 32);
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
                panel.Controls["lblRelic"].Visible = false;
                cmbRelic.Visible = false;
            }

            antiLoop = false;
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
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if(!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[0] = "Seraphim Superior w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Seraphim w/ " + Weapons[currentIndex];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    restrictedIndexes.Clear();

                    if (chosenRelic == "The Ecclesiarch's Fury")
                    {
                        cmbOption1.SelectedIndex = 0;
                        restrictedIndexes.AddRange(new int[] { 1, 2, 4, 5 });
                    }
                    else if (chosenRelic == "Redemption")
                    {
                        cmbOption1.SelectedIndex = 1;
                        restrictedIndexes.AddRange(new int[] { 0, 2, 5 });
                    }

                    this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Two Bolt Pistols");
                        lbModelSelect.Items.Add("Seraphim w/ " + Weapons[temp - 1]);
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
                        cbStratagem5.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.Visible = false;
                        break;
                    }

                    isLoading = true;
                    cmbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;

                    cmbOption1.Items.Clear();
                    if (currentIndex == 0)
                    {
                        cmbOption1.Enabled = true;
                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Bolt Pistol and Chainsword",
                            "Bolt Pistol and Plasma Pistol (+5 pts)",
                            "Bolt Pistol and Power Sword (+5 pts)",
                            "Plasma Pistol and Chainsword (+5 pts)",
                            "Plasma Pistol and Power Sword (+10 pts)",
                            "Two Bolt Pistols"
                        });

                        restrictedIndexes.Clear();

                        if (Relic == "The Ecclesiarch's Fury")
                        {
                            restrictedIndexes.AddRange(new int[] { 1, 2, 4, 5 });
                            cmbOption1.SelectedIndex = 0;
                        }
                        else if (Relic == "Redemption")
                        {
                            restrictedIndexes.AddRange(new int[] { 0, 2, 5 });
                            cmbOption1.SelectedIndex = 1;
                        }

                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }
                    else
                    {
                        cbStratagem5.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.Visible = false;

                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Two Bolt Pistols",
                            "Two Inferno Pistols (+10 pts)",
                            "Two Ministorum Hand Flamers (+10 pts)"
                        });

                        if(restriction == 2 && Weapons[currentIndex] == "Two Bolt Pistols")
                        {
                            cmbOption1.Enabled = false;
                        }
                        else
                        {
                            cmbOption1.Enabled = true;
                        }

                        restrictedIndexes.Clear();
                        this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                    }

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);

                    isLoading = false;
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

            Points = DEFAULT_POINTS * UnitSize;
            restriction = 0;

            foreach (var weapon in Weapons)
            {
                if(weapon == "Bolt Pistol and Plasma Pistol (+5 pts)" || weapon == "Bolt Pistol and Power Sword (+5 pts)"
                    || weapon == "Plasma Pistol and Chainsword (+5 pts)")
                {
                    Points += 5;
                }

                if(weapon == "Plasma Pistol and Power Sword (+10 pts)")
                {
                    Points += 10;
                }

                if(weapon == "Two Inferno Pistols (+10 pts)" || weapon == "Two Ministorum Hand Flamers (+10 pts)")
                {
                    Points += 10;
                    restriction++;
                }
            }
        }

        public override string ToString()
        {
            return "Seraphim Squad - " + Points + "pts";
        }
    }
}
