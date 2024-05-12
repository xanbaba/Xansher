using System.Diagnostics;
using System.Windows;

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

    public void RunProject(string projectDirectory)
    {
        using var process = new Process();
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet.exe",
            Arguments = $"run --project {projectDirectory}"
        };
        process.EnableRaisingEvents = true;
        process.StartInfo = startInfo;
        process.Start();
    }
}