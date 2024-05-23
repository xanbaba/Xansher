using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Xansher.Messages;

namespace Xansher.ViewModels;

public partial class AddNewProjectElementViewModel : BaseViewModel
{
    [ObservableProperty] private string _fileName = string.Empty;
    
    public string? Path { get; set; }
    public bool IsDirectory { get; set; }
    
    [RelayCommand]
    private void CreateNewProjectElement(object? windowsObject)
    {
        if (windowsObject is not Window window) return;
        if (Path == null)
        {
            throw new ArgumentException($"Initialize {nameof(Path)} property!");
        }

        WeakReferenceMessenger.Default.Send(new AddProjectElementMessage(Path, FileName, IsDirectory));
        window.Close();
    }
}