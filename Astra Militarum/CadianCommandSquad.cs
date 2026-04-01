using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class CadianCommandSquad : Datasheets
    {
        public CadianCommandSquad()
        {
            DEFAULT_POINTS = 15;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "cadianCommand";
            Weapons.Add("Laspistol");   //Commander
            Weapons.Add("Chainsword");
            Weapons.Add("Lasgun and Regimental Standard");      //Regimental Standard
            Weapons.Add("Laspistol");   //Master Vox
            Weapons.Add("Chainsword");  //Veteran
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "CADIAN",
                "INFANTRY", "CHARACTER", "OFFICER", "REGIMENTAL", "CADIAN COMMANDER",
                "INFANTRY", "REGIMENTAL", "COMMAND SQUAD"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new CadianCommandSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gb.Controls["gb_cmbOption2"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            panel.Controls["cmbFactionupgrade"].Visible = false;
            panel.Controls["lblFactionupgrade"].Visible = false;
            gb.Text = "Cadian Commander";

            panel.Controls["lblExtra1"].Visible = true;
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(cmbWarlord.Location.X, cmbWarlord.Location.Y + 34);
            panel.Controls["lblExtra1"].Text = "This unit contains:\n - 1 Cadian Commander\n - 1 Cadian Veteran w/ Regimental Standard (Box 1)\n"
                + " - 1 Cadian Veteran w/ Medi-pack (Doesn't have a Box)\n - 1 Cadian Veteran w/ Master Vox (Box 2)\n - 1 Cadian Veteran w/ Laspistol and Chainsword (Box 3)";


            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "Bolt Pistol",
                "Laspistol",
                "Plasma Pistol (+5 pts)"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[0]);

            gb_cmbOption2.Items.Clear();
            gb_cmbOption2.Items.AddRange(new string[]
            {
                "Chainsword",
                "Power Fist (+5 pts)",
                "Power Sword"
            });
            gb_cmbOption2.SelectedIndex = gb_cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Flamer",
                "Grenade Launcher",
                "Lasgun and Regimental Standard",
                "Meltagun",
                "Plasma Gun"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[2]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Bolt Pistol",
                "Laspistol",
                "Plasma Pistol (+5 pts)"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[3]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Chainsword",
                "Flamer",
                "Grenade Launcher",
                "Meltagun",
                "Plasma Gun",
                "Power Fist",
                "Power Sword"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[4]);

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

            if (Relic != null && cmbRelic.Items.Contains(Relic))
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = 0;
            }

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
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gb.Controls["gb_cmbOption2"] as ComboBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[2] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[3] = cmbOption2.SelectedItem.ToString();
                    break;
                case 13:
                    Weapons[4] = cmbOption3.SelectedItem.ToString();
                    break;
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
                    gb_cmbOption1.Enabled = true;
                    gb_cmbOption2.Enabled = true;
                    cmbOption1.Enabled = true;

                    if (chosenRelic == "Legacy of Kalladius")
                    {
                        gb_cmbOption2.SelectedIndex = 0;
                        gb_cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Claw of the Desert Tigers") {
                        gb_cmbOption2.SelectedIndex = 2;
                        gb_cmbOption2.Enabled = false;
                    }
                    else if (chosenRelic == "Finial of the Nemrodesh 1st")
                    {
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }
                    else if (chosenRelic == "The Emperor's Fury")
                    {
                        gb_cmbOption1.SelectedIndex = 2;
                        gb_cmbOption1.Enabled = false;
                    }

                    Relic = chosenRelic;
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; cmbWarlord.SelectedIndex = -1; }
                    break;
                case 411:
                    Weapons[0] = gb_cmbOption1.SelectedItem.ToString();
                    break;
                case 412:
                    Weapons[1] = gb_cmbOption2.SelectedItem.ToString();
                    break;
                case 71:
                    if (cbStratagem1.Checked)
                    {
                        Stratagem.Add(cbStratagem1.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem1.Text))
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

            Points = DEFAULT_POINTS * UnitSize;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            foreach (string weapon in Weapons)
            {
                if(weapon == "Plasma Pistol (+5 pts)" || weapon == "Power Fist (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Cadian Command Squad - " + Points + "pts";
        }
    }
}