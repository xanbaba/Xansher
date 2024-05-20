namespace Xansher.Services;

public interface IFileManager
{
    /// <summary>
    ///     Creates file in a specified directory path
    /// </summary>
    /// <param name="directoryPath">Path to directory where file will be created</param>
    /// <param name="filename">Name of the file</param>
    /// <returns>True, if file created successfully, otherwise false</returns>
    public bool CreateFile(string directoryPath, string filename);
    
    /// <summary>
    ///     Removes a file from a specified path
    /// </summary>
    /// <param name="filePath">Path to the file which will be removed</param>
    /// <returns>True, if file was removed successfully, otherwise false</returns>
    public bool Remove(string filePath);
}