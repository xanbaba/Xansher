using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSharpCode.AvalonEdit;
using Xansher.CustomControls;
using Xansher.Messages;
using Xansher.Model;

namespace Xansher.Views;

public partial class MainView
{
    public static readonly DependencyProperty AddProjectElementClickCommandProperty = DependencyProperty.Register(
        nameof(AddProjectElementClickCommand), typeof(ICommand), typeof(MainView),
        new PropertyMetadata(default(ICommand)));

    public ICommand? AddProjectElementClickCommand
    {
        get => (ICommand)GetValue(AddProjectElementClickCommandProperty);
        set => SetValue(AddProjectElementClickCommandProperty, value);
    }

    public static readonly DependencyProperty RemoveProjectElementCommandProperty = DependencyProperty.Register(
        nameof(RemoveProjectElementCommand), typeof(ICommand), typeof(MainView),
        new PropertyMetadata(default(ICommand)));
    
    public ICommand? RemoveProjectElementCommand
    {
        get => (ICommand)GetValue(RemoveProjectElementCommandProperty);
        set => SetValue(RemoveProjectElementCommandProperty, value);
    }

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

    private void AddProjectElementMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem) return;
        if (menuItem.Parent is not ContextMenu contextMenu) return;
        if (contextMenu.PlacementTarget is not ProjectElementButton projectElementButton) return;

        AddProjectElementClickCommand?.Execute(projectElementButton.ProjectElement.Path);
    }

    private void RemoveProjectElementMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem) return;
        if (menuItem.Parent is not ContextMenu contextMenu) return;
        if (contextMenu.PlacementTarget is not ProjectElementButton projectElementButton) return;
        RemoveProjectElementCommand?.Execute(projectElementButton.ProjectElement);
    }

    private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Dispatcher.BeginInvoke(TabItem_UpdateHandler);
    }


    void TabItem_UpdateHandler()
    {
        if (ActiveFilesTabControl.Template.FindName("PART_SelectedContentHost", ActiveFilesTabControl) is
            not ContentPresenter myContentPresenter) return;
        
        myContentPresenter.ApplyTemplate();
        if (myContentPresenter.ContentTemplate.FindName("TextEditor", myContentPresenter) is TextEditor textEditor)
        {
            WeakReferenceMessenger.Default.Send(new ChangeCurrentActiveFileTextEditorMessage(textEditor));
        }
    }
}