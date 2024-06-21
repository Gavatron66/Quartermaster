using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Boyz : Datasheets
    {
        public Boyz()
        {
            DEFAULT_POINTS = 8;
            UnitSize = 10;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "cultist";
            Weapons.Add("9"); //Sluggas and Choppas
            Weapons.Add("0"); //Shootas
            Weapons.Add("0"); //Big Shootas
            Weapons.Add("0"); //Rokkit Launcha
            Weapons.Add("Slugga"); //Champion Weapon
            Weapons.Add("Choppa"); //Champion Weapon
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "INFANTRY", "MOB", "CORE", "TANKBUSTA BOMBS", "BOYZ"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Boyz();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Orks;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
            NumericUpDown nudOption4 = panel.Controls["nudOption4"] as NumericUpDown;

            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;
            Label lblnud3 = panel.Controls["lblnud3"] as Label;
            Label lblnud4 = panel.Controls["lblnud4"] as Label;
            Label lblExtra1 = panel.Controls["lblExtra1"] as Label;

            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gbUnitLeader.Controls["gb_cmbFactionupgrade"] as ComboBox;

            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X + 80, nudOption1.Location.Y);
            nudOption4.Location = new System.Drawing.Point(nudOption4.Location.X + 10, nudOption4.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 10;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 30;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;

            nudOption3.Minimum = 0;
            nudOption3.Maximum = nudUnitSize.Value / 10;
            nudOption3.Value = 0;

            nudOption4.Minimum = 0;
            nudOption4.Maximum = nudUnitSize.Value / 10;
            nudOption4.Value = 0;

            int temp = int.Parse(Weapons[0]);
            nudOption1.Value = temp;
            temp = int.Parse(Weapons[1]);
            nudOption2.Value = temp;
            temp = int.Parse(Weapons[2]);
            nudOption3.Value = temp;
            temp = int.Parse(Weapons[3]);
            nudOption4.Value = temp;

            lblnud1.Text = "Models with Sluggas and Choppas:";
            lblnud2.Text = "Models with Shootas:";
            lblExtra1.Text = "For every 10x models, one of the following may be taken:";
            lblnud3.Text = "Big Shoota:";
            lblnud4.Text = "Rokkit Launcha (+5 pts):";

            gbUnitLeader.Text = "Boss Nob";

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "Big Choppa (+5 pts)",
                "Choppa",
                "Killsaw (+10 pts)",
                "Kombi-rokkit (+5 pts)",
                "Kombi-skorcha (+5 pts)",
                "Power Klaw (+10 pts)",
                "Power Stabba (+5 pts)",
                "Slugga"
            });

            gb_cmbOption2.Items.Clear();
            gb_cmbOption2.Items.AddRange(new string[]
            {
                "Big Choppa (+5 pts)",
                "Choppa",
                "Killsaw (+10 pts)",
                "Power Klaw (+10 pts)",
                "Power Stabba (+5 pts)",
                "Slugga"
            });

            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[4]);
            gb_cmbOption2.SelectedIndex = gb_cmbOption2.Items.IndexOf(Weapons[5]);

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

            gbUnitLeader.Controls["gb_lblFactionupgrade"].Visible = true;
            gbUnitLeader.Controls["gb_lblFactionupgrade"].Text = "Select one of the following:";
            gbUnitLeader.Controls["gb_cmbFactionupgrade"].Visible = true;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;
            panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(gbUnitLeader.Location.X, 395);
            cmbFaction.Location = new System.Drawing.Point(gbUnitLeader.Location.X + 20, 417);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
            NumericUpDown nudOption4 = panel.Controls["nudOption4"] as NumericUpDown;

            GroupBox gbUnitLeader = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gbUnitLeader.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gbUnitLeader.Controls["gb_cmbFactionupgrade"] as ComboBox;

            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    int oldSize = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (UnitSize > oldSize)
                    {
                        nudOption1.Value += UnitSize - oldSize;
                    }

                    if (UnitSize < oldSize)
                    {
                        if (nudOption1.Value >= oldSize - UnitSize)
                        {
                            nudOption1.Value -= oldSize - UnitSize;
                        }
                        else
                        {
                            nudOption2.Value -= oldSize - UnitSize;
                        }
                    }
                    break;
                case 31:
                    if (nudOption1.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1)
                    {
                        Weapons[0] = Convert.ToString(nudOption1.Value);
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
                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1)
                    {
                        Weapons[1] = Convert.ToString(nudOption2.Value);
                    }
                    else
                    {
                        nudOption2.Value -= 1;
                    }
                    break;
                case 33:
                    if (nudOption3.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1
                        && nudOption3.Value + nudOption4.Value <= nudUnitSize.Value / 10)
                    {
                        Weapons[2] = Convert.ToString(nudOption3.Value);
                    }
                    else
                    {
                        nudOption3.Value -= 1;
                    }
                    break;
                case 34:
                    if (nudOption4.Value == 0)
                    {
                        break;
                    }
                    else if (nudOption1.Value + nudOption2.Value + nudOption3.Value + nudOption4.Value <= nudUnitSize.Value - 1
                        && nudOption3.Value + nudOption4.Value <= nudUnitSize.Value / 10)
                    {
                        Weapons[3] = Convert.ToString(nudOption4.Value);
                    }
                    else
                    {
                        nudOption4.Value -= 1;
                    }
                    break;
                case 411:
                    Weapons[4] = gb_cmbOption1.SelectedItem.ToString();
                    if (Weapons[4] == "Kombi-rokkit (+5 pts)" || Weapons[4] == "Kombi-skorcha (+5 pts)")
                    {
                        gb_cmbOption2.Enabled = false;
                        Weapons[5] = "";
                        gb_cmbOption2.SelectedIndex = -1;
                    }
                    else
                    {
                        gb_cmbOption2.Enabled = true;
                        gb_cmbOption2.SelectedIndex = 1;
                    }
                    break;
                case 416:
                    if(gb_cmbOption2.SelectedIndex == -1)
                    {
                        break;
                    }

                    Weapons[5] = gb_cmbOption2.SelectedItem.ToString();
                    break;
            }

            nudOption3.Maximum = nudUnitSize.Value / 10;
            nudOption4.Maximum = nudUnitSize.Value / 10;

            Points = DEFAULT_POINTS * UnitSize;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if(Weapons.Contains("Big Choppa (+5 pts)") || Weapons.Contains("Kombi-rokkit (+5 pts)")
                 || Weapons.Contains("Kombi-skorcha (+5 pts)") || Weapons.Contains("Power Stabba (+5 pts)"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Killsaw (+10 pts)") || Weapons.Contains("Power Klaw (+10 pts)"))
            {
                Points += 10;
            }

            Points += Convert.ToInt32(Weapons[3].ToString()) * 5;
        }

        public override string ToString()
        {
            return "Ork Boyz - " + Points + "pts";
        }
    }
}
