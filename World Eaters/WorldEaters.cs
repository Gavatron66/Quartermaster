using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.World_Eaters
{
    public class WorldEaters : Faction
    {
        public WorldEaters()
        {
            subFactionName = "<Subfaction>";
            currentSubFaction = "World Eaters";
            factionUpgradeName = "Daemonic Infusions";
            StratagemList.AddRange(new string[]
            {
                "Stratagem: None",
                "Stratagem: YOU SHOULDN'T SEE THIS"
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
            if (currentSubFaction == "Disciples of the Red Angel")
            {
                var datasheets = new List<Datasheets>()
                {
                    new Angron(),
                    new LordInvocatus(),
                    new WE_DaemonPrince(),
                    new LordOnJuggernaut(),
                    new Eightbound(),
                    new ExaltedEightbound(),
                    new WE_ChaosLandRaider(),
                    new WE_Defiler(),
                    new WE_Forgefiend(),
                    new WE_Maulerfiend(),
                    new WE_Heldrake(),
                    new LordOfSkulls()
                };

                return datasheets;
            }
            else
            {
                var datasheets = new List<Datasheets>()
                {
                    new Angron(),
                    //---------- HQ ----------
                    new Kharn(),
                    new LordInvocatus(),
                    new WE_DaemonPrince(),
                    new LordOnJuggernaut(),
                    new WE_MasterOfExecutions(),
                    //---------- Troops ----------
                    new KhorneBerzerkers(),
                    new Jakhals(),
                    //---------- Elites ----------
                    new WE_Terminators(),
                    new Eightbound(),
                    new ExaltedEightbound(),
                    new WE_Helbrute(),
                    //---------- Fast Attack ----------
                    new WE_ChaosSpawn(),
                    //---------- Heavy Support ----------
                    new WE_ChaosLandRaider(),
                    new WE_Defiler(),
                    new WE_ChaosPredatorDestructor(),
                    new WE_ChaosPredatorAnnihilator(),
                    new WE_Forgefiend(),
                    new WE_Maulerfiend(),
                    //---------- Transport ----------
                    new WE_ChaosRhino(),
                    //---------- Flyer ----------
                    new WE_Heldrake(),
                    //---------- Lord of War ----------
                    new LordOfSkulls()
                };

                return datasheets;
            }
        }

        public override int GetFactionUpgradePoints(string upgrade)
        {
            return 0;
        }

        public override List<string> GetFactionUpgrades(List<string> keywords)
        {
            return new List<string>()
            {
                "(None)",
                "Drawn to Power",
                "Mutable Form",
                "Brazen Skin"
            };
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
            List<string> relics = new List<string>() { "(None)" };

            if(currentSubFaction == "Disciples of the Red Angels")
            {
                relics.Add("Burning Plate");

                if(keywords.Contains("DAEMON PRINCE"))
                {
                    relics.Add("Soulburner");
                }

                relics.Add("The Skull of An'gr'ant");
            }
            else
            {
                relics.Add("Helm of Brazen Ire");

                if(keywords.Contains("WORLD EATERS LORD") || keywords.Contains("MASTER OF EXECUTIONS"))
                {
                    relics.Add("Berzerker Glaive");
                }

                relics.Add("Talisman of Rage");
            }

            return relics;
        }

        public override List<string> GetSubFactions()
        {
            return new List<string>()
            {
                "World Eaters",
                "Disciples of the Red Angel"
            };
        }

        public override List<string> GetWarlordTraits(string keyword)
        {
            return new List<string>()
            {
                "Favoured of Khorne",
                "True Berzerker",
                "Battle Lust"
            };
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

        public override void SetPoints(int points)
        {

        }

        public override void SetSubFactionPanel(Panel panel)
        {
            Template template = new Template();
            template.LoadFactionTemplate(1, panel);

            ComboBox cmbSubFaction = panel.Controls["cmbSubFaction"] as ComboBox;

            cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentSubFaction);

            panel.Controls["lblSubfaction"].Text = "Select one of the following:";
            cmbSubFaction.Location = new System.Drawing.Point(cmbSubFaction.Location.X + 15, cmbSubFaction.Location.Y);
        }

        public override void UpdateSubFaction(bool code, Datasheets datasheet)
        {

        }

        public override string ToString()
        {
            return "World Eaters";
        }
    }
}
