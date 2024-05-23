namespace Xansher.Messages;

public class AddProjectElementMessage(string path, string name, bool isDirectory) : IMessage
{
    public string Path { get; } = path;
    public string Name { get; } = name;
    public bool IsDirectory { get; } = isDirectory;
}