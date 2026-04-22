using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Drukhari
{
    public class Wracks : Datasheets
    {
        int currentIndex;

        public Wracks()
        {
            DEFAULT_POINTS = 9;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Wrack Blades");
            Weapons.Add("(None)");
            for (int i = 1; i < UnitSize; i += 5)
            {
                Weapons.Add("(None)");
            }
            Keywords.AddRange(new string[]
            {
                "AELDARI", "DRUKHARI", "<HAEMONCULUS COVEN>",
                "INFANTRY", "CORE", "WRACKS"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Wracks();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Drukhari;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 20;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            if (Weapons[1] == "(None)")
            {
                lbModelSelect.Items.Add("Acothyst w/ " + Weapons[0]);
            }
            else
            {
                lbModelSelect.Items.Add("Acothyst w/ " + Weapons[0] + " and " + Weapons[1]);
            }

            for (int i = 1; i < (UnitSize / 5) + 1; i++)
            {
                if (Weapons[i+1] == "(None)")
                {
                    lbModelSelect.Items.Add("Wrack");
                }
                else
                {
                    lbModelSelect.Items.Add("Wrack w/ " + Weapons[i + 1]);
                }
            }

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
            panel.Controls["lblFactionUpgrade"].Text = "Favoured Retinue";

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            panel.Controls["lblFactionUpgrade"].Visible = true;
            cmbFaction.Visible = true;
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
            ComboBox cmbFactionupgrade = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    if (currentIndex == 0)
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[1] == "(None)")
                        {
                            lbModelSelect.Items[0] = "Acothyst w/ " + Weapons[0];
                        }
                        else
                        {
                            lbModelSelect.Items[0] = "Acothyst w/ " + Weapons[0] + " and " + Weapons[1];
                        }
                        break;
                    }

                    Weapons[currentIndex + 1] = cmbOption1.SelectedItem.ToString();

                    if (Weapons[currentIndex + 1] == "(None)")
                    {
                        lbModelSelect.Items[currentIndex] = "Wrack";
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Wrack w/ " + Weapons[currentIndex + 1];
                    }

                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();

                    if (Weapons[1] == "(None)")
                    {
                        lbModelSelect.Items[0] = "Acothyst w/ " + Weapons[0];
                    }
                    else
                    {
                        lbModelSelect.Items[0] = "Acothyst w/ " + Weapons[0] + " and " + Weapons[1];
                    }
                    break;
                case 16:
                    Factionupgrade = cmbFactionupgrade.Text;
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        if(lbModelSelect.Items.Count < (UnitSize / 5) + 1)
                        {
                            Weapons.Add("(None)");
                            lbModelSelect.Items.Add("Wrack");
                        }
                    }

                    if (temp > UnitSize)
                    {
                        if (lbModelSelect.Items.Count > (UnitSize / 5) + 1)
                        {
                            lbModelSelect.Items.RemoveAt(temp / 5);
                            Weapons.RemoveRange((UnitSize / 5), 1);
                        }
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                        antiLoop = false;
                        break;
                    }

                    if (currentIndex == 0)
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Agoniser (+5 pts)",
                            "Electrocorrosive Whip (+5 pts)",
                            "Flesh Gauntlet (+5 pts)",
                            "Mindphase Gauntlet (+5 pts)",
                            "Scissorhand (+10 pts)",
                            "Venom Blade (+5 pts)",
                            "Wrack Blades"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        cmbOption2.Items.Clear();
                        cmbOption2.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Hexrifle (+5 pts)",
                            "Liquifier Gun (+10 pts)",
                            "Stinger Pistol (+5 pts)"
                        });
                        cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                        antiLoop = false;
                        break;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = true;
                        panel.Controls["lblOption2"].Visible = false;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "(None)",
                            "Liquifier Gun (+10 pts)",
                            "Ossefactor (+5 pts)"
                        });

                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex + 1]);
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            foreach (var item in Weapons)
            {
                if(item == "Agoniser (+5 pts)" || item == "Electrocorrosive Whip (+5 pts)" || item == "Flesh Gauntlet (+5 pts)"
                    || item == "Hexrifle (+5 pts)" || item == "Mindphase Gauntlet (+5 pts)" || item == "Ossefactor (+5 pts)"
                    || item == "Stinger Pistol (+5 pts)" || item == "Venom Blade (+5 pts)")
                {
                    Points += 5;
                }

                if(item == "Liquifier Gun (+10 pts)" || item == "Scissorhand (+10 pts)")
                {
                    Points += 10;
                }
            }

            Points += repo.GetFactionUpgradePoints(Factionupgrade) * UnitSize;
        }

        public override string ToString()
        {
            return "Wracks - " + Points + "pts";
        }
    }
}