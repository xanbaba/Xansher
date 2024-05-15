using System.IO;

namespace Xansher.Services;

public class WindowsDirectoryManager : IDirectoryManager
{
    public bool CreateDirectory(string path)
    {
        if (Directory.Exists(path)) return false;

        try
        {
            Directory.CreateDirectory(path);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public bool RemoveDirectory(string path)
    {
        if (!Directory.Exists(path)) return false;
        
        try
        {
            Directory.Delete(path, true);
        }
        catch (Exception)
        {
            return false;
        }
        return true;

    }
}