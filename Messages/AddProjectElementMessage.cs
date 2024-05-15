namespace Xansher.Messages;

public class AddProjectElementMessage(string path, string fileName) : IMessage
{
    public string Path { get; } = path;
    public string FileName { get; } = fileName;
}