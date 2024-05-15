using System.IO;

namespace Xansher.Services;

public class WindowsFileManager : IFileManager
{
    public bool CreateFile(string directoryPath, string filename)
    {
        if (!Directory.Exists(directoryPath)) return false;
        var filePath = $@"{directoryPath}\{filename}";
        if (File.Exists(filePath)) return false;
        try
        {
            File.Create(filePath).Close();
        }
        catch (DirectoryNotFoundException)
        {
            return false;
        }
        return true;

    }

    public bool Remove(string filePath)
    {
        if (!File.Exists(filePath)) return true;
        
        try
        {
            File.Delete(filePath);
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }
}