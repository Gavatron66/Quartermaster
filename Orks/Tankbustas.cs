using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Tankbustas : Datasheets
    {
        public Tankbustas()
        {
            DEFAULT_POINTS = 16;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "4N";
            Weapons.Add("0"); //Tankhammers
            Weapons.Add("0"); //Rokkit Pistols
            Weapons.Add("0"); //Bomb Squigs
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "INFANTRY", "MOB", "CORE", "TANKBUSTA BOMBS", "TANKBUSTAS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Tankbustas();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Orks;
            Template.LoadTemplate(TemplateCode, panel);

            Label lblnud1 = panel.Controls["lblnud1"] as Label;
            Label lblnud2 = panel.Controls["lblnud2"] as Label;
            Label lblnud3 = panel.Controls["lblnud3"] as Label;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            lblnud1.Text = "Tankhammers (1/5x models):";
            lblnud1.Location = new System.Drawing.Point(lblnud1.Location.X - 40, lblnud1.Location.Y);
            
            lblnud2.Text = "Rokkit Pistols (1/5x models):";
            lblnud2.Location = new System.Drawing.Point(lblnud2.Location.X - 35, lblnud2.Location.Y);

            lblnud3.Text = "Bomb Squigs (+5 pts):";
            //lblnud3.Location = new System.Drawing.Point(lblnud3.Location.X + 10, lblnud3.Location.Y);

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 15;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = 0;
            nudOption1.Maximum = 1;
            nudOption1.Value = Convert.ToDecimal(Weapons[0]);

            nudOption2.Minimum = 0;
            nudOption2.Value = 0;
            nudOption2.Maximum = 1;
            nudOption2.Value = Convert.ToDecimal(Weapons[1]);

            nudOption3.Minimum = 0;
            nudOption3.Value = 0;
            nudOption3.Maximum = 2;
            nudOption3.Value = Convert.ToDecimal(Weapons[2]);

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
            NumericUpDown nudOption2 = panel.Controls["nudOption2"] as NumericUpDown;
            NumericUpDown nudOption3 = panel.Controls["nudOption3"] as NumericUpDown;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    nudOption1.Maximum = UnitSize / 5;
                    nudOption2.Maximum = UnitSize / 5;
                    break;
                case 31:
                    Weapons[0] = nudOption1.Value.ToString();
                    break;
                case 32:
                    Weapons[1] = nudOption2.Value.ToString();
                    break;
                case 33:
                    Weapons[2] = nudOption3.Value.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += (int)nudOption3.Value * 5;
        }

        public override string ToString()
        {
            return "Tankbustas - " + Points + "pts";
        }
    }
}
