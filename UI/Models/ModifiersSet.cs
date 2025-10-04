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
        H = 1,
        N = 2,
        S = 4,
        P = 8,
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
    public enum DarknessModifiers
    {
        Moonlight,
        Starlight,
        Invisible,
        Darkness,
    }
    public DarknessModifiers? DarknessModifier { get; set; } = null;
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

        if (DarknessModifier is not null)
        {
            rtn += DarknessModifier switch
            {
                DarknessModifiers.Moonlight => 3,
                DarknessModifiers.Starlight => 5,
                DarknessModifiers.Invisible => 6,
                DarknessModifiers.Darkness => 8,
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

        if (TargetPosition is not null)
        {
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
                Outnumbered.OneMoreEnemy => 0,
                Outnumbered.MoreEnemies => 0,
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

        if (DarknessModifier is not null)
        {
            rtn += DarknessModifier switch
            {
                DarknessModifiers.Moonlight => 3,
                DarknessModifiers.Starlight => 5,
                DarknessModifiers.Invisible => 6,
                DarknessModifiers.Darkness => 8,
                _ => throw new InvalidOperationException(),
            };

        }

        if (WaterModifier is not null)
        {
            rtn += WaterModifier switch
            {
                WaterModifiers.KneeDeep => 2,
                WaterModifiers.HipDeep => 4,
                WaterModifiers.ShoulderDeep => 6,
                WaterModifiers.Submerged => 6,
                _ => throw new InvalidOperationException(),
            };
        }

        if (CloseQuartersModifier is not null)
        {
            rtn += CloseQuartersModifier switch
            {
                CloseQuartersModifiers.LongSwing => 2,
                CloseQuartersModifiers.ShortSwing => 0,
                CloseQuartersModifiers.PokeyStick => 2,
                _ => throw new InvalidOperationException(),
            };
        }

        if (TargetPosition is not null)
        {
            rtn += TargetPosition switch
            {
                PositionModifier.Laying => -5,
                PositionModifier.Kneeling => -3,
                PositionModifier.Flying => 4,
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

        if (OutnumberedLevel is not null)
        {
            rtn += OutnumberedLevel switch
            {
                Outnumbered.MoreAllies => 0,
                Outnumbered.OneMoreEnemy => 1,
                Outnumbered.MoreEnemies => 2,
                _ => throw new InvalidOperationException(),
            };
        }

        return new(true, rtn);
    }
}

public class RangedModifierSet : ObservableObject
{
    #region Boons/Banes
    public bool HasDistanceSense = false;
    #endregion

    #region WeaponType
    public enum RangedWeaponTypes
    {
        Shot,
        Throw,
    }
    public RangedWeaponTypes WeaponType = RangedWeaponTypes.Shot;
    #endregion

    #region Abilities
    public enum SharpshooterAbilities
    {
        Sharpshooter,
        MasterBowman,
    }
    public SharpshooterAbilities? SharpshooterAbility;
    #endregion

    #region Size
    public enum SizeModifiers
    {
        tiny,
        verySmall,
        small,
        medium,
        large,
        veryLarge,
    }
    public SizeModifiers TargetSize = SizeModifiers.medium;
    #endregion

    #region Cover
    public enum CoverModifiers
    {
        half,
        threeQuarters,
    }
    public CoverModifiers? CoverModifier;
    #endregion

    #region Distance
    public enum DistanceModifiers
    {
        veryClose,
        close,
        medium,
        far,
        veryFar,
    }
    public DistanceModifiers Distance = DistanceModifiers.medium;
    //TODO: one-eyed, color blind
    #endregion

    #region Movement
    public enum MovementModifiers
    {
        affixed,
        still,
        lightMovement,
        fastMovement,
        veryFastMovement,
        //TODO: movement of bodypart when targeting
    }
    public MovementModifiers TargetMovement = MovementModifiers.still;
    public int CombatantsInH = 0;
    public int CombatantsInNS = 0;
    #endregion

    #region Sight
    public enum DarknessModifiers
    {
        DawnDusk,
        Moonlight,
        Starlight,
        Darkness,
    }
    public DarknessModifiers? Darkness;
    public enum NightVisionBoons
    {
        NightVision,
        DarkVision,
        NightBlind,
    }
    public NightVisionBoons? NightVision;
    public enum FogModifiers
    {
        Mist,
        DenseFog,
    }
    public FogModifiers? Fog;
    public bool IsTargetInvisible = false;
    #endregion

    #region Steep Shot
    public enum SteepnessModifiers
    {
        Up,
        Down,
    }
    public SteepnessModifiers? Steepness;
    #endregion

    #region Wind
    public enum WindModifiers
    {
        Gusts,
        StrongGusts,
    }
    public WindModifiers? Wind;
    #endregion

    #region Aiming
    private bool isQuickshot = false;
    public bool IsQuickshot
    {
        get => isQuickshot;
        set
        {
            isQuickshot = value;
            if (isQuickshot)
                ActionsAiming = 0;
        }
    }
    public int ActionsAiming = 0; //TODO: only show and use when no quickshot
    #endregion

    #region Mounted
    public bool IsMounted = false; //TODO: only use the following when actually mounted
    public enum MountMovements
    {
        Still,
        Walking,
        Running,
    }
    public MountMovements MountMovement = MountMovements.Still;
    public bool Mounted_NoHarness = false;
    #endregion

    #region Other
    public bool IsSecondShot = false;
    public bool IsInWater = false;
    #endregion


    /// <summary>
    /// 
    /// </summary>
    /// <returns>False indicates impossibility, int is modifier</returns>
    public Tuple<bool, int> AttackMod()
    {
        int rtn = 0;

        if (HasDistanceSense)
            rtn -= 2;

        rtn += TargetSize switch
        {
            SizeModifiers.tiny => 8,
            SizeModifiers.verySmall => 6,
            SizeModifiers.small => 4,
            SizeModifiers.medium => 2,
            SizeModifiers.large => 0,
            SizeModifiers.veryLarge => -2,
            _ => throw new InvalidOperationException(),
        };

        if (CoverModifier is not null)
            rtn += CoverModifier switch
            {
                CoverModifiers.half => 2,
                CoverModifiers.threeQuarters => 4,
                _ => throw new InvalidOperationException(),
            };

        rtn += Distance switch
        {
            DistanceModifiers.veryClose => -2,
            DistanceModifiers.close => 0,
            DistanceModifiers.medium => 4,
            DistanceModifiers.far => 8,
            DistanceModifiers.veryFar => 12,
            _ => throw new InvalidOperationException(),
        };

        if (CombatantsInH + CombatantsInNS == 0)
            rtn += TargetMovement switch
            {
                MovementModifiers.affixed => -4,
                MovementModifiers.still => -2,
                MovementModifiers.lightMovement => 0,
                MovementModifiers.fastMovement => 2,
                MovementModifiers.veryFastMovement => 4,
                _ => throw new InvalidOperationException(),
            };
        rtn += (CombatantsInH * 2);
        rtn += (CombatantsInNS * 3);

        if (Fog is not null)
            rtn += Fog switch
            {
                FogModifiers.Mist => 2,
                FogModifiers.DenseFog => 4,
                _ => throw new InvalidOperationException(),
            };

        if (Darkness is not null)
        {
            int darknessMod = Darkness switch
            {
                DarknessModifiers.DawnDusk => 2,
                DarknessModifiers.Moonlight => 4,
                DarknessModifiers.Starlight => 6,
                DarknessModifiers.Darkness => 8,
                _ => throw new InvalidOperationException(),
            };

            if (NightVision is not null)
                switch (NightVision)
                {
                    case NightVisionBoons.NightVision: throw new NotImplementedException(); //FIXME
                    case NightVisionBoons.DarkVision: darknessMod /= 2; break;
                    case NightVisionBoons.NightBlind: darknessMod = Math.Min(8, darknessMod * 2); break;
                    default: throw new InvalidOperationException();
                }

            rtn += darknessMod;
        }

        if (IsTargetInvisible)
            rtn += 8;

        if (Steepness is not null)
            rtn += Steepness switch
            {
                SteepnessModifiers.Down => 2,
                SteepnessModifiers.Up => (WeaponType == RangedWeaponTypes.Shot) ? 4 : 8,
                _ => throw new InvalidOperationException(),
            };

        if (Wind is not null)
            rtn += Wind switch
            {
                WindModifiers.Gusts => 4,
                WindModifiers.StrongGusts => 8,
                _ => throw new InvalidOperationException(),
            };

        if (IsQuickshot)
            rtn += SharpshooterAbility switch
            {
                null => 2,
                SharpshooterAbilities.Sharpshooter => 1,
                SharpshooterAbilities.MasterBowman => 0,
                _ => throw new InvalidOperationException(),
            };
        else
        {
            if (SharpshooterAbility is not null)
                rtn -= ActionsAiming;
            else rtn -= ActionsAiming / 2;
        }

        if (IsMounted)
        {
            rtn += MountMovement switch
            {
                MountMovements.Still => (WeaponType == RangedWeaponTypes.Shot) ? 2 : 1,
                MountMovements.Walking => (WeaponType == RangedWeaponTypes.Shot) ? 4 : 2,
                MountMovements.Running => (WeaponType == RangedWeaponTypes.Shot) ? 8 : 4,
                _ => throw new InvalidOperationException(),
            };
            if (Mounted_NoHarness)
                rtn += (WeaponType == RangedWeaponTypes.Shot) ? 4 : 2;
        }

        if (IsSecondShot)
            rtn += (WeaponType == RangedWeaponTypes.Shot) ? 4 : 2;

        if (IsInWater)
            rtn += 5;

        return new(true, rtn);
    }
}