using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tyranids
{
    public class Tyranids : Faction
    {
        public Tyranids()
        {
            subFactionName = "<Hive Fleet>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Adaptive Physiologies";
            customSubFactionTraits = new string[2];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Hive Predator",
                "Stratagem: Rareified Enhancements",
                "Stratagem: Buried in Wait"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string> 
            {
                "Adrenalised Onslaught",
                "Heightened Reflexes",
                "Augmented Ferocity",
                "Synaptic Goading",
                "Ambush Predators",
                "Exoskeletal Reinforcement",
                "Naturalised Camouflage",
                "Territorial Insticts",
                "Unfeeling Resilience",
                "Synaptic Ganglia",
                "Stabilising Membranes",
                "Exoskeletal Stabilisation",
                "Wreathed in Shadow",
                "Relentless Hunger",
                "Unstoppable Swarm"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            string[] hunt = new string[]
            {
                "Adrenalised Onslaught",
                "Heightened Reflexes",
                "Augmented Ferocity",
                "Synaptic Goading",
                "Ambush Predators"
            };

            string[] lurk = new string[]
            {
                "Exoskeletal Reinforcement",
                "Naturalised Camouflage",
                "Territorial Insticts",
                "Unfeeling Resilience",
                "Synaptic Ganglia"
            };

            string[] feed = new string[]
            {
                "Stabilising Membranes",
                "Exoskeletal Stabilisation",
                "Wreathed in Shadow",
                "Relentless Hunger",
                "Unstoppable Swarm"
            };

            return new List<string>
            {
                "Adrenalised Onslaught",
                "Heightened Reflexes",
                "Augmented Ferocity",
                "Synaptic Goading",
                "Ambush Predators",
                "Exoskeletal Reinforcement",
                "Naturalised Camouflage",
                "Territorial Insticts",
                "Unfeeling Resilience",
                "Synaptic Ganglia",
                "Stabilising Membranes",
                "Exoskeletal Stabilisation",
                "Wreathed in Shadow",
                "Relentless Hunger",
                "Unstoppable Swarm"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
                //---------- HQ ----------
                new WingedHiveTyrant(),
                new HiveTyrant(),
                new Swarmlord(),
                new Broodlord(),
                new Neurothrope(),
                new TyranidPrime(),
                new Tervigon(),
                new TrygonPrime(),
                new OldOneEye(),
                //---------- Troops ----------
                new TyranidWarriors(),
                new Termagants(),
                new Hormagaunts(),
                new Gargoyles(),
                //---------- Elites ----------
                new Toxicrene(),
                new TyrantGuard(),
                new Lictor(),
                new Deathleaper(),
                new Maleceptor(),
                new Pyrovores(),
                new Haruspex(),
                new Venomthropes(),
                new Zoanthropes(),
                new Genestealers(),
                //---------- Fast Attack ----------
                new Raveners(),
                new RipperSwarms(),
                new ParasiteofMortrex(),
                new Mawloc(),
                new Trygon(),
                new MucolidSpores(),
                new SporeMines(),
                //---------- Heavy Support ----------
                new Exocrine(),
                new Biovores(),
                new Carnifexes(),
                new ScreamerKillers(),
                new Thornbacks(),
                new HiveGuard(),
                new Tyrannofex(),
                new Tyrannocyte(),
                //---------- Flyers ----------
                new HiveCrone(),
                new Harpy(),
                //---------- Fortification ----------
                new Sporocyst()
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] twentyfive = new string[]
            {
                "Dermic Symbiosis (+25 pts)",
                "Enraged Reserves (+25 pts)"
            };

            string[] twenty = new string[]
            {
                "Precognitive Sensoria (+20 pts)"
            };

            string[] fifteen = new string[]
            {
                "Hardened Biology (+15 pts)",
                "Predator Instincts (+15 pts)",
                "Synaptic Enhancement (+15 pts)",
                "Whipcoil Reflexes (+15 pts)",
                "Voracious Ammunition (+15 pts)"
            };

            if (twentyfive.Contains(upgrade))
            {
                points = 25;
            }

            if (twenty.Contains(upgrade))
            {
                points = 20;
            }

            if (fifteen.Contains(upgrade))
            {
                points = 15;
            }

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>() 
            { 
                "(None)",
                "Dermic Symbiosis (+25 pts)",
                "Enraged Reserves (+25 pts)",
                "Precognitive Sensoria (+20 pts)",
                "Hardened Biology (+15 pts)",
                "Predator Instincts (+15 pts)",
                "Synaptic Enhancement (+15 pts)",
                "Voracious Ammunition (+15 pts)",
                "Whipcoil Reflexes (+15 pts)"
            };

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            List<string> PsychicPowers = new List<string>();

            PsychicPowers = new List<string>
            {
                "Catalyst",
                "The Horror",
                "Neuroparasite",
                "Onslaught",
                "Paroxysm",
                "Psychic Scream"
            };

            if(currentSubFaction == "Behmoth")
            {
                PsychicPowers.Add("Unstoppable Onslaught");
            }
            if (currentSubFaction == "Kraken")
            {
                PsychicPowers.Add("Synaptic Lure");
            }
            if (currentSubFaction == "Leviathan")
            {
                PsychicPowers.Add("Hive Nexus");
            }
            if (currentSubFaction == "Gorgon")
            {
                PsychicPowers.Add("Poisonous Influence");
            }
            if (currentSubFaction == "Jormungandr")
            {
                PsychicPowers.Add("Lurking Maws");
            }
            if (currentSubFaction == "Kronos")
            {
                PsychicPowers.Add("Symbiostorm");
            }
            if (currentSubFaction == "Hydra")
            {
                PsychicPowers.Add("Psychic Shriek");
            }

            return PsychicPowers;
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            relics.Add("The Ymgarl Factor");

            if(keywords.Contains("HIVE TYRANT") || keywords.Contains("TYRANID PRIME"))
            {
                relics.Add("The Reaper of Obliterax");
            }

            relics.Add("The Maw-claws of Thyrax");

            if(keywords.Contains("PSYKER"))
            {
                relics.Add("Resonance Barb");
            }

            relics.Add("Pathogenesis");

            if(keywords.Contains("HIVE TYRANT"))
            {
                relics.Add("Scythes of Tyran");
            }

            if (keywords.Contains("HIVE TYRANT"))
            {
                relics.Add("Balethorn Cannon");
            }

            if (keywords.Contains("HIVE TYRANT"))
            {
                relics.Add("Shardgullet");
            }

            relics.Add("Gestation Sac");
            relics.Add("The Dirgeheart of Kharis");

            if(keywords.Contains("HIVE TYRANT") || keywords.Contains("TERVIGON") || keywords.Contains("TRYGON PRIME")
                || keywords.Contains("TYRANID PRIME"))
            {
                relics.Add("The Passenger");
            }

            if (keywords.Contains("HIVE TYRANT") || keywords.Contains("TERVIGON") || keywords.Contains("TRYGON PRIME")
                || keywords.Contains("TYRANID PRIME"))
            {
                relics.Add("Searhive");
            }

            if (currentSubFaction == "Behemoth")
            {
                relics.Add("Monstrous Musculature");
            }
            if (currentSubFaction == "Kraken")
            {
                relics.Add("Chameleonic Mutation");
            }
            if (currentSubFaction == "Leviathan")
            {
                relics.Add("Preceptic Node");
            }
            if (currentSubFaction == "Gorgon")
            {
                relics.Add("Hypermorphic Biology");
            }
            if (currentSubFaction == "Jormungandr")
            {
                relics.Add("Infrasonic Radar");
            }
            if (currentSubFaction == "Kronos")
            {
                relics.Add("Nullnode");
            }
            if (currentSubFaction == "Hydra")
            {
                relics.Add("Barbworm Infestation");
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Behemoth",
                "Kraken",
                "Leviathan",
                "Gorgon",
                "Jormungandr",
                "Kronos",
                "Hydra",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>();

            traits.AddRange(new string[]
            {
                "Alien Cunning",
                "Heightened Senses",
                "Synaptic Linchpin",
                "Direct Guidance",
                "Synaptic Tendrils",
                "Adaptive Biology"
            });

            if (currentSubFaction == "Behemoth")
            {
                traits.Add("Monstrous Hunger");
            }
            if (currentSubFaction == "Kraken")
            {
                traits.Add("One Step Ahead");
            }
            if (currentSubFaction == "Leviathan")
            {
                traits.Add("Perfectly Adapted");
            }
            if (currentSubFaction == "Gorgon")
            {
                traits.Add("Lethal Miasma");
            }
            if (currentSubFaction == "Jormungandr")
            {
                traits.Add("Insidious Threat");
            }
            if (currentSubFaction == "Kronos")
            {
                traits.Add("Soul Hunger");
            }
            if (currentSubFaction == "Hydra")
            {
                traits.Add("Endless Regeneration");
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

            string[] hunt = new string[]
            {
                "Adrenalised Onslaught",
                "Heightened Reflexes",
                "Augmented Ferocity",
                "Synaptic Goading",
                "Ambush Predators"
            };

            string[] lurk = new string[]
            {
                "Exoskeletal Reinforcement",
                "Naturalised Camouflage",
                "Territorial Insticts",
                "Unfeeling Resilience",
                "Synaptic Ganglia"
            };

            string[] feed = new string[]
            {
                "Stabilising Membranes",
                "Exoskeletal Stabilisation",
                "Wreathed in Shadow",
                "Relentless Hunger",
                "Unstoppable Swarm"
            };

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
                    cmbSubCustom2.Items.Clear();

                    if (hunt.Contains(customSubFactionTraits[0]))
                    {
                        cmbSubCustom2.Items.AddRange(lurk);
                        cmbSubCustom2.Items.AddRange(feed);
                    }
                    else if (lurk.Contains(customSubFactionTraits[0]))
                    {
                        cmbSubCustom2.Items.AddRange(hunt);
                        cmbSubCustom2.Items.AddRange(feed);
                    }
                    else if (feed.Contains(customSubFactionTraits[0]))
                    {
                        cmbSubCustom2.Items.AddRange(hunt);
                        cmbSubCustom2.Items.AddRange(lurk);
                    }
                    else
                    {
                        cmbSubCustom2.Items.AddRange(hunt);
                        cmbSubCustom2.Items.AddRange(lurk);
                        cmbSubCustom2.Items.AddRange(feed);
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
                "Adrenalised Onslaught",
                "Heightened Reflexes",
                "Augmented Ferocity",
                "Synaptic Goading",
                "Ambush Predators",
                "Exoskeletal Reinforcement",
                "Naturalised Camouflage",
                "Territorial Insticts",
                "Unfeeling Resilience",
                "Synaptic Ganglia",
                "Stabilising Membranes",
                "Exoskeletal Stabilisation",
                "Wreathed in Shadow",
                "Relentless Hunger",
                "Unstoppable Swarm"
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
            return "Tyranids";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
