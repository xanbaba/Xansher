namespace Xansher.Services;

public interface IDirectoryDialogManager
{
    // returns the path to selected directory
    public string? ShowOpenDirectoryDialog();
}