using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

public class Msg {

    //FIXME: broken somehow?
    public static bool OverwriteWarning(Window source, string path)
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

        var result = box.ShowAsPopupAsync(source);

        return result.Result == ButtonResult.Yes;
    }

}