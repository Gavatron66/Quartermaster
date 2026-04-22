using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Possessed : Datasheets
    {
        public Possessed()
        {
            DEFAULT_POINTS = 28;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1k";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "<LEGION>",
                "INFANTRY", "DAEMON", "DAEMONKIN", "POSSESSED"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Possessed();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = Convert.ToInt32(UnitSize);

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            cbOption1.Text = "Chaos Icon (+5 pts)";
            if (Weapons.Contains(""))
            {
                cbOption1.Checked = false;
            }
            else
            {
                cbOption1.Checked = true;
            }

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(cbOption1.Location.X + 20, cbOption1.Location.Y + 60);
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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();

                    Relic = chosenRelic;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[0] = "";
                    }
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
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

            if (cbOption1.Checked)
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Possessed - " + Points + "pts";
        }
    }
}
