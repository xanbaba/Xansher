namespace Xansher.Services;

public interface IDirectoryDialogManager
{
    /// <summary>
    ///     Opens an UI window for selecting the directory
    /// </summary>
    /// <returns>The path to selected directory</returns>
    public string? ShowOpenDirectoryDialog();
}