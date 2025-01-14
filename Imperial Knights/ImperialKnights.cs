using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Imperial_Knights
{
    public class ImperialKnights : Faction
    {
        string allegiance = string.Empty;

        public ImperialKnights()
        {
            subFactionName = "Noble Household";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Exalted Court";
            customSubFactionTraits = new string[1];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Knight Baron",
                "Stratagem: Heirlooms of the Household",
                "Stratagem: Revered Paragon",
            });

        }

        public override List<string> GetCustomSubfactionList1()
        {   //For Custom Households
            if(allegiance == "Questor Imperialis")
            {
                return new List<string>
                {
                    "Front-line Fighters",
                    "Glorified History",
                    "Hunters of Beasts",
                    "Noble Combatants",
                    "Paragons of Honour",
                    "Strike and Shield",
                };
            }
            else if (allegiance == "Questor Mechanicus")
            {
                return new List<string>
                {
                    "Blessed Arms",
                    "Fealty to the Cog",
                    "Honoured Sacristans",
                    "Machine Focus",
                    "Steel-Sinewed Aim",
                    "Unremitting"
                };
            }
            else
            {
                return new List<string> { "Something went wrong, you shouldn't see this" };
            }
        }

        public override List<string> GetCustomSubfactionList2()
        {   //For Freeblades
            return new List<string>
            {
                "Front-line Fighters",
                "Hunters of Beasts",
                "Noble Combatants",
                "Paragons of Honour",
                "Strike and Shield",
                "Blessed Arms",
                "Honoured Sacristans",
                "Machine Focus",
                "Steel-Sinewed Aim",
                "Unremitting",
                "Last of Their Line",
                "Mysterious Guardian",
                "Peerless Warrior",
                "Mythic Hero"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
                //---------- Lord of War ----------
                new ArmigerHelverin(),
                new ArmigerWarglaive(),
                new KnightErrant(),
                new KnightWarden(),
                new KnightCrusader(),
                new KnightGallant(),
                new KnightPaladin(),
                new KnightCastellan(),
                new KnightValiant(),
                new KnightPreceptor(),
                new CanisRex(),
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == null)
            {
                return 0;
            }

            if (upgrade.Contains("45 pts"))
            {
                return 45;
            }
            else if (upgrade.Contains("35 pts"))
            {
                return 35;
            }
            else if (upgrade.Contains("30 pts"))
            {
                return 30;
            }
            else if (upgrade.Contains("20 pts"))
            {
                return 20;
            }

            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>()
            {
                "(None)",
                "Master of Justice (+30 pts)",
                "High Monarch (+45 pts)",
                "Monarchsward (+35 pts)",
                "Gatekeeper (+35 pts)",
                "Herald (+20 pts)",
                "Princeps (+35 pts)",
                "Forge Master (+30 pts)",
                "Master Tactician (+30 pts)",
                "Master of Lore (+35 pts)",
                "Master of Vox (+20 pts)"
            };

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string>
            {
                "The Virtue of Courage",
                "The Oath of Justice",
                "The Folly of Mercy",
                "The Knight's Faith",
                "The Warrior's Hope",
                "The Wisdom of Nobility"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            if(!keywords.Contains("DOMINUS-CLASS"))
            {
                relics.Add("Sanctuary"); //Questoris/Armiger Class
            }

            if(keywords.Contains("QUESTORIS-CLASS") && !keywords.Contains("KNIGHT CRUSADER"))
            {
                relics.Add("Ravager"); //Reaper Chainsword
            }

            if(allegiance == "Questor Imperialis" && !keywords.Contains("DOMINUS-CLASS"))
            {
                relics.Add("Helm of the Nameless Warrior"); //Questor Imperialis Questoris/Armiger Class
            }

            if (keywords.Contains("QUESTORIS-CLASS") && !keywords.Contains("KNIGHT PRECEPTOR"))
            {
                relics.Add("The Helm Dominatus"); //Questoris Class w/ Bondsman ability
            }

            if(keywords.Contains("KNIGHT CRUSADER") || keywords.Contains("KNIGHT WARDEN"))
            {
                relics.Add("Endless Fury"); //Avenger Gatling Cannon
            }

            if(keywords.Contains("ARMIGER-CLASS"))
            {
                relics.Add("The Bastard's Helm"); //Armiger-class
            }

            if (allegiance == "Questor Mechanicus" && !keywords.Contains("DOMINUS-CLASS"))
            {
                relics.Add("Mark of the Omnissiah"); //Questor Mechanicus Questoris/Armiger Class
            }

            if (allegiance == "Questor Imperialis" && !keywords.Contains("DOMINUS-CLASS"))
            {
                relics.Add("Banner of Macharius Triumphant"); //Questor Imperialis Questoris Class
            }

            if(keywords.Contains("KNIGHT PRECEPTOR"))
            {
                relics.Add("Mentor's Seal"); //Knight Preceptor
            }

            if (allegiance == "Questor Mechanicus" && !keywords.Contains("DOMINUS-CLASS"))
            {
                relics.Add("The Heart of Ion"); //Questor Mechanicus Questoris/Armiger Class
            }

            if (keywords.Contains("QUESTORIS-CLASS") && !keywords.Contains("KNIGHT CRUSADER"))
            {
                relics.Add("The Paragon Gauntlet"); //Thunderstrike Gauntlet
            }

            if (allegiance == "Questor Imperialis" && keywords.Contains("KNIGHT VALIANT"))
            {
                relics.Add("Traitor's Pyre"); //Questor Imperialis Knight Valiant
            }

            if (allegiance == "Questor Mechanicus" && keywords.Contains("KNIGHT CASTELLAN"))
            {
                relics.Add("Cawl's Wrath"); //Questor Mechanicus Knight Castellan
            }

            if(keywords.Contains("QUESTORIS-CLASS"))
            {
                relics.Add("Judgement"); //Stormspear Rocket Pod
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "House Terryn",
                "House Griffith",
                "House Cadmus",
                "House Hawkshroud",
                "House Mortan",
                "House Raven",
                "House Taranis",
                "House Krast",
                "House Vulker",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            if(keyword == "Armiger")
            {
                return new List<string>()
                {
                    string.Empty,
                    "Cunning Commander",
                    "Blessed by the Sacristans",
                    "Ion Bulwark"
                };
            }

            return new List<string>()
            {
                string.Empty,
                "Cunning Commander",
                "Blessed by the Sacristans",
                "Ion Bulwark",
                "Knight Seneschal",
                "Landstrider",
                "Revered Knight"
            };
        }

        public override void SaveSubFaction(int code, Panel panel)
        {
            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            ComboBox cmbSubCustom2 = panel.Controls["cmbSubCustom2"] as ComboBox;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;
            Label lblSubCustom2 = panel.Controls["lblSubCustom2"] as Label;

            switch (code)
            {
                case 50:
                    currentSubFaction = cmbSubFaction.SelectedItem.ToString();
                    if (currentSubFaction == "<Custom>")
                    {
                        cmbSubCustom1.Visible = true;
                        cmbSubCustom2.Visible = true;
                        lblSubCustom1.Visible = true;
                        lblSubCustom2.Visible = true;
                    }
                    else
                    {
                        cmbSubCustom1.Visible = false;
                        cmbSubCustom2.Visible = false;
                        lblSubCustom1.Visible = false;
                        lblSubCustom2.Visible = false;
                        customSubFactionTraits = new string[2];
                    }

                    if(cmbSubFaction.SelectedIndex > 0 && cmbSubFaction.SelectedIndex <= 5)
                    {
                        allegiance = "Questor Imperialis";
                    }
                    else if(cmbSubFaction.SelectedIndex > 6 && cmbSubFaction.SelectedIndex != 10)
                    {
                        allegiance = "Questor Mechanicus";
                    }

                    break;
                case 51:
                    customSubFactionTraits[0] = cmbSubCustom1.SelectedItem.ToString();
                    allegiance = cmbSubCustom1.SelectedItem.ToString();

                    if(cmbSubCustom1.SelectedIndex == 0)
                    {
                        cmbSubCustom2.Items.Clear();
                        cmbSubCustom2.Items.AddRange(this.GetCustomSubfactionList1().ToArray());
                    }
                    else if (cmbSubCustom1.SelectedIndex == 1)
                    {
                        cmbSubCustom2.Items.Clear();
                        cmbSubCustom2.Items.AddRange(this.GetCustomSubfactionList1().ToArray());
                    }

                    break;
                case 52:
                    customSubFactionTraits[1] = cmbSubCustom2.SelectedItem.ToString();
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
            template.LoadFactionTemplate(3, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            ComboBox cmbSubCustom2 = panel.Controls["cmbSubCustom2"] as ComboBox;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;
            Label lblSubCustom2 = panel.Controls["lblSubCustom2"] as Label;

            if (currentSubFaction != "<Custom>")
            {
                cmbSubCustom1.Visible = false;
                cmbSubCustom2.Visible = false;
                lblSubCustom1.Visible = false;
                lblSubCustom2.Visible = false;
            }
            else
            {
                cmbSubCustom1.Visible = true;
                cmbSubCustom2.Visible = true;
                lblSubCustom1.Visible = true;
                lblSubCustom2.Visible = true;
            }

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
            panel.BringToFront();

            cmbSubCustom1.Items.Clear();
            cmbSubCustom2.Items.Clear();

            cmbSubCustom1.Items.AddRange(new string[]
            {
                "Questor Imperialis",
                "Questor Mechanicus"
            });

            if (customSubFactionTraits[0] != null)
            {
                cmbSubCustom1.SelectedIndex = cmbSubCustom1.Items.IndexOf(customSubFactionTraits[0]);
            }
            antiLoop = false;
        }

        public override string ToString()
        {
            return "Imperial Knights";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
