using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class InvaderATVs : Datasheets
    {
        public InvaderATVs()
        {
            DEFAULT_POINTS = 70;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "3N";
            Weapons.Add("1"); //Onslaught Gatling Cannons
            Weapons.Add("0"); //Multi-meltas
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "BIKER", "PRIMARIS", "INVADER ATV SQUAD"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new InvaderATVs();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            panel.Controls["lblnud1"].Text = "Models with Onslaught Gatling Cannons:";
            panel.Controls["lblnud2"].Text = "Models with Multi-meltas:";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Maximum = nudUnitSize.Maximum;
            nudOption1.Value = 0;
            nudOption1.Location = new System.Drawing.Point(399, 59);

            nudOption2.Minimum = 0;
            nudOption2.Maximum = nudUnitSize.Maximum;
            nudOption2.Value = 0;
            nudOption2.Location = new System.Drawing.Point(399, 91);

            nudOption1.Value = int.Parse(Weapons[0]);
            nudOption2.Value = int.Parse(Weapons[1]);

            if (repo.currentSubFaction == "Black Templars")
            {
                //Relic Bearers Code
                panel.Controls["lblExtra1"].Visible = true;
                panel.Controls["lblExtra1"].Location = new System.Drawing.Point(nudOption2.Location.X, nudOption2.Location.Y + 30);
                panel.Controls["lblExtra1"].Text = "Relic Bearers";

                ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
                cmbFaction.Visible = true;
                cmbFaction.Location = new System.Drawing.Point(nudOption2.Location.X + 4, nudOption2.Location.Y + 54);
                cmbFaction.Items.Clear();
                cmbFaction.Items.AddRange(repo.GetFactionUpgrades(this.Keywords).ToArray());

                if (Factionupgrade != null)
                {
                    cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
                }
                else
                {
                    cmbFaction.SelectedIndex = 0;
                }

                cmbFaction.Visible = true;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    int oldSize = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    antiLoop = true;
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
                    antiLoop = false;
                    break;
                case 31:
                    int temp = Convert.ToInt32(Weapons[0]);
                    antiLoop = true;

                    if (nudOption1.Value > UnitSize)
                    {
                        nudOption1.Value--;
                    }
                    else if (temp < nudOption1.Value)
                    {
                        nudOption2.Value--;
                    }
                    else if (temp > nudOption1.Value)
                    {
                        nudOption2.Value++;
                    }
                    else if (temp != nudOption1.Value)
                    {
                        nudOption2.Value++;
                    }
                    antiLoop = false;

                    Weapons[0] = Convert.ToString(nudOption1.Value);
                    Weapons[1] = Convert.ToString(nudOption2.Value);
                    break;
                case 32:
                    int temp2 = Convert.ToInt32(Weapons[1]);
                    antiLoop = true;

                    if (nudOption2.Value > UnitSize)
                    {
                        nudOption2.Value--;
                    }
                    else if (temp2 < nudOption2.Value)
                    {
                        nudOption1.Value--;
                    }
                    else if (temp2 > nudOption2.Value)
                    {
                        nudOption1.Value++;
                    }
                    else if (temp2 != nudOption2.Value)
                    {
                        nudOption1.Value++;
                    }
                    antiLoop = false;

                    Weapons[0] = Convert.ToString(nudOption1.Value);
                    Weapons[1] = Convert.ToString(nudOption2.Value);
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;
            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Invader ATV Squad - " + Points + "pts";
        }
    }
}
