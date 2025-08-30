using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Venomthropes : Datasheets
    {
        public Venomthropes()
        {
            UnitSize = 3;
            DEFAULT_POINTS = 40;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "INFANTRY", "FLY", "CORE", "SPORECASTER", "TOXIC LASHES", "FEEDER TENDRILS", "VENOMTHROPES"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Venomthropes();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 3;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 6;
            nudUnitSize.Value = currentSize;

            if (repo.currentSubFaction == "Jormungandr")
            {
                cbStratagem3.Visible = true;
            }
            else
            {
                cbStratagem3.Visible = false;
            }

            cbStratagem3.Location = new System.Drawing.Point(nudUnitSize.Location.X, nudUnitSize.Location.Y + 32);
            cbStratagem3.Text = f.StratagemList[2];

            if (Stratagem.Contains(cbStratagem3.Text))
            {
                cbStratagem3.Checked = true;
                cbStratagem3.Enabled = true;
            }
            else
            {
                cbStratagem3.Checked = false;
                cbStratagem3.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem3.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nud.Value.ToString());
                    break;
                case 73:
                    if (cbStratagem3.Checked)
                    {
                        Stratagem.Add(cbStratagem3.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Remove(cbStratagem3.Text);
                        }
                    }
                    break;
            }

            Points = UnitSize * DEFAULT_POINTS;

        }

        public override string ToString()
        {
            return "Venomthropes - " + Points + "pts";
        }
    }
}
