namespace dsa_battle_tracker.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using dsa_battle_tracker.Models;
using dsa_battle_tracker.Views;
using ReactiveUI;

public partial class MainWindowViewModel : ViewModelBase
{
    Lazy<Window?> window = new( () => App.MainWindow );
    public MainWindowViewModel()
    {
        RemoveChar = ReactiveCommand.Create<DSACharacter>(P_RemoveChar);
    }

    public string LoadButtonContent { get; } = "Charakter laden...";
    public string NewButtonContent { get; } = "Neuen Charakter eingeben";
    public void NewCharacter()
    {
        Chars.Add(new());
    }
    public ReactiveCommand<DSACharacter, Unit> RemoveChar { get; }
    private void P_RemoveChar(DSACharacter c)
    {
        Chars.Remove(c);
    }
    public void QuickSaveChar(DSACharacter c)
    {
        if (c.LoadPath is not null)
        {
            c.Save(c.LoadPath);
        }
        else
        {
            SaveChar(c);
        }
    }
    public void SaveChar(DSACharacter c)
    {
        if (window.Value is null)
            throw new Exception("Kein Fenster gefunden");
        var path = window.Value.StorageProvider.SaveFilePickerAsync(
            new Avalonia.Platform.Storage.FilePickerSaveOptions
            {
                SuggestedFileName = c.Name + ".json",
                DefaultExtension = "json",
            }
        ).Result;

        if (path is null)
            return;

        if (File.Exists(path.Path.AbsolutePath)) //FIXME
            return;
            //Msg.OverwriteWarning(window.Value, path.Path.AbsolutePath);

        c.Save(path.Path.AbsolutePath);
    }

    public ObservableCollection<DSACharacter> _chars = [];
    public ObservableCollection<DSACharacter> Chars
    {
        get { return _chars; }
        set { SetProperty(ref _chars, value); }
    }

    public void INISort()
    {
        Chars = new ObservableCollection<DSACharacter>(
            Chars
            .OrderBy(c => c.IniBase)
            .OrderBy(c => c.Ini)
        );
    }

}
