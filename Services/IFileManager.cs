namespace Xansher.Services;

public interface IFileManager
{
    // returns true if file was created successfully, otherwise false
    public bool CreateFile(string directoryPath, string filename);
    bool Remove(string filePath);
}