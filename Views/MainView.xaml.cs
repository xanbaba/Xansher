using System.Windows;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;
using Xansher.CustomControls;

namespace Xansher.Views;

public partial class MainView
{
    public MainView()
    {
        InitializeComponent();
    }

    private void ProjectElement_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is ProjectElementButton button)
        {
            button.DoubleClickCommand.Execute(button);
        }
    }
}