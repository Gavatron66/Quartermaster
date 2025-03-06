using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class DeathGuard : Faction
    {
        //Detachment Ability Variables
        int coreInfantry = 0;
        int plagueFollower = 0;
        int poxwalkers = 0;
        bool mortarion = false;

        public DeathGuard()
        {
            subFactionName = "<Plague Company>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Deadly Pathogen";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Plague-Chosen", //Warlord Stratagem
                "Stratagem: Gifts of Decay", //Relic Stratagem
                "Stratagem: Champion of Disease", //Stratagem Code 1
                "Stratagem: Grandfatherly Influence", //Stratagem Code 2
                "Stratagem: Sevenfold Blessings (The Wretched only)" // The Wretched Unique Stratagem
            });
            restrictedItems.AddRange(new string[]
            {
                "Plague Skull of Glothila",
                "Explosive Outbreak (+15 pts)",
                "Rotten Constitution"
            });
            restrictedDatasheets.AddRange(new int[]
            {
                9, 10
            });
            StratagemCount = new int[] { 0, 0, 0, 0, 0 };
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

        public override List<Datasheets> GetDatasheets()
        {
            return new List<Datasheets>() 
            { 
                //---------- HQ ----------
                new DG_DaemonPrince(),
                new Typhus(),
                new DG_ChaosLord(),
                new DG_TerminatorChaosLord(),
                new LordOfVirulence(),
                new LordOfContagion(),
                new DG_TerminatorSorcerer(),
                new MalignantPlaguecaster(),
                //---------- Troops ----------
                new PlagueMarines(),
                new DG_Cultists(),
                new Poxwalkers(),
                //---------- Elite ----------
                new NoxiousBlightbringer(),
                new FoulBlightspawn(),
                new BiologusPutrifier(),
                new Tallyman(),
                new PlagueSurgeon(),
                new BlightlordTerminators(),
                new DeathshroudTerminators(),
                new DG_Helbrute(),
                new DG_Possessed(), 
                //---------- Fast Attack ----------
                new DG_ChaosSpawn(),
                new MyphiticBlightHauler(),
                new FoetidBloatDrone(),
                //---------- Heavy Support ----------
                new PlagueburstCrawler(),
                new DG_ChaosLandRaider(),
                new DG_ChaosPredatorAnnihilator(),
                new DG_ChaosPredatorDestructor(),
                new DG_Defiler(), 
                //---------- Transport ----------
                new DG_ChaosRhino(),
                //---------- Lord of War ----------
                new Mortarion(), 
                //---------- Fortification ----------
                new MiasmicMalignifier(),
            };
        }

        public override bool GetIfEnabled(int code)
        {
            switch(code)
            {
                case 80:
                    return !hasWarlord;
                case 81:
                    return !hasRelic;
                case 82:
                    return StratagemCount[0] == StratagemLimit[0];
                case 83:
                    return StratagemCount[1] == StratagemLimit[1];
                case 84:
                    return StratagemCount[2] == StratagemLimit[2];
                case 85:
                    return StratagemCount[3] == StratagemLimit[3];
                case 86:
                    return StratagemCount[4] == StratagemLimit[4];
            }

            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string>()
            {
                "Miasma of Pestilence",
                "Gift of Contagion",
                "Plague Wind",
                "Putrescent Vitality",
                "Curse of the Leper",
                "Gift of Plagues"
            };
        }

        public override List<String> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>()
            {
                "Revoltingly Resilient",
                "Living Plague",
                "Hulking Physique",
                "Arch-Contaminator",
                "Rotten Constitution",
                "Foul Effluents"
            };

            if (currentSubFaction != string.Empty)
            {
                if (currentSubFaction == "Harbingers") { traits.Add("Shamblerot (Contagion)"); }
                else if (currentSubFaction == "The Inexorable") { traits.Add("Ferric Blight (Contagion)"); }
                else if (currentSubFaction == "Mortarion's Anvil") { traits.Add("Gloaming Bloat (Contagion)"); }
                else if (currentSubFaction == "The Wretched") { traits.Add("Eater Plague (Contagion)"); }
                else if (currentSubFaction == "The Poxmongers") { traits.Add("Sanguous Flux (Contagion)"); }
                else if (currentSubFaction == "The Ferrymen") { traits.Add("The Droning (Contagion)"); }
                else if (currentSubFaction == "Mortarion's Chosen Sons") { traits.Add("Nurgle's Fruit (Contagion)"); }
            }

            return traits;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            return new List<string>()
            {
                "(None)",
                "Acidic Malady (+15 pts)",
                "Explosive Outbreak (+15 pts)",
                "Virulent Fever (+15 pts)",
                "Befouling Runoff (+5 pts)",
                "Unstable Sickness (+10 pts)",
                "Corrosive Filth (+15 pts)",
                "Viscous Death (+5 pts)"
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            string[] fifteen = new string[]
            {
                "Acidic Malady (+15 pts)", "Explosive Outbreak (+15 pts)", "Virulent Fever (+15 pts)", "Corrosive Filth (+15 pts)"
            };

            string[] five = new string[]
            {
                "Befouling Runoff (+5 pts)", "Viscous Death (+5 pts)"
            };

            if (fifteen.Contains(upgrade))
            {
                return 15;
            }
            else if (five.Contains(upgrade))
            {
                return 5;
            }
            else if (upgrade == "Unstable Sickness (+10 pts)")
            {
                return 10;
            }
            else
            {
                return 0;
            }
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "",
                "Harbingers",
                "The Inexorable",
                "Mortarion's Anvil",
                "The Wretched",
                "The Poxmongers",
                "The Ferrymen",
                "Mortarion's Chosen Sons"
            };
        }

        public override List<string> GetRelics(List<string> Keywords)
        {
            List<string> relicsList = new List<string>();

            relicsList.Add("(None)");

            if(Keywords.Contains("CORE")) //For Stratagem: Champion of Disease
            {
                relicsList.AddRange(new string[]
                {
                    "Reaper of Glorious Entropy",
                    "Plague Skull of Glothila",
                    "Plaguebringer",
                    "Suppurating Plate"
                });

                return relicsList;
            }

            if(Keywords.Contains("LORD OF CONTAGION"))
            {
                relicsList.Add("Reaper of Glorious Entropy");
            }

            relicsList.Add("Plague Skull of Glothila");

            if(Keywords.Contains("NOXIOUS BLIGHTBRINGER"))
            {
                relicsList.Add("Daemon's Toll");
            }

            relicsList.Add("Fugaris' Helm");

            if(Keywords.Contains("SORCERER") || Keywords.Contains("MALIGNANT PLAGUECASTER"))
            {
                relicsList.Add("Putrid Periapt");
            }

            if (Keywords.Contains("TALLYMAN"))
            {
                relicsList.Add("Tollkeeper");
            }

            if (Keywords.Contains("FOUL BLIGHTSPAWN"))
            {
                relicsList.Add("Revolting Stench-vats");
            }

            relicsList.Add("Suppurating Plate");

            if ((Keywords.Contains("CHAOS LORD") && !Keywords.Contains("TERMINATOR")) || Keywords.Contains("BIOLOGUS PUTRIFIER")
                || Keywords.Contains("PLAGUE SURGEON"))
            {
                relicsList.Add("Plaguebringer");
            }

            if (currentSubFaction != string.Empty)
            {
                if (currentSubFaction == "Harbingers") { relicsList.Add("Infected Remains"); }
                else if (currentSubFaction == "The Inexorable") { relicsList.Add("Leechspore Casket"); }
                else if (currentSubFaction == "Mortarion's Anvil") { relicsList.Add("Warp Insect Hive"); }
                else if (currentSubFaction == "The Wretched")
                { 
                    if(Keywords.Contains("MALIGNANT PLAGUECASTER"))
                    {
                        relicsList.Add("The Daemon's Favour");
                    }
                }
                else if (currentSubFaction == "The Poxmongers") { relicsList.Add("Ironclot Furnace"); }
                else if (currentSubFaction == "The Ferrymen")
                {
                    if (Keywords.Contains("LORD OF CONTAGION"))
                    {
                        relicsList.Add("Ferryman's Scythe");
                    }
                }
                else if (currentSubFaction == "Mortarion's Chosen Sons")
                {
                    if (Keywords.Contains("FOUL BLIGHTSPAWN"))
                    {
                        relicsList.Add("Vomitryx");
                    }
                }
            }

            return relicsList;
        }

        public override string ToString()
        {
            return "Death Guard";
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>();
        }

        public override List<string> GetCustomSubfactionList2()
        {
            return new List<string>();
        }

        public override void SetPoints(int points)
        {
            StratagemCount = new int[] { 0, 0, 0, 0, 0 };
            StratagemLimit = new int[] { points / 1000, points / 1000, points / 1000, 3, 1 };

            if (points < 1000)
            {
                StratagemLimit[0] = 1;
                StratagemLimit[1] = 1;
                StratagemLimit[2] = 1;
            }
        }

        public override void SetSubFactionPanel(Panel panel)
        {
            Template template = new Template();
            template.LoadFactionTemplate(1, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
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

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {
            if (datasheet != null)
            {
                if (code)
                {
                    if (datasheet is Mortarion)
                    {
                        for (int i = 0; i < roster.Count; i++)
                        {
                            Datasheets unit = roster[i] as Datasheets;

                            if (unit.WarlordTrait == "Revoltingly Resilient" ||
                                unit.WarlordTrait == "Living Plague" ||
                                unit.WarlordTrait == "Arch-Contaminator")
                            {
                                restrictedItems.Remove(unit.WarlordTrait);
                                unit.WarlordTrait = string.Empty;
                            }

                            if (unit.isWarlord)
                            {
                                unit.isWarlord = false;
                                unit.WarlordTrait = string.Empty;
                            }
                        }

                        restrictedItems.Add("Revoltingly Resilient");
                        restrictedItems.Add("Living Plague");
                        restrictedItems.Add("Arch-Contaminator");

                        restrictedDatasheets.Add(29);
                        hasWarlord = true;
                        mortarion = true;
                    }
                    if(datasheet is Typhus)
                    {
                        restrictedDatasheets.Add(1);
                    }
                    if (datasheet is DG_DaemonPrince)
                    {
                        restrictedDatasheets.Add(0);
                    }
                    if(datasheet.Keywords.Contains("LORD OF THE DEATH GUARD"))
                    {
                        restrictedDatasheets.Add(1);
                        restrictedDatasheets.Add(2);
                        restrictedDatasheets.Add(3);
                        restrictedDatasheets.Add(4);
                        restrictedDatasheets.Add(5);
                    }
                    if (datasheet.Keywords.Contains("CORE") && datasheet.Keywords.Contains("INFANTRY"))
                    {
                        coreInfantry++;
                    }
                    if (datasheet.Keywords.Contains("PLAGUE FOLLOWERS"))
                    {
                        plagueFollower++;

                        if (plagueFollower == coreInfantry)
                        {
                            restrictedDatasheets.Add(9);
                        }
                    }
                    if (datasheet.Keywords.Contains("POXWALKERS"))
                    {
                        poxwalkers++;

                        if (poxwalkers == coreInfantry)
                        {
                            restrictedDatasheets.Add(10);
                        }
                    }
                }
                else
                {
                    if (datasheet is Mortarion)
                    {
                        restrictedItems.Remove("Revoltingly Resilient");
                        restrictedItems.Remove("Living Plague");
                        restrictedItems.Remove("Arch-Contaminator");
                        restrictedDatasheets.Remove(29);
                        mortarion = false;
                    }
                    if (datasheet is Typhus)
                    {
                        restrictedDatasheets.Remove(1);
                    }
                    if (datasheet is DG_DaemonPrince)
                    {
                        restrictedDatasheets.Remove(0);
                    }
                    if (datasheet.Keywords.Contains("LORD OF THE DEATH GUARD"))
                    {
                        restrictedDatasheets.Remove(1);
                        restrictedDatasheets.Remove(2);
                        restrictedDatasheets.Remove(3);
                        restrictedDatasheets.Remove(4);
                        restrictedDatasheets.Remove(5);
                    }
                    if (datasheet.Keywords.Contains("CORE") && datasheet.Keywords.Contains("INFANTRY"))
                    {
                        coreInfantry--;
                    }
                    if (datasheet.Keywords.Contains("PLAGUE FOLLOWERS"))
                    {
                        plagueFollower--;

                    }
                    if (datasheet.Keywords.Contains("POXWALKERS"))
                    {
                        poxwalkers--;

                    }
                    if (datasheet.hasFreeRelic)
                    {
                        this.hasRelic = false;
                    }
                    if (datasheet.isWarlord)
                    {
                        this.hasWarlord = false;
                    }
                }
            }

            if(mortarion)
            {
                hasWarlord = true;
            }

            if (plagueFollower < coreInfantry)
            {
                restrictedDatasheets.Remove(9);
            }
            else if (plagueFollower >= coreInfantry)
            {
                if(plagueFollower != coreInfantry)
                {
                    roster.RemoveAt(roster.FindIndex(d => d.ToString().Contains("Cultists")));
                    plagueFollower--;
                }

                if(!restrictedDatasheets.Contains(9))
                {
                    restrictedDatasheets.Add(9);
                }
            }

            if (poxwalkers < coreInfantry)
            {
                restrictedDatasheets.Remove(10);
            }
            else if (poxwalkers >= coreInfantry)
            {
                if(poxwalkers != coreInfantry)
                {
                    roster.RemoveAt(roster.FindIndex(d => d.ToString().Contains("Poxwalkers")));
                    poxwalkers--;
                }

                if (!restrictedDatasheets.Contains(10))
                {
                    restrictedDatasheets.Add(10);
                }
            }
        }
    }
}
