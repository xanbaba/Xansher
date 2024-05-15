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

        try
        {
            var fileContent = File.ReadAllText(button.ProjectElement.Path);
            WeakReferenceMessenger.Default.Send(new AddActiveDocumentMessage(new AddActiveDocumentMessageMetaData
            {
                CodeFileModel = new CodeFileModel(fileContent)
                {
                    Name = button.ProjectElement.Name,
                    Path = button.ProjectElement.Path
                },
                ProjectElementButton = button
            }));
        }
        catch (FileNotFoundException)
        {
            WeakReferenceMessenger.Default.Send<RefreshProjectElementsMessage>();
        }
    }

    [RelayCommand]
    private void Add(object projectElementButton)
    {
        Console.WriteLine("!");
    }
}