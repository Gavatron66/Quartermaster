using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
    public class Stormboyz : Datasheets
    {
        public Stormboyz()
        {
            DEFAULT_POINTS = 10;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "N1k";
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "ORKS", "<CLAN>",
                "INFANTRY", "JUMP PACK", "FLY", "MOB", "CORE", "STORMBOYZ"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new Stormboyz();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Orks;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 15;
            nudUnitSize.Value = currentSize;

            cbOption1.Text = "Boss Nob Power Klaw (+10 pts)";
            if (Weapons.Contains(""))
            {
                cbOption1.Checked = false;
            }
            else
            {
                cbOption1.Checked = true;
            }

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
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[0] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[0] = "";
                    }
                    break;
                case 30:
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;
            if (Weapons[0] != "")
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Stormboyz - " + Points + "pts";
        }
    }
}
