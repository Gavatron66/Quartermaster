using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class ScreamerKillers : Datasheets
    {
        int currentIndex;

        public ScreamerKillers()
        {
            DEFAULT_POINTS = 130;
            UnitSize = 1;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL3k";
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "HIVE TENDRIL", "TYRANIDS", "<HIVE FLEET>",
                "MONSTER", "CORE", "CARNIFEX", "SCREAMER-KILLER"
            });
            Role = "Heavy Support";
        }

        public override Datasheets CreateUnit()
        {
            return new ScreamerKillers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Tyranids;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 1;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 3;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            for (int i = 0; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Screamer-Killer");
            }

            cbOption1.Text = "Adrenal Glands (+10 pts)";
            cbOption2.Text = "Spore Cysts (+15 pts)";
            cbOption3.Text = "Toxin Sacs (+5 pts)";

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Weapons[(currentIndex * 4) + 3] = cmbFaction.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[currentIndex * 4] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[currentIndex * 4] = "";
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[(currentIndex * 4) + 1] = cbOption2.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 4) + 1] = "";
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        Weapons[(currentIndex * 4) + 2] = cbOption3.Text;
                    }
                    else
                    {
                        Weapons[(currentIndex * 4) + 2] = "";
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        lbModelSelect.Items.Add("Screamer-Killer");
                        Weapons.Add("");
                        Weapons.Add("");
                        Weapons.Add("");
                        Weapons.Add("(None)");
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveRange((UnitSize * 4) - 1, 4);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cbOption1.Visible = false;
                        cbOption2.Visible = false;
                        cbOption3.Visible = false;
                        cmbFaction.Visible = false;
                        panel.Controls["lblFactionUpgrade"].Visible = false;
                        break;
                    }

                    cbOption1.Visible = true;
                    cbOption2.Visible = true;
                    cbOption3.Visible = true;
                    cmbFaction.Visible = true;
                    panel.Controls["lblFactionUpgrade"].Visible = true;

                    if (Weapons[(currentIndex * 4)] == "")
                    {
                        cbOption1.Checked = false;
                    }
                    else
                    {
                        cbOption1.Checked = true;
                    }

                    if (Weapons[(currentIndex * 4) + 1] == "")
                    {
                        cbOption2.Checked = false;
                    }
                    else
                    {
                        cbOption2.Checked = true;
                    }

                    if (Weapons[(currentIndex * 4) + 2] == "")
                    {
                        cbOption3.Checked = false;
                    }
                    else
                    {
                        cbOption3.Checked = true;
                    }

                    cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Weapons[(currentIndex * 4) + 3]);

                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if(lbModelSelect.Items.Count != 0)
            {
                antiLoop = true;
                for(int i = 0; i < UnitSize; i++)
                {
                    int currentModel = DEFAULT_POINTS;

                    if (Weapons[i * 4] == "Adrenal Glands (+10 pts)")
                    {
                        Points += 10;
                        currentModel += 10;
                    }

                    if(Weapons[(i * 4) + 1] == "Spore Cysts (+15 pts)")
                    {
                        Points += 15;
                        currentModel += 15;
                    }

                    if(Weapons[(i * 4) + 2] == "Toxin Sacs (+5 pts)")
                    {
                        Points += 5;
                        currentModel += 5;
                    }

                    Points += repo.GetFactionUpgradePoints(Weapons[(i * 4) + 3]);
                    currentModel += repo.GetFactionUpgradePoints(Weapons[(i * 4) + 3]);

                    lbModelSelect.Items[i] = "Screamer-Killer - " + currentModel + "pts";
                }
                antiLoop = false;
            }
        }

        public override string ToString()
        {
            return "Screamer-Killers - " + Points + "pts";
        }
    }
}
