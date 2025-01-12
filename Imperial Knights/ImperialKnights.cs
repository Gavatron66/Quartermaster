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
            return new List<string>
            {
                "Front-line Fighters",
                "Glorified History",
                "Hunters of Beasts",
                "Noble Combatants",
                "Paragons of Honour",
                "Strike and Shield",
                "Blessed Arms",
                "Fealty to the Cog",
                "Honoured Sacristans",
                "Machine Focus",
                "Steel-Sinewed Aim",
                "Unremitting"
            };
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
            relics.Add("Sanctuary"); //Questoris/Armiger Class
            relics.Add("Ravager"); //Reaper Chainsword
            relics.Add("Helm of the Nameless Warrior"); //Questor Imperialis Questoris/Armiger Class
            relics.Add("The Helm Dominatus"); //Questoris Class w/ Bondsman ability
            relics.Add("Endless Fury"); //Avenger Gatling Cannon
            relics.Add("The Bastard's Helm"); //Armiger-class
            relics.Add("Mark of the Omnissiah"); //Questor Mechanicus Questoris/Armiger Class
            relics.Add("Banner of Macharius Triumphant"); //Questor Imperialis Questoris Class
            relics.Add("Mentor's Seal"); //Knight Preceptor
            relics.Add("The Heart of Ion"); //Questor Mechanicus Questoris/Armiger Class
            relics.Add("The Paragon Gauntlet"); //Thunderstrike Gauntlet
            relics.Add("Traitor's Pyre"); //Questor Imperialis Knight Valiant
            relics.Add("Cawl's Wrath"); //Questor Mechanicus Knight Castellan
            relics.Add("Judgement"); //Stormspear Rocket Pod

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
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;

            switch (code)
            {
                case 50:
                    currentSubFaction = cmbSubFaction.SelectedItem.ToString();
                    if (currentSubFaction == "<Custom>")
                    {
                        cmbSubCustom1.Visible = true;
                        lblSubCustom1.Visible = true;
                    }
                    else
                    {
                        cmbSubCustom1.Visible = false;
                        lblSubCustom1.Visible = false;
                        customSubFactionTraits = new string[1];
                    }
                    break;
                case 51:
                    customSubFactionTraits[0] = cmbSubCustom1.SelectedItem.ToString();
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
            template.LoadFactionTemplate(2, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            ComboBox cmbSubCustom1 = panel.Controls["cmbSubCustom1"] as ComboBox;
            Label lblSubCustom1 = panel.Controls["lblSubCustom1"] as Label;

            if (currentSubFaction != "<Custom>")
            {
                cmbSubCustom1.Visible = false;
                lblSubCustom1.Visible = false;
            }
            else
            {
                cmbSubCustom1.Visible = true;
                lblSubCustom1.Visible = true;
            }

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
            panel.BringToFront();

            cmbSubCustom1.Items.Clear();

            cmbSubCustom1.Items.AddRange(this.GetCustomSubfactionList1().ToArray());

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
