using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Leagues_of_Votann
{
    public class LeaguesOfVotann : Faction
    {
        public LeaguesOfVotann()
        {
            subFactionName = "<League>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "The Votannic Council";
            customSubFactionTraits = new string[3];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Legend of the League",  //Warlord Trait
                "Stratagem: In The Right Hands",    //Relic
                "Stratagem: Bequest of the Votann", //Extra relic for Theyn/Hesyr
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>()
            {
                "Vengeful",
                "Brutal Efficiency",
                "Close Quarters Prioritisation",
                "Taking it Personally",
                "Quick to Judge",
                "Unwavering Discipline"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>()
            {
                "Martial Cloneskeins",
                "Stoic",
                "Honour in Toil",
                "War Songs",
                "Refined Power Cores",
                "Superior Beam Capacitors",
                "Void Hardened",
                "Warrior Pride",
                "Weaponsmiths"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>()
            {
                //---------- HQ ----------
                new ÛtharTheDestined(),
                new Kâhl(),
                new EinhyrChampion(),
                new Grimnyr(),
                new BrôkhyrIronMaster(),
                //---------- Troops ----------
                new HearthkynWarriors(),
                //---------- Elites ----------
                new EinhyrHearthguard(),
                new CthonianBeserks(),
                //---------- Fast Attack ----------
                new HernkynPioneers(),
                new Sagitaur(),
                //---------- Heavy Support ----------
                new BrôkhyrThunderkyn(),
                new HekatonLandFortress()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == "High Kâhl (+40 pts)")
            {
                return 40;
            }
            else if (upgrade == "Lord Grimnyr (+25 pts)" || upgrade == "Brôkhyr Forge-master (+25 pts)")
            {
                return 25;
            }
            else
            {
                return 0;
            }
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrade = new List<string>();

            upgrade.Add("(None)");

            if(keywords.Contains("KÂHL"))
            {
                upgrade.Add("High Kâhl (+40 pts)");
            }

            if (keywords.Contains("GRIMNYR"))
            {
                upgrade.Add("Lord Grimnyr (+25 pts)");
            }

            if (keywords.Contains("BRÔKHYR IRON-MASTER"))
            {
                upgrade.Add("Brôkhyr Forge-master (+25 pts)");
            }

            return upgrade;
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string>()
            {
                "Interface Echo",
                "Fortify",
                "Ancestral Wrath",
                "Grudgepyre",
                "Null Vortex",
                "Crushing Contempt"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();
            
            relics.Add("(None)");

            if(!keywords.Contains("BRÔKHYR"))
            {
                relics.Add("Aktôl's Fortress");
            }

            relics.Add("Ancestral Crest");

            if(keywords.Contains("EINHYR"))
            {
                relics.Add("Exactor");
            }
            
            if(keywords.Contains("GRIMNYR") || keywords.Contains("KÂHL"))
            {
                relics.Add("The First Knife");
            }

            if(keywords.Contains("KÂHL"))
            {
                relics.Add("Flâyre");
            }

            relics.Add("Wayfarer's Grace");

            if (!keywords.Contains("BRÔKHYR"))
            {
                relics.Add("The Grey Crest");
            }

            relics.Add("Grudge's End");

            if(keywords.Contains("EINHYR") || keywords.Contains("KÂHL"))
            {
                relics.Add("Wârpestryk");
            }

            if (keywords.Contains("KÂHL"))
            {
                relics.Add("The Hearthfist");
            }
            
            if(keywords.Contains("GRIMNYR"))
            {
                relics.Add("The Murmuring Stave");
            }
            
            if(!keywords.Contains("EINHYR"))
            {
                relics.Add("Thyrikite Plate");
            }

            if (keywords.Contains("BRÔKHYR"))
            {
                relics.Add("Vôlumm's Master Artifice");
            }

            if (keywords.Contains("EINHYR"))
            {
                relics.Add("Ymmâ's Shield");
            }

            if (currentSubFaction == "Greater Thurian Legaue") 
            { 
                relics.Add("Kôrvyk's Cuirass"); 
            }
            else if (currentSubFaction == "Trans-Hyperian Alliance") 
            { 
                relics.Add("The CORV Duas"); 
            }
            else if (currentSubFaction == "Kronus Hegemony" && (keywords.Contains("EINHYR") || keywords.Contains("KÂHL"))) 
            { 
                relics.Add("The Just Blade"); 
            }
            else if (currentSubFaction == "Ymyr Conglomerate" && !keywords.Contains("BRÔKHYR")) 
            { 
                relics.Add("The Last Crest of Jâluk"); 
            }
            else if (currentSubFaction == "Urani-Surtr Regulates") 
            { 
                relics.Add("The Abiding Mantle"); 
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Greater Thurian League",
                "Trans-Hyperian Alliance",
                "Kronus Hegemony",
                "Ymyr Conglomerate",
                "Urani-Surtr Regulates",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>();

            traits.AddRange(new string[]
            {
                "Ancestral Bearing",
                "Warrior Lord",
                "A Long List",
                "Guild Affiliate",
                "Unrelenting Toil",
                "Grim Demeanour"
            });

            if (currentSubFaction == "Greater Thurian League") { traits.Add("Pragmatic Wisdom"); }
            else if (currentSubFaction == "Trans-Hyperian Alliance") { traits.Add("Nomad Strategist"); }
            else if (currentSubFaction == "Kronus Hegemony") { traits.Add("Exemplary Hero"); }
            else if (currentSubFaction == "Ymyr Conglomerate") { traits.Add("Guild Connections"); }
            else if (currentSubFaction == "Urani-Surtr Regulates") { traits.Add("Grim Pragmatism"); }

            return traits;
        }

        public override void SaveSubFaction(int code, Panel panel)
        {
            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            ComboBox cmbSubCustom2 = panel.Controls["cmbSubCustom2"] as ComboBox;
            ComboBox cmbSubCustom3 = panel.Controls["cmbSubCustom3"] as ComboBox;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;
            Label lblSubCustom2 = panel.Controls["lblSubCustom2"] as Label;
            Label lblSubCustom3 = panel.Controls["lblSubCustom3"] as Label;

            switch (code)
            {
                case 50:
                    currentSubFaction = cmbSubFaction.SelectedItem.ToString();
                    if (currentSubFaction != "<Custom>")
                    {
                        cmbSubCustom1.Visible = false;
                        cmbSubCustom2.Visible = false;
                        cmbSubCustom3.Visible = false;
                        lblSubCustom1.Visible = false;
                        lblSubCustom2.Visible = false;
                        lblSubCustom3.Visible = false;
                    }
                    else
                    {
                        cmbSubCustom1.Visible = true;
                        cmbSubCustom2.Visible = true;
                        cmbSubCustom3.Visible = true;
                        lblSubCustom1.Visible = true;
                        lblSubCustom2.Visible = true;
                        lblSubCustom3.Visible = true;
                        customSubFactionTraits = new string[3];
                    }
                    break;
                case 51:
                    customSubFactionTraits[0] = cmbSubCustom1.SelectedItem.ToString();
                    break;
                case 52:
                    customSubFactionTraits[1] = cmbSubCustom2.SelectedItem.ToString();
                    break;
                case 53:
                    customSubFactionTraits[2] = cmbSubCustom3.SelectedItem.ToString();
                    break;
            }
        }

        public override void SetPoints(int points)
        {

        }

        public override void SetSubFactionPanel(Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            antiLoop = true;
            Template template = new Template();
            template.LoadFactionTemplate(4, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            ComboBox cmbSubCustom2 = panel.Controls["cmbSubCustom2"] as ComboBox;
            ComboBox cmbSubCustom3 = panel.Controls["cmbSubCustom3"] as ComboBox;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;
            Label lblSubCustom2 = panel.Controls["lblSubCustom2"] as Label;
            Label lblSubCustom3 = panel.Controls["lblSubCustom3"] as Label;

            if (currentSubFaction != "<Custom>")
            {
                cmbSubCustom1.Visible = false;
                cmbSubCustom2.Visible = false;
                cmbSubCustom3.Visible = false;
                lblSubCustom1.Visible = false;
                lblSubCustom2.Visible = false;
                lblSubCustom3.Visible = false;
            }
            else
            {
                cmbSubCustom1.Visible = true;
                cmbSubCustom2.Visible = true;
                cmbSubCustom3.Visible = true;
                lblSubCustom1.Visible = true;
                lblSubCustom2.Visible = true;
                lblSubCustom3.Visible = true;
            }

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
            panel.BringToFront();

            cmbSubCustom1.Items.Clear();
            cmbSubCustom2.Items.Clear();
            cmbSubCustom3.Items.Clear();

            cmbSubCustom1.Items.AddRange(this.GetCustomSubfactionList1().ToArray());

            cmbSubCustom2.Items.AddRange(this.GetCustomSubfactionList2().ToArray());

            cmbSubCustom3.Items.AddRange(this.GetCustomSubfactionList2().ToArray());

            if (customSubFactionTraits[0] != null)
            {
                cmbSubCustom1.SelectedIndex = cmbSubCustom1.Items.IndexOf(customSubFactionTraits[0]);
                cmbSubCustom2.SelectedIndex = cmbSubCustom2.Items.IndexOf(customSubFactionTraits[1]);
                cmbSubCustom3.SelectedIndex = cmbSubCustom3.Items.IndexOf(customSubFactionTraits[2]);
            }
            antiLoop = false;
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }

        public override string ToString()
        {
            return "Leagues of Votann";
        }
    }
}
