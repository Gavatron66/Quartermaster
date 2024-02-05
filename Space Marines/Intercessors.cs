using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class Intercessors : Datasheets
    {
        bool loading;

        public Intercessors()
        {
            DEFAULT_POINTS = 18;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2N1mS(2m)";
            Weapons.Add("Bolt Rifle"); //Squad Option
            Weapons.Add("0"); //Astartes Grenade Launchers
            Weapons.Add("Bolt Rifle"); //Sergeant Weapons
            Weapons.Add("(None)"); //

            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "INFANTRY", "CORE", "PRIMARIS", "INTERCESSORS", "INTERCESSOR SQUAD"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new Intercessors();
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

            panel.Controls["lblnud1"].Text = "Number of Astartes Grenade Launchers:";
            panel.Controls["lblnud1"].Location = new System.Drawing.Point(88, 95);

            gb.Text = "Intercessor Sergeant";

            nudOption1.Location = new System.Drawing.Point(404, 93);

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
                "Stalker Bolt Rifle"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            currentSize = Convert.ToInt32(Weapons[1]);
            nudOption1.Minimum = 0;
            nudOption1.Maximum = 2;
            nudOption1.Value = currentSize;
            if(UnitSize < 10)
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
                "Power Sword"
            });
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
            if(loading)
            {
                return;
            }

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            GroupBox gb = panel.Controls["gbUnitLeader"] as GroupBox;
            ComboBox gb_cmbOption1 = gb.Controls["gb_cmbOption1"] as ComboBox;
            ComboBox gb_cmbOption2 = gb.Controls["gb_cmbOption2"] as ComboBox;

            switch(code)
            {
                case 11:
                    string temp = Weapons[0];

                    Weapons[0] = cmbOption1.SelectedItem.ToString();

                    gb_cmbOption1.Items.Remove(temp);

                    if (Weapons[0] == "Stalker Bolt Rifle")
                    {
                        gb_cmbOption1.Items.Insert(4, Weapons[0]);
                    }
                    else
                    {
                        gb_cmbOption1.Items.Insert(1, Weapons[0]);
                    }

                    if (Weapons[0].Contains("Bolt Rifle"))
                    {
                        gb_cmbOption1.SelectedIndex = gb_cmbOption1.Items.IndexOf(Weapons[0]);
                    }

                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if(UnitSize == 10)
                    {
                        nudOption1.Maximum = 2;
                    }

                    if(UnitSize < 10 && nudOption1.Value == 2)
                    {
                        nudOption1.Value--;
                        nudOption1.Maximum = 1;
                    }

                    break;
                case 31:
                    Weapons[1] = nudOption1.Value.ToString();
                    break;
                case 411:
                    Weapons[2] = gb_cmbOption1.SelectedItem.ToString();
                    break;
                case 412:
                    Weapons[3] = gb_cmbOption2.SelectedItem.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
        }

        public override string ToString()
        {
            return "Intercessor Squad - " + Points + "pts";
        }
    }
}
