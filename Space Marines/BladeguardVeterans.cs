using System;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class BladeguardVeterans : Datasheets
    {
        public BladeguardVeterans()
        {
            DEFAULT_POINTS = 35;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NS(1m)";
            Weapons.Add("Heavy Bolt Pistol");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "BLADEGUARD", "BLADEGUARD VETERAN SQUAD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new BladeguardVeterans();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            gbUnitLeader.Text = "Bladeguard Veteran Sergeant";

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Heavy Bolt Pistol",
                "Neo-volkite Pistol",
                "Plasma Pistol"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(gbUnitLeader.Location.X, gbUnitLeader.Location.Y + 10 + gbUnitLeader.Height);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            cbStratagem5.Visible = true;

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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    gb_cmbOption1.Enabled = true;

                    #region Codex Supplement: Ultramarines
                    if (chosenRelic == "Hellfury Bolts")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Sunwrath Pistol")
                    {
                        gb_cmbOption1.SelectedIndex = 2;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    if (chosenRelic == "Dragonrage Bolts")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Raven Guard
                    if (chosenRelic == "Korvidari Bolts" || chosenRelic == "Silentus Pistol")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Iron Hands
                    if (chosenRelic == "Haywire Bolts")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: White Scars
                    if (chosenRelic == "Stormwrath Bolts")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Imperial Fists
                    if (chosenRelic == "Gatebreaker Bolts")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Deathwatch
                    if (chosenRelic == "Banebolts of Eryxia" || chosenRelic == "Artificer Bolt Cache")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Space Wolves
                    if (chosenRelic == "Morkai's Teeth Bolts")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion

                    Relic = chosenRelic;
                    break;
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
                case 411:
                    if (gb_cmbOption1.SelectedIndex != -1)
                    {
                        Weapons[0] = gb_cmbOption1.SelectedItem.ToString();
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

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Bladeguard Veteran Squad - " + Points + "pts";
        }
    }
}