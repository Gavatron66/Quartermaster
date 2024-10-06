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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            CheckBox cb = gb.Controls["cbLeaderOption1"] as CheckBox;
            CheckBox cb2 = gb.Controls["cbLeaderOption2"] as CheckBox;
            ComboBox factionud = panel.Controls["cmbFactionupgrade"] as ComboBox;

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
