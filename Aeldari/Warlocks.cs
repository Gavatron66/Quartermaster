using Roster_Builder.Death_Guard;
using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Warlocks : Datasheets
    {
        decimal witchblades;
        decimal singingSpears;

        public Warlocks()
        {
            DEFAULT_POINTS = 25;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize + 20;
            TemplateCode = "3N_p";
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ASURYANI", "<CRAFTWORLD>",
                "INFANTRY", "PSYKER", "WARLOCKS"
            });
            PsykerPowers = new string[1] { string.Empty };
            Role = "Elites";

            witchblades = 1;
            singingSpears = 0;
        }

        public override Datasheets CreateUnit()
        {
            return new Warlocks();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Aeldari;
            Template.LoadTemplate(TemplateCode, panel);

            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;

            lblnud1.Text = "Singing Spears (+5 pts):";
            lblnud1.Location = new System.Drawing.Point(lblnud1.Location.X - 20, lblnud1.Location.Y);
            lblnud2.Text = "Witchblades:";
            lblnud2.Location = new System.Drawing.Point(lblnud2.Location.X + 45, lblnud2.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = 6;
            nudOption1.Value = singingSpears;

            nudOption2.Minimum = 0;
            nudOption2.Value = 0;
            nudOption2.Maximum = 6;
            nudOption2.Value = witchblades;

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("Battle");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            if(UnitSize < 4)
            {
                lblPsyker.Text = "Select one of the following:";
                clbPsyker.ClearSelected();
                for (int i = 0; i < clbPsyker.Items.Count; i++)
                {
                    clbPsyker.SetItemChecked(i, false);
                }

                if (PsykerPowers[0] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
                }
            }
            else
            {
                lblPsyker.Text = "Select two of the following:";
                clbPsyker.ClearSelected();
                for (int i = 0; i < clbPsyker.Items.Count; i++)
                {
                    clbPsyker.SetItemChecked(i, false);
                }

                if (PsykerPowers[0] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[1]), true);
                }
            }

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
                cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (Relic != null)
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = -1;
            }

            if (UnitSize != 1)
            {
                cbWarlord.Visible = false;
                panel.Controls["lblWarlord"].Visible = false;
                panel.Controls["lblRelic"].Visible = false;
                cmbWarlord.Visible = false;
                cmbRelic.Visible = false;
            }
            else
            {
                cbWarlord.Visible = true;
                panel.Controls["lblWarlord"].Visible = true;
                panel.Controls["lblRelic"].Visible = true;
                cmbWarlord.Visible = true;
                cmbRelic.Visible = true;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 30:
                    int oldSize = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize > oldSize)
                    {
                        nudOption2.Value += UnitSize - oldSize;
                    }

                    if (UnitSize < oldSize)
                    {
                        if (nudOption2.Value >= oldSize - UnitSize)
                        {
                            nudOption2.Value -= oldSize - UnitSize;
                        }
                        else
                        {
                            nudOption1.Value -= oldSize - UnitSize;
                        }
                    }

                    if (UnitSize >= 4)
                    {
                        lblPsyker.Text = "Select two of the following:";
                        string[] temp = new string[2] { PsykerPowers[0], string.Empty };
                        PsykerPowers = temp;
                    }
                    else if (UnitSize < 4 && PsykerPowers.Length == 2)
                    {
                        lblPsyker.Text = "Select one of the following:";
                        if (PsykerPowers[1] != string.Empty)
                        {
                            clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[1]), false);
                        }
                        string[] temp = new string[1] { PsykerPowers[0] };
                        PsykerPowers = temp;
                    }

                    if (UnitSize != 1)
                    {
                        cbWarlord.Visible = false;
                        panel.Controls["lblWarlord"].Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbWarlord.Visible = false;
                        cmbRelic.Visible = false;
                    }
                    else
                    {
                        cbWarlord.Visible = true;
                        panel.Controls["lblWarlord"].Visible = true;
                        panel.Controls["lblRelic"].Visible = true;
                        cmbWarlord.Visible = true;
                        cmbRelic.Visible = true;
                    }
                    break;
                case 31:
                    if (nudOption1.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value <= nudUnitSize.Value)
                    {
                        singingSpears = nudOption1.Value;
                    }
                    else
                    {
                        nudOption1.Value -= 1;
                    }
                    break;
                case 32:
                    if (nudOption2.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value <= nudUnitSize.Value)
                    {
                        witchblades = nudOption2.Value;
                    }
                    else
                    {
                        nudOption2.Value -= 1;
                    }
                    break;
                case 60:
                    if(UnitSize >= 4)
                    {
                        if (clbPsyker.CheckedItems.Count < 2)
                        {
                            break;
                        }
                        else if (clbPsyker.CheckedItems.Count == 2)
                        {
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                            PsykerPowers[1] = clbPsyker.CheckedItems[1] as string;
                        }
                        else
                        {
                            clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                        }
                    }
                    else
                    {
                        if (clbPsyker.CheckedItems.Count < 1)
                        {
                            break;
                        }
                        else if (clbPsyker.CheckedItems.Count == 1)
                        {
                            PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                        }
                        else
                        {
                            clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            if(UnitSize == 1)
            {
                Points = 45;
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            Points += Convert.ToInt32(singingSpears * 5);
        }

        public override string ToString()
        {
            return "Warlocks - " + Points + "pts";
        }
    }
}