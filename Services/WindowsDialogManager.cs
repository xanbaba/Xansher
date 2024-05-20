using Microsoft.Win32;

namespace Xansher.Services;

public class WindowsDialogManager : IFileDialogManager, IDirectoryDialogManager
{
    public string? ShowOpenFileDialog(object? filter)
    {
        if (filter is not string filterString)
        {
            return null;
        }
        var openFileDialog = new OpenFileDialog
        {
            Filter = filterString ?? string.Empty
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