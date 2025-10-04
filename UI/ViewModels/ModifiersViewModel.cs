namespace dsa_battle_tracker.ViewModels;

using System;
using System.Collections.Generic;
using dsa_battle_tracker.Models;

public partial class ModifiersViewModel : ViewModelBase
{
    #region Melee
    public string Header_Melee { get; } = "Nahkampf";
    public MeleeModifierSet MeleeModifiers { get; } = new();
    public static string Label_OwnReach => "Eigene DK";

    public bool Own_H
    {
        get => (MeleeModifiers.Weapon_DK & MeleeModifierSet.Distance.H) > 0;
        set
        {
            if (value)
                MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK | MeleeModifierSet.Distance.H;
            else MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK & ~MeleeModifierSet.Distance.H;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }
    public bool Own_N
    {
        get => (MeleeModifiers.Weapon_DK & MeleeModifierSet.Distance.N) > 0;
        set
        {
            if (value)
                MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK | MeleeModifierSet.Distance.N;
            else MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK & ~MeleeModifierSet.Distance.N;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }
    public bool Own_S
    {
        get => (MeleeModifiers.Weapon_DK & MeleeModifierSet.Distance.S) > 0;
        set
        {
            if (value)
                MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK | MeleeModifierSet.Distance.S;
            else MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK & ~MeleeModifierSet.Distance.S;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }
    public bool Own_P
    {
        get => (MeleeModifiers.Weapon_DK & MeleeModifierSet.Distance.P) > 0;
        set
        {
            if (value)
                MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK | MeleeModifierSet.Distance.P;
            else MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK & ~MeleeModifierSet.Distance.P;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }
    public static string Label_ActualReach => "Tatsächliche DK";
    public static IEnumerable<MeleeModifierSet.Distance> DK_Options => Enum.GetValues<MeleeModifierSet.Distance>();
    public MeleeModifierSet.Distance ActualDK
    {
        get => MeleeModifiers.Actual_DK;
        set
        {
            MeleeModifiers.Actual_DK = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }
    
    public static string Label_TargetLocation => "Ziel";
    public static IEnumerable<MeleeModifierSet.Hitlocation?> HitLocations => [null, .. Enum.GetValues<MeleeModifierSet.Hitlocation>()];
    public MeleeModifierSet.Hitlocation? TargetedLocation
    {
        get => MeleeModifiers.TargetLocation;
        set
        {
            MeleeModifiers.TargetLocation = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }

    public static string Label_DarknessModifier => "Licht";
    public static IEnumerable<MeleeModifierSet.DarknessModifiers?> DarknessModifiers => [null, .. Enum.GetValues<MeleeModifierSet.DarknessModifiers>()];
    public MeleeModifierSet.DarknessModifiers? LightLevel
    {
        get => MeleeModifiers.DarknessModifier;
        set
        {
            MeleeModifiers.DarknessModifier = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }

    public static string Label_CloseQuartersModifier => "Beengte Umgebung";
    public static IEnumerable<MeleeModifierSet.CloseQuartersModifiers?> CQC_Modifiers => [null, .. Enum.GetValues<MeleeModifierSet.CloseQuartersModifiers>()];
    public MeleeModifierSet.CloseQuartersModifiers? CQC
    {
        get => MeleeModifiers.CloseQuartersModifier;
        set
        {
            MeleeModifiers.CloseQuartersModifier = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }

    public static IEnumerable<MeleeModifierSet.PositionModifier?> Position_Modifiers => [null, .. Enum.GetValues<MeleeModifierSet.PositionModifier>()];
    public static string Label_TargetPosition => "Position des Gegners";
    public MeleeModifierSet.PositionModifier? TargetPosition
    {
        get => MeleeModifiers.TargetPosition;
        set
        {
            MeleeModifiers.TargetPosition = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }
    public static string Label_OwnPosition => "Eigene Position";
    public MeleeModifierSet.PositionModifier? OwnPosition
    {
        get => MeleeModifiers.OwnPosition;
        set
        {
            MeleeModifiers.OwnPosition = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod)); 
        }
    }

    public static string Label_OffhandAbility => "Linkhand SF";
    public static IEnumerable<MeleeModifierSet.OffhandAbilities?> OffhandAbilities => [null, .. Enum.GetValues<MeleeModifierSet.OffhandAbilities>()];
    public MeleeModifierSet.OffhandAbilities? OffhandAbility
    {
        get => MeleeModifiers.OffhandAbility;
        set
        {
            MeleeModifiers.OffhandAbility = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }
    public static string Label_IsOffhand => "Falsche Hand?";
    public bool IsOffhand
    {
        get => MeleeModifiers.IsOffhand;
        set
        {
            MeleeModifiers.IsOffhand = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }

    public static string Label_TargetSurpriseLevel => "Ziel überrascht?";
    public static IEnumerable<MeleeModifierSet.TargetSurprise?> SurpriseLevels => [null, .. Enum.GetValues<MeleeModifierSet.TargetSurprise>()];
    public MeleeModifierSet.TargetSurprise? TargetSurprise
    {
        get => MeleeModifiers.TargetSurpriseLevel;
        set
        {
            MeleeModifiers.TargetSurpriseLevel = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }

    public static string Label_OutnumberedLevel => "Überzahl";
    public static IEnumerable<MeleeModifierSet.Outnumbered?> OutnumberedLevels => [null, .. Enum.GetValues<MeleeModifierSet.Outnumbered>()];
    public MeleeModifierSet.Outnumbered? Outnumbered
    {
        get => MeleeModifiers.OutnumberedLevel;
        set
        {
            MeleeModifiers.OutnumberedLevel = value;
            this.OnPropertyChanged(nameof(Sum_AT_Mod));
            this.OnPropertyChanged(nameof(Sum_PA_Mod));
        }
    }


    public string Sum_AT_Mod
    {
        get
        {
            var result = MeleeModifiers.AttackMod();
            if (!result.Item1)
                return "AT: Unmöglich";
            return "AT: " + (result.Item2 > 0 ? "+" : "") + result.Item2.ToString();
        }
    }
    public string Sum_PA_Mod
    {
        get
        {
            var result = MeleeModifiers.DefenseMod();
            if (!result.Item1)
                return "PA: Unmöglich";
            return "PA: " + (result.Item2 > 0 ? "+" : "") + result.Item2.ToString();
        }
    }
    #endregion

    #region Ranged
    public string Header_Ranged { get; } = "Fernkampf";
    #endregion

    #region Spells
    public string Header_Magic { get; } = "Zauber";
    #endregion

    #region Miracles
    public string Header_Carmic { get; } = "Liturgien";
    #endregion
}
