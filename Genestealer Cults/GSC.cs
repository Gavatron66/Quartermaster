using Roster_Builder.Adeptus_Custodes;
using Roster_Builder.Necrons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Genestealer_Cults
{
    public class GSC : Faction
    {
        int customSubPoints;

        public GSC()
        {
            subFactionName = "<Cult>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Proficient Planning";
            customSubFactionTraits = new string[4];
            customSubPoints = 0;
            StratagemList.AddRange(new string[]
            {
                "Gene-sire's Gifts",
                "Leaders of the Cult",
                "Xenoform Bionics"
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
            return new List<Datasheets>()
            {
                //---------- HQ ----------
                new Patriarch(),
                new Primus(),
                new Magus(),
                new AcolyteIconward(),
                new JackalAlphus(),
                //---------- Troops ----------
                new AcolyteHybrids(),
                new NeophyteHybrids(),
                //---------- Elites ----------
                new PurestrainGenestealers(),
                new HybridMetamorphs(),
                new Aberrants(),
                new Abominant(),
                new Nexos(),
                new Clamavus(),
                new Locus(),
                new Kelermorph(),
                new Sanctus(),
                new ReductusSaboteur(),
                new Biophagus(),
                //---------- Fast Attack ----------
                new AtalanJackals(),
                new AchillesRidgerunners(),
                //---------- Heavy Support ----------
                new GoliathRockgrinder(),
                //---------- Transport ----------
                new GoliathTruck(),
            };
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            int points = 0;

            string[] twenty = new string[]
            {
                "Lying in Wait (+20 pts)",
                "Excavate (+20 pts)"
            };

            string[] fifteen = new string[]
            {
                "Exacting Planner (+15 pts)",
                "Alchemist Supreme (+15 pts)",
                "A Trap Sprung (+15 pts)",
                "A Perfect Ambush (+15 pts)",
                "Meditations in Shadow (+15 pts)",
                "Our Time is Nigh (+15 pts)"
            };

            string[] ten = new string[]
            {
                "From Every Angle (+10 pts)",
                "They Came From Below (+10 pts)"
            };

            if(twenty.Contains(upgrade))
            {
                points = 20;
            }

            if (fifteen.Contains(upgrade))
            {
                points = 15;
            }

            if (ten.Contains(upgrade))
            {
                points = 10;
            }

            return points;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            return new List<string>()
            {
                "(None)",
                "Lying in Wait (+20 pts)",
                "Exacting Planner (+15 pts)",
                "Alchemist Supreme (+15 pts)",
                "A Trap Sprung (+15 pts)",
                "A Perfect Ambush (+15 pts)",
                "From Every Angle (+10 pts)",
                "Meditations in Shadow (+15 pts)",
                "Excavate (+20 pts)",
                "They Came From Below (+10 pts)",
                "Our Time is Nigh (+15 pts)"
            };
        }

        public override bool GetIfEnabled(int index)
        {
            /*
            if (StratagemCount[index] < StratagemLimit[index])
            {
                return true;
            }

            /return false;
            */
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string>()
            {
                "Mass Hypnosis",
                "Mind Control",
                "Psionic Blast",
                "Mental Onslaught",
                "Psychic Stimulus",
                "Might From Beyond"
            };
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            if(keywords.Contains("PRIMUS") || keywords.Contains("LOCUS"))
            {
                relics.Add("Sword of the Void's Eye");
            }

            if (keywords.Contains("PRIMUS"))
            {
                relics.Add("Hand of Aberrance");
            }

            relics.Add("Amulet of the Voidwyrm");

            if (keywords.Contains("ACOLYTE ICONWARD") || keywords.Contains("JACKAL ALPHUS") || 
                keywords.Contains("MAGUS") || keywords.Contains("BIOPHAGUS") ||
                keywords.Contains("CLAMAVUS") || keywords.Contains("NEXOS") || 
                keywords.Contains("REDUCTUS SABOTEUR"))
            {
                relics.Add("Oppressor's Bane");
            }

            if (keywords.Contains("KELERMORPH"))
            {
                relics.Add("Wyrmtooth Rounds");
            }

            if (keywords.Contains("MAGUS") || keywords.Contains("SANCTUS"))
            {
                relics.Add("Dagger of Swift Sacrifice");
            }

            if (keywords.Contains("MAGUS") || keywords.Contains("PATRIARCH") ||
                keywords.Contains("ABOMINANT") || keywords.Contains("BIOPHAGUS") ||
                keywords.Contains("SANCTUS"))
            {
                relics.Add("The Crouchling");
            }

            if (keywords.Contains("JACKAL ALPHUS") || keywords.Contains("SANCTUS"))
            {
                relics.Add("The Gift From Beyond");
            }

            if (keywords.Contains("MAGUS") || keywords.Contains("PATRIARCH"))
            {
                relics.Add("The Unwilling Orb");
            }

            if (keywords.Contains("NEXOS"))
            {
                relics.Add("Cranial Inlay");
            }

            if (keywords.Contains("CLAMAVUS"))
            {
                relics.Add("The Voice of the Liberator");
            }

            if ((keywords.Contains("PRIMUS") || keywords.Contains("LOCUS") 
                && currentSubFaction == "Cult of the Four-armed Emperor"))
            {
                relics.Add("Sword of the Four-armed Emperor");
            }

            if (currentSubFaction == "Hivecult")
            {
                relics.Add("Vockor's Talisman");
            }

            if (currentSubFaction == "Bladed Cog")
            {
                relics.Add("Mark of the Clawed Omnissiah");
            }

            if (currentSubFaction == "Rusted Claw")
            {
                relics.Add("The Nomad's Mantle");
            }

            if (currentSubFaction == "Pauper Princes")
            {
                relics.Add("Reliquary of Saint Tenndarc");
            }

            if (currentSubFaction == "Twisted Helix")
            {
                relics.Add("Elixir of the Prime Specimen");
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Cult of the Four-armed Emperor",
                "The Hivecult",
                "The Bladed Cog",
                "The Rusted Claw",
                "The Pauper Princes",
                "The Twisted Helix",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>()
            {
                "Focus of Adoration",
                "Shadow Stalker",
                "Biomorph Adapation",
                "Prowling Agitant",
                "Alien Majesty",
                "Preternatural Speed"
            };

            if (currentSubFaction == "Cult of the Four-armed Emperor") { traits.Add("Inscrutable Cunning"); }
            else if (currentSubFaction == "The Hivecult") { traits.Add("Hivelord"); }
            else if (currentSubFaction == "The Bladed Cog") { traits.Add("Single-minded Obsession"); }
            else if (currentSubFaction == "The Rusted Claw") { traits.Add("Entropic Touch"); }
            else if (currentSubFaction == "The Pauper Princes") { traits.Add("Xenoprophet"); }
            else if (currentSubFaction == "The Twisted Helix") { traits.Add("Bio-alchemist"); }

            return traits;
        }

        public override void SaveSubFaction(int code, Panel panel)
        {
            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            CheckedListBox clbSubCustom = panel.Controls["clbSubCustom"] as CheckedListBox;

            switch (code)
            {
                case 50:
                    currentSubFaction = cmbSubFaction.SelectedItem.ToString();
                    if (currentSubFaction == "<Custom>")
                    {
                        panel.Controls["lblSubCustomCLB"].Visible = true;
                        clbSubCustom.Visible = true;
                    }
                    else
                    {
                        panel.Controls["lblSubCustomCLB"].Visible = false;
                        clbSubCustom.Visible = false;
                    }
                    break;
                case 57:

                    customSubPoints = 0;
                    foreach (string item in clbSubCustom.CheckedItems)
                    {
                        if(item.Contains("(1)")) {
                            customSubPoints += 1;
                        }
                        else if (item.Contains("(2)"))
                        {
                            customSubPoints += 2;
                        }
                        else if (item.Contains("(3)"))
                        {
                            customSubPoints += 3;
                        }
                    }

                    if(customSubPoints == 4)
                    {
                        customSubFactionTraits = new string[clbSubCustom.CheckedItems.Count];
                        for(int i = 0; i < clbSubCustom.CheckedItems.Count; i++)
                        {
                            customSubFactionTraits[i] = clbSubCustom.CheckedItems[i].ToString();
                        }
                    }
                    else if (customSubPoints > 4)
                    {
                        clbSubCustom.SetItemChecked(clbSubCustom.SelectedIndex, false);
                    }
                    break;
            }
        }

        public override void SetPoints(int points)
        {
            StratagemCount = new int[] { 0, 0, 0 };
            StratagemLimit = new int[] { points / 1000, 1, 1 };

            if (points < 1000)
            {
                StratagemLimit[0] = 1;
                StratagemLimit[1] = 1;
            }
        }

        public override void SetSubFactionPanel(Panel panel)
        {
            Template template = new Template();
            template.LoadFactionTemplate(1, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;
            CheckedListBox clbSubCustom = panel.Controls["clbSubCustom"] as CheckedListBox;

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);
            panel.Controls["lblSubCustomCLB"].Text = "May take up to 4 points of the following:";

            if (currentSubFaction != "<Custom>")
            {
                panel.Controls["lblSubCustomCLB"].Visible = false;
                clbSubCustom.Visible = false;
            }
            else
            {
            panel.Controls["lblSubCustomCLB"].Visible = true;
                clbSubCustom.Visible = true;
            }


            clbSubCustom.Items.Clear();
            clbSubCustom.Items.AddRange(new string[]
            {
                "(3) Hunter's Instincts",
                "(3) Impassioned",
                "(3) Symbiotic Broodmind",
                "(2) Deep Supplies",
                "(2) Agile Guerrillas",
                "(2) Thralls of the Patriarch",
                "(2) Toxin Agents",
                "(2) Martial",
                "(1) Accustomed to Toil",
                "(1) Industrial Affinity",
                "(1) Alien Fury",
                "(1) War Convoy",
                "(1) Synaptic Resonance",
                "(1) Cold-Eyed Killers"
            });

            for(int i = 0; i < clbSubCustom.Items.Count; i++)
            {
                if (customSubFactionTraits.Contains(clbSubCustom.Items[i]))
                {
                    clbSubCustom.SetItemChecked(i, true);
                }
            }
        }

        public override string ToString()
        {
            return "Genestealer Cults";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
