using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DG_ChaosSpawn : Datasheets
    {
        public DG_ChaosSpawn()
        {
            UnitSize = 1;
            DEFAULT_POINTS = 21;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N1k";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "BEAST", "CHAOS SPAWN"
            });
            Role = "Fast Attack";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as DeathGuard;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 5;
            nudUnitSize.Value = currentSize;

            cbOption1.Visible = false;
            cbStratagem3.Text = repo.StratagemList[3];
            cbStratagem3.Visible = true;
            cbStratagem3.Location = new System.Drawing.Point(cbOption1.Location.X, cbOption1.Location.Y);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            switch (code)
            {
                case 30:
                    UnitSize = int.Parse(nud.Value.ToString());
                    Points = UnitSize * DEFAULT_POINTS;
                    break;
                case 73:
                    if (cbStratagem3.Checked)
                    {
                        if (!Stratagem.Contains(cbStratagem3.Text))
                        {
                            Stratagem.Add(cbStratagem3.Text);
                        }
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
        }

        public override string ToString()
        {
            return "Chaos Spawn - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new DG_ChaosSpawn();
        }
    }
}

