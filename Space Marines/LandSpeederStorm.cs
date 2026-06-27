using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines
{
    public class LandSpeederStorm : Datasheets
    {
        public LandSpeederStorm()
        {
            Points = 50;
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "<CHAPTER>",
                "VEHICLE", "LAND SPEEDER", "SCOUT", "FLY", "TRANSPORT", "LAND SPEEDER STORM"
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new LandSpeederStorm();
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
            return "Land Speeder Storm - " + Points + "pts";
        }
    }
}
