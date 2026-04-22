using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class FlashGitz : Datasheets
    {
        public FlashGitz()
        {
            DEFAULT_POINTS = 20;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "2NS(1m1k)";
            Weapons.Add("(None)");
            Weapons.Add("");
            Weapons.Add("0");
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "INFANTRY", "MOB", "FLASH GITZ"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new FlashGitz();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Orks;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            CheckBox cbLeaderOption1 = gb.Controls["cbLeaderOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            panel.Controls["lblnud1"].Text = "Ammo Runts (1/5x models, +5pts each):";
            panel.Controls["lblnud1"].Location = new System.Drawing.Point(panel.Controls["lblnud1"].Location.X - 75, panel.Controls["lblnud1"].Location.Y);
            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X + 35, nudOption1.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Maximum = 1;
            nudOption1.Value = Convert.ToUInt32(Weapons[2]);

            gb.Text = "Kaptin";

            cbLeaderOption1.Text = "Gitfinda Squig (+5 pts)";
            if (Weapons[1] == cbLeaderOption1.Text)
            {
                cbLeaderOption1.Checked = true;
            }
            else
            {
                cbLeaderOption1.Checked = false;
            }

            gb_cmbOption1.Items.Clear();
            gb_cmbOption1.Items.AddRange(new string[]
            {
                "(None)",
                "Choppa",
                "Slugga"
            });
            gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[0]);

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
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            CheckBox cbLeaderOption1 = gb.Controls["cbLeaderOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
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
                    Weapons[2] = nudOption1.Value.ToString();
                    break;
                case 411:
                    Weapons[0] = gb_cmbOption1.SelectedItem.ToString();
                    break;
                case 421:
                    if (cbLeaderOption1.Checked)
                    {
                        Weapons[1] = cbLeaderOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += (int)nudOption1.Value * 5;
            if(cbLeaderOption1.Checked)
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Flash Gitz - " + Points + "pts";
        }
    }
}
