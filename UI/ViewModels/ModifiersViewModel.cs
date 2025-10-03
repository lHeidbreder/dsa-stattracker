namespace dsa_battle_tracker.ViewModels;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using dsa_battle_tracker.Models;
using DynamicData.Binding;

public partial class ModifiersViewModel : ViewModelBase
{
    #region Melee
    public string Header_Melee { get; } = "Nahkampf";
    public MeleeModifierSet MeleeModifiers { get; } = new();
    public static string Label_OwnReach => "Eigene DK";
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Sum_AT_Mod))]
    public bool Own_H
    {
        get => (MeleeModifiers.Weapon_DK & MeleeModifierSet.Distance.H) > 0;
        set
        {
            if (value)
                MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK | MeleeModifierSet.Distance.H;
            else MeleeModifiers.Weapon_DK = MeleeModifiers.Weapon_DK & ~MeleeModifierSet.Distance.H;
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
        }
    }
    public static string Label_ActualReach => "Tatsächliche DK";
    public static IEnumerable<MeleeModifierSet.Distance> DK_Options => Enum.GetValues<MeleeModifierSet.Distance>();
    public MeleeModifierSet.Distance ActualDK { get; set; } = MeleeModifierSet.Distance.N;

    public static IEnumerable<MeleeModifierSet.Hitlocation?> HitLocations => [null, .. Enum.GetValues<MeleeModifierSet.Hitlocation>()];
    public MeleeModifierSet.Hitlocation? TargetedLocation { get; set; }

    public static IEnumerable<MeleeModifierSet.DarknessModifiers?> DarknessModifiers => [null, .. Enum.GetValues<MeleeModifierSet.DarknessModifiers>()];
    public MeleeModifierSet.DarknessModifiers LightLevel { get; set; }


    public string Sum_AT_Mod
    {
        get
        {
            var result = MeleeModifiers.AttackMod();
            if (!result.Item1)
                return "Unmöglich";
            return result.Item2.ToString();
        }
    }
    public string Sum_PA_Mod
    {
        get
        {
            var result = MeleeModifiers.DefenseMod(); //FIXME: not implemented
            if (!result.Item1)
                return "Unmöglich";
            return result.Item2.ToString();
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
