using System.Diagnostics;

namespace Xansher.Services;

public class WindowsCliManager : ICliManager
{
    public bool CreateNewProject(string projectTemplate, string projectDirectory)
    {
        using var process = new Process();
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet.exe",
            Arguments = $"new {projectTemplate} -o {projectDirectory}",
            WindowStyle = ProcessWindowStyle.Hidden
        };
        process.StartInfo = startInfo;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.Start();
        var error = process.StandardError.ReadToEnd();
        return string.IsNullOrWhiteSpace(error);
    }

    public void RunProject(string projectDirectory, EventHandler? onExit)
    {
        var process = new Process();

        var startInfo = new ProcessStartInfo
        {
            FileName = "CMD.exe",
            Arguments = $"/C dotnet run --project {projectDirectory} & Pause",
            RedirectStandardError = true
        };
        process.EnableRaisingEvents = true;
        process.StartInfo = startInfo;
        process.Exited += (sender, args) =>
        {
            onExit?.Invoke(sender, args);
            process.Dispose();
        };
        process.Start();
    }

    public void BuildProject(string projectDirectory, EventHandler? onExit)
    {
        var process = new Process();

        var startInfo = new ProcessStartInfo
        {
            FileName = "CMD.exe",
            Arguments = $"/C dotnet build {projectDirectory} & Pause",
            RedirectStandardError = true
        };
        process.EnableRaisingEvents = true;
        process.StartInfo = startInfo;
        process.Exited += (sender, args) =>
        {
            onExit?.Invoke(sender, args);
            process.Dispose();
        };
        process.Start();
    }
}