using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Grey_Knights
{
    public class GreyKnights : Faction
    {
        public GreyKnights()
        {
            subFactionName = "<Brotherhood>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Wisdom of the Prognosticars";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Shield of Humanity",
                "Stratagem: Armoury of Titan",
                "Stratagem: Charge of the Ancients",
                "Stratagem: Exemplar of the Silvered Host",
                "Stratagem: Endownment in Extremis"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>();
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>();
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>
            {
                //---------- HQ ----------
                new KaldorDraigo(),
                new GMVoldus(),
                new GrandMaster(),
                new GMDreadknight(),
                new CastellanCrowe(),
                new BrotherCaptainStern(),
                new BrotherCaptain(),
                new BrotherhoodChampion(),
                new BrotherhoodLibrarian(),
                new BrotherhoodTechmarine(),
                new BrotherhoodChaplain(),
                //---------- Troops ----------
                new BrotherhoodTerminators(),
                new StrikeSquad(),
                //---------- Elites ----------
                new BrotherhoodApothecary(),
                new BrotherhoodAncient(),
                new Paladins(),
                new Purifiers(),
                new PaladinAncient(),
                new GKServitors(),
                new GKVenerableDreadnought(),
                new GKDreadnought(),
                //---------- Fast Attack ----------
                new Interceptors(),
                //---------- Heavy Support ----------
                new PurgationSquad(),
                new NemesisDreadknight(),
                new GKLandRaider(),
                new GKLandRaiderCrusader(),
                new GKLandRaiderRedeemer(),
                //---------- Transport ----------
                new GKRazorback(),
                new GKRhino(),
                //---------- Flyer ----------
                new GKStormhawkInterceptor(),
                new GKStormtalonGunship(),
                new GKStormravenGunship()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {

            string[] twentyfive = new string[]
            {
                "Omen of Incursion (+25 pts)",
                "Foretelling of Locus (+25 pts)",
                "Severance Bolt (+25 pts)"
            };

            string[] fifteen = new string[]
            {
                "Augury of Aggression (+15 pts)",
                "A Noble Death (+15 pts)",
                "Servant of the Throne (+15 pts)"
            };

            string[] ten = new string[]
            {
                "Heroism's Favour (+10 pts)",
                "Presaged Paralysis (+10 pts)",
                "Temporal Bombs (+10 pts)",
                "Deluminator of Majesty (+10 pts)",
                "Gem of Inoktu (+10 pts)"
            };

            string[] five = new string[]
            {
                "True Name Shard (+5 pts)"
            };

            if(twentyfive.Contains(upgrade))
            {
                return 25;
            }

            if (fifteen.Contains(upgrade))
            {
                return 15;
            }

            if (ten.Contains(upgrade))
            {
                return 10;
            }

            if (five.Contains(upgrade))
            {
                return 5;
            }

            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>()
            {
                "(None)"
            };

            upgrades.AddRange(new string[]
            {
                "Augury of Aggression (+15 pts)",
                "Heroism's Favour (+10 pts)",
                "A Noble Death (+15 pts)",
                "Omen of Incursion (+25 pts)",
                "Presaged Paralysis (+10 pts)",
                "Foretelling of Locus (+25 pts)",
                "True Name Shard (+5 pts)",
                "Temporal Bombs (+10 pts)",
                "Servant of the Throne (+15 pts)",
                "Deluminator of Majesty (+10 pts)",
                "Gem of Inoktu (+10 pts)",
                "Severance Bolt (+25 pts)"
            });

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            if (keywords == "Dominus")
            {
                return new List<string>()
                {
                    "Gate of Infinity",
                    "Empyric Amplification",
                    "Sanctuary",
                    "Vortex of Doom",
                    "Warp Shaping",
                    "Ghostly Bonds"
                };
            }
            else if (keywords == "Sanctic")
            {
                return new List<string>()
                {
                    "Astral Aim",
                    "Purge Soul",
                    "Hammerhand",
                    "Purifying Flame",
                    "Armoured Resilience",
                    "Ethereal Castigation"
                };
            }
            else if (keywords == "Litany")
            {
                return new List<string>()
                {
                    "Words of Power",
                    "Intonement for Guidance",
                    "Psalm of Purity",
                    "Refrain of Convergence",
                    "Recitation of Projection",
                    "Invocation of Focus"
                };
            }

            return new List<string>();
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>()
            {
                "(None)"
            };

            if(keywords.Contains("BROTHER-CAPTAIN") || keywords.Contains("LIBRARIAN") ||
                (keywords.Contains("GRAND MASTER") && !keywords.Contains("NEMESIS DREADKNIGHT")) ||
                keywords.Contains("APOTHECARY"))
            {
                relics.Add("Soul Glaive");
            }

            if (keywords.Contains("BROTHER-CAPTAIN") || keywords.Contains("LIBRARIAN") ||
                (keywords.Contains("GRAND MASTER") && !keywords.Contains("NEMESIS DREADKNIGHT")) ||
                keywords.Contains("APOTHECARY"))
            {
                relics.Add("Destroyer of Crys'yllix");
            }

            if(!(keywords.Contains("TECHMARINE") || keywords.Contains("NEMESIS DREADKNIGHT")
                || keywords.Contains("APOTHECARY")))
            {
                relics.Add("Fury of Deimos");
            }

            if(keywords.Contains("ANCIENT"))
            {
                relics.Add("Banner of Refining Flame");
            }

            relics.Add("Domina Liber Daemonica");

            if(!keywords.Contains("NEMESIS DREADKNIGHT"))
            {
                relics.Add("Cuirass of Sacrifice");
            }

            relics.Add("Sanctic Shard");

            relics.Add("Gyrotemporal Vault");

            if(keywords.Contains("BROTHER-CAPTAIN") || keywords.Contains("CHAMPION")
                || keywords.Contains("LIBRARIAN")
                || (keywords.Contains("GRAND MASTER") && !keywords.Contains("NEMESIS DREADKNIGHT"))
                || keywords.Contains("APOTHECARY")) 
            {
                relics.Add("Blade of the Forsworn");
            }

            relics.Add("Sigil of Exigence");

            if(!(keywords.Contains("CHAPLAIN") || keywords.Contains("TECHMARINE")))
            {
                relics.Add("Augurium Scrolls");
            }

            if (keywords.Contains("BROTHER-CAPTAIN") || keywords.Contains("LIBRARIAN") ||
                (keywords.Contains("GRAND MASTER") && !keywords.Contains("NEMESIS DREADKNIGHT")) ||
                keywords.Contains("APOTHECARY"))
            {
                relics.Add("Stave of Supremacy");
            }

            if (!keywords.Contains("NEMESIS DREADKNIGHT"))
            {
                relics.Add("Kantu Vambrace");
            }

            if(keywords.Contains("LIBRARIAN"))
            {
                relics.Add("Artisan Nullifier Matrix");
            }

            if (keywords.Contains("TECHMARINE"))
            {
                relics.Add("Aetheric Conduit");
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Swordbearers",
                "Blades of Victory",
                "Wardmakers",
                "Prescient Brethren",
                "Preservers",
                "Rapiers",
                "Exactors",
                "Silver Blades"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>();

            traits.AddRange(new string[]
            {
                "Daemon-slayer",
                "Hammer of Righteous",
                "Unyielding Anvil",
                "First to the Fray",
                "Nemesis Lord",
                "Psychic Epitome"
            });

            if(currentSubFaction == "Swordbearers")
            {
                traits.Add("Rites of Protection");
            }
            if (currentSubFaction == "Blades of Victory")
            {
                traits.Add("Vanguard Aggression");
            }
            if (currentSubFaction == "Wardmakers")
            {
                traits.Add("Loremaster");
            }
            if (currentSubFaction == "Prescient Brethren")
            {
                traits.Add("Divination");
            }
            if (currentSubFaction == "Preservers")
            {
                traits.Add("Radiant Exemplar");
            }
            if (currentSubFaction == "Rapiers")
            {
                traits.Add("Inescapable Wrath");
            }
            if (currentSubFaction == "Exactors")
            {
                traits.Add("Oath of Witness");
            }
            if (currentSubFaction == "Silver Blades")
            {
                traits.Add("Martial Perfection");
            }

            return traits;
        }

        public override void SaveSubFaction(int code, Panel panel)
        {
            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;

            switch (code)
            {
                case 50:
                    currentSubFaction = cmbSubFaction.SelectedItem.ToString();
                    break;
            }
        }

        public override void SetPoints(int points)
        {
        }

        public override void SetSubFactionPanel(Panel panel)
        {
            Template template = new Template();
            template.LoadFactionTemplate(1, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
        }

        public override string ToString()
        {
            return "Grey Knights";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
