using Roster_Builder.Aeldari.Harlequins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adeptus_Mechanicus
{
    public class AdMech : Faction
    {
        public AdMech()
        {
            subFactionName = "<Forge World>";
            currentSubFaction = string.Empty;
            factionUpgradeName = "Holy Orders";
            customSubFactionTraits = new string[2];
            StratagemList.AddRange(new string[]
            {
                "Stratagem: Mechanicus Locum",
                "Stratagem: Archeotech Specialists",
                "Stratagem: Indentured Machines",
                "Stratagem: Host of the Intermediary",
                "Stratagem: Artefactorum"
            });
        }

        public override List<string> GetCustomSubfactionList1()
        {
            return new List<string>
            {
                "Rad-Saturated Forge World",
                "Expansionist Forge World",
                "Data-hoard Forge World",
                "Reignited Forge World"
            };
        }

        public override List<string> GetCustomSubfactionList2()
        {
            string[] rad = new string[]
            {
                "Luminary Suffusion",
                "Scarifying Weaponry",
                "Machine God's Chosen"
            };

            string[] expansionist = new string[]
            {
                "Forward Operators",
                "Acquisitive Reach",
                "Rugged Explorators"
            };

            string[] datahoard = new string[]
            {
                "Omnitrac Impellors",
                "Autosavant Spirits",
                "Servo-focused Auguries"
            };

            string[] reignited = new string[]
            {
                "Data-bleed Generators",
                "Purified Datasphere",
                "Engineered Nanophages"
            };

            return new List<string>
            {
                "Rad-Saturated Forge World",
                "Expansionist Forge World",
                "Data-hoard Forge World",
                "Reignited Forge World"
            };
        }

        public override List<Datasheets> GetDatasheets()
        {
            var datasheets = new List<Datasheets>()
            {
                //---------- HQ ----------
                new BelisariusCawl(),
                new TechPriestManipulus(),
                new TechPriestDominus(),
                new Technoarcheologist(),
                new SkitariiMarshal(),
                new TechPriestEnginseer(),
                //---------- Troops ----------
                new SkitariiRangers(),
                new SkitariiVanguard(),
                new KataphronBreachers(),
                new KataphronDestroyers(),
                //---------- Elites ----------
                new AMServitors(),
                new FulguriteElectroPriests(),
                new CorpuscariiElectroPriests(),
                new CyberneticaDatasmith(),
                new SicarianInfiltrators(),
                new SicarianRuststalkers(),
                //---------- Fast Attack ----------
                new SerberysRaiders(),
                new SerberysSulphurhounds(),
                new PteraxiiSterylizors(),
                new PteraxiiSkystalkers(),
                new IronstriderBallistarii(),
                new SydonianDragoons(),
                //---------- Heavy Support ----------
                new KastelanRobots(),
                new SkorpiusDisintegrator(),
                new OnagerDunecrawler(),
                //---------- Transport ----------
                new SkorpiusDunerider(),
                //---------- Flyers ----------
                new ArchaeopterTransvector(),
                new ArchaeopterStratoraptor(),
                new ArchaeopterFusilave()
            };

            return datasheets;
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            if(upgrade == "Genetors (+25 pts)")
            {
                return 25;
            }
            else if (upgrade == "Logi (+40 pts)")
            {
                return 40;
            }
            else if (upgrade == "Magi (+30 pts)")
            {
                return 30;
            }
            else if (upgrade == "Artisans (35 pts)")
            {
                return 35;
            }

            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            List<string> upgrades = new List<string>()
            {
                "(None)",
                "Genetors (+25 pts)",
                "Logi (+40 pts)",
                "Magi (+30 pts)",
                "Artisans (35 pts)"
            };

            return upgrades;
        }

        public override bool GetIfEnabled(int index)
        {
            return true;
        }

        public override List<string> GetPsykerPowers(string keywords)
        {
            return new List<string>();
        }

        public override List<string> GetRelics(List<string> keywords)
        {
            List<string> relics = new List<string>();

            relics.Add("(None)");

            if(keywords.Contains("CYBERNETICA DATASMITH"))
            {
                relics.Add("The Uncreator Gauntlet");
            }

            relics.Add("Raiment of the Technomartyr");
            relics.Add("The Skull of Elder Nikola");

            if(keywords.Contains("SKITARII MARSHAL"))
            {
                relics.Add("The Purgation's Purity");
                relics.Add("Exemplar's Eternity");
            }

            if(keywords.Contains("TECH-PRIEST DOMINUS"))
            {
                relics.Add("Phosphoenix");
            }

            if (keywords.Contains("TECH-PRIEST DOMINUS") || keywords.Contains("TECH-PRIEST ENGINSEER"))
            {
                relics.Add("Pater Cog-tooth");
            }

            relics.Add("Anzion's Pseudogenetor");
            relics.Add("The Omniscient Mask");

            if (keywords.Contains("TECH-PRIEST MANIPULUS"))
            {
                relics.Add("Sonic Reaper");
            }

            relics.Add("Temporcopia");
            relics.Add("The Cage of Varadimas");

            if (currentSubFaction == "Mars" && keywords.Contains("TECH-PRIEST DOMINUS"))
            {
                relics.Add("The Red Axe");
            }
            if (currentSubFaction == "Lucius")
            {
                relics.Add("The Solar Flare");
            }
            if (currentSubFaction == "Agripinaa")
            {
                relics.Add("The Eye of Xi-Lexum");
            }
            if (currentSubFaction == "Graia")
            {
                relics.Add("The Cerebral Techno-mitre");
            }
            if (currentSubFaction == "Stygies VIII")
            {
                relics.Add("The Omnissiah's Hand");
            }
            if (currentSubFaction == "Ryza" && keywords.Contains("TECH-PRIEST DOMINUS"))
            {
                relics.Add("Weapon XCIX");
            }
            if (currentSubFaction == "Metalica")
            {
                relics.Add("The Adamantine Arm");
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                string.Empty,
                "Mars",
                "Lucius",
                "Agripinaa",
                "Graia",
                "Stygies VIII",
                "Ryza",
                "Metalica",
                "<Custom>"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            List<string> traits = new List<string>();

            if(keyword.Contains("Priest")) 
            {
                traits.AddRange(new string[]
                {
                    "Emotionless Clarity",
                    "Masterwork Bionics",
                    "First-hand Field Testing",
                    "Necromechanic",
                    "Cartogrammatist",
                    "Supervisory Radiance"
                });
            }

            if(keyword.Contains("Skitarii"))
            {
                traits.AddRange(new string[]
                {
                    "Multitasking Cortex",
                    "Battle-sphere Uplink",
                    "Programmed Retreat",
                    "Archived Engagements",
                    "Firepoint Telemetry Cache",
                    "Eyes of the Omnissiah"
                });
            }

            if(currentSubFaction == "Mars")
            {
                traits.Add("Panegyric Procession");
            }
            if (currentSubFaction == "Lucius")
            {
                traits.Add("Luminescent Blessing");
            }
            if (currentSubFaction == "Agripinaa")
            {
                traits.Add("Verse of Vengeance");
            }
            if (currentSubFaction == "Graia")
            {
                traits.Add("Mantra of Discipline");
            }
            if (currentSubFaction == "Stygies VIII")
            {
                traits.Add("Veiled Hunter");
            }
            if (currentSubFaction == "Ryza")
            {
                traits.Add("Citation in Savagery");
            }
            if (currentSubFaction == "Metalica")
            {
                traits.Add("Tribute of Emphatic Veneration");
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

            string[] rad = new string[]
            {
                "Luminary Suffusion",
                "Scarifying Weaponry",
                "Machine God's Chosen"
            };

            string[] expansionist = new string[]
            {
                "Forward Operators",
                "Acquisitive Reach",
                "Rugged Explorators"
            };

            string[] datahoard = new string[]
            {
                "Omnitrac Impellors",
                "Autosavant Spirits",
                "Servo-focused Auguries"
            };

            string[] reignited = new string[]
            {
                "Data-bleed Generators",
                "Purified Datasphere",
                "Engineered Nanophages"
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

                    switch(cmbSubCustom1.SelectedIndex)
                    {
                        case 0:
                            cmbSubCustom2.Items.AddRange(rad);
                            break;
                        case 1:
                            cmbSubCustom2.Items.AddRange(expansionist);
                            break;
                        case 2:
                            cmbSubCustom2.Items.AddRange(datahoard);
                            break;
                        case 3:
                            cmbSubCustom2.Items.AddRange(reignited);
                            break;
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
                "Rad-Saturated Forge World",
                "Expansionist Forge World",
                "Data-hoard Forge World",
                "Reignited Forge World"
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
            return "Adeptus Mechanicus";
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }
    }
}
