using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class Necrons : Faction
    {
        public Necrons()
        {
            subFactionName = "<Dynasty>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Cryptek Arkana";
            customSubFactionTraits = new string[2];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Rarefied Nobility",
                "Stratagem: Dynastic Heirlooms",
                "Stratagem: Hand of the Phaeron"
            });
        }
        public override void SetUpForm(Form form)
        {
            base.SetUpForm(form);

            Panel panel = form.Controls["panel1"] as Panel;

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;
            Label lblFactionupgrade = panel.Controls["lblFactionupgrade"] as Label;

            cbStratagem1.Text = StratagemList[0];
            cbStratagem2.Text = StratagemList[1];
            lblFactionupgrade.Text = factionUpgradeName;
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>()
            {
                "Eternal Conquerors",
                "Pitiless Hunters",
                "Superior Artisans",
                "Rad-wreathed",
                "Immovable Phalanx",
                "Unyielding",
                "Contemptuous of the Codes",
                "The Unmerciful Horde",
                "Masters of the Martial",
                "Butchers",
                "Severed",
                "Vassal Kingdom"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>()
            {
                "The Ancients Stir",
                "Arise Against the Interlopers",
                "Healthy Paranoia",
                "Relentlessly Expansionist",
                "Isolationists",
                "Warrior Nobles",
                "Interplanetary Invaders"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>()
            {
                //---------- HQ ----------
                new Imotekh(),
                new Orikan(),
                new Anrakyr(),
                new Obyron(),
                new Szeras(),
                new Zahndrekh(),
                new Trazyn(),
                new RoyalWarden(),
                new SkorpekhLord(),
                new LokhustLord(),
                new NecronLord(),
                new CatacombBarge(),
                new Overlord(),
                new Technomancer(),
                new Psychomancer(),
                new Chronomancer(),
                new Plasmancer(),
                //---------- Troops ----------
                new NecronWarriors(),
                new Immortals(),
                //---------- Elites ----------
                new CanoptekReanimator(),
                new HexmarkDestroyer(),
                new Lychguard(),
                new Deathmarks(),
                new FlayedOnes(),
                new Cryptothralls(),
                new SkorpekhDestroyers(),
                new CanoptekPlasmacyte(),
                new TriarchStalker(),
                new CtanDeceiver(),
                new CtanNightbringer(),
                new CtanVoidDragon(),
                new TranscendentCtan(),
                new CanoptekSpyders(),
                //---------- Fast Attack ----------
                new CanoptekScarabs(),
                new OphydianDestroyers(),
                new TombBlades(),
                new TriarchPraetorians(),
                new CanoptekWraiths(),
                //---------- Heavy Support ----------
                new AnnihilationBarge(),
                new DoomsdayArk(),
                new LokhustDestroyers(),
                new LokhustHeavyDestroyers(),
                new CanoptekDoomstalker(),
                //---------- Transport ----------
                new GhostArk(),
                //---------- Flyers ----------
                new DoomScythe(),
                new NightScythe(),
                //---------- Lords of War ----------
                new Obelisk(),
                new TesseractVault(),
                new Monolith(),
                new SilentKing(),
                //---------- Fortification ----------
                new ConvergenceOfDominion(),
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] thirty = new string[]
            {
                "Countertemporal Nanomines (+25 pts)", "Fail-Safe Overcharger (+25 pts)"
            };

            string[] twentyFive = new string[]
            {
                "Atavindicator (+20 pts)", "Hypermaterial Ablator (+20 pts)"
            };

            string[] twenty = new string[]
            {
                "Metalodermal Telsa Weave (+15 pts)", "Photonic Transubjector (+15 pts)", "Phylacterine Hive (+15 pts)",
                "Prismatic Obfuscatron (+15 pts)", "Quantum Orb (+15 pts)"
            };

            string[] fifteen = new string[]
            {
                "Cortical Subjugator Scarabs (+10 pts)", "Cryptogeometric Adjustor (+10 pts)", "Dimensional Sanctum (+10 pts)"
            };

            if(thirty.Contains(upgrade))
            {
                points += 30;
            }
            else if (twentyFive.Contains(upgrade))
            {
                points += 25;
            }
            else if (twenty.Contains(upgrade))
            {
                points += 20;
            }
            else if (fifteen.Contains(upgrade))
            {
                points += 15;
            }

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>() { "(None)" };

            if(keywords.Contains("PSYCHOMANCER"))
            {
                upgrades.Add("Atavindicator (+20 pts)");
            }

            upgrades.Add("Cortical Subjugator Scarabs (+10 pts)");

            if(keywords.Contains("CHRONOMANCER"))
            {
                upgrades.Add("Countertemporal Nanomines (+25 pts)");
            }

            upgrades.Add("Cryptogeometric Adjustor (+10 pts)");
            upgrades.Add("Dimensional Sanctum (+10 pts)");

            if (keywords.Contains("TECHNOMANCER"))
            {
                upgrades.Add("Fail-Safe Overcharger (+25 pts)");
            }

            upgrades.Add("Hypermaterial Ablator (+20 pts)");
            upgrades.Add("Metalodermal Telsa Weave (+15 pts)");
            upgrades.Add("Photonic Transubjector (+15 pts)");

            if (keywords.Contains("TECHNOMANCER"))
            {
                upgrades.Add("Phylacterine Hive (+15 pts)");
            }

            upgrades.Add("Prismatic Obfuscatron (+15 pts)");

            if (keywords.Contains("PLASMANCER"))
            {
                upgrades.Add("Quantum Orb (+15 pts)");
            }

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            /*
            if (StratagemCount[index] < StratagemLimit[index])
            {
                return true;
            }

            return false;
            */
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string>()
            {
                "Antimatter Meteor",
                "Time's Arrow",
                "Sky of Falling Stars",
                "Cosmic Fire",
                "Seismic Assault",
                "Transdimensional Thunderbolt"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            if(keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
               keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD"))
            {
                relics.Add("Orb of Eternity");  //Lokhust Lord, Lord, Catacomb Command Barge, Overlord
            }

            relics.Add("Nanoscarab Casket"); //Any

            relics.Add("Gauntlet of the Conflagrator"); //Any

            relics.Add("Veil of Darkness"); //Any

            if (keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
                keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD") ||
                keywords.Contains("TECHNOMANCER"))
            {
                relics.Add("Voltaic Staff");    //Lokhust Lord, Lord, Catacomb Command Barge, Overlord, Technomancer
            }

            if (keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
                keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD"))
            {
                relics.Add("Voidreaper");   //Lokhust Lord, Lord, Catacomb Command Barge, Overlord
            }

            if (keywords.Contains("LORD") || (keywords.Contains("OVERLORD") && !keywords.Contains("CATACOMB COMMAND BARGE")))
            {
                relics.Add("Sempiternal Weave");    //Lord, Overlord
            }

            if (keywords.Contains("OVERLORD") && !keywords.Contains("CATACOMB COMMAND BARGE"))
            {
                relics.Add("The Arrow of Infinity");    //Overlord
            }

            if (keywords.Contains("ROYAL WARDEN") && currentSubFaction == "Mephrit")
            {
                relics.Add("Conduit of Stars"); //Mephrit - Royal Warden
            }

            if ((keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
                keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD") ||
                keywords.Contains("TECHNOMANCER")) && currentSubFaction == "Nephrekh")
            {
                relics.Add("Solar Staff");  //Nephrekh - Lokhust Lord, Lord, Catacomb Command Barge, Overlord, Technomancer
            }

            if (currentSubFaction == "Nihilakh")
            {
                relics.Add("Infinity Mantle");  //Nihilakh
            }

            if ((keywords.Contains("LOKHUST LORD") || keywords.Contains("LORD") ||
                keywords.Contains("CATACOMB COMMAND BARGE") || keywords.Contains("OVERLORD")) &&
                currentSubFaction == "Novokh")
            {
                relics.Add("Blood Scythe"); //Novokh - Lokhust Lord, Lord, Catacomb Command Barge, Overlord
            }

            if (currentSubFaction == "Sautekh")
            {
                relics.Add("The Vanquisher's Mask"); //Sautekh
            }

            if ((keywords.Contains("LORD") || keywords.Contains("CATACOMB COMMAND BARGE") ||
                keywords.Contains("OVERLORD")) && currentSubFaction == "Szarekhan")
            {
                relics.Add("The Sovereign Coronal"); //Szarekhan - Lord, Catacomb Command Barge, Overlord
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Mephrit",
                "Novokh",
                "Szarekhan",
                "Nephrekh",
                "Nihilakh",
                "Sautekh",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>()
            {
                "Enduring Will",
                "Eternal Madness",
                "Immortal Pride",
                "Thrall of the Silent King",
                "Implacable Conqueror (Aura)",
                "Honourable Combatant"
            };

            if(currentSubFaction == "Mephrit") { traits.Add("Merciless Tyrant"); }
            else if (currentSubFaction == "Nephrekh") { traits.Add("Skin of Living Gold"); }
            else if (currentSubFaction == "Nihilakh") { traits.Add("Precognitive Strike"); }
            else if (currentSubFaction == "Novokh") { traits.Add("Blood-fuelled Fury"); }
            else if (currentSubFaction == "Sautekh") { traits.Add("Hyperlogical Strategist"); }
            else if (currentSubFaction == "Szarekhan") { traits.Add("The Triarch's Will"); }

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
            StratagemCount = new int[] { 0, 0, 0 };
            StratagemLimit = new int[] { points / 1000, points / 1000, 1 };

            if (points < 1000)
            {
                StratagemLimit[0] = 1;
                StratagemLimit[1] = 1;
            }
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
                "Eternal Conquerors",
                "Pitiless Hunters",
                "Superior Artisans",
                "Rad-wreathed",
                "Immovable Phalanx",
                "Unyielding",
                "Contemptuous of the Codes",
                "The Unmerciful Horde",
                "Masters of the Martial",
                "Butchers",
                "Severed",
            });

            cmbSubCustom2.Items.AddRange(new string[]
            {
                "The Ancients Stir",
                "Arise Against the Interlopers",
                "Healthy Paranoia",
                "Relentlessly Expansionist",
                "Isolationists",
                "Warrior Nobles",
                "Interplanetary Invaders"
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
            return "Necrons";
        }
    }
}
