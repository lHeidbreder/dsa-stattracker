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
    
    public string LoadButtonContent { get; } = "Charakter laden...";
    public string NewButtonContent { get; } = "Neuen Charakter eingeben";

    public async Task LoadCharacter()
    {
        if (window.Value is null)
            throw new Exception("Kein Fenster gefunden");
        var path = await window.Value.StorageProvider.OpenFilePickerAsync(
            new Avalonia.Platform.Storage.FilePickerOpenOptions
            {
                FileTypeFilter = [new FilePickerFileType("JSON")],
                AllowMultiple = false,
            }
        );

        if (path.Count <= 0)
            return;

        var c = DSACharacter.Load(path[0].Path.AbsolutePath);
        if (c is not null)
            Chars.Add(c);
    }
    public void NewCharacter()
    {
        Chars.Add(new());
    }
    private void RemoveChar(DSACharacter c)
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
            _ = SaveChar(c);
        }
    }
    public async Task SaveChar(DSACharacter c)
    {
        if (window.Value is null)
            throw new Exception("Kein Fenster gefunden");
        var path = await window.Value.StorageProvider.SaveFilePickerAsync(
            new Avalonia.Platform.Storage.FilePickerSaveOptions
            {
                SuggestedFileName = c.Name + ".json",
                DefaultExtension = "json",
            }
        );

        if (path is null)
            return;

        if (File.Exists(path.Path.AbsolutePath)
            && !await Msg.OverwriteWarning(window.Value, path.Path.AbsolutePath))
            return;

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
    public void AlphaSort()
    {
        Chars = new ObservableCollection<DSACharacter>(
            Chars
            .OrderBy(c => c.Name)
        );
    }
}
