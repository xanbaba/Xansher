namespace Xansher.Services;

public interface IDirectoryManager
{
    // returns true if directory was created successfully, otherwise false
    public bool CreateDirectory(string path);
    public bool RemoveDirectory(string path);
}