using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class AdeptaSororitas : Faction
    {
        public AdeptaSororitas()
        {
            subFactionName = "<Order>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Blessings of the Faithful";
            customSubFactionTraits = new string[2];
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
            List<string> relics = new List<string>();
            relics.Add("(None)");

            //Superior Relics
            if(keywords.Contains("CORE"))
            {
                relics.Add("Litanies of Faith");

                if(!(keywords.Contains("CELESTIAN SACRESANTS") || keywords.Contains("PARAGON WARSUITS") || keywords.Contains("ZEPHYRIM SQUAD")))
                {
                    relics.Add("The Ecclesiarch's Fury");
                }

                if(!(keywords.Contains("PARAGON WARSUITS")))
                {
                    relics.Add("Redemption");
                }

                relics.Add("Blessings of Sebastian Thor");

                return relics;
            }

            if(keywords.Contains("CANONESS"))
            {
                relics.Add("Blade of Saint Ellynor");
                relics.Add("Brazier of Eternal Flame");
            }

            if(!(keywords.Contains("IMAGIFIER") || keywords.Contains("PREACHER") || keywords.Contains("REPENTIA SUPERIOR") || keywords.Contains("MISSIONARY")))
            {
                relics.Add("Wrath of the Emperor");
            }

            if(keywords.Contains("ADEPTA SORORITAS"))
            {
                relics.Add("Litanies of Faith");
            }

            if(keywords.Contains("CANONESS"))
            {
                relics.Add("Mantle of Ophelia");
            }

            if(keywords.Contains("SANCTIFIED") || keywords.Contains("CULT IMPERIALIS"))
            {
                relics.Add("Triptych of the Macharian Crusade");
            }

            relics.Add("Book of Saint Lucius");

            if (keywords.Contains("ADEPTA SORORITAS"))
            {
                relics.Add("Iron Surplice of Saint Istaela");
            }

            if(keywords.Contains("CANONESS") || keywords.Contains("MISSIONARY") || keywords.Contains("PREACHER"))
            {
                relics.Add("The Ecclesiarch's Fury");
            }

            if(keywords.Contains("CANONESS") || keywords.Contains("PALATINE"))
            {
                relics.Add("Redemption");
            }

            if(keywords.Contains("PRIEST"))
            {
                relics.Add("The Sigil Ecclesiasticus");
            }

            if(keywords.Contains("ADEPTA SORORITAS"))
            {
                relics.Add("Blessings of Sebastian Thor");
            }

            if(keywords.Contains("IMAGIFIER"))
            {
                relics.Add("Simulacrum Sanctorum");
            }

            if(keywords.Contains("ADEPTA SORORITAS"))
            {
                relics.Add("Chaplet of Sacrifice");
            }

            //if (currentSubFaction == "Order of Our Martyred Lady" && keywords.Contains("CANONESS"))
            //{
            //    relics.Add("Martyrs' Vengeance");
            //}

            //if (currentSubFaction == "Order of the Valorous Heart" && keywords.Contains("ADEPTA SORORITAS"))
            //{
            //    relics.Add("Casket of Penance");
            //}

            //if (currentSubFaction == "Order of the Bloody Rose" && keywords.Contains("CANONESS"))
            //{
            //    relics.Add("Beneficence");
            //}

            //if (currentSubFaction == "Order of the Ebon Chalice" && keywords.Contains("CANONESS"))
            //{
            //    relics.Add("Annunciation of the Creed");
            //}

            //if (currentSubFaction == "Order of the Argent Shroud" && keywords.Contains("ADEPTA SORORITAS"))
            //{
            //    relics.Add("Quicksilver Veil");
            //}

            //if (currentSubFaction == "Order of the Sacred Rose" && keywords.Contains("CANONESS"))
            //{
            //    relics.Add("Light of Saint Agnaetha");
            //}

            if (keywords.Contains("CANONESS"))
            {
                relics.Add("Martyrs' Vengeance");
            }

            if (keywords.Contains("ADEPTA SORORITAS"))
            {
                relics.Add("Casket of Penance");
            }

            if (keywords.Contains("CANONESS"))
            {
                relics.Add("Beneficence");
            }

            if (keywords.Contains("CANONESS"))
            {
                relics.Add("Annunciation of the Creed");
            }

            if (keywords.Contains("ADEPTA SORORITAS"))
            {
                relics.Add("Quicksilver Veil");
            }

            if (keywords.Contains("CANONESS"))
            {
                relics.Add("Light of Saint Agnaetha");
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Order of Our Martyred Lady",
                "Order of the Valorous Heart",
                "Order of the Bloody Rose",
                "Order of the Ebon Chalice",
                "Order of the Argent Shroud",
                "Order of the Sacred Rose",
                "<Custom>"
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
                    break;
                case 51:
                    customSubFactionTraits[0] = cmbSubCustom1.SelectedItem.ToString();
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
            });

            cmbSubCustom2.Items.AddRange(new string[]
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
            });

            if (customSubFactionTraits[0] != null)
            {
                cmbSubCustom1.SelectedIndex = cmbSubCustom1.Items.IndexOf(customSubFactionTraits[0]);
                cmbSubCustom2.SelectedIndex = cmbSubCustom2.Items.IndexOf(customSubFactionTraits[1]);
            }
            antiLoop = false;
        }

        public override string ToString()
        {
            return "Adepta Sororitas";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
