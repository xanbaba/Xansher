using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Xansher.ViewModels;
using Xansher.Views;

namespace Xansher;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();

        var serviceProvider = services.BuildServiceProvider();

        MainWindow = serviceProvider.GetService<MainView>()!;
        MainWindow.DataContext = serviceProvider.GetService<MainViewModel>();

        MainWindow.ShowDialog();
        
        base.OnStartup(e);
    }
}