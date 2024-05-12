using System.IO;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Xansher.CustomControls;
using Xansher.Messages;
using Xansher.Messages.MetaData;

namespace Xansher.Model;

public partial class ProjectElement
{
    public string? Name { get; init; }
    public string? Path { get; init; }
    public List<ProjectElement>? ProjectElements { get; set; }
    public bool IsDirectory { get; set; }
    
    [RelayCommand]
    private void OpenFileInEditor(ProjectElementButton button)
    {
        if (button.ProjectElement.IsDirectory)
        {
            return;
        }
        if (button.ProjectElement.Path == null)
        {
            return;
        }
        WeakReferenceMessenger.Default.Send(new AddActiveDocumentMessage(new AddActiveDocumentMessageMetaData
        {
            CodeFileModel = new CodeFileModel(File.ReadAllText(button.ProjectElement.Path))
            {
                Name = button.ProjectElement.Name,
                Path = button.ProjectElement.Path
            },
            ProjectElementButton = button
        }));
    }
}