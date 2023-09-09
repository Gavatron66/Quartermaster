using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DG_DaemonPrince : Datasheets
    {
        public DG_DaemonPrince()
        {
            DEFAULT_POINTS = 140;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m2k_pc";
            Weapons.Add("Hellforged Sword");
            Weapons.Add(""); //Plague Spewer
            Weapons.Add(""); //Foetid Wings
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAUGE COMPANY>",
                "MONSTER", "CHARACTER", "PSYKER", "DAEMON", "BUBONIC ASTARTES", "LORD OF THE DEATH GUARD",
                    "DAEMON PRINCE"
            });
            PsykerPowers = new string[1] { string.Empty };
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox check1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox check2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }


            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Hellforged Sword", 
                "Daemonic Axe", 
                "Malefic Talons"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            check1.Text = "Plague Spewer";
            if (Weapons[1] != string.Empty)
            {
                check1.Checked = true;
            }
            else
            {
                check1.Checked = false;
            }

            check2.Text = "Foetid Wings";
            if (Weapons[2] != string.Empty)
            {
                check2.Checked = true;
            }
            else
            {
                check2.Checked = false;
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

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            List<string> psykerpowers = new List<string>();
            psykerpowers = repo.GetPsykerPowers("");
            clbPsyker.Items.Clear();
            foreach (string power in psykerpowers)
            {
                clbPsyker.Items.Add(power);
            }

            lblPsyker.Text = "Select one of the following:";
            clbPsyker.ClearSelected();
            for(int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
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

            panel.Controls["lblFactionupgrade"].Visible = true;
            panel.Controls["cmbFactionupgrade"].Visible = true;

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            if (Stratagem.Contains(cbStratagem1.Text))
            {
                cbStratagem1.Checked = true;
                cbStratagem1.Enabled = true;
            }
            else
            {
                cbStratagem1.Checked = false;
                cbStratagem1.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem1.Text));
            }

            if (Stratagem.Contains(cbStratagem2.Text))
            {
                cbStratagem2.Checked = true;
                cbStratagem2.Enabled = true;
            }
            else
            {
                cbStratagem2.Checked = false;
                cbStratagem2.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem2.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cb = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cb2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmb = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox warlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox factionud = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckedListBox clb = panel.Controls["clbPsyker"] as CheckedListBox;
            CheckBox isWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmb.SelectedItem.ToString();
                    break;
                case 21:
                    if (cb.Checked)
                    {
                        Weapons[1] = cb.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
                case 22:
                    if (cb2.Checked)
                    {
                        Weapons[2] = cb2.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
                case 25:
                    if (isWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; }
                    break;
                case 15:
                    if (warlord.SelectedIndex != -1)
                    {
                        WarlordTrait = warlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }

                    break;
                case 16:
                    Factionupgrade = factionud.Text;
                    break;
                case 60:
                    if (clb.CheckedItems.Count == 1)
                    {
                        PsykerPowers[0] = clb.SelectedItem.ToString();
                    }
                    else
                    {
                        clb.SetItemChecked(clb.SelectedIndex, false);
                    }
                    break;
                case 17:
                    Relic = cmbRelic.SelectedItem.ToString();
                    break;
                case 71:
                    if (cbStratagem1.Checked)
                    {
                        Stratagem.Add(cbStratagem1.Text);
                    }
                    else
                    {
                        if(Stratagem.Contains(cbStratagem1.Text))
                        {
                            Stratagem.Remove(cbStratagem1.Text);
                        }
                    }
                    break;
                case 72:
                    if (cbStratagem2.Checked)
                    {
                        Stratagem.Add(cbStratagem2.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem2.Text))
                        {
                            Stratagem.Remove(cbStratagem2.Text);
                        }
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            if (cb.Checked && (code == 21 || code == -1))
            {
                cmb.Enabled = false;
                cb2.Enabled = false;
                cb2.Checked = false;
                Weapons[2] = string.Empty;
                cmb.SelectedItem = "Hellforged Sword";
            }
            else if (code == 21)
            {
                cmb.Enabled = true;
                cb2.Enabled = true;
                Weapons[1] = string.Empty;
            }

            if (Weapons.Contains("Daemonic Axe") || Weapons.Contains("Hellforged Sword"))
            {
                Points += 10;
            }

            if (Weapons.Contains("Plague Spewer"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Foetid Wings"))
            {
                Points += 35;
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Death Guard Daemon Prince - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new DG_DaemonPrince();
        }
    }
}
