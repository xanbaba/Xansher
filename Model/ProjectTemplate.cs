namespace Xansher.Model;

public class ProjectTemplate(string cliIdentificator, string userInterfaceIdentificator)
{
    public string CliIdentificator { get; set; } = cliIdentificator;
    public string UserInterfaceIdentificator { get; set; } = userInterfaceIdentificator;
}