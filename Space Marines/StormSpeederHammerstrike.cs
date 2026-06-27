using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class StormSpeederHammerstrike : Datasheets
    {
        public StormSpeederHammerstrike()
        {
            DEFAULT_POINTS = 125;
            UnitSize = 1;
            Points = DEFAULT_POINTS * UnitSize;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "FLY", "STORM SPEEDER", "HAMMERSTRIKE"
            });
            Role = "Fast Attack";
        }

        public override Datasheets CreateUnit()
        {
            return new StormSpeederHammerstrike();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;

            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;
            cbStratagem5.Location = new System.Drawing.Point(86, 29);

            if (repo.customSubFactionTraits[2] == "Blood Angels")
            {
                cbStratagem5.Visible = true;
                cbStratagem5.Text = "Stratagem: Lucifer-pattern Engine";
            }
            else
            {
                cbStratagem5.Visible = false;
                if (Stratagem.Contains(cbStratagem5.Text))
                {
                    Stratagem.Remove(cbStratagem5.Text);
                }
            }

            if (Stratagem.Contains(cbStratagem5.Text))
            {
                cbStratagem5.Checked = true;
                cbStratagem5.Enabled = true;
            }
            else
            {
                cbStratagem5.Checked = false;
                cbStratagem5.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem5.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            switch (code)
            {
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        Stratagem.Add(cbStratagem5.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            Stratagem.Remove(cbStratagem5.Text);
                        }
                    }
                    break;
            }
        }

        public override string ToString()
        {
            return "Storm Speeder Hammerstrike - " + Points + "pts";
        }
    }
}
