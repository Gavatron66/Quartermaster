using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Space_Marines
{
    public class Havocs : Datasheets
    {
        int currentIndex;

        public Havocs()
        {
            DEFAULT_POINTS = 25;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL2m";
            Weapons.Add("Flamer (+5 pts)");
            Weapons.Add("Astartes Chainsword");
            Weapons.Add("Havoc Autocannon");
            Weapons.Add("Havoc Autocannon");
            Weapons.Add("Lascannon (+5 pts)");
            Weapons.Add("Lascannon (+5 pts)");
            Keywords.AddRange(new string[]
            {
                "CHAOS", "HERETIC ASTARTES", "TRAITORIS ASTARTES", "CHAOS UNDIVIDED", "<LEGION>",
                "INFANTRY", "CORE", "HAVOCS"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Havocs();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as ChaosSpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["lblModelPoints"].Visible = false;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Havoc Champion w/ " + Weapons[0] + " and " + Weapons[1]);

            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Havoc w/ " + Weapons[i + 1]);
            }

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

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

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblFactionupgrade"].Location.X, cmbFaction.Location.Y + 30);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);

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

            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Havoc Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    else
                    {
                        Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();
                        lbModelSelect.Items[currentIndex] = "Havoc w/ " + Weapons[currentIndex + 1];
                    }

                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    lbModelSelect.Items[0] = "Havoc Champion w/ " + Weapons[0] + " and " + Weapons[1];
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 17:
                    string chosenRelic = cmbRelic.SelectedItem.ToString();
                    cmbOption1.Enabled = true;
                    cmbOption2.Enabled = true;
                    Relic = chosenRelic;

                    if(Relic == "Hyper-Growth Bolts" || Relic == "Loyalty's Reward")
                    {
                        cmbOption1.SelectedIndex = 1;
                        cmbOption1.Enabled = false;
                    }
                    else if(Relic == "Ashen Axe (Slot 1)" || Relic == "Axe of the Forgemaster (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 6;
                        cmbOption1.Enabled = false;
                    }
                    else if (Relic == "Ashen Axe (Slot 2)" || Relic == "Axe of the Forgemaster (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 1;
                        cmbOption2.Enabled = false;
                    }
                    else if (Relic == "Distortion (Slot 1)" || Relic == "Blade of the Relentless (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 9;
                        cmbOption1.Enabled = false;
                    }
                    else if (Relic == "Distortion (Slot 2)" || Relic == "Blade of the Relentless (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 4;
                        cmbOption2.Enabled = false;
                    }
                    else if (Relic == "The Black Mace (Slot 1)")
                    {
                        cmbOption1.SelectedIndex = 8;
                        cmbOption1.Enabled = false;
                    }
                    else if (Relic == "The Black Mace (Slot 2)")
                    {
                        cmbOption2.SelectedIndex = 3;
                        cmbOption2.Enabled = false;
                    }

                    break;
                case 61:
                    if (antiLoop)
                    {
                        break;
                    }

                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;
                        cbStratagem5.Visible = true;
                        cmbOption1.Enabled = true;
                        cmbOption2.Enabled = true;

                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            panel.Controls["lblRelic"].Visible = true;
                            cmbRelic.Visible = true;
                        }

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Boltgun",
                            "Flamer (+5 pts)",
                            "Meltagun (+10 pts)",
                            "Plasma Pistol (+5 pts)",
                            "Plasma Gun (+10 pts)",
                            "Power Axe (+5 pts)",
                            "Power Fist (+10 pts)",
                            "Power Maul (+5 pts)",
                            "Power Sword (+5 pts)",
                            "Tainted Chainaxe (+5 pts)"
                        });

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "Astartes Chainsword",
                            "Power Axe (+5 pts)",
                            "Power Fist (+10 pts)",
                            "Power Maul (+5 pts)",
                            "Power Sword (+5 pts)",
                            "Tainted Chainaxe (+5 pts)"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        if (Relic == "Hyper-Growth Bolts" || Relic == "Loyalty's Reward")
                        {
                            cmbOption1.SelectedIndex = 1;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Ashen Axe (Slot 1)" || Relic == "Axe of the Forgemaster (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 6;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Ashen Axe (Slot 2)" || Relic == "Axe of the Forgemaster (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 1;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "Distortion (Slot 1)" || Relic == "Blade of the Relentless (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 9;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "Distortion (Slot 2)" || Relic == "Blade of the Relentless (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 4;
                            cmbOption2.Enabled = false;
                        }
                        else if (Relic == "The Black Mace (Slot 1)")
                        {
                            cmbOption1.SelectedIndex = 8;
                            cmbOption1.Enabled = false;
                        }
                        else if (Relic == "The Black Mace (Slot 2)")
                        {
                            cmbOption2.SelectedIndex = 3;
                            cmbOption2.Enabled = false;
                        }
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;
                        cbStratagem5.Visible = false;
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Havoc Autocannon",
                            "Heavy Bolter",
                            "Lascannon (+5 pts)",
                            "Missile Launcher (+5 pts)",
                            "Reaper Chaincannon (+5 pts)"
                        });

                        antiLoop = true;
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    antiLoop = false;

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

            foreach (var weapon in Weapons)
            {
                if(weapon.Contains("+5 pts"))
                {
                    Points += 5;
                }
                else if (weapon.Contains("+10 pts"))
                {
                    Points += 10;
                }
            }
        }

        public override string ToString()
        {
            return "Havocs - " + Points + "pts";
        }
    }
}
