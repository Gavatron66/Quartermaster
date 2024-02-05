using Roster_Builder.Death_Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Tyrannofex : Datasheets
    {
        public Tyrannofex()
        {
            DEFAULT_POINTS = 185;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m2k";
            Weapons.Add("Fleshborer Hive");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "MONSTER", "HORNED CHITIN", "TYRANNOFEX"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new Tyrannofex();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Tyranids;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Acid Spray (+20 pts)",
                "Fleshborer Hive",
                "Rupture Cannon (+20 pts)"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Adrenal Glands (+10 pts)";
            if (Weapons[1] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Toxin Sacs (+5 pts)";
            if (Weapons[2] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
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
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if(Weapons.Contains("Acid Spray (+20 pts)") || Weapons.Contains("Rupture Cannon (+20 pts)"))
            {
                Points += 20;
            }
            if (Weapons.Contains("Adrenal Glands (+10 pts)"))
            {
                Points += 10;
            }
            if (Weapons.Contains("Toxin Sacs (+5 pts)"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Tyrannofex - " + Points + "pts";
        }
    }
}
