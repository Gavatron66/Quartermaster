using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class CenturionDevastators : Datasheets
    {
        int currentIndex;
        public CenturionDevastators()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 3;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Grav-cannon");
                Weapons.Add("Hurricane Bolter");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CENTURION", "CENTURION DEVASTATOR SQUAD"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new CenturionDevastators();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Assault Centurion Sergeant w/ " + Weapons[0] + " and " + Weapons[1]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Assault Centurion w/ " + Weapons[i * 2] + " and " + Weapons[(i * 2) + 1]);
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Grav-cannon",
                "Two Heavy Bolters",
                "Two Lascannons"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Centurion Missile Launcher",
                "Hurricane Bolter"
            });

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["cbOption1"].Location.X, panel.Controls["cbOption1"].Location.Y + 60);
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
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 2] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Assault Centurion Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Assault Centurion w/ " + Weapons[currentIndex * 2] +
                            " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 2) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[0] = "Assault Centurion Sergeant w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Assault Centurion w/ " + Weapons[currentIndex * 2] +
                            " and " + Weapons[(currentIndex * 2) + 1];
                    }
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    antiLoop = true;

                    #region Codex Supplement: Ultramarines Strat Relics
                    if (chosenRelic == "Hellfury Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Hellfury Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders Strat Relics
                    if (chosenRelic == "Dragonrage Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Dragonrage Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard Strat Relics
                    if (chosenRelic == "Korvidari Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Korvidari Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Iron Hands Strat Relics
                    if (chosenRelic == "Haywire Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Haywire Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: White Scars Strat Relics
                    if (chosenRelic == "Stormwrath Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Stormwrath Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Imperial Fists Strat Relics
                    if (chosenRelic == "Gatebreaker Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Gatebreaker Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Deathwatch Strat Relics
                    if (chosenRelic == "Banebolts of Eryxia (Slot 1)" || chosenRelic == "Artificer Bolt Cache (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Banebolts of Eryxia (Slot 2)" || chosenRelic == "Artificer Bolt Cache (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Space Wolves Strat Relics
                    if (chosenRelic == "Morkai's Teeth Bolts (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Morkai's Teeth Bolts (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    #endregion

                    Relic = chosenRelic;
                    antiLoop = false;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Grav-cannon");
                        Weapons.Add("Hurricane Bolter");
                        lbModelSelect.Items.Add("Assault Centurion w/ " + Weapons[currentIndex * 2] +
                            " and " + Weapons[(currentIndex * 2) + 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 2) - 1, 2);
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

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 2]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 2) + 1]);

                    if (currentIndex == 0)
                    {
                        cbStratagem5.Visible = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        #region Codex Supplement: Ultramarines Strat Relics
                        if (Relic == "Hellfury Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Hellfury Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Salamanders Strat Relics
                        else if (Relic == "Dragonrage Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Dragonrage Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Raven Guard Strat Relics
                        else if (Relic == "Korvidari Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Korvidari Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Iron Hands Strat Relics
                        else if (Relic == "Haywire Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Haywire Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: White Scars Strat Relics
                        else if (Relic == "Stormwrath Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Stormwrath Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Imperial Fists Strat Relics
                        else if (Relic == "Gatebreaker Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Gatebreaker Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
                        #region Codex Supplement: Space Wolves Strat Relics
                        if (Relic == "Morkai's Teeth Bolts (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Morkai's Teeth Bolts (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        #endregion
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

            Points = UnitSize * DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Centurion Devastator Squad - " + Points + "pts";
        }
    }
}
