using Roster_Builder.Death_Guard;
using Roster_Builder.Necrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class PurestrainGenestealers : Datasheets
    {
        public PurestrainGenestealers()
        {
            UnitSize = 5;
            DEFAULT_POINTS = 14;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "N";
            Keywords.AddRange(new string[]
            {
                "TYRANIDS", "GENESTEALER CULTS", "<CULT>",
                "INFANTRY", "GENESTEALER", "PURESTRAIN GENESTEALERS"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new PurestrainGenestealers();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as GSC;

            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            panel.Controls["lblModelPoints"].Text = "(+" + DEFAULT_POINTS + " pts/model)";

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            nudUnitSize.Value = nudUnitSize.Minimum;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

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

            cbStratagem3.Text = repo.StratagemList[2].ToString();
            cbStratagem3.Location = new System.Drawing.Point(cmbFaction.Location.X, cmbFaction.Location.Y + 32);
            cbStratagem3.Visible = true;

            if (repo.currentSubFaction == "The Bladed Cog")
            {
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
            else
            {
                cbStratagem3.Enabled = false;
                cbStratagem3.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            NumericUpDown nud = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            CheckBox cbStratagem3 = panel.Controls["cbStratagem3"] as CheckBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
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
            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }

        public override string ToString()
        {
            return "Purestrain Genestealers - " + Points + "pts";
        }
    }
}