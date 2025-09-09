using System;
using System.IO;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace dsa_battle_tracker.Models;

public class DSACharacter : ObservableObject
{
    [JsonIgnore]
    public string? LoadPath { get; private set; }
    private string _name = "";
    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }
    private int[] _stats = { 0, 0, 0, 0 };
    private int[] _maxstats = { 0, 0, 0, 0 };
    public int LE
    {
        get => _stats[StatIndices.LE];
        set { SetProperty(ref _stats[0], value); }
    }
    public int MaxLE
    {
        get => _maxstats[StatIndices.LE];
        set { SetProperty(ref _maxstats[0], value); }
    }
    
    public int AE
    {
        get => _stats[StatIndices.AE];
        set { SetProperty(ref _stats[StatIndices.AE], value); }
    }
    public int MaxAE
    {
        get => _maxstats[StatIndices.AE];
        set { SetProperty(ref _maxstats[StatIndices.AE], value); }
    }

    public int AU
    {
        get => _stats[StatIndices.AU];
        set{ SetProperty(ref _stats[StatIndices.AU], value); }
    }
    public int MaxAU
    {
        get => _maxstats[StatIndices.AU];
        set { SetProperty(ref _maxstats[StatIndices.AU], value); }
    }

    public int KE
    {
        get => _stats[StatIndices.KE];
        set { SetProperty(ref _maxstats[StatIndices.KE], value); }
    }
    public int MaxKE
    {
        get => _maxstats[StatIndices.KE];
        set { SetProperty(ref _maxstats[StatIndices.KE], value); }
    }

    public int IniBase { get; set; }
    public int Ini { get; set; }

    public DSACharacter(string Name = "Neuer Charakter", int MaxLE = 0, int MaxAU = 0, int MaxAE = 0, int MaxKE = 0, string? LoadPath = null)
    {
        this.LoadPath = LoadPath;

        this.Name = Name;

        this.MaxLE = MaxLE;
        this.LE = MaxLE;

        this.MaxAU = MaxAU;
        this.AU = MaxAU;

        this.MaxAE = MaxAE;
        this.AE = MaxAE;

        this.MaxKE = MaxKE;
        this.KE = MaxKE;
    }

    public static System.Collections.Generic.IEnumerable<DSACharacter>? LoadList(string Path)
    {
        if (!File.Exists(Path))
            return null;
        
        var s = File.ReadAllText(Path);
        return System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.IEnumerable<DSACharacter>>(s);
    }
    public static DSACharacter? Load(string Path)
    {
        if (!File.Exists(Path))
            return null;

        var s = File.ReadAllText(Path);
        var c = System.Text.Json.JsonSerializer.Deserialize<DSACharacter>(s);
        if (c is not null)
            c.LoadPath = Path;
        return c;
    }
    public void Save(string Path)
    {
        var o = System.Text.Json.JsonSerializer.Serialize(this);
        File.WriteAllText(Path, o);
        this.LoadPath = Path;
    }

    public static class StatIndices
    {
        public static readonly int LE = 0;
        public static readonly int AU = 1;
        public static readonly int AE = 2;
        public static readonly int KE = 3;
    }
}
