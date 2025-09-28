namespace dsa_battle_tracker.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using dsa_battle_tracker.Models;

public partial class ModifiersViewModel : ViewModelBase
{
    #region Melee
    public string Header_Melee { get; } = "Nahkampf";
    public MeleeModifierSet MeleeModifiers { get; } = new();
    #endregion

    #region Ranged
    public string Header_Ranged { get; } = "Fernkampf";
    #endregion

    #region Spells
    #endregion

    #region Miracles
    #endregion
}
