using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Blood_Angels
{
    public class PrimarisDeathCompany : Datasheets
    {
        bool loading;

        public PrimarisDeathCompany()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2N1mS(2m)";
            Weapons.Add("Bolt Rifle"); //Squad Option
            Weapons.Add("0"); //Astartes Grenade Launchers
            Weapons.Add("Bolt Rifle"); //Psuedo-Sergeant Weapons
            Weapons.Add("(None)"); //
            Weapons.Add(""); //Second Heavy Bolt Pistol Exchange

            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "BLOOD ANGELS",
                "INFANTRY", "CORE", "PRIMARIS", "DEATH COMPANY", "INTERCESSORS", "DEATH COMPANY INTERCESSORS"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new PrimarisDeathCompany();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);
            loading = true;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gb.Controls["gb_cmbOption2"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            panel.Controls["lblnud1"].Text = "Astartes Grenade Launchers (1x/5 models):";
            panel.Controls["lblnud1"].Location = new System.Drawing.Point(88, 95);

            gb.Text = "One model may be equipped with the following:";

            nudOption1.Location = new System.Drawing.Point(404, 93);
            panel.Controls["lblExtra1"].Location = new System.Drawing.Point(gb.Location.X, gb.Location.Y + gb.Height + 22);
            panel.Controls["lblExtra1"].Text = "One model may have their Heavy Bolt Pistol exchanged for the following:";
            panel.Controls["lblExtra1"].Visible = true;
            cmbOption2.Location = new System.Drawing.Point(cmbOption1.Location.X, gb.Location.Y + gb.Height + 54);
            cmbOption2.Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new object[]
            {
                "Auto Bolt Rifle",
                "Bolt Rifle",
                "Heavy Bolt Pistol and Astartes Chainsword",
                "Stalker Bolt Rifle"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new object[]
            {
                "Hand Flamer",
                "Heavy Bolt Pistol",
                "Plasma Pistol"
            });

            if (Weapons[0] == "Heavy Bolt Pistol and Astartes Chainsword")
            {
                panel.Controls["lblExtra1"].Visible = true;
                cmbOption2.Visible = true;
                cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[4]);
            }
            else
            {
                panel.Controls["lblExtra1"].Visible = false;
                cmbOption2.Visible = false;
            }

            currentSize = Convert.ToInt32(Weapons[1]);
            nudOption1.Minimum = 0;
            nudOption1.Maximum = 2;
            nudOption1.Value = currentSize;
            if (UnitSize < 10)
            {
                nudOption1.Maximum--;
            }

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new object[]
            {
                "Astartes Chainsword",
                "Bolt Rifle",
                "Hand Flamer",
                "Plasma Pistol",
                "Power Sword",
            });

            antiLoop = true;
            if (Weapons[0] == "Stalker Bolt Rifle")
            {
                gb_cmbOption1.Items.Insert(2, Weapons[0]);

                if (Weapons[2].Contains("Bolt Rifle"))
                {
                    gb_cmbOption1.SelectedIndex = 2;
                }

                gb_cmbOption1.Items.RemoveAt(0);
            }
            else
            {
                gb_cmbOption1.Items.Insert(1, Weapons[0]);

                if (Weapons[2].Contains("Bolt Rifle"))
                {
                    gb_cmbOption1.SelectedIndex = 1;
                }

                gb_cmbOption1.Items.RemoveAt(1);
            }
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[2]);

            gb_cmbOption2.Items.Clear();
            gb_cmbOption2.Items.AddRange(new object[]
            {
                "(None)",
                "Astartes Chainsword",
                "Power Fist",
                "Power Sword",
                "Thunder Hammer"
            });
            gb_cmbOption2.SelectedIndex = gb_cmbOption2.Items.IndexOf(Weapons[3]);


            loading = false;
        }


        public override void SaveDatasheets(int code, Panel panel)
        {
            if (loading)
            {
                return;
            }

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gb.Controls["gb_cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    string temp = Weapons[0];

                    Weapons[0] = cmbOption1.SelectedItem.ToString();

                    if (temp.Contains("Heavy Bolt Pistol"))
                    {
                        gb_cmbOption1.Items.Remove("Heavy Bolt Pistol");
                    }
                    else
                    {
                        gb_cmbOption1.Items.Remove(temp);
                    }

                    if (Weapons[0] == "Stalker Bolt Rifle")
                    {
                        gb_cmbOption1.Items.Insert(4, Weapons[0]);

                        if (Weapons[2].Contains("Bolt Rifle") || Weapons[2].Contains("Heavy Bolt Pistol"))
                        {
                            gb_cmbOption1.SelectedIndex = 4;
                        }
                    }
                    else if (Weapons[0].Contains("Heavy Bolt Pistol"))
                    {
                        gb_cmbOption1.Items.Insert(2, "Heavy Bolt Pistol");

                        if (Weapons[2].Contains("Bolt Rifle") || Weapons[2].Contains("Heavy Bolt Pistol"))
                        {
                            gb_cmbOption1.SelectedIndex = 2;
                        }

                        if (Weapons[3] == "(None)")
                        {
                            gb_cmbOption2.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        gb_cmbOption1.Items.Insert(1, Weapons[0]);

                        if (Weapons[2].Contains("Bolt Rifle") || Weapons[2].Contains("Heavy Bolt Pistol"))
                        {
                            gb_cmbOption1.SelectedIndex = 1;
                        }
                    }

                    restrictedIndexes.Clear();
                    if (Weapons[0].Contains("Heavy Bolt Pistol"))
                    {
                        panel.Controls["lblExtra1"].Visible = true;
                        cmbOption2.Visible = true;

                        cmbOption2.SelectedIndex = 1;

                        restrictedIndexes.AddRange(new int[] { 0, 4 });
                    }
                    else
                    {
                        panel.Controls["lblExtra1"].Visible = false;
                        cmbOption2.Visible = false;

                        cmbOption2.SelectedIndex = -1;
                        Weapons[4] = "";
                    }
                    this.DrawItemWithRestrictions(restrictedIndexes, gb_cmbOption1);

                    break;
                case 12:
                    if(cmbOption2.SelectedIndex != -1)
                    {
                        Weapons[4] = cmbOption2.SelectedItem.ToString();
                    }
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize == 10)
                    {
                        nudOption1.Maximum = 2;
                    }

                    if (UnitSize < 10 && nudOption1.Value == 2)
                    {
                        nudOption1.Value--;
                        nudOption1.Maximum = 1;
                    }

                    break;
                case 31:
                    Weapons[1] = nudOption1.Value.ToString();
                    break;
                case 411:
                    if(!restrictedIndexes.Contains(gb_cmbOption1.SelectedIndex))
                    {
                        Weapons[2] = gb_cmbOption1.SelectedItem.ToString();
                    }
                    else
                    {
                        gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[2]);
                    }
                    break;
                case 412:
                    Weapons[3] = gb_cmbOption2.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Death Company Intercessors - " + Points + "pts";
        }
    }
}