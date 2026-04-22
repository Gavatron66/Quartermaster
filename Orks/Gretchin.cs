using Roster_Builder.Tyranids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Orks
{
	public class Gretchin : Datasheets
	{
		public Gretchin()
		{
			DEFAULT_POINTS = 4;
			UnitSize = 10;
			Points = DEFAULT_POINTS * UnitSize;
			TemplateCode = "N";
			Keywords.AddRange(new string[]
			{
				"ORKS", "<CLAN>",
				"INFANTRY", "CORE", "GRETCHIN"
			});
			Role = "Troops";
		}

		public override Datasheets CreateUnit()
		{
			return new Gretchin();
		}

		public override void LoadDatasheets(Panel panel, Faction f)
		{
			Template.LoadTemplate(TemplateCode, panel);
			repo = f as Orks;

			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            cmbFaction.Visible = true;
            panel.Controls["lblFactionupgrade"].Visible = true;

            int currentSize = UnitSize;
			nudUnitSize.Minimum = 10;
			nudUnitSize.Value = nudUnitSize.Minimum;
			nudUnitSize.Maximum = 30;
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
        }

		public override void SaveDatasheets(int code, Panel panel)
		{
			NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch (code)
            {
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
                case 30:
					UnitSize = Convert.ToInt32(nudUnitSize.Value);
					break;
			}

			Points = DEFAULT_POINTS * UnitSize;
		}

		public override string ToString()
		{
			return "Gretchin - " + Points + "pts";
		}
	}
}
