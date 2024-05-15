using ICSharpCode.AvalonEdit;

namespace Xansher.Messages;

public class ChangeCurrentActiveFileTextEditorMessage(TextEditor textEditor) : IMessage
{
    public TextEditor TextEditor { get; } = textEditor;
}