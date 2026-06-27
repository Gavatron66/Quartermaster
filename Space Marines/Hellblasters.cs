using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class Hellblasters : Datasheets
    {
        public Hellblasters()
        {
            DEFAULT_POINTS = 30;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1m1k";
            Weapons.Add("Plasma Incinerator");
            Weapons.Add(""); //Plasma Pistol
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "HELLBLASTER SQUAD"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Hellblasters();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as SpaceMarines;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Assault Plasma Incinerator",
                "Heavy Plasma Incinerator",
                "Plasma Incinerator"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Plasma Pistol (Hellblaster Sergeant)";
            if (Weapons[1] != "")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(86, 29);
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblNumModels"].Location.X, panel.Controls["cbOption1"].Location.Y + 60);
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

            if (repo.currentSubFaction == "Black Templars")
            {
                cbStratagem4.Text = repo.StratagemList[3];
                cbStratagem4.Location = new System.Drawing.Point(cbStratagem5.Location.X + cbStratagem5.Width + 10, cbStratagem5.Location.Y);
                cbStratagem4.Visible = true;

                //Relic Bearers Code
                panel.Controls["lblExtra1"].Visible = true;
                panel.Controls["lblExtra1"].Location = new System.Drawing.Point(cbStratagem4.Location.X, cbStratagem4.Location.Y + 30);
                panel.Controls["lblExtra1"].Text = "Relic Bearers";

                ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
                cmbFaction.Visible = true;
                cmbFaction.Location = new System.Drawing.Point(cbStratagem4.Location.X + 4, cbStratagem4.Location.Y + 54);
                cmbFaction.Items.Clear();
                cmbFaction.Items.AddRange(repo.GetFactionUpgrades(this.Keywords).ToArray());

                if (Factionupgrade != null)
                {
                    cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
                }
                else
                {
                    cmbFaction.SelectedIndex = 0;
                }
            }
            else
            {
                cbStratagem4.Visible = false;
            }

            if (Stratagem.Contains(cbStratagem4.Text))
            {
                cbStratagem4.Checked = true;
                cbStratagem4.Enabled = true;
            }
            else
            {
                cbStratagem4.Checked = false;
                cbStratagem4.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem4.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cbOption1.Enabled = true;

                    if (chosenRelic == "Hellfury Bolts" || chosenRelic == "Dragonrage Bolts" || chosenRelic == "Korvidari Bolts"
                        || chosenRelic == "Haywire Bolts" || chosenRelic == "Stormwrath Bolts" || chosenRelic == "Gatebreaker Bolts"
                        || chosenRelic == "Banebolts of Eryxia" || chosenRelic == "Artificer Bolt Cache" || chosenRelic == "Morkai's Teeth Bolts"
                        || chosenRelic == "Bolts of Judgement" || chosenRelic == "Witchseeker Bolts" || chosenRelic == "Quake Bolts")
                    {
                        cbOption1.Checked = false;
                        cbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Sunwrath Pistol" || chosenRelic == "Atonement") //Ultramarines/Dark Angels Plasma Pistol
                    {
                        cbOption1.Checked = true;
                        cbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Silentus Pistol") //Raven Guard Bolt Pistol
                    {
                        cbOption1.Checked = false;
                        cbOption1.Enabled = false;
                    }

                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
                case 74:
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
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
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Hellblaster Squad - " + Points + "pts";
        }
    }
}
