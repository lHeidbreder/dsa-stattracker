using System.Threading.Tasks;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

public class Msg {

    public static async Task<bool> OverwriteWarning(Window source, string path)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(
            new MessageBoxStandardParams
            {
                ContentTitle = "Datei existiert",
                ContentMessage = $"Die Datei \"{path}\" existiert bereits.\nÜberschreiben?",
                ButtonDefinitions = ButtonEnum.YesNo,
                Icon = Icon.Warning,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            });

        var result = await box.ShowAsPopupAsync(source);

        return result == ButtonResult.Yes;
    }

}