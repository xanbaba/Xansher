namespace Xansher.Services;

public interface ICliManager
{
    /// <summary>
    ///     Creates new .NET project in a specified path. See: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new
    /// </summary>
    /// <param name="projectTemplate">See: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-sdk-templates</param>
    /// <param name="projectDirectory">Path to the directory where the project will be created</param>
    /// <returns>True, if project was created successfully, otherwise false</returns>
    public bool CreateNewProject(string projectTemplate, string projectDirectory);

    /// <summary>
    /// Runs a .NET project in a specified path. See: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run
    /// </summary>
    /// <param name="projectDirectory">Specifies the path of the project file to run (folder name or full path). If not specified, it defaults to the current directory.</param>
    /// <param name="onExit">EventHandler which is called when .NET application finishes its execution</param>
    public void RunProject(string projectDirectory, EventHandler? onExit);

    /// <summary>
    ///     Builds a .NET project in a specified path. See: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build
    /// </summary>
    /// <param name="projectDirectory">The project or solution file to build. If a project or solution file isn't specified, MSBuild searches the current working directory for a file that has a file extension that ends in either proj or sln and uses that file.</param>
    /// <param name="onExit">EventHandler which is called when build is finished</param>
    public void BuildProject(string projectDirectory, EventHandler? onExit);
}