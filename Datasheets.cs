using Roster_Builder.Death_Guard;
using Roster_Builder.Necrons;
using Roster_Builder.Adeptus_Custodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Roster_Builder.Genestealer_Cults;

namespace Roster_Builder
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$unit")]
    #region JSON Attributes
        #region Death Guard JSON
        [JsonDerivedType(typeof(BiologusPutrifier), "BiologusPutrifier")]
        [JsonDerivedType(typeof(BlightlordTerminators), "BlightlordTerminators")]
        [JsonDerivedType(typeof(ChaosLandRaider), "ChaosLandRaider")]
        [JsonDerivedType(typeof(ChaosPredatorAnnihilator), "ChaosPredatorAnnihilator")]
        [JsonDerivedType(typeof(ChaosPredatorDestructor), "ChaosPredatorDestructor")]
        [JsonDerivedType(typeof(ChaosRhino), "ChaosRhino")]
        [JsonDerivedType(typeof(ChaosSpawn), "ChaosSpawn")]
        [JsonDerivedType(typeof(DeathshroudTerminators), "DeathshroudTerminators")]
        [JsonDerivedType(typeof(Defiler), "Defiler")]
        [JsonDerivedType(typeof(DG_ChaosLord), "DG_ChaosLord")]
        [JsonDerivedType(typeof(DG_Cultists), "DG_Cultists")]
        [JsonDerivedType(typeof(DG_DaemonPrince), "DG_DaemonPrince")]
        [JsonDerivedType(typeof(DG_Possessed), "DG_Possessed")]
        [JsonDerivedType(typeof(DG_TerminatorChaosLord), "DG_TerminatorChaosLord")]
        [JsonDerivedType(typeof(DG_TerminatorSorcerer), "DG_TerminatorSorcerer")]
        [JsonDerivedType(typeof(FoetidBloatDrone), "FoetidBloatDrone")]
        [JsonDerivedType(typeof(FoulBlightspawn), "FoulBlightspawn")]
        [JsonDerivedType(typeof(Helbrute), "Helbrute")]
        [JsonDerivedType(typeof(LordOfContagion), "LordOfContagion")]
        [JsonDerivedType(typeof(LordOfVirulence), "LordOfVirulence")]
        [JsonDerivedType(typeof(MalignantPlaguecaster), "MalignantPlaguecaster")]
        [JsonDerivedType(typeof(MiasmicMalignifier), "MiasmicMalignifier")]
        [JsonDerivedType(typeof(Mortarion), "Mortarion")]
        [JsonDerivedType(typeof(MyphiticBlightHauler), "MyphiticBlightHauler")]
        [JsonDerivedType(typeof(NoxiousBlightbringer), "NoxiousBlightbringer")]
        [JsonDerivedType(typeof(PlagueburstCrawler), "PlagueburstCrawler")]
        [JsonDerivedType(typeof(PlagueMarines), "PlagueMarines")]
        [JsonDerivedType(typeof(PlagueSurgeon), "PlagueSurgeon")]
        [JsonDerivedType(typeof(Poxwalkers), "Poxwalkers")]
        [JsonDerivedType(typeof(Tallyman), "Tallyman")]
        [JsonDerivedType(typeof(Typhus), "Typhus")]
        #endregion
        #region Necrons JSON
        [JsonDerivedType(typeof(AnnihilationBarge), "Annihilation Barge")]
        [JsonDerivedType(typeof(Anrakyr), "Anraky")]
        [JsonDerivedType(typeof(CanoptekDoomstalker), "Canoptek Doomstalker")]
        [JsonDerivedType(typeof(CanoptekPlasmacyte), "Canoptek Plasmacyte")]
        [JsonDerivedType(typeof(CanoptekReanimator), "Canoptek Reanimator")]
        [JsonDerivedType(typeof(CanoptekScarabs), "Canoptek Scarabs")]
        [JsonDerivedType(typeof(CanoptekSpyders), "Canoptek Spyders")]
        [JsonDerivedType(typeof(CanoptekWraiths), "Canoptek Wraiths")]
        [JsonDerivedType(typeof(CatacombBarge), "Catacomb Command Barge")]
        [JsonDerivedType(typeof(Chronomancer), "Chronomancer")]
        [JsonDerivedType(typeof(ConvergenceOfDominion), "Convergence of Dominion")]
        [JsonDerivedType(typeof(Cryptothralls), "Cryptothralls")]
        [JsonDerivedType(typeof(CtanDeceiver), "C'tan Deceiver")]
        [JsonDerivedType(typeof(CtanNightbringer), "C'tan Nightbringer")]
        [JsonDerivedType(typeof(CtanVoidDragon), "C'tan Void Dragon")]
        [JsonDerivedType(typeof(Deathmarks), "Deathmarks")]
        [JsonDerivedType(typeof(DoomScythe), "Doom Scythe")]
        [JsonDerivedType(typeof(DoomsdayArk), "Doomsday Ark")]
        [JsonDerivedType(typeof(FlayedOnes), "Flayed Ones")]
        [JsonDerivedType(typeof(GhostArk), "Ghost Ark")]
        [JsonDerivedType(typeof(HexmarkDestroyer), "Hexmark Destroyer")]
        [JsonDerivedType(typeof(Immortals), "Immortals")]
        [JsonDerivedType(typeof(Imotekh), "Imotekh")]
        [JsonDerivedType(typeof(LokhustDestroyers), "Lokhust Destroyers")]
        [JsonDerivedType(typeof(LokhustHeavyDestroyers), "Lokhust Heavy Destroyers")]
        [JsonDerivedType(typeof(LokhustLord), "Lokhust Lord")]
        [JsonDerivedType(typeof(Lychguard), "Lychguard")]
        [JsonDerivedType(typeof(Monolith), "Monolith")]
        [JsonDerivedType(typeof(NecronLord), "Necron Lord")]
        [JsonDerivedType(typeof(NecronWarriors), "Necron Warriors")]
        [JsonDerivedType(typeof(NightScythe), "Night Scythe")]
        [JsonDerivedType(typeof(Obelisk), "Obelisk")]
        [JsonDerivedType(typeof(Obyron), "Obyron")]
        [JsonDerivedType(typeof(OphydianDestroyers), "Ophydian Destroyers")]
        [JsonDerivedType(typeof(Orikan), "Orikan")]
        [JsonDerivedType(typeof(Overlord), "Overlord")]
        [JsonDerivedType(typeof(Plasmancer), "Plasmancer")]
        [JsonDerivedType(typeof(Psychomancer), "Psychomancer")]
        [JsonDerivedType(typeof(RoyalWarden), "Royal Warden")]
        [JsonDerivedType(typeof(SilentKing), "Silent King")]
        [JsonDerivedType(typeof(SkorpekhDestroyers), "Skorpekh Destroyers")]
        [JsonDerivedType(typeof(SkorpekhLord), "Skorpekh Lord")]
        [JsonDerivedType(typeof(Szeras), "Szeras")]
        [JsonDerivedType(typeof(Technomancer), "Technomancer")]
        [JsonDerivedType(typeof(TesseractVault), "Tesseract Vault")]
        [JsonDerivedType(typeof(TombBlades), "Tomb Blades")]
        [JsonDerivedType(typeof(TranscendentCtan), "Transcendent C'tan")]
        [JsonDerivedType(typeof(Trazyn), "Trazyn")]
        [JsonDerivedType(typeof(TriarchPraetorians), "Triarch Praetorians")]
        [JsonDerivedType(typeof(TriarchStalker), "Triarch Stalker")]
        [JsonDerivedType(typeof(Zahndrekh), "Zahndrekh")]
    #endregion
        #region Adeptus Custodes JSON
        [JsonDerivedType(typeof(Aleya), "Aleya")]
        [JsonDerivedType(typeof(AllarusCustodians), "AllarusCustodians")]
        [JsonDerivedType(typeof(AllarusShieldCaptain), "AllarusShieldCaptain")]
        [JsonDerivedType(typeof(AllarusVexilusPraetor), "AllarusVexilusPraetor")]
        [JsonDerivedType(typeof(AnathemaPsykanaRhino), "AnathemaPsykanaRhino")]
        [JsonDerivedType(typeof(BladeChampion), "BladeChampion")]
        [JsonDerivedType(typeof(CustodianGuard), "CustodianGuard")]
        [JsonDerivedType(typeof(CustodianWardens), "CustodianWardens")]
        [JsonDerivedType(typeof(KnightCentura), "KnightCentura")]
        [JsonDerivedType(typeof(Prosecutors), "Prosecutors")]
        [JsonDerivedType(typeof(ShieldCaptain), "ShieldCaptain")]
        [JsonDerivedType(typeof(TrajannValoris), "TrajannValoris")]
        [JsonDerivedType(typeof(Valerian), "Valerian")]
        [JsonDerivedType(typeof(VCDreadnought), "VCDreadnought")]
        [JsonDerivedType(typeof(VenerableLandRaider), "VenerableLandRaider")]
        [JsonDerivedType(typeof(VertusPraetors), "VertusPraetors")]
        [JsonDerivedType(typeof(VertusShieldCaptain), "VertusShieldCaptain")]
        [JsonDerivedType(typeof(VexilusPraetor), "VexilusPraetor")]
        [JsonDerivedType(typeof(Vigilators), "Vigilators")]
        [JsonDerivedType(typeof(Witchseekers), "Witchseekers")]
    #endregion
        #region Genestealer Cults JSON
        [JsonDerivedType(typeof(Aberrants), "Aberrants")]
        [JsonDerivedType(typeof(Abominant), "Abominant")]
        [JsonDerivedType(typeof(AchillesRidgerunners), "AchillesRidgerunners")]
        [JsonDerivedType(typeof(AcolyteHybrids), "AcolyteHybrids")]
        [JsonDerivedType(typeof(AcolyteIconward), "AcolyteIconward")]
        [JsonDerivedType(typeof(AtalanJackals), "AtalanJackals")]
        [JsonDerivedType(typeof(Biophagus), "Biophagus")]
        [JsonDerivedType(typeof(Clamavus), "Clamavus")]
        [JsonDerivedType(typeof(GoliathRockgrinder), "GoliathRockgrinder")]
        [JsonDerivedType(typeof(GoliathTruck), "GoliathTruck")]
        [JsonDerivedType(typeof(HybridMetamorphs), "HybridMetamorphs")]
        [JsonDerivedType(typeof(JackalAlphus), "JackalAlphus")]
        [JsonDerivedType(typeof(Kelermorph), "Kelermorph")]
        [JsonDerivedType(typeof(Locus), "Locus")]
        [JsonDerivedType(typeof(Magus), "Magus")]
        [JsonDerivedType(typeof(NeophyteHybrids), "NeophyteHybrids")]
        [JsonDerivedType(typeof(Nexos), "Nexos")]
        [JsonDerivedType(typeof(Patriarch), "Patriarch")]
        [JsonDerivedType(typeof(Primus), "Primus")]
        [JsonDerivedType(typeof(PurestrainGenestealers), "PurestrainGenestealers")]
        [JsonDerivedType(typeof(ReductusSaboteur), "ReductusSaboteur")]
        [JsonDerivedType(typeof(Sanctus), "Sanctus")]
        #endregion
    #endregion
    public abstract class Datasheets
    {
        public int UnitSize { get; set; }
        public int Points { get; set; }
        public string TemplateCode { get; set; }
        public List<string> Weapons { get; set; }
        public List<string> Keywords { get; set; }
        protected int DEFAULT_POINTS { get; set; }
        public bool isWarlord { get; set; }
        public string WarlordTrait { get; set; }
        public string[] PsykerPowers { get; set; }
        public string Factionupgrade { get; set; }
        public Template Template { get; }
        public string Relic { get; set; }
        public bool antiLoop { get; set; }
        public string Stratagem { get; set; }
        public Datasheets()
        {
            Weapons = new List<string>();
            Keywords = new List<string>();
            WarlordTrait = string.Empty;
            Template = new Template();
            Relic = "(None)";
        }

        public abstract void LoadDatasheets(Panel panel, Faction f);
        public abstract void SaveDatasheets(int code, Panel panel);
        public abstract Datasheets CreateUnit();
    }
}
