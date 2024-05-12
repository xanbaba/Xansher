namespace Xansher.Messages;

public class OpenProjectMessage(string path)
{
    public string Path { get; set; } = path;
}