using Roster_Builder.Adeptus_Mechanicus;
using Roster_Builder.Death_Guard;
using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class CommanderShadowsun : Datasheets
    {
        public CommanderShadowsun()
        {
            DEFAULT_POINTS = 150;
            Points = DEFAULT_POINTS;
            TemplateCode = "1m_c";
            Weapons.Add("Two High-energy Fusion Blasters");
            Keywords.AddRange(new string[]
            {
                "T'AU EMPIRE", "T'AU SEPT",
                "INFANTRY", "CHARACTER", "BATTLESUIT", "FLY", "SUPREME COMMANDER", "JET PACK",
                "COMMANDER", "SHADOWSUN"
            });
            Role = "HQ";
            WarlordTrait = "Exemplar of the Kauyon";
        }

        public override Datasheets CreateUnit()
        {
            return new CommanderShadowsun();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AdMech;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            panel.Controls["lblRelic"].Visible = false;
            panel.Controls["cmbRelic"].Visible = false;
            panel.Controls["cbStratagem1"].Visible = false;
            panel.Controls["cbStratagem2"].Visible = false;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "High-energy and Dispersed Fusion Blasters",
                "Two Dispersed Fusion Blasters",
                "Two High-energy Fusion Blasters"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbWarlord.Items.Clear();
            cmbWarlord.Items.Add(WarlordTrait);
            cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
            cmbWarlord.Enabled = false;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 15:
                    if (cmbWarlord.SelectedIndex != -1)
                    {
                        WarlordTrait = cmbWarlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; }
                    break;
                default: break;
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Commander Shadowsun - " + Points + "pts";
        }
    }
}