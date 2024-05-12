namespace Xansher.Services;

public interface IFileDialogManager
{
    // returns the filename of selected file
    public string? ShowOpenFileDialog(string? filter);
}