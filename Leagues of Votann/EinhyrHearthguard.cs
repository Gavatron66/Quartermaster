using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class EinhyrHearthguard : Datasheets
    {
        public EinhyrHearthguard()
        {
            DEFAULT_POINTS = 45;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N2mS(2m)";
            Weapons.Add("EtaCarn Plasma Guns");
            Weapons.Add("Concussion Gauntlets");
            Weapons.Add("Concussion Gauntlet");
            Weapons.Add("Weavefield Crest");
            Keywords.AddRange(new string[]
            {
                "VOTANN", "<LEAGUE>",
                "INFANTRY", "CORE", "EXO-ARMOUR", "EINHYR", "HEARTHGUARD"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new EinhyrHearthguard();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as LeaguesOfVotann;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gbUnitLeader.Controls["gb_cmbOption2"] as ComboBox;
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
                "EtaCarn Plasma Guns",
                "Volkanite Disintegrators"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Concussion Gauntlets",
                "Plasma Blade Gauntlets"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            gbUnitLeader.Text = "Hesyr";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "Concussion Gauntlet",
                "Concussion Hammer (+10 pts)",
                "Plasma Blade Gauntlet"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[2]);

            gb_cmbOption2.Items.Clear();
            gb_cmbOption2.Items.AddRange(new string[]
            {
                "Teleport Crest",
                "Weavefield Crest"
            });
            gb_cmbOption2.SelectedIndex = gb_cmbOption2.Items.IndexOf(Weapons[3]);

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(gbUnitLeader.Location.X, gbUnitLeader.Location.Y + 10 + gbUnitLeader.Height);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
            cbStratagem5.Visible = true;

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
            antiLoop = false;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gbUnitLeader.Controls["gb_cmbOption2"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem as string;
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    gb_cmbOption1.Enabled = true;
                    gb_cmbOption2.Enabled = true;

                    if (chosenRelic == "The Grey Crest")
                    {
                        gb_cmbOption2.SelectedIndex = 1;
                        gb_cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "The Hearthfist")
                    {
                        gb_cmbOption1.SelectedIndex = 0;
                        gb_cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "Warpestryk")
                    {
                        gb_cmbOption2.SelectedIndex = 0;
                        gb_cmbOption2.Enabled = false;
                    }

                    Relic = chosenRelic;
                    break;
                case 30:
                    UnitSize = Convert.ToInt32(nudUnitSize.Value);
                    break;
                case 411:
                    Weapons[2] = gb_cmbOption1.SelectedItem as string;
                    break;
                case 412:
                    Weapons[3] = gb_cmbOption2.SelectedItem as string;
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

            if (Weapons[2] == "Concussion Hammer (+10 pts)")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Einhyr Hearthguard - " + Points + "pts";
        }
    }
}
