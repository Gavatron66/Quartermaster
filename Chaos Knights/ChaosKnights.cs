using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Chaos_Knights
{
    public class ChaosKnights : Faction
    {
        string allegiance = string.Empty;
        public bool hasDreadblade = false;

        public ChaosKnights()
        {
            subFactionName = "Dread Household";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Favour of the Dark Gods";
            customSubFactionTraits = new string[1];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Tyrannical Court", //warlord
                "Stratagem: Corrupted Heirlooms", //Relic
                "Stratagem: Arch-Tyrant", //Extra warlord trait for warlord
                "Stratagem: Chosen by the Gods"
            });

        }

        public override List<string> GetCustomSubfactionList1()
        {   //For Custom Households
            if(allegiance == "Iconoclast Household")
            {
                return new List<string>
                {
                    "Bold Tyrants",
                    "Frenzied Invaders",
                    "Learned Idolators",
                    "Loping Predators",
                    "Precision Cruelty",
                    "Prideful Wrath",
                    "Worthy Offerings"
                };
            }
            else if (allegiance == "Infernal Household")
            {
                return new List<string>
                {
                    "Dark Forging",
                    "Merciless Tormentors",
                    "Gheists of Ruin",
                    "Hellforged Construction",
                    "Biomechanical Fusion",
                    "Unhallowed Inscriptions",
                    "Warp Vision"
                };
            }
            else
            {
                return new List<string> { "Something went wrong, you shouldn't see this" };
            }
        }

        public override List<string> GetCustomSubfactionList2()
        {   //For Dreadblades
            return new List<string>
            {
                "Bold Tyrants",
                "Frenzied Invaders",
                "Learned Idolators",
                "Loping Predators",
                "Precision Cruelty",
                "Prideful Wrath",
                "Worthy Offerings",
                "Dark Forging",
                "Merciless Tormentors",
                "Gheists of Ruin",
                "Hellforged Construction",
                "Biomechanical Fusion",
                "Unhallowed Inscriptions",
                "Warp Vision"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
                //---------- Lord of War ----------
                new WarDogExecutioner(),
                new WarDogStalker(),
                new WarDogKarnivore(),
                new WarDogBrigand(),
                new WarDogHuntsman(),
                new KnightDespoiler(),
                new KnightDesecrator(),
                new KnightRampager(),
                new KnightAbominant(),
                new KnightTyrant(),
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == null)
            {
                return 0;
            }

            if (upgrade.Contains("60 pts"))
            {
                return 60;
            }
            else if (upgrade.Contains("50 pts"))
            {
                return 50;
            }
            else if (upgrade.Contains("45 pts"))
            {
                return 45;
            }
            else if (upgrade.Contains("40 pts"))
            {
                return 40;
            }
            else if (upgrade.Contains("35 pts"))
            {
                return 35;
            }
            else if (upgrade.Contains("30 pts"))
            {
                return 30;
            }
            else if (upgrade.Contains("25 pts"))
            {
                return 25;
            }
            else if (upgrade.Contains("20 pts"))
            {
                return 20;
            }
            else if (upgrade.Contains("15 pts"))
            {
                return 15;
            }
            else if (upgrade.Contains("10 pts"))
            {
                return 10;
            }

            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string> { "(None)" };

            if(keywords.Contains("WAR DOG-CLASS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Blood Shield (+30 pts)",
                    "Collar of Infernal Brass (+15 pts)",
                    "Throne Mechanicum of Skulls (+15 pts)",
                    "Pyrothrone (+25 pts)",
                    "Cursed Rune of Fate (+15 pts)",
                    "Mirror of Fates (+15 pts)",
                    "The Putrid Carapace (+20 pts)",
                    "Blessing of a Thousand Poxes (+15 pts)",
                    "Aura of Corruption (+25 pts)",
                    "Quicksilver Throne (+15 pts)",
                    "Beguiling Majesty (+20 pts)",
                    "Subjugator Machine Spirit (+15 pts)",
                    "Mark of the Dread Knight (+20 pts)",
                    "Warp-borne Stalker (+15 pts)",
                    "Blessing of the Dark Master (+20 pts)"
                });
            }
            else if (keywords.Contains("ABHORRENT-CLASS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Blood Shield (+45 pts)",
                    "Collar of Infernal Brass (+25 pts)",
                    "Throne Mechanicum of Skulls (+25 pts)",
                    "Pyrothrone (+35 pts)",
                    "Cursed Rune of Fate (+35 pts)",
                    "Mirror of Fates (+20 pts)",
                    "The Putrid Carapace (+40 pts)",
                    "Blessing of a Thousand Poxes (+30 pts)",
                    "Aura of Corruption (+35 pts)",
                    "Quicksilver Throne (+20 pts)",
                    "Beguiling Majesty (+40 pts)",
                    "Subjugator Machine Spirit (+30 pts)",
                    "Mark of the Dread Knight (+40 pts)",
                    "Warp-borne Stalker (+30 pts)",
                    "Blessing of the Dark Master (+30 pts)"
                });
            }
            else if (keywords.Contains("TYRANT-CLASS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Blood Shield (+30 pts)",
                    "Collar of Infernal Brass (+25 pts)",
                    "Throne Mechanicum of Skulls (+15 pts)",
                    "Pyrothrone (+45 pts)",
                    "Cursed Rune of Fate (+50 pts)",
                    "Mirror of Fates (+30 pts)",
                    "The Putrid Carapace (+60 pts)",
                    "Blessing of a Thousand Poxes (+15 pts)",
                    "Aura of Corruption (+35 pts)",
                    "Quicksilver Throne (+10 pts)",
                    "Beguiling Majesty (+40 pts)",
                    "Subjugator Machine Spirit (+20 pts)",
                    "Mark of the Dread Knight (+60 pts)",
                    "Warp-borne Stalker (+30 pts)",
                    "Blessing of the Dark Master (+50 pts)"
                });
            }

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
                "Winds of the Warp",
                "Vortex Terrors",
                "The Storm Malevolent",
                "Cyclonic Lamentation",
                "Coruscating Hate",
                "Spitesquall"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            if(allegiance == "Infernal Household" && !keywords.Contains("TYRANT-CLASS"))
            {
                relics.Add("The Blasphemous Engine");
            }

            if (allegiance == "Iconoclast Household" && !keywords.Contains("TYRANT-CLASS"))
            {
                relics.Add("Veil of Medrengard");
            }

            if (!keywords.Contains("TYRANT-CLASS"))
            {
                relics.Add("Helm of Warp-Sight");
            }

            if(keywords.Contains("KNIGHT DESECRATOR"))
            {
                relics.Add("The Diamonas");
            }

            if(keywords.Contains("ABHORRENT-CLASS") && !keywords.Contains("KNIGHT ABOMINANT"))
            {
                relics.Add("The Teeth That Hungers");
            }

            if(keywords.Contains("ABHORRENT-CLASS"))
            {
                relics.Add("The Tyrant's Banner");
            }

            if (!keywords.Contains("TYRANT-CLASS"))
            {
                relics.Add("Bound Varadian Psychogheist");
            }

            relics.Add("The Traitor's Mark");

            if (keywords.Contains("ABHORRENT-CLASS") && !keywords.Contains("KNIGHT ABOMINANT"))
            {
                relics.Add("The Gauntlet of Ascension");
            }

            if(keywords.Contains("PSYKER"))
            {
                relics.Add("The Twisted Mask");
            }

            if (!keywords.Contains("TYRANT-CLASS"))
            {
                relics.Add("Panoply of the Cursed Knights");
            }

            if(keywords.Contains("PTERRORSHADES"))
            {
                relics.Add("Soul-Raptor Swarm");
            }

            if(keywords.Contains("WAR DOG-CLASS"))
            {
                relics.Add("Helm of Dogs");
            }

            if (currentSubFaction == "House Herpetrax" && !keywords.Contains("TYRANT-CLASS"))
            {
                relics.Add("Crown of Jedathra");
            }

            if(currentSubFaction == "House Lucaris" &&
                ((keywords.Contains("WAR DOG-CLASS") && !keywords.Contains("WAR DOG KARNIVORE")) || keywords.Contains("KNIGHT DESPOILER") || keywords.Contains("KNIGHT TYRANT")))
            {
                if (keywords.Contains("KNIGHT DESPOILER"))
                {
                    relics.Add("Serpentstrike Core (Slot 1)");
                    relics.Add("Serpentstrike Core (Slot 2)");
                    relics.Add("Serpentstrike Core (Slot 3)");
                }
                else
                {
                    relics.Add("Serpentstrike Core");
                }
            }

            if(currentSubFaction == "House Khymere")
            {
                relics.Add("Warpfire Shield");
            }

            if (currentSubFaction == "House Vextrix")
            {
                relics.Add("Heretek Power Core");
            }

            if (currentSubFaction == "House Khomentis" && keywords.Contains("PTERRORSHADES"))
            {
                relics.Add("Daemonic Shrike");
            }

            if (currentSubFaction == "House Korvax")
            {
                relics.Add("Rune of Darkness");
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "House Herpetrax",
                "House Lucaris",
                "House Khymere",
                "House Vextrix",
                "House Khomentis",
                "House Korvax",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>() { string.Empty };

            if (keyword == "War Dog")
            {
                traits.AddRange(new string[]
                {
                    "Eager for the Kill",
                    "Harbinger of Scrapcode",
                    "Warp-Haunted Hull",
                });
            }
            else
            {
                traits.AddRange(new string[]
                {
                    "Eager for the Kill",
                    "Harbinger of Scrapcode",
                    "Warp-Haunted Hull",
                    "Knight Diabolus",
                    "Infernal Quest",
                    "Aura of Terror"
                });
            }

            if (currentSubFaction == "House Herpetrax")
            {
                traits.Add("Bow to None");
            }
            else if (currentSubFaction == "House Lucaris")
            {
                traits.Add("Strike First, Strike Often");
            }
            else if (currentSubFaction == "House Khymere")
            {
                traits.Add("Maddened Cries");
            }
            else if (currentSubFaction == "House Vextrix")
            {
                traits.Add("Favour of the Dark Mechanicum");
            }
            else if (currentSubFaction == "House Khomentis")
            {
                traits.Add("Dread Hunter");
            }
            else if (currentSubFaction == "House Korvax")
            {
                traits.Add("Lord of Dread");
            }

            return traits;
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

                    if(cmbSubFaction.SelectedIndex > 0 && cmbSubFaction.SelectedIndex <= 3)
                    {
                        allegiance = "Iconoclast Household";
                    }
                    else if(cmbSubFaction.SelectedIndex > 3)
                    {
                        allegiance = "Infernal Household";
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
                "Iconoclast Household",
                "Infernal Household"
            });

            if (customSubFactionTraits[0] != null)
            {
                cmbSubCustom1.SelectedIndex = cmbSubCustom1.Items.IndexOf(customSubFactionTraits[0]);
            }
            antiLoop = false;
        }

        public override string ToString()
        {
            return "Chaos Knights";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
