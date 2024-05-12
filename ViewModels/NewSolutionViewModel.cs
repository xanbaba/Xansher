using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Xansher.Messages;
using Xansher.Model;
using Xansher.Services;
using Timer = System.Timers.Timer;

namespace Xansher.ViewModels;

public partial class NewSolutionViewModel : BaseViewModel
{
    private readonly IDirectoryDialogManager _directoryDialogManager;
    private readonly ICliManager _cliManager;

    public NewSolutionViewModel(IDirectoryDialogManager directoryDialogManager, ICliManager cliManager)
    {
        _directoryDialogManager = directoryDialogManager;
        _cliManager = cliManager;
        ProjectTemplates.Add(new ProjectTemplate("console", "Console Application"));
        ProjectTemplates.Add(new ProjectTemplate("wpf", "WPF Application"));

        SelectedProjectTemplate = ProjectTemplates.First();
    }

    [RelayCommand]
    private void SolutionDirectory()
    {
        var folderName = _directoryDialogManager.ShowOpenDirectoryDialog();
        if (folderName != null)
        {
            Directory = folderName;
        }
    }

    [RelayCommand]
    private void Back(Window? window)
    {
        window?.Close();
    }

    [RelayCommand]
    private void Create(Window? window)
    {
        if (Directory == string.Empty || ProjectName == string.Empty)
        {
            DisplayErrorMessage("Not all fields are filled!");
            return;
        }

        var projectDirectory = Directory + '\\' + ProjectName;
        System.IO.Directory.CreateDirectory(projectDirectory);
        var projectCreationOutput =
            _cliManager.CreateNewProject(SelectedProjectTemplate.CliIdentificator, projectDirectory);
        if (!projectCreationOutput)
        {
            DisplayErrorMessage(
                "An error occured while creating project. Ensure that a project with the same name does not exist in this directory");
        }

        WeakReferenceMessenger.Default.Send(new OpenProjectMessage(projectDirectory + '\\' + ProjectName + ".csproj"));
        Back(window);
    }

    private void DisplayErrorMessage(string errorMessage)
    {
        var timer = new Timer();
        timer.Interval = 5000;
        timer.AutoReset = false;
        timer.Elapsed += (_, _) => { ErrorMessage = string.Empty; };
        ErrorMessage = errorMessage;
        timer.Start();
    }

    [ObservableProperty] private string _directory = string.Empty;

    [ObservableProperty] private List<ProjectTemplate> _projectTemplates = [];

    [ObservableProperty] private ProjectTemplate _selectedProjectTemplate;

    [ObservableProperty] private string _projectName = string.Empty;

    [ObservableProperty] private string _errorMessage = string.Empty;
}