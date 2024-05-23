using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICSharpCode.AvalonEdit.Document;

namespace Xansher.Model;

public partial class CodeFileModel : ObservableObject
{
    public CodeFileModel(string content)
    {
        Content = new TextDocument(content);
        Content.TextChanged += TextChanged;
    }
    
    public string? Name { get; set; }
    public string? Path { get; set; }

    [ObservableProperty] private TextDocument _content;

    private void TextChanged(object? sender, EventArgs eventArgs)
    {
        if (Path == null)
        {
            return;
        }
        try
        {
            File.WriteAllText(Path, Content.Text);
        }
        catch (DirectoryNotFoundException)
        {
        }
    }

}