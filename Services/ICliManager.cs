using System.Diagnostics;

namespace Xansher.Services;

public interface ICliManager
{
    public bool CreateNewProject(string projectTemplate, string projectDirectory);
    
    public void RunProject(string projectDirectory);
}