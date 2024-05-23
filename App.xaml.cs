using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Xansher.Services;
using Xansher.ViewModels;
using Xansher.Views;

namespace Xansher;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public static ServiceProvider ServiceProvider { get; private set; } = null!;
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();
        services.AddTransient<NewSolutionView>();
        services.AddTransient<AddNewProjectElementView>();
        services.AddTransient<NewSolutionViewModel>();
        services.AddTransient<AddNewProjectElementViewModel>();
        services.AddTransient<IFileDialogManager, WindowsDialogManager>();
        services.AddTransient<IDirectoryDialogManager, WindowsDialogManager>();
        services.AddTransient<ICliManager, WindowsCliManager>();
        services.AddTransient<IFileManager, WindowsFileManager>();
        services.AddTransient<IDirectoryManager, WindowsDirectoryManager>();

        ServiceProvider = services.BuildServiceProvider();

        var mainView = ServiceProvider.GetService<MainView>()!;
        var mainViewModel = ServiceProvider.GetService<MainViewModel>()!;
        mainView.DataContext = mainViewModel;
        mainView.AddFileProjectElementClickCommand = mainViewModel.ShowAddNewFileProjectElementViewCommand;
        mainView.AddDirectoryProjectElementCommand = mainViewModel.ShowAddNewDirectoryProjectElementViewCommand;
        mainView.RemoveProjectElementCommand = mainViewModel.RemoveProjectElementCommand;
        

        mainView.ShowDialog();
        
        base.OnStartup(e);
    }
}