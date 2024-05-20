namespace Xansher.Services;

public interface IDirectoryManager
{
    /// <summary>
    ///     Creates a directory in a specified path
    /// </summary>
    /// <param name="path">Path to the directory(including the name of new directory itself)</param>
    /// <returns>True, if directory was created successfully, otherwise false</returns>
    public bool CreateDirectory(string path);
    
    /// <summary>
    ///     Removes a directory in a specified path
    /// </summary>
    /// <param name="path">Path to the directory(including the name of new directory itself)</param>
    /// <returns>True, if directory was removed successfully, otherwise false</returns>
    public bool RemoveDirectory(string path);
}