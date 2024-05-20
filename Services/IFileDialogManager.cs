namespace Xansher.Services;

public interface IFileDialogManager
{
    /// <summary>
    ///     Opens an UI window for selecting the file
    /// </summary>
    /// <param name="filter">Is needed for some UI windows in order to restrict the extension that selected file can have</param>
    /// <returns>Path to the selected file</returns>
    public string? ShowOpenFileDialog(object? filter);
}