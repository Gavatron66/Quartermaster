using Roster_Builder.Adeptus_Mechanicus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class CrisisBodyguards : Datasheets
    {
        int currentIndex;
        public CrisisBodyguards()
        {
            DEFAULT_POINTS = 45;
            UnitSize = 2;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "NL5m1k";
            for (int i = 0; i < UnitSize; i++)
            {
                Weapons.Add("Burst Cannon (+5/+10/+20 pts)"); //Three Weapons
                Weapons.Add("(None)");
                Weapons.Add("(None)");
                Weapons.Add("(None)"); //Two Drones
                Weapons.Add("(None)");
                Weapons.Add("");
            }
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "<SEPT>",
                "INFANTRY", "CORE", "BATTLESUIT", "FLY", "JET PACK", "CRISIS", "CRISIS BODYGUARD"
            });
            Role = "Elites";
        }
        public override Datasheets CreateUnit()
        {
            return new CrisisBodyguards();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as T_au;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 2;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Crisis Bodyguard Shas'vre - " + CalcPoints(0) + " pts");
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Crisis Bodyguard Shas'ui - " + CalcPoints(i) + " pts");
            }

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Airbursting Fragmentation Projector (+10/+15/+20 pts)",
                "Burst Cannon (+5/+10/+20 pts)",
                "Counterfire Defence System",
                "Cyclic Ion Blaster (+10/+20/+25 pts)",
                "Early Warning Override",
                "Fusion Blaster (+10/+15/+25 pts)",
                "Missile Pod (+10/+15/+20 pts)",
                "Multi-tracker",
                "Plasma Rifle (+10/+15/+20 pts)",
                "Shield Generator (+5 pts)",
                "Target Lock",
                "T'au Flamer (+5/+10/+15 pts)",
                "Velocity Tracker"
            });

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "(None)",
                "Airbursting Fragmentation Projector (+10/+15/+20 pts)",
                "Burst Cannon (+5/+10/+20 pts)",
                "Counterfire Defence System",
                "Cyclic Ion Blaster (+10/+20/+25 pts)",
                "Early Warning Override",
                "Fusion Blaster (+10/+15/+25 pts)",
                "Missile Pod (+10/+15/+20 pts)",
                "Multi-tracker",
                "Plasma Rifle (+10/+15/+20 pts)",
                "Shield Generator (+5 pts)",
                "Target Lock",
                "T'au Flamer (+5/+10/+15 pts)",
                "Velocity Tracker"
            });

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "(None)",
                "Airbursting Fragmentation Projector (+10/+15/+20 pts)",
                "Burst Cannon (+5/+10/+20 pts)",
                "Counterfire Defence System",
                "Cyclic Ion Blaster (+10/+20/+25 pts)",
                "Early Warning Override",
                "Fusion Blaster (+10/+15/+25 pts)",
                "Missile Pod (+10/+15/+20 pts)",
                "Multi-tracker",
                "Plasma Rifle (+10/+15/+20 pts)",
                "Shield Generator (+5 pts)",
                "Target Lock",
                "T'au Flamer (+5/+10/+15 pts)",
                "Velocity Tracker"
            });

            cmbOption4.Items.Clear();
            cmbOption4.Items.AddRange(new string[]
            {
                "(None)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });

            cmbOption5.Items.Clear();
            cmbOption5.Items.AddRange(new string[]
            {
                "(None)",
                "Gun Drone (+10 pts)",
                "Marker Drone (+10 pts)",
                "Shield Drone (+15 pts)"
            });

            cbOption1.Text = "Iridium Battlesuit (+10 pts)";

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbOption4 = panel.Controls["cmbOption4"] as ComboBox;
            ComboBox cmbOption5 = panel.Controls["cmbOption5"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex * 6] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'vre - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'ui - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 12:
                    Weapons[(currentIndex * 6) + 1] = cmbOption2.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'vre - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'ui - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 13:
                    Weapons[(currentIndex * 6) + 2] = cmbOption3.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'vre - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'ui - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 14:
                    Weapons[(currentIndex * 6) + 3] = cmbOption4.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'vre - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'ui - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 18:
                    Weapons[(currentIndex * 6) + 4] = cmbOption5.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'vre - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'ui - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[(currentIndex * 6) + 5] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 6) + 5] = "";
                    }

                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'vre - " + CalcPoints(currentIndex) + " pts");
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = ("Crisis Bodyguard Shas'ui - " + CalcPoints(currentIndex) + " pts");
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Burst Cannon (+5/+10/+20 pts)"); //Three Weapons
                        Weapons.Add("(None)");
                        Weapons.Add("(None)");
                        Weapons.Add("(None)"); //Two Drones
                        Weapons.Add("(None)");
                        Weapons.Add("");
                        lbModelSelect.Items.Add("Crisis Bodyguard Shas'ui - " + CalcPoints(UnitSize - 1) + " pts");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 6) - 1, 6);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        cmbOption3.Visible = false;
                        cmbOption4.Visible = false;
                        cmbOption5.Visible = false;
                        cmbFaction.Visible = false;
                        cbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        panel.Controls["lblOption3"].Visible = false;
                        panel.Controls["lblOption4"].Visible = false;
                        panel.Controls["lblOption5"].Visible = false;
                        panel.Controls["lblFactionupgrade"].Visible = false;
                        break;
                    }

                    cmbOption1.Visible = true;
                    cmbOption2.Visible = true;
                    cmbOption3.Visible = true;
                    cmbOption4.Visible = true;
                    cmbOption5.Visible = true;
                    cbOption1.Visible = true;
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption4"].Visible = true;
                    panel.Controls["lblOption5"].Visible = true;

                    cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex * 6]);
                    cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[(currentIndex * 6) + 1]);
                    cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[(currentIndex * 6) + 2]);
                    cmbOption4.SelectedIndex = cmbOption4.Items.IndexOf(Weapons[(currentIndex * 6) + 3]);
                    cmbOption5.SelectedIndex = cmbOption5.Items.IndexOf(Weapons[(currentIndex * 6) + 4]);

                    if (CalcPoints(-2) == UnitSize / 3 && Weapons[(currentIndex * 6) + 5] == "")
                    {
                        if(UnitSize != 2)
                        {
                            cbOption1.Enabled = false;
                        }
                    }
                    else
                    {
                        cbOption1.Enabled = true;
                    }

                    if (Weapons[(currentIndex * 6) + 5] != "")
                    {
                        cbOption1.Checked = true;
                    }
                    else
                    {
                        cbOption1.Checked = false;
                    }

                    if (currentIndex == 0)
                    {
                        panel.Controls["lblFactionupgrade"].Visible = true;
                        cmbFaction.Visible = true;
                    }
                    else
                    {
                        panel.Controls["lblFactionupgrade"].Visible = false;
                        cmbFaction.Visible = false;
                    }

                    break;
            }

            Points = (DEFAULT_POINTS * UnitSize) + CalcPoints(-1);

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Crisis Bodyguards - " + Points + "pts";
        }

        private int CalcPoints(int index)
        {
            int points = 0;
            int irridium = 0;
            int[] weapons = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

            if (index <= -1)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    if (i % 6 == 0)
                    {
                        weapons = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
                    }

                    if (Weapons[i] == "Airbursting Fragmentation Projector (+10/+15/+20 pts)")
                    {
                        weapons[0] += 1;
                        if (weapons[0] == 1)
                        {
                            points += 10;
                        }
                        else if (weapons[0] == 2)
                        {
                            points += 15;
                        }
                        else if (weapons[0] > 2)
                        {
                            points += 20;
                        }
                    }
                    else if (Weapons[i] == "Burst Cannon (+5/+10/+20 pts)")
                    {
                        weapons[1] += 1;
                        if (weapons[1] == 1)
                        {
                            points += 5;
                        }
                        else if (weapons[1] == 2)
                        {
                            points += 10;
                        }
                        else if (weapons[1] > 2)
                        {
                            points += 20;
                        }
                    }
                    else if (Weapons[i] == "Cyclic Ion Blaster (+10/+20/+25 pts)")
                    {
                        weapons[2] += 1;
                        if (weapons[2] == 1)
                        {
                            points += 10;
                        }
                        else if (weapons[2] == 2)
                        {
                            points += 20;
                        }
                        else if (weapons[2] > 2)
                        {
                            points += 25;
                        }
                    }
                    else if (Weapons[i] == "Fusion Blaster (+10/+15/+25 pts)")
                    {
                        weapons[3] += 1;
                        if (weapons[3] == 1)
                        {
                            points += 10;
                        }
                        else if (weapons[3] == 2)
                        {
                            points += 15;
                        }
                        else if (weapons[3] > 2)
                        {
                            points += 25;
                        }
                    }
                    else if (Weapons[i] == "Missile Pod (+10/+15/+20 pts)")
                    {
                        weapons[4] += 1;
                        if (weapons[4] == 1)
                        {
                            points += 10;
                        }
                        else if (weapons[4] == 2)
                        {
                            points += 15;
                        }
                        else if (weapons[4] > 2)
                        {
                            points += 20;
                        }
                    }
                    else if (Weapons[i] == "Plasma Rifle (+10/+15/+20 pts)")
                    {
                        weapons[5] += 1;
                        if (weapons[5] == 1)
                        {
                            points += 10;
                        }
                        else if (weapons[5] == 2)
                        {
                            points += 15;
                        }
                        else if (weapons[5] > 2)
                        {
                            points += 20;
                        }
                    }
                    else if (Weapons[i] == "T'au Flamer (+5/+10/+15 pts)")
                    {
                        weapons[6] += 1;
                        if (weapons[6] == 1)
                        {
                            points += 5;
                        }
                        else if (weapons[6] == 2)
                        {
                            points += 10;
                        }
                        else if (weapons[6] >= 2)
                        {
                            points += 15;
                        }
                    }
                    else if (Weapons[i] == "Gun Drone (+10 pts)")
                    {
                        points += 10;
                    }
                    else if (Weapons[i] == "Iridium Battlesuit (+10 pts)")
                    {
                        points += 10;
                        irridium += 1;
                    }
                    else if (Weapons[i] == "Marker Drone (+10 pts)")
                    {
                        points += 10;
                    }
                    else if (Weapons[i] == "Shield Drone (+15 pts)")
                    {
                        points += 15;
                    }
                    else if (Weapons[i] == "Shield Generator (+5 pts)")
                    {
                        points += 5;
                    }
                }

                if (index == -2)
                {
                    return irridium;
                }

                return points;
            }

            for (int i = 6 * index; i < 6 * (index + 1); i++)
            {
                if (Weapons[i] == "Airbursting Fragmentation Projector (+10/+15/+20 pts)")
                {
                    weapons[0] += 1;
                    if (weapons[0] == 1)
                    {
                        points += 10;
                    }
                    else if (weapons[0] == 2)
                    {
                        points += 15;
                    }
                    else if (weapons[0] > 2)
                    {
                        points += 20;
                    }
                }
                else if (Weapons[i] == "Burst Cannon (+5/+10/+20 pts)")
                {
                    weapons[1] += 1;
                    if (weapons[1] == 1)
                    {
                        points += 5;
                    }
                    else if (weapons[1] == 2)
                    {
                        points += 10;
                    }
                    else if (weapons[1] > 2)
                    {
                        points += 20;
                    }
                }
                else if (Weapons[i] == "Cyclic Ion Blaster (+10/+20/+25 pts)")
                {
                    weapons[2] += 1;
                    if (weapons[2] == 1)
                    {
                        points += 10;
                    }
                    else if (weapons[2] == 2)
                    {
                        points += 20;
                    }
                    else if (weapons[2] > 2)
                    {
                        points += 25;
                    }
                }
                else if (Weapons[i] == "Fusion Blaster (+10/+15/+25 pts)")
                {
                    weapons[3] += 1;
                    if (weapons[3] == 1)
                    {
                        points += 10;
                    }
                    else if (weapons[3] == 2)
                    {
                        points += 15;
                    }
                    else if (weapons[3] > 2)
                    {
                        points += 25;
                    }
                }
                else if (Weapons[i] == "Missile Pod (+10/+15/+20 pts)")
                {
                    weapons[4] += 1;
                    if (weapons[4] == 1)
                    {
                        points += 10;
                    }
                    else if (weapons[4] == 2)
                    {
                        points += 15;
                    }
                    else if (weapons[4] > 2)
                    {
                        points += 20;
                    }
                }
                else if (Weapons[i] == "Plasma Rifle (+10/+15/+20 pts)")
                {
                    weapons[5] += 1;
                    if (weapons[5] == 1)
                    {
                        points += 10;
                    }
                    else if (weapons[5] == 2)
                    {
                        points += 15;
                    }
                    else if (weapons[5] > 2)
                    {
                        points += 20;
                    }
                }
                else if (Weapons[i] == "T'au Flamer (+5/+10/+15 pts)")
                {
                    weapons[6] += 1;
                    if (weapons[6] == 1)
                    {
                        points += 5;
                    }
                    else if (weapons[6] == 2)
                    {
                        points += 10;
                    }
                    else if (weapons[6] >= 2)
                    {
                        points += 15;
                    }
                }
                else if (Weapons[i] == "Gun Drone (+10 pts)")
                {
                    points += 10;
                }
                else if (Weapons[i] == "Iridium Battlesuit (+10 pts)")
                {
                    points += 10;
                }
                else if (Weapons[i] == "Marker Drone (+10 pts)")
                {
                    points += 10;
                }
                else if (Weapons[i] == "Shield Drone (+15 pts)")
                {
                    points += 15;
                }
                else if (Weapons[i] == "Shield Generator (+5 pts)")
                {
                    points += 5;
                }
            }

            return 45 + points;
        }
    }
}
