using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Deffkoptas : Datasheets
    {
        public Deffkoptas()
        {
            DEFAULT_POINTS = 50;
            UnitSize = 3;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "2N";
            Weapons.Add("0");
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "VEHICLE", "FLY", "SPEED FREEKS", "DEFFKOPTAS"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Deffkoptas();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Orks;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            NumericUpDown nudOption1 = panel.Controls["nudOption1"] as NumericUpDown;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            nudOption1.Minimum = 0;
            nudOption1.Value = nudOption1.Minimum;
            nudOption1.Maximum = UnitSize / 3;
            nudOption1.Value = Convert.ToInt32(Weapons[0]);
            panel.Controls["lblnud1"].Text = "Kustom Mega-blasta(s) and Big Bomb(s) (+5 pts, 1/3x models):";
            nudOption1.Location = new System.Drawing.Point(nudOption1.Location.X - 15, nudOption1.Location.Y + 25);

            panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(panel.Controls["lblFactionupgrade"].Location.X, panel.Controls["lblFactionupgrade"].Location.Y + 25);
            cmbFaction.Location = new System.Drawing.Point(cmbFaction.Location.X, cmbFaction.Location.Y + 25);

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
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    nudOption1.Maximum = UnitSize / 3;
                    break;
                case 31:
                    Weapons[0] = nudOption1.Value.ToString();
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            Points += int.Parse(Weapons[0]) * 5;
        }

        public override string ToString()
        {
            return "Deffkoptas - " + Points + "pts";
        }
    }
}
