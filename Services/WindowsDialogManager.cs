using Microsoft.Win32;

namespace Xansher.Services;

public class WindowsDialogManager : IFileDialogManager, IDirectoryDialogManager
{
    public string? ShowOpenFileDialog(string? filter)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = filter ?? string.Empty
        };
        
        var dialogResult = openFileDialog.ShowDialog();
        if (dialogResult.HasValue && dialogResult.Value)
        {
            return openFileDialog.FileName;
        }

        return null;
    }

    public string? ShowOpenDirectoryDialog()
    {
        var openDialog = new OpenFolderDialog();
        
        var dialogResult = openDialog.ShowDialog();
        if (dialogResult.HasValue && dialogResult.Value)
        {
            return openDialog.FolderName;
        }

        return null;
    }
}