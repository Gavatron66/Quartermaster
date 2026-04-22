using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class SerberysSulphurhounds : Datasheets
    {
        public SerberysSulphurhounds()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize + 10; //Alpha is extra points for some reason
            TemplateCode = "2N";
            Weapons.Add("0");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MECHANICUS", "SKITARII", "<FORGE WORLD>",
                "CAVALRY", "CORE", "SERBERYS", "SERBERYS SULPHURHOUNDS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new SerberysSulphurhounds();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AdMech;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;
            CheckBox cbStratagem4 = panel.Controls["cbStratagem4"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 9;
            nudUnitSize.Value = currentSize;

            antiLoop = true;
            nudOption1.Minimum = 0;
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Maximum = UnitSize / 3;
            nudOption1.Value = Convert.ToInt32(Weapons[0]);
            antiLoop = false;
            panel.Controls["lblnud1"].Text = "Phosphor Blast Carbines (+10 pts, 1/3x models):";
            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X + 90, nudOption1.Location.Y + 4);

            cbStratagem3.Text = repo.StratagemList[2];
            cbStratagem3.Location = new System.Drawing.Point(panel.Controls["nudUnitSize"].Location.X + 20, panel.Controls["nudOption1"].Location.Y + 60);
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
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    nudOption1.Maximum = UnitSize / 3;
                    break;
                case 31:
                    Weapons[0] = nudOption1.Value.ToString();
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

            Points = DEFAULT_POINTS * UnitSize + 10;
            Points += int.Parse(Weapons[0]) * 10;
        }

        public override string ToString()
        {
            return "Serberys Sulphurhounds - " + Points + "pts";
        }
    }
}
