﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Tau_Empire
{
    public class T_au : Faction
    {
        public T_au()
        {
            subFactionName = "<Sept>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Prototype Systems";
            customSubFactionTraits = new string[2];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Promising Pupil",
                "Stratagem: Emergency Dispensation",
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            string[] sectorA = new string[]
            {
                "Strike Swiftly",
                "Play Their Part",
                "Adherents to the Teachings",
                "Calm Under Pressure"
            };

            string[] sectorB = new string[]
            {
                "Camoflauge Experts",
                "Defensive Doctrines",
                "Blocking Tactics",
                "Fire Caste Marksmen"
            };

            string[] sectorC = new string[]
            {
                "Evasion Manoeuvres",
                "Pinpoint Targeting",
                "Disengagement Protocols",
                "Fire Saturation"
            };

            string[] sectorD = new string[]
            {
                "Reliable Weaponry",
                "Defenders of the Cause",
                "Reinforced Armour",
                "Hardened Warheads"
            };

            string[] sectorE = new string[]
            {
                "Turbo-Jets",
                "Loyal to the End",
                "Rapid Retreat",
                "Enriched Reactors"
            };

            return new List<string>();
        }

        public override List<string> GetCustomSubfactionList2()
        {
            //See the above
            return new List<string>();
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
                //---------- HQ ----------
                new CommanderShadowsun(),
                new CommanderFarsight(),
                new CrisisCommander(),
                new EnforcerCommander(),
                new ColdstarCommander(),
                new CadreFireblade(),
                new KrootShaper(),
                new Ethereal(),
                new Aun_Va(),
                new Aun_Shi(),
                new Darkstrider(),
                new Longstrike(),
                //---------- Troops ----------
                new StrikeTeam(),
                new BreacherTeam(),
                new KrootCarnivores(),
                //---------- Elites ----------
                new KrootoxRiders(),
                new CrisisBattlesuits(),
                new CrisisBodyguards(),
                new StealthBattlesuits(),
                new Ghostkeel(),
                new FiresightMarksman(),
                //---------- Fast Attack ----------
                new TacticalDrones(),
                new Pathfinders(),
                new Piranhas(),
                new VespidStingwings(),
                new KrootHounds(),
                //---------- Heavy Support ----------
                new Broadsides(),
                new Riptide(),
                new HammerheadGunship(),
                new SkyRayGunship(),
                //---------- Transport ----------
                new Devilfish(),
                //---------- Flyer ----------
                new RazorsharkFighter(),
                new SunSharkBomber(),
                //---------- Fortification ----------
                new TidewallShieldline(),
                new TidewallDefensePlatform(),
                new TidewallDroneport(),
                new TidewallGunrig(),
                //---------- Lord of War ----------
                new Stormsurge()
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] thirty = new string[]
            {
                "E-H Disruption Suite (+30 pts)",
                "Resonator Warheads (+30 pts)",
            };

            string[] twentyfive = new string[]
            {
                "Alternating Fusion Blaster (+25 pts)",
                "Dominator Fragmentation Launcher (+25 pts)"
            };

            string[] twenty = new string[]
            {
                "Novasurge Plasma Rifle (+20 pts)",
                "Starflare Ignition System (+20 pts)",
                "Thermoneutronic Projector (+20 pts)",
                "Wide Sprectrum Scanners (+20 pts)"
            };

            string[] fifteen = new string[]
            {
                "DW-02 Advanced Burst Cannon (+15 pts)",
                "Internal Grenade Racks (+15 pts)",
                "Sensory Negation Coutermeasures (+15 pts)",
            };

            string[] ten = new string[]
            {
                "Stimm Injector (+10 pts)",
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
            List<string> upgrades = new List<string>()
            {
                "(None)"
            };

            if(!keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("Alternating Fusion Blaster (+25 pts)"); //Commander/Crisis w/ Fusion Blasters
            };

            if (!keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("Dominator Fragmentation Launcher (+25 pts)"); //Commander/Crisis w/ Airbursting Frag Projectors
            };

            if (!keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("DW-02 Advanced Burst Cannon (+15 pts)"); //Commander/Crisis w/ Burst Cannons
			}

            if (keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("E-H Disruption Suite (+30 pts)"); //Ghostkeel
            }

            upgrades.Add("Internal Grenade Racks (+15 pts)");

            if (!keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("Novasurge Plasma Rifle (+20 pts)"); //Commander/Crisis w/ Plasma Rifles
			}

            if (!keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("Resonator Warheads (+30 pts)"); //Commander/Crisis w/ Missile Pods
			}

            if (!keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("Sensory Negation Coutermeasures (+15 pts)"); //Commander/Crisis
            }

            if (keywords.Contains("COLDSTAR"))
            {
                upgrades.Add("Starflare Ignition System (+20 pts)"); //Coldstar only
            }

            if (!keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("Stimm Injector (+10 pts)"); //Commander/Crisis
			}

            if (!keywords.Contains("GHOSTKEEL BATTLESUIT"))
            {
                upgrades.Add("Thermoneutronic Projector (+20 pts)"); //Commander/Crisis with Flamers
			}

            if (keywords.Contains("CRISIS"))
            {
                upgrades.Add("Wide Sprectrum Scanners (+20 pts)"); //Early Warning Override only
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
                "Storm of Fire",
                "Sense of Stone",
                "Zephyr's Grace",
                "Power of Tides",
                "Unifying Mantra",
                "Wisdom of the Guides"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
		{
			List<string> relics = new List<string>();

            relics.Add("(None)");
            relics.Add("Puretide Engram Neurochip");

            if(keywords.Contains("BATTLESUIT"))
            {
                relics.Add("Onager Gauntlet");      //Battlesuit
            }

            relics.Add("Multi-sensory Discouragement Array");
            relics.Add("Solid-image Projection Unit");
            relics.Add("Advanced EM Scrambler");

            if(keywords.Contains("KROOT"))
            {
                relics.Add("Borthrod Gland");       //Kroot
            }

            if(keywords.Contains("CADRE FIREBLADE") || keywords.Contains("FIRESIGHT MARKSMAN"))
            {
                relics.Add("Ohr'tu's Lantern");     //Markerlight
            }

            if(keywords.Contains("ETHEREAL"))
            {
                relics.Add("The Humble Stave");     //Ethereal
			}

            if (keywords.Contains("CADRE FIREBLADE"))
            {
                relics.Add("The Kindled Blade");    //Cadre Fireblade
			}

            if (keywords.Contains("ETHEREAL"))
            {
                relics.Add("Neuro-empathic Nullifier"); //Ethereal
            }

            relics.Add("The Be'gel Hunter's Plate");

            if (keywords.Contains("KROOT SHAPER"))
            {
                relics.Add("Ka'chak'tarr");         //Kroot Shaper
            }

			if (currentSubFaction == "T'au" && keywords.Contains("BATTLESUIT"))
			{
                relics.Add("Vectored Manoeuvring Thursters");
			}
			else if (currentSubFaction == "Vior'la" || keywords.Contains("COMMANDER"))
			{
                relics.Add("Automated Armour Defences"); //Commander
			}
			else if (currentSubFaction == "Sa'cea")
			{
                relics.Add("Grav-Inhibitor Field");
			}
			else if (currentSubFaction == "Dal'yth")
			{
                relics.Add("Dynamic Mirror Field");
			}
			else if (currentSubFaction == "Bork'an" || keywords.Contains("COMMANDER"))
			{
                relics.Add("Overdrive Power Systems"); //Commander
			}
			else if (currentSubFaction == "Farsight Enclaves")
			{
                relics.Add("Talisman of Arthas Moloch");
			}

			return relics;
		}

        public override List<string> GetSubFactions()
		{
            return new List<string>() 
            {
                string.Empty,
                "T'au",
                "Vior'la",
                "Sa'cea",
                "Dal'yth",
                "Bork'an",
                "Farisght Enclaves",
                "<Custom>"
            };
		}

        public override List<string> GetWarlordTraits(string keyword)
		{
			List<string> traits = new List<string>();

            if (keyword == "T'au")
            {
                traits.AddRange(new string[] {
                    "Precision of the Hunter",
                    "Through Unity, Devastation",
                    "A Ghost Walks Among Us",
                    "Through Boldness, Victory",
                    "Exemplar of the Kauyon",
                    "Exemplar of the Mont'ka"
                });

                if (currentSubFaction == "T'au")
                {
                    traits.Add("Strength of Conviction");
                }
                else if (currentSubFaction == "Vior'la")
                {
                    traits.Add("Academy Luminary");
                }
                else if (currentSubFaction == "Sa'cea")
                {
                    traits.Add("Strategic Conqueror");
                }
                else if (currentSubFaction == "Dal'yth")
				{
                    traits.Add("Unifying Influence");
				}
				else if (currentSubFaction == "Bork'an")
				{
                    traits.Add("Seeker of Perfection");
				}
				else if (currentSubFaction == "Farsight Enclaves")
				{
                    traits.Add("Master of the Killing Blow");
				}
			}

			if (keyword == "Kroot")
			{
                traits.AddRange(new string[]
                {
                    "Master of the Hunt",
                    "Pack Leader",
                    "Nomadic Hunter"
                });
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

            string[] sectorA = new string[]
            {
                "Strike Swiftly",
                "Play Their Part",
                "Adherents to the Teachings",
                "Calm Under Pressure"
            };

            string[] sectorB = new string[]
            {
                "Camoflauge Experts",
                "Defensive Doctrines",
                "Blocking Tactics",
                "Fire Caste Marksmen"
            };

            string[] sectorC = new string[]
            {
                "Evasion Manoeuvres",
                "Pinpoint Targeting",
                "Disengagement Protocols",
                "Fire Saturation"
            };

            string[] sectorD = new string[]
            {
                "Reliable Weaponry",
                "Defenders of the Cause",
                "Reinforced Armour",
                "Hardened Warheads"
            };

            string[] sectorE = new string[]
            {
                "Turbo-Jets",
                "Loyal to the End",
                "Rapid Retreat",
                "Enriched Reactors"
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

                    if (sectorA.Contains(customSubFactionTraits[0]))
                    {
                        cmbSubCustom2.Items.AddRange(sectorB);
                        cmbSubCustom2.Items.AddRange(sectorC);
                        cmbSubCustom2.Items.AddRange(sectorD);
                        cmbSubCustom2.Items.AddRange(sectorE);
                    }
                    else if (sectorB.Contains(customSubFactionTraits[0]))
                    {
                        cmbSubCustom2.Items.AddRange(sectorA);
                        cmbSubCustom2.Items.AddRange(sectorC);
                    }
                    else if (sectorC.Contains(customSubFactionTraits[0]))
                    {
                        cmbSubCustom2.Items.AddRange(sectorA);
                        cmbSubCustom2.Items.AddRange(sectorB);
                    }
                    else if (sectorD.Contains(customSubFactionTraits[0]))
                    {
                        cmbSubCustom2.Items.AddRange(sectorA);
                        cmbSubCustom2.Items.AddRange(sectorE);
                    }
                    else if (sectorE.Contains(customSubFactionTraits[0]))
                    {
                        cmbSubCustom2.Items.AddRange(sectorA);
                        cmbSubCustom2.Items.AddRange(sectorD);
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
                "Strike Swiftly",
                "Play Their Part",
                "Adherents to the Teachings",
                "Calm Under Pressure",
                "Camoflauge Experts",
                "Defensive Doctrines",
                "Blocking Tactics",
                "Fire Caste Marksmen",
                "Evasion Manoeuvres",
                "Pinpoint Targeting",
                "Disengagement Protocols",
                "Fire Saturation",
                "Reliable Weaponry",
                "Defenders of the Cause",
                "Reinforced Armour",
                "Hardened Warheads",
                "Turbo-Jets",
                "Loyal to the End",
                "Rapid Retreat",
                "Enriched Reactors"
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
            return "T'au Empire";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
