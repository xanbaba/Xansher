using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Messaging;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using Xansher.CustomControls;
using Xansher.Messages;

namespace Xansher.Views;

public partial class MainView
{
    public static readonly DependencyProperty AddFileProjectElementClickCommandProperty = DependencyProperty.Register(
        nameof(AddFileProjectElementClickCommand), typeof(ICommand), typeof(MainView),
        new PropertyMetadata(default(ICommand)));

    public ICommand? AddFileProjectElementClickCommand
    {
        get => (ICommand?)GetValue(AddFileProjectElementClickCommandProperty);
        set => SetValue(AddFileProjectElementClickCommandProperty, value);
    }

    public static readonly DependencyProperty AddDirectoryProjectElementCommandProperty = DependencyProperty.Register(
        nameof(AddDirectoryProjectElementCommand), typeof(ICommand), typeof(MainView), new PropertyMetadata(default(ICommand)));

    public ICommand? AddDirectoryProjectElementCommand
    {
        get => (ICommand?)GetValue(AddDirectoryProjectElementCommandProperty);
        set => SetValue(AddDirectoryProjectElementCommandProperty, value);
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

        ActiveFilesTabControl.SelectedIndex = 0;
    }

    private void AddFileProjectElementMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem) return;
        if (menuItem.Parent is not MenuItem parentMenuItem) return;
        if (parentMenuItem.Parent is not ContextMenu contextMenu) return;
        if (contextMenu.PlacementTarget is not ProjectElementButton projectElementButton) return;

        AddFileProjectElementClickCommand?.Execute(projectElementButton.ProjectElement.Path);
    }

    private void AddDirectoryProjectElementMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem) return;
        if (menuItem.Parent is not MenuItem parentMenuItem) return;
        if (parentMenuItem.Parent is not ContextMenu contextMenu) return;
        if (contextMenu.PlacementTarget is not ProjectElementButton projectElementButton) return;

        AddDirectoryProjectElementCommand?.Execute(projectElementButton.ProjectElement.Path);
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
        var editor = TextEditor();

        if (editor != null)
        {
            WeakReferenceMessenger.Default.Send(new ChangeCurrentActiveFileTextEditorMessage(editor));
        }
    }

    private void ActiveFilesTabControl_OnLoaded(object sender, RoutedEventArgs e)
    {
        var editor = TextEditor();

        if (editor == null)
        {
            return;
        }
        var highlighting = editor.SyntaxHighlighting;
        highlighting.GetNamedColor("NumberLiteral").Foreground = new SimpleHighlightingBrush(Colors.Plum);
        highlighting.GetNamedColor("Comment").Foreground = new SimpleHighlightingBrush(Colors.ForestGreen);
        highlighting.GetNamedColor("MethodCall").Foreground = new SimpleHighlightingBrush(Colors.DarkGoldenrod);
        highlighting.GetNamedColor("GetSetAddRemove").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("Visibility").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("ParameterModifiers").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("Modifiers").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("String").Foreground = new SimpleHighlightingBrush(Colors.Brown);
        highlighting.GetNamedColor("Char").Foreground = new SimpleHighlightingBrush(Colors.Red);
        highlighting.GetNamedColor("Preprocessor").Foreground = new SimpleHighlightingBrush(Colors.DarkGray);
        highlighting.GetNamedColor("TrueFalse").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("Keywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("ValueTypeKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("SemanticKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("NamespaceKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("ReferenceTypeKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("ThisOrBaseReference").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("NullOrValueKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("GotoKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("ContextKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("ExceptionKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("CheckedKeyword").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("UnsafeKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("OperatorKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        highlighting.GetNamedColor("SemanticKeywords").Foreground = new SimpleHighlightingBrush(Colors.Blue);
        
        editor.SyntaxHighlighting = null;
        editor.SyntaxHighlighting = highlighting;
    }

    private TextEditor? TextEditor()
    {
        TextEditor? editor = null;

        var dictionaryEnumerator = ActiveFilesTabControl.Resources.GetEnumerator();
        using var disposeDictionaryEnumerator = dictionaryEnumerator as IDisposable;
        while (dictionaryEnumerator.MoveNext())
        {
            if ((string)dictionaryEnumerator.Key == "TabControlContentTemplate")
            {
                if (dictionaryEnumerator.Value is not Grid grid)
                {
                    continue;
                }

                var children = grid.Children;
                foreach (UIElement child in children)
                {
                    editor = child as TextEditor;
                }
            }
        }

        return editor;
    }
}