using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    internal class SicarianRuststalkers : Datasheets
    {
        public SicarianRuststalkers()
        {
            DEFAULT_POINTS = 16;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "3NS(1m)";
            Weapons.Add("5");  //Chordclaw and Transonic Razer
            Weapons.Add("0");   //Transonic Blades
            Weapons.Add("Chordclaw and Transonic Razer"); //Princeps
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "INFANTRY", "CORE", "SICARIAN", "SICARIAN RUSTALKERS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new SicarianRuststalkers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AdMech;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            panel.Controls["lblnud1"].Text = "Models with Chordclaws and Transonic Razers:";
            panel.Controls["lblnud2"].Text = "Models with Transonic Blades:";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            antiLoop = true;
            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;
            nudOption1.Location = new System.Drawing.Point(429, 59);

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;
            nudOption2.Location = new System.Drawing.Point(429, 91);

            nudOption1.Value = int.Parse(Weapons[0]);
            nudOption2.Value = int.Parse(Weapons[1]);
            antiLoop = false;

            gb.Text = "Sicarian Ruststalker Princeps";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "Chordclaw and Transonic Razer",
                "Chordclaw and Transonic Blades",
                "Transonic Blades"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[2]);

            cbStratagem3.Text = repo.StratagemList[2];
            cbStratagem3.Location = new System.Drawing.Point(panel.Controls["nudUnitSize"].Location.X + 20, panel.Controls["cmbOption2"].Location.Y + 60 + gb.Height);
            cbStratagem4.Text = repo.StratagemList[3];
            cbStratagem4.Location = new System.Drawing.Point(panel.Controls["cbStratagem3"].Location.X, panel.Controls["cbStratagem3"].Location.Y + 30);

            panel.Controls["lblWarlord"].Location = new System.Drawing.Point(cbStratagem4.Location.X, cbStratagem4.Location.Y + 30);
            cmbWarlord.Location = new System.Drawing.Point(cbStratagem4.Location.X, cbStratagem4.Location.Y + 50);
            panel.Controls["lblWarlord"].Visible = false;
            cmbRelic.Visible = false;
            cbStratagem3.Visible = true;

            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cmbWarlord.Location.X, cmbWarlord.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cmbWarlord.Location.X, cmbWarlord.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
            cbStratagem4.Visible = true;

            cmbWarlord.Items.Clear();
            cmbWarlord.Items.AddRange(f.GetWarlordTraits("Skitarii").ToArray());

            if (Stratagem.Contains(cbStratagem3.Text))
            {
                cbStratagem3.Checked = true;
                cbStratagem3.Enabled = true;

                cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
                panel.Controls["lblWarlord"].Visible = true;
                cmbWarlord.Visible = true;
            }
            else
            {
                cbStratagem3.Checked = false;
                cmbWarlord.SelectedIndex = -1;
                panel.Controls["lblWarlord"].Visible = false;
                cmbWarlord.Visible = false;
            }

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            if (Stratagem.Contains(cbStratagem4.Text))
            {
                cbStratagem4.Checked = true;
                cbStratagem4.Enabled = true;

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

                panel.Controls["lblRelic"].Visible = true;
                cmbRelic.Visible = true;
            }
            else
            {
                cbStratagem4.Checked = false;
                cmbRelic.SelectedIndex = 0;
                panel.Controls["lblRelic"].Visible = false;
                cmbRelic.Visible = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 15:
                    if (cmbWarlord.SelectedIndex != -1)
                    {
                        WarlordTrait = cmbWarlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }

                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    Relic = chosenRelic;
                    break;
                case 30:
                    int oldSize = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    antiLoop = true;
                    if (UnitSize > oldSize)
                    {
                        nudOption1.Value += UnitSize - oldSize;
                    }

                    if (UnitSize < oldSize)
                    {
                        if (nudOption1.Value >= oldSize - UnitSize)
                        {
                            nudOption1.Value -= oldSize - UnitSize;
                        }
                        else
                        {
                            nudOption2.Value -= oldSize - UnitSize;
                        }
                    }
                    antiLoop = false;
                    break;
                case 31:
                    int temp = Convert.ToInt32(Weapons[0]);
                    antiLoop = true;

                    if (nudOption1.Value < 0)
                    {
                        nudOption1.Value++;
                    }
                    else if (nudOption1.Value > UnitSize)
                    {
                        nudOption1.Value--;
                    }
                    else if (temp < nudOption1.Value)
                    {
                        nudOption2.Value--;
                    }
                    else if (temp > nudOption1.Value)
                    {
                        nudOption2.Value++;
                    }
                    antiLoop = false;

                    Weapons[0] = Convert.ToString(nudOption1.Value);
                    Weapons[1] = Convert.ToString(nudOption2.Value);
                    break;
                case 32:
                    int temp2 = Convert.ToInt32(Weapons[1]);
                    antiLoop = true;

                    if (nudOption2.Value < 0)
                    {
                        nudOption2.Value++;
                    }
                    else if (nudOption2.Value > UnitSize)
                    {
                        nudOption2.Value--;
                    }
                    else if (temp2 < nudOption2.Value)
                    {
                        nudOption1.Value--;
                    }
                    else if (temp2 > nudOption2.Value)
                    {
                        nudOption1.Value++;
                    }
                    antiLoop = false;

                    Weapons[0] = Convert.ToString(nudOption1.Value);
                    Weapons[1] = Convert.ToString(nudOption2.Value);
                    break;
                case 411:
                    Weapons[2] = gb_cmbOption1.SelectedItem.ToString();
                    break;
                case 73:
                    if (cbStratagem3.Checked)
                    {
                        Stratagem.Add(cbStratagem3.Text);
                        panel.Controls["lblWarlord"].Visible = true;
                        cmbWarlord.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Remove(cbStratagem3.Text);
                        }
                        cmbWarlord.Visible = false;
                        panel.Controls["lblWarlord"].Visible = false;
                        cmbWarlord.SelectedIndex = -1;
                    }
                    break;
                case 74:
                    if (cbStratagem4.Checked)
                    {
                        Stratagem.Add(cbStratagem4.Text);
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem4.Text))
                        {
                            Stratagem.Remove(cbStratagem4.Text);
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
            return "Sicarian Ruststalkers - " + Points + "pts";
        }
    }
}
