using Roster_Builder.Aeldari.Ynnari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class Aeldari : Faction
    {
        public Aeldari()
        {
            subFactionName = "<Craftworld>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Exarch Powers";
            customSubFactionTraits = new string[2];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Champion of the Aeldari",
                "Stratagem: Treasures of the Aeldari",
                "Stratagem: Relics of the Shrines",
                "Stratagem: Seer Council"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>
            {
                "Children of Khaine",
                "Children of Morai-Heg",
                "Children of Prophecy",
                "Children of the Open Skies",
                "Diviners of Fate",
                "Elite Citizenry",
                "Expert Crafters",
                "Grim",
                "Hail of Doom",
                "Headstrong",
                "Hunters of Ancient Relics",
                "Masterful Shots",
                "Masters of Concealment",
                "Mobile Fighters",
                "Savage Blades",
                "Swift Strikes",
                "Students of Vaul",
                "Superior Shurikens",
                "Vengeful",
                "Warding Runes",
                "Webway Warriors",
                "Wrath of the Dead"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>
            {
                "Children of Khaine",
                "Children of Morai-Heg",
                "Children of Prophecy",
                "Children of the Open Skies",
                "Diviners of Fate",
                "Elite Citizenry",
                "Expert Crafters",
                "Grim",
                "Hail of Doom",
                "Headstrong",
                "Hunters of Ancient Relics",
                "Masterful Shots",
                "Masters of Concealment",
                "Mobile Fighters",
                "Savage Blades",
                "Swift Strikes",
                "Students of Vaul",
                "Superior Shurikens",
                "Vengeful",
                "Warding Runes",
                "Webway Warriors",
                "Wrath of the Dead"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>
            {
                //---------- HQ ----------
                new AvatarofKhaine(),
                new FarseerSkyrunner(),
                new EldradUlthran(),
                new Farseer(),
                new Autarch(),
                new PrinceYriel(),
                new AutarchSkyrunner(),
                new Asurmen(),
                new Baharroth(),
                new Fuegan(),
                new JainZar(),
                new Karandras(),
                new MauganRa(),
                new IllicNightspear(),
                new Spiritseer(),
                //---------- Troops ----------
                new GuardianDefenders(),
                new Rangers(),
                new StormGuardians(),
                new CorsairVoidreavers(),
                //---------- Elites ----------
                new CorsairVoidscarred(),
                new Warlocks(),
                new WarlockSkyrunners(),
                new DireAvengers(),
                new FireDragons(),
                new HowlingBanshees(),
                new StrikingScorpions(),
                new Wraithblades(),
                new Wraithguard(),
                new Wraithlord(),
                //---------- Fast Attack ----------
                new Windriders(),
                new Vypers(),
                new SwoopingHawks(),
                new WarpSpiders(),
                new ShiningSpears(),
                new ShroudRunners(),
                //---------- Heavy Support ----------
                new WarWalkers(),
                new DarkReapers(),
                new SupportWeapons(),
                new Falcon(),
                new NightSpinner(),
                new FirePrism(),
                //---------- Transport ----------
                new WaveSerpent(),
                //---------- Flyers ----------
                new CrimsonHunter(),
                new HemlockWraithfighter(),
                //---------- Lords of War ----------
                new Wraithknight(),
                //---------- Fortification ----------
                new WebwayGate(),
                //---------- Ynarri (Temp) ----------
                new Yvraine(),
                new TheVisarch(),
                new Yncarne()
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] thirty = new string[]
            {
                "Eyes of Khaine (+30 pts)",
                "Scorpion's Sting (+30 pts)"
            };

            string[] twentyfive = new string[]
            {
                "Swooping Evasion (+25 pts)",
                "Shredding Fire (+25 pts)",
                "Bringer of Death (+25 pts)",
                "Burning Heat (+25 pts)"
            };

            string[] twenty = new string[]
            {
                "Strafing Assault (+20 pts)",
                "Defensive Stance (+20 pts)",
                "Expert Lancers (+20 pts)",
                "Lightning Attacks (+20 pts)",
                "Graceful Avoidance (+20 pts)",
                "Deadly Ambush (+20 pts)",
                "Blazing Fury (+20 pts)",
                "Rapid Deployment (+20 pts)",
                "Heartstrike (+20 pts)",
            };

            string[] fifteen = new string[]
            {
                "Spider's Lair (+15 pts)",
                "Surprise Assault (+15 pts)",
                "Web of Deceit (+15 pts)",
                "Piercing Strikes (+15 pts)",
                "Curshing Blows (+15 pts)",
                "Focused Fire (+15 pts)",
                "Reaper's Reach (+15 pts)",
                "Dragon's Bite (+15 pts)",
                "Suppressing Fire (+15 pts)",
                "Winged Evasion (+15 pts)",
                "Stand Firm (+15 pts)",
            };

            string[] ten = new string[]
            {
                "Nerve Shredding Shriek (+10 pts)"
            };

            if(thirty.Contains(upgrade))
            {
                points += 30;
            }
            else if (twentyfive.Contains(upgrade))
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
            else if (ten.Contains(upgrade))
            {
                points += 10;
            }

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>() { "(None)" };

            if(keywords.Contains("CRIMSON HUNTER"))
            {
                upgrades.AddRange(new string[]
                {
                    "Eyes of Khaine (+30 pts)",
                    "Strafing Assault (+20 pts)",
                    "Swooping Evasion (+25 pts)"
                });
            }

            if (keywords.Contains("DIRE AVENGERS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Defensive Stance (+20 pts)",
                    "Shredding Fire (+25 pts)",
                    "Stand Firm (+15 pts)",
                });
            }

            if (keywords.Contains("WARP SPIDERS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Spider's Lair (+15 pts)",
                    "Surprise Assault (+15 pts)",
                    "Web of Deceit (+15 pts)",
                });
            }

            if (keywords.Contains("SHINING SPEARS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Expert Lancers (+20 pts)",
                    "Heartstrike (+20 pts)",
                    "Lightning Attacks (+20 pts)"
                });
            }

            if (keywords.Contains("HOWLING BANSHEES"))
            {
                upgrades.AddRange(new string[]
                {
                    "Graceful Avoidance (+20 pts)",
                    "Nerve Shredding Shriek (+10 pts)",
                    "Piercing Strikes (+15 pts)"
                });
            }

            if (keywords.Contains("STRIKING SCORPIONS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Curshing Blows (+15 pts)",
                    "Deadly Ambush (+20 pts)",
                    "Scorpion's Sting (+30 pts)"
                });
            }

            if (keywords.Contains("DARK REAPERS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Bringer of Death (+25 pts)",
                    "Focused Fire (+15 pts)",
                    "Reaper's Reach (+15 pts)"
                });
            }

            if (keywords.Contains("FIRE DRAGONS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Blazing Fury (+20 pts)",
                    "Burning Heat (+25 pts)",
                    "Dragon's Bite (+15 pts)"
                });
            }

            if (keywords.Contains("SWOOPING HAWKS"))
            {
                upgrades.AddRange(new string[]
                {
                    "Rapid Deployment (+20 pts)",
                    "Suppressing Fire (+15 pts)",
                    "Winged Evasion (+15 pts)"
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
            List<string> PsychicPowers = new List<string>();

            if (keywords == "Battle")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Conceal/Reveal",
                    "Embolden/Horrify",
                    "Enhance/Drain",
                    "Protect/Jinx",
                    "Quicken/Restrain",
                    "Empower/Enervate"
                });
            }

            if (keywords == "Fate")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Guide",
                    "Doom",
                    "Fortune",
                    "Executioner",
                    "Will of Asuryan",
                    "Mind War"
                });
            }

            if (keywords == "Fortune")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Fateful Divergence",
                    "Witch Strike",
                    "Ghostwalk",
                    "Crushing Orb",
                    "Focus Will",
                    "Impair Senses"
                });
            }

            if(keywords == "Farseer")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Guide",
                    "Doom",
                    "Fortune",
                    "Executioner",
                    "Will of Asuryan",
                    "Mind War",
                    "Fateful Divergence",
                    "Witch Strike",
                    "Ghostwalk",
                    "Crushing Orb",
                    "Focus Will",
                    "Impair Senses"
                });
            }

            if(keywords == "Spiritseer")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Conceal/Reveal",
                    "Embolden/Horrify",
                    "Enhance/Drain",
                    "Protect/Jinx",
                    "Quicken/Restrain",
                    "Empower/Enervate",
                    "Fateful Divergence",
                    "Witch Strike",
                    "Ghostwalk",
                    "Crushing Orb",
                    "Focus Will",
                    "Impair Senses"
                });
            }

            if (keywords == "Revenant")
            {
                PsychicPowers.AddRange(new string[]
                {
                    "Gaze of Ynnead",
                    "Storm of Whispers",
                    "Word of the Phoenix",
                    "Unbind Souls",
                    "Shield of Ynnead",
                    "Ancestors' Grace"
                });
            }

            return PsychicPowers;
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>() { "(None)" };

            if (keywords.Contains("DIRE AVENGERS"))
            {
                relics.Add("The Avenging Blade");
            }
            else if (keywords.Contains("FIRE DRAGONS"))
            {
                relics.Add("Dragon's Fury");
            }
            else if (keywords.Contains("STRIKING SCORPIONS"))
            {
                relics.Add("Shadowsting");
            }
            else if (keywords.Contains("HOWLING BANSHEES"))
            {
                relics.Add("Cronescream");
            }
            else if (keywords.Contains("SWOOPING HAWKS"))
            {
                relics.Add("The Phoenix Plume");
            }
            else if (keywords.Contains("WARP SPIDERS"))
            {
                relics.Add("The Spider's Bite");
            }
            else if (keywords.Contains("SHINING SPEARS"))
            {
                relics.Add("Khaine's Lance");
            }
            else if (keywords.Contains("DARK REAPERS"))
            {
                relics.Add("Shrine Skull");
            }
            else
            {
                if(!keywords.Contains("AUTARCH SKYRUNNER")) {
                    relics.Add("Kurnous' Bow");
                }

                relics.Add("The Phoenix Gem");

                if(keywords.Contains("AUTARCH"))
                {
                    relics.Add("Shard of Anaris");
                }

                if(!keywords.Contains("BIKER"))
                {
                    relics.Add("Faolchú's Wing");
                }

                if (keywords.Contains("AUTARCH"))
                {
                    relics.Add("Firesabre");
                }

                if (keywords.Contains("BIKER"))
                {
                    relics.Add("Sunstorm");
                }

                if (keywords.Contains("AUTARCH"))
                {
                    relics.Add("Aegis of Eldanesh");
                }

                if(keywords.Contains("PSYKER") && keywords.Contains("BIKER"))
                {
                    relics.Add("The Weeping Stones");
                }
                
                if(currentSubFaction == "Ulthwé" && keywords.Contains("PSYKER"))
                {
                    relics.Add("The Ghosthelm of Alishazier");
                }
                else if (currentSubFaction == "Alaitoc" && !keywords.Contains("BIKER"))
                {
                    relics.Add("Shiftshroud of Alanssair");
                }
                else if(currentSubFaction == "Biel-tan" && keywords.Contains("PSYKER"))
                {
                    relics.Add("The Spirit Stone of Anath'lan");
                }
                else if(currentSubFaction == "Iyanden")
                {
                    relics.Add("Psytronome of Iyanden");
                }
                else if (currentSubFaction == "Saim-hann")
                {
                    relics.Add("Talisman of Tionchar");
                }
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>
            {
                string.Empty,
                "Ulthwé",
                "Alaitoc",
                "Biel-tan",
                "Iyanden",
                "Saim-hann",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>()
            {
                "Ambush of Blades",
                "Walker of Many Paths",
                "Falcon's Swiftness",
                "Fate's Messenger",
                "Mark of the Incomparable Hunter",
                "Seer of the Shifting Vector"
            };

            if (currentSubFaction == "Ulthwé")
            {
                traits.Add("Fate Reader");
            }
            else if (currentSubFaction == "Alaitoc")
            {
                traits.Add("Master of Ambush");
            }
            else if (currentSubFaction == "Biel-tan")
            {
                traits.Add("Natural Leader");
            }
            else if (currentSubFaction == "Iyanden")
            {
                traits.Add("Enduring Resolve");
            }
            else if (currentSubFaction == "Saim-hann")
            {
                traits.Add("Wild Rider Chieftain");
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
                "Children of Khaine",
                "Children of Morai-Heg",
                "Children of Prophecy",
                "Children of the Open Skies",
                "Diviners of Fate",
                "Elite Citizenry",
                "Expert Crafters",
                "Grim",
                "Hail of Doom",
                "Headstrong",
                "Hunters of Ancient Relics",
                "Masterful Shots",
                "Masters of Concealment",
                "Mobile Fighters",
                "Savage Blades",
                "Swift Strikes",
                "Students of Vaul",
                "Superior Shurikens",
                "Vengeful",
                "Warding Runes",
                "Webway Warriors",
                "Wrath of the Dead"
            });

            cmbSubCustom2.Items.AddRange(new string[]
            {
                "Children of Khaine",
                "Children of Morai-Heg",
                "Children of Prophecy",
                "Children of the Open Skies",
                "Diviners of Fate",
                "Elite Citizenry",
                "Expert Crafters",
                "Grim",
                "Hail of Doom",
                "Headstrong",
                "Hunters of Ancient Relics",
                "Masterful Shots",
                "Masters of Concealment",
                "Mobile Fighters",
                "Savage Blades",
                "Swift Strikes",
                "Students of Vaul",
                "Superior Shurikens",
                "Vengeful",
                "Warding Runes",
                "Webway Warriors",
                "Wrath of the Dead"
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
            return "Aeldari";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
