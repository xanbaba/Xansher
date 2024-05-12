using Xansher.CustomControls;
using Xansher.Messages.MetaData;
using Xansher.Model;

namespace Xansher.Messages;

public class AddActiveDocumentMessage(AddActiveDocumentMessageMetaData activeDocumentMessageMetaData) : IMessage
{
    public CodeFileModel CodeFileModel { get; } = activeDocumentMessageMetaData.CodeFileModel;
    public ProjectElementButton ProjectElementButton { get; } = activeDocumentMessageMetaData.ProjectElementButton;
}