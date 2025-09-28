using System;
using System.ComponentModel.DataAnnotations;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData.Aggregation;
using MsBox.Avalonia.Dto;

namespace dsa_battle_tracker.Models;

public class MeleeModifierSet : ObservableObject
{
    #region Distance
    [Flags]
    public enum Distance
    {
        H,
        N,
        S,
        P = 4,
    }
    public Distance Weapon_DK { get; set; } = Distance.H | Distance.N | Distance.S | Distance.P;
    public Distance Actual_DK { get; set; } = Distance.N;
    public static int DK_Difference(Distance own, Distance actual)
    {
        if ((own & actual) == actual)
            return 0;

        if (actual > own)
            return Util.MaxBit(own) - Util.MinBit(actual);
        else
            return Util.MinBit(own) - Util.MaxBit(actual);
    }
    #endregion

    #region Location
    public enum Hitlocation
    {
        Head,
        Chest,
        MainArm,
        OffArm,
        Belly,
        Leg,
    }
    public Hitlocation? TargetLocation { get; set; } = null;
    #endregion

    #region Sight
    public enum SightModifiers
    {
        Moonlight,
        Starlight,
        Invisible,
        Darkness,
    }
    public SightModifiers? SightModifier { get; set; } = null;
    #endregion

    #region Water
    public enum WaterModifiers
    {
        KneeDeep,
        HipDeep,
        ShoulderDeep,
        Submerged,
    }
    public WaterModifiers? WaterModifier { get; set; } = null;
    #endregion

    #region CQ
    public enum CloseQuartersModifiers
    {
        LongSwing,
        ShortSwing,
        PokeyStick,
    }
    public CloseQuartersModifiers? CloseQuartersModifier { get; set; } = null;
    #endregion

    #region Position
    public enum PositionModifier
    {
        Laying,
        Kneeling,
        Flying,
    }
    public PositionModifier? TargetPosition { get; set; } = null;
    public PositionModifier? OwnPosition { get; set; } = null;
    #endregion

    #region Offhand
    public enum OffhandAbilities
    {
        Offhand,
        DualWield1,
        DualWield2,
    }
    public OffhandAbilities? OffhandAbility { get; set; } = null;
    public bool IsOffhand { get; set; } = false;
    #endregion

    #region Surprise
    public enum TargetSurprise
    {
        Surprised,
        Restrained,
        Immobile,
    }
    public TargetSurprise? TargetSurpriseLevel { get; set; } = null;
    #endregion

    #region Numbers
    public enum Outnumbered
    {
        MoreAllies,
        OneMoreEnemy,
        MoreEnemies,
    }
    public Outnumbered? OutnumberedLevel { get; set; } = null;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <returns>False indicates impossibility, int is modifier</returns>
    public Tuple<bool, int> AttackMod()
    {
        int rtn = 0;

        var distance = Math.Abs(DK_Difference(this.Weapon_DK, this.Actual_DK));
        if (distance >= 2)
            return new(false, 0);
        if (distance > 0)
            rtn += 6;

        if (TargetLocation is not null)
        {
            rtn += TargetLocation switch
            {
                Hitlocation.Head => 4,
                Hitlocation.Chest => 6,
                Hitlocation.MainArm => 6,
                Hitlocation.OffArm => 6,
                Hitlocation.Belly => 4,
                Hitlocation.Leg => 2,
                _ => throw new InvalidOperationException(),
            };

        }

        if (SightModifier is not null)
        {
            rtn += SightModifier switch
            {
                SightModifiers.Moonlight => 3,
                SightModifiers.Starlight => 5,
                SightModifiers.Invisible => 6,
                SightModifiers.Darkness => 8,
                _ => throw new InvalidOperationException(),
            };

        }

        if (WaterModifier is not null)
        {
            rtn += WaterModifier switch
            {
                WaterModifiers.KneeDeep => 0,
                WaterModifiers.HipDeep => 2,
                WaterModifiers.ShoulderDeep => 4,
                WaterModifiers.Submerged => 6,
                _ => throw new InvalidOperationException(),
            };
        }

        if (CloseQuartersModifier is not null)
        {
            rtn += CloseQuartersModifier switch
            {
                CloseQuartersModifiers.LongSwing => 6,
                CloseQuartersModifiers.ShortSwing => 2,
                CloseQuartersModifiers.PokeyStick => 2,
                _ => throw new InvalidOperationException(),
            };
        }

        if (TargetPosition is not null) {
            rtn += TargetPosition switch
            {
                PositionModifier.Laying => -3,
                PositionModifier.Kneeling => -1,
                PositionModifier.Flying => 2,
                _ => throw new InvalidOperationException(),
            };
        }

        if (OwnPosition is not null)
        {
            rtn += OwnPosition switch
            {
                PositionModifier.Laying => 3,
                PositionModifier.Kneeling => 1,
                PositionModifier.Flying => 0,
                _ => throw new InvalidOperationException(),
            };
        }

        if (IsOffhand)
        {
            if (OffhandAbility is not null)
            {
                rtn += OffhandAbility switch
                {
                    OffhandAbilities.Offhand => 6,
                    OffhandAbilities.DualWield1 => 3,
                    OffhandAbilities.DualWield2 => 0,
                    _ => throw new InvalidOperationException(),
                };
            }
            else rtn += 9;
        }

        if (TargetSurpriseLevel is not null)
        {
            rtn += TargetSurpriseLevel switch
            {
                TargetSurprise.Surprised => -5,
                TargetSurprise.Restrained => -8,
                TargetSurprise.Immobile => -10,
                _ => throw new InvalidOperationException(),
            };
        }

        if (OutnumberedLevel is not null)
        {
            rtn += OutnumberedLevel switch
            {
                Outnumbered.MoreAllies => -1,
                Outnumbered.OneMoreEnemy => 1,
                Outnumbered.MoreEnemies => 2,
                _ => throw new InvalidOperationException(),
            };
        }

        return new(true, rtn);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>False indicates impossibility, int is modifier</returns>
    public Tuple<bool, int> DefenseMod()
    {
        int rtn = 0;

        var distance = DK_Difference(this.Weapon_DK, this.Actual_DK);
        if (distance >= 2)
            return new(false, 0);
        if (distance > 0)
            rtn += 6;

        return new(true, rtn);
    }
}