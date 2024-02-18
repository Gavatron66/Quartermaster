using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class Ghostkeel : Datasheets
    {
        public Ghostkeel()
        {
            DEFAULT_POINTS = 160;
            Points = DEFAULT_POINTS;
            TemplateCode = "3m";
            Weapons.Add("Fusion Collider");
            Weapons.Add("Two T'au Flamers");
            Weapons.Add("(None)");
            Keywords.AddRange(new string[]
            {
                "T'AU", "<SEPT>",
                "VEHICLE", "BATTLESUIT", "FLY", "JET PACK", "GHOSTKEEL BATTLESUIT"
            });
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new Ghostkeel();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as T_au;

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            panel.Controls["cmbFactionUpgrade"].Visible = true;
            panel.Controls["lblFactionUpgrade"].Visible = true;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[] 
            {
                "Cyclic Ion Raker",
                "Fusion Collider"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cmbOption2.Items.Clear();
            cmbOption2.Items.AddRange(new string[] 
            {
                "Two Burst Cannons",
                "Two Fusion Blasters (+10 pts)",
                "Two T'au Flamers"
            });
            cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

            cmbOption3.Items.Clear();
            cmbOption3.Items.AddRange(new string[] 
            {
                "(None)",
                "Early Warning Override",
                "Flare Launcher (+15 pts)"
            });
            cmbOption3.SelectedIndex = cmbOption3.Items.IndexOf(Weapons[2]);

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
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            ComboBox cmbOption3 = panel.Controls["cmbOption3"] as ComboBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;

            switch(code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 12:
                    Weapons[1] = cmbOption2.SelectedItem.ToString();
                    break;
                case 13:
                    Weapons[2] = cmbOption3.SelectedItem.ToString();
                    break;
                case 16:
                    Factionupgrade = cmbFaction.Text;
                    break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);

            if(Weapons.Contains("Flare Launcher (+15 pts)"))
            {
                Points += 15;
            }

            if(Weapons.Contains("Two Fusion Blasters (+10 pts)"))
            {
                Points += 10;
            }
        }

        public override string ToString()
        {
            return "Ghostkeel Battlesuit - " + Points + "pts";
        }
    }
}
