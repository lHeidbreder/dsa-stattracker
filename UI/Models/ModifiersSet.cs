using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace dsa_battle_tracker.Models;

public class MeleeModifierSet : ObservableObject
{
    [Flags]
    public enum Distance
    {
        H,
        N,
        S,
        P,
    }
    public Distance Weapon_DK { get; set; } = Distance.H | Distance.N | Distance.S | Distance.P;
    public Distance Actual_DK { get; set; } = Distance.N;

    public enum Hitlocation
    {
        //TODO
    }
    public Hitlocation? TargetLocation { get; set; } = null;
}