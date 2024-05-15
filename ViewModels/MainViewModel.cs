using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSharpCode.AvalonEdit;
using Microsoft.Extensions.DependencyInjection;
using Xansher.Messages;
using Xansher.Model;
using Xansher.Services;
using Xansher.Views;

namespace Xansher.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IFileDialogManager _fileDialogManager;
    private readonly ICliManager _cliManager;
    private readonly IFileManager _fileManager;
    private readonly IDirectoryManager _directoryManager;
    [ObservableProperty] private ObservableCollection<CodeFileModel> _activeFiles = [];
    [ObservableProperty] private TextEditor? _selectedActiveFileCodeEditor;
    [ObservableProperty] private ObservableCollection<ProjectElement> _projectElements = [];
    [ObservableProperty] private string _errorMessage = string.Empty;
    private TextEditor? _currentActiveFileTextEditor;
    private string? _projectDirectory;

    public MainViewModel(IFileDialogManager fileDialogManager, ICliManager cliManager, IFileManager fileManager, IDirectoryManager directoryManager)
    {
        _fileDialogManager = fileDialogManager;
        _cliManager = cliManager;
        _fileManager = fileManager;
        _directoryManager = directoryManager;
        WeakReferenceMessenger.Default.Register<AddActiveDocumentMessage>(this, AddActiveFile);
        WeakReferenceMessenger.Default.Register<OpenProjectMessage>(this, (_, message) =>
        {
            LoadProject(message.Path);
        });
        WeakReferenceMessenger.Default.Register<AddProjectElementMessage>(this, AddNewProjectElementToDirectory);
        WeakReferenceMessenger.Default.Register<ChangeCurrentActiveFileTextEditorMessage>(this, ChangeCurrentActiveFileTextEditor);
        WeakReferenceMessenger.Default.Register<RefreshProjectElementsMessage>(this, (_, _) =>
        {
            LoadProjectElements();
        });
    }

    private void ChangeCurrentActiveFileTextEditor(object recipient, ChangeCurrentActiveFileTextEditorMessage message)
    {
        _currentActiveFileTextEditor = message.TextEditor;
    }

    private void AddNewProjectElementToDirectory(object recipient, AddProjectElementMessage message)
    {
        _fileManager.CreateFile(message.Path, message.FileName);
        LoadProjectElements();
    }

    private void AddActiveFile(object recipient, AddActiveDocumentMessage message)
    {
        if (ActiveFiles.Any(x => x.Path == message.CodeFileModel.Path))
        {
            return;
        }

        ActiveFiles.Add(message.CodeFileModel);
    }

    [RelayCommand]
    private void Run()
    {
        if (_projectDirectory == null)
        {
            return;
        }

        _cliManager.RunProject(_projectDirectory, (sender, _) =>
        {
            if (sender is Process process)
            {
                ErrorMessage = process.StandardError.ReadToEnd();
            }

            Application.Current.Dispatcher.Invoke(LoadProjectElements);
        });
        
    }

    [RelayCommand]
    private void Cut()
    {
        _currentActiveFileTextEditor?.Cut();
    }
    
    [RelayCommand]
    private void Copy()
    {
        _currentActiveFileTextEditor?.Copy();
    }
    
    [RelayCommand]
    private void Paste()
    {
        _currentActiveFileTextEditor?.Paste();
    }
    
    [RelayCommand]
    private void Undo()
    {
        _currentActiveFileTextEditor?.Undo();
    }
    
    [RelayCommand]
    private void Redo()
    {
        _currentActiveFileTextEditor?.Redo();
    }
    
    [RelayCommand]
    private void ShowAddNewProjectElementView(string? path)
    {
        var addProjectElementView = App.ServiceProvider.GetService<AddNewProjectElementView>()!;
        var addNewProjectElementViewModel = App.ServiceProvider.GetService<AddNewProjectElementViewModel>()!;
        addNewProjectElementViewModel.Path = path;
        addProjectElementView.DataContext = addNewProjectElementViewModel;
        addProjectElementView.ShowDialog();
    }

    [RelayCommand]
    private void RemoveProjectElement(ProjectElement projectElement)
    {
        if (projectElement.Path == null)
        {
            return;
        }
        if (projectElement.IsDirectory)
        {
            _directoryManager.RemoveDirectory(projectElement.Path);
        }
        else
        {
            _fileManager.Remove(projectElement.Path);
        }
        LoadProjectElements();
    }

    [RelayCommand]
    private void RemoveTab(object tab)
    {
        if (tab is CodeFileModel codeFileModel)
        {
            ActiveFiles.Remove(codeFileModel);
        }
    }

    [RelayCommand]
    private void NewSolution()
    {
        var newSolutionView = App.ServiceProvider.GetService<NewSolutionView>()!;
        newSolutionView.DataContext = App.ServiceProvider.GetService<NewSolutionViewModel>();
        newSolutionView.ShowDialog();
    }

    [RelayCommand]
    private void OpenProject()
    {
        var fileName = _fileDialogManager.ShowOpenFileDialog("Project File|*.csproj");
        LoadProject(fileName);
    }

    

    private void LoadProject(string? fileName)
    {
        if (fileName == null) return;

        var directoryPath = fileName.Split('\\');
        var directory = string.Join('\\', directoryPath.SkipLast(1));
        _projectDirectory = directory;
        LoadProjectElements();
    }

    private void LoadProjectElements()
    {
        if (_projectDirectory == null) return;

        ProjectElements.Clear();
        ProjectElements.Add(new ProjectElement
        {
            Name = _projectDirectory.Split('\\')[^1],
            Path = _projectDirectory,
            IsDirectory = true
        });
        AddProjectElementsToDirectory(ProjectElements.First());
    }

    private void AddProjectElementsToDirectory(ProjectElement projectElement)
    {
        projectElement.ProjectElements = [];

        // Add directories
        AddDirectory(projectElement);

        // Add Files
        AddFiles(projectElement);
    }

    private static void AddFiles(ProjectElement projectElement)
    {
        if (projectElement.Path == null) throw new ArgumentException("Path is null");
        if (projectElement.ProjectElements == null) throw new ArgumentException("ProjectElements is null");

        var files = Directory.GetFiles(projectElement.Path);
        foreach (var file in files)
        {
            projectElement.ProjectElements.Add(new ProjectElement
            {
                Name = file.Split('\\').Last(),
                Path = file,
                IsDirectory = false,
            });
        }
    }

    private void AddDirectory(ProjectElement projectElement)
    {
        if (projectElement.Path == null) throw new ArgumentException("Path is null");
        if (projectElement.ProjectElements == null) throw new ArgumentException("ProjectElements is null");

        var directories = Directory.GetDirectories(projectElement.Path);
        foreach (var directory in directories)
        {
            projectElement.ProjectElements.Add(new ProjectElement
            {
                Name = directory.Split('\\').Last(),
                Path = directory,
                IsDirectory = true
            });
            AddProjectElementsToDirectory(projectElement.ProjectElements.Last());
        }
    }
}