using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class VeteranIntercessors : Datasheets
    {
        bool loading;

        public VeteranIntercessors()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2N1mS(2m)";
            Weapons.Add("Bolt Rifle"); //Squad Option
            Weapons.Add("0"); //Astartes Grenade Launchers
            Weapons.Add("Bolt Rifle"); //Sergeant Weapons
            Weapons.Add("(None)"); //

            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "INTERCESSORS", "VETERAN INTERCESSOR SQUAD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new VeteranIntercessors();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);
            loading = true;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gb.Controls["gb_cmbOption2"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            panel.Controls["lblnud1"].Text = "Astartes Grenade Launchers (1x/5 models):";
            panel.Controls["lblnud1"].Location = new System.Drawing.Point(88, 95);

            gb.Text = "Intercessor Sergeant";

            nudOption1.Location = new System.Drawing.Point(404, 93);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new object[]
            {
                "Auto Bolt Rifle",
                "Bolt Rifle",
                "Heavy Bolt Pistol and Astartes Chainsword",
                "Stalker Bolt Rifle"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            currentSize = Convert.ToInt32(Weapons[1]);
            nudOption1.Minimum = 0;
            nudOption1.Maximum = 2;
            nudOption1.Value = currentSize;
            if (UnitSize < 10)
            {
                nudOption1.Maximum--;
            }

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new object[]
            {
                "Astartes Chainsword",
                "Bolt Rifle",
                "Hand Flamer",
                "Plasma Pistol",
                "Power Sword",
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[2]);

            gb_cmbOption2.Items.Clear();
            gb_cmbOption2.Items.AddRange(new object[]
            {
                "(None)",
                "Astartes Chainsword",
                "Power Fist",
                "Power Sword",
                "Thunder Hammer"
            });
            gb_cmbOption2.SelectedIndex = gb_cmbOption2.Items.IndexOf(Weapons[3]);

            cbStratagem5.Text = repo.StratagemList[4];
            cbStratagem5.Location = new System.Drawing.Point(gb.Location.X, gb.Location.Y + 10 + gb.Height);
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
            loading = false;
        }


        public override void SaveDatasheets(int code, Panel panel)
        {
            if (loading)
            {
                return;
            }

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gb.Controls["gb_cmbOption2"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            switch (code)
            {
                case 11:
                    string temp = Weapons[0];

                    Weapons[0] = cmbOption1.SelectedItem.ToString();

                    gb_cmbOption1.Items.Remove(temp);

                    if (Weapons[0] == "Stalker Bolt Rifle")
                    {
                        gb_cmbOption1.Items.Insert(4, Weapons[0]);

                        if (Weapons[2].Contains("Bolt Rifle") || Weapons[2].Contains("Heavy Bolt Pistol"))
                        {
                            gb_cmbOption1.SelectedIndex = 4;
                        }
                    }
                    else if (Weapons[0].Contains("Heavy Bolt Pistol"))
                    {
                        gb_cmbOption1.Items.Insert(2, "Heavy Bolt Pistol");

                        if (Weapons[2].Contains("Bolt Rifle") || Weapons[2].Contains("Heavy Bolt Pistol"))
                        {
                            gb_cmbOption1.SelectedIndex = 2;
                        }

                        if (Weapons[3] == "(None)")
                        {
                            gb_cmbOption2.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        gb_cmbOption1.Items.Insert(1, Weapons[0]);

                        if (Weapons[2].Contains("Bolt Rifle") || Weapons[2].Contains("Heavy Bolt Pistol"))
                        {
                            gb_cmbOption1.SelectedIndex = 1;
                        }
                    }

                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    gb_cmbOption1.Enabled = true;
                    gb_cmbOption2.Enabled = true;

                    #region Codex Supplement: Ultramarines
                    if (chosenRelic == "Sunwrath Pistol")
                    {
                        gb_cmbOption1.SelectedIndex = 3;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Salamanders
                    else if (chosenRelic == "Drakeblade")
                    {
                        gb_cmbOption2.SelectedIndex = 3;
                        gb_cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Iron Hands
                    else if (chosenRelic == "Teeth of Mars")
                    {
                        gb_cmbOption2.SelectedIndex = 1;
                        gb_cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Imperial Fists
                    else if (chosenRelic == "Fist of Terra")
                    {
                        gb_cmbOption2.SelectedIndex = 2;
                        gb_cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Space Wolves
                    else if (chosenRelic == "Frost Weapon")
                    {
                        gb_cmbOption2.SelectedIndex = 3;
                        gb_cmbOption2.Enabled = false;
                    }
                    #endregion
                    #region Codex Supplement: Dark Angels
                    else if (chosenRelic == "Atonement")
                    {
                        gb_cmbOption1.SelectedIndex = 3;
                        gb_cmbOption1.Enabled = false;
                    }
                    #endregion

                    Relic = chosenRelic;
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize == 10)
                    {
                        nudOption1.Maximum = 2;
                    }

                    if (UnitSize < 10 && nudOption1.Value == 2)
                    {
                        nudOption1.Value--;
                        nudOption1.Maximum = 1;
                    }

                    break;
                case 31:
                    Weapons[1] = nudOption1.Value.ToString();
                    break;
                case 411:
                    Weapons[2] = gb_cmbOption1.SelectedItem.ToString();
                    break;
                case 412:
                    Weapons[3] = gb_cmbOption2.SelectedItem.ToString();
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
            return "Veteran Intercessor Squad - " + Points + "pts";
        }
    }
}