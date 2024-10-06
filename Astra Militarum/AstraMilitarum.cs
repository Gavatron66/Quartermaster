using Roster_Builder.Drukhari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class AstraMilitarum : Faction
    {
        public AstraMilitarum()
        {
            subFactionName = "Regimental Doctrine";
            currentSubFaction = "Born Soldiers";
            factionUpgradeName = "Tank Aces";
            customSubFactionTraits = new string[2];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Officer Cadre",
                "Stratagem: Imperial Commanders Armoury",
                "Stratagem: Battlefield Bequest",
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>
            {
                "Mechanised Infantry",
                "Parade Drill",
                "Armoured Superiority",
                "Blitz Division",
                "Expert Bobardiers",
                "Heirloom Weapon",
                "Recon Operators",
                "Trophy Hunters",
                "Grim Demeanour",
                "Brutal Strength",
                "Veteran Guerrillas",
                "Elite Sharpshooters",
                "Cult of Sacrifice",
                "Industrial Efficiency",
                "Swift as the Wind"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>
            {
                "Mechanised Infantry",
                "Parade Drill",
                "Armoured Superiority",
                "Blitz Division",
                "Expert Bobardiers",
                "Heirloom Weapon",
                "Recon Operators",
                "Trophy Hunters",
                "Grim Demeanour",
                "Brutal Strength",
                "Veteran Guerrillas",
                "Elite Sharpshooters",
                "Cult of Sacrifice",
                "Industrial Efficiency",
                "Swift as the Wind"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
				//---------- HQ ----------
                new LordSolarLeontus(),
                new UrsulaCreed(),
                new CadianCastellan(),
                new TankCommander(),
                new PlatoonCommandSquad(),
                new CadianCommandSquad(),
                new TempestusCommandSquad(),
                new IronHandStraken(),
                new PrimarisPsyker(),
                new GauntsGhosts(),
                new MasterOfOrdnance(),
                new OfficerOfTheFleet(),
                new Astropath(),
                new OgrynBodyguard(),
                new NorkDeddog(),
				//---------- Troops ----------
                new InfantrySquad(),
                new CadianShockTroops(),
                new DeathKorpsOfKrieg(),
                new CatachanJungleFighters(),
				//---------- Elites ----------
                new TempestusScions(),
                new SlyMarbo(),
                new Kasrkin(),
                new RegimentalPreacher(),
                new SergeantHarker(),
                new RegimentalEnginseer(),
                new MunitorumServitors(),
                new Commissar(),
                new Ogryns(),
                new Bullgryns(),
                new Ratlings(),
				//---------- Fast Attack ----------
                new AttilanRoughRiders(),
                new ScoutSentinels(),
                new ArmouredSentinels(),
                new Hellhound(),
				//---------- Heavy Support ----------
                new RogalDorn(),
                new HeavyWeaponsSquad(),
                new FieldOrdnanceBattery(),
                new LemanRuss(),
                new Basilisk(),
                new Hydra(),
                new Manticore(),
                new Wyvern(),
                new Deathstrike(),
				//---------- Transport ----------
                new Chimera(),
                new Taurox(),
                new TauroxPrime(),
				//---------- Flyer ----------
                new Valkyrie(),
                //---------- Fortification ----------
                new AegisDefenceLine(),
                //---------- Lord of War ----------
                new Baneblade(),
                new Banehammer(),
                new Banesword(),
                new Doomhammer(),
                new Hellhammer(),
                new Shadowsword(),
                new Stormlord(),
                new Stormsword(),
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == null)
            {
                return 0;
            }

            if(upgrade.Contains("+15"))
            {
                return 15;
            }
            else if (upgrade.Contains("+20"))
            {
                return 20;
            }
            else if (upgrade.Contains("+25"))
            {
                return 25;
            }
            else if (upgrade.Contains("+30"))
            {
                return 30;
            }
            else if (upgrade.Contains("+35"))
            {
                return 35;
            }
            else if (upgrade.Contains("+40"))
            {
                return 40;
            }
            else
            {
                return 0;
            }
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            if(keywords.Contains("BATTLE TANK"))
            {
                return new List<string>
                {
                    "(None)",
                    "Vaunted Praetorian (+15 pts)",
                    "Meticulous Calibrator (+20 pts)",
                    "Mechanical Pack Rat (+20 pts)",
                    "Veteran Commandeer (+20 pts)",
                    "Knight of Piety (+25 pts)",
                    "Master of Camouflage (+25 pts)",
                    "Steel Commissar (+25 pts)"
                };
            }
            else if (keywords.Contains("SUPER-HEAVY"))
            {
                return new List<string>
                {
                    "(None)",
                    "Mechanical Pack Rat (+30 pts)",
                    "Vaunted Praetorian (+30 pts)",
                    "Veteran Commandeer (+30 pts)",
                    "Knight of Piety (+35 pts)",
                    "Master of Camouflage (+35 pts)",
                    "Meticulous Calibrator (+40 pts)",
                };
            }
            else
            {
                return new List<string> { };
            }
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string> 
            {
                "Terrifying Visions",
                "Gaze of the Emperor",
                "Psychic Barrier",
                "Nightshroud",
                "Mental Shackles",
                "Psychic Maelstrom"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");
            relics.Add("The Emperor's Benediction"); //Commissar w/ Bolt Pistol
            relics.Add("Tactical Auto-reliquary of Tyberius"); //Officer
            relics.Add("Death Mask of Ollanius"); //Infantry
            relics.Add("The Barbicant's Key"); //Infantry
            relics.Add("Kurov's Aquila");
            relics.Add("Gatekeeper"); //Tank Commander w/ LR Battle Cannon
            relics.Add("Relic of Lost Cadia"); //Cadian
            relics.Add("Order of the Bastium Stellaris"); //Infantry
            relics.Add("Legacy of Kalladius"); //Model w/ Chainsword
            relics.Add("Psy-sigil of Sanction"); //Psyker
            relics.Add("Armour of Graf Toschenko");
            relics.Add("Laurels of Command"); //Officer
            relics.Add("Claw of the Desert Tigers"); //Power Sword/Sabre
            relics.Add("Clarion Proclamatus"); //Command Squad w/ Master Vox
            relics.Add("Finial of the Nemrodesh 1st"); //Command Squad w/ Regimental Std
            relics.Add("Null Coat"); //Tempestor Prime or Commissar
            relics.Add("The Emperor's Fury"); //Plasma Pistol
            relics.Add("Refractor Field Generator"); //Tempestor Prime

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "Born Soldiers",
                "Custom Doctrine"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            if(keyword == "Tempestus")
            {
                return new List<string>() 
                {
                    "Drill Commander",
                    "Precision Targeting",
                    "Uncompromising Prosecution"
                };
            }
            else
            {
                return new List<string>()
                {
                    "Front-line Combatant",
                    "Master Tactician",
                    "Grand Strategist",
                    "Superior Tactical Training",
                    "Old Grudges",
                    "Lead by Example"
                };
            }
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
                    if (currentSubFaction == "Custom Doctrine")
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

            if (currentSubFaction != "Custom Doctrine")
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
                "Mechanised Infantry",
                "Parade Drill",
                "Armoured Superiority",
                "Blitz Division",
                "Expert Bobardiers",
                "Heirloom Weapon",
                "Recon Operators",
                "Trophy Hunters",
                "Grim Demeanour",
                "Brutal Strength",
                "Veteran Guerrillas",
                "Elite Sharpshooters",
                "Cult of Sacrifice",
                "Industrial Efficiency",
                "Swift as the Wind"
            });

            cmbSubCustom2.Items.AddRange(new string[]
            {
                "Mechanised Infantry",
                "Parade Drill",
                "Armoured Superiority",
                "Blitz Division",
                "Expert Bobardiers",
                "Heirloom Weapon",
                "Recon Operators",
                "Trophy Hunters",
                "Grim Demeanour",
                "Brutal Strength",
                "Veteran Guerrillas",
                "Elite Sharpshooters",
                "Cult of Sacrifice",
                "Industrial Efficiency",
                "Swift as the Wind"
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
            return "Astra Militarum";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
