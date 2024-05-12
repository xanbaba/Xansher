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
        services.AddTransient<NewSolutionViewModel>();
        services.AddTransient<IFileDialogManager, WindowsDialogManager>();
        services.AddTransient<IDirectoryDialogManager, WindowsDialogManager>();
        services.AddTransient<ICliManager, WindowsCliManager>();

        ServiceProvider = services.BuildServiceProvider();

        MainWindow = ServiceProvider.GetService<MainView>()!;
        MainWindow.DataContext = ServiceProvider.GetService<MainViewModel>();

        MainWindow.ShowDialog();
        
        base.OnStartup(e);
    }
}