using Roster_Builder.Drukhari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster_Builder.Adepta_Sororitas
{
    public class AdeptaSororitas : Faction
    {
        public AdeptaSororitas()
        {
            subFactionName = "<Order>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Blessings of the Faithful";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Saint in the Making",
                "Stratagem: Open the Reliquaries",
                "Stratagem: A Sacred Burden"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>
            {
                "Shield of Aversion",
                "Hallowed Martyrs",
                "Conviction of Faith",
                "Devout Fanaticism",
                "Guided By the Emperor's Will",
                "Holy Wrath",
                "Perfervid Belief",
                "Purifying Recitations",
                "Raging Fervour",
                "Rites of Fire",
                "Righteous Suffering",
                "Slayers of Heretics",
                "Unbridled Valour",
                "Unshakable Vengeance",
                "Witch Hunters"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>
            {
                "Shield of Aversion",
                "Hallowed Martyrs",
                "Conviction of Faith",
                "Devout Fanaticism",
                "Guided By the Emperor's Will",
                "Holy Wrath",
                "Perfervid Belief",
                "Purifying Recitations",
                "Raging Fervour",
                "Rites of Fire",
                "Righteous Suffering",
                "Slayers of Heretics",
                "Unbridled Valour",
                "Unshakable Vengeance",
                "Witch Hunters"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
                //---------- HQ ----------
                new MorvennVahl(),
                new Canoness(),
                new Palatine(),
                new JunithEruita(),
                new Missionary(),
                new Celestine(),
                new TriumphOfStKatherine(),
                new EphraelSternKyganil(),
                //---------- Troops ----------
                new BattleSisters(),
                //---------- Elites ----------
                new AestredThurgaAgathaeDolan(),
                new Imagifier(),
                new Dialogus(),
                new Preacher(),
                new Celestian(),
                new CelestianSacresants(),
                new Hospitaller(),
                new Dogmata(),
                new ParagonWarsuits(),
                new RepentiaSuperior(),
                new SistersRepentia(),
                new Crusaders(),
                new ArcoFlagellants(),
                new DeathCultAssassins(),
                //---------- Fast Attack ----------
                new DominionSquad(),
                new Seraphim(),
                new Zephyrim(),
                //---------- Heavy Support ----------
                new Retributors(),
                new Mortifiers(),
                new PenitentEngines(),
                new Exorcist(),
                new Castigator(),
                //---------- Transport ----------
                new SororitasRhino(),
                new Immolator(),
                //---------- Fortification ----------
                new BattleSanctum()
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == "Word of the Emperor (+40 pts)")
            {
                return 40;
            }
            if (upgrade == "Rapturous Blows (+25 pts)")
            {
                return 25;
            }
            if (upgrade == "Divine Deliverance (+15 pts)")
            {
                return 15;
            }
            if (upgrade == "The Emperor's Grace (+20 pts)")
            {
                return 20;
            }
            if (upgrade == "Righteous Judgement (+25 pts)")
            {
                return 25;
            }
            if (upgrade == "Blinding Radiance (+30 pts)")
            {
                return 30;
            }

            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>()
            {
                "(None)",
                "Word of the Emperor (+40 pts)",
                "Rapturous Blows (+25 pts)",
                "Divine Deliverance (+15 pts)",
                "The Emperor's Grace (+20 pts)",
                "Righteous Judgement (+25 pts)",
                "Blinding Radiance (+30 pts)"
            };

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            if(keywords == "Priest")
            {
                return new List<string>()
                {
                    "Refrain of Blazing Piety",
                    "Chorus of Spiritual Fortitude",
                    "Psalm of Righteous Smiting"
                };
            }

            return new List<string>()
            {
                "Refrain of Blazing Piety",
                "Chorus of Spiritual Fortitude",
                "Psalm of Righteous Smiting",
                "Litany of Enduring Faith",
                "Verse of Holy Piety",
                "Catechism of Repugnance"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>()
            {
                "(None)",
                "Blade of Saint Ellynor",
                "Brazier of Eternal Flame",
                "Wrath of the Emperor",
                "Litanies of Faith",
                "Mantle of Ophelia",
                "Triptych of the Macharian Crusade",
                "Book of Saint Lucius",
                "Iron Surplice of Saint Istaela",
                "The Ecclesiarch's Fury",
                "Redemption",
                "The Sigil Ecclesiasticus",
                "Blessings of Sebastian Thor",
                "Simulacrum Sanctorum",
                "Chaplet of Sacrifice"
            };

            if (currentSubFaction == "Order of Our Martyred Lady")
            {
                relics.Add("Martyrs' Vengeance");
            }

            if (currentSubFaction == "Order of the Valorous Heart")
            {
                relics.Add("Casket of Penance");
            }

            if (currentSubFaction == "Order of the Bloody Rose")
            {
                relics.Add("Beneficence");
            }

            if (currentSubFaction == "Order of the Ebon Chalice")
            {
                relics.Add("Annunciation of the Creed");
            }

            if (currentSubFaction == "Order of the Argent Shroud")
            {
                relics.Add("Quicksilver Veil");
            }

            if (currentSubFaction == "Order of the Sacred Rose")
            {
                relics.Add("Light of Saint Agnaetha");
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "Order of Our Martyred Lady",
                "Order of the Valorous Heart",
                "Order of the Bloody Rose",
                "Order of the Ebon Chalice",
                "Order of the Argent Shroud",
                "Order of the Sacred Rose"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>();

            traits.AddRange(new string[]
            {
                "Inspiring Orator",
                "Righteous Rage",
                "Executioner of Heretics",
                "Beacon of Faith",
                "Indomitable Belief",
                "Pure of Will"
            });

            if(currentSubFaction == "Order of Our Martyred Lady")
            {
                traits.Add("Shield Bearer");
            }

            if (currentSubFaction == "Order of the Valorous Heart")
            {
                traits.Add("Impervious to Pain");
            }

            if (currentSubFaction == "Order of the Bloody Rose")
            {
                traits.Add("Blazing Ire");
            }

            if (currentSubFaction == "Order of the Ebon Chalice")
            {
                traits.Add("Terrible Knowledge");
            }

            if (currentSubFaction == "Order of the Argent Shroud")
            {
                traits.Add("Selfless Heroism");
            }

            if (currentSubFaction == "Order of the Sacred Rose")
            {
                traits.Add("Light of the Divine");
            }

            return traits;
        }

        public override void SetPoints(int points)
        {
        }

        public override string ToString()
        {
            return "Adepta Sororitas";
        }
    }
}
