using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Custodes
{
    public class VCDreadnought : Datasheets
    {
        public VCDreadnought()
        {
            DEFAULT_POINTS = 155;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m";
            Weapons.Add("Multi-melta");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS CUSTODES", "<SHIELD HOST>",
                "VEHICLE", "CORE", "DREADNOUGHT", "VENERABLE CONTEMPTOR DREADNOUGHT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new VCDreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as AdeptusCustodes;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Kheres-pattern Assault Cannon",
                "Multi-melta"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            cbStratagem3.Location = new System.Drawing.Point(cmbOption1.Location.X, cmbOption1.Location.Y + 32);
            cbStratagem3.Visible = true;
            cbStratagem3.Text = repo.StratagemList[3];
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem as string;
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

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Venerable Contemptor Dreadnought - " + Points + "pts";
        }
    }
}
