using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Space_Marines.Deathwatch
{
    public class FuriosoDreadnought : Datasheets
    {
        public FuriosoDreadnought()
        {
            DEFAULT_POINTS = 120;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m1k";
            Weapons.Add("Heavy Frag Cannon");
            Weapons.Add("Furioso Fist w/ Storm Bolter");
            Weapons.Add("Smoke Launchers");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS ASTARTES", "BLOOD ANGELS",
                "VEHICLE", "DREADNOUGHT", "FURIOSO DREADNOUGHT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new FuriosoDreadnought();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as SpaceMarines;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Furioso Fist w/ Heavy Flamer",
                "Furioso Fist w/ Meltagun",
                "Furioso Fist w/ Storm Bolter",
                "Heavy Frag Cannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[]
            {
                "Furioso Fist w/ Heavy Flamer",
                "Furioso Fist w/ Storm Bolter",
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[]
            {
                "Magna-Grapple",
                "Smoke Launchers"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

            cbOption1.Text = "Replace both Furioso Fists w/ Blood Talons";
            if (Weapons[3] == cbOption1.Text)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            Weapons[0] = cmbOption1.SelectedItem.ToString();

            if (Weapons[0].Contains("Storm Bolter"))
            {
                cmbOption2.Enabled = false;
            }
            else
            {
                cmbOption2.Enabled = true;
            }

            restrictedIndexes.Clear();
            if (Weapons[1].Contains("Storm Bolter"))
            {
                restrictedIndexes.Add(2);
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;

            switch (code)
            {
                case 11:
                    if(!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[0] = cmbOption1.SelectedItem.ToString();

                        if (Weapons[0].Contains("Storm Bolter"))
                        {
                            cmbOption2.Enabled = false;
                        }
                        else
                        {
                            cmbOption2.Enabled = true;
                        }

                        if (Weapons[0] == "Heavy Frag Cannon")
                        {
                            cbOption1.Enabled = false;
                        }
                        else
                        {
                            cbOption1.Enabled = true;
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);
                    }
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();

                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[3] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[3] = "";
                    }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;

            restrictedIndexes.Clear();
            if (Weapons[1].Contains("Storm Bolter"))
            {
                restrictedIndexes.Add(2);
            }
            if(cbOption1.Checked)
            {
                restrictedIndexes.Add(3);
            }

            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
        }

        public override string ToString()
        {
            return "Furioso Dreadnought - " + Points + "pts";
        }
    }
}