using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DeathshroudTerminators : Datasheets
    {
        public DeathshroudTerminators()
        {
            DEFAULT_POINTS = 50;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NS(2k)";
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "CORE", "BUBONIC ASTARTES", "TERMINATOR", "DEATHSHROUD TERMINATORS"
            });
            Role = "Elites";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            factionsRestrictions = repo.restrictedItems;
            Template.LoadTemplate(TemplateCode, panel);

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            cbStratagem3.Visible = true;

            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;
            int currentSize = UnitSize;
            nud.Minimum = 3;
            nud.Value = nud.Minimum;
            nud.Maximum = 6;
            nud.Value = currentSize;

            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            gb.Text = "Deathshroud Champion";

            CheckBox cb = gb.Controls["cbLeaderOption1"] as CheckBox;
            cb.Text = "Additional Plaguespurt Gauntlet";
            if (Weapons[0] != string.Empty)
            {
                cb.Checked = true;
            } else
            {
                cb.Checked = false;
            }

            CheckBox cb2 = gb.Controls["cbLeaderOption2"] as CheckBox;
            cb2.Text = "Chimes of Contagion";
            if (Weapons[1] != string.Empty)
            {
                cb2.Checked = true;
            }
            else
            {
                cb2.Checked = false;
            }

            ComboBox factionud = panel.Controls["cmbFactionupgrade"] as ComboBox;

            factionud.Items.Clear();
            factionud.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                factionud.SelectedIndex = factionud.Items.IndexOf(Factionupgrade);
            }
            else
            {
                factionud.SelectedIndex = 0;
            }

            restrictedIndexes = new List<int>();
            for (int i = 0; i < factionud.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(factionud.Items[i]) && Factionupgrade != factionud.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, factionud);

            panel.Controls["lblFactionupgrade"].Visible = true;
            panel.Controls["cmbFactionupgrade"].Visible = true;

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            cbStratagem3.Text = repo.StratagemList[2];
            cbStratagem3.Location = new System.Drawing.Point(panel.Controls["lblFactionUpgrade"].Location.X, factionud.Location.Y + 30);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem3.Location.X, cbStratagem3.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(factionud.Location.X, cbStratagem3.Location.Y + 50);

            if (Stratagem.Contains(cbStratagem3.Text))
            {
                cbStratagem3.Checked = true;
                cbStratagem3.Enabled = true;

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
                cbStratagem3.Checked = false;
                cbStratagem3.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem3.Text));
                cmbRelic.SelectedIndex = 0;
            }

            restrictedIndexes = new List<int>();
            for (int i = 0; i < cmbRelic.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(cmbRelic.Items[i]) && Relic != cmbRelic.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbRelic);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            CheckBox cb = gb.Controls["cbLeaderOption1"] as CheckBox;
            CheckBox cb2 = gb.Controls["cbLeaderOption2"] as CheckBox;
            ComboBox factionud = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nud.Value.ToString());
                    break;
                case 16:
                    if (!factionsRestrictions.Contains(factionud.Text))
                    {
                        if (Factionupgrade == "(None)")
                        {
                            Factionupgrade = factionud.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(Factionupgrade);
                            Factionupgrade = factionud.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                    }
                    else
                    {
                        factionud.SelectedIndex = factionud.Items.IndexOf(Factionupgrade);
                    }
                    break;
                case 17:
                    if (!factionsRestrictions.Contains(cmbRelic.Text))
                    {
                        if (Relic == "(None)")
                        {
                            Relic = cmbRelic.Text == "" ? "(None)" : cmbRelic.Text;

                            if (Relic != "(None)")
                            {
                                repo.restrictedItems.Add(Relic);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(Relic);
                            Relic = cmbRelic.Text;
                            if (Relic != "(None)")
                            {
                                repo.restrictedItems.Add(Relic);
                            }
                        }
                    }
                    else
                    {
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                        cmbRelic.Enabled = true;
                    }
                    break;
                case 421:
                    if (cb.Checked)
                    {
                        Weapons[0] = cb.Text;
                    }
                    else { Weapons[0] = string.Empty; }
                    break;
                case 422:
                    if (cb2.Checked)
                    {
                        Weapons[1] = cb2.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
                case 73:
                    if (cbStratagem3.Checked)
                    {
                        if (!Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Add(cbStratagem3.Text);
                        }
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Remove(cbStratagem3.Text);
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
            return "Deathshroud Terminators - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new DeathshroudTerminators();
        }
    }
}
