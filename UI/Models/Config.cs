using System;
using System.IO;

namespace dsa_battle_tracker.Models;

public class Config
{
    public static Config Instance { get; } = new();
    public readonly string AppDataPath;

    public Config()
    {
        AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DSAStattracker/";
        if (!Directory.Exists(AppDataPath))
            Directory.CreateDirectory(AppDataPath);
    }
}