using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xansher.Model;

namespace Xansher.CustomControls;

public class ProjectElementButton : Button
{
    public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.Register(
        nameof(DoubleClickCommand), typeof(ICommand), typeof(ProjectElementButton), new PropertyMetadata(default(ICommand)));

    public ICommand DoubleClickCommand
    {
        get => (ICommand)GetValue(DoubleClickCommandProperty);
        set => SetValue(DoubleClickCommandProperty, value);
    }

    public static readonly DependencyProperty ProjectElementProperty = DependencyProperty.Register(
        nameof(ProjectElement), typeof(ProjectElement), typeof(ProjectElementButton), new PropertyMetadata(default(ProjectElement)));

    public ProjectElement ProjectElement
    {
        get => (ProjectElement)GetValue(ProjectElementProperty);
        set => SetValue(ProjectElementProperty, value);
    }
}